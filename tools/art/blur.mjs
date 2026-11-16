// Gaussian blur on premultiplied alpha, so transparent pixels never darken the edge.
import { Canvas } from './painter.mjs';
/** Returns a new Canvas (w+2*pad, h+2*pad). Outside the source is transparent, or the edge colour when clamp is set (opaque layers). */
export function blur(cv, sigma, pad = 0, clamp = false) {
  const W = cv.w + 2 * pad, H = cv.h + 2 * pad, r = Math.ceil(sigma * 3), k = [];
  let sum = 0; for (let i = -r; i <= r; i++) { const v = Math.exp(-(i * i) / (2 * sigma * sigma)); k.push(v); sum += v; }
  for (let i = 0; i < k.length; i++) k[i] /= sum;
  let a = new Float32Array(W * H * 4);
  for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
    let sx = x - pad, sy = y - pad;
    if (clamp) { sx = Math.max(0, Math.min(cv.w - 1, sx)); sy = Math.max(0, Math.min(cv.h - 1, sy)); }
    else if (sx < 0 || sy < 0 || sx >= cv.w || sy >= cv.h) continue;
    const i = (sy * cv.w + sx) * 4, al = cv.d[i + 3] / 255, o = (y * W + x) * 4;
    a[o] = cv.d[i] * al; a[o + 1] = cv.d[i + 1] * al; a[o + 2] = cv.d[i + 2] * al; a[o + 3] = al * 255;
  }
  for (const horiz of [true, false]) {
    const b = new Float32Array(a.length);
    for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
      const o = (y * W + x) * 4;
      for (let t = -r; t <= r; t++) {
        const xx = horiz ? Math.max(0, Math.min(W - 1, x + t)) : x, yy = horiz ? y : Math.max(0, Math.min(H - 1, y + t)), p = (yy * W + xx) * 4, w = k[t + r];
        b[o] += a[p] * w; b[o + 1] += a[p + 1] * w; b[o + 2] += a[p + 2] * w; b[o + 3] += a[p + 3] * w;
      }
    }
    a = b;
  }
  const out = new Canvas(W, H);
  for (let i = 0; i < W * H; i++) {
    const al = a[i * 4 + 3] / 255; if (al < 0.002) continue;
    out.d[i * 4] = a[i * 4] / al; out.d[i * 4 + 1] = a[i * 4 + 1] / al; out.d[i * 4 + 2] = a[i * 4 + 2] / al; out.d[i * 4 + 3] = a[i * 4 + 3];
  }
  return out;
}
