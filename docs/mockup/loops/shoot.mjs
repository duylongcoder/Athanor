// Evidence for Phase 5b task 5b-02: renders docs/mockup/loops/index.html in headless Chrome (CDP, the pattern of tools/web/rotate.mjs)
// and writes one before|after sheet per stream at 30, 60 and 144 Hz, a paused live overview, stats.json and console.txt.
//   node docs/mockup/loops/shoot.mjs <outDir>
// Exit 0 only with 0 console errors and every check passing (sheets drawn; the loop draws the same pixels at the same moment at
// 30, 60 and 144 Hz; dust rounding identical). Node 22+ (global WebSocket), Chrome.
import { spawn } from 'node:child_process';
import { createServer } from 'node:http';
import { mkdirSync, writeFileSync, mkdtempSync, readFileSync, existsSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join, dirname, extname } from 'node:path';
import { fileURLToPath } from 'node:url';

const outDir = process.argv[2];
if (!outDir) { console.error('usage: node docs/mockup/loops/shoot.mjs <outDir>'); process.exit(2); }
mkdirSync(outDir, { recursive: true });
const here = dirname(fileURLToPath(import.meta.url));

const chromeBin = () => process.env.CHROME_BIN
  || ['/Applications/Google Chrome.app/Contents/MacOS/Google Chrome', '/usr/bin/google-chrome', '/usr/bin/google-chrome-stable', '/usr/bin/chromium']
    .find((p) => existsSync(p)) || 'google-chrome';

const TYPES = { '.html': 'text/html; charset=utf-8', '.js': 'text/javascript', '.css': 'text/css', '.png': 'image/png' };
const server = createServer((req, res) => {
  const u = decodeURIComponent(req.url.split('?')[0]);
  const f = join(here, u === '/' ? 'index.html' : u);
  if (!f.startsWith(here) || !existsSync(f)) { res.writeHead(404); res.end(); return; }
  res.writeHead(200, { 'content-type': TYPES[extname(f)] || 'application/octet-stream' }); res.end(readFileSync(f));
});
await new Promise((r) => server.listen(0, '127.0.0.1', r));
const base = `http://127.0.0.1:${server.address().port}/`;

const port = 9336, profile = mkdtempSync(join(tmpdir(), 'athanor-loops-'));
const chrome = spawn(chromeBin(), ['--headless=new', `--remote-debugging-port=${port}`, `--user-data-dir=${profile}`, '--window-size=1340,900',
  '--no-first-run', '--mute-audio', '--hide-scrollbars', 'about:blank'], { stdio: 'ignore' });
const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
const all = [], errors = [];
let ws, id = 0; const pending = new Map();
const send = (method, params = {}) => new Promise((res, rej) => { const i = ++id; pending.set(i, { res, rej }); ws.send(JSON.stringify({ id: i, method, params })); });
const fails = [];
const expect = (ok, what) => { console.log((ok ? '  ok   ' : '  FAIL ') + what); if (!ok) fails.push(what); };
const written = [];

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
      const line = `[${d.params.type}] ` + d.params.args.map((a) => a.value ?? a.description ?? '').join(' ').slice(0, 400);
      all.push(line); if (['error', 'assert'].includes(d.params.type)) errors.push(line);
    }
    if (d.method === 'Runtime.exceptionThrown') { const line = '[exception] ' + (d.params.exceptionDetails.exception?.description ?? d.params.exceptionDetails.text).slice(0, 400); all.push(line); errors.push(line); }
    if (d.method === 'Log.entryAdded') {
      const line = `[log:${d.params.entry.level}] ` + d.params.entry.text.slice(0, 400) + ' ' + (d.params.entry.url ?? '');
      all.push(line); if (d.params.entry.level === 'error') errors.push(line);
    }
  };
  await send('Runtime.enable'); await send('Log.enable'); await send('Page.enable');
  await send('Emulation.setDeviceMetricsOverride', { width: 1340, height: 900, deviceScaleFactor: 1, mobile: false });
  const evalJs = async (expression) => {
    const r = await send('Runtime.evaluate', { expression, returnByValue: true, awaitPromise: true });
    if (r.exceptionDetails) throw new Error('page script: ' + (r.exceptionDetails.exception?.description ?? r.exceptionDetails.text));
    return r.result.value;
  };
  const load = async (q) => {
    all.push('--- ' + q);
    await send('Page.navigate', { url: base + 'index.html' + q });
    for (let i = 0; i < 100; i++) { await sleep(100); if (await evalJs("document.body && document.body.dataset.ready === '1'").catch(() => false)) return true; }
    return false;
  };
  const shoot = async (name, selector) => {
    const r = await evalJs(`(() => { const b = document.querySelector(${JSON.stringify(selector)}).getBoundingClientRect(); return { x: b.left + scrollX, y: b.top + scrollY, w: b.width, h: b.height }; })()`);
    const png = (await send('Page.captureScreenshot', { format: 'png', captureBeyondViewport: true, clip: { x: Math.floor(r.x), y: Math.floor(r.y), width: Math.ceil(r.w), height: Math.ceil(r.h), scale: 1 } })).data;
    writeFileSync(join(outDir, name), Buffer.from(png, 'base64')); written.push(`${name} ${Math.ceil(r.w)}x${Math.ceil(r.h)}`);
  };

  const stats = {};
  for (const stream of ['embers', 'streaks', 'rune']) {
    for (const hz of [30, 60, 144]) {
      const name = `${stream}-${hz}hz`;
      console.log(name);
      const before = errors.length;
      expect(await load(`?shot=${stream}&hz=${hz}`), 'sheet drawn');
      const s = await evalJs('LOOPS.stats');
      stats[name] = s;
      const rows = await evalJs("document.querySelectorAll('#sheet .row').length");
      expect(rows === Object.keys(s.rows).length && rows >= 4, `${rows} before|after rows`);
      const h = s.afterSameAcrossHz;
      expect(h['30v60'] + h['60v144'] + h['30v144'] === 0, `after: same pixels at t = ${h.t} s at 30/60/144 Hz (differing ${h['30v60']}, ${h['60v144']}, ${h['30v144']})`);
      console.log(`  steady on screen: before ${s.steady.before.mean} (sd ${s.steady.before.sd}), after ${s.steady.after.mean} (sd ${s.steady.after.sd}); births/s before ${s.births.before}, after ${s.births.after}`);
      console.log(`  stall frame: born before ${s.stall.bornBefore}, after ${s.stall.bornAfter}; on screen before ${s.stall.onBefore}, after ${s.stall.onAfter}` +
        (s.hide ? `; root hidden: before ${s.hide.onBefore} fly on ${s.hide.beforeLastsMs} ms, after ${s.hide.onAfter} -> ${s.hide.nextAfter}` : ''));
      await shoot(name + '.png', '#sheet');
      expect(errors.length === before, `0 console errors${errors.length > before ? ': ' + errors.slice(before).join(' | ') : ''}`);
    }
  }
  console.log('live overview');
  {
    const before = errors.length;
    expect(await load('?hz=60&at=2.5'), 'live view advanced to 2.5 s and paused');
    await shoot('page-live-60hz-t2.5.png', 'main');
    const dust = await evalJs('LOOPS.dust');
    stats.dust = dust;
    expect([30, 60, 144].every((hz) => dust[hz].differingPixels === 0), 'dust: Px.Snap and SparkMesh rounding agree for every mote, 20 s at 30/60/144 Hz');
    const hz = await evalJs("JSON.stringify(['embers','streaks','rune'].map((id) => LOOPS.hzCheck(id).before))");
    stats.beforeAcrossHz = JSON.parse(hz);
    console.log('  before, pixels differing between rates at t = 3 s (random, expected): ' + stats.beforeAcrossHz.map((b) => `${b['30v60']}/${b['60v144']}/${b['30v144']}`).join(', '));
    expect(errors.length === before, '0 console errors');
  }
  writeFileSync(join(outDir, 'stats.json'), JSON.stringify(stats, null, 2) + '\n');
} catch (e) {
  console.error('shoot.mjs: ' + e.message); fails.push('crashed: ' + e.message);
} finally {
  try { ws?.close(); } catch { /* closed */ }
  chrome.kill(); server.close();
}
writeFileSync(join(outDir, 'console.txt'), all.join('\n') + `\n\nconsole errors: ${errors.length}\n`);
console.log('wrote: ' + written.join(', '));
console.log(`console errors: ${errors.length}`);
console.log(fails.length || errors.length ? `FAIL: ${fails.length} check(s) failed` : 'OK: all checks passed');
process.exit(fails.length || errors.length ? 1 : 0);
