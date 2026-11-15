#!/usr/bin/env node
// Shrinks exported pixelfx sheets in place (called by export.sh after the copy):
//   1. keeps every STEP-th frame and multiplies its duration by STEP (same total time, fewer frames),
//   2. crops all frames to the union of their non-transparent pixels (pivot moved to match),
//   3. repacks the grid and writes an indexed PNG (palette + tRNS) when the sheet has <= 256 colours,
//      otherwise snaps colours to 5-5-5-4 bits first. Needs ffmpeg to decode the source PNG.
// usage: node tools/fx/shrink.mjs <sheetsDir> [name:step ...]   e.g. holy-pillar:2 implosion:2
import { readFileSync, writeFileSync } from "node:fs";
import { spawnSync } from "node:child_process";
import { deflateSync } from "node:zlib";
import { join } from "node:path";

const dir = process.argv[2], specs = process.argv.slice(3).map((s) => s.split(":"));
const CRC = new Int32Array(256);
for (let n = 0; n < 256; n++) { let c = n; for (let k = 0; k < 8; k++) c = c & 1 ? 0xedb88320 ^ (c >>> 1) : c >>> 1; CRC[n] = c; }
const crc = (b) => { let c = -1; for (const v of b) c = CRC[(c ^ v) & 255] ^ (c >>> 8); return (c ^ -1) >>> 0; };
const chunk = (t, d) => { const l = Buffer.alloc(4), b = Buffer.concat([Buffer.from(t), d]), c = Buffer.alloc(4); l.writeUInt32BE(d.length); c.writeUInt32BE(crc(b)); return Buffer.concat([l, b, c]); };

function indexedPng(w, h, rgba, pal, idx) {
  const raw = Buffer.alloc((w + 1) * h);
  for (let y = 0; y < h; y++) { raw[y * (w + 1)] = 0; for (let x = 0; x < w; x++) raw[y * (w + 1) + 1 + x] = idx[y * w + x]; }
  const ih = Buffer.alloc(13); ih.writeUInt32BE(w, 0); ih.writeUInt32BE(h, 4); ih[8] = pal.length <= 16 ? 8 : 8; ih[9] = 3;
  const plte = Buffer.alloc(pal.length * 3), trns = Buffer.alloc(pal.length);
  pal.forEach((c, i) => { plte[i * 3] = c >>> 24; plte[i * 3 + 1] = (c >>> 16) & 255; plte[i * 3 + 2] = (c >>> 8) & 255; trns[i] = c & 255; });
  return Buffer.concat([Buffer.from([137, 80, 78, 71, 13, 10, 26, 10]), chunk("IHDR", ih), chunk("PLTE", plte), chunk("tRNS", trns), chunk("IDAT", deflateSync(raw, { level: 9 })), chunk("IEND", Buffer.alloc(0))]);
}

for (const [name, stepS] of specs) {
  const step = +stepS || 1, jf = join(dir, name + ".json"), pf = join(dir, name + ".png");
  const j = JSON.parse(readFileSync(jf, "utf8")), W = j.meta.size.w, H = j.meta.size.h;
  const dec = spawnSync("ffmpeg", ["-v", "error", "-i", pf, "-f", "rawvideo", "-pix_fmt", "rgba", "-"], { maxBuffer: 1 << 30 });
  if (dec.status !== 0) throw new Error("ffmpeg: " + dec.stderr);
  const src = dec.stdout, fw = j.frames[0].frame.w, fh = j.frames[0].frame.h;
  const keep = j.frames.map((f, i) => i).filter((i) => i % step === 0);
  let x0 = fw, y0 = fh, x1 = -1, y1 = -1;
  for (const i of keep) { const f = j.frames[i].frame; for (let y = 0; y < fh; y++) for (let x = 0; x < fw; x++) if (src[((f.y + y) * W + f.x + x) * 4 + 3]) { if (x < x0) x0 = x; if (x > x1) x1 = x; if (y < y0) y0 = y; if (y > y1) y1 = y; } }
  // keep the pivot inside the crop
  const pv = j.meta.pixelfx.pivot; x0 = Math.min(x0, pv[0]); y0 = Math.min(y0, pv[1]); x1 = Math.max(x1, pv[0]); y1 = Math.max(y1, pv[1]);
  const cw = x1 - x0 + 1, ch = y1 - y0 + 1, n = keep.length;
  const cols = Math.min(n, Math.max(1, Math.ceil(Math.sqrt((n * ch) / cw)))), rows = Math.ceil(n / cols), nw = cols * cw, nh = rows * ch;
  const out = new Uint32Array(nw * nh);
  const quant = (name_) => name_; void quant;
  keep.forEach((i, k) => {
    const f = j.frames[i].frame, ox = (k % cols) * cw, oy = Math.floor(k / cols) * ch;
    for (let y = 0; y < ch; y++) for (let x = 0; x < cw; x++) {
      const o = ((f.y + y0 + y) * W + f.x + x0 + x) * 4, a = src[o + 3];
      out[(oy + y) * nw + ox + x] = a ? (((src[o] << 24) | (src[o + 1] << 16) | (src[o + 2] << 8) | a) >>> 0) : 0;
    }
  });
  let uniq = new Set(out);
  let snapped = false;
  if (uniq.size > 256) {
    // snap to 4 bits per channel step by step until <= 256 colours
    for (const bits of [7, 6, 5, 4, 3]) {
      const L = (1 << bits) - 1, q = (v) => Math.round((Math.round((v * L) / 255) * 255) / L);
      for (let p = 0; p < out.length; p++) { const c = out[p]; if (c) out[p] = (((q(c >>> 24) << 24) | (q((c >>> 16) & 255) << 16) | (q((c >>> 8) & 255) << 8) | Math.max(1, q(c & 255))) >>> 0); }
      uniq = new Set(out); snapped = bits; if (uniq.size <= 256) break;
    }
  }
  if (uniq.size > 256) throw new Error(name + ": still " + uniq.size + " colours");
  const pal = [...uniq]; const lut = new Map(pal.map((c, i) => [c, i])); const idx = new Uint8Array(out.length);
  for (let p = 0; p < out.length; p++) idx[p] = lut.get(out[p]);
  const png = indexedPng(nw, nh, null, pal, idx);
  writeFileSync(pf, png);
  const px = j.meta.pixelfx, dur = j.frames[0].duration * step;
  j.frames = keep.map((_, k) => ({ filename: `${name} ${k}`, frame: { x: (k % cols) * cw, y: Math.floor(k / cols) * ch, w: cw, h: ch }, rotated: false, trimmed: false, spriteSourceSize: { x: 0, y: 0, w: cw, h: ch }, sourceSize: { w: cw, h: ch }, duration: dur }));
  j.meta.size = { w: nw, h: nh }; j.meta.frameTags = [{ name, from: 0, to: n - 1, direction: "forward" }];
  Object.assign(px, { fps: Math.round(px.fps / step), frames: n, frameSize: [cw, ch], pivot: [pv[0] - x0, pv[1] - y0], columns: cols });
  writeFileSync(jf, JSON.stringify(j, null, 1));
  console.log(`${name}: ${keep.length} frames ${cw}x${ch} (was ${fw}x${fh}), ${pal.length} colours${snapped ? " (snapped to " + snapped + " bits)" : ""}, ${png.length} B`);
}
