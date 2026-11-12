// The workshop and cutscene scenery, as unlit albedo layers. Lighting is added in Unity.
import { R, INK, hsh, Spr, Canvas } from './painter.mjs';
import { potionRound, potionTall, potionFlask, crystals, horn, eye } from './icons.mjs';

export const W = 640, H = 360;

/* ---------------- the bench's perspective ---------------- */
// (u, v) on the slab: u in -1..1 left to right, v in 0..1 back to front
export const proj = (u, v) => { const half = 98 + 34 * v; return [320 + u * half, 146 + 94 * v]; };
export const LIFT = 5; // tiles stand this many pixels proud of the slab
export function tileRect(i) { const c = i % 3, r = (i / 3) | 0; return { u0: -1 + c * (2 / 3) + 0.03, u1: -1 + (c + 1) * (2 / 3) - 0.03, v0: r / 3 + 0.035, v1: (r + 1) / 3 - 0.035 }; }
export function tileCenter(i) { const q = tileRect(i), p = proj((q.u0 + q.u1) / 2, (q.v0 + q.v1) / 2); return { x: p[0], y: p[1] - LIFT, w: (98 + 34 * (q.v0 + q.v1) / 2) * (q.u1 - q.u0) }; }
/** Candle sprite top-left corners (12x20 sprite; the wick is at x + 6, y + 1). */
export const CANDLES = [[150, 232], [498, 226], [226, 136], [208, 93], [420, 93]];
export const CAULDRON = { x: 220, y: 118, rimX: 320, rimY: 161, fireY: 290 };

function quad(g, u0, v0, u1, v1, lift, col) {
  const yA = Math.round(proj(0, v0)[1]), yB = Math.round(proj(0, v1)[1]);
  for (let y = yA; y < yB; y++) {
    const v = (y + 0.5 - 146) / 94, a = proj(u0, v)[0], b = proj(u1, v)[0];
    for (let x = Math.round(a); x < Math.round(b); x++) { const c = col(x, y, (x - a) / (b - a), (y - yA) / (yB - yA)); if (c) { g.fillStyle = c; g.fillRect(x, y - lift, 1, 1); } }
  }
}

function stoneWall(g, y0, y1, seed, dark) {
  g.fillStyle = '#17131c'; g.fillRect(0, y0, W, y1 - y0);
  const P = dark ? ['#2b2733', '#322d3b', '#383240', '#2e2937'] : ['#3b3542', '#443d4a', '#4b4350', '#40394a'];
  for (let row = 0, y = y0; y < y1; y += 16, row++) for (let x = -(row % 2) * 18 - 4; x < W; x += 36) {
    const h = hsh(x, row, seed);
    g.fillStyle = P[Math.floor(h * 4)]; g.fillRect(x, y, 35, 15);
    g.fillStyle = dark ? '#443e4c' : '#5c5464'; g.fillRect(x, y, 35, 1); g.fillRect(x, y, 1, 15);
    g.fillStyle = '#262130'; g.fillRect(x, y + 14, 35, 1); g.fillRect(x + 34, y, 1, 15);
    for (let k = 0; k < 9; k++) {
      const sx = x + 2 + Math.floor(hsh(x, row, k + 10) * 31), sy = y + 2 + Math.floor(hsh(row, x, k + 20) * 11);
      g.fillStyle = hsh(sx, sy, 3) > 0.5 ? '#585062' : '#2c2734'; g.fillRect(sx, sy, hsh(sx, sy, 4) > 0.6 ? 2 : 1, 1);
    }
    if (h > 0.8) { g.fillStyle = '#231e2b'; for (let k = 0; k < 7; k++) g.fillRect(x + 8 + k + Math.floor(hsh(k, row, 5) * 2), y + 3 + k, 1, 1); }
  }
}

// distance from the arched window's outline centre line: < 40 is glass, 40..47 the stone frame
const windowDist = (x, y) => { const dx = x + 0.5 - 320; return y < 56 ? Math.hypot(dx, y + 0.5 - 56) : Math.abs(dx); };

/** Back wall with the window frame, shelves and sconces, and the dark floor. Opaque, 640x360. */
export function benchWall() {
  const g = new Canvas(W, H);
  stoneWall(g, 0, 136, 1);
  for (let y = 8; y <= 104; y++) for (let x = 268; x <= 372; x++) {
    const d = windowDist(x, y);
    if (d > 47) continue;
    g.fillStyle = d > 45 ? '#2a2532' : d > 40 ? (x < 320 ? '#7a7284' : '#5c5464') : '#0c1030'; g.fillRect(x, y, 1, 1);
  }
  g.fillStyle = '#7a7284'; g.fillRect(262, 104, 116, 3); g.fillStyle = '#4b4350'; g.fillRect(262, 107, 116, 4); g.fillStyle = '#231e2b'; g.fillRect(264, 111, 112, 2);
  const shelf = (x, y, w) => {
    g.fillStyle = R.wood[3]; g.fillRect(x, y, w, 1); g.fillStyle = R.wood[2]; g.fillRect(x, y + 1, w, 3); g.fillStyle = R.wood[0]; g.fillRect(x, y + 4, w, 2);
    g.fillStyle = '#120e16'; g.fillRect(x + 2, y + 6, w - 4, 3);
    for (const bx of [x + 8, x + w - 12]) { g.fillStyle = R.wood[1]; g.fillRect(bx, y + 4, 4, 9); g.fillStyle = R.wood[0]; g.fillRect(bx + 3, y + 4, 1, 9); }
  };
  const books = (x, y, n, seed) => {
    for (let i = 0; i < n; i++) {
      const w = 5 + Math.floor(hsh(i, seed, 1) * 3), h = 15 + Math.floor(hsh(i, seed, 2) * 7), K = [R.red, R.blue, R.green, R.violet, R.wood, R.teal][Math.floor(hsh(i, seed, 3) * 6)];
      g.fillStyle = INK; g.fillRect(x - 1, y - h - 1, w + 2, h + 1); g.fillStyle = K[1]; g.fillRect(x, y - h, w, h); g.fillStyle = K[2]; g.fillRect(x, y - h, 1, h); g.fillStyle = K[0]; g.fillRect(x + w - 1, y - h, 1, h);
      g.fillStyle = R.gold[3]; g.fillRect(x + 1, y - h + 3, w - 2, 1); g.fillRect(x + 1, y - 5, w - 2, 1); x += w + 1;
    }
    return x;
  };
  const put = (s, x, y) => g.drawImage(s, x, y - 24);
  shelf(14, 122, 150); shelf(476, 122, 150);
  for (const bx of [208, 420]) { g.fillStyle = INK; g.fillRect(bx - 1, 111, 14, 5); g.fillStyle = R.iron[1]; g.fillRect(bx, 112, 12, 3); g.fillStyle = R.iron[2]; g.fillRect(bx, 112, 12, 1); g.fillStyle = R.iron[0]; g.fillRect(bx + 5, 115, 2, 8); }
  let x = books(22, 122, 5, 1); put(potionRound(R.violet), x + 2, 123); put(potionTall(R.gold), x + 26, 123); put(crystals(R.teal), x + 48, 123); put(potionFlask(R.pink), x + 74, 123);
  put(potionFlask(R.blue), 482, 123); put(horn(), 506, 123); x = books(536, 122, 4, 3); put(potionTall(R.teal), x + 2, 123); put(eye(), x + 24, 123);
  g.fillStyle = '#0d0a12'; g.fillRect(0, 136, W, H - 136);
  for (let y = 136; y < H; y += 2) for (let xx = 0; xx < W; xx += 2) if (hsh(xx, y, 77) > 0.9) { g.fillStyle = '#17131e'; g.fillRect(xx, y, 2, 1); }
  return g;
}

/** The night sky seen through the window: transparent outside the glass. Drawn unlit in Unity so it glows. */
export function windowPane() {
  const g = new Canvas(W, H);
  for (let y = 8; y <= 104; y++) for (let x = 268; x <= 372; x++) {
    if (windowDist(x, y) > 40) continue;
    const band = (y - 12) / 92 + (hsh(x, y, 2) - 0.5) * 0.12;
    let c = band < 0.3 ? '#0c1030' : band < 0.55 ? '#141a4a' : band < 0.8 ? '#1c2a66' : '#27408c';
    if (hsh(x, y, 9) > 0.992) c = '#e0f6ff';
    const md = Math.hypot(x + 0.5 - 336, y + 0.5 - 42);
    if (md < 12) c = md > 10.5 ? '#a8c8e8' : Math.hypot(x - 332, y - 38) < 3 || Math.hypot(x - 340, y - 47) < 2.2 ? '#bcd8ee' : '#eefaff';
    if ((x + y) % 14 === 0 || (x - y + 1400) % 14 === 0 || Math.abs(x + 0.5 - 320) < 1) c = '#120e18';
    g.fillStyle = c; g.fillRect(x, y, 1, 1);
  }
  return g;
}

/** The oak table, brass-rimmed slab, nine raised tiles and props. Transparent elsewhere, 640x360. */
export function benchTable() {
  const g = new Canvas(W, H);
  for (let y = 132; y <= 256; y++) {
    const t = (y - 132) / 124, half = 172 + 46 * t;
    for (let x = Math.round(320 - half); x < 320 + half; x++) {
      const u = (x + 0.5 - 320) / half, pl = (u + 1) * 4.5, fr = pl - Math.floor(pl), n = hsh(Math.floor(u * 90), (y / 3) | 0, Math.floor(pl));
      let c = Math.floor(pl) % 2 ? '#6a3c1e' : '#72421f';
      if (fr < 0.035) c = '#2a150c'; else if (n > 0.8) c = '#7e4c26'; else if (n < 0.14) c = '#583018';
      if (y < 134) c = '#3a2010'; if (y > 254) c = R.wood[4];
      g.fillStyle = c; g.fillRect(x, y, 1, 1);
    }
  }
  for (let y = 257; y <= 276; y++) for (let x = 102; x < 538; x++) {
    let c = y < 259 ? R.wood[3] : y > 273 ? '#1a0c08' : '#3e2012';
    if (y > 261 && y < 272) { const k = Math.abs(((x - 102) % 24) - 12) + Math.abs(y - 266.5); if (k < 4) c = '#2a140a'; else if (k < 5) c = '#5a3018'; }
    g.fillStyle = c; g.fillRect(x, y, 1, 1);
  }
  quad(g, -1.12, -0.09, 1.12, 1.09, -4, () => '#160d0a');
  quad(g, -1.12, -0.09, 1.12, 1.09, 0, (x, y, a, b) => (a < 0.012 || b < 0.03 ? R.gold[3] : a > 0.988 || b > 0.965 ? R.gold[1] : (a < 0.03 || a > 0.97 || b < 0.06 || b > 0.935) ? R.gold[2] : hsh(x, y, 5) > 0.85 ? '#2e2a3a' : '#25212f'));
  for (const [u, v] of [[-1.06, -0.04], [1.06, -0.04], [-1.06, 1.04], [1.06, 1.04]]) {
    const p = proj(u, v), px = Math.round(p[0]), py = Math.round(p[1]);
    g.fillStyle = R.gold[4]; g.fillRect(px - 2, py - 2, 4, 4); g.fillStyle = R.gold[1]; g.fillRect(px - 1, py - 1, 3, 3); g.fillStyle = R.gold[3]; g.fillRect(px - 1, py - 1, 2, 2);
  }
  for (let i = 0; i < 9; i++) {
    const q = tileRect(i), c = tileCenter(i), rx = c.w * 0.3, ry = rx * 0.42;
    quad(g, q.u0, q.v1 - 0.01, q.u1, q.v1 + 0.062, 0, (x, y, a, b) => (b < 0.2 ? '#565064' : '#2a2634'));
    quad(g, q.u0, q.v0, q.u1, q.v1, LIFT, (x, y, a, b) => {
      const e = Math.hypot((x + 0.5 - c.x) / rx, (y - LIFT + 0.5 - c.y) / ry), n = hsh(x, y, 21 + i);
      if (Math.abs(e - 1) < 0.1) return '#3c384a';
      if (Math.abs(e - 0.62) < 0.07 && ((Math.atan2(y - LIFT - c.y, x - c.x) * 6 / Math.PI + 12) | 0) % 2) return '#3c384a';
      if (b < 0.07 || a < 0.02) return '#a09aac';
      return n > 0.88 ? '#7a7488' : n < 0.1 ? '#5c566a' : '#6a6478';
    });
  }
  const mort = new Spr(26, 22); mort.tube((t) => [18 - t * 9, 2 + t * 9], () => 1.7, (t, nx) => R.wood[nx < 0 ? 4 : 2], 20);
  mort.ball(12, 12, 10, 9, R.stone, { clip: (x, y) => y > 9.5, facet: 3 }); mort.ell(12, 10, 10, 2.6, (nx, ny) => R.stone[ny < 0 ? 0 : 3]); mort.rect(8, 20, 9, 1, R.stone[1]); mort.outline();
  g.drawImage(mort, 462, 150);
  const bk = new Spr(40, 20); bk.rect(2, 12, 34, 6, (x, y) => (y < 13 ? R.red[2] : y > 16 ? R.red[0] : x < 5 ? R.gold[3] : R.red[1])); bk.rect(4, 6, 32, 6, (x, y) => (y < 7 ? R.blue[3] : y > 10 ? R.blue[0] : x > 32 ? R.bone[3] : R.blue[1]));
  bk.rect(1, 1, 30, 5, (x, y) => (y < 2 ? R.green[3] : y > 4 ? R.green[0] : x > 27 ? R.bone[3] : R.green[1])); bk.rect(8, 3, 14, 1, R.gold[3]); bk.outline();
  g.drawImage(bk, 130, 196);
  return g;
}

/** Light cookie for the moonbeam falling from the window onto the bench: white, brightness = strength. */
export function moonShaft() {
  const g = new Canvas(W, H);
  for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
    const t = (y - 20) / 250, cx = 320 - 10 * t, half = 34 + 70 * t;
    if (t <= 0 || t >= 1.05) continue;
    const f = Math.max(0, 1 - Math.abs(x - cx) / half), k = Math.round(255 * Math.pow(f, 0.7) * (1 - t * 0.45));
    if (k > 0) g.set(x, y, k, k, k, 255);
  }
  return g;
}

export function candle() {
  const s = new Spr(12, 20), X = ['#5e4436', '#8e6e58', '#b8957a', '#d8b896', '#f0dcc0'];
  s.rect(3, 4, 6, 13, (x) => X[[1, 4, 4, 3, 2, 1][x - 3]]); s.rect(3, 4, 6, 1, X[4]); s.px(8, 6, X[4]); s.px(8, 7, X[3]); s.px(8, 8, X[3]); s.px(3, 9, X[4]);
  s.px(6, 3, INK); s.px(6, 2, INK);
  s.ell(6, 17.5, 6, 2.2, (nx, ny) => R.gold[ny < -0.2 ? 3 : nx < 0 ? 2 : 1]); s.rect(2, 19, 8, 1, R.gold[1]);
  return s.outline();
}

/** One frame of a flame, its base at the bottom centre of the canvas. */
export function flame(big, t, ph) {
  const hgt = big ? 16 : 9, half = big ? 10 : 6, g = new Canvas(half * 2 + 1, hgt + 1);
  for (let i = 0; i < hgt; i++) {
    const u = i / (hgt - 1), wv = Math.sin(u * Math.PI * 0.9 + 0.25) * (big ? 5 : 2.6) * (1 - u * 0.35), sway = Math.sin(t * 6 + ph + u * 2.5) * u * (big ? 3 : 1.6);
    for (let dx = -Math.ceil(wv); dx <= Math.ceil(wv); dx++) {
      const a = Math.abs(dx) / (wv + 0.01); if (a > 1) continue;
      g.fillStyle = a < 0.35 && u < 0.6 ? '#ffffff' : a < 0.7 && u < 0.8 ? R.gold[4] : u < 0.2 ? R.blue[3] : R.orange[3];
      g.fillRect(Math.round(half + dx + sway), hgt - i, 1, 1);
    }
  }
  return g;
}

/* ---------------- the cutscene room ---------------- */
const CAUL = ['#0c0a14', '#1a1826', '#2e2c40', '#4e4c64', '#8280a0'];

/** Darker wall, a flagstone floor in perspective and the rune circle cut into it. Opaque, 640x360. */
export function cutRoom() {
  const g = new Canvas(W, H);
  stoneWall(g, 0, 232, 5, true);
  for (let y = 232; y < H; y++) {
    const t = (y - 232) / 128, row = Math.floor(Math.log(1 + t * 3) * 4);
    for (let x = 0; x < W; x++) {
      const u = (x - 320) / (1 + t * 1.6), k = u / 44 + 100 + (row % 2) * 0.5, col = Math.floor(k), fr = k % 1, n = hsh(x, y, 3);
      g.fillStyle = fr < 0.03 || y === 232 ? '#141018' : n > 0.9 ? '#3e3848' : (col + row) % 2 ? '#2e2a38' : '#332e3e'; g.fillRect(x, y, 1, 1);
    }
  }
  for (let a = 0; a < 6.283; a += 0.004) for (const [k, c] of [[1, '#4a4458'], [0.985, '#1a1620'], [0.8, '#1a1620'], [0.79, '#4a4458']]) { g.fillStyle = c; g.fillRect(Math.round(320 + Math.cos(a) * 200 * k), Math.round(292 + Math.sin(a) * 46 * k), 1, 1); }
  return g;
}
export function logs() {
  const lg = new Spr(120, 22);
  lg.tube((t) => [8 + t * 104, 14 - t * 5], () => 5, (t, nx, ny) => R.wood[ny < -0.4 ? 3 : ny < 0.3 ? 2 : 0], 60);
  lg.tube((t) => [112 - t * 100, 15 - t * 6], () => 4.5, (t, nx, ny) => R.wood[ny < -0.4 ? 2 : ny < 0.3 ? 1 : 0], 60);
  return lg.outline();
}
export function cauldron() {
  const s = new Spr(200, 170);
  for (const lx of [44, 156]) s.tube((t) => [lx + (lx < 100 ? -10 : 10) * t, 130 + t * 34], (t) => 6 - t * 2, (t, nx) => CAUL[nx < -0.3 ? 3 : nx < 0.4 ? 2 : 1], 20);
  s.ball(100, 92, 84, 66, CAUL, { clip: (x, y) => y > 42 });
  s.ell(100, 44, 78, 17, (nx, ny) => (ny < 0 ? CAUL[1] : CAUL[4]));
  s.ell(100, 43, 70, 13, () => CAUL[0]);
  for (let a = 0.15; a < 3; a += 0.02) { const x = 100 + Math.cos(a) * 83, y = 98 + Math.sin(a) * 12; s.px(x, y, R.gold[a > 2 ? 3 : a > 1 ? 2 : 1]); s.px(x, y + 1, R.gold[a > 2 ? 2 : 1]); s.px(x, y + 2, R.gold[0]); }
  for (let a = 0.3; a < 2.9; a += 0.33) { const x = Math.round(100 + Math.cos(a) * 82), y = Math.round(99 + Math.sin(a) * 12); s.rect(x - 1, y - 1, 3, 3, R.gold[4]); s.px(x + 1, y + 1, R.gold[1]); }
  return s.outline();
}

/* ---------------- white shapes for glow and effects (tinted in Unity) ---------------- */
const white = (g, x, y, a) => g.set(Math.round(x), Math.round(y), 255, 255, 255, Math.round(Math.max(0, Math.min(1, a)) * 255));

/** Soft round glow: alpha falls off as (1 - d)^2. */
export function glow(size = 64) {
  const g = new Canvas(size, size), r = size / 2;
  for (let y = 0; y < size; y++) for (let x = 0; x < size; x++) { const d = Math.hypot(x + 0.5 - r, y + 0.5 - r) / r; if (d < 1) white(g, x, y, (1 - d) * (1 - d)); }
  return g;
}
export function sparkleDot() { const g = new Canvas(5, 5); for (const [x, y, a] of [[2, 2, 1], [1, 2, 0.8], [3, 2, 0.8], [2, 1, 0.8], [2, 3, 0.8], [0, 2, 0.35], [4, 2, 0.35], [2, 0, 0.35], [2, 4, 0.35]]) white(g, x, y, a); return g; }
/** An ellipse outline, one pixel thick. */
export function ring(rx, ry) {
  const g = new Canvas(Math.ceil(rx) * 2 + 3, Math.ceil(ry) * 2 + 3), cx = g.w / 2, cy = g.h / 2;
  for (let a = 0; a < 6.283; a += 0.5 / Math.max(rx, ry)) white(g, Math.floor(cx + Math.cos(a) * rx), Math.floor(cy + Math.sin(a) * ry), 1);
  return g;
}
/** The filled top face of tile i, for hover and reaction highlights. Full-frame coordinates. */
export function tileFace(i) {
  const g = new Canvas(W, H), q = tileRect(i);
  quad(g, q.u0, q.v0, q.u1, q.v1, LIFT, (x, y, a, b) => (a < 0.04 || a > 0.96 || b < 0.07 || b > 0.93 ? '#ffffff' : 'rgba(255,255,255,0.28)'));
  return g;
}
/** The glowing lines of the cutscene's floor circle: two ellipses and twelve nodes. */
export function runeCircle() {
  const g = new Canvas(404, 98), cx = 202, cy = 49;
  for (let a = 0; a < 6.283; a += 0.003) { white(g, cx + Math.cos(a) * 198, cy + Math.sin(a) * 45.5, 1); white(g, cx + Math.cos(a) * 159, cy + Math.sin(a) * 36.5, 0.8); }
  for (let k = 0; k < 12; k++) { const a = k * Math.PI / 6, x = Math.round(cx + Math.cos(a) * 179), y = Math.round(cy + Math.sin(a) * 41); for (const [dx, dy] of [[0, 0], [1, 0], [-1, 0], [0, 1], [0, -1], [2, 0], [-2, 0]]) white(g, x + dx, y + dy, 1); }
  return g;
}
/** The surface of the brew, with a ripple pattern. */
export function liquid(phase) {
  const g = new Canvas(139, 25);
  for (let y = -12; y <= 12; y++) for (let x = -69; x <= 69; x++) {
    if ((x / 69) ** 2 + (y / 12) ** 2 > 1) continue;
    const w = Math.sin(x * 0.18 + phase * 3 + y * 0.5) * Math.sin(x * 0.07 - phase * 1.7);
    white(g, x + 69, y + 12, 0.55 + 0.45 * w);
  }
  return g;
}
/** A column of light, brightest along its axis and at the base. */
export function pillar() {
  const g = new Canvas(89, 161);
  for (let y = 0; y < 161; y++) for (let x = -44; x <= 44; x++) white(g, x + 44, y, (1 - Math.abs(x) / 44) ** 2 * (0.35 + 0.65 * (y / 161)));
  return g;
}
/** A tapering ray pointing right from its left edge. */
export function ray(len = 150) {
  const g = new Canvas(len, 13);
  for (let d = 0; d < len; d++) { const wdt = 1 + d * 0.035; for (let q = -6; q <= 6; q++) if (Math.abs(q) <= wdt) white(g, d, q + 6, 0.9 * (1 - d / len)); }
  return g;
}
