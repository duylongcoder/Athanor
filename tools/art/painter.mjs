// Pixel-art painter: palette ramps, the Spr sprite buffer and an RGBA Canvas. No dependencies.
import { encodePng } from './png.mjs';

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
  /** As an RGBA canvas, optionally scaled by an integer. */
  canvas(k = 1) {
    const cv = new Canvas(this.w * k, this.h * k);
    for (let y = 0; y < this.h; y++) for (let x = 0; x < this.w; x++) { const v = this.d[y * this.w + x]; if (v) { cv.fillStyle = v; cv.fillRect(x * k, y * k, k, k); } }
    return cv;
  }
}


const parsed = new Map();
function parseColor(c) {
  let v = parsed.get(c); if (v) return v;
  if (c[0] === '#') v = [...rgbOf(c), 255];
  else { const m = c.match(/rgba?\(([^)]+)\)/)[1].split(',').map(Number); v = [m[0], m[1], m[2], Math.round((m[3] === undefined ? 1 : m[3]) * 255)]; }
  parsed.set(c, v); return v;
}

/** A small stand-in for a 2D canvas context: RGBA pixels, fillRect and drawImage with alpha blending. */
class Canvas {
  constructor(w, h) { this.w = w; this.h = h; this.d = new Uint8ClampedArray(w * h * 4); this.fillStyle = '#000000'; }
  getContext() { return this; }
  set(x, y, r, g, b, a) {
    if (x < 0 || y < 0 || x >= this.w || y >= this.h || a <= 0) return;
    const i = (y * this.w + x) * 4, d = this.d, da = d[i + 3];
    if (a >= 255 || da === 0) { d[i] = r; d[i + 1] = g; d[i + 2] = b; d[i + 3] = a; return; }
    const sa = a / 255, oa = sa + (da / 255) * (1 - sa);
    d[i] = (r * sa + d[i] * (da / 255) * (1 - sa)) / oa; d[i + 1] = (g * sa + d[i + 1] * (da / 255) * (1 - sa)) / oa;
    d[i + 2] = (b * sa + d[i + 2] * (da / 255) * (1 - sa)) / oa; d[i + 3] = oa * 255;
  }
  fillRect(x, y, w, h) {
    const [r, g, b, a] = parseColor(this.fillStyle); x = Math.round(x); y = Math.round(y); w = Math.round(w); h = Math.round(h);
    for (let yy = y; yy < y + h; yy++) for (let xx = x; xx < x + w; xx++) this.set(xx, yy, r, g, b, a);
  }
  clearRect(x, y, w, h) { for (let yy = y; yy < y + h; yy++) for (let xx = x; xx < x + w; xx++) if (xx >= 0 && yy >= 0 && xx < this.w && yy < this.h) this.d.fill(0, (yy * this.w + xx) * 4, (yy * this.w + xx) * 4 + 4); }
  /** Draws a Canvas or Spr; w/h scale it with nearest-neighbour sampling. */
  drawImage(src, x, y, w, h) {
    if (src instanceof Spr) src = src.canvas();
    w = w ?? src.w; h = h ?? src.h; x = Math.round(x); y = Math.round(y);
    for (let yy = 0; yy < h; yy++) for (let xx = 0; xx < w; xx++) {
      const i = (Math.floor(yy * src.h / h) * src.w + Math.floor(xx * src.w / w)) * 4;
      this.set(x + xx, y + yy, src.d[i], src.d[i + 1], src.d[i + 2], src.d[i + 3]);
    }
  }
  crop(x, y, w, h) { const c = new Canvas(w, h); c.drawImage(this, -x, -y); return c; }
  /** Smallest rectangle holding every non-transparent pixel: {x, y, w, h}. */
  bounds() {
    let x0 = this.w, y0 = this.h, x1 = -1, y1 = -1;
    for (let y = 0; y < this.h; y++) for (let x = 0; x < this.w; x++) if (this.d[(y * this.w + x) * 4 + 3]) { x0 = Math.min(x0, x); x1 = Math.max(x1, x); y0 = Math.min(y0, y); y1 = Math.max(y1, y); }
    return x1 < 0 ? { x: 0, y: 0, w: 1, h: 1 } : { x: x0, y: y0, w: x1 - x0 + 1, h: y1 - y0 + 1 };
  }
  png() { return encodePng(this.w, this.h, this.d); }
}

export { R, GLASS, HOLLOW, INK, rgbOf, hexOf, mixc, hsh, Spr, Canvas };
