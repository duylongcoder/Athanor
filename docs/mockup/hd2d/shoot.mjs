// Screenshots of the HD-2D mockup (Phase 5b-01a) in headless Chrome over CDP (pattern of tools/web/rotate.mjs). Node 22+, Chrome.
//   node docs/mockup/hd2d/shoot.mjs <outDir>
// Writes one 1280x720 PNG of the stage per case (all effects on, each effect off in turn, all off, flat), page-controls.png,
// console.txt. Exit 0 only with 0 console errors, restdiff=0 and the HUD pixels identical in every frame.
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
const expect = (ok, what) => { console.log((ok ? '  ok   ' : '  FAIL ') + what); if (!ok) fails.push(what); };

const OFF = { tilt: false, far: false, near: false, fg: false, fogb: false, fogf: false };
const ON = { tilt: true, far: true, near: true, fg: true, fogb: true, fogf: true };
const CASES = [
  ['01-all-on', { mode: 'hd2d', ...ON }],
  ['02-off-tiltshift', { mode: 'hd2d', ...ON, tilt: false }],
  ['03-off-far-preblur', { mode: 'hd2d', ...ON, far: false }],
  ['04-off-near-preblur', { mode: 'hd2d', ...ON, near: false }],
  ['05-off-foreground-strip', { mode: 'hd2d', ...ON, fg: false }],
  ['06-off-back-fog', { mode: 'hd2d', ...ON, fogb: false }],
  ['07-off-front-fog', { mode: 'hd2d', ...ON, fogf: false }],
  ['08-all-off', { mode: 'hd2d', ...OFF }],
  ['09-flat', { mode: 'flat', ...ON }],
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
  const mask = Buffer.from(await evalJs('hd2d.hudMask()'), 'base64');
  const shot = async (clip) => Buffer.from((await send('Page.captureScreenshot', { format: 'png', ...(clip ? { clip: { x: rect.x, y: rect.y, width: 1280, height: 720, scale: 1 } } : {}) })).data, 'base64');
  let ref = null; const hashes = new Set();
  for (const [name, set] of CASES) {
    await evalJs(`hd2d.set(${JSON.stringify(set)})`);
    await sleep(120);
    const png = await shot(true); writeFileSync(join(outDir, name + '.png'), png);
    hashes.add(await evalJs('hd2d.hudHash()'));
    const img = decodePng(png);
    if (!ref) ref = img;
    let diff = 0, n = 0;
    for (let y = 0; y < 720; y++) for (let x = 0; x < 1280; x++) {
      if (!mask[(y >> 1) * 640 + (x >> 1)]) continue; n++;
      const i = (y * 1280 + x) * 4; if (img.px[i] !== ref.px[i] || img.px[i + 1] !== ref.px[i + 1] || img.px[i + 2] !== ref.px[i + 2]) diff++;
    }
    expect(img.w === 1280 && img.h === 720 && diff === 0, `${name}: ${img.w}x${img.h}, HUD pixels differing from 01-all-on: ${diff} of ${n}`);
  }
  expect(hashes.size === 1, `HUD canvas hash identical in all frames (${[...hashes].join(',')})`);
  const rd = await evalJs('hd2d.restdiff()');
  expect(rd === 0, `restdiff=${rd} (hd2d world, all effects off, vs flat 1:1)`);
  await evalJs(`hd2d.set(${JSON.stringify({ mode: 'hd2d', ...ON })})`); await sleep(120);
  writeFileSync(join(outDir, 'page-controls.png'), await shot(false));
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
console.log(fails.length ? `FAIL: ${fails.length} check(s) failed` : 'OK: all hd2d mockup checks passed');
process.exit(fails.length || code ? 1 : 0);
