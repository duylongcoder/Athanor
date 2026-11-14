// Lets the scripted player play a development web build in headless Chrome and photographs it (no dependencies; Node 22+).
//   node autoplay.mjs <url> <outDir> [seconds = 120] [every = 1.5]
// Needs the build made by WebBuild.BuildDev: F9 hands the bench to the scripted player only in a development build.
// It starts a run by hand (aspect, event), presses F9, takes a picture every <every> seconds for <seconds>, then
// opens the Goals page from the run summary, starts a second run (by then a starter is usually won, so the hand
// is six cards) and plays some of it. Look at the pictures: goal toasts, tile gems, big hands, the summary and the
// finished goals are all in them.
import { spawn } from 'node:child_process';
import { mkdirSync, writeFileSync, mkdtempSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';
import { existsSync } from 'node:fs';
const chromeBin = () => process.env.CHROME_BIN
  || ['/Applications/Google Chrome.app/Contents/MacOS/Google Chrome', '/usr/bin/google-chrome', '/usr/bin/google-chrome-stable', '/usr/bin/chromium']
    .find((p) => existsSync(p)) || 'google-chrome';

const [url, outDir, secondsArg, everyArg] = process.argv.slice(2);
const seconds = Number(secondsArg ?? 120), every = Number(everyArg ?? 1.5);
mkdirSync(outDir, { recursive: true });
const port = 9334, profile = mkdtempSync(join(tmpdir(), 'athanor-chrome-'));
const chrome = spawn(chromeBin(), ['--headless=new', `--remote-debugging-port=${port}`, `--user-data-dir=${profile}`, '--window-size=1280,720',
  '--no-first-run', '--mute-audio', '--enable-unsafe-swiftshader', '--ignore-gpu-blocklist', 'about:blank'], { stdio: 'ignore' });
const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
const logs = [];
let ws, id = 0; const pending = new Map();
const send = (method, params = {}) => new Promise((res, rej) => { const i = ++id; pending.set(i, { res, rej }); ws.send(JSON.stringify({ id: i, method, params })); });

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
  };
  await send('Runtime.enable'); await send('Page.enable');
  await send('Emulation.setDeviceMetricsOverride', { width: 1280, height: 720, deviceScaleFactor: 1, mobile: false });
  const shot = async (name) => writeFileSync(join(outDir, name + '.png'), Buffer.from((await send('Page.captureScreenshot', { format: 'png' })).data, 'base64'));
  // art-space click: at 1280x720 the 640x360 picture is drawn twice its size
  const click = async (ax, ay, wait = 350) => {
    const x = ax * 2, y = ay * 2;
    await send('Input.dispatchMouseEvent', { type: 'mouseMoved', x, y });
    await sleep(60);
    await send('Input.dispatchMouseEvent', { type: 'mousePressed', x, y, button: 'left', clickCount: 1 });
    await sleep(60);
    await send('Input.dispatchMouseEvent', { type: 'mouseReleased', x, y, button: 'left', clickCount: 1 });
    await sleep(wait);
  };
  const hover = async (ax, ay, wait = 500) => { await send('Input.dispatchMouseEvent', { type: 'mouseMoved', x: ax * 2, y: ay * 2 }); await sleep(wait); };
  const f9 = async () => {
    const key = { key: 'F9', code: 'F9', windowsVirtualKeyCode: 120, nativeVirtualKeyCode: 120 };
    await send('Input.dispatchKeyEvent', { type: 'rawKeyDown', ...key });
    await sleep(80);
    await send('Input.dispatchKeyEvent', { type: 'keyUp', ...key });
    await sleep(300);
  };

  await send('Page.navigate', { url });
  let loaded = false;
  for (let i = 0; i < 240 && !loaded; i++) { await sleep(500); loaded = await send('Runtime.evaluate', { expression: "document.querySelector('#loading') === null", returnByValue: true }).then((r) => r.result.value).catch(() => false); }
  if (!loaded) throw new Error('the build did not load');
  await sleep(4000);
  await click(320, 227, 1500);                               // Begin
  for (let i = 0; i < 22; i++) await click(60, 180, 450);    // through the intro
  await sleep(2500);
  await click(320, 200, 1800);                               // the middle aspect
  await click(320, 165, 1000);                               // the event's first option, if there is an event
  await click(320, 260, 1500);                               // Continue
  await hover(207, 325);                                     // point at the second card of the hand
  await shot('hand_hover');
  await f9();
  const n = Math.round(seconds / every);
  for (let i = 0; i < n; i++) { await sleep(every * 1000); await shot('p' + String(i).padStart(3, '0')); }
  console.log('pictures:', n);
  await f9();                                                // the player takes the bench back
  await sleep(600);
  await shot('q1_end');
  await click(320, 226, 900);                                // the goals line of the run summary, if the run is over
  await shot('q2_goals_from_summary');
  await click(8, 200, 700);
  // a second run: a starter won in the first makes the hand six cards, which overlap
  await click(248, 255, 2500);                               // Descend again
  await sleep(2500);
  await click(320, 200, 1800);
  await click(320, 165, 1000);
  await click(320, 260, 1500);
  await shot('r1_second_run');
  await hover(200, 325);                                     // point at the second card: it comes to the front
  await shot('r2_second_run_hover');
  await f9();
  const m = Math.round(Math.min(seconds, 60) / every / 2);
  for (let i = 0; i < m; i++) { await sleep(every * 2000); await shot('s' + String(i).padStart(3, '0')); }
  await f9();
  await sleep(600);
  await shot('r3_second_run_end');
  await click(390, 255, 2500);                               // Title, if that run is over too
  await shot('q3_title');
  await click(320, 287, 900);                                // Goals, from the title
  await shot('q4_goals_from_title');
} catch (e) {
  console.log('TEST ERROR', e.message);
} finally {
  const bad = logs.filter((l) => /\[(error|exception)\]/.test(l));
  console.log(`console: ${logs.length} messages, ${bad.length} errors`);
  for (const l of bad.slice(0, 12)) console.log('  ', l);
  writeFileSync(join(outDir, 'console.txt'), logs.join('\n'));
  try { ws?.close(); } catch { /* closing */ }
  chrome.kill();
}
