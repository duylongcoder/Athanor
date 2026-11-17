// 5b-06 probe for the HD-2D prototype build (and Main, for comparison): frame time, renderer name, console errors, frame sequences.
// Drives Chrome over the DevTools protocol; no dependencies (Node 22+). Does not change the game.
//   node tools/web/depthprobe.mjs <url> <outDir> <viewportWxH> <steps> [--gpu] [--headful] [--uncapped]
// <steps> is a comma list run in order after load:
//   wait:<s>              wait (pictures are taken so a headless page keeps drawing)
//   key:<k>               press a key on the canvas (s sway, e effects, k shake, 0..5 depth preset)
//   perf:<tag>:<s>        sample requestAnimationFrame for <s> seconds, no pictures taken; writes <tag>.json (frame ms list + stats)
//   frames:<tag>:<n>:<ms> n PNG pictures, ~<ms> apart, to <outDir>/<tag>/NNN.png with a time stamp list in times.json
// --gpu       use the machine's GPU (ANGLE GL) instead of SwiftShader;  --headful  a visible window (needs DISPLAY);
// --uncapped  disable vsync and the frame-rate limit, so the frame time is the cost of a frame and not the display rate.
// Always writes <outDir>/env.json (WebGL UNMASKED_RENDERER, Chrome version, flags) and <outDir>/console.txt.
import { spawn } from 'node:child_process';
import { existsSync, mkdirSync, mkdtempSync, writeFileSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';

const args = process.argv.slice(2);
const flags = args.filter((a) => a.startsWith('--'));
const [url, outDir, vp, stepsArg] = args.filter((a) => !a.startsWith('--'));
if (!url || !outDir || !vp || !stepsArg) { console.log('usage: node depthprobe.mjs <url> <outDir> <WxH> <steps> [--gpu] [--headful] [--uncapped]'); process.exit(2); }
const [VW, VH] = vp.split('x').map(Number);
const gpu = flags.includes('--gpu'), headful = flags.includes('--headful'), uncapped = flags.includes('--uncapped');
const chromeBin = process.env.CHROME_BIN
  || ['/usr/bin/google-chrome', '/usr/bin/google-chrome-stable', '/usr/bin/chromium', '/Applications/Google Chrome.app/Contents/MacOS/Google Chrome']
    .find((p) => existsSync(p)) || 'google-chrome';
mkdirSync(outDir, { recursive: true });
const port = 9400 + Math.floor(Math.random() * 300), profile = mkdtempSync(join(tmpdir(), 'athanor-probe-'));
const cargs = [`--remote-debugging-port=${port}`, `--user-data-dir=${profile}`, `--window-size=${VW},${VH + 120}`, '--no-first-run', '--mute-audio',
  '--disable-background-timer-throttling', '--disable-renderer-backgrounding', '--disable-backgrounding-occluded-windows'];
if (!headful) cargs.push('--headless=new');
if (gpu) cargs.push('--ignore-gpu-blocklist', '--enable-gpu-rasterization', ...(process.env.GPU_FLAGS ?? '--use-angle=vulkan --enable-features=Vulkan --disable-vulkan-surface').split(' '));
else cargs.push('--enable-unsafe-swiftshader', '--ignore-gpu-blocklist', '--use-gl=angle', '--use-angle=swiftshader');
if (uncapped) cargs.push('--disable-gpu-vsync', '--disable-frame-rate-limit');
cargs.push('about:blank');
const chrome = spawn(chromeBin, cargs, { stdio: 'ignore' });
const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
const logs = [];
let ws, id = 0, code = 1;
const pending = new Map();
const send = (method, params = {}) => new Promise((res, rej) => {
  const i = ++id;
  const timer = setTimeout(() => { pending.delete(i); rej(new Error(`no answer to ${method} in 60s`)); }, 60000);
  pending.set(i, { res: (v) => { clearTimeout(timer); res(v); }, rej: (e) => { clearTimeout(timer); rej(e); } });
  ws.send(JSON.stringify({ id: i, method, params }));
});
const ev = (expression) => send('Runtime.evaluate', { expression, returnByValue: true, awaitPromise: true }).then((r) => r.result.value);
const watchdog = setTimeout(() => { console.log('PROBE hard deadline'); chrome.kill(); process.exit(1); }, 25 * 60 * 1000);
const keyDefs = { s: ['KeyS', 83], e: ['KeyE', 69], k: ['KeyK', 75] };
for (let d = 0; d <= 5; d++) keyDefs[String(d)] = ['Digit' + d, 48 + d];
const stats = (a) => {
  const s = [...a].sort((x, y) => x - y), n = s.length, mean = a.reduce((x, y) => x + y, 0) / n;
  const q = (p) => s[Math.min(n - 1, Math.floor(p * n))];
  return { n, mean_ms: +mean.toFixed(2), median_ms: +q(0.5).toFixed(2), p95_ms: +q(0.95).toFixed(2), p99_ms: +q(0.99).toFixed(2), max_ms: +s[n - 1].toFixed(2), fps_mean: +(1000 / mean).toFixed(1), over_33ms: a.filter((x) => x > 33.4).length };
};

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
    if (d.method === 'Runtime.consoleAPICalled') logs.push(`[${d.params.type}] ` + d.params.args.map((a) => a.value ?? a.description ?? '').join(' ').slice(0, 400));
    if (d.method === 'Runtime.exceptionThrown') logs.push('[exception] ' + (d.params.exceptionDetails.exception?.description ?? d.params.exceptionDetails.text).slice(0, 400));
    if (d.method === 'Log.entryAdded' && d.params.entry.level === 'error') logs.push(`[error] (${d.params.entry.source}) ${d.params.entry.text} ${d.params.entry.url ?? ''}`.slice(0, 400));
  };
  await send('Runtime.enable'); await send('Page.enable'); await send('Log.enable');
  await send('Emulation.setDeviceMetricsOverride', { width: VW, height: VH, deviceScaleFactor: 1, mobile: false });
  // a requestAnimationFrame recorder, started on demand
  await send('Page.addScriptToEvaluateOnNewDocument', { source: `window.__rec = { on:false, t:[] };
    (function tick(ts){ if (window.__rec.on) window.__rec.t.push(ts); requestAnimationFrame(tick); })(0);` });
  const t0 = Date.now();
  await send('Page.navigate', { url });
  let state = 'nopage';
  for (let i = 0; i < 240 && state !== 'up'; i++) {
    await sleep(500);
    state = await ev("document.querySelector('#unity-canvas') ? (document.querySelector('#loading') === null ? 'up' : 'loading') : 'nopage'").catch(() => 'nopage');
  }
  if (state !== 'up') throw new Error('the build did not load in 120 s: ' + state);
  const loadS = (Date.now() - t0) / 1000;
  const env = await ev(`(() => { const c = document.createElement('canvas'); const g = c.getContext('webgl2'); if (!g) return { webgl2: false };
    const x = g.getExtension('WEBGL_debug_renderer_info');
    return { webgl2: true, vendor: x ? g.getParameter(x.UNMASKED_VENDOR_WEBGL) : g.getParameter(g.VENDOR), renderer: x ? g.getParameter(x.UNMASKED_RENDERER_WEBGL) : g.getParameter(g.RENDERER) }; })()`);
  const ver = (await (await fetch(`http://127.0.0.1:${port}/json/version`)).json()).Browser;
  const canvas = await ev('({ w: document.querySelector("#unity-canvas").width, h: document.querySelector("#unity-canvas").height, dpr: window.devicePixelRatio, iw: innerWidth, ih: innerHeight })');
  const envOut = { url, viewport: vp, headful, gpuFlag: gpu, uncapped, chrome: ver, flags: cargs.filter((a) => !a.startsWith('--user-data') && !a.startsWith('--remote') && a !== 'about:blank'), load_seconds: +loadS.toFixed(1), canvas, webgl: env };
  writeFileSync(join(outDir, 'env.json'), JSON.stringify(envOut, null, 1));
  console.log(`loaded in ${loadS.toFixed(1)} s; canvas ${canvas.w}x${canvas.h}; ${env.renderer}`);

  // focus the canvas
  await send('Input.dispatchMouseEvent', { type: 'mouseMoved', x: 5, y: 5 });
  await send('Input.dispatchMouseEvent', { type: 'mousePressed', x: 5, y: 5, button: 'left', clickCount: 1 });
  await send('Input.dispatchMouseEvent', { type: 'mouseReleased', x: 5, y: 5, button: 'left', clickCount: 1 });

  const shot = async (file) => writeFileSync(file, Buffer.from((await send('Page.captureScreenshot', { format: 'png' })).data, 'base64'));
  for (const step of stepsArg.split(',')) {
    const [op, a, b, c] = step.split(':');
    if (op === 'wait') {
      const until = Date.now() + Number(a) * 1000;
      while (Date.now() < until) { await send('Page.captureScreenshot', { format: 'jpeg', quality: 10 }); await sleep(100); }
    } else if (op === 'key') {
      const [cd, vk] = keyDefs[a];
      await send('Input.dispatchKeyEvent', { type: 'rawKeyDown', key: a, code: cd, windowsVirtualKeyCode: vk });
      await sleep(80);
      await send('Input.dispatchKeyEvent', { type: 'keyUp', key: a, code: cd, windowsVirtualKeyCode: vk });
      await sleep(300);
    } else if (op === 'perf') {
      await ev('window.__rec.t = []; window.__rec.on = true');
      await sleep(Number(b) * 1000);
      const t = await ev('window.__rec.on = false; window.__rec.t');
      const d = t.slice(1).map((x, i) => x - t[i]);
      const out = { tag: a, seconds: Number(b), ...stats(d.slice(Math.min(10, d.length >> 3))), frame_ms: d.map((x) => +x.toFixed(1)) };
      writeFileSync(join(outDir, a + '.json'), JSON.stringify(out));
      const { frame_ms, ...show } = out;
      console.log('perf', JSON.stringify(show));
    } else if (op === 'frames') {
      const dir = join(outDir, a); mkdirSync(dir, { recursive: true });
      const times = [];
      for (let i = 0; i < Number(b); i++) {
        const s0 = Date.now();
        times.push(await ev('performance.now()'));
        await shot(join(dir, String(i).padStart(3, '0') + '.png'));
        const wait = Number(c) - (Date.now() - s0);
        if (wait > 0) await sleep(wait);
      }
      writeFileSync(join(dir, 'times.json'), JSON.stringify(times));
      console.log(`frames ${a}: ${b} pictures`);
    }
  }
  const bad = logs.filter((l) => /\[(error|exception)\]/.test(l));
  console.log(`console: ${logs.length} messages, ${bad.length} errors`);
  for (const l of bad.slice(0, 12)) console.log('  ', l);
  code = bad.length === 0 ? 0 : 1;
} catch (e) {
  console.log('PROBE ERROR', e.message);
} finally {
  writeFileSync(join(outDir, 'console.txt'), logs.join('\n'));
  try { ws?.close(); } catch { /* closing */ }
  clearTimeout(watchdog);
  chrome.kill();
  process.exit(code);
}
