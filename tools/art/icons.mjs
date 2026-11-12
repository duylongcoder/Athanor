// The material and catalyst icon set (24x24), drawn in code.
import { R, GLASS, HOLLOW, INK, hsh, Spr } from './painter.mjs';

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

function ashPile(P, withBone = true) {
  const s = spr();
  mound(s, 12, 19, 10, 9.5, P, 7);
  s.rect(3, 20, 18, 1, (x) => P[hsh(x, 1, 2) > 0.5 ? 1 : 0]);
  if (withBone) {
    // a charred bone lying across the heap, knuckles at both ends
    const B = R.bone;
    s.tube((t) => [6 + t * 12, 13 - t * 6], () => 1.7, (t, nx, ny) => (ny < -0.1 ? '#ffffff' : ny < 0.55 ? B[4] : B[2]), 30);
    for (const [x, y] of [[4.6, 12], [6.2, 14.6], [17.6, 5.2], [19.4, 7.6]]) s.ball(x, y, 2.1, 2.1, [B[2], B[3], B[4], '#ffffff', '#ffffff']);
  }
  s.px(7, 17, P[4]); s.px(5, 21, P[2]); s.px(21, 21, P[3]); s.px(15, 16, P[4]);
  return s.outline();
}
function quicksilver() {
  const s = spr(), M = R.iron, D = R.stone;
  // a shallow stone dish holding one fat mirror-bright bead
  s.ell(12, 17, 10, 5, (nx, ny) => (ny < 0 ? null : D[nx < -0.4 ? 3 : nx < 0.4 ? 2 : 1]));
  s.ell(12, 17, 10, 2.4, (nx, ny) => D[ny < 0 ? 0 : 3]); s.rect(8, 22, 8, 1, D[1]);
  s.ball(12, 13, 7, 5.6, M, { bias: 0.12 });
  s.ell(12, 15.4, 6, 1.2, (nx) => M[nx < 0 ? 1 : 0]); s.ell(13, 17, 4.5, 0.8, M[3]);
  s.ell(9.2, 10.6, 2.8, 1.3, '#ffffff'); s.px(15, 10, M[4]); s.px(16, 11, M[4]);
  s.ball(20, 14.5, 1.8, 1.5, M, { bias: 0.2 }); s.px(19, 14, '#ffffff'); s.ball(4.5, 13.5, 1.3, 1.1, M, { bias: 0.2 }); s.px(4, 13, '#ffffff');
  return s.outline();
}
// a filled, rotated ellipse test
const lobe = (cx, cy, rx, ry, ang) => { const c = Math.cos(ang), n = Math.sin(ang); return (x, y) => { const dx = x - cx, dy = y - cy; return ((dx * c + dy * n) / rx) ** 2 + ((-dx * n + dy * c) / ry) ** 2 <= 1; }; };
function wing() {
  const s = spr(), root = [4.5, 20.5], up = lobe(13, 9.5, 9.6, 4.4, -0.62), low = lobe(13.5, 18, 7.4, 3.1, -0.08);
  const col = (x, y) => {
    const d = Math.hypot(x - root[0], y - root[1]), a = Math.atan2(root[1] - y, x - root[0]);
    if (d > 4 && Math.abs(((a * 7.5) % 1) - 0.5) < 0.11) return R.violet[3];
    return d < 6 ? R.pink[2] : d < 10 ? R.pink[3] : d < 14 ? R.pink[4] : d < 17 ? R.teal[4] : R.teal[3];
  };
  s.shape(low, col); s.shape(up, col);
  s.line(7, 12, 12, 5, '#ffffff'); s.line(12, 5, 17, 3, '#ffffff'); s.line(8, 17, 13, 15, R.pink[4]);
  s.px(19, 4, '#ffffff'); s.px(20, 16, '#ffffff'); sparkle(s, 20, 9); s.px(3, 11, R.pink[4]); s.px(17, 22, R.teal[4]);
  s.ball(4.5, 20.5, 1.6, 1.6, R.violet);
  return s.outline(0.6);
}

/* ---------------- discoveries added for the full game ---------------- */
function steamWisp() {
  const s = spr(), K = ['#3a6a80', '#6aa8b8', '#a8dce0', '#e0fbf8', '#ffffff'];
  s.tube((t) => [14 + Math.sin(t * 4) * 3, 9 - t * 7], (t) => 2.6 * (1 - t) + 0.5, (t, nx) => K[nx < 0 ? 4 : 3], 20);
  s.ball(8, 16.5, 4.4, 3.8, K); s.ball(16.5, 16.5, 4.8, 3.8, K); s.ball(12, 13, 7, 6, K);
  s.px(10, 13, INK); s.px(14, 13, INK); s.px(10, 12, INK); s.px(14, 12, INK); s.px(11, 16, K[0]); s.px(12, 16, K[0]); s.px(13, 16, K[0]); s.px(9, 10, '#ffffff');
  return s.outline(0.55);
}
function cinnabar() {
  const s = spr(), K = R.red;
  s.ball(9, 15, 6.5, 6, K, { facet: 2 }); s.ball(15, 12.5, 6.5, 6.5, K, { facet: 2 }); s.ball(13, 18, 8.5, 4, K, { facet: 2, bias: -0.1 });
  s.line(10, 10, 13, 14, K[4]); s.line(13, 14, 18, 12, K[3]); s.line(13, 14, 12, 19, K[0]);
  s.px(8, 12, '#ffffff'); s.px(9, 11, '#ffffff'); s.px(16, 9, R.orange[4]);
  s.ball(19.5, 20, 2, 1.6, R.iron, { bias: 0.2 }); s.px(19, 19, '#ffffff'); s.ball(4, 20.5, 1.4, 1.1, R.iron, { bias: 0.2 });
  return s.outline();
}
function stormGlass() {
  const s = spr(), B = R.blue;
  s.ell(12, 21, 6, 2, (nx, ny) => R.gold[ny < 0 ? 3 : 1]); s.rect(9, 18, 6, 3, (x) => R.gold[x < 11 ? 3 : x < 13 ? 2 : 1]);
  s.ell(12, 11, 9, 9, (nx, ny) => {
    const d = Math.hypot(nx, ny);
    if (d > 0.86) return nx + ny < -0.3 ? GLASS[3] : nx + ny < 0.6 ? GLASS[2] : GLASS[1];
    return ny < -0.25 ? B[0] : ny < 0.3 ? B[1] : '#1c2a66';
  });
  s.ball(9, 8.5, 3.2, 2.2, R.iron, { bias: -0.1 }); s.ball(14, 8, 3.6, 2.4, R.iron, { bias: -0.2 }); s.ball(11.5, 6.8, 3, 2, R.iron);
  for (const [a, b, c, d] of [[12, 10, 10, 13], [10, 13, 13, 13], [13, 13, 11, 17]]) s.line(a, b, c, d, R.gold[4]);
  s.px(11, 17, '#ffffff'); s.px(12, 10, '#ffffff'); s.px(15, 14, B[3]); s.px(8, 15, B[3]);
  s.px(6, 6, '#ffffff'); s.px(6, 7, '#ffffff'); s.px(7, 5, '#ffffff');
  return s.outline();
}
function lifewater() {
  const s = potionRound(R.green), G = R.green;
  s.poly([[13, 1], [19, -1], [18, 4], [14, 3]], (x, y) => G[x < 16 ? 4 : 3]); s.line(13, 2, 18, 0, G[2]);
  sparkle(s, 12, 16, '#ffffff'); s.px(15, 18, G[4]); s.px(9, 18, G[4]);
  return s;
}
function nightshadeInk() {
  const s = spr(), V = R.violet;
  s.ball(11, 17, 8, 5.5, [V[0], V[0], V[1], V[2], V[3]]);
  s.ell(11, 12.5, 4.5, 1.6, (nx, ny) => (ny < 0 ? GLASS[1] : GLASS[3])); s.ell(11, 12.5, 3.2, 0.9, INK);
  s.rect(5, 21, 13, 1, V[0]); s.px(6, 15, V[4]); s.px(5, 16, V[4]); s.px(6, 17, V[3]);
  // the quill standing in it
  s.tube((t) => [12 + t * 8, 12 - t * 10], (t) => 0.5 + Math.sin(Math.min(1, t * 1.15) * Math.PI) * 2.2, (t, nx, ny) => (Math.abs(nx + ny) < 0.35 ? R.bone[2] : nx - ny > 0 ? R.bone[3] : R.bone[4]), 30);
  s.px(12, 12, INK); s.px(13, 11, V[1]); s.px(17, 19, V[2]); s.px(19, 21, V[1]);
  return s.outline();
}
function starShard() {
  const s = spr(), M = ['#8c6a2a', '#d4b050', '#f8e488', '#fff6c8', '#ffffff'], pts = [];
  for (let k = 0; k < 8; k++) { const a = -Math.PI / 2 + k * Math.PI / 4, r = k % 2 ? 3.2 : k % 4 === 0 ? 10.5 : 7.5; pts.push([12 + Math.cos(a) * r, 12 + Math.sin(a) * r]); }
  s.poly(pts, (x, y) => { const dx = x - 12, dy = y - 12; return Math.hypot(dx, dy) < 2.6 ? M[4] : dx + dy < -1 ? M[3] : dx + dy < 3 ? (dx - dy > 0 ? M[2] : M[3]) : M[1]; });
  s.line(12, 3, 12, 21, M[4]); s.line(6, 12, 18, 12, M[4]);
  s.px(4, 5, M[3]); s.px(19, 6, '#ffffff'); s.px(5, 19, M[2]); sparkle(s, 19, 19, M[4]);
  return s.outline();
}
function auroraDust() {
  const s = pouch(R.teal, R.violet);
  for (const [x, y, c] of [[10, 5, R.pink[4]], [14, 6, R.green[4]], [12, 4, '#ffffff'], [7, 2, R.pink[3]], [17, 2, R.green[3]], [15, 0, R.teal[4]], [9, 0, R.violet[4]], [20, 5, R.pink[4]], [3, 6, R.green[4]]]) s.px(x, y, c);
  return s;
}
function seerOrb() {
  const s = spr(), V = R.violet;
  s.poly([[6, 22], [18, 22], [16, 18], [8, 18]], (x, y) => R.gold[y < 19 ? 4 : x < 10 ? 3 : x < 15 ? 2 : 1]); s.rect(7, 17, 10, 1, R.gold[3]);
  s.ball(12, 10, 8.4, 8.4, V, { bias: 0.05 });
  s.ell(12.5, 11, 4.6, 3, (nx, ny) => { const d = Math.hypot(nx, ny); return d > 0.8 ? V[4] : d > 0.42 ? '#ffffff' : V[0]; });
  s.px(12, 11, R.teal[4]); s.px(13, 11, R.teal[3]);
  s.px(7, 5, '#ffffff'); s.px(8, 4, '#ffffff'); s.px(7, 6, '#ffffff'); s.px(17, 15, V[4]);
  sparkle(s, 20, 4, V[4]); s.px(3, 9, V[3]);
  return s.outline();
}
function sunAmalgam() {
  const s = spr(), M = R.gold;
  for (let k = 0; k < 12; k++) { const a = k * Math.PI / 6, L = k % 2 ? 10.5 : 8.5; s.line(12 + Math.cos(a) * 6, 12 + Math.sin(a) * 6, 12 + Math.cos(a) * L, 12 + Math.sin(a) * L, k % 2 ? R.orange[3] : M[3]); }
  s.ball(12, 12, 6.6, 6.6, M, { bias: 0.1 });
  s.ell(12, 14.5, 4.6, 1.1, (nx) => M[nx < 0 ? 2 : 1]); s.ell(9.6, 9.6, 2.2, 1.2, '#ffffff'); s.px(15, 9, M[4]);
  s.px(10, 13, R.orange[1]); s.px(14, 13, R.orange[1]); s.px(11, 15, R.orange[1]); s.px(12, 16, R.orange[1]); s.px(13, 15, R.orange[1]);
  return s.outline();
}
function everflame() {
  const s = spr(), O = R.orange;
  s.ell(12, 14, 8.5, 8.5, (nx, ny) => { const d = Math.hypot(nx, ny); return d > 0.86 ? (nx + ny < -0.3 ? GLASS[3] : nx + ny < 0.6 ? GLASS[2] : GLASS[1]) : '#2a1420'; });
  s.rect(8, 3, 8, 3, (x, y) => R.gold[y === 3 ? 4 : x < 11 ? 3 : x < 14 ? 2 : 1]); s.rect(10, 1, 4, 2, R.gold[2]); s.px(10, 1, R.gold[4]);
  s.rect(7, 21, 10, 2, (x, y) => R.gold[y === 21 ? 3 : 1]);
  s.tube((t) => [12 + Math.sin(t * 3.4) * 1.4, 19 - t * 11], (t) => 4.2 * Math.pow(1 - t, 0.7) + 0.4, (t, nx, ny, d) => (d < 0.4 && t < 0.55 ? '#ffffff' : d < 0.72 ? R.gold[4] : t < 0.15 ? R.blue[3] : O[3]), 30);
  s.px(7, 9, '#ffffff'); s.px(7, 10, '#ffffff'); s.px(8, 8, '#ffffff'); s.px(16, 17, O[2]);
  return s.outline();
}
function livingGold() {
  const s = ingot(R.gold), G = R.green;
  s.line(12, 10, 12, 4, G[2]); s.line(13, 9, 13, 5, G[1]);
  s.poly([[12, 6], [7, 3], [6, 6], [10, 8]], (x, y) => G[y < 5 ? 4 : 3]); s.poly([[13, 5], [18, 1], [19, 4], [15, 7]], (x, y) => G[y < 3.5 ? 3 : 2]);
  s.px(12, 3, G[4]); s.px(8, 16, R.gold[4]); s.px(9, 15, '#ffffff'); s.px(15, 16, G[3]); s.px(16, 17, G[2]); s.px(14, 17, G[2]); s.px(15, 18, G[1]);
  sparkle(s, 20, 9, R.gold[4]);
  return s;
}
function azoth() {
  const s = potionRound(['#0b3a44', '#2a8a90', '#7cdcd4', '#c8fff4', '#ffffff']);
  s.rect(9, 8, 6, 1, R.gold[3]); s.px(9, 8, R.gold[4]); s.px(14, 8, R.gold[1]);
  for (const [x, y] of [[10, 15], [11, 14], [13, 14], [14, 15], [14, 17], [13, 18], [11, 18]]) s.px(x, y, '#ffffff');
  s.px(12, 16, R.gold[4]); sparkle(s, 19, 5, '#ffffff'); s.px(4, 7, '#c8fff4'); s.px(20, 12, '#7cdcd4');
  return s;
}
function worldseed() {
  const s = spr(), M = ['#7a6a3a', '#d4c080', '#fff0b0', '#ffffff', '#ffffff'], G = R.green;
  const ring = (front) => { for (let a = 0; a < 6.283; a += 0.03) { const x = 12 + Math.cos(a) * 10.5, y = 15 + Math.sin(a) * 3.6; if ((Math.sin(a) > 0) === front) s.px(x, y, Math.cos(a) < -0.3 ? R.teal[4] : Math.cos(a) < 0.5 ? R.teal[3] : R.blue[3]); } };
  ring(false);
  s.ell(12, 13.5, 5.6, 7.6, (nx, ny) => { const l = -nx * 0.6 - ny * 0.55 + 0.25; return ny < -0.75 && Math.abs(nx) > 0.55 ? null : M[l > 0.75 ? 4 : l > 0.35 ? 3 : l > -0.1 ? 2 : l > -0.5 ? 1 : 0]; });
  s.line(12, 8, 12, 19, M[1]); s.px(12, 13, R.gold[3]); s.px(12, 14, R.gold[3]);
  ring(true);
  s.line(12, 6, 12, 2, G[3]); s.poly([[12, 4], [8, 1], [7, 4], [10, 5]], G[4]); s.poly([[12, 3], [16, 0], [17, 3], [14, 4]], G[3]);
  sparkle(s, 4, 6, '#ffffff'); sparkle(s, 20, 21, R.teal[4]); s.px(20, 5, '#ffffff'); s.px(3, 20, R.gold[4]);
  return s.outline();
}

/* ---------------- catalysts ---------------- */
function catBellows() {
  const s = spr(), Wd = R.wood;
  s.poly([[4, 6], [16, 4], [19, 12], [16, 20], [4, 18]], (x, y) => (y < 8 ? Wd[4] : y < 16 ? R.cork[2] : Wd[1]));
  for (const y of [9, 12, 15]) s.line(5, y, 17, y, R.cork[0]);
  s.poly([[19, 10], [23, 11], [23, 13], [19, 14]], (x, y) => R.iron[y < 12 ? 3 : 1]); s.rect(1, 5, 3, 3, Wd[2]); s.rect(1, 16, 3, 3, Wd[2]);
  s.px(6, 6, '#ffffff'); s.px(10, 5, R.gold[3]); s.px(10, 18, R.gold[2]);
  return s.outline();
}
function catBook(K, mark) {
  const s = spr();
  s.poly([[4, 5], [18, 3], [20, 19], [6, 21]], (x, y) => K[x < 7 ? 3 : y < 6 ? 3 : y > 18 ? 0 : 1]);
  s.poly([[18, 3], [20, 4], [22, 20], [20, 19]], (x, y) => R.bone[y % 2 ? 3 : 2]); s.rect(7, 20, 14, 1, R.bone[3]);
  mark(s);
  s.px(5, 6, K[4]); s.rect(8, 6, 8, 1, R.gold[3]); s.rect(9, 17, 8, 1, R.gold[2]);
  return s.outline();
}
function catHeartTile() {
  const s = spr(), D = R.stone;
  s.poly([[12, 6], [22, 12], [12, 18], [2, 12]], (x, y) => D[x + y < 22 ? 4 : 3]); s.poly([[2, 12], [12, 18], [12, 22], [2, 16]], D[2]); s.poly([[12, 18], [22, 12], [22, 16], [12, 22]], D[1]);
  s.ball(10.2, 10.6, 2.3, 2, R.red, { bias: 0.2 }); s.ball(13.8, 10.6, 2.3, 2, R.red); s.poly([[8, 11.4], [16, 11.4], [12, 15.5]], R.red[2]);
  s.px(9, 10, '#ffffff'); s.px(12, 14, R.red[1]);
  return s.outline();
}
function catSpark() {
  const s = spr(), M = R.gold, pts = [];
  for (let k = 0; k < 8; k++) { const a = -Math.PI / 2 + k * Math.PI / 4, r = k % 2 ? 2.6 : 10; pts.push([11 + Math.cos(a) * r, 13 + Math.sin(a) * r]); }
  s.poly(pts, (x, y) => (Math.hypot(x - 11, y - 13) < 3 ? '#ffffff' : x + y < 24 ? M[4] : R.orange[3]));
  sparkle(s, 19, 5, M[4]); s.px(21, 19, R.orange[3]); s.px(3, 4, M[3]); s.px(4, 21, R.orange[2]);
  return s.outline();
}
function catMirror() {
  const s = spr();
  // a standing looking-glass leaning to one side, with a crack across it
  const frame = [[5, 4], [15, 1], [20, 5], [18, 20], [8, 22], [3, 17]], glass = [[7, 6], [14, 4], [17, 7], [16, 18], [9, 19], [6, 15]];
  s.poly(frame, (x, y) => R.gold[x + y < 16 ? 4 : x + y < 26 ? 3 : 1]);
  s.poly(glass, (x, y) => { const k = (x * 0.9 + y) % 8; return k < 1.5 ? '#ffffff' : k < 4.5 ? GLASS[3] : GLASS[2]; });
  for (const [a, b, c, d] of [[9, 8, 12, 11], [12, 11, 10, 14], [10, 14, 14, 17], [12, 11, 15, 10]]) s.line(a, b, c, d, GLASS[0]);
  s.px(4, 10, R.gold[4]); s.px(19, 12, R.gold[1]); s.rect(9, 22, 8, 1, R.wood[1]);
  return s.outline();
}
function catHourglass() {
  const s = spr(), M = R.gold;
  s.rect(5, 2, 14, 3, (x, y) => M[y === 2 ? 4 : x < 9 ? 3 : 1]); s.rect(5, 19, 14, 3, (x, y) => M[y === 19 ? 3 : x < 9 ? 2 : 1]);
  s.poly([[6, 5], [18, 5], [13, 12], [18, 19], [6, 19], [11, 12]], (x, y) => (y < 9 ? HOLLOW : y < 12.5 ? R.gold[3] : y < 15 ? (Math.abs(x - 12) < 0.8 ? R.gold[3] : HOLLOW) : R.gold[x < 12 ? 4 : 2]));
  s.line(6, 5, 11, 12, GLASS[3]); s.line(11, 12, 6, 19, GLASS[3]); s.line(17, 5, 12, 12, GLASS[1]); s.line(12, 12, 17, 19, GLASS[1]);
  s.px(8, 7, '#ffffff'); s.px(8, 8, '#ffffff'); s.rect(4, 5, 1, 14, R.wood[2]); s.rect(19, 5, 1, 14, R.wood[1]);
  return s.outline();
}
function catSatchel() {
  const s = spr(), C = R.wood;
  s.tube((t) => [5 + t * 14, 8 - Math.sin(t * Math.PI) * 6], () => 1.1, (t, nx, ny) => C[ny < 0 ? 3 : 1], 24);
  s.ball(12, 15, 9.5, 7, [C[0], C[1], C[2], C[3], C[4]]); s.rect(3, 20, 18, 2, C[0]);
  s.poly([[3, 9], [21, 9], [19, 15], [12, 17], [5, 15]], (x, y) => C[y < 11 ? 4 : x < 9 ? 3 : 2]); s.line(5, 15, 12, 17, C[0]); s.line(12, 17, 19, 15, C[0]);
  s.rect(10, 15, 4, 4, (x, y) => R.gold[y === 15 ? 4 : x === 13 ? 1 : 3]); s.px(11, 17, INK); s.px(12, 17, INK); s.px(5, 10, '#ffffff');
  return s.outline();
}
function catLens() {
  const s = spr();
  s.tube((t) => [14 + t * 7, 14 + t * 7], () => 1.8, (t, nx) => R.wood[nx < 0 ? 4 : 1], 14);
  s.ell(10, 10, 8.6, 8.6, (nx, ny) => { const d = Math.hypot(nx, ny); return d > 0.76 ? R.gold[nx + ny < -0.3 ? 4 : nx + ny < 0.5 ? 2 : 1] : nx + ny < -0.5 ? '#ffffff' : nx + ny < 0.2 ? R.blue[4] : R.blue[3]; });
  sparkle(s, 11, 10, R.gold[4]); s.px(6, 7, '#ffffff'); s.px(7, 6, '#ffffff'); s.px(13, 13, R.blue[2]);
  return s.outline();
}
function catSlab() {
  const s = spr(), D = R.stone, V = R.violet;
  s.ell(12, 15, 10.5, 6, (nx, ny) => D[ny > 0.5 ? 1 : 2]); s.ell(12, 12.5, 10.5, 6, (nx, ny) => { const d = Math.hypot(nx, ny); return Math.abs(d - 0.78) < 0.09 || Math.abs(d - 0.42) < 0.1 ? V[d < 0.6 ? 4 : 3] : D[nx + ny < -0.3 ? 4 : 3]; });
  s.px(12, 12, '#ffffff'); s.px(11, 12, V[4]); s.px(13, 12, V[4]); s.px(12, 5, V[3]); s.px(4, 6, V[4]); s.px(20, 7, V[3]); s.px(12, 3, V[4]);
  return s.outline();
}
function catMoon() {
  const s = spr(), B = ['#3a4a7a', '#7088b8', '#b8cce8', '#e8f4ff', '#ffffff'];
  s.ell(11, 12, 8.6, 8.6, (nx, ny, x, y) => (Math.hypot(x - 15, y - 9.5) < 7.4 ? null : B[-nx * 0.5 - ny * 0.6 > 0.35 ? 4 : -nx * 0.5 - ny * 0.6 > -0.1 ? 3 : 2]));
  s.px(6, 15, B[1]); s.px(8, 18, B[1]); sparkle(s, 18, 7, '#ffffff'); s.px(20, 14, B[3]); s.px(16, 17, B[2]); s.px(21, 3, B[3]);
  return s.outline();
}

/** A round pearl lit from the upper left. */
function pearl(P, glint = '#ffffff') {
  const s = spr();
  s.ell(12, 13, 7.5, 7.5, (nx, ny) => { const l = -nx * 0.6 - ny * 0.6; return P[l > 0.55 ? 4 : l > 0.15 ? 3 : l > -0.35 ? 2 : 1]; });
  s.px(9, 10, glint); s.px(10, 9, glint); s.px(15, 16, P[3]);
  return s.outline();
}
const voidDark = ['#0c0a14', '#22103e', '#4a1f7a', '#8a3fc8', '#f4dcff'];

const pale = ['#3a4a6a', '#8fa8d0', '#c8d8f0', '#e8f4ff', '#ffffff'];
/** Material id (as in content.json) => builder returning a 24x24 Spr. */
const ICONS = {
  ember_coal: emberCoal, iron_ore: () => ore(R.stone, R.iron, R.orange[2]), starmetal_ore: () => ore(R.dark, R.blue, '#ffffff'),
  gold_ingot: () => ingot(R.gold), sky_iron: () => ingot(R.iron, R.blue[3]), quicksilver, sun_amalgam: sunAmalgam, living_gold: livingGold,
  humstone: () => crystals(R.violet), frost_salt: () => cubes(R.blue), cinnabar, storm_glass: stormGlass, star_shard: starShard, seer_s_orb: seerOrb, worldseed,
  sulfur: () => bowlPowder(R.gold), moondust: () => pouch(R.blue, R.wood), bone_ash: () => ashPile(R.ash), aurora_dust: auroraDust,
  heaven_s_ash: () => { const s = ashPile(pale, false); sparkle(s, 12, 8, '#ffffff'); s.px(6, 12, '#ffffff'); s.px(18, 13, '#e8f4ff'); s.px(4, 16, '#c8d8f0'); return s; },
  deep_tear: () => potionRound(R.teal), dragon_blood: () => potionTall(R.red), verdant_tonic: () => potionFlask(R.green), lifewater,
  nightshade_ink: nightshadeInk, red_tincture: () => potionFlask(R.red), everflame, azoth,
  glowmoss: moss, moonpetal: () => flowerRound(R.blue, R.gold), sunbloom: () => flowerSun(R.orange, R.wood), dusk_cap: () => mushroom(R.violet, R.teal[4]), mandrake,
  phoenix_feather: feather, dragon_scale: () => scale(R.teal), wyrm_fang: fang, basilisk_eye: eye, unicorn_horn: horn, pixie_wing: wing, steam_wisp: steamWisp,
  // Phase 2: combo materials
  hearth_glass: () => cubes(R.orange), wyrm_ichor: () => potionTall(R.green), forge_slag: () => ore(R.dark, R.orange, R.red[3]),
  primal_bloom: () => flowerRound(R.pink, R.gold), thunder_ingot: () => ingot(R.blue, '#ffffff'), witch_salve: () => potionRound(R.violet),
  chimera_hide: () => scale(R.orange), void_pearl: () => pearl(voidDark, '#f4dcff'), titan_alloy: () => ingot(R.stone, R.orange[3]),
  dream_spore: () => mushroom(R.pink, R.blue[4]), philosopher_s_salt: () => bowlPowder(R.red), chrysopoeia: () => ingot(R.gold, R.red[3]),
};
/** Catalyst id => builder. */
const CATALYST_ICONS = {
  bellows: catBellows,
  bestiary: () => catBook(R.red, (s) => { for (const x of [10, 12, 14]) s.line(x, 9, x + 1, 15, R.gold[4]); }),
  heart_tile: catHeartTile, first_spark: catSpark, crooked_mirror: catMirror, hourglass: catHourglass, deep_satchel: catSatchel,
  eureka_lens: catLens, resonant_slab: catSlab, quiet_bench: catMoon,
};
const KIND_COL = { Ore: '#c08a5c', Metal: '#a8b8c4', Crystal: '#c486f4', Powder: '#e4d2aa', Potion: '#6ee6c8', Plant: '#8ad04a', Beast: '#ee6a3c' };

export { ICONS, CATALYST_ICONS, KIND_COL, potionRound, potionTall, potionFlask, crystals, horn, eye, sparkle, spr, lobe };
