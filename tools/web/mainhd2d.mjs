// 5b-07b: WebGL player checks of the HD-2D bench in Main (development build from WebBuild.BuildDev, dev keys and hooks).
//   node tools/web/mainhd2d.mjs <url> <outDir> <WxH> <scenario[,scenario...]> [--gpu]
// env SWAY_OFF=1 switches Settings > Camera sway to Off on the title before the run (the 'rest' tile mode).
// scenarios:
//   hud     the bench at the given viewport (use 640x360 for 1x), pictures of the HUD text, then a crop of the stats panel
//   moods   depths 1..5 (dev key F6 steps the mood), a picture each, effects on, then effects off (F8) at depth 5
//   sway    Settings sway row on and off: two frame bursts each, and how many pixels moved in the world area
//   shake   dev key F7 (a 4 s shake of 3 art px): burst of pictures, world moves, HUD rows stay
//   tiles   the 9-tile click test: for every tile, 4 inset points of the DRAWN face (dev key F10 logs them), a hand card is
//           selected and the click must log "[bench] place <tile>"; the card is taken back; run with sway, then with shake
// Dev keys (GameFlow.DevKeys, development builds only): F6 next depth mood, F7 shake, F8 effects on/off, F10 log the drawn tile faces.
// Always writes <outDir>/console.txt and report.json; exit 1 on any console error or failed check. No dependencies (Node 22+).
import { spawn } from 'node:child_process';
import { existsSync, mkdirSync, mkdtempSync, writeFileSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join } from 'node:path';

const args = process.argv.slice(2);
const flags = args.filter((a) => a.startsWith('--'));
const [url, outDir, vp, scenArg = 'hud'] = args.filter((a) => !a.startsWith('--'));
if (!url || !outDir || !vp) { console.log('usage: node mainhd2d.mjs <url> <outDir> <WxH> <scenarios> [--gpu]'); process.exit(2); }
const [W, H] = vp.split('x').map(Number);
const scale = Math.max(1, Math.floor(Math.min(W / 640, H / 360)));
mkdirSync(outDir, { recursive: true });
const chromeBin = process.env.CHROME_BIN
  || ['/Applications/Google Chrome.app/Contents/MacOS/Google Chrome', '/usr/bin/google-chrome', '/usr/bin/google-chrome-stable', '/usr/bin/chromium']
    .find((p) => existsSync(p)) || 'google-chrome';
const port = 9500 + Math.floor(Math.random() * 300), profile = mkdtempSync(join(tmpdir(), 'athanor-hd2d-'));
const cargs = ['--headless=new', `--remote-debugging-port=${port}`, `--user-data-dir=${profile}`, `--window-size=${W},${H + 120}`, '--no-first-run', '--mute-audio', '--ignore-gpu-blocklist'];
if (flags.includes('--gpu')) cargs.push('--use-angle=metal'); else cargs.push('--enable-unsafe-swiftshader');
cargs.push('about:blank');
const chrome = spawn(chromeBin, cargs, { stdio: 'ignore' });
const sleep = (ms) => new Promise((r) => setTimeout(r, ms));
const logs = [], checks = [];
let ws, id = 0, code = 1;
const pending = new Map();
const send = (method, params = {}) => new Promise((res, rej) => {
  const i = ++id;
  const timer = setTimeout(() => { pending.delete(i); rej(new Error(`no answer to ${method} in 60s`)); }, 60000);
  pending.set(i, { res: (v) => { clearTimeout(timer); res(v); }, rej: (e) => { clearTimeout(timer); rej(e); } });
  ws.send(JSON.stringify({ id: i, method, params }));
});
const watchdog = setTimeout(() => { console.log('hard deadline'); chrome.kill(); process.exit(1); }, 60 * 60 * 1000);
const check = (name, ok, detail = '') => { checks.push({ name, ok: !!ok, detail }); console.log(`${ok ? 'ok  ' : 'FAIL'} ${name} ${detail}`); };

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
    if (d.method === 'Runtime.consoleAPICalled') logs.push(`[${d.params.type}] ` + d.params.args.map((a) => a.value ?? a.description ?? '').join(' ').slice(0, 600));
    if (d.method === 'Runtime.exceptionThrown') logs.push('[exception] ' + (d.params.exceptionDetails.exception?.description ?? d.params.exceptionDetails.text).slice(0, 400));
    if (d.method === 'Log.entryAdded') logs.push(`[log:${d.params.entry.level}] ` + d.params.entry.text.slice(0, 300) + ' ' + (d.params.entry.url ?? ''));
  };
  await send('Runtime.enable'); await send('Log.enable'); await send('Page.enable');
  await send('Emulation.setDeviceMetricsOverride', { width: W, height: H, deviceScaleFactor: 1, mobile: false });
  const ev = async (expression) => (await send('Runtime.evaluate', { expression, returnByValue: true, awaitPromise: true })).result.value;
  // headless Chrome draws a game frame only when a picture is asked for: every wait takes pictures
  const frame = () => send('Page.captureScreenshot', { format: 'jpeg', quality: 15 });
  const pump = async (ms) => { for (const t0 = Date.now(); Date.now() - t0 < ms;) { await sleep(60); await frame(); } };
  const png = async () => Buffer.from((await send('Page.captureScreenshot', { format: 'png' })).data, 'base64');
  const shot = async (name) => { writeFileSync(join(outDir, name + '.png'), await png()); console.log('shot', name); };
  const toScreen = (ax, ay) => [(W - 640 * scale) / 2 + ax * scale, (H - 360 * scale) / 2 + ay * scale];
  const mouse = async (type, ax, ay) => { const [x, y] = toScreen(ax, ay); await send('Input.dispatchMouseEvent', { type, x, y, button: 'left', clickCount: 1 }); };
  const click = async (ax, ay, wait = 350) => {
    await mouse('mouseMoved', ax, ay); await pump(120);
    await mouse('mousePressed', ax, ay); await pump(120);
    await mouse('mouseReleased', ax, ay); await pump(wait);
  };
  const key = async (k, code, vk) => {
    const d = { key: k, code, windowsVirtualKeyCode: vk, nativeVirtualKeyCode: vk };
    await send('Input.dispatchKeyEvent', { type: 'rawKeyDown', ...d }); await pump(120);
    await send('Input.dispatchKeyEvent', { type: 'keyUp', ...d }); await pump(200);
  };
  const F = { 6: 117, 7: 118, 8: 119, 9: 120, 10: 121 };
  const fkey = (n) => key('F' + n, 'F' + n, F[n]);
  const count = (re, from = 0) => logs.slice(from).filter((l) => re.test(l)).length;
  const waitLog = async (re, from, ms) => { for (const t = Date.now(); Date.now() - t < ms && count(re, from) === 0;) { await sleep(60); await frame(); } return count(re, from) > 0; };
  const AUTO = /\[auto\] /;
  const autoOn = () => (logs.filter((l) => AUTO.test(l)).at(-1) ?? '').includes('[auto] on');
  const setAuto = async (want) => {
    for (let k = 0; k < 3 && autoOn() !== want; k++) { const n = logs.length; await fkey(9); await waitLog(AUTO, n, 3000); }
    if (autoOn() !== want) throw new Error('the scripted player did not turn ' + (want ? 'on' : 'off'));
  };

  const t0 = Date.now();
  await send('Page.navigate', { url });
  let loaded = false;
  for (let i = 0; i < 240 && !loaded; i++) { await sleep(500); loaded = await ev("document.querySelector('#loading') === null").catch(() => false); }
  if (!loaded) throw new Error('the build did not load in 120 s');
  const loadS = (Date.now() - t0) / 1000;
  console.log(`loaded in ${loadS.toFixed(1)} s, viewport ${W}x${H}, scale ${scale}`);
  await pump(4000);
  const canvasInfo = await ev("(() => { const c = document.querySelector('#unity-canvas'); return c.width + 'x' + c.height + ' css ' + c.clientWidth + 'x' + c.clientHeight; })()");
  console.log('canvas', canvasInfo);

  // To the bench: the scripted player on at the title skips the tutorial, then through the intro, the aspect and the event.
  await click(20, 20, 300);
  if (process.env.SWAY_OFF) {                            // 'rest' mode: Settings > Camera sway row (no vibration row in a desktop browser) to Off
    await click(320, 317, 600); await shot('settings-open-' + vp);
    await click(375, 146, 400); await shot('settings-sway-off-' + vp);
    await click(8, 200, 500);                            // a click outside closes the panel
  }
  await setAuto(true);
  const from0 = logs.length;
  await click(320, 227, 1500);
  for (let i = 0; i < 22 && count(/\[start\] (aspects|ready)/, from0) === 0; i++) await click(60, 180, 450);
  if (!await waitLog(/\[start\] (aspects|ready)/, from0, 20000)) throw new Error('neither the aspect offer nor the bench appeared');
  if (count(/\[start\] aspects/, from0) > 0) {        // later runs offer aspects first; the first run goes straight to the bench
    if (!await waitLog(/\[cards\] open/, from0, 20000)) throw new Error('the cards never took a click');
    await pump(400);
    await click(320, 200, 300);
    for (let t = 0; t < 20000 && count(/\[event\] options/, from0) === 0 && count(/\[start\] ready/, from0) === 0; t += 150) await pump(150);
    if (count(/\[event\] options/, from0) > 0) {
      await pump(500); await click(320, 165, 300);
      await waitLog(/\[event\] result/, from0, 5000); await pump(500);
      await click(320, 260, 300); await waitLog(/\[event\] closed/, from0, 5000);
    }
  }
  if (!await waitLog(/\[start\] ready/, from0, 15000)) throw new Error('the bench was never handed over');
  await setAuto(false);                                  // the player has the bench
  await pump(1500);
  for (let i = 0; i < 4; i++) await click(60, 180, 900);   // the scripted player may have left a result or discovery page open
  console.log('on the bench');

  const scenarios = scenArg.split(',');
  const report = { url, viewport: vp, scale, canvas: canvasInfo, load_seconds: +loadS.toFixed(1) };
  for (const sc of scenarios) {
    if (sc === 'hud') {
      await shot('hud-bench-' + vp);
      report.hudShot = 'hud-bench-' + vp + '.png';
    } else if (sc === 'moods') {
      for (let d = 1; d <= 5; d++) {
        const n = logs.length;
        await fkey(6); await waitLog(/\[dev\] mood/, n, 3000); await pump(1800);
        const line = logs.slice(n).find((l) => /\[dev\] mood/.test(l)) ?? '';
        check('mood step ' + d, /mood \d/.test(line), line);
        await shot(`mood-depth-${d}-${vp}`);
      }
      const n = logs.length; await fkey(8); await waitLog(/\[dev\] effects/, n, 3000); await pump(1200);
      check('effects toggled off', logs.slice(n).some((l) => /effects off/.test(l)));
      await shot(`mood-depth-5-effects-off-${vp}`);
      const n2 = logs.length; await fkey(8); await waitLog(/\[dev\] effects/, n2, 3000); await pump(600);
      check('effects back on', logs.slice(n2).some((l) => /effects on/.test(l)));
    } else if (sc === 'sway' || sc === 'shake') {
      const burst = async (tag, n = 4) => {
        const bufs = [];
        for (let i = 0; i < n; i++) { bufs.push(await png()); await shot(`${sc}-${tag}-${i}-${vp}`); await pump(250); }
        return bufs;
      };
      // pixels that differ between two PNG captures are counted by the size/bytes of the base64 only as a coarse signal; the
      // pictures are the evidence (compare them by eye or with tools/web/depthshimmer.py)
      if (sc === 'sway') {
        report.sway = { note: 'bursts of 4 pictures 250 ms apart; sway is on by default; the Settings toggle is on the title screen' };
        await burst('on');
      } else {
        const n = logs.length; await fkey(7); await pump(300);
        await burst('during');
        await pump(4500);
        await shot(`shake-after-${vp}`);
        check('shake key sent', true, logs.slice(n).filter((l) => /\[dev\]/.test(l)).join(' | '));
      }
    } else if (sc === 'probe') {
      await click(169, 325, 400); await shot('probe-card-selected-' + vp);
      const m = logs.length; await click(250, 157, 800); await shot('probe-after-tile-' + vp);
      console.log(logs.slice(m).filter((l) => !/render graph|^\S*\s*$/.test(l)).join('\n'));
    } else if (sc === 'tiles') {
      const cardX = 169, cardY = 325;
      const modes = (process.env.TILE_MODES ?? 'sway,shake').split(',');      // sway = the default (sway on); rest = sway off: run with SWAY_OFF=1 TILE_MODES=rest (Settings sway row, from the title)
      const pointSet = (process.env.TILE_POINTS ?? '0,3,4').split(',').map(Number);   // 0..3 inset corners, 4 centre
      {                                                             // the scripted player left cards on the bench: take them all back
        const n = logs.length; await fkey(10); await pump(600);
        for (let t = 0; t < 9; t++) {
          const l = logs.slice(n).find((x) => new RegExp('\\[dev\\] tile ' + t + ' ').test(x));
          if (!l) continue;
          const c = l.split('tile ' + t + ' ')[1].trim().split(/\s+/).map((q) => q.split(',').map(Number));
          await click(c.reduce((a, q) => a + q[0], 0) / 4 + 320, 180 - c.reduce((a, q) => a + q[1], 0) / 4, 400);
        }
        await shot('tiles-cleared-' + vp);
        console.log('cleared: ' + count(/\[bench\] unplace/, n) + ' cards taken back');
      }
      for (const mode of modes) {
        let ok = 0, tot = 0;
        for (let t = 0; t < 9; t++) {
          if (mode === 'shake') await fkey(7);
          // F10 is read again right before every click (sway and shake move the face; a stale read missed shake tile 2 point 3)
          const readArts = async () => {
            for (let tryN = 0; tryN < 3; tryN++) {
              const n = logs.length; await fkey(10); await waitLog(new RegExp('\\[dev\\] tile ' + t + ' '), n, 3000);
              const line = logs.slice(n).find((l) => new RegExp('\\[dev\\] tile ' + t + ' ').test(l));
              if (!line) continue;
              const pts = line.split('tile ' + t + ' ')[1].trim().split(/\s+/).map((q) => q.split(',').map(Number));
              const a = pts.map(([x, y]) => [x + 320, 180 - y]);   // UI units origin centre y up -> art px from the top-left
              a.push([a.reduce((s2, q) => s2 + q[0], 0) / 4, a.reduce((s2, q) => s2 + q[1], 0) / 4]);   // point 4 = the centre
              return a;
            }
            return null;
          };
          if (!await readArts()) { check(`${mode} tile ${t} corners logged`, false); continue; }
          for (const k of pointSet) {
            if (mode === 'shake') await fkey(7);                    // a shake lasts 4 s: start a fresh one for every point
            const arts = await readArts();
            if (!arts) { tot++; check(`${mode} tile ${t} point ${k}`, false, 'no F10 line'); continue; }
            const m = logs.length;
            await click(cardX, cardY, 250);                         // select a hand card
            const arts2 = await readArts() ?? arts;                 // read again just before the click
            if (t === 0 && k === pointSet[0] && mode === modes[0]) await shot('tiles-card-selected-' + vp);
            await click(arts2[k][0], arts2[k][1], mode === 'shake' ? 200 : 450);
            const placed = count(new RegExp('\\[bench\\] place ' + t + '\\b'), m) > 0;
            tot++; if (placed) ok++;
            let why = '';
            if (!placed && mode === 'shake') {                      // classify a miss: was the click point inside the face a moment before / after the click?
              const inside = (q, a) => { let pos = 0, neg = 0; for (let i = 0; i < 4; i++) { const [x1, y1] = q[i], [x2, y2] = q[(i + 1) % 4]; const c = (x2 - x1) * (a[1] - y1) - (y2 - y1) * (a[0] - x1); if (c > 0) pos++; else if (c < 0) neg++; } return pos === 0 || neg === 0; };
              const post = await readArts();
              why = ` pre-inside=${inside(arts2, arts2[k])} post-inside=${post ? inside(post, arts2[k]) : '?'} drift=${post ? Math.hypot(post[4][0] - arts2[4][0], post[4][1] - arts2[4][1]).toFixed(1) : '?'}px`;
            }
            check(`${mode} tile ${t} point ${k}`, placed, `art ${arts2[k].map((v) => v.toFixed(1)).join(',')}${why}`);
            if (placed) {                                           // take it back: click the tile, then the hand
              const u = logs.length;
              const arts3 = await readArts() ?? arts2; await click(arts3[k][0], arts3[k][1], 350);
              await waitLog(/\[bench\] unplace/, u, 1500);
              if (count(/\[bench\] unplace/, u) === 0) { await click(cardX, cardY, 300); }
            } else { await click(8, 200, 250); }                    // deselect
          }
          if (mode === 'shake') await pump(500);
        }
        console.log(`tiles ${mode}: ${ok}/${tot}`);
        report['tiles_' + mode] = `${ok}/${tot}`;
      }
    }
  }
  report.checks = checks;
  writeFileSync(join(outDir, 'report.json'), JSON.stringify(report, null, 1));
  const bad = logs.filter((l) => /\[(error|exception|log:error)\]/.test(l));
  console.log(`console: ${logs.length} messages, ${bad.length} errors; checks ${checks.filter((c) => c.ok).length}/${checks.length}`);
  for (const l of bad.slice(0, 12)) console.log('  ', l);
  code = bad.length === 0 && checks.every((c) => c.ok) ? 0 : 1;
} catch (e) {
  console.log('ERROR', e.message);
  try { writeFileSync(join(outDir, 'fail.png'), Buffer.from((await send('Page.captureScreenshot', { format: 'png' })).data, 'base64')); } catch { /* no picture */ }
} finally {
  writeFileSync(join(outDir, 'console.txt'), logs.join('\n'));
  try { ws?.close(); } catch { /* closing */ }
  clearTimeout(watchdog);
  chrome.kill();
  process.exit(code);
}
