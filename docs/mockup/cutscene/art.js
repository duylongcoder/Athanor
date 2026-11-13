// Athanor opening cutscene mockup: images, colour helpers, crisp text, and the PLACEHOLDER art for the six new assets
// (cellar_stair, keeper_walk, keeper_kneel, keeper_last, cellar_bottom, roots_grow), drawn procedurally once at load.
// Plain script (no modules) so the page works when opened straight from disk (file://).
'use strict';
const W = 640, H = 360;
const BAY = [0, 8, 2, 10, 12, 4, 14, 6, 3, 11, 1, 9, 15, 7, 13, 5].map((v) => (v + 0.5) / 16);
const rgbOf = (h) => [parseInt(h.slice(1, 3), 16), parseInt(h.slice(3, 5), 16), parseInt(h.slice(5, 7), 16)];
const hexOf = (c) => '#' + c.map((v) => Math.max(0, Math.min(255, Math.round(v))).toString(16).padStart(2, '0')).join('');
const mixc = (a, b, t) => { const A = rgbOf(a), B = rgbOf(b); return hexOf(A.map((v, i) => v + (B[i] - v) * t)); };
const hsh = (x, y, s = 0) => { let n = (x * 374761393 + y * 668265263 + s * 2147483647) | 0; n = (n ^ (n >>> 13)) * 1274126177 | 0; return ((n ^ (n >>> 16)) >>> 0) / 4294967296; };
const clamp01 = (v) => Math.max(0, Math.min(1, v));
const lerp = (a, b, t) => a + (b - a) * t;
const ease = { inCubic: (t) => t * t * t, outCubic: (t) => 1 - (1 - t) ** 3, inOut: (t) => (t < 0.5 ? 4 * t * t * t : 1 - (-2 * t + 2) ** 3 / 2), outBack: (t) => 1 + 2.70158 * (t - 1) ** 3 + 1.70158 * (t - 1) ** 2 };
function rng(seed) { let s = seed >>> 0 || 1; return () => { s ^= s << 13; s ^= s >>> 17; s ^= s << 5; return ((s >>> 0) % 100000) / 100000; }; }
function mk(w, h) { const c = document.createElement('canvas'); c.width = w; c.height = h; c.getContext('2d').imageSmoothingEnabled = false; return c; }

// the game's colours (SimpleCutscene, SceneBuilder, GameFlow, docs/mockup/art.js)
const COL = {
  dark: '#0c0706', brass: '#8c5a14', cream: '#f0dcc0', gold: '#f8d250', prompt: '#7a5a3a', dim: '#a88a6a',
  ambient: [0.52 * 0.25, 0.52 * 0.25, 1 * 0.25], moon: [0.42, 0.6, 1], candle: [1, 0.63, 0.33], fire: [1, 0.57, 0.27],
  stone: ['#2b2733', '#322d3b', '#383240', '#2e2937'], rock: ['#211b25', '#271f2b', '#1c1720', '#2d2533'],
  root: ['#140a08', '#3a1f12', '#5a3018', '#7a4420'], silhouette: '#08060b',
};

/* ---------------- images ---------------- */
const IMG = {};
const IMG_LIST = ['workshop_wall', 'window', 'workshop_table', 'candle', 'cutscene_room', 'logs', 'cauldron', 'journal', 'glow', 'rune_circle', 'floor_ring',
  ...[0, 1, 2, 3, 4, 5, 6, 7].flatMap((k) => ['flame_small_' + k, 'flame_big_' + k])];
function loadImages() {
  const all = IMG_LIST.map((n) => new Promise((res, rej) => { const i = new Image(); i.onload = () => { IMG[n] = i; res(); }; i.onerror = () => rej(new Error('missing img/' + n + '.png')); i.src = 'img/' + n + '.png'; }));
  for (const k of Object.keys(FX_SHEETS)) for (const v of ['', '-athanor']) {
    if (v && !FX_SHEETS[k].athanor) continue;
    all.push(new Promise((res, rej) => { const i = new Image(); i.onload = () => { IMG['fx:' + k + v] = i; res(); }; i.onerror = () => rej(new Error('missing fx/' + k + v + '.png')); i.src = 'fx/' + k + v + '.png'; }));
  }
  return Promise.all(all);
}
// a copy of an image in one flat colour (for additive glows); cached
const tintCache = new Map();
function tinted(img, col) {
  const key = (img.src || img.id || '') + col; let c = tintCache.get(key); if (c) return c;
  c = mk(img.width, img.height); const g = c.getContext('2d');
  g.drawImage(img, 0, 0); g.globalCompositeOperation = 'source-in'; g.fillStyle = col; g.fillRect(0, 0, c.width, c.height);
  tintCache.set(key, c); return c;
}
// additive soft glow (the game's Make.Glow: glow.png scaled to a radius)
function glowAt(g, x, y, r, col, a) {
  if (a <= 0.004) return;
  g.save(); g.globalCompositeOperation = 'lighter'; g.globalAlpha = Math.min(1, a);
  g.drawImage(tinted(IMG.glow, col), Math.round(x - r), Math.round(y - r), r * 2, r * 2); g.restore();
}

/* ---------------- crisp pixel text (as docs/mockup/scene.js) ---------------- */
const FONT = { body: '16px BitPotion', tiny: '8px PixOp8', title: '16px Alagard' };
const tcache = new Map();
function crisp(str, font, col) {
  const key = str + '|' + font + '|' + col; let t = tcache.get(key); if (t) return t;
  t = document.createElement('canvas'); const g = t.getContext('2d'); g.font = font;
  const w = Math.max(1, Math.ceil(g.measureText(str).width) + 2), h = parseInt(font.match(/(\d+)px/)[1], 10) + 8;
  t.width = w; t.height = h; g.font = font; g.textBaseline = 'top'; g.fillStyle = '#fff'; g.fillText(str, 1, 1);
  const im = g.getImageData(0, 0, w, h), c = rgbOf(col);
  for (let i = 0; i < im.data.length; i += 4) { const on = im.data[i + 3] > 110; im.data[i] = c[0]; im.data[i + 1] = c[1]; im.data[i + 2] = c[2]; im.data[i + 3] = on ? 255 : 0; }
  g.putImageData(im, 0, 0); if (tcache.size > 400) tcache.clear(); tcache.set(key, t); return t;
}
// centred text; shadow one pixel right and down, as Make.Text does
function textC(g, str, x, y, font, col, shadow, alpha = 1) {
  if (!str || alpha <= 0) return;
  const t = crisp(str, font, col), X = Math.round(x - t.width / 2);
  g.save(); g.globalAlpha = alpha;
  if (shadow) g.drawImage(crisp(str, font, shadow), X + 1, y + 1);
  g.drawImage(t, X, y); g.restore();
}

/* ---------------- pixel buffer ---------------- */
class Buf {
  constructor(w, h) { this.w = w; this.h = h; this.c = mk(w, h); this.g = this.c.getContext('2d'); this.im = this.g.createImageData(w, h); this.d = this.im.data; }
  px(x, y, col, a = 255) { x |= 0; y |= 0; if (!col || x < 0 || y < 0 || x >= this.w || y >= this.h) return; const c = typeof col === 'string' ? rgbOf(col) : col, i = (y * this.w + x) * 4; this.d[i] = c[0]; this.d[i + 1] = c[1]; this.d[i + 2] = c[2]; this.d[i + 3] = a; }
  a(x, y) { return x < 0 || y < 0 || x >= this.w || y >= this.h ? 0 : this.d[(y * this.w + x) * 4 + 3]; }
  rgb(x, y) { const i = (y * this.w + x) * 4; return [this.d[i], this.d[i + 1], this.d[i + 2]]; }
  scale(x, y, k) { if (x < 0 || y < 0 || x >= this.w || y >= this.h) return; const i = (y * this.w + x) * 4; this.d[i] *= k; this.d[i + 1] *= k; this.d[i + 2] *= k; }
  disc(cx, cy, r, col) { for (let y = Math.floor(cy - r); y <= cy + r; y++) for (let x = Math.floor(cx - r); x <= cx + r; x++) if ((x + 0.5 - cx) ** 2 + (y + 0.5 - cy) ** 2 <= r * r) this.px(x, y, col); }
  rect(x, y, w, h, col) { for (let yy = y; yy < y + h; yy++) for (let xx = x; xx < x + w; xx++) this.px(xx, yy, typeof col === 'function' ? col(xx, yy) : col); }
  poly(pts, col) {
    const n = pts.length; let x0 = 1e9, x1 = -1e9, y0 = 1e9, y1 = -1e9;
    for (const [x, y] of pts) { x0 = Math.min(x0, x); x1 = Math.max(x1, x); y0 = Math.min(y0, y); y1 = Math.max(y1, y); }
    for (let y = Math.floor(y0); y <= y1; y++) for (let x = Math.floor(x0); x <= x1; x++) {
      const px = x + 0.5, py = y + 0.5; let inside = false;
      for (let i = 0, j = n - 1; i < n; j = i++) { const [xi, yi] = pts[i], [xj, yj] = pts[j]; if (yi > py !== yj > py && px < ((xj - xi) * (py - yi)) / (yj - yi) + xi) inside = !inside; }
      if (inside) this.px(x, y, typeof col === 'function' ? col(x, y) : col);
    }
  }
  line(x0, y0, x1, y1, w, col) { const n = Math.ceil(Math.max(Math.abs(x1 - x0), Math.abs(y1 - y0)) * 2) + 1; for (let i = 0; i <= n; i++) this.disc(x0 + (x1 - x0) * i / n, y0 + (y1 - y0) * i / n, w / 2, col); }
  done() { this.g.putImageData(this.im, 0, 0); return this.c; }
}

// a root: a wandering tube, outline, body, top-left highlight
function drawRoot(b, pts, r0, r1) {
  const pass = (rf, col, off) => {
    for (let i = 0; i < pts.length - 1; i++) {
      const [ax, ay] = pts[i], [bx, by] = pts[i + 1], n = Math.ceil(Math.hypot(bx - ax, by - ay) * 2) + 1;
      for (let k = 0; k <= n; k++) { const u = (i + k / n) / (pts.length - 1), r = lerp(r0, r1, u) * rf; if (r < 0.35) continue; b.disc(ax + (bx - ax) * k / n + off * r, ay + (by - ay) * k / n + off * r, r, col); }
    }
  };
  pass(1.0, COL.root[0], 0); pass(0.72, COL.root[1], 0); pass(0.38, COL.root[2], -0.32); pass(0.16, COL.root[3], -0.5);
}
function wander(R, x, y, dx, dy, n, step, jitter) {
  const pts = [[x, y]]; let a = Math.atan2(dy, dx);
  for (let i = 0; i < n; i++) { a += (R() - 0.5) * jitter; x += Math.cos(a) * step; y += Math.sin(a) * step; pts.push([x, y]); a = lerp(a, Math.atan2(dy, dx), 0.18); }
  return pts;
}

/* ---------------- shared stone textures ---------------- */
function voronoi(x, y, cell, seed, sy = 1) {
  const gx = Math.floor(x / cell), gy = Math.floor(y * sy / cell); let d1 = 1e9, d2 = 1e9, id = 0;
  for (let j = -1; j <= 1; j++) for (let i = -1; i <= 1; i++) {
    const cx = (gx + i + hsh(gx + i, gy + j, seed)) * cell, cy = (gy + j + hsh(gx + i, gy + j, seed + 1)) * cell, d = Math.hypot(x - cx, y * sy - cy);
    if (d < d1) { d2 = d1; d1 = d; id = hsh(gx + i, gy + j, seed + 2); } else if (d < d2) d2 = d;
  }
  return { id, edge: d2 - d1 };
}
function worked(x, y) {   // squared stone blocks, 26x12
  const row = Math.floor(y / 12), bx = Math.floor((x + (row % 2) * 13) / 26), lx = (x + (row % 2) * 13) % 26, ly = y % 12, h = hsh(bx, row, 3);
  let c = rgbOf(COL.stone[Math.floor(h * 4)]);
  if (ly === 0 || lx === 0) c = rgbOf('#443e4c'); else if (ly === 11 || lx === 25) c = rgbOf('#1a1620');
  else if (hsh(x, y, 9) > 0.93) c = rgbOf('#4a4352'); else if (hsh(x, y, 10) < 0.05) c = rgbOf('#221d29');
  return { c, block: h };
}
function rough(x, y, seed, darkK = 1) {   // old rock: irregular cells with dark cracks
  const v = voronoi(x, y, 22, seed, 1.25), k = v.edge < 1.4 ? 0 : 1;
  let c = rgbOf(COL.rock[Math.floor(v.id * 4)]);
  if (!k) c = rgbOf('#100c13'); else if (v.edge < 3 && hsh(x, y, 5) > 0.5) c = rgbOf('#3a3242');
  else if (hsh(x, y, 6) > 0.95) c = rgbOf('#352d3c');
  return c.map((u) => u * darkK);
}

/* ---------------- cellar_stair (640x720, placeholder) ---------------- */
// Flight A (top half): landing at y 186 from x 0 to 112, then steps 21 wide and 8 high down to the right, into a tunnel.
// Flight C (bottom half): out of a tunnel at the left, landing at y 480 to x 70, then down to (570, 672) and into the dark.
const STAIR = {
  A: { x0: 112, y0: 186, x1: 600 }, C: { x0: 70, y0: 480, x1: 600 }, sw: 21, sh: 8,
  tread(f, x) { const F = STAIR[f]; return x < F.x0 ? F.y0 : F.y0 + STAIR.sh * (Math.floor((x - F.x0) / STAIR.sw) + 1); },
  // the body's line down a flight (feet glide; the walk bob does the rest)
  feet(f, x) { const F = STAIR[f]; return x < F.x0 ? F.y0 : F.y0 + (x - F.x0) * STAIR.sh / STAIR.sw + 4; },
};
function makeStair() {
  const w = 640, h = 720, b = new Buf(w, h);
  for (let y = 0; y < h; y++) for (let x = 0; x < w; x++) {
    // worked stone above, giving way to old rock below
    const wk = worked(x, y), mixY = clamp01((y - 300) / 150);
    let c = wk.block > mixY * 1.15 ? wk.c : rough(x, y, 7, 0.95);
    b.px(x, y, c);
  }
  const R = rng(11);
  // roots in the lower half (thicker and more of them the deeper)
  for (let i = 0; i < 16; i++) {
    const fromLeft = R() < 0.5, y0 = 380 + R() * 320, x0 = fromLeft ? -4 : w + 4;
    drawRoot(b, wander(R, x0, y0, fromLeft ? 1 : -1, 0.25 + R() * 0.5, 14 + Math.floor(R() * 18), 9, 0.9), 2.2 + R() * 2.4, 0.4);
  }
  for (let i = 0; i < 10; i++) { const x0 = R() * w; drawRoot(b, wander(R, x0, 352 + R() * 40, (R() - 0.5) * 0.6, 1, 8 + Math.floor(R() * 10), 8, 0.7), 1.6 + R() * 1.6, 0.3); }
  // two old niches in the worked stone
  for (const [nx, ny] of [[470, 120], [210, 300]]) {
    b.poly([[nx, ny + 30], [nx, ny + 8], [nx + 4, ny + 2], [nx + 12, ny], [nx + 20, ny + 2], [nx + 24, ny + 8], [nx + 24, ny + 30]], '#0e0b12');
    b.rect(nx - 2, ny + 30, 28, 2, '#4a4352');
  }
  // the flights: tread top light, riser, a stone band beneath
  const flight = (f, landX0) => {
    const F = STAIR[f];
    for (let x = landX0; x < w; x++) {
      const ty = STAIR.tread(f, x), local = x < F.x0 ? -1 : (x - F.x0) % STAIR.sw;
      for (let y = ty; y < ty + 34; y++) {
        const d = y - ty;
        let c = d === 0 ? '#7a7282' : d === 1 ? '#5c5464' : d < 8 ? (local === 0 ? '#2a2532' : '#3e3848') : d < 30 ? '#262130' : '#141118';
        if (d >= 2 && d < 30 && hsh(x, y, 21) > 0.92) c = '#4a4352';
        if (local === 0 && d < 8) c = '#1a1620';
        b.px(x, y, c);
      }
      // underside: darkness under the stair band
      for (let y = ty + 34; y < ty + 60; y++) b.scale(x, y, 0.45 + 0.55 * (y - ty - 34) / 26);
    }
  };
  flight('A', 0);
  // old iron rail along flight A
  for (let x = 30; x < 590; x += 42) {
    const ty = STAIR.tread('A', x);
    b.rect(x, ty - 20, 2, 20, (xx) => (xx === x ? '#6a788c' : '#3a4256'));
  }
  for (let x = 30; x < 592; x++) { const ty = STAIR.feet('A', Math.max(x, 112)) - 4 - 20; b.px(x, Math.round(x < 112 ? 166 : ty), '#6a788c'); b.px(x, Math.round(x < 112 ? 167 : ty + 1), '#2a3040'); }
  // tunnel mouths: right end of flight A, left start of flight C, the dark at the bottom right
  const mouth = (cx, cy, rx, ry) => { for (let y = Math.floor(cy - ry); y < cy + ry; y++) for (let x = Math.floor(cx - rx); x < cx + rx; x++) { const d = ((x - cx) / rx) ** 2 + ((y - cy) / ry) ** 2; if (d < 1) b.scale(x, y, Math.min(1, d * d * 0.9)); } };
  mouth(640, 340, 90, 70);
  flight('C', 0);
  mouth(10, 470, 70, 62);
  mouth(640, 700, 170, 110);
  // deeper is darker; the text band at the top of each half stays dark
  for (let y = 0; y < h; y++) {
    const half = y % 360, band = half < 92 ? 0.38 + 0.62 * ease.inOut(clamp01((half - 40) / 52)) : 1, deep = 1 - 0.3 * (y / h);
    for (let x = 0; x < w; x++) b.scale(x, y, band * deep * (0.82 + 0.18 * (1 - Math.abs(x - 320) / 320)));
  }
  return b.done();
}

/* ---------------- cellar_bottom (640x360 back + 640x150 floor, placeholder) ---------------- */
function makeBottom() {
  const b = new Buf(W, H);
  for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) b.px(x, y, rough(x, y, 31, 1.15));
  const R = rng(5);
  for (let i = 0; i < 22; i++) {   // roots hanging from the top edge
    const x0 = 20 + R() * 600, len = 6 + Math.floor(R() * 16);
    drawRoot(b, wander(R, x0, -6, (R() - 0.5) * 0.5, 1, len, 7, 0.8), 1.4 + R() * 2.6, 0.3);
  }
  for (const side of [-1, 1]) for (let i = 0; i < 4; i++) {   // thick roots down the side walls
    const x0 = side < 0 ? -6 : W + 6;
    drawRoot(b, wander(R, x0, 20 + R() * 120, -side * 0.5, 1, 22, 9, 0.6), 4 + R() * 3, 1.2);
  }
  for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
    const side = (Math.abs(x - 320) / 320) ** 1.6, top = y < 92 ? 0.45 + 0.55 * clamp01((y - 40) / 52) : 1;
    b.scale(x, y, (1 - 0.8 * side) * top * (0.75 + 0.25 * clamp01(y / 250)));
  }
  const back = b.done();
  const f = new Buf(W, 150);
  for (let y = 0; y < 150; y++) for (let x = 0; x < W; x++) {
    const v = voronoi(x, y, 26, 77, 2.6), edgeTop = 2 + Math.round(hsh(x >> 3, 0, 4) * 3);
    if (y < edgeTop - 2) continue;
    let c = rgbOf(['#3a3444', '#433c4d', '#363040', '#4a4354'][Math.floor(v.id * 4)]);
    if (v.edge < 1.1) c = rgbOf('#1a1620'); else if (y <= edgeTop) c = rgbOf('#6a6272'); else if (hsh(x, y, 3) > 0.94) c = rgbOf('#55505e');
    const side = (Math.abs(x - 320) / 320) ** 1.5, depth = 1 - 0.35 * clamp01(y / 90);
    f.px(x, y, c.map((u) => u * (1 - 0.85 * side) * depth));
  }
  return { back, floor: f.done() };
}

/* ---------------- roots_grow (160x48, 6 frames; drawn as growth 0..1, placeholder) ---------------- */
const ROOTS = (() => {
  const R = rng(3), list = [];
  for (let i = 0; i < 7; i++) {
    const dir = i % 2 ? 1 : -1, pts = wander(R, 80 + dir * 2, 22 + (R() - 0.5) * 6, dir, (R() - 0.5) * 0.5, 10 + Math.floor(R() * 4), 6.5, 0.9);
    list.push({ pts, t0: R() * 0.15, w: 1.6 + R() * 0.8 });
    for (let k = 3; k < pts.length - 2; k += 3) list.push({ pts: wander(R, pts[k][0], pts[k][1], dir, R() < 0.5 ? -0.8 : 0.8, 4, 5, 1.2), t0: 0.2 + k / pts.length * 0.6, w: 0.9 });
  }
  return list;
})();
function drawRootsGrow(g, ox, oy, growth, tip) {
  const frame = Math.floor(clamp01(growth) * 5.999) / 5;       // six frames, as the sprite will have
  for (const r of ROOTS) {
    const u = clamp01((frame - r.t0) / (1 - r.t0)); if (u <= 0) continue;
    const n = Math.max(1, Math.floor(u * (r.pts.length - 1)));
    for (let i = 0; i < n; i++) {
      const [ax, ay] = r.pts[i], [bx, by] = r.pts[i + 1], steps = 8;
      for (let k = 0; k <= steps; k++) {
        const x = Math.round(ox + ax + (bx - ax) * k / steps), y = Math.round(oy + ay + (by - ay) * k / steps), wd = r.w * (1 - i / r.pts.length * 0.6);
        g.fillStyle = COL.root[0]; g.fillRect(x - 1, y - 1, Math.ceil(wd) + 2, Math.ceil(wd) + 1);
        g.fillStyle = COL.root[2]; g.fillRect(x, y - 1, Math.ceil(wd), 1);
        g.fillStyle = COL.root[1]; g.fillRect(x, y, Math.ceil(wd), Math.max(1, Math.ceil(wd) - 1));
      }
    }
    if (tip && u < 1) { const p = r.pts[n]; g.fillStyle = tip; g.fillRect(Math.round(ox + p[0]), Math.round(oy + p[1]), 1, 1); }
  }
}

/* ---------------- Keepers (32x56 silhouettes, placeholder) ---------------- */
// kind: hood | hat | bare. pose: walk0..3 | stand | kneel0..2. faces: show a face. carry: book | lantern
function keeperSprite(kind, pose, o = {}) {
  const b = new Buf(32, 56), S = COL.silhouette, mask = new Buf(32, 56);
  const put = (fn) => fn(mask);
  const walk = pose.startsWith('walk') ? +pose.slice(4) : -1, kneel = pose.startsWith('kneel') ? +pose.slice(5) : -1;
  const bob = walk === 1 || walk === 3 ? 1 : 0;
  let headX = 17, headY = 8 + bob, handX = 23, handY = 31 + bob;
  if (kneel >= 0) {
    const bend = kneel > 0;
    headX = bend ? 23 : 19; headY = bend ? 27 : 21;
    put((m) => {
      m.poly(bend ? [[12, 34], [21, 29], [27, 39], [24, 46], [12, 46]] : [[13, 26], [22, 26], [24, 45], [11, 45]], S);   // torso (cloak)
      m.line(17, 44, 25, 45, 4, S); m.line(25, 45, 25, 54, 3, S); m.rect(25, 53, 4, 3, S);                               // front leg
      m.line(14, 44, 12, 54, 4, S); m.line(12, 54, 3, 55, 3, S);                                                         // knee down
      m.rect(6, bend ? 33 : 29, 6, 8, S);                                                                                // satchel
      handX = bend ? 28 : 25; handY = bend ? 52 : 36;
      m.line(bend ? 22 : 20, bend ? 31 : 28, handX, handY, 3, S); m.rect(handX - 1, handY - 1, 4, 3, S);                 // arms, cupped hands
    });
  } else {
    const s = walk >= 0 ? [1, 0, -1, 0][walk] : 0, front = 18 + 3 * s, back = 15 - 3 * s;
    put((m) => {
      m.poly([[12, 13 + bob], [22, 13 + bob], [24, 44 + bob], [9, 44 + bob]], S);   // coat
      m.line(16, 42 + bob, back, 53, 3, S); m.rect(back - 1, 53, 4, 3, S);
      m.line(18, 42 + bob, front, 53, 3, S); m.rect(front, 53, 4, 3, S);
      m.rect(6, 27 + bob, 6, 8, S); m.line(20, 14 + bob, 10, 27 + bob, 1, S);      // satchel and strap
      m.line(20, 16 + bob, handX, handY, 3, S);
    });
  }
  // heads
  put((m) => {
    if (kind === 'hood') m.poly([[headX - 5, headY + 7], [headX - 5, headY - 1], [headX - 2, headY - 6], [headX + 2, headY - 7], [headX + 5, headY - 4], [headX + 5, headY + 1], [headX + 4, headY + 7]], S);
    else if (kind === 'hat') { m.disc(headX, headY + 3, 4, S); m.rect(headX - 8, headY, 16, 2, S); m.poly([[headX - 4, headY], [headX - 3, headY - 5], [headX + 3, headY - 6], [headX + 4, headY]], S); }
    else { m.disc(headX, headY, 4.2, S); m.rect(headX - 2, headY + 3, 5, 3, S); }
  });
  // copy mask into the sprite, warm rim on the front (right) side
  for (let y = 0; y < 56; y++) for (let x = 0; x < 32; x++) {
    if (!mask.a(x, y)) continue;
    const edge = !mask.a(x + 1, y), near = Math.hypot(x - handX, y - handY);
    b.px(x, y, edge ? (near < 9 ? '#c4652e' : '#3a1c14') : S);
  }
  if (o.faces && kneel < 0) {
    const fx = kind === 'hood' ? headX + 2 : headX + 1, fy = kind === 'hat' ? headY + 1 : headY - 1;
    b.rect(fx, fy, 3, 4, kind === 'hood' ? '#6a4a3a' : '#a07858'); b.px(fx + 2, fy + 1, '#1a0e0a'); b.px(fx + 2, fy + 3, kind === 'hood' ? '#4a3026' : '#7a5440');
  } else if (o.faces && kneel >= 0) { b.rect(headX + 1, headY - 1, 3, 3, '#6a4a3a'); b.px(headX + 3, headY, '#1a0e0a'); }
  if (o.carry === 'book') { b.rect(handX - 1, handY - 1, 3, 3, '#3a1a08'); b.px(handX, handY - 1, '#f8d250'); b.px(handX + 1, handY - 1, '#fff6b8'); }
  if (o.carry === 'lantern') { b.px(handX, handY + 1, '#3a4256'); b.rect(handX - 1, handY + 2, 3, 5, '#2a1a10'); b.rect(handX, handY + 3, 1, 3, '#fff6b8'); }
  return { c: b.done(), hand: [handX, handY] };
}
function mirror(c) { const m = mk(c.width, c.height), g = m.getContext('2d'); g.translate(c.width, 0); g.scale(-1, 1); g.drawImage(c, 0, 0); return m; }
// dithered alpha (Bayer), cached per canvas and level; only for canvases made here (never tainted)
const ditherCache = new WeakMap();
function dithered(c, a) {
  const lv = Math.round(clamp01(a) * 16); if (lv >= 16) return c; if (lv <= 0) return null;
  let m = ditherCache.get(c); if (!m) { m = []; ditherCache.set(c, m); } if (m[lv]) return m[lv];
  const o = mk(c.width, c.height), g = o.getContext('2d'); g.drawImage(c, 0, 0);
  const im = g.getImageData(0, 0, c.width, c.height);
  for (let y = 0; y < c.height; y++) for (let x = 0; x < c.width; x++) if (lv / 16 <= BAY[((y & 3) << 2) | (x & 3)]) im.data[(y * c.width + x) * 4 + 3] = 0;
  g.putImageData(im, 0, 0); m[lv] = o; return o;
}

/* ---------------- open blank book (option for scene 6, ~200x130, placeholder) ---------------- */
function makeBook() {
  const b = new Buf(204, 134);
  b.rect(0, 4, 204, 130, '#2a120c'); b.rect(1, 5, 202, 128, '#4a2414'); b.rect(1, 5, 202, 1, '#6a3420');
  for (const [x0, x1] of [[8, 100], [104, 196]]) for (let y = 8; y < 126; y++) for (let x = x0; x < x1; x++) {
    const n = hsh(x, y, 12), edge = Math.min(x - x0, x1 - 1 - x, y - 8, 125 - y), gut = x0 === 8 ? x1 - x : x - x0;
    let c = edge < 1 ? '#a8854e' : n > 0.92 ? '#e8d4a0' : n < 0.07 ? '#c4a870' : '#d8c088';
    b.px(x, y, c); if (gut < 8) b.scale(x, y, 0.72 + gut * 0.035);
  }
  b.rect(100, 6, 4, 124, '#1a0a06'); b.rect(101, 6, 2, 124, '#3a1a0c');
  b.rect(96, 128, 3, 6, '#7a1626'); b.rect(96, 133, 1, 1, '#3b0d1e');   // ribbon
  return b.done();
}

const ART = {};
function buildArt() {
  ART.stair = makeStair();
  Object.assign(ART, makeBottom());
  ART.book = makeBook();
  ART.keepers = {};
  ART.keeper = (kind, pose, o) => { const k = kind + pose + JSON.stringify(o); return ART.keepers[k] || (ART.keepers[k] = keeperSprite(kind, pose, o)); };
}
