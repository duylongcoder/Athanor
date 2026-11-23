// Renders the WebGL template (macros replaced by dummy values, a stub Unity loader) in headless Chrome and checks the rotate-device
// layer (Phase 5 W2-07, SPEC change 26). Needs no Unity and no build (Node 22+, Chrome).
//   node tools/web/rotate.mjs <outDir>        writes one PNG per case, exit 0 only if every case and the table sync pass
// Cases: phone portrait touch EN and VI (layer shown), same phone turned (gone), 390x844 (shown), tablet 768x1024 touch (not shown),
// desktop 500x800 without touch (not shown), desktop 1280x720 (not shown). 0 console errors in all of them.
import { spawn } from 'node:child_process';
import { createServer } from 'node:http';
import { mkdirSync, writeFileSync, mkdtempSync, readFileSync, existsSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';
import { check, TEMPLATE } from './rotate_sync.mjs';

const outDir = process.argv[2];
if (!outDir) { console.error('usage: node rotate.mjs <outDir>'); process.exit(2); }
mkdirSync(outDir, { recursive: true });

const chromeBin = () => process.env.CHROME_BIN
  || ['/Applications/Google Chrome.app/Contents/MacOS/Google Chrome', '/usr/bin/google-chrome', '/usr/bin/google-chrome-stable', '/usr/bin/chromium']
    .find((p) => existsSync(p)) || 'google-chrome';

// Template -> plain HTML. Unity's `#if` lines are dropped, the macros get dummy values.
const page = readFileSync(TEMPLATE, 'utf8').split('\n').filter((l) => !/^\s*#(if|endif|else)\b/.test(l)).join('\n')
  .replace(/\{\{\{\s*JSON\.stringify\((\w+)\)\s*\}\}\}/g, (_, n) => JSON.stringify('dummy-' + n))
  .replace(/\{\{\{\s*(\w+)\s*\}\}\}/g, (_, n) => (n.endsWith('_FILENAME') ? n.toLowerCase() + '.js' : 'Athanor'));
if (/\{\{\{/.test(page)) { console.error('template still has macros'); process.exit(2); }
// A loader that behaves like Unity at the start: reports some progress and never finishes, so the loading bar stays up.
const stub = 'window.createUnityInstance = function (c, cfg, onProgress) { onProgress(0.4); return new Promise(function () {}); };';
const server = createServer((req, res) => {
  const u = req.url.split('?')[0];
  if (u === '/') { res.writeHead(200, { 'content-type': 'text/html; charset=utf-8' }); res.end(page); return; }
  if (/^\/Build\/.*\.js$/.test(u)) { res.writeHead(200, { 'content-type': 'text/javascript' }); res.end(u.includes('loader') ? stub : ''); return; }
  res.writeHead(404); res.end();
});
await new Promise((r) => server.listen(0, '127.0.0.1', r));
const url = `http://127.0.0.1:${server.address().port}/`;

const port = 9334, profile = mkdtempSync(join(tmpdir(), 'athanor-rotate-'));
const chrome = spawn(chromeBin(), ['--headless=new', `--remote-debugging-port=${port}`, `--user-data-dir=${profile}`, '--window-size=1280,720',
  '--no-first-run', '--mute-audio', 'about:blank'], { stdio: 'ignore' });
const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
const logs = [];
let ws, id = 0; const pending = new Map();
const send = (method, params = {}) => new Promise((res, rej) => { const i = ++id; pending.set(i, { res, rej }); ws.send(JSON.stringify({ id: i, method, params })); });
const fails = [];
const expect = (ok, what) => { console.log((ok ? '  ok   ' : '  FAIL ') + what); if (!ok) fails.push(what); };

// name, width, height, touch, languages, layer expected, caption
const CASES = [
  ['360x800-portrait-touch-en', 360, 800, true, 'en-US', true],
  ['360x800-portrait-touch-vi', 360, 800, true, 'vi-VN', true],
  ['800x360-landscape-touch-en', 800, 360, true, 'en-US', false],
  ['800x360-landscape-touch-vi', 800, 360, true, 'vi-VN', false],
  ['390x844-portrait-touch-en', 390, 844, true, 'en-US', true],
  ['768x1024-tablet-portrait-touch-en', 768, 1024, true, 'en-US', false],
  ['500x800-desktop-no-touch-en', 500, 800, false, 'en-US', false],
  ['1280x720-desktop-en', 1280, 720, false, 'en-US', false],
];
const DPR = 2;

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
    if (d.method === 'Runtime.consoleAPICalled' && ['error', 'assert'].includes(d.params.type)) logs.push(`[${d.params.type}] ` + d.params.args.map((a) => a.value ?? a.description ?? '').join(' ').slice(0, 300));
    if (d.method === 'Runtime.exceptionThrown') logs.push('[exception] ' + (d.params.exceptionDetails.exception?.description ?? d.params.exceptionDetails.text).slice(0, 300));
    if (d.method === 'Log.entryAdded' && ['error', 'warning'].includes(d.params.entry.level)) logs.push(`[log:${d.params.entry.level}] ` + d.params.entry.text.slice(0, 300) + ' ' + (d.params.entry.url ?? ''));
  };
  await send('Runtime.enable'); await send('Log.enable'); await send('Page.enable');
  const evalJs = async (expression) => {
    const r = await send('Runtime.evaluate', { expression, returnByValue: true });
    if (r.exceptionDetails) throw new Error('page script: ' + (r.exceptionDetails.exception?.description ?? r.exceptionDetails.text));
    return r.result.value;
  };

  for (const [name, w, h, touch, acceptLanguage, shown] of CASES) {
    console.log(name);
    const before = logs.length;
    await send('Emulation.setUserAgentOverride', { userAgent: touch ? 'Mozilla/5.0 (Linux; Android 14; Pixel 7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0 Mobile Safari/537.36' : 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0 Safari/537.36', acceptLanguage });
    await send('Emulation.setDeviceMetricsOverride', { width: w, height: h, deviceScaleFactor: touch ? DPR : 1, mobile: touch,
      screenOrientation: { type: w < h ? 'portraitPrimary' : 'landscapePrimary', angle: w < h ? 0 : 90 } });
    await send('Emulation.setTouchEmulationEnabled', touch ? { enabled: true, maxTouchPoints: 5 } : { enabled: false });
    await send('Page.navigate', { url });
    await sleep(900);
    const s = JSON.parse(await evalJs(`JSON.stringify((() => {
      const r = document.querySelector('#rotate'), cs = getComputedStyle(r), t = r.querySelector('.rot-title'), b = r.querySelector('.rot-text'), body = r.querySelector('.rot-body');
      const lines = (e) => Math.round(e.getBoundingClientRect().height / parseFloat(getComputedStyle(e).lineHeight));
      const c = document.querySelector('#unity-canvas'), top = document.elementFromPoint(innerWidth / 2, innerHeight / 2);
      const bb = body.getBoundingClientRect();
      return { display: cs.display, coarse: matchMedia('(pointer: coarse)').matches, portrait: matchMedia('(orientation: portrait)').matches, w: innerWidth, h: innerHeight,
        langs: navigator.languages.join(','), htmlLang: document.documentElement.lang, layerLang: r.lang, title: t.textContent, body: b.textContent,
        titleLines: lines(t), bodyLines: lines(b), above: Math.round(bb.top), below: Math.round(innerHeight - bb.bottom), overflowX: r.scrollWidth > r.clientWidth, overflowY: r.scrollHeight > r.clientHeight,
        hit: top && top.closest('#rotate') ? 'rotate' : (top ? top.id || top.tagName : 'none'), loading: !!document.querySelector('#loading') && getComputedStyle(document.querySelector('#loading')).display !== 'none',
        fill: document.querySelector('#fill').style.width, buttons: r.querySelectorAll('button,a,input').length,
        canvas: c.width + 'x' + c.height + ' css ' + c.style.width + ' x ' + c.style.height + ' at ' + c.style.left + ',' + c.style.top };
    })())`));
    console.log('  ' + JSON.stringify(s));
    expect((s.display === 'flex') === shown, `layer ${shown ? 'shown' : 'hidden'} (display=${s.display}, coarse=${s.coarse}, portrait=${s.portrait})`);
    const vi = acceptLanguage.startsWith('vi');
    expect(s.htmlLang === (vi ? 'vi' : 'en') && s.layerLang === s.htmlLang, `language ${s.htmlLang} from navigator.languages=${s.langs}`);
    expect(s.buttons === 0, 'no close control in the layer');
    expect(s.loading && s.fill === '40%', 'loading bar present and stub progress applied (Unity loading is not blocked)');
    if (shown) {
      expect(s.hit === 'rotate', `layer takes the touch at the centre (hit: ${s.hit})`);
      expect(s.title === (vi ? 'Hãy xoay thiết bị' : 'Rotate your device') && s.body === (vi ? 'Xoay điện thoại sang ngang để chơi.' : 'Turn your phone sideways to play.'), 'text matches the table');
      if (w === 360) expect(s.titleLines === 1 && s.bodyLines === 2, `title 1 line, body 2 lines (got ${s.titleLines}, ${s.bodyLines})`);
      expect(!s.overflowX && !s.overflowY && s.above >= 16 && s.below >= 16, `no overflow, free space above ${s.above} / below ${s.below} px (>= 16)`);
    } else {
      expect(s.hit !== 'rotate', `layer does not take the touch (hit: ${s.hit})`);
      expect(/^\d+x\d+ css /.test(s.canvas), 'canvas laid out by fit(): ' + s.canvas);
    }
    writeFileSync(join(outDir, name + '.png'), Buffer.from((await send('Page.captureScreenshot', { format: 'png' })).data, 'base64'));
    const errs = logs.slice(before);
    expect(errs.length === 0, `0 console errors${errs.length ? ': ' + errs.join(' | ') : ''}`);
  }
  // Turning the phone with the page open: the layer hides at once with no reload, and fit() re-fits the canvas.
  console.log('live-turn');
  const before = logs.length, phone = (w, h) => send('Emulation.setDeviceMetricsOverride', { width: w, height: h, deviceScaleFactor: DPR, mobile: true,
    screenOrientation: { type: w < h ? 'portraitPrimary' : 'landscapePrimary', angle: w < h ? 0 : 90 } });
  await send('Emulation.setTouchEmulationEnabled', { enabled: true, maxTouchPoints: 5 });
  await phone(360, 800); await send('Page.navigate', { url }); await sleep(900);
  const disp = () => evalJs("getComputedStyle(document.querySelector('#rotate')).display");
  expect(await disp() === 'flex', 'live: portrait 360x800 shows the layer');
  await phone(800, 360); await sleep(500);
  expect(await disp() === 'none', 'live: turned to 800x360, layer gone without reload');
  expect(/css 100vw x 100vh at 0px,0px/.test(await evalJs("(() => { const c = document.querySelector('#unity-canvas'); return c.width + 'x' + c.height + ' css ' + c.style.width + ' x ' + c.style.height + ' at ' + c.style.left + ',' + c.style.top; })()")), 'live: canvas re-fitted by resize');
  writeFileSync(join(outDir, 'live-turn-800x360.png'), Buffer.from((await send('Page.captureScreenshot', { format: 'png' })).data, 'base64'));
  await phone(360, 800); await sleep(500);
  expect(await disp() === 'flex', 'live: turned back to portrait, layer returns');
  expect(logs.length === before, '0 console errors during the live turn');
} catch (e) {
  console.error('rotate.mjs: ' + e.message); fails.push('crashed: ' + e.message);
} finally {
  try { ws?.close(); } catch { /* closed */ }
  chrome.kill(); server.close();
}
const sync = check();
expect(sync.length === 0, 'template table equals en.json / vi.json' + (sync.length ? ': ' + sync.join(' | ') : ''));
console.log(fails.length ? `FAIL: ${fails.length} check(s) failed` : 'OK: all rotate-layer checks passed');
process.exit(fails.length ? 1 : 0);
