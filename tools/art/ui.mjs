// HUD and menu pieces, each drawn at the exact size it is shown (no stretching).
import { R, INK, hsh, Spr, Canvas } from './painter.mjs';

function panel(g, x, y, w, h, studs = true) {
  g.fillStyle = '#0c0706'; g.fillRect(x, y, w, h);
  g.fillStyle = '#2c1a14'; g.fillRect(x + 1, y + 1, w - 2, h - 2);
  for (let yy = y + 2; yy < y + h - 2; yy++) for (let xx = x + 2; xx < x + w - 2; xx++) { const n = hsh(xx >> 2, yy, 41); if (n > 0.86) { g.fillStyle = '#3a241a'; g.fillRect(xx, yy, 1, 1); } else if (n < 0.1) { g.fillStyle = '#22130f'; g.fillRect(xx, yy, 1, 1); } }
  g.fillStyle = '#6a4428'; g.fillRect(x + 1, y + 1, w - 2, 1); g.fillRect(x + 1, y + 1, 1, h - 2);
  g.fillStyle = '#170c0a'; g.fillRect(x + 1, y + h - 2, w - 2, 1); g.fillRect(x + w - 2, y + 1, 1, h - 2);
  if (studs) for (const [cx, cy] of [[x + 2, y + 2], [x + w - 6, y + 2], [x + 2, y + h - 6], [x + w - 6, y + h - 6]]) {
    g.fillStyle = R.gold[1]; g.fillRect(cx, cy, 4, 4); g.fillStyle = R.gold[3]; g.fillRect(cx, cy, 3, 3); g.fillStyle = R.gold[4]; g.fillRect(cx, cy, 1, 1); g.fillStyle = R.gold[2]; g.fillRect(cx + 1, cy + 1, 2, 2);
  }
}
function parchment(g, x, y, w, h, seed = 12) {
  g.fillStyle = 'rgba(12,6,4,0.6)'; g.fillRect(x + 1, y + 2, w, h);
  for (let yy = 0; yy < h; yy++) for (let xx = 0; xx < w; xx++) {
    if ((xx < 2 || xx >= w - 2) && hsh(xx, yy >> 1, 7) > 0.6) continue;
    const n = hsh(xx, yy, seed), edge = Math.min(xx, yy, w - 1 - xx, h - 1 - yy);
    g.fillStyle = edge < 2 ? '#a8854e' : n > 0.9 ? '#e8d4a0' : n < 0.08 ? '#c4a870' : '#d8c088'; g.fillRect(x + xx, y + yy, 1, 1);
  }
}
function socket(g, x, y, filled, size = 30) {
  const s = size;
  g.fillStyle = '#0c0706'; g.fillRect(x, y, s, s);
  g.fillStyle = filled ? R.gold[2] : '#4a3020'; g.fillRect(x + 1, y + 1, s - 2, s - 2);
  g.fillStyle = filled ? R.gold[4] : '#6a4428'; g.fillRect(x + 1, y + 1, s - 2, 1); g.fillRect(x + 1, y + 1, 1, s - 2);
  g.fillStyle = filled ? R.gold[1] : '#24140e'; g.fillRect(x + 1, y + s - 2, s - 2, 1); g.fillRect(x + s - 2, y + 1, 1, s - 2);
  g.fillStyle = '#160e14'; g.fillRect(x + 3, y + 3, s - 6, s - 6); g.fillStyle = '#241828'; g.fillRect(x + 4, y + 4, s - 8, s - 8);
}
function button(g, x, y, w, h, state) {
  const K = state === 'off' ? ['#1c1a26', '#3a3646', '#514c5c', '#6e6878'] : [R.red[0], R.red[1], R.red[2], R.orange[3]], down = state === 'down' ? 2 : 0, lip = 4 - down * 2;
  g.fillStyle = '#0c0706'; g.fillRect(x, y, w, h);
  g.fillStyle = K[1]; g.fillRect(x + 1, y + 1, w - 2, h - 2);
  g.fillStyle = K[2]; g.fillRect(x + 2, y + 2 + down, w - 4, h - 4 - lip - down);
  g.fillStyle = K[3]; g.fillRect(x + 2, y + 2 + down, w - 4, 1); g.fillRect(x + 2, y + 2 + down, 1, h - 4 - lip - down);
  g.fillStyle = K[0]; g.fillRect(x + 2, y + h - 2 - lip, w - 4, lip);
}

const make = (w, h, draw) => { const g = new Canvas(w, h); draw(g); return g; };

export const CARD = { w: 74, h: 70 };
export const RACK = { w: 380, h: 30, slots: 12, first: 6, pitch: 31, well: 26 };
export const JOURNAL = { w: 600, h: 330, cols: 10, rows: 6, cell: 27, well: 25, gridX: 33, gridY: 60, pageX: 324, pageY: 30, pageW: 260, hintsY: 240 };

function card(sel) {
  return make(CARD.w + 3, CARD.h + 4, (g) => {
    const w = CARD.w, h = CARD.h;
    g.fillStyle = 'rgba(0,0,0,0.55)'; g.fillRect(3, 4, w, h);
    g.fillStyle = '#0c0706'; g.fillRect(0, 0, w, h);
    g.fillStyle = sel ? R.gold[3] : '#7a5430'; g.fillRect(1, 1, w - 2, h - 2);
    g.fillStyle = sel ? R.gold[4] : '#a87a48'; g.fillRect(1, 1, w - 2, 1); g.fillRect(1, 1, 1, h - 2);
    g.fillStyle = sel ? R.gold[1] : '#4a2e18'; g.fillRect(1, h - 2, w - 2, 1); g.fillRect(w - 2, 1, 1, h - 2);
    for (let yy = 3; yy < h - 3; yy++) for (let xx = 3; xx < w - 3; xx++) { const n = hsh(xx, yy, 31); g.fillStyle = n > 0.9 ? '#ecd8a6' : n < 0.08 ? '#c8ac74' : '#dcc48c'; g.fillRect(xx, yy, 1, 1); }
    g.fillStyle = '#3a2416'; g.fillRect(6, 5, w - 12, 30); g.fillStyle = '#1a1220'; g.fillRect(7, 6, w - 14, 28);
    g.fillStyle = '#2a1e34'; g.fillRect(8, 7, w - 16, 13); g.fillStyle = '#221830'; g.fillRect(8, 20, w - 16, 13);
    g.fillStyle = '#3a2416'; g.fillRect(10, 58, 54, 9);
  });
}

/** name => Canvas for every UI sprite. */
export function uiSprites() {
  const out = {};
  out.hud_left = make(152, 76, (g) => {
    panel(g, 0, 0, 152, 76);
    g.fillStyle = '#0c0706'; g.fillRect(8, 60, 136, 9); g.fillStyle = R.gold[1]; g.fillRect(9, 61, 134, 7); g.fillStyle = '#1a0e14'; g.fillRect(10, 62, 132, 5);
  });
  out.bar_fill = make(4, 5, (g) => { g.fillStyle = R.orange[2]; g.fillRect(0, 0, 4, 5); g.fillStyle = R.gold[3]; g.fillRect(0, 0, 4, 2); });
  out.hud_right = make(152, 76, (g) => { panel(g, 0, 0, 152, 76); for (let i = 0; i < 4; i++) socket(g, 8 + i * 35, 16, false); });
  out.socket_filled = make(30, 30, (g) => socket(g, 0, 0, true));
  out.note = make(124, 93, (g) => { parchment(g, 0, 0, 122, 90); g.fillStyle = '#b89860'; g.fillRect(5, 53, 112, 1); });
  out.corner = make(122, 68, (g) => panel(g, 0, 0, 122, 68));
  for (const s of ['up', 'down', 'off']) out['ignite_' + s] = make(112, 36, (g) => button(g, 0, 0, 112, 36, s));
  for (const s of ['up', 'down']) out['button_' + s] = make(120, 26, (g) => button(g, 0, 0, 120, 26, s));
  out.book = (() => { const b = new Spr(18, 14); b.rect(1, 1, 16, 12, (x, y) => (x < 3 ? R.red[0] : y < 2 ? R.red[3] : R.red[1])); b.rect(4, 12, 13, 1, R.bone[3]); b.rect(7, 4, 6, 5, R.gold[2]); b.rect(8, 5, 4, 3, R.gold[4]); return b.outline().canvas(); })();
  out.card = card(false); out.card_sel = card(true);
  out.rack = make(RACK.w, RACK.h, (g) => {
    panel(g, 0, 0, RACK.w, RACK.h, false);
    for (let i = 0; i < RACK.slots; i++) socket(g, RACK.first + i * RACK.pitch, 2, false, RACK.well);
  });
  out.rack_slot_lit = make(RACK.well, RACK.well, (g) => socket(g, 0, 0, true, RACK.well));
  out.journal = make(JOURNAL.w, JOURNAL.h, (g) => {
    const J = JOURNAL;
    panel(g, 0, 0, J.w, J.h);
    parchment(g, 16, 30, 300, 286, 3); parchment(g, J.pageX, J.pageY, J.pageW, 286, 9);
    for (let r = 0; r < J.rows; r++) for (let c = 0; c < J.cols; c++) {
      const x = J.gridX + c * J.cell, y = J.gridY + r * J.cell;
      g.fillStyle = '#8a6a3c'; g.fillRect(x, y, J.well, J.well); g.fillStyle = '#b89860'; g.fillRect(x + 1, y + 1, J.well - 2, J.well - 2);
      g.fillStyle = '#c8ac74'; g.fillRect(x + 2, y + 2, J.well - 3, J.well - 3);
    }
    g.fillStyle = '#b89860'; g.fillRect(28, J.hintsY - 6, 276, 1);
  });
  out.journal_pick = make(JOURNAL.well + 4, JOURNAL.well + 4, (g) => { g.fillStyle = R.orange[2]; g.fillRect(0, 0, g.w, g.h); g.clearRect(2, 2, g.w - 4, g.h - 4); });
  out.choice = make(152, 184, (g) => { parchment(g, 0, 0, 150, 180, 21); socket(g, 60, 14, true); g.fillStyle = '#b89860'; g.fillRect(14, 76, 122, 1); });
  out.choice_lit = make(158, 188, (g) => { g.fillStyle = R.gold[3]; g.fillRect(0, 0, 158, 188); g.fillStyle = R.gold[4]; g.fillRect(0, 0, 158, 2); g.clearRect(3, 3, 152, 182); });
  out.tooltip = make(176, 58, (g) => { panel(g, 0, 0, 176, 58, false); g.fillStyle = R.gold[1]; g.fillRect(1, 1, 174, 1); });
  out.dialog = make(320, 210, (g) => panel(g, 0, 0, 320, 210));
  return out;
}
