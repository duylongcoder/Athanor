#!/usr/bin/env node
// HD-2D DepthTest layer art (task 5b-04): pre-blurred far layers, two near foreground corners, two mist layers.
// Writes Athanor/Assets/Art/Depth/*.png and the sharp corner sources to tools/art/out/. Also called by export.mjs.
//   node tools/art/export-depth.mjs [contactSheetPath]
import { writeFileSync, mkdirSync, readdirSync, statSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';
import { Canvas, Spr, R, hsh } from './painter.mjs';
import { blur } from './blur.mjs';
import * as S from './scene.mjs';
const here = dirname(fileURLToPath(import.meta.url)), depthDir = join(here, '../../Athanor/Assets/Art/Depth'), outDir = join(here, 'out');
export const FAR_SIGMA = 0.5, NEAR_SIGMA = 3, NEAR_PAD = 6, FAR_PAD = 2;

/* ---- foreground corners, art-pixel local coordinates: left art x -96..144, right 514..736, y 214..306; the board top sits at y 46 (art 260) ---- */
const INKC = '#120b16';
function board(s) {                                    // lighter oak than the stand-in's near-black underside
  const W = s.w;
  s.rect(0, 46, W, 2, R.wood[4]); s.rect(0, 48, W, 12, R.wood[3]); s.rect(0, 60, W, 14, R.wood[2]); s.rect(0, 74, W, 18, R.wood[1]);
  for (let y = 48; y < 92; y++) for (let x = 0; x < W; x++) if (hsh(x, y, 9) > 0.94) s.px(x, y, y < 74 ? R.wood[2] : R.wood[0]);
  for (let x = 0; x < W; x += 3) if (hsh(x, 1, 5) > 0.5) s.px(x, 47, R.wood[3]);
}
function glassBody(s, x, y, w, h, tint, round) {       // bottle body, lit from the left
  if (round) s.ball(x + w / 2, y + h / 2, w / 2, h / 2, tint);
  else s.rect(x, y, w, h, (px) => tint[px < x + w * 0.3 ? 3 : px < x + w * 0.7 ? 2 : 1]);
  s.px(x + 2, y + 3, tint[4]); s.px(x + 2, y + 4, tint[4]);
}
function bottle(s, cx, base, w, h, tint, round = false, neck = 5) {
  const bh = round ? w : h - 12;
  glassBody(s, cx - w / 2, base - bh, w, bh, tint, round);
  s.rect(cx - neck / 2, base - bh - 9, neck, 10, (px) => GL(px < cx));
  s.rect(cx - neck / 2 - 1, base - bh - 12, neck + 2, 4, (px) => R.cork[px < cx ? 3 : 2]);
}
const GL = (l) => (l ? '#9cc0d0' : '#5a7890');
function books(s, x, base, specs) {
  let y = base;
  for (const [w, h, K, off] of specs) { y -= h; s.rect(x + off, y, w, h, K[2]); s.rect(x + off, y, w, 1, K[3]); s.rect(x + off, y + h - 1, w, 1, K[0]); s.rect(x + off, y, 2, h, K[3]); s.rect(x + off + 4, y + 2, w - 8, 1, R.gold[3]); s.rect(x + off + w - 3, y + 1, 2, h - 2, R.bone[3]); }
}
function mortar(s, cx, base) {
  s.rect(cx - 8, base - 3, 16, 4, R.stone[1]);
  s.ball(cx, base - 12, 19, 14, R.stone, { clip: (x, y) => y > base - 20 });
  s.ell(cx, base - 21, 19, 4, (nx, ny) => (ny < 0 ? R.stone[3] : R.stone[4])); s.ell(cx, base - 21, 15, 2.6, () => R.stone[0]);
  s.tube((t) => [cx - 2 + t * 22, base - 20 - t * 26], () => 3, (t, nx) => R.bone[nx < 0 ? 3 : 2], 24);
  s.ell(cx - 2, base - 20, 4, 3.4, (nx) => R.bone[nx < 0 ? 3 : 2]);
}
function shard(s, cx, base, w, h, lean, ramp) {
  s.poly([[cx - w / 2, base], [cx - w / 2 + lean * 0.6, base - h * 0.78], [cx + lean, base - h], [cx + w / 2 + lean * 0.6, base - h * 0.78], [cx + w / 2, base]],
    (x, y) => ramp[y < base - h * 0.8 ? 4 : x < cx + lean * 0.4 ? 3 : 1]);
  s.rect(Math.round(cx + lean * 0.4), Math.round(base - h * 0.75), 1, Math.round(h * 0.75), ramp[2]);
}
function rock(s, cx, base, rx, ry) { s.ball(cx, base - ry + 3, rx, ry, R.stone, { facet: 3, clip: (x, y) => y < base + 3 }); }

/** Left corner (240x92): oak board, stacked books, a mortar and pestle, a round pink flask and a tall teal bottle. Ends at local x 226, clear of the table candle (art 150 = local 246). */
export function cornerLeft() {
  const s = new Spr(240, 92);
  books(s, 12, 46, [[56, 8, R.red, 0], [50, 7, R.blue, 4], [44, 7, R.green, 1], [36, 6, R.violet, 5]]);
  mortar(s, 104, 48);
  bottle(s, 152, 46, 24, 24, R.pink, true, 7);
  bottle(s, 196, 46, 22, 38, R.teal, false, 7);
  s.outline(); board(s);
  return s.canvas();
}
/** Right corner (222x92): a violet crystal cluster, rocks, a short book pile and a blue flask. */
export function cornerRight() {
  const s = new Spr(222, 92);
  shard(s, 22, 48, 14, 36, -3, R.violet); shard(s, 38, 48, 18, 44, 2, R.violet); shard(s, 54, 48, 12, 26, 4, R.violet); shard(s, 66, 48, 8, 18, 3, R.pink);
  rock(s, 92, 48, 14, 10); rock(s, 112, 48, 10, 7);
  books(s, 130, 46, [[40, 8, R.wood, 0], [36, 7, R.gold, 3], [30, 7, R.red, 1]]);
  bottle(s, 190, 46, 26, 26, R.blue, true, 7);
  s.outline(); board(s);
  return s.canvas();
}

/* ---- mist: the mockup's fogCanvas, white + alpha, 4 alpha steps, 4x4 Bayer, tileable in x ---- */
export function fog(w, h, seed) {
  const cv = new Canvas(w, h), bayer = [0, 8, 2, 10, 12, 4, 14, 6, 3, 11, 1, 9, 15, 7, 13, 5];
  const hash = (x, y) => { const q = Math.sin(x * 127.1 + y * 311.7 + seed * 74.7) * 43758.5453; return q - Math.floor(q); };
  const noise = (x, y, p) => { const xi = Math.floor(x), yi = Math.floor(y), fx = x - xi, fy = y - yi, sx = fx * fx * (3 - 2 * fx), sy = fy * fy * (3 - 2 * fy);
    const v = (a, b) => hash(((a % p) + p) % p, b); return (v(xi, yi) * (1 - sx) + v(xi + 1, yi) * sx) * (1 - sy) + (v(xi, yi + 1) * (1 - sx) + v(xi + 1, yi + 1) * sx) * sy; };
  for (let y = 0; y < h; y++) for (let x = 0; x < w; x++) {
    let n = 0, amp = 0.55, fq = 1;
    for (let o = 0; o < 4; o++) { const p = 6 * fq; n += amp * noise((x / w) * p, (y / h) * 3 * fq + o * 7, p); amp *= 0.5; fq *= 2; }
    const band = Math.sin(Math.PI * y / h); let v = Math.max(0, n * 1.25 - 0.3) * band * band;
    v = Math.floor(v * 4 + bayer[(y % 4) * 4 + (x % 4)] / 16) / 4;
    const a = Math.min(255, Math.round(v * 255)); if (a > 0) cv.set(x, y, 255, 255, 255, a);
  }
  return cv;
}

/** Builds every Depth texture. Returns { name: Canvas } for the shipped ones and { sharp: {...} } for the unshipped sources. */
export function buildDepth() {
  const o = {};
  o.workshop_wall_far = blur(S.benchWall(), FAR_SIGMA, 0, true);
  for (let d = 1; d <= 5; d++) { const v = S.windowView(d), b = v.bounds(); o[`window_d${d}_far`] = blur(v.crop(b.x, b.y, b.w, b.h), FAR_SIGMA, FAR_PAD); }
  o.candle_far = blur(S.candle().canvas(), FAR_SIGMA, FAR_PAD);
  for (let k = 0; k < 8; k++) o[`flame_small_far_${k}`] = blur(S.flame(false, k * 0.171, 0.3), FAR_SIGMA, FAR_PAD);
  const sharp = { fg_corner_left_sharp: cornerLeft(), fg_corner_right_sharp: cornerRight() };
  o.fg_corner_left_near = blur(sharp.fg_corner_left_sharp, NEAR_SIGMA, NEAR_PAD);
  o.fg_corner_right_near = blur(sharp.fg_corner_right_sharp, NEAR_SIGMA, NEAR_PAD);
  o.fog_back = fog(320, 150, 1); o.fog_front = fog(320, 110, 2);
  return { o, sharp };
}
export function writeDepth() {
  const { o, sharp } = buildDepth();
  mkdirSync(depthDir, { recursive: true }); mkdirSync(outDir, { recursive: true });
  for (const [n, cv] of Object.entries(o)) writeFileSync(join(depthDir, n + '.png'), cv.png());
  for (const [n, cv] of Object.entries(sharp)) writeFileSync(join(outDir, n + '.png'), cv.png());
  return { count: Object.keys(o).length, o, sharp };
}

function contact(path, o, sharp) {
  const WIDTH = 1300, k = 2, G = 8, big = new Canvas(WIDTH, 3400);
  const tile = (c) => { const t = new Canvas(c.w * 2, c.h); t.drawImage(c, 0, 0); t.drawImage(c, c.w, 0); return t; };
  const items = [];
  for (const [n, c] of Object.entries(o)) if (n !== 'workshop_wall_far' && !n.startsWith('fg_') && !n.startsWith('fog_')) items.push(c);
  items.push(sharp.fg_corner_left_sharp, o.fg_corner_left_near, sharp.fg_corner_right_sharp, o.fg_corner_right_near, tile(o.fog_back), tile(o.fog_front));
  const flow = (y0, draw) => {
    let x = G, y = y0, rowH = 0;
    for (const c of items) { const w = c.w * k; if (x + w > WIDTH - G && x > G) { x = G; y += rowH + G; rowH = 0; } if (draw) big.drawImage(c, x, y, w, c.h * k); x += w + G; rowH = Math.max(rowH, c.h * k); }
    return y + rowH + G;
  };
  let y = 0;
  const wall = o.workshop_wall_far;
  big.fillStyle = '#1a1220'; big.fillRect(0, y, WIDTH, wall.h * k + 2 * G); big.drawImage(wall, G, y + G, wall.w * k, wall.h * k); y += wall.h * k + 2 * G;
  for (const g of ['#15101c', '#7a7684']) {
    const h = flow(y, false) - y; big.fillStyle = g; big.fillRect(0, y, WIDTH, h); flow(y + G, true); y += h + G;
  }
  mkdirSync(dirname(path), { recursive: true }); writeFileSync(path, big.crop(0, 0, WIDTH, y).png());
}
if (process.argv[1] === fileURLToPath(import.meta.url)) {
  const r = writeDepth();
  console.log(`depth: ${r.count} textures written`);
  if (process.argv[2]) {
    contact(process.argv[2], r.o, r.sharp);
    const names = readdirSync(depthDir).filter((f) => f.endsWith('.png')).sort(); let tot = 0;
    const lines = names.map((f) => { const b = statSync(join(depthDir, f)).size; tot += b; return `${f.padEnd(28)} ${String(b).padStart(8)} B`; });
    lines.push('-'.repeat(40), `${'TOTAL'.padEnd(28)} ${String(tot).padStart(8)} B`, `SPEC set A estimate         283349 B  (delta ${tot - 283349})`, `Left for 5b                1100387 B  (remaining after this ${1100387 - tot})`);
    writeFileSync(join(dirname(process.argv[2]), 'bytes.txt'), lines.join('\n') + '\n');
    console.log(lines.slice(-4).join('\n'));
  }
}
