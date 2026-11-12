// Lit pixel stage + the bench scene, the reaction cutscene and the material gallery.
const W = 640, H = 360;
const BAY = [0, 8, 2, 10, 12, 4, 14, 6, 3, 11, 1, 9, 15, 7, 13, 5].map((v) => (v + 0.5) / 16);
const f3 = (h, k = 1) => rgbOf(h).map((v) => (v / 255) * k);
const FROZEN = (location.hash.match(/t=([\d.]+)/) || [])[1];

class Stage {
  constructor(host) {
    const mk = (cls) => { const c = document.createElement('canvas'); c.width = W; c.height = H; if (cls) c.className = cls; host.appendChild(c); return c; };
    this.main = mk().getContext('2d'); this.bloom = mk('bloom').getContext('2d'); this.ui = mk().getContext('2d');
    this.ui.imageSmoothingEnabled = false;
    this.img = new ImageData(W, H); this.bimg = new ImageData(W, H);
    this.alb = new Uint8ClampedArray(W * H * 4);
    this.stat = new Float32Array(W * H * 3); this.dyn = new Float32Array(W * H * 3); this.emit = new Float32Array(W * H * 3);
  }
  setBase(cv) { this.base = cv.getContext('2d').getImageData(0, 0, W, H).data; }
  begin() { this.alb.set(this.base); this.dyn.fill(0); this.emit.fill(0); this.ui.clearRect(0, 0, W, H); }
  px(x, y, hex) { x |= 0; y |= 0; if (x < 0 || y < 0 || x >= W || y >= H) return; const c = rgbOf(hex), i = (y * W + x) * 4; this.alb[i] = c[0]; this.alb[i + 1] = c[1]; this.alb[i + 2] = c[2]; }
  epx(x, y, c, k = 1) { x |= 0; y |= 0; if (x < 0 || y < 0 || x >= W || y >= H) return; const i = (y * W + x) * 3; this.emit[i] += c[0] * k; this.emit[i + 1] += c[1] * k; this.emit[i + 2] += c[2] * k; }
  // draw a Spr: o.k integer scale, o.emit adds its own colour as light, o.a dithered alpha
  blit(s, x, y, o = {}) {
    if (!s.rgb) s.rgb = s.d.map((c) => (c ? rgbOf(c) : null));
    const k = o.k || 1, em = o.emit || 0, a = o.a === undefined ? 1 : o.a; x = Math.round(x); y = Math.round(y);
    for (let sy = 0; sy < s.h; sy++) for (let sx = 0; sx < s.w; sx++) {
      const c = s.rgb[sy * s.w + sx]; if (!c) continue;
      for (let qy = 0; qy < k; qy++) for (let qx = 0; qx < k; qx++) {
        const X = x + sx * k + qx, Y = y + sy * k + qy;
        if (X < 0 || Y < 0 || X >= W || Y >= H) continue;
        if (a < 1 && a <= BAY[((Y & 3) << 2) | (X & 3)]) continue;
        const i = (Y * W + X) * 4; this.alb[i] = c[0]; this.alb[i + 1] = c[1]; this.alb[i + 2] = c[2];
        if (em) { const j = (Y * W + X) * 3; this.emit[j] += c[0] / 255 * em; this.emit[j + 1] += c[1] / 255 * em; this.emit[j + 2] += c[2] / 255 * em; }
      }
    }
  }
  shadow(cx, cy, rx, ry, k) {
    for (let y = Math.floor(cy - ry); y <= cy + ry; y++) for (let x = Math.floor(cx - rx); x <= cx + rx; x++) {
      if (x < 0 || y < 0 || x >= W || y >= H || ((x - cx) / rx) ** 2 + ((y - cy) / ry) ** 2 > 1) continue;
      const i = (y * W + x) * 4; this.alb[i] *= k; this.alb[i + 1] *= k; this.alb[i + 2] *= k;
    }
  }
  _add(buf, x, y, r, c, k, sy) {
    const x0 = Math.max(0, Math.floor(x - r)), x1 = Math.min(W - 1, Math.ceil(x + r)), ry = r / sy;
    const y0 = Math.max(0, Math.floor(y - ry)), y1 = Math.min(H - 1, Math.ceil(y + ry));
    for (let Y = y0; Y <= y1; Y++) { const dy = (Y + 0.5 - y) * sy; for (let X = x0; X <= x1; X++) {
      const dx = X + 0.5 - x, d = Math.sqrt(dx * dx + dy * dy) / r; if (d >= 1) continue;
      const f = (1 - d) * (1 - d) * k, i = (Y * W + X) * 3; buf[i] += c[0] * f; buf[i + 1] += c[1] * f; buf[i + 2] += c[2] * f;
    } }
  }
  light(x, y, r, c, k = 1, sy = 1) { this._add(this.dyn, x, y, r, c, k, sy); }
  glow(x, y, r, c, k = 1, sy = 1) { this._add(this.emit, x, y, r, c, k, sy); }
  end(o = {}) {
    const out = this.img.data, bo = this.bimg.data, A = this.alb, S = this.stat, D = this.dyn, E = this.emit, LV = 9, fl = (o.flash || 0) * 255;
    for (let y = 0, i = 0, j = 0; y < H; y++) for (let x = 0; x < W; x++, i += 4, j += 3) {
      const th = BAY[((y & 3) << 2) | (x & 3)];
      const lr = Math.min(1.7, Math.floor((S[j] + D[j]) * LV + th) / LV), lg = Math.min(1.7, Math.floor((S[j + 1] + D[j + 1]) * LV + th) / LV), lb = Math.min(1.7, Math.floor((S[j + 2] + D[j + 2]) * LV + th) / LV);
      const er = E[j] * 255, eg = E[j + 1] * 255, eb = E[j + 2] * 255;
      out[i] = A[i] * lr + er + fl; out[i + 1] = A[i + 1] * lg + eg + fl; out[i + 2] = A[i + 2] * lb + eb + fl; out[i + 3] = 255;
      bo[i] = er + fl * 0.6; bo[i + 1] = eg + fl * 0.6; bo[i + 2] = eb + fl * 0.6; bo[i + 3] = 255;
    }
    this.main.fillStyle = '#000'; this.main.fillRect(0, 0, W, H);
    this.main.putImageData(this.img, o.sx || 0, o.sy || 0); this.bloom.putImageData(this.bimg, o.sx || 0, o.sy || 0);
  }
}

/* ---------------- crisp text ---------------- */
const FONTSETS = {
  A: { title: '16px Alagard', big: '32px Alagard', huge: '48px Alagard', num: '32px Alagard', body: '16px Romulus', tiny: '8px PixOp8' },
  B: { title: '16px Alagard', big: '32px Alagard', huge: '48px Alagard', num: '32px Alagard', body: '16px PixOpB', tiny: '8px PixOp8' },
  C: { title: '16px Alagard', big: '32px Alagard', huge: '48px Alagard', num: '32px Alagard', body: '16px BitPotion', tiny: '8px PixOp8' },
};
const F = { ...FONTSETS[(location.hash.match(/set=(\w)/) || [0, 'A'])[1]] };
const tc = (s) => s.toLowerCase().replace(/(^|\s)\w/g, (c) => c.toUpperCase());
const tcache = new Map();
function crisp(str, font, col) {
  const key = str + '|' + font + '|' + col; let t = tcache.get(key); if (t) return t;
  t = document.createElement('canvas'); const g = t.getContext('2d'); g.font = font;
  const w = Math.ceil(g.measureText(str).width) + 2, h = parseInt(font.match(/(\d+)px/)[1]) + 8;
  t.width = w; t.height = h; g.font = font; g.textBaseline = 'top'; g.fillStyle = '#fff'; g.fillText(str, 1, 1);
  const im = g.getImageData(0, 0, w, h), c = rgbOf(col);
  for (let i = 0; i < im.data.length; i += 4) { const on = im.data[i + 3] > 110; im.data[i] = c[0]; im.data[i + 1] = c[1]; im.data[i + 2] = c[2]; im.data[i + 3] = on ? 255 : 0; }
  g.putImageData(im, 0, 0); tcache.set(key, t); return t;
}
function text(g, str, x, y, font, col, o = {}) {
  const t = crisp(str, font, col), X = Math.round(o.align === 'center' ? x - t.width / 2 : o.align === 'right' ? x - t.width : x);
  if (o.shadow) { const s = crisp(str, font, o.shadow); g.drawImage(s, X + 1, y + 1); g.drawImage(s, X, y + 1); }
  g.drawImage(t, X, y); return t.width;
}

/* ---------------- UI pieces ---------------- */
function panel(g, x, y, w, h) {
  g.fillStyle = '#0c0706'; g.fillRect(x, y, w, h);
  g.fillStyle = '#2c1a14'; g.fillRect(x + 1, y + 1, w - 2, h - 2);
  for (let yy = y + 2; yy < y + h - 2; yy++) for (let xx = x + 2; xx < x + w - 2; xx++) { const n = hsh(xx >> 2, yy, 41); if (n > 0.86) { g.fillStyle = '#3a241a'; g.fillRect(xx, yy, 1, 1); } else if (n < 0.1) { g.fillStyle = '#22130f'; g.fillRect(xx, yy, 1, 1); } }
  g.fillStyle = '#6a4428'; g.fillRect(x + 1, y + 1, w - 2, 1); g.fillRect(x + 1, y + 1, 1, h - 2);
  g.fillStyle = '#170c0a'; g.fillRect(x + 1, y + h - 2, w - 2, 1); g.fillRect(x + w - 2, y + 1, 1, h - 2);
  for (const [cx, cy] of [[x + 2, y + 2], [x + w - 6, y + 2], [x + 2, y + h - 6], [x + w - 6, y + h - 6]]) {
    g.fillStyle = R.gold[1]; g.fillRect(cx, cy, 4, 4); g.fillStyle = R.gold[3]; g.fillRect(cx, cy, 3, 3); g.fillStyle = R.gold[4]; g.fillRect(cx, cy, 1, 1); g.fillStyle = R.gold[2]; g.fillRect(cx + 1, cy + 1, 2, 2);
  }
}
function parchment(g, x, y, w, h) {
  g.fillStyle = '#1a0e0a'; g.fillRect(x + 1, y + 2, w, h);
  for (let yy = 0; yy < h; yy++) for (let xx = 0; xx < w; xx++) {
    if ((xx < 2 || xx >= w - 2) && hsh(xx, yy >> 1, 7) > 0.6) continue;
    const n = hsh(xx, yy, 12), edge = Math.min(xx, yy, w - 1 - xx, h - 1 - yy);
    g.fillStyle = edge < 2 ? '#a8854e' : n > 0.9 ? '#e8d4a0' : n < 0.08 ? '#c4a870' : '#d8c088'; g.fillRect(x + xx, y + yy, 1, 1);
  }
}
function socket(g, x, y, filled) {
  g.fillStyle = '#0c0706'; g.fillRect(x, y, 30, 30);
  g.fillStyle = filled ? R.gold[2] : '#4a3020'; g.fillRect(x + 1, y + 1, 28, 28);
  g.fillStyle = filled ? R.gold[4] : '#6a4428'; g.fillRect(x + 1, y + 1, 28, 1); g.fillRect(x + 1, y + 1, 1, 28);
  g.fillStyle = filled ? R.gold[1] : '#24140e'; g.fillRect(x + 1, y + 28, 28, 1); g.fillRect(x + 28, y + 1, 1, 28);
  g.fillStyle = '#160e14'; g.fillRect(x + 3, y + 3, 24, 24); g.fillStyle = '#241828'; g.fillRect(x + 4, y + 4, 22, 22);
}
const MAT = {}; let ICON = {};
function wrap2(name) { const p = name.split(' '); return p.length > 1 ? [p[0], p.slice(1).join(' ')] : [name]; }
const CW = 74;
function card(g, m, x, y, sel) {
  const mid = x + CW / 2;
  g.fillStyle = 'rgba(0,0,0,.55)'; g.fillRect(x + 3, y + 4, CW, 82);
  g.fillStyle = '#0c0706'; g.fillRect(x, y, CW, 82);
  g.fillStyle = sel ? R.gold[3] : '#7a5430'; g.fillRect(x + 1, y + 1, CW - 2, 80);
  g.fillStyle = sel ? R.gold[4] : '#a87a48'; g.fillRect(x + 1, y + 1, CW - 2, 1); g.fillRect(x + 1, y + 1, 1, 80);
  g.fillStyle = sel ? R.gold[1] : '#4a2e18'; g.fillRect(x + 1, y + 80, CW - 2, 1); g.fillRect(x + CW - 2, y + 1, 1, 80);
  for (let yy = 3; yy < 79; yy++) for (let xx = 3; xx < CW - 3; xx++) { const n = hsh(xx, yy, 31); g.fillStyle = n > 0.9 ? '#ecd8a6' : n < 0.08 ? '#c8ac74' : '#dcc48c'; g.fillRect(x + xx, y + yy, 1, 1); }
  g.fillStyle = '#3a2416'; g.fillRect(x + 6, y + 6, CW - 12, 36); g.fillStyle = '#1a1220'; g.fillRect(x + 7, y + 7, CW - 14, 34);
  g.fillStyle = '#2a1e34'; g.fillRect(x + 8, y + 8, CW - 16, 16); g.fillStyle = '#221830'; g.fillRect(x + 8, y + 24, CW - 16, 16);
  if (m.glow) { const c = rgbOf(m.glow); for (let r = 14; r > 0; r -= 3) { g.fillStyle = `rgba(${c[0]},${c[1]},${c[2]},.13)`; g.fillRect(mid - r, y + 24 - Math.round(r * 0.8), r * 2, Math.round(r * 1.6)); } }
  g.drawImage(ICON[m.name], mid - 12, y + 12);
  const lines = wrap2(tc(m.name));
  lines.forEach((l, i) => text(g, l, mid, y + (lines.length > 1 ? 41 : 47) + i * 12, '16px PixOpB', '#3a2212', { align: 'center' }));
  g.fillStyle = '#3a2416'; g.fillRect(mid - 27, y + 69, 54, 9);
  text(g, m.kind, mid, y + 68, F.tiny, KIND_COL[m.kind], { align: 'center' });
}

/* ---------------- shared scenery ---------------- */
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
function flame(st, x, y, t, ph, big) {
  const hgt = big ? 16 : 9, fl = 0.85 + 0.15 * Math.sin(t * 11 + ph) * Math.sin(t * 5.3 + ph * 2);
  for (let i = 0; i < hgt; i++) {
    const u = i / (hgt - 1), wv = Math.sin(u * Math.PI * 0.9 + 0.25) * (big ? 5 : 2.6) * (1 - u * 0.35), sway = Math.sin(t * 6 + ph + u * 2.5) * u * (big ? 3 : 1.6);
    for (let dx = -Math.ceil(wv); dx <= Math.ceil(wv); dx++) {
      const a = Math.abs(dx) / (wv + 0.01); if (a > 1) continue;
      const X = Math.round(x + dx + sway), Y = y - i, c = a < 0.35 && u < 0.6 ? '#ffffff' : a < 0.7 && u < 0.8 ? R.gold[4] : u < 0.2 ? R.blue[3] : R.orange[3];
      st.px(X, Y, c); st.epx(X, Y, f3(c), 0.9);
    }
  }
  st.glow(x, y - hgt * 0.5, big ? 60 : 30, f3('#ff9a3a'), 0.75 * fl);
  return fl;
}
const candleSpr = (() => { const s = new Spr(12, 20), X = ['#5e4436', '#8e6e58', '#b8957a', '#d8b896', '#f0dcc0'];
  s.rect(3, 4, 6, 13, (x) => X[[1, 4, 4, 3, 2, 1][x - 3]]); s.rect(3, 4, 6, 1, X[4]); s.px(8, 6, X[4]); s.px(8, 7, X[3]); s.px(8, 8, X[3]); s.px(3, 9, X[4]);
  s.px(6, 3, INK); s.px(6, 2, INK);
  s.ell(6, 17.5, 6, 2.2, (nx, ny) => R.gold[ny < -0.2 ? 3 : nx < 0 ? 2 : 1]); s.rect(2, 19, 8, 1, R.gold[1]);
  return s.outline(); })();

/* ================= SCREEN 1: THE BENCH ================= */
const proj = (u, v) => { const half = 98 + 34 * v; return [320 + u * half, 146 + 94 * v]; };
function tileRect(i) { const c = i % 3, r = (i / 3) | 0; return { u0: -1 + c * (2 / 3) + 0.03, u1: -1 + (c + 1) * (2 / 3) - 0.03, v0: r / 3 + 0.035, v1: (r + 1) / 3 - 0.035 }; }
function tileCenter(i) { const q = tileRect(i), p = proj((q.u0 + q.u1) / 2, (q.v0 + q.v1) / 2); return { x: p[0], y: p[1] - 5, w: (98 + 34 * (q.v0 + q.v1) / 2) * (q.u1 - q.u0) }; }
const GRID = [null, 'MOONDUST', null, 'EMBER COAL', 'DEEP TEAR', null, null, 'GLOWMOSS', 'BASILISK EYE'];
const LINKS = [[3, 4], [1, 4], [4, 7], [7, 8]];

function benchBase() {
  const cv = document.createElement('canvas'); cv.width = W; cv.height = H; const g = cv.getContext('2d');
  stoneWall(g, 0, 136, 1);
  // arched window
  for (let y = 8; y <= 104; y++) for (let x = 268; x <= 372; x++) {
    const dx = x + 0.5 - 320, d = y < 56 ? Math.hypot(dx, y + 0.5 - 56) : Math.abs(dx);
    if (d > 47) continue;
    if (d > 40) { g.fillStyle = d > 45 ? '#2a2532' : (dx < 0 ? '#7a7284' : '#5c5464'); g.fillRect(x, y, 1, 1); continue; }
    const band = (y - 12) / 92 + (hsh(x, y, 2) - 0.5) * 0.12;
    g.fillStyle = band < 0.3 ? '#0c1030' : band < 0.55 ? '#141a4a' : band < 0.8 ? '#1c2a66' : '#27408c'; g.fillRect(x, y, 1, 1);
    if (hsh(x, y, 9) > 0.992) { g.fillStyle = '#e0f6ff'; g.fillRect(x, y, 1, 1); }
    const md = Math.hypot(x + 0.5 - 336, y + 0.5 - 42);
    if (md < 12) { g.fillStyle = md > 10.5 ? '#a8c8e8' : Math.hypot(x - 332, y - 38) < 3 || Math.hypot(x - 340, y - 47) < 2.2 ? '#bcd8ee' : '#eefaff'; g.fillRect(x, y, 1, 1); }
    if ((x + y) % 14 === 0 || (x - y + 1400) % 14 === 0 || Math.abs(dx) < 1) { g.fillStyle = '#120e18'; g.fillRect(x, y, 1, 1); }
  }
  g.fillStyle = '#7a7284'; g.fillRect(262, 104, 116, 3); g.fillStyle = '#4b4350'; g.fillRect(262, 107, 116, 4); g.fillStyle = '#231e2b'; g.fillRect(264, 111, 112, 2);
  // shelves and their clutter
  const shelf = (x, y, w) => {
    g.fillStyle = R.wood[3]; g.fillRect(x, y, w, 1); g.fillStyle = R.wood[2]; g.fillRect(x, y + 1, w, 3); g.fillStyle = R.wood[0]; g.fillRect(x, y + 4, w, 2);
    g.fillStyle = '#120e16'; g.fillRect(x + 2, y + 6, w - 4, 3);
    for (const bx of [x + 8, x + w - 12]) { g.fillStyle = R.wood[1]; g.fillRect(bx, y + 4, 4, 9); g.fillStyle = R.wood[0]; g.fillRect(bx + 3, y + 4, 1, 9); }
  };
  const books = (x, y, n, seed) => { for (let i = 0; i < n; i++) {
    const w = 5 + Math.floor(hsh(i, seed, 1) * 3), h = 15 + Math.floor(hsh(i, seed, 2) * 7), K = [R.red, R.blue, R.green, R.violet, R.wood, R.teal][Math.floor(hsh(i, seed, 3) * 6)];
    g.fillStyle = INK; g.fillRect(x - 1, y - h - 1, w + 2, h + 1); g.fillStyle = K[1]; g.fillRect(x, y - h, w, h); g.fillStyle = K[2]; g.fillRect(x, y - h, 1, h); g.fillStyle = K[0]; g.fillRect(x + w - 1, y - h, 1, h);
    g.fillStyle = R.gold[3]; g.fillRect(x + 1, y - h + 3, w - 2, 1); g.fillRect(x + 1, y - 5, w - 2, 1); x += w + 1; } return x; };
  const put = (s, x, y) => g.drawImage(s.canvas(), x, y - 24);
  shelf(14, 122, 150); shelf(476, 122, 150);
  for (const bx of [208, 420]) { g.fillStyle = INK; g.fillRect(bx - 1, 111, 14, 5); g.fillStyle = R.iron[1]; g.fillRect(bx, 112, 12, 3); g.fillStyle = R.iron[2]; g.fillRect(bx, 112, 12, 1); g.fillStyle = R.iron[0]; g.fillRect(bx + 5, 115, 2, 8); }
  let x = books(22, 122, 5, 1); put(potionRound(R.violet), x + 2, 123); put(potionTall(R.gold), x + 26, 123); put(crystals(R.teal), x + 48, 123); put(potionFlask(R.pink), x + 74, 123);
  put(potionFlask(R.blue), 482, 123); put(horn(), 506, 123); x = books(536, 122, 4, 3); put(potionTall(R.teal), x + 2, 123); put(eye(), x + 24, 123);
  // floor
  g.fillStyle = '#0d0a12'; g.fillRect(0, 136, W, H - 136);
  for (let y = 136; y < H; y += 2) for (let x = 0; x < W; x += 2) if (hsh(x, y, 77) > 0.9) { g.fillStyle = '#17131e'; g.fillRect(x, y, 2, 1); }
  // table top, in perspective
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
  // apron
  for (let y = 257; y <= 276; y++) for (let x = 102; x < 538; x++) {
    let c = y < 259 ? R.wood[3] : y > 273 ? '#1a0c08' : '#3e2012';
    if (y > 261 && y < 272) { const k = Math.abs(((x - 102) % 24) - 12) + Math.abs(y - 266.5); if (k < 4) c = '#2a140a'; else if (k < 5) c = '#5a3018'; }
    g.fillStyle = c; g.fillRect(x, y, 1, 1);
  }
  // transmutation slab: brass rim, stone body
  const quad = (u0, v0, u1, v1, lift, col) => { const yA = Math.round(proj(0, v0)[1]), yB = Math.round(proj(0, v1)[1]);
    for (let y = yA; y < yB; y++) { const v = (y + 0.5 - 146) / 94, a = proj(u0, v)[0], b = proj(u1, v)[0];
      for (let x = Math.round(a); x < Math.round(b); x++) { const c = col(x, y, (x - a) / (b - a), (y - yA) / (yB - yA)); if (c) { g.fillStyle = c; g.fillRect(x, y - lift, 1, 1); } } } };
  quad(-1.12, -0.09, 1.12, 1.09, -4, () => '#160d0a');
  quad(-1.12, -0.09, 1.12, 1.09, 0, (x, y, a, b) => (a < 0.012 || b < 0.03 ? R.gold[3] : a > 0.988 || b > 0.965 ? R.gold[1] : (a < 0.03 || a > 0.97 || b < 0.06 || b > 0.935) ? R.gold[2] : hsh(x, y, 5) > 0.85 ? '#2e2a3a' : '#25212f'));
  for (const [u, v] of [[-1.06, -0.04], [1.06, -0.04], [-1.06, 1.04], [1.06, 1.04]]) { const p = proj(u, v); g.fillStyle = R.gold[4]; g.fillRect(Math.round(p[0]) - 2, Math.round(p[1]) - 2, 4, 4); g.fillStyle = R.gold[1]; g.fillRect(Math.round(p[0]) - 1, Math.round(p[1]) - 1, 3, 3); g.fillStyle = R.gold[3]; g.fillRect(Math.round(p[0]) - 1, Math.round(p[1]) - 1, 2, 2); }
  for (let i = 0; i < 9; i++) {
    const q = tileRect(i), c = tileCenter(i), rx = c.w * 0.3, ry = rx * 0.42;
    quad(q.u0, q.v1 - 0.01, q.u1, q.v1 + 0.062, 0, (x, y, a, b) => (b < 0.2 ? '#565064' : '#2a2634'));
    quad(q.u0, q.v0, q.u1, q.v1, 5, (x, y, a, b) => {
      const e = Math.hypot((x + 0.5 - c.x) / rx, (y - 5 + 0.5 - c.y) / ry), n = hsh(x, y, 21 + i);
      if (Math.abs(e - 1) < 0.1) return '#3c384a';
      if (Math.abs(e - 0.62) < 0.07 && ((Math.atan2(y - 5 - c.y, x - c.x) * 6 / Math.PI + 12) | 0) % 2) return '#3c384a';
      if (b < 0.07 || a < 0.02) return '#a09aac';
      return n > 0.88 ? '#7a7488' : n < 0.1 ? '#5c566a' : '#6a6478';
    });
  }
  // props on the table
  const mort = new Spr(26, 22); mort.tube((t) => [18 - t * 9, 2 + t * 9], () => 1.7, (t, nx) => R.wood[nx < 0 ? 4 : 2], 20);
  mort.ball(12, 12, 10, 9, R.stone, { clip: (x, y) => y > 9.5, facet: 3 }); mort.ell(12, 10, 10, 2.6, (nx, ny) => R.stone[ny < 0 ? 0 : 3]); mort.rect(8, 20, 9, 1, R.stone[1]); mort.outline();
  g.drawImage(mort.canvas(), 462, 150);
  const bk = new Spr(40, 20); bk.rect(2, 12, 34, 6, (x, y) => (y < 13 ? R.red[2] : y > 16 ? R.red[0] : x < 5 ? R.gold[3] : R.red[1])); bk.rect(4, 6, 32, 6, (x, y) => (y < 7 ? R.blue[3] : y > 10 ? R.blue[0] : x > 32 ? R.bone[3] : R.blue[1]));
  bk.rect(1, 1, 30, 5, (x, y) => (y < 2 ? R.green[3] : y > 4 ? R.green[0] : x > 27 ? R.bone[3] : R.green[1])); bk.rect(8, 3, 14, 1, R.gold[3]); bk.outline();
  g.drawImage(bk.canvas(), 130, 196);
  return cv;
}

function benchStatic(st) {
  const S = st.stat;
  for (let y = 0, j = 0; y < H; y++) for (let x = 0; x < W; x++, j += 3) {
    const vg = 1 - 0.55 * Math.pow(Math.hypot((x - 320) / 320, (y - 190) / 190), 2.2);
    let r = 0.13 * vg, g = 0.13 * vg, b = 0.25 * vg;
    const t = (y - 20) / 250, cx = 320 - 10 * t, half = 34 + 70 * t;
    if (t > 0 && t < 1.05) { const f = Math.max(0, 1 - Math.abs(x - cx) / half), k = Math.pow(f, 0.7) * (1 - t * 0.45) * 0.55; r += 0.42 * k; g += 0.6 * k; b += 1.0 * k; }
    const dx = x - 320, wd = y < 56 ? Math.hypot(dx, y - 56) : y < 104 ? Math.abs(dx) : 99;
    if (wd < 40) { r += 0.8; g += 0.85; b += 0.9; }
    S[j] = r; S[j + 1] = g; S[j + 2] = b;
  }
}

function benchFrame(st, t) {
  st.begin();
  // candles on the table
  const cands = [[150, 232, 0.3], [498, 226, 1.9], [226, 136, 3.1], [208, 93, 4.4], [420, 93, 5.2]];
  for (const [x, y, ph] of cands) { st.shadow(x + 6, y + 19, 9, 3, 0.55); st.blit(candleSpr, x, y); const fl = flame(st, x + 6, y + 1, t, ph); st.light(x + 6, y - 2, y < 120 ? 170 : 210, [1.5, 0.95, 0.5], (y < 120 ? 0.8 : 1.05) * fl, 1.25); }
  // materials on tiles
  GRID.forEach((name, i) => {
    if (!name) return; const m = MAT[name], c = tileCenter(i), bob = Math.round(Math.sin(t * 2 + i * 1.7) * 1.5), gc = f3(m.glow || '#f0dcc0');
    const rx = c.w * 0.3, ry = rx * 0.42;
    for (let a = 0; a < 6.283; a += 0.035) { const X = c.x + Math.cos(a) * rx, Y = c.y + Math.sin(a) * ry; st.epx(X, Y, gc, 0.55 + 0.25 * Math.sin(t * 3 + i + a * 2)); }
    st.shadow(c.x, c.y + 3, 9, 3, 0.5);
    st.blit(m.spr, c.x - 12, c.y - 22 + bob, { emit: m.glow ? 0.16 : 0.08 });
    st.light(c.x, c.y - 6, 46, gc, m.glow ? 0.95 : 0.3, 1.3); st.glow(c.x, c.y - 8, 24, gc, m.glow ? 0.26 : 0.06);
  });
  // links between tiles that will react
  for (const [a, b] of LINKS) { const A = tileCenter(a), B = tileCenter(b);
    for (let k = 0; k < 9; k++) { const f = (k / 9 + t * 0.35) % 1, arc = Math.sin(f * Math.PI) * 7, X = A.x + (B.x - A.x) * f, Y = A.y - 9 + (B.y - A.y) * f - arc, s = Math.sin(f * Math.PI);
      st.epx(X, Y, [1, 0.9, 0.6], 0.9 * s); st.epx(X + 1, Y, [1, 0.7, 0.3], 0.5 * s); st.epx(X, Y + 1, [1, 0.7, 0.3], 0.5 * s); st.glow(X, Y, 6, [1, 0.75, 0.3], 0.35 * s); } }
  // the tile under the dragged card
  { const q = tileRect(5), pu = 0.55 + 0.45 * Math.sin(t * 5), n = 40;
    for (let k = 0; k <= n; k++) { const f = k / n; for (const [u, v] of [[q.u0 + (q.u1 - q.u0) * f, q.v0], [q.u0 + (q.u1 - q.u0) * f, q.v1], [q.u0, q.v0 + (q.v1 - q.v0) * f], [q.u1, q.v0 + (q.v1 - q.v0) * f]]) { const p = proj(u, v); if ((k + Math.floor(t * 8)) % 4 < 2) st.epx(p[0], p[1] - 5, [1, 0.85, 0.45], pu); } }
    const c = tileCenter(5); st.glow(c.x, c.y, 30, [1, 0.8, 0.4], 0.16 * pu); }
  // dust in the moonbeam
  for (let i = 0; i < 46; i++) { const ph = hsh(i, 3, 1), y = 30 + ((ph * 300 + t * (5 + (i % 5) * 2.5)) % 220), tt = (y - 20) / 250, x = 320 - 10 * tt + (hsh(i, 7, 2) - 0.5) * 2 * (30 + 60 * tt) + Math.sin(t * 0.7 + i) * 4;
    st.epx(x, y, [0.7, 0.85, 1], 0.25 + 0.3 * Math.sin(t * 2 + i * 2.1) ** 2); }
  st.end();

  /* ---- UI ---- */
  const g = st.ui;
  panel(g, 8, 8, 152, 76);
  text(g, 'Depth 3', 16, 8, F.big, '#f0dcc0', { shadow: '#0c0706' });
  text(g, 'The Humming Dark', 17, 35, F.body, '#a88a6a');
  text(g, 'POTENCY', 17, 51, F.tiny, '#a88a6a'); text(g, '1,240', 70, 49, F.title, R.gold[3], { shadow: '#3a1a08' }); text(g, '/ 3,000', 153, 51, F.tiny, '#a88a6a', { align: 'right' });
  g.fillStyle = '#0c0706'; g.fillRect(16, 68, 136, 9); g.fillStyle = R.gold[1]; g.fillRect(17, 69, 134, 7); g.fillStyle = '#1a0e14'; g.fillRect(18, 70, 132, 5);
  g.fillStyle = R.orange[2]; g.fillRect(18, 70, 55, 5); g.fillStyle = R.gold[3]; g.fillRect(18, 70, 55, 2); g.fillStyle = '#ffffff'; g.fillRect(72, 70, 1, 5);
  panel(g, 480, 8, 152, 76);
  text(g, 'CATALYSTS', 489, 13, F.tiny, '#a88a6a');
  ['UNICORN HORN', 'DRAGON SCALE', 'DUSK CAP', null].forEach((n, i) => { const x = 488 + i * 35; socket(g, x, 24, !!n); if (n) g.drawImage(ICON[n], x + 3, 27); else text(g, '+', x + 15, 31, F.body, '#4a3020', { align: 'center' }); });
  text(g, 'TRIALS', 489, 62, F.tiny, '#a88a6a'); text(g, '3', 534, 47, F.num, '#ee6a3c', { shadow: '#0c0706' });
  text(g, 'REDRAWS', 556, 62, F.tiny, '#a88a6a'); text(g, '2', 611, 47, F.num, '#6ee6c8', { shadow: '#0c0706' });
  parchment(g, 4, 262, 122, 90);
  text(g, 'LAST REACTION', 10, 265, F.tiny, '#7a5430');
  text(g, 'Ember Coal', 10, 274, F.body, '#3a2212'); text(g, '+ Deep Tear', 10, 285, F.body, '#3a2212');
  text(g, '= Steam Wisp', 10, 297, F.body, '#11606a'); text(g, '+120  x2', 10, 308, F.body, '#9a3414');
  g.fillStyle = '#b89860'; g.fillRect(9, 325, 112, 1);
  text(g, 'HINT', 10, 328, F.tiny, '#7a5430'); text(g, '??? + Glowmoss', 10, 336, F.body, '#4a1f7a');
  panel(g, 514, 284, 122, 68);
  { const b = new Spr(18, 14); b.rect(1, 1, 16, 12, (x, y) => (x < 3 ? R.red[0] : y < 2 ? R.red[3] : R.red[1])); b.rect(4, 12, 13, 1, R.bone[3]); b.rect(7, 4, 6, 5, R.gold[2]); b.rect(8, 5, 4, 3, R.gold[4]); b.outline(); g.drawImage(b.canvas(), 520, 290); }
  text(g, 'Journal', 542, 288, F.title, '#f0dcc0'); text(g, '14/40', 630, 293, F.tiny, '#a88a6a', { align: 'right' });
  const pu = 0.5 + 0.5 * Math.sin(t * 3);
  g.fillStyle = '#0c0706'; g.fillRect(519, 310, 112, 36); g.fillStyle = R.red[1]; g.fillRect(520, 311, 110, 34); g.fillStyle = R.red[2]; g.fillRect(521, 312, 108, 28);
  g.fillStyle = R.orange[3]; g.fillRect(521, 312, 108, 1); g.fillRect(521, 312, 1, 28); g.fillStyle = R.red[0]; g.fillRect(521, 340, 108, 4);
  g.fillStyle = `rgba(255,190,90,${0.1 + 0.2 * pu})`; g.fillRect(521, 312, 108, 28);
  text(g, 'Ignite', 575, 304, F.big, '#fff6b8', { align: 'center', shadow: '#3b0d1e' });
  ['DRAGON BLOOD', 'MOONPETAL', 'PHOENIX FEATHER', 'HUMSTONE', 'GOLD INGOT'].forEach((n, i) => {
    const sel = i === 2; card(g, MAT[n], 131 + i * 76, sel ? 258 + Math.round(Math.sin(t * 3) * 1.5) : 274, sel); });
}

/* ================= SCREEN 2: THE CUTSCENE ================= */
const CAUL = ['#0c0a14', '#1a1826', '#2e2c40', '#4e4c64', '#8280a0'];
const wisp = (() => { const s = spr(), K = ['#3a6a80', '#6aa8b8', '#a8dce0', '#e0fbf8', '#ffffff'];
  s.tube((t) => [14 + Math.sin(t * 4) * 3, 9 - t * 7], (t) => 2.6 * (1 - t) + 0.5, (t, nx) => K[nx < 0 ? 4 : 3], 20);
  s.ball(8, 16.5, 4.4, 3.8, K); s.ball(16.5, 16.5, 4.8, 3.8, K); s.ball(12, 13, 7, 6, K);
  s.px(10, 13, INK); s.px(14, 13, INK); s.px(10, 12, INK); s.px(14, 12, INK); s.px(11, 16, K[0]); s.px(12, 16, K[0]); s.px(13, 16, K[0]); s.px(9, 10, '#ffffff');
  return s.outline(0.55); })();
function cutBase() {
  const cv = document.createElement('canvas'); cv.width = W; cv.height = H; const g = cv.getContext('2d');
  stoneWall(g, 0, 232, 5, true);
  for (let y = 232; y < H; y++) { const t = (y - 232) / 128, rowH = 14 + t * 26, row = Math.floor(Math.log(1 + t * 3) * 4);
    for (let x = 0; x < W; x++) { const u = (x - 320) / (1 + t * 1.6), col = Math.floor(u / 44 + 100 + (row % 2) * 0.5), fr = (u / 44 + 100 + (row % 2) * 0.5) % 1, n = hsh(x, y, 3);
      g.fillStyle = fr < 0.03 || y === 232 ? '#141018' : n > 0.9 ? '#3e3848' : (col + row) % 2 ? '#2e2a38' : '#332e3e'; g.fillRect(x, y, 1, 1); } }
  // rune circle cut into the floor
  for (let a = 0; a < 6.283; a += 0.004) for (const [k, c] of [[1, '#4a4458'], [0.985, '#1a1620'], [0.8, '#1a1620'], [0.79, '#4a4458']]) { g.fillStyle = c; g.fillRect(Math.round(320 + Math.cos(a) * 200 * k), Math.round(292 + Math.sin(a) * 46 * k), 1, 1); }
  // logs
  const lg = new Spr(120, 22); lg.tube((t) => [8 + t * 104, 14 - t * 5], () => 5, (t, nx, ny) => R.wood[ny < -0.4 ? 3 : ny < 0.3 ? 2 : 0], 60); lg.tube((t) => [112 - t * 100, 15 - t * 6], () => 4.5, (t, nx, ny) => R.wood[ny < -0.4 ? 2 : ny < 0.3 ? 1 : 0], 60); lg.outline();
  g.drawImage(lg.canvas(), 260, 282);
  // cauldron
  const s = new Spr(200, 170);
  for (const lx of [44, 156]) s.tube((t) => [lx + (lx < 100 ? -10 : 10) * t, 130 + t * 34], (t) => 6 - t * 2, (t, nx) => CAUL[nx < -0.3 ? 3 : nx < 0.4 ? 2 : 1], 20);
  s.ball(100, 92, 84, 66, CAUL, { clip: (x, y) => y > 42 });
  s.ell(100, 44, 78, 17, (nx, ny) => (ny < 0 ? CAUL[1] : CAUL[4]));
  s.ell(100, 43, 70, 13, (nx, ny) => CAUL[0]);
  for (let a = 0.15; a < 3; a += 0.02) { const x = 100 + Math.cos(a) * 83, y = 98 + Math.sin(a) * 12; s.px(x, y, R.gold[a > 2 ? 3 : a > 1 ? 2 : 1]); s.px(x, y + 1, R.gold[a > 2 ? 2 : 1]); s.px(x, y + 2, R.gold[0]); }
  for (let a = 0.3; a < 2.9; a += 0.33) { const x = Math.round(100 + Math.cos(a) * 82), y = Math.round(99 + Math.sin(a) * 12); s.rect(x - 1, y - 1, 3, 3, R.gold[4]); s.px(x + 1, y + 1, R.gold[1]); }
  s.outline(); g.drawImage(s.canvas(), 220, 118);
  return cv;
}
function cutStatic(st) {
  const S = st.stat;
  for (let y = 0, j = 0; y < H; y++) for (let x = 0; x < W; x++, j += 3) { const vg = 1 - 0.6 * Math.pow(Math.hypot((x - 320) / 320, (y - 200) / 200), 2); S[j] = 0.11 * vg; S[j + 1] = 0.11 * vg; S[j + 2] = 0.2 * vg; }
}
function cutFrame(st, t) {
  const T = t % 7, CX = 320, RIM = 161, after = T > 2.3, gather = Math.min(1, T / 2);
  st.begin();
  const fa = f3('#ff6a1e'), fb = f3('#6ee6c8'), res = f3('#a8fff4');
  // fire under the cauldron
  let fl = 0; for (let i = 0; i < 7; i++) fl += flame(st, 272 + i * 16 + Math.sin(i * 3) * 3, 290 - (i % 2) * 3, t, i * 1.3, true);
  st.light(CX, 285, 250, [1.5, 0.85, 0.4], 0.95 * fl / 7, 1.6);
  // rune circle lighting up
  const rk = after ? 1 : gather * gather, rc = after ? res : [0.75, 0.5, 1];
  for (let a = 0; a < 6.283; a += 0.006) { const p = 0.6 + 0.4 * Math.sin(a * 9 - t * 4); st.epx(CX + Math.cos(a) * 198, 292 + Math.sin(a) * 45.5, rc, rk * p); st.epx(CX + Math.cos(a) * 159, 292 + Math.sin(a) * 36.5, rc, rk * p * 0.8); }
  for (let k = 0; k < 12; k++) { const a = k * Math.PI / 6 + t * 0.3, x = CX + Math.cos(a) * 179, y = 292 + Math.sin(a) * 41; st.glow(x, y, 9, rc, rk * 0.6); st.epx(x, y, [1, 1, 1], rk); st.epx(x + 1, y, rc, rk); st.epx(x - 1, y, rc, rk); st.epx(x, y - 1, rc, rk); }
  st.light(CX, 292, 230, rc, rk * 0.5, 2.6);
  // the liquid
  const lc = after ? res : [0.35 + 0.4 * gather, 0.15 + 0.25 * gather, 0.6];
  for (let y = -12; y <= 12; y++) for (let x = -69; x <= 69; x++) { if ((x / 69) ** 2 + (y / 12) ** 2 > 1) continue;
    const w = Math.sin(x * 0.18 + t * 3 + y * 0.5) * Math.sin(x * 0.07 - t * 1.7), k = 0.25 + 0.2 * w + (after ? 0.5 : gather * 0.25); st.epx(CX + x, RIM + y, lc, k); }
  for (let i = 0; i < 9; i++) { const ph = (t * (0.6 + hsh(i, 1, 1) * 0.6) + hsh(i, 2, 2)) % 1, x = CX + (hsh(i, 3, 3) - 0.5) * 110, y = RIM + (hsh(i, 4, 4) - 0.5) * 12 - ph * 5; st.glow(x, y, 3 + ph * 4, lc, 0.7 * (1 - ph)); }
  st.glow(CX, RIM, 100, lc, after ? 0.55 : 0.2 + gather * 0.25, 2.2); st.light(CX, RIM - 20, 200, lc, after ? 1.3 : 0.5 * gather + 0.2, 1.2);
  // the two materials spiralling in
  if (T < 2.05) { const f = T / 2.05, rad = 150 * (1 - f) ** 1.4 + 4;
    [['EMBER COAL', 0, fa], ['DEEP TEAR', Math.PI, fb]].forEach(([n, off, c]) => {
      const pos = (q) => { const ff = Math.max(0, q), r = 150 * (1 - ff) ** 1.4 + 4, a = off + ff * 9; return [CX + Math.cos(a) * r, 70 + ff * ff * 84 + Math.sin(a) * r * 0.3]; };
      for (let q = 1; q < 22; q++) { const p = pos(f - q * 0.012); st.glow(p[0], p[1], 7 - q * 0.25, c, 0.5 * (1 - q / 22)); st.epx(p[0], p[1], c, 1 - q / 22); }
      const p = pos(f); st.glow(p[0], p[1], 34, c, 0.75); st.light(p[0], p[1], 90, c, 1.1);
      st.blit(MAT[n].spr, p[0] - 24, p[1] - 24, { k: 2, emit: 0.5 }); });
  }
  let flash = 0, sx = 0, sy = 0;
  if (T > 2.0 && T < 2.9) { flash = T < 2.3 ? (T - 2.0) / 0.3 : Math.max(0, 1 - (T - 2.3) / 0.6); flash *= flash; sx = Math.round(Math.sin(t * 97) * 5 * flash); sy = Math.round(Math.cos(t * 71) * 4 * flash); }
  if (after) {
    const a1 = Math.min(1, (T - 2.3) * 1.6), rise = Math.min(1, (T - 2.3) / 1.2), e = 1 - (1 - rise) ** 3, iy = RIM - 20 - e * 62 + Math.sin(t * 2.2) * 3;
    // column of light and turning rays
    for (let y = 0; y < RIM; y++) for (let x = -44; x <= 44; x++) { const d = Math.abs(x) / 44, k = (1 - d) ** 2 * (0.25 + 0.5 * (y / RIM)) * a1 * (0.85 + 0.15 * Math.sin(y * 0.3 - t * 9)); st.epx(CX + x, y, res, k * 0.8); }
    for (let k = 0; k < 14; k++) { const a = k * Math.PI / 7 + t * 0.35, len = (k % 2 ? 150 : 105) * a1;
      for (let d = 22; d < len; d += 1) { const wdt = 1 + d * 0.035; for (let q = -wdt; q <= wdt; q++) st.epx(CX + Math.cos(a) * d - Math.sin(a) * q, iy + Math.sin(a) * d + Math.cos(a) * q, [1, 0.95, 0.75], 0.2 * (1 - d / len)); } }
    // rings racing across the floor
    for (let k = 0; k < 3; k++) { const r = ((T - 2.3) * 0.55 + k / 3) % 1; for (let a = 0; a < 6.283; a += 0.008) st.epx(CX + Math.cos(a) * (60 + r * 300), 292 + Math.sin(a) * (14 + r * 70), res, (1 - r) * 0.8); }
    // sparks
    for (let i = 0; i < 70; i++) { const ph = (hsh(i, 5, 5) + t * (0.25 + hsh(i, 6, 6) * 0.35)) % 1, x = CX + (hsh(i, 7, 7) - 0.5) * (120 + ph * 260), y = RIM - ph * 190 + Math.sin(t * 3 + i) * 3, k = (1 - ph) * a1;
      st.epx(x, y, i % 3 ? res : [1, 0.9, 0.6], k); if (i % 4 === 0) { st.epx(x + 1, y, res, k * 0.6); st.epx(x - 1, y, res, k * 0.6); st.epx(x, y - 1, res, k * 0.6); st.epx(x, y + 1, res, k * 0.6); st.glow(x, y, 6, res, k * 0.4); } }
    st.glow(CX, iy, 70, res, 0.3 * a1); st.light(CX, iy, 260, res, 1.25 * a1);
    st.blit(wisp, CX - 36, iy - 36, { k: 3, emit: 0.12 });
  }
  st.end({ flash, sx, sy });

  const g = st.ui;
  g.fillStyle = '#000'; g.fillRect(0, 0, W, 30); g.fillRect(0, H - 30, W, 30);
  g.fillStyle = R.gold[1]; g.fillRect(0, 30, W, 1); g.fillRect(0, H - 31, W, 1);
  text(g, 'Experiment 14', 12, 8, F.body, '#7a5a3a'); text(g, 'Ember Coal + Deep Tear', W - 12, 8, F.body, '#7a5a3a', { align: 'right' });
  text(g, 'CLICK TO SKIP', CX, H - 20, F.tiny, '#5a4028', { align: 'center' });
  if (T > 2.9) {
    const k = Math.min(1, (T - 2.9) / 0.25), dy = Math.round((1 - k) * 6);
    text(g, 'New Discovery', CX, 36 - dy, F.big, R.gold[3], { align: 'center', shadow: '#3a1a08' });
    text(g, 'Steam Wisp', CX, 178 + dy, F.huge, '#ffffff', { align: 'center', shadow: '#0b2b3a' });
    if (T > 3.4) { const n = Math.min(240, Math.floor((T - 3.4) * 260)), done = n === 240;
      text(g, '+120', CX - 84, 228, F.num, R.gold[3], { align: 'center', shadow: '#3a1a08' }); text(g, 'x2', CX - 16, 228, F.num, '#ee6a3c', { align: 'center', shadow: '#3b0d1e' });
      text(g, '= ' + n, CX + 66, done ? 226 : 228, F.num, done ? '#ffffff' : R.gold[4], { align: 'center', shadow: '#3a1a08' }); }
  }
}

/* ================= SCREEN 3: MATERIALS ================= */
function gallery(host) {
  const cv = document.createElement('canvas'); cv.width = 640; cv.height = 5 * 70 + 6; host.appendChild(cv); const g = cv.getContext('2d'); g.imageSmoothingEnabled = false;
  g.fillStyle = '#120c12'; g.fillRect(0, 0, cv.width, cv.height);
  MATERIALS.forEach((m, i) => { const x = 4 + (i % 5) * 127, y = 4 + Math.floor(i / 5) * 70;
    g.fillStyle = '#0c0706'; g.fillRect(x, y, 124, 67); g.fillStyle = '#241828'; g.fillRect(x + 1, y + 1, 122, 65); g.fillStyle = '#2e2036'; g.fillRect(x + 1, y + 1, 122, 30);
    if (m.glow) { const c = rgbOf(m.glow); for (let r = 26; r > 0; r -= 4) { g.fillStyle = `rgba(${c[0]},${c[1]},${c[2]},.07)`; g.fillRect(x + 28 - r, y + 30 - r, r * 2, r * 2); } }
    g.drawImage(ICON[m.name], x + 4, y + 6, 48, 48);
    wrap2(tc(m.name)).forEach((l, k) => text(g, l, x + 58, y + 10 + k * 13, F.body, '#f0dcc0'));
    text(g, m.kind, x + 58, y + 42, F.tiny, KIND_COL[m.kind]); });
}

async function boot() {
  await document.fonts.ready; await Promise.all(Object.values(FONTSETS).flatMap((o) => Object.values(o)).map((f) => document.fonts.load(f)));
  for (const m of MATERIALS) { const s = m.make(); MAT[m.name] = { ...m, spr: s }; ICON[m.name] = s.canvas(); }
  const a = new Stage(document.getElementById('s1')), b = new Stage(document.getElementById('s2'));
  a.setBase(benchBase()); benchStatic(a); b.setBase(cutBase()); cutStatic(b);
  const regal = () => { const h = document.getElementById('s3'); h.innerHTML = ''; gallery(h); }; regal();
  window.setFonts = (k) => { Object.assign(F, FONTSETS[k]); regal(); document.querySelectorAll('[data-set]').forEach((b) => b.classList.toggle('on', b.dataset.set === k)); };
  const loop = (ms) => { const t = FROZEN !== undefined ? +FROZEN : ms / 1000; benchFrame(a, t); cutFrame(b, t); if (FROZEN === undefined) requestAnimationFrame(loop); else document.title = 'ready'; };
  requestAnimationFrame(loop);
}
boot();
