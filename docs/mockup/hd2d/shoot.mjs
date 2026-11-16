// Screenshots and checks of the HD-2D mockup (Phase 5b-01) in headless Chrome over CDP (pattern of tools/web/rotate.mjs).
// Node 22+, Chrome.     node docs/mockup/hd2d/shoot.mjs <outDir>
// Writes 1280x720 PNGs of the stage (world at 2x under the 640x360 HUD), page-controls.png (the whole page with the panel),
// textures.txt (PNG bytes of hd2d.exportTextures()), console.txt:
//   01..11  effects, depth 1, t = 2 s: all 8 on, each one off in turn, all off, flat
//   12..16  one frame per depth preset 1..5, all effects on
//   17..19  camera, depth 1: sway on at t = 1.5 s (camera right) and t = 4.5 s (camera left), shake at t = 2 s
// What is compared with what (HUD pixels = screen pixels under the opaque HUD pixels of that depth's hud canvas):
//   01..11  HUD pixels against 01-all-on; every "off" frame must differ from 01 in the world; 10-all-off = 11-flat
//   12..16  HUD pixels against an unsaved all-effects-off frame of the SAME depth (the HUD text changes with the depth,
//           so depths are never compared with each other); page values = that depth's preset; restdiff = 0 at each
//           depth; 5 different HUD hashes over the 5 depths; 12-depth-1 = 01-all-on; an unsaved depth 1 frame taken
//           after depth 5 = 01-all-on (presets and HUD survive the round trip)
//   17..19  HUD pixels against 01-all-on (same depth, no sway, no shake); the world must differ from the frame at rest
//           at the same time (unsaved for the sway frames, 01-all-on for the shake frame)
// Exit 0 only with 0 console errors and every check ok.
import { spawn } from 'node:child_process';
import { mkdirSync, writeFileSync, mkdtempSync, existsSync, readFileSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join, dirname, resolve } from 'node:path';
import { fileURLToPath, pathToFileURL } from 'node:url';
import { inflateSync } from 'node:zlib';

const outDir = process.argv[2];
if (!outDir) { console.error('usage: node shoot.mjs <outDir>'); process.exit(2); }
mkdirSync(outDir, { recursive: true });
const here = dirname(fileURLToPath(import.meta.url));
if (!existsSync(join(here, 'assets.js'))) { console.error('assets.js missing: run bash docs/mockup/hd2d/build.sh'); process.exit(2); }
const url = pathToFileURL(resolve(here, 'index.html')).href + '?still=1&t=2';

const chromeBin = () => process.env.CHROME_BIN
  || ['/Applications/Google Chrome.app/Contents/MacOS/Google Chrome', '/usr/bin/google-chrome', '/usr/bin/google-chrome-stable', '/usr/bin/chromium']
    .find((p) => existsSync(p)) || 'google-chrome';

// minimal PNG decoder (8-bit RGB/RGBA, not interlaced: what Chrome writes)
function decodePng(buf) {
  let p = 8, w = 0, h = 0, ct = 0; const idat = [];
  while (p < buf.length) {
    const len = buf.readUInt32BE(p), type = buf.toString('ascii', p + 4, p + 8), data = buf.subarray(p + 8, p + 8 + len);
    if (type === 'IHDR') { w = data.readUInt32BE(0); h = data.readUInt32BE(4); ct = data[9]; if (data[8] !== 8 || data[12]) throw new Error('png: unsupported'); }
    if (type === 'IDAT') idat.push(data);
    p += 12 + len;
  }
  const bpp = ct === 6 ? 4 : 3, raw = inflateSync(Buffer.concat(idat)), stride = w * bpp, out = Buffer.alloc(w * h * 4);
  let prev = Buffer.alloc(stride);
  for (let y = 0; y < h; y++) {
    const f = raw[y * (stride + 1)], line = Buffer.from(raw.subarray(y * (stride + 1) + 1, (y + 1) * (stride + 1)));
    for (let i = 0; i < stride; i++) {
      const a = i >= bpp ? line[i - bpp] : 0, b = prev[i], c = i >= bpp ? prev[i - bpp] : 0;
      const pa = Math.abs(b - c), pb = Math.abs(a - c), pc = Math.abs(a + b - 2 * c);
      line[i] = (line[i] + [0, a, b, (a + b) >> 1, pa <= pb && pa <= pc ? a : pb <= pc ? b : c][f]) & 255;
    }
    for (let x = 0; x < w; x++) for (let k = 0; k < 4; k++) out[(y * w + x) * 4 + k] = k < bpp ? line[x * bpp + k] : 255;
    prev = line;
  }
  return { w, h, px: out };
}

const port = 9336, profile = mkdtempSync(join(tmpdir(), 'athanor-hd2d-'));
const chrome = spawn(chromeBin(), ['--headless=new', `--remote-debugging-port=${port}`, `--user-data-dir=${profile}`, '--window-size=1640,900',
  '--no-first-run', '--mute-audio', '--hide-scrollbars', '--force-color-profile=srgb', 'about:blank'], { stdio: 'ignore' });
const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
const all = [], errors = [];
let ws, id = 0; const pending = new Map();
const send = (method, params = {}) => new Promise((res, rej) => { const i = ++id; pending.set(i, { res, rej }); ws.send(JSON.stringify({ id: i, method, params })); });
const fails = [];
let checks = 0;
const expect = (ok, what) => { checks++; console.log((ok ? '  ok   ' : '  FAIL ') + what); if (!ok) fails.push(what); };

const FX = ['tilt', 'far', 'near', 'fg', 'fogb', 'fogf', 'bloom', 'vig'];   // must equal hd2d.FX (checked below)
const OFF = Object.fromEntries(FX.map((k) => [k, false])), ON = Object.fromEntries(FX.map((k) => [k, true]));
const REST = { mode: 'hd2d', sway: false, shake: false, t: 2 };
const CASES = [
  ['01-all-on', { ...ON }],
  ['02-off-tiltshift', { ...ON, tilt: false }],
  ['03-off-far-preblur', { ...ON, far: false }],
  ['04-off-near-preblur', { ...ON, near: false }],
  ['05-off-foreground-strip', { ...ON, fg: false }],
  ['06-off-back-fog', { ...ON, fogb: false }],
  ['07-off-front-fog', { ...ON, fogf: false }],
  ['08-off-bloom', { ...ON, bloom: false }],
  ['09-off-vignette', { ...ON, vig: false }],
  ['10-all-off', { ...OFF }],
  ['11-flat', { ...ON, mode: 'flat' }],
];
const DEPTHS = [[12, 'workshop'], [13, 'cellar'], [14, 'undercroft'], [15, 'furnace-deep'], [16, 'last-vault']];
const PRESET_KEYS = ['bth', 'bin', 'vi', 'fba', 'ffa'];
const CAMERA = [   // sway x = sa sin(2 pi t / 6), y = sa/2 sin(2 pi t / 9): t 1.5 and 4.5 are the two ends of the x swing
  ['17-sway-camera-right', { sway: true, t: 1.5 }, { t: 1.5 }],
  ['18-sway-camera-left', { sway: true, t: 4.5 }, { t: 4.5 }],
  ['19-shake', { shake: true, t: 2 }, null],            // rest frame at t = 2 is 01-all-on
];

let code = 1;
try {
  let target;
  for (let i = 0; i < 50 && !target; i++) {
    await sleep(200);
    try { target = (await (await fetch(`http://127.0.0.1:${port}/json`)).json()).find((t) => t.type === 'page'); } catch { /* not up yet */ }
  }
  if (!target) throw new Error('Chrome did not start');
  ws = new WebSocket(target.webSocketDebuggerUrl);
  await new Promise((r) => (ws.onopen = r));
  ws.onmessage = (m) => {
    const d = JSON.parse(m.data);
    if (d.id && pending.has(d.id)) { const p = pending.get(d.id); pending.delete(d.id); d.error ? p.rej(new Error(d.error.message)) : p.res(d.result); return; }
    if (d.method === 'Runtime.consoleAPICalled') {
      const line = `[${d.params.type}] ` + d.params.args.map((a) => a.value ?? a.description ?? '').join(' ');
      all.push(line); if (['error', 'assert'].includes(d.params.type)) errors.push(line);
    }
    if (d.method === 'Runtime.exceptionThrown') { const l = '[exception] ' + (d.params.exceptionDetails.exception?.description ?? d.params.exceptionDetails.text); all.push(l); errors.push(l); }
    if (d.method === 'Log.entryAdded') { const l = `[log:${d.params.entry.level}] ` + d.params.entry.text + ' ' + (d.params.entry.url ?? ''); all.push(l); if (d.params.entry.level === 'error') errors.push(l); }
  };
  await send('Runtime.enable'); await send('Log.enable'); await send('Page.enable');
  await send('Emulation.setDeviceMetricsOverride', { width: 1640, height: 900, deviceScaleFactor: 1, mobile: false });
  const evalJs = async (expression) => {
    const r = await send('Runtime.evaluate', { expression, returnByValue: true, awaitPromise: true });
    if (r.exceptionDetails) throw new Error('page script: ' + (r.exceptionDetails.exception?.description ?? r.exceptionDetails.text));
    return r.result.value;
  };
  await send('Page.navigate', { url });
  for (let i = 0; i < 100 && !(await evalJs('!!(window.hd2d && window.hd2d.ready)')); i++) await sleep(100);
  if (!(await evalJs('!!(window.hd2d && window.hd2d.ready)'))) throw new Error('page not ready: ' + errors.join(' | '));

  const rect = await evalJs("(() => { const r = document.getElementById('stage').getBoundingClientRect(); return { x: r.left, y: r.top, w: r.width, h: r.height }; })()");
  const grab = async (clip, beyond) => Buffer.from((await send('Page.captureScreenshot', { format: 'png', clip: { ...clip, scale: 1 }, captureBeyondViewport: !!beyond })).data, 'base64');
  const shot = () => grab({ x: rect.x, y: rect.y, width: 1280, height: 720 });                       // the stage only
  const page = async () => { const m = await send('Page.getLayoutMetrics'), c = m.cssContentSize ?? m.contentSize;   // the whole page, panel included
    return grab({ x: 0, y: 0, width: Math.ceil(c.width), height: Math.ceil(c.height) }, true); };
  const hudMask = async () => Buffer.from(await evalJs('hd2d.hudMask()'), 'base64');
  // one frame: apply settings, screenshot the stage, save it when it has a name
  const frame = async (name, set) => {
    await evalJs(`hd2d.set(${JSON.stringify(set)})`);
    await sleep(120);
    const png = await shot(); if (name) writeFileSync(join(outDir, name + '.png'), png);
    const img = decodePng(png);
    return { name, img, hash: await evalJs('hd2d.hudHash()'), size: img.w === 1280 && img.h === 720 };
  };
  // pixels differing between two frames, split by the HUD mask (640x360, 1 = opaque HUD pixel)
  const diff = (a, b, mask) => {
    let hud = 0, world = 0, n = 0;
    for (let y = 0; y < 720; y++) for (let x = 0; x < 1280; x++) {
      const m = mask[(y >> 1) * 640 + (x >> 1)], i = (y * 1280 + x) * 4; n += m;
      if (a.px[i] !== b.px[i] || a.px[i + 1] !== b.px[i + 1] || a.px[i + 2] !== b.px[i + 2]) { if (m) hud++; else world++; }
    }
    return { hud, world, n };
  };
  const pageFx = await evalJs('JSON.stringify(hd2d.FX)');
  expect(pageFx === JSON.stringify(FX), `effect list equals hd2d.FX (${JSON.parse(pageFx).length} effects: ${JSON.parse(pageFx).join(' ')})`);

  // 01..11: effects at depth 1
  console.log('effects (depth 1, t = 2 s)');
  const mask1 = await hudMask(); let ref = null, allOff = null; const hashes = new Set();
  for (const [name, set] of CASES) {
    const f = await frame(name, { ...REST, depth: 1, ...set }); hashes.add(f.hash);
    if (!ref) ref = f; if (name === '10-all-off') allOff = f;
    const d = diff(f.img, ref.img, mask1), isRef = f === ref;
    expect(f.size && d.hud === 0, `${name}: ${f.img.w}x${f.img.h}, HUD pixels differing from 01-all-on: ${d.hud} of ${d.n}`);
    if (!isRef) expect(d.world > 0, `${name}: world pixels differing from 01-all-on: ${d.world} (the switch changes the picture)`);
    if (name === '11-flat') { const e = diff(f.img, allOff.img, mask1); expect(e.hud + e.world === 0, `11-flat equals 10-all-off: ${e.hud + e.world} pixels differ (screenshot form of restdiff)`); }
  }
  expect(hashes.size === 1, `HUD canvas hash identical in frames 01..11 (${[...hashes].join(',')})`);
  const hash1 = [...hashes][0];

  // 12..16: depth presets; each depth is compared with its own all-off frame, never with another depth
  console.log('depth presets (all effects on, t = 2 s)');
  const depthHashes = new Set(); let depth1 = null;
  for (let d = 1; d <= 5; d++) {
    const name = `${DEPTHS[d - 1][0]}-depth-${d}-${DEPTHS[d - 1][1]}`;
    const own = await frame(null, { ...REST, depth: d, ...OFF });
    const mask = await hudMask();
    const f = await frame(name, { ...REST, depth: d, ...ON }); depthHashes.add(f.hash); if (d === 1) depth1 = f;
    const vals = JSON.parse(await evalJs(`JSON.stringify({ s: ${JSON.stringify(PRESET_KEYS)}.map((k) => hd2d.S[k]), m: ${JSON.stringify(PRESET_KEYS)}.map((k) => hd2d.MOODS[${d}][k]), name: hd2d.MOODS[${d}].name })`));
    expect(JSON.stringify(vals.s) === JSON.stringify(vals.m), `${name}: page values = preset "${vals.name}" (${PRESET_KEYS.map((k, i) => k + ' ' + vals.s[i]).join(', ')})`);
    const e = diff(f.img, own.img, mask);
    expect(f.size && e.hud === 0 && f.hash === own.hash, `${name}: HUD pixels differing from the depth ${d} all-off frame: ${e.hud} of ${e.n}; HUD hash ${f.hash}`);
    expect(e.world > 0, `${name}: world pixels differing from the depth ${d} all-off frame: ${e.world}`);
    const rd = await evalJs('hd2d.restdiff()');
    expect(rd === 0, `depth ${d}: restdiff=${rd} (hd2d world, all effects off, camera at rest, vs flat 1:1)`);
  }
  expect(depthHashes.size === 5, `5 different HUD hashes over the 5 depths (the HUD text follows the depth): ${[...depthHashes].join(',')}`);
  { const e = diff(depth1.img, ref.img, mask1); expect(e.hud + e.world === 0 && depth1.hash === hash1, `12-depth-1-workshop equals 01-all-on: ${e.hud + e.world} pixels differ`); }

  // 17..19: camera. Sway moves the camera (layers shift by their parallax), shake moves the world; the HUD does neither.
  console.log('camera (depth 1, all effects on)');
  { const back = await frame(null, { ...REST, depth: 1, ...ON }), e = diff(back.img, ref.img, mask1);
    expect(e.hud + e.world === 0 && back.hash === hash1, `back at depth 1 after depth 5 equals 01-all-on: ${e.hud + e.world} pixels differ (presets and HUD survive the round trip)`); }
  const moved = [];
  for (const [name, set, restSet] of CAMERA) {
    const rest = restSet ? await frame(null, { ...REST, depth: 1, ...ON, ...restSet }) : ref;
    const f = await frame(name, { ...REST, depth: 1, ...ON, ...set }); moved.push(f);
    const h = diff(f.img, ref.img, mask1), w = diff(f.img, rest.img, mask1);
    expect(f.size && h.hud === 0 && f.hash === hash1, `${name}: HUD pixels differing from 01-all-on: ${h.hud} of ${h.n}; HUD hash ${f.hash}`);
    expect(w.world > 0 && w.hud === 0, `${name}: world pixels differing from the frame at rest at the same time: ${w.world}`);
  }
  { const e = diff(moved[0].img, moved[1].img, mask1); expect(e.world > 0 && e.hud === 0, `17 and 18 (two ends of the sway): ${e.world} world pixels differ, ${e.hud} HUD pixels differ`); }

  await evalJs(`hd2d.set(${JSON.stringify({ ...REST, depth: 1, ...ON })})`);
  { const rd = await evalJs('hd2d.restdiff()'); expect(rd === 0, `page-controls: restdiff=${rd} at depth 1 after all frames (also refreshes the panel readout)`); }
  await sleep(120);
  writeFileSync(join(outDir, 'page-controls.png'), await page());

  // stand-in textures 5b-04 would ship: PNG bytes of each (data-URL payload), for the estimate in SPEC.md
  const tex = JSON.parse(await evalJs("JSON.stringify(Object.entries(hd2d.exportTextures()).map(([k, v]) => [k, v.w, v.h, atob(v.png.split(',')[1]).length]))"));
  const total = tex.reduce((s, t) => s + t[3], 0);
  writeFileSync(join(outDir, 'textures.txt'), 'PNG bytes of the stand-in textures from hd2d.exportTextures() (headless Chrome; not Unity build bytes)\n'
    + tex.map(([k, w, h, b]) => `${String(b).padStart(8)}  ${w}x${h}  ${k}`).join('\n') + `\n${String(total).padStart(8)}  total, ${tex.length} textures\n`);
  expect(tex.length > 0 && total > 0, `textures.txt: ${tex.length} stand-in textures, ${total} PNG bytes in total`);
  code = 0;
} catch (e) {
  console.error('shoot.mjs: ' + e.message); fails.push('crashed: ' + e.message);
} finally {
  try { ws?.close(); } catch { /* closed */ }
  chrome.kill();
}
writeFileSync(join(outDir, 'console.txt'), all.join('\n') + '\n');
console.log(`console errors: ${errors.length}` + (errors.length ? '\n  ' + errors.join('\n  ') : ''));
if (errors.length) fails.push('console errors');
console.log(fails.length ? `FAIL: ${fails.length} check(s) failed` : `OK: all ${checks} hd2d mockup checks passed`);
process.exit(fails.length || code ? 1 : 0);
