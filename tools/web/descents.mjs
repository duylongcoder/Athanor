// Photographs each Descent cutscene (5a-14: the depth intro line and the window view) of a development build in headless Chrome.
// The scripted player (F9) plays; Descend logs "[descent] n" in development builds, and this script takes pictures at fixed
// offsets after it: the name and intro line, the room rising back, the bench with the new window view.
// Lets the scripted player play a development web build in headless Chrome and photographs it (no dependencies; Node 22+).
//   node descents.mjs <url> <outDir> [seconds = 240] [lang = en|vi]
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

const [url, outDir, secondsArg, lang = 'en'] = process.argv.slice(2);
const seconds = Number(secondsArg ?? 240);
mkdirSync(outDir, { recursive: true });
const port = 9335, profile = mkdtempSync(join(tmpdir(), 'athanor-chrome-'));
const chrome = spawn(chromeBin(), ['--headless=new', `--remote-debugging-port=${port}`, `--user-data-dir=${profile}`, '--window-size=1280,720',
  '--no-first-run', '--mute-audio', '--enable-unsafe-swiftshader', '--ignore-gpu-blocklist', 'about:blank'], { stdio: 'ignore' });
const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
const logs = [];
let ws, id = 0; const pending = new Map();
// A command Chrome never answers (a hung renderer, a closed socket) must not hang the script: 20 s per command, and a hard
// deadline for the whole run (seconds + 240) after which the console log is saved, Chrome is killed and the script exits 2.
const SEND_TIMEOUT_MS = 20000;
const send = (method, params = {}) => new Promise((res, rej) => {
  const i = ++id;
  const timer = setTimeout(() => { pending.delete(i); rej(new Error(`Chrome did not answer ${method} in ${SEND_TIMEOUT_MS / 1000}s`)); }, SEND_TIMEOUT_MS);
  pending.set(i, { res: (v) => { clearTimeout(timer); res(v); }, rej: (e) => { clearTimeout(timer); rej(e); } });
  try { ws.send(JSON.stringify({ id: i, method, params })); } catch (e) { clearTimeout(timer); pending.delete(i); rej(e); }
});
const watchdog = setTimeout(() => {
  console.log(`TEST ERROR hard deadline (${seconds + 240}s) reached`);
  try { writeFileSync(join(outDir, 'console.txt'), logs.join('\n')); } catch { /* best effort */ }
  chrome.kill(); process.exit(2);
}, (seconds + 240) * 1000);

try {
  let target;
  for (let i = 0; i < 50 && !target; i++) {
    await sleep(200);
    try { target = (await (await fetch(`http://127.0.0.1:${port}/json`)).json()).find((t) => t.type === 'page'); } catch { /* not up yet */ }
  }
  if (!target) throw new Error('Chrome did not start');
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
  if (lang === 'vi') { await click(320, 317, 800); await click(375, 146, 900); await click(320, 244, 900); }   // Settings, Language, Back
  await shot(`title_${lang}`);                                // the title keeps the original night sky
  // (headless Chrome draws a frame only when a picture is asked for, so every wait below takes pictures)
  const frame = () => send('Page.captureScreenshot', { format: 'jpeg', quality: 20 });
  const count = (re) => logs.filter((l) => re.test(l)).length;
  // Wait (taking pictures) until the log has more than n lines matching re; false after ms.
  const waitLog = async (re, n, ms) => { for (const t = Date.now(); Date.now() - t < ms && count(re) <= n;) await frame(); return count(re) > n; };
  // F9 is a toggle, and the script cannot see the state from its own presses (one press too many after depth 2 left the scripted
  // player off and the run stood still for 11 minutes). GameFlow logs "[auto] on|off" (GameFlow.AutoHook): set the wanted state
  // and wait for the game to confirm it. A press with no frame while the key is down is lost, so an unconfirmed press is sent again.
  const AUTO = /\[auto\] /;
  const autoOn = () => (logs.filter((l) => AUTO.test(l)).at(-1) ?? '').includes('[auto] on');
  const setAuto = async (want) => {
    for (let k = 0; k < 3 && autoOn() !== want; k++) { const n = count(AUTO); await f9(); await waitLog(AUTO, n, 3000); }
    if (autoOn() !== want) throw new Error('the scripted player did not turn ' + (want ? 'on' : 'off'));
  };
  // The card chooser logs "[cards] open" once the dealt cards take a click (CatalystChoiceView.OpenHook); a click before it is dropped.
  const CARDS = /\[cards\] open/;
  let cardsDone = 0, aspectsDone = 0, eventsDone = 0, readyDone = 0;
  const waitCards = async (tag) => {
    if (!await waitLog(CARDS, cardsDone, 20000)) { await shot(`fail_cards_${tag}`); throw new Error('the cards never took a click: ' + tag); }
    cardsDone = count(CARDS);
    for (let i = 0; i < 3; i++) await frame();
  };
  // The start of a depth played by hand (scripted player off): the aspect offer (depths 1, 3, 5), then the event if one was drawn,
  // until the bench is handed over ("[start] ready"). Each step waits for the game's own hook, never for a guessed time.
  const byHand = async (tag) => {
    for (const t = Date.now(); count(/\[start\] ready/) <= readyDone;) {
      if (Date.now() - t > 40000) { await shot(`fail_start_${tag}`); throw new Error('the bench was never handed over: ' + tag); }
      await frame();
      if (count(/\[start\] aspects/) > aspectsDone) {
        aspectsDone = count(/\[start\] aspects/);
        await waitCards(tag); await shot(`${tag}_aspects_${lang}`);
        await click(320, 200, 300);                          // the middle aspect
      } else if (count(/\[event\] options/) > eventsDone) {
        eventsDone = count(/\[event\] options/);
        await shot(`${tag}_event_${lang}`);
        await click(320, 165, 300);                          // the event's first option
        if (!await waitLog(/\[event\] result/, eventsDone - 1, 8000)) { await shot(`fail_event_${tag}`); throw new Error('the event never showed its result: ' + tag); }
        for (let i = 0; i < 4; i++) await frame();
        await shot(`${tag}_event_result_${lang}`);
        await click(320, 260, 300);                          // Continue
      }
    }
    readyDone = count(/\[start\] ready/);
    for (let i = 0; i < 6; i++) await frame();
  };

  // The scripted player on, on the title: it begins the run and the tutorial is skipped (GameFlow.Main: first = !Auto && ...),
  // so this is a normal run.
  await click(20, 20, 300);                                   // focus the canvas for the key (a corner of the title does nothing)
  await setAuto(true);
  let introAt = 0;
  // A fresh profile first plays the story intro (Save.IntroSeen): a page of it is the 5a-10 intro frame.
  for (const t = Date.now(); Date.now() - t < 6000;) await frame();
  await shot(`intro_story_${lang}`);
  // Then click it away (a click turns the page) until the depth-1 line shows.
  for (let i = 0; i < 600 && !introAt; i++) {
    await frame();
    if (i % 6 === 5) await click(60, 180, 50);
    if (logs.some((l) => /\[intro\] 1/.test(l))) introAt = Date.now();
  }
  if (!introAt) { await shot(`fail_${lang}`); throw new Error('the run-start intro never showed'); }
  await setAuto(false);                                       // the intro plays and the aspect is picked by hand
  for (const o of [0.25, 0.7, 1.3, 1.9]) {
    while ((Date.now() - introAt) / 1000 < o) await frame();
    await shot(`d1_intro_${lang}_${String(o).replace('.', '_')}`);
  }
  for (let i = 0; i < 12; i++) await frame();
  await shot(`d1_after_intro_${lang}`);
  await byHand('d1');
  await shot(`d1_bench_${lang}`);
  await setAuto(true);
  const seen = new Set();
  const offsets = [2.2, 3.4, 4.6, 6, 7.5, 9.5];
  let runOver = false;
  const t0 = Date.now();
  while ((Date.now() - t0) / 1000 < seconds && !seen.has(5) && !runOver) {
    await sleep(150); await send('Page.captureScreenshot', { format: 'png' });
    if (count(/\[runover\]/) > 0) { runOver = true; break; }
    // a depth began with the scripted player off (it shows its offer or event): play its start by hand, then hand the bench back
    if (count(/\[start\] aspects/) > aspectsDone || count(/\[event\] options/) > eventsDone) { await byHand('x' + (seen.size + 1)); await setAuto(true); continue; }
    readyDone = count(/\[start\] ready/); cardsDone = count(CARDS);   // starts the scripted player played itself
    const hit = logs.map((l) => /\[descent\] (\d+)/.exec(l)).filter(Boolean).map((m) => Number(m[1])).find((n) => !seen.has(n));
    if (!hit) continue;
    seen.add(hit);
    const t1 = Date.now();
    let manual = false;
    for (const o of offsets) {
      while ((Date.now() - t1) / 1000 < o) {
        await sleep(120); await send('Page.captureScreenshot', { format: 'png' });
        // the scripted player picks a catalyst 0.8 s after the choice appears: take the bench back during the cutscene, pick by hand
        if (!manual && (Date.now() - t1) / 1000 > 1) { manual = true; await setAuto(false); }
      }
      await shot(`d${hit}_${lang}_${String(o).replace('.', '_')}`);
    }
    await waitCards(`c${hit}`);
    await shot(`c${hit}_choice_${lang}`);
    await click(320, 200, 300);                               // the middle catalyst
    await byHand(`s${hit}`);                                  // the new depth's aspect offer and event, if any
    await shot(`b${hit}_bench_${lang}`);                      // the bench with the new window view
    await setAuto(true);                                      // the scripted player plays on
  }
  if (runOver || count(/\[runover\]/) > 0) {
    for (const [n, w] of [['a', 2.0], ['b', 3.5]]) {
      const t2 = Date.now();
      while ((Date.now() - t2) / 1000 < w) { await sleep(150); await send('Page.captureScreenshot', { format: 'png' }); }
      await shot(`r${n}_runover_${lang}`);
    }
  }
  console.log('descents photographed:', [...seen].join(','));
} catch (e) {
  console.log('TEST ERROR', e.message);
} finally {
  const bad = logs.filter((l) => /\[(error|exception)\]/.test(l));
  console.log(`console: ${logs.length} messages, ${bad.length} errors`);
  for (const l of bad.slice(0, 12)) console.log('  ', l);
  writeFileSync(join(outDir, 'console.txt'), logs.join('\n'));
  try { ws?.close(); } catch { /* closing */ }
  clearTimeout(watchdog);
  chrome.kill();
}
