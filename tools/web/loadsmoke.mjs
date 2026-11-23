// Load smoke for a RELEASE web build (the release-gate's size-checked build has none of the dev-only hooks webtest.mjs needs).
// Opens the page in headless Chrome, waits until the loader removes #loading, lets the title run a few seconds, takes one
// picture, and fails on any console error or exception. No dependencies (Node 22+).
//   node tools/web/loadsmoke.mjs <url> <outDir> [seconds = 6]      exit 0 = loaded and clean, 1 = not loaded or errors, 2 = Chrome failed
import { spawn } from 'node:child_process';
import { existsSync, mkdirSync, mkdtempSync, writeFileSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';

const [url, outDir, secondsArg] = process.argv.slice(2);
if (!url || !outDir) { console.log('usage: node tools/web/loadsmoke.mjs <url> <outDir> [seconds]'); process.exit(2); }
const seconds = Number(secondsArg ?? 6);
const chromeBin = process.env.CHROME_BIN
  || ['/Applications/Google Chrome.app/Contents/MacOS/Google Chrome', '/usr/bin/google-chrome', '/usr/bin/google-chrome-stable', '/usr/bin/chromium']
    .find((p) => existsSync(p)) || 'google-chrome';
mkdirSync(outDir, { recursive: true });
const port = 9337, profile = mkdtempSync(join(tmpdir(), 'athanor-smoke-'));
const chrome = spawn(chromeBin, ['--headless=new', `--remote-debugging-port=${port}`, `--user-data-dir=${profile}`, '--window-size=1280,720',
  '--no-first-run', '--mute-audio', '--enable-unsafe-swiftshader', '--ignore-gpu-blocklist', 'about:blank'], { stdio: 'ignore' });
const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
const logs = [];
let ws, id = 0, code = 1;
const pending = new Map();
// every command answers in 20 s or fails, and the whole run has a hard deadline, so the gate can never hang
const send = (method, params = {}) => new Promise((res, rej) => {
  const i = ++id;
  const timer = setTimeout(() => { pending.delete(i); rej(new Error(`Chrome did not answer ${method} in 20s`)); }, 20000);
  pending.set(i, { res: (v) => { clearTimeout(timer); res(v); }, rej: (e) => { clearTimeout(timer); rej(e); } });
  try { ws.send(JSON.stringify({ id: i, method, params })); } catch (e) { clearTimeout(timer); pending.delete(i); rej(e); }
});
const watchdog = setTimeout(() => { console.log('SMOKE ERROR hard deadline reached'); chrome.kill(); process.exit(1); }, (seconds + 180) * 1000);

try {
  let target;
  for (let i = 0; i < 50 && !target; i++) {
    await sleep(200);
    try { target = (await (await fetch(`http://127.0.0.1:${port}/json`)).json()).find((t) => t.type === 'page'); } catch { /* not up yet */ }
  }
  if (!target) { code = 2; throw new Error('Chrome did not start'); }
  ws = new WebSocket(target.webSocketDebuggerUrl);
  await new Promise((r) => (ws.onopen = r));
  ws.onclose = () => { for (const p of pending.values()) p.rej(new Error('Chrome closed the debugging socket')); pending.clear(); };
  ws.onmessage = (m) => {
    const d = JSON.parse(m.data);
    if (d.id && pending.has(d.id)) { const p = pending.get(d.id); pending.delete(d.id); d.error ? p.rej(new Error(d.error.message)) : p.res(d.result); return; }
    if (d.method === 'Runtime.consoleAPICalled') logs.push(`[${d.params.type}] ` + d.params.args.map((a) => a.value ?? a.description ?? '').join(' ').slice(0, 400));
    if (d.method === 'Runtime.exceptionThrown') logs.push('[exception] ' + (d.params.exceptionDetails.exception?.description ?? d.params.exceptionDetails.text).slice(0, 400));
  };
  await send('Runtime.enable'); await send('Page.enable');
  const t0 = Date.now();
  await send('Page.navigate', { url });
  // Loaded = the Athanor page is there (its canvas exists) and the loader's .then removed #loading. A missing page, a 404 or
  // a Chrome error page has no canvas, so it never counts as loaded; "Could not start" keeps #loading and fails too.
  const probe = "document.querySelector('#unity-canvas') ? (document.querySelector('#loading') === null ? 'up' : 'loading') : 'nopage'";
  let state = 'nopage';
  for (let i = 0; i < 240 && state !== 'up'; i++) {
    await sleep(500);
    state = await send('Runtime.evaluate', { expression: probe, returnByValue: true }).then((r) => r.result.value).catch(() => 'nopage');
  }
  if (state !== 'up') {
    const why = await send('Runtime.evaluate', { expression: "document.querySelector('#loading')?.textContent ?? document.title", returnByValue: true }).then((r) => r.result.value).catch(() => '?');
    throw new Error(`the build did not load in 120 s (${state}: ${String(why).slice(0, 200)})`);
  }
  console.log(`loaded in ${((Date.now() - t0) / 1000).toFixed(1)} s`);
  // headless Chrome draws a frame only when a picture is asked for, so the wait takes pictures
  const until = Date.now() + seconds * 1000;
  while (Date.now() < until) { await send('Page.captureScreenshot', { format: 'jpeg', quality: 20 }); await sleep(250); }
  writeFileSync(join(outDir, 'title.png'), Buffer.from((await send('Page.captureScreenshot', { format: 'png' })).data, 'base64'));
  const bad = logs.filter((l) => /\[(error|exception)\]/.test(l));
  console.log(`console: ${logs.length} messages, ${bad.length} errors`);
  for (const l of bad.slice(0, 12)) console.log('  ', l);
  code = bad.length === 0 ? 0 : 1;
} catch (e) {
  console.log('SMOKE ERROR', e.message);
} finally {
  writeFileSync(join(outDir, 'console.txt'), logs.join('\n'));
  try { ws?.close(); } catch { /* closing */ }
  clearTimeout(watchdog);
  chrome.kill();
  process.exit(code);
}
