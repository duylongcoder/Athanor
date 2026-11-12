// Pixel-art painter and the material icon set. Plain script (no modules) so it runs from file:// and in node.
const R = {
  red: ['#3b0d1e', '#7a1626', '#c22b2b', '#ee6a3c', '#ffc27a'],
  orange: ['#4a1712', '#9a3414', '#e06a1c', '#ffa63a', '#ffe48a'],
  gold: ['#4a2a0e', '#8c5a14', '#d49a22', '#f8d250', '#fff6b8'],
  green: ['#0f2f24', '#1d5a2c', '#3f9a38', '#8ad04a', '#dcf58a'],
  teal: ['#0b2b3a', '#11606a', '#1fa8a0', '#6ee6c8', '#d8fff0'],
  blue: ['#141a4a', '#27408c', '#3f7ad8', '#7cc0f8', '#e0f6ff'],
  violet: ['#22103e', '#4a1f7a', '#8a3fc8', '#c486f4', '#f4dcff'],
  pink: ['#3e1030', '#8a1f5a', '#d8488a', '#f890b8', '#ffe0ea'],
  stone: ['#1c1a26', '#3a3646', '#625c6c', '#948c96', '#cfc8c4'],
  iron: ['#1a1c2a', '#3a4256', '#6a788c', '#a8b8c4', '#eef6f8'],
  bone: ['#3a2a2a', '#7a6050', '#b89c7c', '#e4d2aa', '#fffbe4'],
  wood: ['#24120e', '#4a2616', '#7a4420', '#a86a34', '#d49a5c'],
  cork: ['#3a2212', '#6e4420', '#a87238', '#d4a25c', '#f0cf8c'],
  dark: ['#0c0a14', '#1c1626', '#30263a', '#4a3c54', '#6e5c74'],
  ash: ['#22202a', '#4a4650', '#7a747c', '#aaa4a6', '#dedad6'],
};
const GLASS = ['#3a5068', '#5a7890', '#9cc0d0', '#d8f0f4', '#ffffff'], HOLLOW = '#1e2838', INK = '#120b16';
const rgbOf = (h) => [parseInt(h.slice(1, 3), 16), parseInt(h.slice(3, 5), 16), parseInt(h.slice(5, 7), 16)];
const hexOf = (r, g, b) => '#' + [r, g, b].map((v) => Math.max(0, Math.min(255, Math.round(v))).toString(16).padStart(2, '0')).join('');
const mixc = (a, b, t) => { const A = rgbOf(a), B = rgbOf(b); return hexOf(A[0] + (B[0] - A[0]) * t, A[1] + (B[1] - A[1]) * t, A[2] + (B[2] - A[2]) * t); };
const hsh = (x, y, s = 0) => { let n = (x * 374761393 + y * 668265263 + s * 2147483647) | 0; n = (n ^ (n >>> 13)) * 1274126177 | 0; return ((n ^ (n >>> 16)) >>> 0) / 4294967296; };

class Spr {
  constructor(w, h) { this.w = w; this.h = h; this.d = new Array(w * h).fill(null); }
  px(x, y, c) { x = Math.floor(x); y = Math.floor(y); if (c && x >= 0 && y >= 0 && x < this.w && y < this.h) this.d[y * this.w + x] = c; }
  clear(x, y) { if (x >= 0 && y >= 0 && x < this.w && y < this.h) this.d[y * this.w + x] = null; }
  get(x, y) { return x < 0 || y < 0 || x >= this.w || y >= this.h ? null : this.d[y * this.w + x]; }
  rect(x, y, w, h, c) { for (let yy = y; yy < y + h; yy++) for (let xx = x; xx < x + w; xx++) this.px(xx, yy, typeof c === 'function' ? c(xx, yy) : c); }
  shape(test, c) { for (let y = 0; y < this.h; y++) for (let x = 0; x < this.w; x++) if (test(x + 0.5, y + 0.5)) this.px(x, y, typeof c === 'function' ? c(x + 0.5, y + 0.5) : c); }
  ell(cx, cy, rx, ry, c) {
    this.shape((x, y) => ((x - cx) / rx) ** 2 + ((y - cy) / ry) ** 2 <= 1, typeof c === 'function' ? (x, y) => c((x - cx) / rx, (y - cy) / ry, x, y) : c);
  }
  // sphere-shaded ellipse: light from the upper left; facet > 0 snaps normals for a chiselled rock look
  ball(cx, cy, rx, ry, r, o = {}) {
    const f = o.facet || 0, bias = o.bias || 0;
    this.ell(cx, cy, rx, ry, (nx, ny, x, y) => {
      if (o.clip && !o.clip(x, y)) return null;
      if (f) { nx = Math.round(nx * f) / f; ny = Math.round(ny * f) / f; }
      const nz = Math.sqrt(Math.max(0, 1 - nx * nx - ny * ny)), l = -nx * 0.5 - ny * 0.62 + nz * 0.6 + bias;
      return r[l > 0.84 ? 4 : l > 0.52 ? 3 : l > 0.16 ? 2 : l > -0.22 ? 1 : 0];
    });
  }
  poly(pts, c) {
    const n = pts.length;
    this.shape((x, y) => {
      let inside = false;
      for (let i = 0, j = n - 1; i < n; j = i++) {
        const [xi, yi] = pts[i], [xj, yj] = pts[j];
        if (yi > y !== yj > y && x < ((xj - xi) * (y - yi)) / (yj - yi) + xi) inside = !inside;
      }
      return inside;
    }, c);
  }
  line(x0, y0, x1, y1, c) {
    const n = Math.max(Math.abs(x1 - x0), Math.abs(y1 - y0), 1);
    for (let i = 0; i <= n; i++) this.px(Math.round(x0 + (x1 - x0) * i / n), Math.round(y0 + (y1 - y0) * i / n), typeof c === 'function' ? c(i / n) : c);
  }
  // a tapering tube along a path: cf(t, nx, ny) picks the colour from position along and across
  tube(pf, rf, cf, n = 48) {
    for (let y = 0; y < this.h; y++) for (let x = 0; x < this.w; x++) {
      let best = null;
      for (let i = 0; i <= n; i++) {
        const t = i / n, p = pf(t), r = rf(t), d = Math.hypot(x + 0.5 - p[0], y + 0.5 - p[1]) / r;
        if (d < 1 && (!best || d < best.d)) best = { d, t, nx: (x + 0.5 - p[0]) / r, ny: (y + 0.5 - p[1]) / r };
      }
      if (best) this.px(x, y, cf(best.t, best.nx, best.ny, best.d));
    }
  }
  // dark outline tinted by the pixel it hugs
  outline(k = 0.74) {
    const add = [];
    for (let y = 0; y < this.h; y++) for (let x = 0; x < this.w; x++) {
      if (this.get(x, y)) continue;
      const nb = this.get(x - 1, y) || this.get(x + 1, y) || this.get(x, y - 1) || this.get(x, y + 1);
      if (nb) add.push([x, y, mixc(nb, INK, k)]);
    }
    for (const [x, y, c] of add) this.px(x, y, c);
    return this;
  }
  canvas() {
    const cv = document.createElement('canvas'); cv.width = this.w; cv.height = this.h;
    const c = cv.getContext('2d');
    for (let y = 0; y < this.h; y++) for (let x = 0; x < this.w; x++) { const v = this.d[y * this.w + x]; if (v) { c.fillStyle = v; c.fillRect(x, y, 1, 1); } }
    return cv;
  }
}

const N = 24, spr = () => new Spr(N, N);
const sparkle = (s, x, y, c = '#ffffff') => { s.px(x, y, c); s.px(x - 1, y, c); s.px(x + 1, y, c); s.px(x, y - 1, c); s.px(x, y + 1, c); };
function cork(s, x, y, w, h) { s.rect(x, y, w, h, (xx, yy) => R.cork[xx === x ? 3 : xx === x + w - 1 ? 1 : 2]); s.rect(x, y, w, 1, R.cork[3]); s.px(x, y, R.cork[4]); }
const liquid = (L, x, cx, hw) => { const u = (x - cx) / hw; return u < -0.45 ? L[3] : u < 0.35 ? L[2] : L[1]; };

function potionRound(L) {
  const s = spr();
  s.ell(12, 15, 7.5, 7.5, (nx, ny, x, y) => {
    const d = Math.hypot(nx, ny);
    if (d > 0.85) return nx + ny < -0.35 ? GLASS[3] : nx + ny < 0.6 ? GLASS[2] : GLASS[1];
    if (y < 11) return HOLLOW;
    if (y < 12) return L[4];
    const l = -nx * 0.55 - ny * 0.25 + 0.3;
    return l > 0.7 ? L[3] : l > 0.2 ? L[2] : L[1];
  });
  s.rect(10, 4, 4, 5, (x) => [GLASS[1], GLASS[3], GLASS[2], GLASS[0]][x - 10]);
  s.rect(9, 4, 6, 1, GLASS[2]); cork(s, 10, 1, 4, 3);
  s.px(8, 12, '#ffffff'); s.px(8, 13, '#ffffff'); s.px(8, 14, GLASS[3]); s.px(9, 11, '#ffffff');
  s.px(14, 16, L[4]); s.px(11, 18, L[3]); s.px(15, 14, L[3]);
  return s.outline();
}
function potionTall(L) {
  const s = spr();
  for (let y = 8; y <= 21; y++) for (let x = 8; x <= 15; x++) {
    let c;
    if (x === 8) c = GLASS[3]; else if (x === 15 || y === 21) c = GLASS[1];
    else if (y < 12) c = HOLLOW; else if (y === 12) c = L[4]; else c = liquid(L, x + 0.5, 12, 3.5);
    s.px(x, y, c);
  }
  s.clear(8, 8); s.clear(15, 8); s.clear(8, 21); s.clear(15, 21); s.rect(9, 8, 6, 1, GLASS[2]);
  s.rect(10, 4, 4, 4, (x) => [GLASS[1], GLASS[3], GLASS[2], GLASS[0]][x - 10]); s.rect(9, 4, 6, 1, GLASS[2]);
  cork(s, 10, 1, 4, 3);
  s.rect(10, 14, 1, 4, '#ffffff'); s.px(10, 19, L[4]); s.px(13, 16, L[4]); s.px(12, 19, L[3]);
  // paper label
  s.rect(10, 15, 5, 3, (x, y) => (y === 16 && x > 10 && x < 14 ? R.bone[1] : R.bone[3])); s.rect(10, 14, 1, 1, '#ffffff');
  return s.outline();
}
function potionFlask(L) {
  const s = spr();
  for (let y = 10; y <= 21; y++) {
    const hw = 1.6 + (y - 10) * 0.56, x0 = Math.round(12 - hw), x1 = Math.round(12 + hw) - 1;
    for (let x = x0; x <= x1; x++) {
      let c;
      if (x === x0) c = GLASS[3]; else if (x === x1 || y === 21) c = GLASS[1];
      else if (y < 15) c = HOLLOW; else if (y === 15) c = L[4]; else c = liquid(L, x + 0.5, 12, hw);
      s.px(x, y, c);
    }
  }
  s.rect(10, 4, 4, 6, (x) => [GLASS[1], GLASS[3], GLASS[2], GLASS[0]][x - 10]); s.rect(9, 4, 6, 1, GLASS[2]);
  cork(s, 10, 1, 4, 3);
  s.px(9, 17, '#ffffff'); s.px(8, 18, '#ffffff'); s.px(8, 19, GLASS[3]); s.px(13, 18, L[4]); s.px(15, 19, L[3]); s.px(11, 20, L[4]);
  s.px(12, 12, L[3]); s.px(13, 9, L[4]);
  return s.outline();
}
function ingot(M, rune) {
  const s = spr();
  s.poly([[3, 13], [21, 13], [20, 20], [4, 20]], (x, y) => (y > 18 ? M[1] : x > 18 ? M[1] : M[2]));
  s.poly([[6, 8], [18, 8], [21, 13], [3, 13]], (x, y) => (y < 9 ? M[4] : x < 7 + (13 - y) * 0.6 ? M[4] : M[3]));
  s.rect(4, 13, 16, 1, M[4]);
  s.rect(9, 10, 6, 2, M[2]); s.rect(9, 10, 6, 1, M[1]);
  if (rune) { s.px(11, 15, rune); s.px(12, 16, rune); s.px(13, 15, rune); s.px(12, 17, rune); s.px(12, 14, rune); }
  s.px(5, 14, '#ffffff'); s.px(7, 9, '#ffffff');
  return s.outline();
}
function rock(s, K) {
  s.ball(9.5, 15, 7, 6, K, { facet: 2.5 }); s.ball(15, 13, 6.5, 6, K, { facet: 2.5 }); s.ball(13, 17.5, 8.5, 4.5, K, { facet: 2.5, bias: -0.12 });
}
function gemAt(s, x, y, V) { s.px(x, y - 1, V[4]); s.px(x - 1, y, V[3]); s.px(x, y, V[3]); s.px(x + 1, y, V[2]); s.px(x, y + 1, V[1]); }
function ore(K, V, specks) {
  const s = spr(); rock(s, K);
  gemAt(s, 8, 13, V); gemAt(s, 14, 11, V); gemAt(s, 16, 16, V); gemAt(s, 11, 18, V); s.px(18, 12, V[3]); s.px(6, 17, V[2]);
  if (specks) for (const [x, y] of [[12, 14], [17, 14], [9, 17], [14, 19]]) s.px(x, y, specks);
  return s.outline();
}
function emberCoal() {
  const s = spr(); rock(s, R.dark);
  const O = R.orange, crack = (pts) => { for (let i = 1; i < pts.length; i++) s.line(pts[i - 1][0], pts[i - 1][1], pts[i][0], pts[i][1], O[3]); };
  crack([[6, 14], [9, 15], [11, 13], [14, 14], [16, 11]]); crack([[11, 13], [12, 17], [15, 19]]); crack([[14, 14], [18, 16]]);
  for (const [x, y] of [[9, 15], [11, 13], [14, 14], [12, 17]]) s.px(x, y, R.gold[4]);
  s.px(8, 11, O[2]); s.px(17, 18, O[2]); s.px(15, 9, O[4]); s.px(18, 7, O[3]); s.px(12, 6, O[3]);
  return s.outline();
}
function crystals(M) {
  const s = spr();
  s.ell(12, 21, 9, 2.4, (nx) => R.stone[nx < -0.3 ? 2 : 1]);
  const prism = (bx, by, w, h, lean) => {
    const sh = w * 0.75, ax = (y) => bx + lean * (by - y) / h;
    s.poly([[bx - w / 2, by], [ax(by - h + sh) - w / 2, by - h + sh], [bx + lean, by - h], [ax(by - h + sh) + w / 2, by - h + sh], [bx + w / 2, by]],
      (x, y) => { const u = (x - ax(y)) / (w / 2); return y > by - 1.5 ? M[1] : u < -0.2 ? M[3] : u < 0.2 ? M[4] : y < by - h + sh ? M[2] : M[1]; });
  };
  prism(7, 21, 5, 11, -2); prism(17.5, 21, 5, 13, 2.5); prism(12, 22, 7, 19, 0);
  sparkle(s, 12, 5); s.px(6, 12, '#ffffff'); s.px(19, 10, '#ffffff');
  return s.outline();
}
function cubes(M) {
  const s = spr();
  const cube = (cx, cy, k) => {
    s.poly([[cx - k, cy - k / 2], [cx, cy], [cx, cy + k], [cx - k, cy + k / 2]], M[3]);
    s.poly([[cx, cy], [cx + k, cy - k / 2], [cx + k, cy + k / 2], [cx, cy + k]], M[1]);
    s.poly([[cx, cy - k], [cx + k, cy - k / 2], [cx, cy], [cx - k, cy - k / 2]], M[4]);
    s.line(cx - k, cy - k / 2, cx, cy, '#ffffff');
  };
  cube(15, 9, 5); cube(9, 13, 6.5); cube(17, 17, 4); s.px(5, 20, M[3]); s.px(13, 21, M[4]); s.px(20, 12, M[4]);
  sparkle(s, 7, 8);
  return s.outline();
}
function mound(s, cx, cy, rx, ry, P, seed) {
  s.ell(cx, cy, rx, ry, (nx, ny, x, y) => {
    if (ny > 0.25) return null;
    const l = -nx * 0.5 - ny * 0.7, h = hsh(Math.floor(x), Math.floor(y), seed);
    let i = l > 0.75 ? 4 : l > 0.35 ? 3 : l > -0.1 ? 2 : 1;
    if (h > 0.82) i = Math.min(4, i + 1); else if (h < 0.16) i = Math.max(0, i - 1);
    return P[i];
  });
}
function bowlPowder(P) {
  const s = spr();
  s.ell(12, 16, 9.5, 6, (nx, ny) => (ny < 0 ? null : R.wood[nx < -0.5 ? 3 : nx < 0.3 ? 2 : 1]));
  s.rect(9, 21, 6, 1, R.wood[1]);
  s.ell(12, 16, 9.5, 2.2, (nx, ny) => R.wood[ny < 0 ? 1 : 3]);
  mound(s, 12, 15.5, 7.5, 6.5, P, 3);
  s.ell(12, 16.6, 7.5, 1.4, (nx, ny, x, y) => P[hsh(Math.floor(x), Math.floor(y), 5) > 0.5 ? 2 : 1]);
  s.px(3, 16, R.wood[4]); s.px(4, 15, R.wood[4]); sparkle(s, 10, 11, P[4]);
  return s.outline();
}
function pouch(P, C) {
  const s = spr();
  s.ball(12, 16, 8, 6.5, C);
  s.rect(8, 7, 8, 4, (x, y) => C[x < 10 ? 3 : x < 13 ? 2 : 1]);
  s.rect(7, 6, 10, 1, (x) => C[x % 2 ? 3 : 2]);
  s.ell(12, 6.5, 4, 1.6, (nx, ny, x, y) => P[hsh(Math.floor(x), Math.floor(y), 9) > 0.55 ? 4 : 3]);
  s.rect(8, 10, 8, 1, R.gold[2]); s.px(9, 10, R.gold[4]); s.px(16, 11, R.gold[2]); s.px(17, 12, R.gold[3]); s.px(17, 13, R.gold[1]);
  s.px(9, 14, C[4]); s.px(8, 15, C[4]);
  for (const [x, y, i] of [[18, 21, 3], [20, 21, 4], [19, 20, 4], [21, 20, 2], [4, 21, 3]]) s.px(x, y, P[i]);
  sparkle(s, 12, 3, P[4]);
  return s.outline();
}
function ashPile(P) {
  const s = spr();
  mound(s, 12, 19, 10, 9.5, P, 7);
  s.rect(3, 20, 18, 1, (x) => P[hsh(x, 1, 2) > 0.5 ? 1 : 0]);
  s.tube((t) => [14 + t * 5, 12 - t * 2], () => 1.1, (t, nx, ny) => R.bone[ny < 0 ? 4 : 3], 12);
  s.px(13, 12, R.bone[4]); s.px(13, 11, R.bone[3]); s.px(20, 9, R.bone[4]); s.px(20, 11, R.bone[3]);
  s.px(7, 15, P[4]); s.px(5, 21, P[2]); s.px(21, 21, P[3]);
  return s.outline();
}
function quicksilver() {
  const s = spr(), M = R.iron;
  s.ball(12, 15, 8, 5.5, M); s.ball(19.5, 19.5, 2.2, 1.7, M); s.ball(4.5, 19.5, 1.7, 1.3, M);
  s.ell(12, 16.5, 6.5, 1.3, (nx) => M[nx < 0 ? 1 : 0]); s.ell(13, 18.6, 5, 0.9, M[3]);
  s.ell(9, 12.6, 2.6, 1.2, '#ffffff'); s.px(15, 12, M[4]); s.px(19, 19, '#ffffff'); s.px(4, 19, '#ffffff');
  return s.outline();
}
function moss() {
  const s = spr(), G = R.green;
  s.ball(12, 19, 9, 3.6, R.stone, { facet: 2 });
  s.ball(7.5, 15.5, 4.2, 3.6, G); s.ball(17, 16, 4, 3.2, G); s.ball(12.5, 13, 5.6, 4.8, G);
  for (const [x, y] of [[10, 10], [14, 12], [7, 14], [17, 15], [12, 15], [9, 17], [15, 9], [19, 17]]) s.px(x, y, R.teal[4]);
  for (const [x, y] of [[11, 11], [15, 13], [6, 16], [13, 16]]) s.px(x, y, G[4]);
  s.px(13, 6, R.teal[3]); s.px(8, 8, R.teal[4]); s.px(18, 10, R.teal[3]);
  return s.outline();
}
function stem(s, x, y0, y1) {
  const G = R.green; s.line(x, y0, x, y1, G[2]); s.line(x + 1, y0 + 1, x + 1, y1, G[1]);
  s.poly([[x, 18], [x - 5, 15.5], [x - 6, 18], [x - 2, 20]], (px, py) => G[py < 17.5 ? 3 : 2]);
  s.poly([[x + 1, 20], [x + 6, 17], [x + 7, 19.5], [x + 3, 22]], (px, py) => G[py < 19.5 ? 2 : 1]);
}
function flowerRound(Pt, Ct) {
  const s = spr(); stem(s, 12, 12, 22);
  for (const k of [3, 4, 5, 0, 2, 1]) { const a = -Math.PI / 2 + k * Math.PI / 3; s.ball(12 + Math.cos(a) * 4.4, 8.5 + Math.sin(a) * 4.4, 3.1, 3.1, Pt, { bias: k > 2 ? -0.25 : 0.05 }); }
  s.ball(12, 8.5, 2.4, 2.4, Ct, { bias: 0.2 }); s.px(11, 7, '#ffffff');
  s.px(5, 4, Pt[4]); s.px(19, 3, Pt[4]); s.px(20, 11, Pt[3]);
  return s.outline();
}
function flowerSun(Pt, Ct) {
  const s = spr(); stem(s, 12, 13, 22);
  for (let k = 0; k < 10; k++) {
    const a = k * Math.PI / 5 + 0.2, c = Math.cos(a), n = Math.sin(a), L = k % 2 ? 7.5 : 6, w = 1.9;
    s.poly([[12 + c * L, 9 + n * L], [12 - n * w + c * 2, 9 + c * w + n * 2], [12 + n * w + c * 2, 9 - c * w + n * 2]], Pt[k % 2 ? 3 : n > 0.3 ? 1 : 2]);
  }
  s.ball(12, 9, 3.6, 3.6, Ct); s.px(11, 8, Ct[4]); s.px(13, 10, Ct[0]); s.px(11, 10, Ct[1]); s.px(13, 8, Ct[1]);
  return s.outline();
}
function mushroom(Cp, Sp) {
  const s = spr();
  s.tube((t) => [12 + Math.sin(t * 2) * 0.8, 12 + t * 9], (t) => 2.6 + t * 1.1, (t, nx) => R.bone[nx < -0.4 ? 4 : nx < 0.3 ? 3 : 2], 20);
  s.ell(12, 13, 8, 2, (nx, ny, x) => (ny < 0 ? null : Math.floor(x) % 2 ? R.bone[2] : Cp[0]));
  s.ball(12, 13, 9.5, 9, Cp, { clip: (x, y) => y < 13.2 });
  for (const [x, y] of [[8, 8], [9, 8], [8, 9], [14, 6], [15, 6], [15, 7], [17, 10], [11, 11], [5, 11], [12, 5]]) s.px(x, y, Sp);
  s.px(6, 20, Sp); s.px(19, 17, Sp); s.px(18, 4, Sp);
  return s.outline();
}
function mandrake() {
  const s = spr(), C = R.cork, G = R.green, sk = (t, nx) => C[nx < -0.35 ? 4 : nx < 0.3 ? 3 : 2];
  s.tube((t) => [10.5 - t * 3, 17 + t * 5], (t) => 1.9 - t * 1.1, sk, 14); s.tube((t) => [13.5 + t * 3.5, 17 + t * 5], (t) => 1.9 - t * 1.1, sk, 14);
  s.tube((t) => [8 - t * 4, 13 + t * 2.5], (t) => 1.4 - t * 0.7, sk, 10); s.tube((t) => [16 + t * 4, 13 - t * 1.5], (t) => 1.4 - t * 0.7, sk, 10);
  s.ball(12, 14, 4.8, 5.6, [C[1], C[2], C[3], C[4], C[4]]);
  s.poly([[12, 9], [7, 2], [10, 3], [12, 6]], G[3]); s.poly([[12, 9], [12, 1], [14, 3], [13, 7]], G[2]); s.poly([[12, 9], [18, 3], [16, 7], [13, 9]], G[1]);
  s.px(8, 3, G[4]); s.px(12, 2, G[4]);
  s.px(10, 13, INK); s.px(14, 13, INK); s.px(10, 12, C[1]); s.px(14, 12, C[1]); s.px(11, 16, INK); s.px(12, 17, INK); s.px(13, 16, INK);
  s.px(9, 16, C[1]); s.px(15, 17, C[1]); s.px(12, 19, C[1]);
  return s.outline();
}
function feather() {
  const s = spr(), ax = [6, 21], bx = [18.5, 2.5], dx = bx[0] - ax[0], dy = bx[1] - ax[1], L = Math.hypot(dx, dy);
  s.shape((x, y) => {
    const u = ((x - ax[0]) * dx + (y - ax[1]) * dy) / (L * L), v = ((x - ax[0]) * dy - (y - ax[1]) * dx) / L;
    if (u < 0.02 || u > 1) return false;
    if (u < 0.16) return Math.abs(v) < 0.6;
    const w = Math.pow(Math.sin(Math.PI * Math.min(1, (u - 0.1) / 0.9)), 0.55) * 4.6;
    if (Math.abs(v) > w) return false;
    return !(Math.floor(u * 17) % 6 === 2 && Math.abs(v) > w * 0.45);
  }, (x, y) => {
    const u = ((x - ax[0]) * dx + (y - ax[1]) * dy) / (L * L), v = ((x - ax[0]) * dy - (y - ax[1]) * dx) / L;
    if (Math.abs(v) < 0.6) return R.gold[4];
    const lit = v > 0 ? 1 : 0;
    if (u < 0.36) return R.red[2 + lit]; if (u < 0.6) return R.orange[2 + lit]; if (u < 0.84) return R.orange[3 + lit];
    return R.gold[3 + lit];
  });
  s.px(20, 5, R.gold[4]); s.px(4, 10, R.orange[3]); s.px(9, 6, R.gold[3]); sparkle(s, 19, 13, R.gold[4]);
  return s.outline();
}
function scale(M) {
  const s = spr(), pts = [[12, 2], [19, 7], [19, 15], [12, 22], [5, 15], [5, 7]];
  s.poly(pts, (x, y) => {
    const ax = Math.abs(x - 12), band = Math.floor((y + ax * 0.9) / 3.2) % 2;
    let i = band ? 2 : 1;
    if (x < 12 && y < 13) i += 1;
    if (x > 14 && y > 12) i -= 1;
    if (ax < 0.7) i = 4;
    return M[Math.max(0, Math.min(4, i))];
  });
  s.line(12, 2, 5, 7, M[4]); s.line(5, 7, 5, 14, M[3]); s.px(8, 7, '#ffffff'); s.px(9, 6, '#ffffff'); s.px(8, 8, M[4]);
  return s.outline();
}
function fang() {
  const s = spr(), B = R.bone;
  s.tube((t) => [8.5 + 8.5 * t * t, 5 + 16.5 * t], (t) => 4.4 * Math.pow(1 - t, 0.75) + 0.5, (t, nx) => {
    if (t < 0.13) return R.red[nx < -0.3 ? 2 : 1];
    return B[nx < -0.45 ? 4 : nx < 0.15 ? 3 : nx < 0.6 ? 2 : 1];
  });
  s.px(6, 8, '#ffffff'); s.px(6, 9, '#ffffff'); s.px(7, 11, '#ffffff'); s.px(11, 9, B[2]); s.px(12, 12, B[2]);
  return s.outline();
}
function eye() {
  const s = spr();
  s.tube((t) => [17 + t * 4, 18 + t * 3 + Math.sin(t * 5)], (t) => 1.5 - t * 0.8, (t, nx, ny) => R.red[ny < 0 ? 3 : 2], 12);
  s.ball(11.5, 12.5, 8.5, 8.5, ['#7a4a52', '#b89098', '#e4d2c8', '#fbf2e2', '#ffffff']);
  for (const [x, y] of [[4, 13], [5, 14], [17, 16], [18, 15], [16, 6], [8, 19], [9, 18], [15, 18]]) s.px(x, y, R.red[2]);
  s.ell(10.5, 11.5, 4.8, 4.8, (nx, ny) => { const d = Math.hypot(nx, ny); return d > 0.82 ? R.green[0] : d > 0.5 ? (ny < 0 ? R.gold[3] : R.gold[2]) : ny < 0 ? R.green[3] : R.green[2]; });
  s.ell(10.5, 11.5, 1.3, 4, INK); s.px(8, 9, '#ffffff'); s.px(9, 9, '#ffffff'); s.px(8, 10, '#ffffff');
  return s.outline();
}
function horn() {
  const s = spr(), B = R.bone, P = R.pink;
  s.tube((t) => [6.5 + 11.5 * t, 20.5 - 18 * t], (t) => 3.7 * (1 - t) + 0.6, (t, nx, ny) => {
    const side = nx - ny, band = Math.floor(t * 8 + side * 0.55) % 2;
    return band ? (side < -0.3 ? B[4] : side < 0.5 ? B[3] : B[2]) : side < -0.3 ? P[4] : side < 0.5 ? P[3] : P[2];
  }, 60);
  s.px(5, 18, '#ffffff'); s.px(6, 17, '#ffffff'); sparkle(s, 19, 9, R.gold[4]); s.px(5, 6, R.gold[4]); s.px(21, 16, R.gold[3]); s.px(13, 3, '#ffffff');
  return s.outline();
}
function wing() {
  const s = spr(), root = [5, 20], col = (x, y) => {
    const d = Math.hypot(x - root[0], y - root[1]), a = Math.atan2(root[1] - y, x - root[0]);
    if (Math.abs(((a * 9) % 1) - 0.5) < 0.09 && d > 3) return R.violet[2];
    return d < 6 ? R.pink[3] : d < 11 ? R.pink[4] : d < 15 ? R.teal[4] : R.teal[3];
  };
  s.poly([[5, 20], [6, 9], [11, 3], [18, 2], [21, 6], [19, 12], [13, 17]], col);
  s.poly([[5, 20], [13, 18], [19, 17], [20, 20], [15, 23], [9, 22]], col);
  s.line(11, 3, 18, 2, '#ffffff'); s.line(18, 2, 21, 6, R.teal[4]); s.px(8, 8, '#ffffff'); sparkle(s, 16, 7); s.px(18, 20, '#ffffff'); s.px(3, 12, R.pink[4]);
  return s.outline(0.6);
}

// name, kind, builder, glow colour (null = does not glow)
const MATERIALS = [
  ['EMBER COAL', 'ORE', emberCoal, '#ff6a1e'],
  ['IRON ORE', 'ORE', () => ore(R.stone, R.iron, R.orange[2]), null],
  ['STARMETAL ORE', 'ORE', () => ore(R.dark, R.blue, '#ffffff'), '#7cc0f8'],
  ['GOLD INGOT', 'METAL', () => ingot(R.gold), '#f8d250'],
  ['SKY IRON', 'METAL', () => ingot(R.iron, R.blue[3]), null],
  ['QUICKSILVER', 'METAL', quicksilver, null],
  ['HUMSTONE', 'CRYSTAL', () => crystals(R.violet), '#c486f4'],
  ['FROST SALT', 'CRYSTAL', () => cubes(R.blue), '#7cc0f8'],
  ['SULFUR', 'POWDER', () => bowlPowder(R.gold), null],
  ['MOONDUST', 'POWDER', () => pouch(R.blue, R.wood), '#7cc0f8'],
  ['BONE ASH', 'POWDER', () => ashPile(R.ash), null],
  ['DEEP TEAR', 'POTION', () => potionRound(R.teal), '#6ee6c8'],
  ['DRAGON BLOOD', 'POTION', () => potionTall(R.red), '#ee6a3c'],
  ['VERDANT TONIC', 'POTION', () => potionFlask(R.green), '#8ad04a'],
  ['GLOWMOSS', 'PLANT', moss, '#6ee6c8'],
  ['MOONPETAL', 'PLANT', () => flowerRound(R.blue, R.gold), '#7cc0f8'],
  ['SUNBLOOM', 'PLANT', () => flowerSun(R.orange, R.wood), '#ffa63a'],
  ['DUSK CAP', 'PLANT', () => mushroom(R.violet, R.teal[4]), '#c486f4'],
  ['MANDRAKE', 'PLANT', mandrake, null],
  ['PHOENIX FEATHER', 'BEAST', feather, '#ffa63a'],
  ['DRAGON SCALE', 'BEAST', () => scale(R.teal), null],
  ['WYRM FANG', 'BEAST', fang, null],
  ['BASILISK EYE', 'BEAST', eye, '#8ad04a'],
  ['UNICORN HORN', 'BEAST', horn, '#f890b8'],
  ['PIXIE WING', 'BEAST', wing, '#f890b8'],
].map(([name, kind, make, glow]) => ({ name, kind, make, glow }));
const KIND_COL = { ORE: '#c08a5c', METAL: '#a8b8c4', CRYSTAL: '#c486f4', POWDER: '#e4d2aa', POTION: '#6ee6c8', PLANT: '#8ad04a', BEAST: '#ee6a3c' };
