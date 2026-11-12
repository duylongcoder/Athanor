#!/usr/bin/env node
// Writes every sprite the game uses into the Unity project, plus Resources/layout.json and a contact sheet.
//   node tools/art/export.mjs
import { writeFileSync, mkdirSync, readFileSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';
import { Canvas } from './painter.mjs';
import { ICONS, CATALYST_ICONS } from './icons.mjs';
import * as S from './scene.mjs';
import { uiSprites, CARD, RACK, JOURNAL } from './ui.mjs';

const here = dirname(fileURLToPath(import.meta.url)), assets = join(here, '../../Athanor/Assets');
const content = JSON.parse(readFileSync(join(assets, 'Resources/content.json'), 'utf8'));
const placed = {};
let count = 0;
function save(rel, cv) { const f = join(assets, 'Art', rel); mkdirSync(dirname(f), { recursive: true }); writeFileSync(f, cv.png()); count++; }
/** Saves a full-frame layer cropped to its content and records where it sits. */
function saveCropped(rel, cv) { const b = cv.bounds(); placed[rel.split('/').pop().replace('.png', '')] = b; save(rel, cv.crop(b.x, b.y, b.w, b.h)); }

// icons
const missing = content.materials.filter((m) => !ICONS[m.id]).map((m) => m.id).concat(content.catalysts.filter((c) => !CATALYST_ICONS[c.id]).map((c) => c.id));
if (missing.length) { console.error('no icon for: ' + missing.join(', ')); process.exit(1); }
for (const m of content.materials) save(`Icons/${m.id}.png`, ICONS[m.id]().canvas());
for (const c of content.catalysts) save(`Icons/cat_${c.id}.png`, CATALYST_ICONS[c.id]().canvas());

// scenery
save('Background/workshop_wall.png', S.benchWall());
saveCropped('Background/window.png', S.windowPane());
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
  card: CARD, rack: RACK, journal: JOURNAL,
};
writeFileSync(join(assets, 'Resources/layout.json'), JSON.stringify(layout, null, 1));

// contact sheet of every icon at 4x for a quick look
const all = [...content.materials.map((m) => ICONS[m.id]), ...content.catalysts.map((c) => CATALYST_ICONS[c.id])], cols = 10, k = 4, cell = 24 * k + 8;
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
console.log(`${count} sprites written to Athanor/Assets/Art, layout.json to Resources`);
