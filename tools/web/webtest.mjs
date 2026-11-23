// Drives the web build in headless Chrome over the DevTools protocol (no dependencies; Node 22+).
//   node webtest.mjs <url> <outDir>
// A: title and bench at 1280x720 and 1000x700, the aspect offer and the depth's event, a first trial with real mouse
//    events, journal, tiny window.
// D: Settings panel: open, switch Vietnamese and back, saved across a reload, Begin/Journal stay put.
// B: reload, the journal and the goals are still there; Reset progress asks, then erases the goals and keeps the journal.
// E: pause (5a-13): open in a run, Resume, Esc, Quit with confirm, Begin/Journal unchanged, pause inside the tutorial (time stops, no step advances), Quit from it; EN and VI, one narrow window.
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
const MIN_SHOTS = Number(process.env.MIN_SHOTS ?? 31);
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
  const size = async (w, h) => { W = w; H = h; await send('Emulation.setDeviceMetricsOverride', { width: w, height: h, deviceScaleFactor: 1, mobile: false }); await pump(1500); };
  const evalJs = async (expression) => {
    const r = await send('Runtime.evaluate', { expression, returnByValue: true, awaitPromise: true });
    if (r.exceptionDetails) throw new Error('page script: ' + (r.exceptionDetails.exception?.description ?? r.exceptionDetails.text));
    return r.result.value;
  };
  const shot = async (name) => { writeFileSync(join(outDir, name + '.png'), Buffer.from((await send('Page.captureScreenshot', { format: 'png' })).data, 'base64')); console.log('shot', name); shots++; };
  const canvas = () => evalJs("(() => { const c = document.querySelector('#unity-canvas'); return c.width + 'x' + c.height + ' css ' + c.clientWidth + 'x' + c.clientHeight + ' at ' + c.style.left + ',' + c.style.top; })()");
  // art-space click: the picture is drawn at a whole multiple of 640x360, centred in the window
  // Headless Chrome only runs the game's frames when asked for a picture, so waits take pictures instead of just sleeping.
  const frame = async () => (await send('Page.captureScreenshot', { format: 'png' })).data;
  const pump = async (ms) => { for (const t0 = Date.now(); Date.now() - t0 < ms;) { await sleep(120); await frame(); } };
  const click = async (ax, ay, wait = 350) => {
    const s = Math.max(1, Math.floor(Math.min(W / 640, H / 360))), x = (W - 640 * s) / 2 + ax * s, y = (H - 360 * s) / 2 + ay * s;
    await send('Input.dispatchMouseEvent', { type: 'mouseMoved', x, y });
    await sleep(60);
    await send('Input.dispatchMouseEvent', { type: 'mousePressed', x, y, button: 'left', clickCount: 1 });
    await sleep(60);
    await send('Input.dispatchMouseEvent', { type: 'mouseReleased', x, y, button: 'left', clickCount: 1 });
    await pump(wait);
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
  // The event dialog is followed by its own hooks ("[event] options" / "result" / "closed", development builds), so the script
  // waits for the dialog instead of guessing how long a random event takes to fade in; a depth with no event logs nothing.
  const evCount = (w, from = 0) => logs.slice(from).filter((l) => l.includes('[event] ' + w)).length;
  let evFrom = 0;
  const waitEvent = async (w, n, ms) => { for (let t = 0; t < ms && evCount(w, evFrom) <= n; t += 150) { await sleep(150); await frame(); } return evCount(w, evFrom) > n; };
  // The card chooser (aspects, catalysts) logs "[cards] open" once the dealt cards take a click (CatalystChoiceView.OpenHook).
  // A click before it is dropped by the game; a fixed wait here was the flake of e08/e10 (the click missed, the depth never started).
  const cardsOpen = (from) => logs.slice(from).filter((l) => l.includes('[cards] open')).length;
  const waitCards = async (from, tag) => {
    for (let t = 0; t < 20000 && cardsOpen(from) === 0; t += 150) { await sleep(150); await frame(); }
    if (cardsOpen(from) === 0) { await shot('fail_cards_' + tag); throw new Error('the cards never took a click: ' + tag); }
    await pump(300);                                 // one more picture so the shot shows the cards at rest
  };
  const startOfDepth = async (tag, from) => {      // from: logs.length taken before the click that starts the depth
    evFrom = from;
    const st = (w) => logs.slice(from).filter((l) => l.includes('[start] ' + w)).length;
    const o = 0, r = 0, c = 0, a = 0, rd = 0;
    for (let t = 0; t < 20000 && st('aspects') <= a; t += 200) { await sleep(200); await frame(); }
    if (st('aspects') <= a) { await shot('fail_aspects_' + tag); throw new Error('the aspect offer never appeared: ' + tag); }
    await waitCards(from, tag);
    await shot(tag + '_aspects');
    await click(320, 200, 300);                      // the middle card
    // after the pick the game either opens the event (its options hook) or hands over the bench (ready): wait for whichever comes,
    // instead of guessing how long a drawn event takes to appear
    for (let t = 0; t < 20000 && evCount('options', from) <= o && st('ready') <= rd; t += 150) { await sleep(150); await frame(); }
    const hasEvent = evCount('options', from) > o;
    if (!hasEvent && st('ready') <= rd) { await shot('fail_pick_' + tag); throw new Error('neither the event nor the bench followed the aspect pick: ' + tag); }
    await sleep(400); await shot(tag + '_event');
    if (hasEvent) {
      await click(320, 165, 300);                    // the event's first option
      if (!await waitEvent('result', r, 5000)) { await shot('fail_event_' + tag); throw new Error('the event never showed its result: ' + tag); }
      await sleep(400);
    }
    await shot(tag + '_event_result');
    if (hasEvent) {
      await click(320, 260, 300);                    // Continue (always in the same place)
      if (!await waitEvent('closed', c, 5000)) { await shot('fail_event_' + tag); throw new Error('Continue did not close the event: ' + tag); }
    }
    for (let t = 0; t < 10000 && st('ready') <= rd; t += 200) { await sleep(200); await frame(); }
    if (st('ready') <= rd) { await shot('fail_ready_' + tag); throw new Error('the bench was never handed over: ' + tag); }
    await sleep(800);
  };

  // The tutorial (5a-12). TutorialController logs "[tut] <line>" whenever it shows a step, so the script follows the game
  // instead of counting clicks: wait for the next step (skipping cutscenes with clicks), look at it, then do what it asks.
  // Tile centres are the layout's x + 25 (row 2 is 77 apart).
  const tileAt = (i) => [[256, 157], [333, 157], [410, 157], [263, 188], [340, 188], [417, 188], [256, 219], [333, 219], [410, 219]][i];
  const tutLogs = () => logs.filter((l) => l.includes('[tut]'));
  let tutSeen = 0, tutN = 0;
  const waitStep = async (t, slug) => {
    for (let i = 0; i < 60 && tutLogs().length <= tutSeen; i++) await click(60, 180, 500);   // cutscenes and descents
    if (tutLogs().length <= tutSeen) { await shot('fail_' + slug); throw new Error('tutorial step never appeared: ' + slug); }
    const line = tutLogs()[tutSeen++];
    await sleep(700);                                  // the box and the rim settle
    await shot(`${t}_${String(++tutN).padStart(2, '0')}_${slug}`);
    return line;
  };
  const lay = async (card, tile) => { await click(card, 325, 300); const p = tileAt(tile); await click(p[0], p[1], 600); };
  const playTutorial = async (t) => {
    if (t !== 'a') {                                    // How to play: step 1.1 is logged right after '[drive] start', before this runs
      const start = logs.map((l, i) => (l.includes('[drive] start') ? i : -1)).filter((i) => i >= 0).at(-1) ?? 0;
      tutSeen = tutLogs().filter((l) => logs.indexOf(l) < start).length;
    }                                                   // (the first run's step 1.1 was logged while the intro was being skipped)
    tutN = 0;
    const lines = [];
    const step = async (slug, act) => { lines.push(await waitStep(t, slug)); await act(); };
    const read = () => click(60, 180, 500), ignite = () => click(575, 328, 2500);
    await step('1_1_bench', read);
    await step('1_2_lay_ember_coal', () => lay(169, 4));
    await step('1_3_lay_deep_tear', () => lay(207, 5));
    await step('1_4_take_back', read);
    await step('1_5_ignite', ignite);
    await step('1_6_potency_parts', read); await step('1_6b_formula', read); await step('1_7_rack', read);
    await step('2_1_lay_miss', async () => { await lay(169, 3); await lay(207, 4); });
    await step('2_2_ignite', ignite);
    await step('2_3_nothing_reacted', read);
    await step('2_4_open_journal', () => click(575, 296, 900));
    await step('2_5_journal', read); await step('2_6_index', read);
    await step('2_7_hint', () => click(8, 200, 900));
    await step('3_0_redraw', read);
    await step('3_1_lay_chain', async () => { await lay(169, 3); await lay(207, 4); await lay(245, 5); });
    await step('3_2_order', read);
    await step('3_3_ignite', ignite);
    await step('3_3b_step1', read); await step('3_3c_step2', read);
    await step('3_4_chain', read); await step('3_5_parts', read); await step('3_6_without_moondust', read);
    let offerFrom = 0;                                  // the cards are dealt after 5.2 is read: wait for them to take a click
    await step('5_1_offer', read); await step('5_2_offer', async () => { offerFrom = logs.length; await read(); });
    await step('5_3_take_one', async () => { await waitCards(offerFrom, t + '_5_3'); await click(320, 200, 2500); });
    await step('5_4_lay_catalyst_pair', async () => { await lay(169, 4); await lay(207, 5); });
    await step('5_4b_ignite', ignite);
    await step('5_4c_catalyst_parts', read);
    await step('5_5_done', read);
    const all = lines.join('\n');
    for (const need of t === 'a' ? ['Base 20', 'x2', '40', '240'] : ['40', '240']) if (!all.includes(need)) throw new Error('tutorial popups lack ' + need);
    console.log(`tutorial ${t}: ${lines.length} steps`);
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
  for (let i = 0; i < 40 && tutLogs().length === 0; i++) await click(60, 180, 450);  // through the intro, up to the first tutorial step
  await playTutorial('a');
  await sleep(2000);
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
  await click(320, 317, 800);                        // Settings (260,304 120x26); Reset progress lives inside it
  await click(320, 213, 700);                        // Reset progress: it asks first
  await shot('b04_reset_asks');
  await click(392, 238, 900);                        // Erase
  await shot('b05_after_reset');
  await click(320, 244, 700);                        // Back
  console.log('errors after B:', errorsSoFar());

  // ---- D: the Settings panel: language VI and back (desktop: no Vibration row), saved on close ----
  await click(320, 317, 800);                        // Settings
  await shot('d01_settings_en');
  await click(375, 178, 900);                        // the whole Language cell switches to Vietnamese at once
  await shot('d02_settings_vi');
  await size(640, 360);                              // 1x: the panel at its true pixel size, VI then EN
  await shot('d02_settings_vi_1x');
  await click(375, 178, 900);
  await shot('d02_settings_en_1x');
  await click(375, 178, 900);                        // back to Vietnamese for the rest of D
  await size(1280, 720);
  await click(320, 244, 900);                        // Back writes the file
  await shot('d03_title_vi');
  await click(320, 257, 900);                        // Journal has not moved
  await shot('d04_journal_vi');
  await click(8, 200, 700);
  await load('reload');                              // the choice is kept
  await shot('d05_title_vi_after_reload');
  await click(320, 317, 800);
  await click(375, 178, 900);                        // and back to English
  await shot('d06_settings_en_again');
  await click(8, 20, 700);                           // a click outside closes
  await shot('d07_title_en_again');
  await click(320, 257, 900);                        // Journal still at (320,257)
  await shot('d08_journal_en');
  await click(8, 200, 700);
  console.log('errors after D:', errorsSoFar());

  // ---- T: How to play in Vietnamese, from the title, on a throwaway journal ----
  const langs = () => logs.filter((l) => l.includes('[loc]'));
  const before = langs().length;
  await click(320, 317, 800);                        // Settings
  await click(375, 178, 900);                        // the language cell
  if (!langs().slice(before).some((l) => l.includes('[loc] vi'))) await click(375, 178, 900);   // it was Vietnamese: switch back to it
  if (!langs().at(-1).includes('[loc] vi')) throw new Error('language is not Vietnamese: ' + langs().at(-1));
  await click(320, 244, 900);                        // Back
  await click(190, 317, 1500);                       // How to play
  await playTutorial('vi');
  if (!tutLogs().slice(-30).some((l) => /Bấm|Bước/.test(l))) throw new Error('no Vietnamese tutorial line in the log');
  await sleep(2500); await shot('vi_back_on_title');
  console.log('errors after T:', errorsSoFar());

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
  const c04From = logs.length;
  await click(320, 227, 1000);                       // Begin: the intro has been seen, so straight to the first depth
  await startOfDepth('c04', c04From);
  await shot('c05_bench_after_damage');
  console.log('errors after C:', errorsSoFar());

  // ---- E: the pause menu (5a-13). Button picture (12,152) 28x20, hit box 44x44 at (4,140); desktop menu has no Vibration row ----
  const quits = () => logs.filter((l) => l.includes('[pause] quit')).length;
  const esc = async (wait = 600) => { for (const type of ['keyDown', 'keyUp']) await send('Input.dispatchKeyEvent', { type, key: 'Escape', code: 'Escape', windowsVirtualKeyCode: 27, nativeVirtualKeyCode: 27 }); await sleep(wait); };
  const PAUSE = [26, 162], RESUME = [320, 138], QUIT = [320, 210], QUIT_YES = [392, 238], QUIT_NO = [248, 238];
  // Waiting by condition: the Quit hook is the only log line, so a screen is "settled" when two screenshots 700 ms apart are
  // byte-identical (the game's own fades have finished); the pause button is checked by eye in the shots.
  const settled = async (maxMs = 15000) => {
    let prev = await frame();
    for (let t = 0; t < maxMs; t += 800) { await sleep(700); const cur = await frame(); if (cur === prev) return true; prev = cur; }
    return false;
  };
  const doQuit = async (tag) => {                    // click Yes, wait for the hook, then for the title to stop moving
    const n = quits(); await click(...QUIT_YES, 300);
    for (let i = 0; i < 30 && quits() === n; i++) { await sleep(400); await frame(); }   // headless Chrome only runs the game's frames when asked for a picture
    if (quits() !== n + 1) { await shot('fail_quit_' + tag); throw new Error('Quit did not reach the game: ' + tag); }
    await sleep(2500);
    if (!await settled()) console.log('note: screen still moving after Quit: ' + tag);
  };
  const pauseAndQuit = async (tag) => {
    await click(...PAUSE, 800); await shot(tag + '_menu');
    await click(...QUIT, 800); await shot(tag + '_confirm');
    await doQuit(tag);
  };
  const beginRun = async (tag) => { const from = logs.length; await click(320, 227, 1500); await sleep(5000); await startOfDepth(tag, from); await settled(); };   // Begin: intro seen, so the aspect offer
  // the language survives a damaged save (the choice is its own file): read it from the [loc] hook, else from the first shot
  const setLang = async (want) => {
    for (let k = 0; k < 2; k++) {
      const n = langs().length;
      await click(320, 317, 800); await click(375, 178, 900);
      const now = langs().length > n ? langs().at(-1) : '';
      await click(320, 244, 800);
      if (now.includes('[loc] ' + want)) return;
    }
    throw new Error('could not switch language to ' + want);
  };
  await pauseAndQuit('e00_vi');                      // C's bench is a Vietnamese run: leave it by Quit, then switch to English
  await setLang('en');
  await beginRun('e00');
  // the bench of c05 is a run in progress
  await click(...PAUSE, 800); await shot('e01_pause_menu_en');
  await click(...RESUME, 800); await shot('e02_resumed');
  await click(...PAUSE, 800);                        // open again
  await click(...QUIT, 800); await shot('e03_quit_confirm_en');
  await size(500, 300); await shot('e04_quit_confirm_narrow_500x300'); await size(1280, 720);
  await click(...QUIT_NO, 800); await shot('e04b_cancel_back_to_menu');       // Cancel: back to the menu
  await esc(); await shot('e05_esc_closed');         // Esc closes the menu
  await esc(); await shot('e05b_esc_opens');         // and opens it again
  await click(...RESUME, 800);
  await click(...PAUSE, 800);
  await click(...QUIT, 800); await shot('e05c_confirm_again'); await doQuit('e05'); await shot('e05d_after_yes');
  await shot('e06_title_after_quit');
  await click(320, 257, 900); await shot('e07_journal_from_title'); await click(8, 200, 700);   // Journal still at (320,257)
  await beginRun('e08'); await shot('e09_new_run_begins');       // Begin works
  await pauseAndQuit('e10');
  // pause inside the tutorial: How to play, step 1.1, pause, nothing advances, Esc, pause, Quit
  tutSeen = tutLogs().length;
  await click(190, 317, 1500);
  const first = await waitStep('e', 'tut_before_pause');
  await click(...PAUSE, 800); await shot('e11_tutorial_paused');
  const tutBefore = tutLogs().length; await pump(3500); await click(60, 180, 600);   // a click outside the menu closes it: this one proves the step does not advance on the way out, and does it after 3.5 s paused
  await shot('e11b_after_outside_click');
  if (tutLogs().length !== tutBefore) throw new Error('the tutorial advanced while paused');
  await esc(); await shot('e12_esc_opens_again');
  await esc(700); await shot('e12b_tutorial_resumed_by_esc');
  await pauseAndQuit('e13_tut');
  await shot('e14_title_after_tutorial_quit');
  // (TutorialDone staying unset after Quit is not testable here: the session has long finished the tutorial; covered in PauseTests)
  console.log('errors after E (EN):', errorsSoFar(), '| first tutorial step:', first.slice(0, 60));
  // Vietnamese
  await setLang('vi');
  await beginRun('e15');
  await click(...PAUSE, 800); await shot('e16_pause_menu_vi');
  await click(...QUIT, 800); await shot('e17_quit_confirm_vi');
  await size(500, 300); await shot('e18_quit_confirm_vi_narrow_500x300'); await size(1280, 720);
  await doQuit('e19');
  await shot('e19_title_vi_after_quit');
  console.log('errors after E:', errorsSoFar());
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
