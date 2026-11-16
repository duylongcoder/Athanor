#!/usr/bin/env node
// Writes every sprite the game uses into the Unity project, plus Resources/layout.json and a contact sheet.
//   node tools/art/export.mjs
import { writeFileSync, mkdirSync, readFileSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';
import { spawnSync } from 'node:child_process';
import { Canvas } from './painter.mjs';
import { ICONS, CATALYST_ICONS, ASPECT_ICONS, EVENT_ICONS } from './icons.mjs';
import * as S from './scene.mjs';
import { uiSprites, CARD, RACK, JOURNAL, GOALS, OPTION } from './ui.mjs';

const here = dirname(fileURLToPath(import.meta.url)), assets = join(here, '../../Athanor/Assets');
const content = JSON.parse(readFileSync(join(assets, 'Resources/content.json'), 'utf8'));
const placed = {};
let count = 0;
function save(rel, cv) { const f = join(assets, 'Art', rel); mkdirSync(dirname(f), { recursive: true }); writeFileSync(f, cv.png()); count++; }
/** Saves a full-frame layer cropped to its content and records where it sits. */
function saveCropped(rel, cv) { const b = cv.bounds(); placed[rel.split('/').pop().replace('.png', '')] = b; save(rel, cv.crop(b.x, b.y, b.w, b.h)); }

// icons
const eventTypes = [...new Set((content.events ?? []).map((e) => e.type))];
const missing = content.materials.filter((m) => !ICONS[m.id]).map((m) => m.id).concat(content.catalysts.filter((c) => !CATALYST_ICONS[c.id]).map((c) => c.id))
  .concat((content.aspects ?? []).filter((a) => !ASPECT_ICONS[a.id]).map((a) => 'aspect ' + a.id)).concat(eventTypes.filter((t) => !EVENT_ICONS[t]).map((t) => 'event type ' + t));
if (missing.length) { console.error('no icon for: ' + missing.join(', ')); process.exit(1); }
for (const m of content.materials) save(`Icons/${m.id}.png`, ICONS[m.id]().canvas());
for (const c of content.catalysts) save(`Icons/cat_${c.id}.png`, CATALYST_ICONS[c.id]().canvas());
for (const a of content.aspects ?? []) save(`Icons/asp_${a.id}.png`, ASPECT_ICONS[a.id]().canvas());
for (const t of eventTypes) save(`Icons/evt_${t.toLowerCase()}.png`, EVENT_ICONS[t]().canvas());

// scenery
save('Background/workshop_wall.png', S.benchWall());
saveCropped('Background/window.png', S.windowPane());
for (let d = 1; d <= 5; d++) saveCropped(`Background/window_d${d}.png`, S.windowView(d));
saveCropped('Background/workshop_table.png', S.benchTable());
save('Background/moon_shaft.png', S.moonShaft());
save('Background/candle.png', S.candle().canvas());
for (let k = 0; k < 8; k++) { save(`Background/flame_small_${k}.png`, S.flame(false, k * 0.171, 0.3)); save(`Background/flame_big_${k}.png`, S.flame(true, k * 0.171, 1.1)); }
save('Background/cutscene_room.png', S.cutRoom());
save('Background/logs.png', S.logs().canvas());
save('Background/cauldron.png', S.cauldron().canvas());

// effect shapes
save('Fx/glow.png', S.glow(64));
save('Fx/sparkle.png', S.sparkleDot());
save('Fx/dot.png', (() => { const g = new Canvas(4, 4); g.fillStyle = '#ffffff'; g.fillRect(0, 0, 4, 4); return g; })());
save('Fx/rune_circle.png', S.runeCircle());
save('Fx/floor_ring.png', S.ring(60, 14));
for (let k = 0; k < 4; k++) save(`Fx/liquid_${k}.png`, S.liquid(k * 0.55));
save('Fx/pillar.png', S.pillar());
save('Fx/ray.png', S.ray());
save('Fx/window_drip.png', S.windowDrip());

// tiles
const tiles = [];
for (let i = 0; i < 9; i++) {
  const c = S.tileCenter(i), rx = c.w * 0.3, ry = rx * 0.42;
  saveCropped(`Fx/tile_face_${i}.png`, S.tileFace(i));
  if (i % 3 === 0) save(`Fx/tile_ring_${i / 3}.png`, S.ring(rx, ry));
  tiles.push({ x: Math.round(c.x), y: Math.round(c.y), w: Math.round(c.w), face: placed['tile_face_' + i] });
}

// ui
const ui = uiSprites();
for (const [name, cv] of Object.entries(ui)) save(`UI/${name}.png`, cv);

const layout = {
  w: S.W, h: S.H, tiles,
  placed: Object.entries(placed).map(([name, b]) => ({ name, ...b })),
  candles: S.CANDLES.map(([x, y]) => ({ x, y })), cauldron: S.CAULDRON,
  card: CARD, rack: RACK, journal: JOURNAL, goals: GOALS, option: OPTION,
};
writeFileSync(join(assets, 'Resources/layout.json'), JSON.stringify(layout, null, 1));

// contact sheet of every icon at 4x for a quick look
const all = [...content.materials.map((m) => ICONS[m.id]), ...content.catalysts.map((c) => CATALYST_ICONS[c.id]),
  ...(content.aspects ?? []).map((a) => ASPECT_ICONS[a.id]), ...eventTypes.map((t) => EVENT_ICONS[t])], cols = 10, k = 4, cell = 24 * k + 8;
const sheet = new Canvas(cols * cell, Math.ceil(all.length / cols) * cell); sheet.fillStyle = '#1a1220'; sheet.fillRect(0, 0, sheet.w, sheet.h);
all.forEach((mk, i) => sheet.drawImage(mk().canvas(), (i % cols) * cell + 4, Math.floor(i / cols) * cell + 4, 24 * k, 24 * k));
mkdirSync(join(here, 'out'), { recursive: true });
writeFileSync(join(here, 'out/contact.png'), sheet.png());
// previews: the bench layers stacked, the cutscene room, and the UI pieces
const bench = S.benchWall(); bench.drawImage(S.windowPane(), 0, 0); bench.drawImage(S.benchTable(), 0, 0);
S.CANDLES.forEach(([x, y]) => { bench.drawImage(S.candle(), x, y); bench.drawImage(S.flame(false, 0, 0), x, y - 8); });
bench.drawImage(ui.hud_left, 8, 8); bench.drawImage(ui.hud_right, 480, 8); bench.drawImage(ui.note, 4, 262); bench.drawImage(ui.corner, 514, 284); bench.drawImage(ui.ignite_up, 519, 310);
bench.drawImage(ui.rack, 130, 256); for (let i = 0; i < 5; i++) bench.drawImage(i === 2 ? ui.card_sel : ui.card, 131 + i * 76, i === 2 ? 280 : 290);
writeFileSync(join(here, 'out/bench.png'), bench.png());
const cut = S.cutRoom(); cut.drawImage(S.logs(), 260, 282); cut.drawImage(S.cauldron(), 220, 118);
for (let i = 0; i < 7; i++) cut.drawImage(S.flame(true, i, i), 262 + i * 16, 274);
writeFileSync(join(here, 'out/cutscene.png'), cut.png());
const menus = new Canvas(640, 360); menus.fillStyle = '#1a1220'; menus.fillRect(0, 0, 640, 360);
menus.drawImage(ui.journal, 20, 15);
writeFileSync(join(here, 'out/journal.png'), menus.png());
const goalsPage = new Canvas(640, 360); goalsPage.fillStyle = '#1a1220'; goalsPage.fillRect(0, 0, 640, 360);
goalsPage.drawImage(ui.goals, 20, 15); goalsPage.drawImage(ui.option_up, 176, 200); goalsPage.drawImage(ui.toast, 170, 4);
writeFileSync(join(here, 'out/goals.png'), goalsPage.png());
console.log(`${count} sprites written to Athanor/Assets/Art, layout.json to Resources`);

// window views: export-window.mjs stays its own script (it can run alone) but one command rebuilds all art.
// A failure there is reported by name and fails this command; nothing is swallowed.
const win = spawnSync(process.execPath, [join(here, 'export-window.mjs')], { stdio: 'inherit' });
if (win.status !== 0) { console.error(`export-window.mjs failed (${win.error ? win.error.message : 'exit ' + win.status}); window views may be stale`); process.exit(win.status || 1); }

// HD-2D depth layers (pre-blurred far art, near corners, mist) -> Art/Depth, same pattern.
const dep = spawnSync(process.execPath, [join(here, 'export-depth.mjs')], { stdio: 'inherit' });
if (dep.status !== 0) { console.error(`export-depth.mjs failed (${dep.error ? dep.error.message : 'exit ' + dep.status})`); process.exit(dep.status || 1); }
