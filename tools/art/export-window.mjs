#!/usr/bin/env node
// Writes only the window views (Background/window_d1..d5.png, Fx/window_drip.png) and a contact sheet; no layout.json, no other sprites.
//   node tools/art/export-window.mjs [contactSheetPath]
import { writeFileSync, mkdirSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';
import { Canvas } from './painter.mjs';
import * as S from './scene.mjs';
const here = dirname(fileURLToPath(import.meta.url)), art = join(here, '../../Athanor/Assets/Art');
const put = (rel, cv) => { const f = join(art, rel); mkdirSync(dirname(f), { recursive: true }); writeFileSync(f, cv.png()); };
const views = [1, 2, 3, 4, 5].map((d) => S.windowView(d)), crops = [];
views.forEach((v, i) => { const b = v.bounds(); crops.push([b, v]); put(`Background/window_d${i + 1}.png`, v.crop(b.x, b.y, b.w, b.h)); });
put('Fx/window_drip.png', S.windowDrip());
console.log('bounds', JSON.stringify(crops.map(([b]) => b)), 'window.png bounds', JSON.stringify(S.windowPane().bounds()));
if (process.argv[2]) {
  const k = 4, cw = 105 * k + 8, sheet = new Canvas(cw * 6 + 8, 97 * k + 8 + 120);
  sheet.fillStyle = '#1a1220'; sheet.fillRect(0, 0, sheet.w, sheet.h);
  [S.windowPane(), ...views].forEach((v, i) => { const b = v.bounds(), c = v.crop(b.x, b.y, b.w, b.h); sheet.drawImage(c, 8 + i * cw, 8, c.w * k, c.h * k); });
  const dr = S.windowDrip(); sheet.drawImage(dr, 8, 97 * k + 24, dr.w * 8, dr.h * 8);
  mkdirSync(dirname(process.argv[2]), { recursive: true }); writeFileSync(process.argv[2], sheet.png());
}
