// Drives the web build in headless Chrome over the DevTools protocol (no dependencies; Node 22+).
//   node webtest.mjs <url> <outDir>
// A: title and bench at 1280x720 and 1000x700, the aspect offer and the depth's event, a first trial with real mouse
//    events, journal, tiny window.
// B: reload, the journal and the goals are still there; Reset progress asks, then erases the goals and keeps the journal.
// C: damage the saved journal and the saved profile, reload, the game starts with an empty journal and a fresh profile.
import { spawn } from 'node:child_process';
import { mkdirSync, writeFileSync, mkdtempSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';
import { existsSync } from 'node:fs';
const chromeBin = () => process.env.CHROME_BIN
  || ['/Applications/Google Chrome.app/Contents/MacOS/Google Chrome', '/usr/bin/google-chrome', '/usr/bin/google-chrome-stable', '/usr/bin/chromium']
    .find((p) => existsSync(p)) || 'google-chrome';

const [url, outDir] = process.argv.slice(2);
mkdirSync(outDir, { recursive: true });
const port = 9333, profile = mkdtempSync(join(tmpdir(), 'athanor-chrome-'));
const chrome = spawn(chromeBin(), ['--headless=new', `--remote-debugging-port=${port}`, `--user-data-dir=${profile}`, '--window-size=1280,720',
  '--no-first-run', '--mute-audio', '--enable-unsafe-swiftshader', '--ignore-gpu-blocklist', 'about:blank'], { stdio: 'ignore' });
const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
const logs = [];
let shots = 0;
const MIN_SHOTS = Number(process.env.MIN_SHOTS ?? 20);
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
    if (d.method === 'Log.entryAdded') logs.push(`[log:${d.params.entry.level}] ` + d.params.entry.text.slice(0, 300) + ' ' + (d.params.entry.url ?? ''));
  };
  await send('Runtime.enable'); await send('Log.enable'); await send('Page.enable');
  let W = 1280, H = 720;
  const size = async (w, h) => { W = w; H = h; await send('Emulation.setDeviceMetricsOverride', { width: w, height: h, deviceScaleFactor: 1, mobile: false }); await sleep(1500); };
  const evalJs = async (expression) => {
    const r = await send('Runtime.evaluate', { expression, returnByValue: true, awaitPromise: true });
    if (r.exceptionDetails) throw new Error('page script: ' + (r.exceptionDetails.exception?.description ?? r.exceptionDetails.text));
    return r.result.value;
  };
  const shot = async (name) => { writeFileSync(join(outDir, name + '.png'), Buffer.from((await send('Page.captureScreenshot', { format: 'png' })).data, 'base64')); console.log('shot', name); shots++; };
  const canvas = () => evalJs("(() => { const c = document.querySelector('#unity-canvas'); return c.width + 'x' + c.height + ' css ' + c.clientWidth + 'x' + c.clientHeight + ' at ' + c.style.left + ',' + c.style.top; })()");
  // art-space click: the picture is drawn at a whole multiple of 640x360, centred in the window
  const click = async (ax, ay, wait = 350) => {
    const s = Math.max(1, Math.floor(Math.min(W / 640, H / 360))), x = (W - 640 * s) / 2 + ax * s, y = (H - 360 * s) / 2 + ay * s;
    await send('Input.dispatchMouseEvent', { type: 'mouseMoved', x, y });
    await sleep(60);
    await send('Input.dispatchMouseEvent', { type: 'mousePressed', x, y, button: 'left', clickCount: 1 });
    await sleep(60);
    await send('Input.dispatchMouseEvent', { type: 'mouseReleased', x, y, button: 'left', clickCount: 1 });
    await sleep(wait);
  };
  const load = async (how) => {
    const t0 = Date.now();
    await (how === 'reload' ? send('Page.reload') : send('Page.navigate', { url }));
    await sleep(1000);
    let loaded = false;
    for (let i = 0; i < 240 && !loaded; i++) { await sleep(500); loaded = await evalJs("document.querySelector('#loading') === null").catch(() => false); }
    console.log(loaded ? `loaded in ${((Date.now() - t0) / 1000).toFixed(1)} s` : 'NOT LOADED after 120 s: ' + await evalJs("document.querySelector('#loading')?.textContent"));
    await sleep(4000);                               // the title fades in and its lettering settles
  };
  const errorsSoFar = () => logs.filter((l) => /\[(error|exception|log:error)\]/.test(l)).length;
  // The start of a depth: take the middle aspect card, then the event's first option and Continue. Both of the event's
  // buttons are always in the same place; when no event was drawn those two clicks land on the bench and do nothing.
  const startOfDepth = async (tag) => {
    await sleep(2500);
    await shot(tag + '_aspects');
    await click(320, 200, 1800);                     // the middle card
    await shot(tag + '_event');
    await click(320, 165, 1000);                     // the event's first option
    await shot(tag + '_event_result');
    await click(320, 260, 1500);                     // Continue
  };

  // ---- A: first visit ----
  await size(W, H);
  await load();
  console.log('canvas 1280x720:', await canvas());
  await shot('a01_title_1280');
  await size(1000, 700); console.log('canvas 1000x700:', await canvas());
  await shot('a02_title_1000x700');
  await size(1280, 720);
  await click(320, 227, 1500);                       // Begin
  await shot('a03_intro');
  for (let i = 0; i < 22; i++) await click(60, 180, 450);  // through the intro
  await startOfDepth('a04');
  await shot('a04_bench_1280');
  await click(169, 325); await click(251, 157);      // five cards: the first goes top-left
  await click(207, 325); await click(320, 157);      // four left: the first goes top-middle
  await click(245, 325); await click(243, 188);      // three left: the first goes below the first
  await click(283, 325); await click(320, 188);      // two left: the first goes to the centre
  await sleep(600);
  await shot('a05_placed');
  await click(575, 328, 100); await click(575, 328, 2500);   // Ignite, clicked twice in a row
  await shot('a06_after_ignite');
  for (let i = 0; i < 14; i++) await click(60, 180, 700);    // skip whatever cutscenes follow
  await sleep(3000);
  await shot('a07_after_trial');
  await click(575, 296, 900);                        // Journal
  await shot('a08_journal');
  await click(8, 200, 700);                          // a click outside closes it
  await size(1000, 700); console.log('canvas 1000x700:', await canvas());
  await shot('a09_bench_1000x700');
  await size(500, 300); console.log('canvas 500x300:', await canvas());
  await shot('a10_bench_500x300');
  await size(1280, 720);
  console.log('errors after A:', errorsSoFar());

  // ---- B: come back later ----
  await sleep(1500);
  await load('reload');
  await shot('b01_title_again');
  await click(320, 257, 900);                        // Journal, from the title
  await shot('b02_journal_kept');
  await click(8, 200, 700);
  await click(320, 287, 900);                        // Goals, from the title
  await shot('b03_goals');
  await click(8, 200, 700);
  await click(320, 326, 700);                        // Reset progress: it asks first
  await shot('b04_reset_asks');
  await click(392, 238, 900);                        // Erase
  await shot('b05_after_reset');
  console.log('errors after B:', errorsSoFar());

  // ---- C: a damaged save ----
  const damage = await evalJs(`(async () => {
    const dbs = (await indexedDB.databases()).map((d) => d.name);
    const out = { dbs, keys: [], changed: 0 };
    for (const name of dbs) {
      const db = await new Promise((res, rej) => { const r = indexedDB.open(name); r.onsuccess = () => res(r.result); r.onerror = () => rej(r.error); });
      for (const storeName of db.objectStoreNames) {
        const tx = db.transaction(storeName, 'readwrite'), store = tx.objectStore(storeName);
        const keys = await new Promise((res) => { const r = store.getAllKeys(); r.onsuccess = () => res(r.result); });
        for (const key of keys) {
          const v = await new Promise((res) => { const r = store.get(key); r.onsuccess = () => res(r.result); });
          const bytes = v && v.contents;
          out.keys.push(name + ' / ' + storeName + ' / ' + key + ' (' + (bytes ? bytes.length : 'no') + ' bytes)');
          if (!bytes || !String(key).endsWith('PlayerPrefs')) continue;
          const text = Array.from(bytes, (b) => String.fromCharCode(b)).join('');
          // the journal starts {"known", the profile {"version": the '{' of each becomes '<', which is no longer JSON
          let hit = false;
          for (const marker of ['{"known"', '{"version"']) {
            const at = text.indexOf(marker);
            if (at < 0) continue;
            (out.around ??= []).push(text.slice(Math.max(0, at - 30), at + 70));
            bytes[at] = 60; hit = true; out.changed++;
          }
          if (!hit) { out.around = [text.slice(0, 200)]; continue; }
          await new Promise((res, rej) => { const r = store.put(v, key); r.onsuccess = res; r.onerror = () => rej(r.error); });
        }
        await new Promise((res) => { tx.oncomplete = res; if (tx.commit) tx.commit(); });
      }
      db.close();
    }
    return out;
  })()`);
  console.log('damage:', JSON.stringify(damage));
  await load('reload');
  await shot('c01_title_after_damage');
  await click(320, 257, 900);
  await shot('c02_journal_after_damage');
  await click(8, 200, 700);
  await click(320, 287, 900);
  await shot('c03_goals_after_damage');
  await click(8, 200, 700);
  await click(320, 227, 1000);                       // Begin: the intro has been seen, so straight to the first depth
  await startOfDepth('c04');
  await shot('c05_bench_after_damage');
  console.log('errors after C:', errorsSoFar());
} catch (e) {
  console.log('TEST ERROR', e.message);
  process.exitCode = 1;
} finally {
  const bad = logs.filter((l) => /\[(error|exception|log:error)\]/.test(l));
  console.log(`console: ${logs.length} messages, ${bad.length} errors, ${shots} screenshots`);
  if (bad.length > 0) process.exitCode = 1;                       // a console error or exception fails the run
  if (shots < MIN_SHOTS) { console.log(`only ${shots} screenshots, expected ${MIN_SHOTS}: the run stopped early`); process.exitCode = 1; }
  for (const l of bad.slice(0, 12)) console.log('  ', l);
  writeFileSync(join(outDir, 'console.txt'), logs.join('\n'));
  try { ws?.close(); } catch { /* closing */ }
  chrome.kill();
}
