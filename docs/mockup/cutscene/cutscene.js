// Athanor opening cutscene mockup: lighting, particles, the pixelfx flipbooks, the seven scenes, the player,
// the owner's choices panel and the flipbook gallery.
'use strict';

/* ---------------- owner choices (the 11 from docs/devlogs/phase-4-lore.md) + effects to try ---------------- */
const CHOICES = [
  { id: 'faces', q: 'Show the Keepers\' faces?', opts: [['no', 'No, silhouettes only'], ['yes', 'Yes, a small lit face']], def: 'no' },
  { id: 'names', q: 'Names under the Keepers in scene 3 (Tiny font, fading)?', opts: [['no', 'No'], ['yes', 'Yes (edit the names below)']], def: 'no', names: true },
  { id: 'count', q: 'How many Keepers in scene 3?', opts: [['2', '2'], ['3', '3'], ['4', '4'], ['5', '5']], def: '3' },
  { id: 'look', q: 'Does the last Keeper look back in scene 5?', opts: [['yes', 'Yes, once'], ['no', 'No']], def: 'yes' },
  { id: 'book', q: 'Scene 6 close-up', opts: [['journal', 'Reuse the empty journal panel'], ['new', 'New open blank book (~200x130)']], def: 'journal' },
  { id: 'light', q: 'Colour of the light in the hands (scene 4)', opts: [['gold', 'Plain warm gold'], ['seed', 'The Worldseed\'s colour (white)']], def: 'gold' },
  { id: 'dawn', q: 'Scene 6: same night, or dawn?', opts: [['night', 'Same night, candles burnt low'], ['dawn', 'Dawn light through the window']], def: 'night' },
  { id: 'skip', q: 'A way to skip the whole cutscene?', opts: [['none', 'Not now; per line only'], ['esc', 'Esc skips'], ['hold', 'Hold click 1 s']], def: 'none' },
  { id: 'replay', q: 'When does it play?', opts: [['first', 'First launch, plus a title button later'], ['firstonly', 'First launch only'], ['always', 'Every launch (skippable)']], def: 'first' },
  { id: 'music', q: 'Music', opts: [['drop', 'One cue, drops out in scene 5, back in 7'], ['one', 'One cue throughout'], ['change', 'A second cue from scene 5']], def: 'drop' },
  { id: 'more', q: 'More scenes to add (where, and what)?', text: true, def: '' },
];
const FXCHOICES = [
  { id: 'fxPlant', q: 'Planting (scene 4)', opts: [['none', 'Rune circle and rings only'], ['implosion', '+ implosion (pops as the hands press)'], ['pillar', '+ holy pillar (from the press)']], def: 'none' },
  { id: 'fxExit', q: 'A Keeper leaving (scene 3)', opts: [['fade', 'Fades into the dark'], ['poof', 'Poof']], def: 'fade' },
  { id: 'fxBook', q: 'The Book appearing (scene 6)', opts: [['none', 'Plain cut'], ['burst', '+ collect burst']], def: 'none' },
  { id: 'fxPal', q: 'Effect colours', opts: [['athanor', 'Re-tinted to the game\'s gold / cellar greys'], ['orig', 'pixelfx originals']], def: 'athanor' },
];
const DEFAULT_NAMES = ['Maren', 'Tobiah', 'Ilse', 'Corvin', 'Agathe'];
const O = {};
function resetChoices() { for (const c of [...CHOICES, ...FXCHOICES]) O[c.id] = c.def; O.names_list = DEFAULT_NAMES.slice(); }
resetChoices();
try { const s = JSON.parse(localStorage.getItem('athanor-cutscene-choices') || 'null'); if (s) Object.assign(O, s); } catch (e) { /* storage blocked: defaults */ }
function saveChoices() { try { localStorage.setItem('athanor-cutscene-choices', JSON.stringify(O)); } catch (e) { /* ignore */ } }

/* ---------------- lighting: a light map multiplied over the albedo (the game's Light2D, approximated) ---------------- */
const LM = document.createElement('canvas'); LM.width = W; LM.height = H; const LG = LM.getContext('2d', { willReadFrequently: true });
const css = (c, k = 1) => `rgb(${Math.min(255, c[0] * 255 * k) | 0},${Math.min(255, c[1] * 255 * k) | 0},${Math.min(255, c[2] * 255 * k) | 0})`;
const light = {
  begin(amb) { LG.globalCompositeOperation = 'source-over'; LG.fillStyle = css(amb); LG.fillRect(0, 0, W, H); },
  add(x, y, r, c, k, squash = 1) {
    if (k <= 0 || r <= 0) return;
    LG.save(); LG.globalCompositeOperation = 'lighter'; LG.translate(x, y); LG.scale(1, squash);
    const gr = LG.createRadialGradient(0, 0, 0, 0, 0, r);
    gr.addColorStop(0, css(c, k)); gr.addColorStop(0.35, css(c, k * 0.5)); gr.addColorStop(0.7, css(c, k * 0.15)); gr.addColorStop(1, 'rgb(0,0,0)');
    LG.fillStyle = gr; LG.fillRect(-r, -r, r * 2, r * 2); LG.restore();
  },
  // the moon shaft cookie: a trapezoid from the window to the table, bright in the middle
  shaft(oy, c, k) {
    if (k <= 0) return;
    LG.save(); LG.globalCompositeOperation = 'lighter';
    const gr = LG.createLinearGradient(203, 0, 416, 0);
    gr.addColorStop(0, 'rgb(0,0,0)'); gr.addColorStop(0.5, css(c, k)); gr.addColorStop(1, 'rgb(0,0,0)');
    LG.fillStyle = gr; LG.beginPath(); LG.moveTo(287, 20 + oy); LG.lineTo(353, 20 + oy); LG.lineTo(416, 282 + oy); LG.lineTo(203, 282 + oy); LG.closePath(); LG.fill(); LG.restore();
  },
  // posterise with a Bayer dither (the game is pixel-snapped; keeps the light banded like the other mockups)
  apply(g) {
    const im = LG.getImageData(0, 0, W, H), d = im.data, LV = 10;
    for (let y = 0, i = 0; y < H; y++) for (let x = 0; x < W; x++, i += 4) {
      const th = BAY[((y & 3) << 2) | (x & 3)];
      d[i] = Math.min(255, Math.floor(d[i] / 255 * LV + th) / LV * 255); d[i + 1] = Math.min(255, Math.floor(d[i + 1] / 255 * LV + th) / LV * 255); d[i + 2] = Math.min(255, Math.floor(d[i + 2] / 255 * LV + th) / LV * 255);
    }
    LG.putImageData(im, 0, 0);
    g.save(); g.globalCompositeOperation = 'multiply'; g.drawImage(LM, 0, 0); g.restore();
  },
};

/* ---------------- particles (embers, motes, streaks, rune sparks) ---------------- */
class Particles {
  constructor() { this.p = []; }
  clear() { this.p.length = 0; }
  emit(x, y, vx, vy, col, life, grav = 0, big = false) { this.p.push({ x, y, vx, vy, col, life, age: 0, grav, big }); }
  update(dt) { for (const q of this.p) { q.age += dt; q.x += q.vx * dt; q.y += q.vy * dt; q.vy -= q.grav * dt; } this.p = this.p.filter((q) => q.age < q.life && q.y > -20 && q.y < H + 20); }
  draw(g) {
    g.save(); g.globalCompositeOperation = 'lighter';
    for (const q of this.p) { const a = 1 - q.age / q.life; g.globalAlpha = a; g.fillStyle = q.col; const s = q.big ? 2 : 1; g.fillRect(Math.round(q.x), Math.round(q.y), s, q.vy < -200 ? 4 : s); }
    g.restore();
  }
}
const parts = new Particles();
let rand = Math.random;

/* ---------------- pixelfx flipbooks ---------------- */
function fxImage(name) { const v = O.fxPal === 'athanor' && FX_SHEETS[name].athanor ? '-athanor' : ''; return IMG['fx:' + name + v]; }
// draws frame (t seconds in) with its pivot at x, y; returns false once finished
function drawFx(g, name, t, x, y, a = 1) {
  const m = FX_SHEETS[name], f = Math.floor(t * m.fps);
  if (t < 0 || f >= m.frames) return false;
  g.save(); g.globalAlpha = a;
  g.drawImage(fxImage(name), (f % m.cols) * m.w, Math.floor(f / m.cols) * m.h, m.w, m.h, Math.round(x - m.px), Math.round(y - m.py), m.w, m.h);
  g.restore(); return true;
}

/* ---------------- the line slots ---------------- */
const LINES = [
  'Under the old workshop, the cellar keeps going down.',
  'No one dug it. It grew, one Keeper at a time.',
  'Each went down with a satchel, a bench, and a Book.',
  'At the bottom they made something, and planted it.',
  'The last Keeper went down and did not come back up.',
  'The bench is yours now. The Book is empty.',
  'Each depth asks for a stronger brew. Nothing down here is labelled.',
  'The Book will remember what your hands forget.',
  'So mix. Watch what happens. Remember.',
];
const CPS = 42;                                   // SimpleCutscene.Type: 42 characters a second
const typeDur = (i) => LINES[i].length / CPS;
function drawLine(g, i, slot, start, t, alpha = 1) {
  if (t < start) return;
  const n = Math.floor((t - start) * CPS), s = LINES[i].slice(0, n);
  // centre on the full line's width so it types out from a fixed left edge, as TMP's maxVisibleCharacters does
  const full = crisp(LINES[i], FONT.body, COL.cream), X = Math.round(320 - full.width / 2);
  if (!s) return;
  g.save(); g.globalAlpha = alpha;
  g.drawImage(crisp(s, FONT.body, COL.dark), X + 1, 38 + slot * 16 + 1); g.drawImage(crisp(s, FONT.body, COL.cream), X, 38 + slot * 16);
  g.restore();
}

/* ---------------- shared scenery ---------------- */
const CANDLES = [[150, 232], [498, 226], [226, 136], [208, 93], [420, 93]];   // layout.json
const FIRE = [0, 1, 2, 3, 4, 5, 6].map((i) => [Math.round(272 + i * 16 + Math.sin(i * 3) * 3), 290 - (i % 2) * 3]);
const flameFrame = (kind, i, t) => IMG[`flame_${kind}_${Math.floor((t + i * 1.3) * 11) % 8}`];
const flick = (i, t) => 0.85 + 0.15 * Math.sin(t * 11 + i * 1.3) * Math.sin(t * 5.3 + i * 2.6);

// the bench scene with nothing on it. oy: vertical offset (the slide), low: scene 6 (candles burnt low, one out)
function drawWorkshop(g, T, oy, low, dawn) {
  g.drawImage(IMG.workshop_wall, 0, oy);
  g.drawImage(IMG.workshop_table, 102, 132 + oy);
  const cut = low ? 7 : 0;
  CANDLES.forEach(([x, y], i) => g.drawImage(IMG.candle, 0, cut, 12, 20 - cut, x, y + cut + oy, 12, 20 - cut));
  light.begin(dawn ? [0.3, 0.22, 0.24] : COL.ambient);
  light.shaft(oy, dawn ? [1, 0.62, 0.42] : COL.moon, dawn ? 0.75 : low ? 0.36 : 0.62);
  CANDLES.forEach(([x, y], i) => {
    if (low && i === 2) return;
    const onWall = y < 120, k = (onWall ? 1.2 : 1.55) * flick(i, T) * (low ? 0.62 : 1);
    light.add(x + 6, y + 1 - 2 + cut + oy, (onWall ? 170 : 210) * (low ? 0.72 : 1), COL.candle, k, 0.8);
  });
  light.apply(g);
  g.drawImage(IMG.window, 280, 16 + oy);                 // unlit, as in the game
  if (dawn) {
    g.save(); g.beginPath(); g.rect(284, 50 + oy, 72, 52); g.arc(320, 51 + oy, 36, Math.PI, 0); g.clip();
    const gr = g.createLinearGradient(0, 18 + oy, 0, 102 + oy); gr.addColorStop(0, '#3a3a6a'); gr.addColorStop(0.55, '#c86a5a'); gr.addColorStop(1, '#f0b070');
    g.globalAlpha = 0.85; g.fillStyle = gr; g.fillRect(280, 14 + oy, 80, 92); g.restore();
  }
  CANDLES.forEach(([x, y], i) => {
    if (low && i === 2) {   // the candle that went out: a thread of smoke
      for (let k = 0; k < 9; k++) { const sy = y + cut - 3 - k * 3 - ((T * 9) % 3), sx = x + 6 + Math.round(Math.sin(T * 2 + k * 0.7) * (k * 0.35)); g.fillStyle = `rgba(150,140,160,${0.45 - k * 0.045})`; g.fillRect(sx, sy + oy, 1, 2); }
      return;
    }
    g.drawImage(flameFrame('small', i, T), x, y - 8 + cut + oy);
    glowAt(g, x + 6, y + 5 + cut + oy, 30 * (low ? 0.8 : 1), '#ff9a3a', 0.5 * flick(i, T));
  });
  // dust in the beam
  for (let k = 0; k < 14; k++) { const u = (T * 0.02 * (1 + hsh(k, 1)) + hsh(k, 2)) % 1, y = 30 + u * 240, x = 320 + (hsh(k, 3) - 0.5) * (70 + u * 180) + Math.sin(T + k) * 3; g.fillStyle = `rgba(190,210,255,${0.35 * Math.sin(u * Math.PI) * (low ? 0.6 : 1)})`; g.fillRect(Math.round(x), Math.round(y + oy), 1, 1); }
}

function drawRoom(g, T) {
  g.drawImage(IMG.cutscene_room, 0, 0); g.drawImage(IMG.logs, 260, 282); g.drawImage(IMG.cauldron, 220, 118);
  light.begin(COL.ambient);
  light.add(320, 285, 250, COL.fire, 1.4 * flick(0, T * 0.7), 0.62);
  light.apply(g);
  FIRE.forEach(([x, y], i) => {
    g.drawImage(flameFrame('big', i, T), x - 10, y - 17);
    if (i % 2 === 0) glowAt(g, x, y - 8, 60, '#ff9a3a', 0.4 * flick(i, T));
  });
}

// the stair seen through the picture: camY 0 = top half, 360 = bottom half. dark: extra darkness (scene 5)
function drawStair(g, camY, amb, lights) {
  g.drawImage(ART.stair, 0, -Math.round(camY));
  light.begin(amb);
  for (const l of lights) light.add(...l);
  light.apply(g);
}

/* ---------------- scenes ---------------- */
// Each scene: dur, skips (the times a click jumps to: end of a line, of a wait, of a move), draw(g, t, T), info.
// t: scene time; T: wall-clock time for flicker. Times follow the storyboard (docs/devlogs/phase-4-lore.md).
function buildScenes() {
  const S = [];
  // 1 ----------------------------------------------------------------------------------------------
  {
    const tIn = 0.9, tType = tIn, tSlide = 4.1, dur = 5.0;
    S.push({
      n: 1, name: 'The workshop above', dur, skips: [tType + typeDur(0), tSlide, dur],
      sound: 'Candles, wind at the window', music: 'cue', art: [],
      draw(g, t, T) {
        const s = clamp01((t - tSlide) / (dur - tSlide)), oy = -Math.round(ease.inCubic(s) * 400);
        g.fillStyle = '#000'; g.fillRect(0, 0, W, H);
        drawWorkshop(g, T, oy, false, false);
        if (s > 0) for (let n = 0; n < 70 * s * 0.05; n++) if (rand() < 0.7) parts.emit(rand() * W, H + 4, 0, -(380 + rand() * 240), '#7cc0f8', 1.1);
        parts.draw(g);
        drawLine(g, 0, 0, tType, t, 1 - clamp01(s * 3));
        return { fade: 1 - clamp01(t / tIn) };
      },
    });
  }
  // 2 ----------------------------------------------------------------------------------------------
  {
    const tIn = 0.3, s0 = 0.3, s1 = 4.3, tType = 0.5, dur = 5.0;
    S.push({
      n: 2, name: 'The stair going down', dur, skips: [tType + typeDur(1), s1, dur],
      sound: 'Low rumble, slow heartbeat of the roots', music: 'cue', art: ['cellar_stair'],
      draw(g, t, T) {
        const cam = ease.inOut(clamp01((t - s0) / (s1 - s0))) * 360;
        drawStair(g, cam, [0.8, 0.8, 1], []);
        if (rand() < 0.5) parts.emit(rand() * W, H + 2, (rand() - 0.5) * 4, -(10 + rand() * 18), rand() < 0.3 ? '#c4652e' : '#7c90c8', 9);
        parts.draw(g);
        drawLine(g, 1, 0, tType, t);
        return { fade: 1 - clamp01(t / tIn) };
      },
    });
  }
  // 3 ----------------------------------------------------------------------------------------------
  {
    const N = +O.count, gap = Math.min(1.5, 3.0 / Math.max(1, N - 1)), walk = 3.6, k0 = 0.2;
    const walksEnd = k0 + gap * (N - 1) + walk, dur = Math.max(7.0, walksEnd + 0.2), tType = 0.3;
    const kinds = ['hood', 'hat', 'bare'];
    const exitU = 0.74;    // where along the flight a Keeper is gone into the dark
    S.push({
      n: 3, name: 'The Keepers', dur, skips: [tType + typeDur(2), walksEnd, dur],
      sound: 'Footsteps on stone, one set after another', music: 'cue', art: ['cellar_stair', 'keeper_walk'],
      draw(g, t, T) {
        const ks = [];
        for (let i = 0; i < N; i++) {
          const u = (t - k0 - i * gap) / walk; if (u < 0 || u > 1.2) continue;
          const x = lerp(70, 570, Math.min(u, 1)), y = STAIR.feet('C', x) - 360;
          let a = clamp01(u / 0.12) * (O.fxExit === 'poof' ? (u < exitU ? 1 : 0) : 1 - clamp01((u - 0.55) / (exitU - 0.55)));
          ks.push({ i, u, x, y, a });
        }
        drawStair(g, 360, [0.72, 0.72, 0.95], ks.filter((k) => k.a > 0).map((k) => [k.x + 6, k.y - 25, 64, [1, 0.7, 0.35], 1.5 * k.a]));
        if (rand() < 0.4) parts.emit(rand() * W, H + 2, (rand() - 0.5) * 4, -(10 + rand() * 18), rand() < 0.3 ? '#c4652e' : '#7c90c8', 9);
        parts.draw(g);
        for (const k of ks) {
          const kind = kinds[k.i % 3], frame = Math.floor(k.u * walk * 7) % 4, spr = ART.keeper(kind, 'walk' + frame, { faces: O.faces === 'yes', carry: 'book' });
          const c = dithered(spr.c, k.a);
          if (c) { g.drawImage(c, Math.round(k.x - 16), Math.round(k.y - 56)); glowAt(g, k.x - 16 + spr.hand[0], k.y - 56 + spr.hand[1], 9, '#f8b040', 0.8 * k.a); }
          if (O.names === 'yes') textC(g, O.names_list[k.i] || '', k.x, Math.round(k.y + 3), FONT.tiny, COL.dim, COL.dark, k.a * clamp01((k.u - 0.08) / 0.1));
          if (O.fxExit === 'poof') drawFx(g, 'poof', (k.u - exitU) * walk + 0.1, k.x, k.y);
        }
        drawLine(g, 2, 0, tType, t);
        return { fade: clamp01((t - (dur - 0.4)) / 0.4) };
      },
    });
  }
  // 4 ----------------------------------------------------------------------------------------------
  {
    const tIn = 0.5, tType = 0.5, press = 2.6, into = 2.9, roots0 = 3.0, roots1 = 4.6, shake0 = 4.6, sink0 = 4.8, sink1 = 5.6, flash = 6.4, dur = 7.0;
    S.push({
      n: 4, name: 'The planting', dur, skips: [tType + typeDur(3), press, sink1, dur],
      sound: 'A hum, then a deep crack', music: 'cue', art: ['cellar_bottom', 'keeper_kneel', 'roots_grow'],
      draw(g, t, T) {
        const lightCol = O.light === 'seed' ? '#ffffff' : COL.gold, lc = O.light === 'seed' ? [1, 1, 1] : [1, 0.78, 0.35];
        const sink = Math.round(ease.inOut(clamp01((t - sink0) / (sink1 - sink0))) * 24), rune = clamp01((t - into) / 0.25);
        const pulse = rune * (0.7 + 0.3 * Math.sin(T * 4));
        let sx = 0, sy = 0;
        if (t > shake0 && t < shake0 + 0.4) { const k = 2 * (1 - (t - shake0) / 0.4); sx = Math.round((rand() * 2 - 1) * k); sy = Math.round((rand() * 2 - 1) * k); }
        g.save(); g.translate(sx, sy);
        g.fillStyle = '#000'; g.fillRect(-4, -4, W + 8, H + 8);
        g.drawImage(ART.back, 0, 0, W, 250, 0, 0, W, 250);
        // the gap that opens as the floor sinks
        if (sink > 0) { const gr = g.createLinearGradient(0, 250, 0, 250 + sink); gr.addColorStop(0, '#000'); gr.addColorStop(1, '#120a04'); g.fillStyle = gr; g.fillRect(0, 250, W, sink); }
        g.drawImage(ART.floor, 0, 248 + sink);
        // hands: the light between them, then down into the floor
        const pressing = t >= press, kneel = t < press ? 0 : t < 3.3 ? 1 : 2;
        const spr = ART.keeper('hood', 'kneel' + kneel, { faces: O.faces === 'yes' });
        const kx = 304, ky = 262 + sink, hx = kx + spr.hand[0] + 1, hy = ky + spr.hand[1];
        const handK = pressing ? 1 - clamp01((t - press) / (into - press)) : 0.75 + 0.25 * Math.sin(T * 3);
        light.begin([0.5, 0.48, 0.66]);
        light.add(hx, hy, 70, lc, 1.5 * handK);
        light.add(320, 300 + sink, 230, [1, 0.72, 0.3], 1.3 * pulse, 0.3);
        if (sink > 0) light.add(320, 262, 260, [1, 0.6, 0.25], 0.6 * sink / 24, 0.15);
        light.apply(g);
        g.save(); g.globalCompositeOperation = 'lighter';
        if (rune > 0) { g.globalAlpha = pulse; g.drawImage(tinted(IMG.rune_circle, '#ffd25e'), 118, 251 + sink); }
        for (const r0 of [into, into + 0.45]) {
          const r = (t - r0) / 1.5; if (r < 0 || r > 1) continue;
          const w = (60 + r * 320) / 60 * 123 / 2.05, h = (14 + r * 76) / 14 * 31 / 2.2;
          g.globalAlpha = (1 - r) * 0.8; g.drawImage(tinted(IMG.floor_ring, '#ffd25e'), Math.round(320 - w / 2), Math.round(300 + sink - h / 2), Math.round(w), Math.round(h));
        }
        g.restore();
        if (t > roots0) drawRootsGrow(g, 240, 276 + sink, (t - roots0) / (roots1 - roots0), rune > 0 ? '#f8d250' : null);
        if (rune > 0 && rand() < 0.5) parts.emit(320 + (rand() - 0.5) * 280, 300 + sink + (rand() - 0.5) * 28, 0, -(20 + rand() * 40), '#ffd25e', 1.3, -4, rand() < 0.3);
        parts.draw(g);
        g.drawImage(spr.c, kx, ky);
        if (handK > 0) {
          const ly = pressing ? lerp(hy, hy + 4, 1 - handK) : hy, r = pressing ? 1 + 2 * handK : 2.5;
          glowAt(g, hx, ly, 14 * Math.max(handK, 0.3), lightCol, 0.9 * handK);
          g.fillStyle = lightCol; g.fillRect(Math.round(hx - r / 2), Math.round(ly - r / 2), Math.max(1, Math.round(r)), Math.max(1, Math.round(r)));
          g.fillStyle = COL.silhouette; g.fillRect(hx - 3, hy - 3, 2, 4); g.fillRect(hx + 2, hy - 3, 2, 4);   // fingers covering it
        }
        if (O.fxPlant === 'implosion') drawFx(g, 'implosion', t - (press + 0.3 - 1.02), hx, hy + 4);
        if (O.fxPlant === 'pillar') drawFx(g, 'holy-pillar', t - (into - 0.7), 320, 300 + sink);
        g.restore();
        drawLine(g, 3, 0, tType, t);
        const fl = t > flash ? Math.max(0, 0.5 * (1 - (t - flash) / 0.3)) : 0;
        return { fade: Math.max(1 - clamp01(t / tIn), t > flash + 0.3 ? 1 : 0), flash: fl };
      },
    });
  }
  // 5 ----------------------------------------------------------------------------------------------
  {
    const tIn = 0.4, tType = 0.4, w0 = 0.4, w1 = 1.4, lookOn = O.look === 'yes', l0 = 1.6, l1 = 2.2, go = lookOn ? 2.3 : 1.6, out0 = 4.0, out1 = 5.0, black = 5.4, dur = 6.0;
    S.push({
      n: 5, name: 'The last Keeper', dur, skips: [tType + typeDur(4), out1, dur],
      sound: 'One set of footsteps, fading; silence', music: O.music === 'drop' ? 'silent' : O.music === 'change' ? 'cue2' : 'cue', art: ['cellar_stair', 'keeper_last'],
      draw(g, t, T) {
        // walk: three steps (63 px), stop, look back, on into the dark
        let x = 96, moving = false;
        if (t > w0) x += 63 * clamp01((t - w0) / (w1 - w0));
        if (t > go) x += 92 * (t - go);
        moving = (t > w0 && t < w1) || t > go;
        const y = STAIR.feet('A', x), lan = 1 - clamp01((t - out0) / (out1 - out0));
        const looking = lookOn && t > l0 && t < l1;
        const frame = moving ? Math.floor(t * 7) % 4 : -1;
        const spr = ART.keeper('bare', moving ? 'walk' + frame : 'stand', { faces: O.faces === 'yes', carry: 'lantern' });
        const hx = x - 16 + (looking ? 31 - spr.hand[0] : spr.hand[0]), hy = y - 56 + spr.hand[1] + 4;
        drawStair(g, 0, [0.3, 0.3, 0.45], lan > 0 ? [[hx, hy, 60 * (0.25 + 0.75 * lan), [1, 0.7, 0.35], 1.6 * lan]] : []);
        const a = 1 - clamp01((t - (out0 + 0.3)) / 0.7);
        const c = dithered(looking ? mirror(spr.c) : spr.c, a);
        if (c) g.drawImage(c, Math.round(x - 16), Math.round(y - 56));
        if (lan > 0) { glowAt(g, hx, hy, 22 * lan + 3, '#ffb040', 0.9 * lan); g.fillStyle = '#fff6b8'; g.fillRect(Math.round(hx), Math.round(hy), 1, 1); }
        if (O.names === 'yes') textC(g, 'The Keeper', x, Math.round(y + 3), FONT.tiny, COL.dim, COL.dark, a * clamp01((t - 0.6) / 0.3));
        drawLine(g, 4, 0, tType, t);
        return { fade: Math.max(1 - clamp01(t / tIn), clamp01((t - (black - 0.4)) / 0.4)) };
      },
    });
  }
  // 6 ----------------------------------------------------------------------------------------------
  {
    const tIn = 0.6, cut = 2.0, tType = 2.2, out = 4.7, dur = 5.0;
    S.push({
      n: 6, name: 'The empty bench and the Book', dur, skips: [cut, tType + typeDur(5), out, dur],
      sound: 'A page turning; quiet', music: O.music === 'drop' ? 'silent' : O.music === 'change' ? 'cue2' : 'cue', art: O.book === 'new' ? ['book (new)'] : [],
      draw(g, t, T) {
        g.fillStyle = '#000'; g.fillRect(0, 0, W, H);
        if (t < cut) drawWorkshop(g, T, 0, true, O.dawn === 'dawn');
        else if (O.book === 'journal') g.drawImage(IMG.journal, 20, 15);
        else {
          // the new book on the dark table, candlelit
          g.fillStyle = '#0c0706'; g.fillRect(0, 0, W, H);
          glowAt(g, 320, 210, 190, '#ff9a3a', 0.16);
          g.drawImage(ART.book, 218, 120);
        }
        if (t >= cut && O.fxBook === 'burst') drawFx(g, 'collect-burst', t - cut, 320, O.book === 'journal' ? 230 : 200);
        drawLine(g, 5, 0, tType, t);
        return { fade: Math.max(1 - clamp01(t / tIn), clamp01((t - out) / (dur - out))) };
      },
    });
  }
  // 7 ----------------------------------------------------------------------------------------------
  {
    const tIn = 0.3, a0 = 0.5, a1 = a0 + typeDur(6), b0 = a1 + 0.8, b1 = b0 + typeDur(7), c0 = b1 + 0.8, c1 = c0 + typeDur(8), pr = c1 + 0.3, wait = pr + 8, dur = wait + 0.4;
    S.push({
      n: 7, name: 'The cauldron room', dur, skips: [a1, b0, b1, c0, c1, pr, wait, dur], promptAt: pr, waitEnd: wait,
      sound: 'Fire, bubbling, the music\'s first full phrase', music: O.music === 'change' ? 'cue2' : 'cue', art: [],
      draw(g, t, T) {
        drawRoom(g, T);
        if (rand() < 22 / 60) parts.emit(320 + (rand() * 2 - 1) * 56, 286, (rand() * 2 - 1) * 10, -(22 + rand() * 38), '#ffa040', 1.5, -6, rand() < 0.25);
        parts.draw(g);
        drawLine(g, 6, 0, a0, t); drawLine(g, 7, 1, b0, t); drawLine(g, 8, 2, c0, t);
        return { fade: Math.max(1 - clamp01(t / tIn), clamp01((t - wait) / 0.4)), prompt: t >= pr && t < wait ? 'CLICK TO BEGIN' : '' };
      },
    });
  }
  return S;
}

/* ---------------- the player ---------------- */
const scr = document.getElementById('screen'), G = scr.getContext('2d');
G.imageSmoothingEnabled = false;
let SC = [], cur = 0, t = 0, playing = true, speed = 1, done = false, T = 0, holdStart = -1, last = performance.now();
function rebuild() { SC = buildScenes(); if (cur >= SC.length) cur = SC.length - 1; t = Math.min(t, SC[cur].dur - 1e-3); updateUi(); }
function seek(i, at = 0) {
  cur = i; t = at; done = false; parts.clear();
  // warm the particles up so a jump lands on a settled frame
  const keep = rand; let s = 7; rand = () => ((s = (s * 16807) % 2147483647) / 2147483647);
  for (let k = Math.max(0, at - 2); k < at; k += 1 / 15) { SC[cur].draw(G, k, T); parts.update(1 / 15); }
  rand = keep; updateUi();
}
function totalBefore(i) { let s = 0; for (let k = 0; k < i; k++) s += SC[k].dur; return s; }
function click() {
  if (done) { seek(0); playing = true; updateUi(); return; }
  const s = SC[cur].skips.find((v) => v > t + 1e-4);
  if (s !== undefined) t = s;
}
function skipAll() { const s7 = SC[6]; if (cur !== 6) seek(6, s7.waitEnd); else t = Math.max(t, s7.waitEnd); }

function frame(now) {
  const dt = Math.min(0.1, (now - last) / 1000); last = now;
  if (playing) T += dt * speed;
  if (playing && !done) {
    t += dt * speed;
    while (t >= SC[cur].dur) { if (cur === SC.length - 1) { done = true; t = SC[cur].dur; break; } t -= SC[cur].dur; cur++; parts.clear(); }
    parts.update(dt * speed);
    if (holdStart >= 0 && O.skip === 'hold' && now - holdStart > 1000) { holdStart = -1; skipAll(); }
  }
  render(now);
  updateHud();
  requestAnimationFrame(frame);
}

function render(now) {
  G.save(); G.globalCompositeOperation = 'source-over'; G.globalAlpha = 1;
  G.fillStyle = '#000'; G.fillRect(0, 0, W, H);
  if (done) {
    textC(G, 'The bench (the run starts here)', 320, 160, FONT.body, COL.dim, COL.dark);
    textC(G, O.replay === 'first' ? 'FIRST LAUNCH; REPLAY FROM THE TITLE LATER' : O.replay === 'firstonly' ? 'FIRST LAUNCH ONLY' : 'PLAYS ON EVERY LAUNCH', 320, 182, FONT.tiny, COL.prompt);
    textC(G, 'CLICK TO WATCH AGAIN', 320, 341, FONT.tiny, COL.prompt);
    bars(G); G.restore(); return;
  }
  const r = SC[cur].draw(G, t, T) || {};
  G.globalAlpha = 1; G.globalCompositeOperation = 'source-over';
  if (r.flash) { G.fillStyle = `rgba(255,246,220,${r.flash})`; G.fillRect(0, 0, W, H); }
  if (r.fade > 0) { G.fillStyle = `rgba(0,0,0,${Math.min(1, r.fade)})`; G.fillRect(0, 0, W, H); }
  bars(G);
  if (r.prompt) textC(G, r.prompt, 320, 341, FONT.tiny, COL.prompt);
  if (O.skip === 'esc') textC(G, 'ESC: SKIP', 600, 341, FONT.tiny, '#4a3a2a');
  if (O.skip === 'hold') {
    textC(G, 'HOLD TO SKIP', 596, 341, FONT.tiny, '#4a3a2a');
    if (holdStart >= 0) { const k = clamp01((now - holdStart) / 1000); G.fillStyle = COL.brass; G.fillRect(560, 352, Math.round(72 * k), 2); }
  }
  G.restore();
}
function bars(g) {
  g.fillStyle = '#000'; g.fillRect(0, 0, W, 30); g.fillRect(0, H - 30, W, 30);
  g.fillStyle = COL.brass; g.fillRect(0, 30, W, 1); g.fillRect(0, H - 31, W, 1);
}

/* ---------------- page UI ---------------- */
const $ = (s) => document.querySelector(s);
function updateUi() {
  document.querySelectorAll('#scenes button').forEach((b, i) => b.classList.toggle('on', i === cur && !done));
  $('#play').textContent = playing ? 'Pause' : 'Play';
  document.querySelectorAll('#speed button').forEach((b) => b.classList.toggle('on', +b.dataset.v === speed));
}
function updateHud() {
  const s = SC[cur]; if (!s) return;
  const tot = SC.reduce((a, b) => a + b.dur, 0), at = totalBefore(cur) + t;
  $('#hud-scene').textContent = done ? 'Ended: the bench' : `Scene ${s.n} · ${s.name}`;
  $('#hud-time').textContent = `${t.toFixed(1)} / ${s.dur.toFixed(1)} s · total ${at.toFixed(1)} / ${tot.toFixed(1)} s`;
  $('#hud-sound').textContent = s.sound;
  const m = s.music === 'silent' ? 'music: silent (dropped out)' : s.music === 'cue2' ? 'music: second cue' : 'music: main cue';
  $('#hud-music').textContent = m;
  $('#hud-art').textContent = s.art.length ? 'Placeholder art: ' + s.art.join(', ') : 'All existing game art';
  $('#bar').style.width = (100 * at / tot).toFixed(2) + '%';
}

let zoom = 0;   // 0: the largest whole factor that fits beside the panel
function fitStage() {
  const lay = $('.layout'), box = $('#stagebox');
  lay.classList.remove('stack');
  let k = Math.max(1, Math.floor(Math.min(box.clientWidth / W, (window.innerHeight - 150) / H)));
  if (zoom) { k = zoom; if (W * k > box.clientWidth) lay.classList.add('stack'); }
  const fit = $('#stagebox').clientWidth < W * k;
  scr.style.width = fit ? '100%' : W * k + 'px'; scr.style.height = fit ? 'auto' : H * k + 'px';
  $('#scale').textContent = fit ? '(fit)' : '(' + k + 'x)';
  document.querySelectorAll('#zoom button').forEach((b) => b.classList.toggle('on', +b.dataset.z === zoom));
}

function choiceRow(c, host, onChange) {
  const box = document.createElement('fieldset'); box.className = 'choice'; box.dataset.id = c.id;
  const lg = document.createElement('legend'); lg.textContent = c.q; box.appendChild(lg);
  if (c.text) {
    const ta = document.createElement('textarea'); ta.rows = 2; ta.placeholder = 'e.g. after scene 4, a shot of the new depth'; ta.value = O[c.id] || '';
    ta.oninput = () => { O[c.id] = ta.value; onChange(); }; box.appendChild(ta);
  } else for (const [v, label] of c.opts) {
    const l = document.createElement('label'), r = document.createElement('input');
    r.type = 'radio'; r.name = c.id; r.value = v; r.checked = O[c.id] === v;
    r.onchange = () => { O[c.id] = v; onChange(); };
    l.appendChild(r); l.append(' ' + label);
    if (v === c.def) { const p = document.createElement('span'); p.className = 'prop'; p.textContent = 'proposed'; l.appendChild(p); }
    box.appendChild(l);
  }
  if (c.names) {
    const row = document.createElement('div'); row.className = 'names';
    for (let i = 0; i < 5; i++) { const inp = document.createElement('input'); inp.value = O.names_list[i] || ''; inp.placeholder = 'Keeper ' + (i + 1); inp.oninput = () => { O.names_list[i] = inp.value; onChange(); }; row.appendChild(inp); }
    box.appendChild(row);
  }
  host.appendChild(box);
}
function buildPanel() {
  const h1 = $('#choices'), h2 = $('#fxchoices'); h1.innerHTML = ''; h2.innerHTML = '';
  const changed = () => { saveChoices(); rebuild(); summary(); markChanged(); };
  CHOICES.forEach((c, i) => { choiceRow({ ...c, q: `${i + 1}. ${c.q}` }, h1, changed); });
  FXCHOICES.forEach((c) => choiceRow(c, h2, changed));
  summary(); markChanged();
}
function markChanged() { document.querySelectorAll('fieldset.choice').forEach((f) => { const c = [...CHOICES, ...FXCHOICES].find((x) => x.id === f.dataset.id); f.classList.toggle('changed', (O[c.id] || '') !== c.def); }); }
function label(c) { if (c.text) return (O[c.id] || '').trim() || '(none)'; const o = c.opts.find(([v]) => v === O[c.id]); return o ? o[1] : O[c.id]; }
function summary() {
  const L = ['Athanor opening cutscene: my choices (from docs/mockup/cutscene)'];
  CHOICES.forEach((c, i) => {
    let s = label(c);
    if (c.id === 'names' && O.names === 'yes') s += ': ' + O.names_list.slice(0, +O.count).filter(Boolean).join(', ');
    L.push(`${i + 1}. ${c.q} ${s}${(O[c.id] || '') === c.def ? ' (as proposed)' : ' (CHANGED)'}`);
  });
  L.push('Effects: ' + FXCHOICES.map((c) => `${c.q}: ${label(c)}`).join('; ') + '.');
  $('#summary').value = L.join('\n');
}
async function copySummary() {
  const ta = $('#summary'), text = ta.value; let ok = false;
  try { await navigator.clipboard.writeText(text); ok = true; } catch (e) { /* fall back */ }
  if (!ok) { ta.focus(); ta.select(); try { ok = document.execCommand('copy'); } catch (e) { ok = false; } }
  $('#copied').textContent = ok ? 'Copied.' : 'Select the text below and copy it.';
  setTimeout(() => ($('#copied').textContent = ''), 2500);
}

/* ---------------- flipbook gallery ---------------- */
const GALLERY = [
  { k: 'holy-pillar', where: 'Scene 4, the planting: light comes down onto the spot from the press until the sink. Reads as "holy" and comes from above, which the cellar does not have; strong but off-theme.' },
  { k: 'implosion', where: 'Scene 4, the planting: everything is pulled into the hands and pops as they press. Fits "something is made"; re-tinted gold here.' },
  { k: 'poof', where: 'Scene 3, a Keeper leaving: a smoke pop instead of fading into the dark. Cartoony (stars, speed lines) next to the quiet tone.' },
  { k: 'collect-burst', where: 'Scene 6, the Book appearing; or in play, when the Book records a discovery. Small and bright; fits a reward moment best.' },
];
function buildGallery() {
  const host = $('#gallery');
  for (const it of GALLERY) {
    const m = FX_SHEETS[it.k], d = document.createElement('figure'), c = mk(m.w, m.h);
    c.style.width = m.w * 2 + 'px'; c.style.height = m.h * 2 + 'px';
    d.appendChild(c);
    const cap = document.createElement('figcaption');
    cap.innerHTML = `<b>${m.name}</b> <code>${m.id}</code><br>${m.w}x${m.h}, ${m.frames} frames at ${m.fps} fps (${(m.frames / m.fps).toFixed(2)} s), pivot ${m.px},${m.py}.<br>` +
      `<span class="src">${m.athanor ? 'Real export, re-rendered with the game palette (and the original)' : 'Real export (its own palette is already gold)'}: pixelfx <code>tools/render.mjs --sheet</code>.</span><br>${it.where}`;
    d.appendChild(cap); host.appendChild(d);
    it.c = c; it.g = c.getContext('2d'); it.g.imageSmoothingEnabled = false;
  }
  const loop = (now) => {
    for (const it of GALLERY) {
      const m = FX_SHEETS[it.k], len = m.frames / m.fps + 0.6, tt = (now / 1000) % len;
      it.g.fillStyle = '#0c0a10'; it.g.fillRect(0, 0, m.w, m.h);
      it.g.fillStyle = '#1c1626'; it.g.fillRect(0, m.py + 1, m.w, m.h - m.py - 1);
      drawFx(it.g, it.k, tt, m.px, m.py);
    }
    requestAnimationFrame(loop);
  };
  requestAnimationFrame(loop);
}

/* ---------------- start ---------------- */
async function start() {
  try {
    await Promise.all([loadImages(), document.fonts.load(FONT.body), document.fonts.load(FONT.tiny), document.fonts.load(FONT.title)]);
  } catch (e) { $('#err').textContent = 'Could not load: ' + e.message + '. Open index.html from docs/mockup/cutscene/ with its img/ and fx/ folders beside it.'; throw e; }
  buildArt();
  buildPanel();
  SC = buildScenes();
  buildGallery();
  document.querySelectorAll('#scenes button').forEach((b, i) => (b.onclick = () => { seek(i); updateUi(); }));
  $('#play').onclick = () => { playing = !playing; updateUi(); };
  $('#restart').onclick = () => { seek(0); playing = true; updateUi(); };
  document.querySelectorAll('#speed button').forEach((b) => (b.onclick = () => { speed = +b.dataset.v; updateUi(); }));
  $('#copy').onclick = copySummary;
  $('#reset').onclick = () => { resetChoices(); saveChoices(); buildPanel(); rebuild(); };
  let downAt = 0;
  scr.addEventListener('pointerdown', (e) => { e.preventDefault(); downAt = performance.now(); if (O.skip === 'hold') holdStart = downAt; });
  scr.addEventListener('pointerup', () => { const held = performance.now() - downAt; holdStart = -1; if (O.skip !== 'hold' || held < 300) click(); });
  scr.addEventListener('pointerleave', () => (holdStart = -1));
  window.addEventListener('keydown', (e) => {
    if (e.target && e.target.matches && e.target.matches('input, textarea')) return;
    if (e.key === 'Escape' && O.skip === 'esc') skipAll();
    else if (e.key === ' ') { e.preventDefault(); playing = !playing; updateUi(); }
    else if (e.key >= '1' && e.key <= '7') { seek(+e.key - 1); updateUi(); }
  });
  document.querySelectorAll('#zoom button').forEach((b) => (b.onclick = () => { zoom = +b.dataset.z; fitStage(); }));
  window.addEventListener('resize', fitStage); fitStage();
  // #scene=4&t=3.2&pause : open on a frame (for screenshots)
  const q = new URLSearchParams(location.hash.slice(1));
  if (q.has('scene')) { seek(+q.get('scene') - 1, +(q.get('t') || 0)); if (q.has('pause')) playing = false; }
  updateUi();
  requestAnimationFrame((now) => { last = now; frame(now); });
  window.MOCK = { seek: (n, at) => { seek(n - 1, at); }, pause: () => { playing = false; updateUi(); }, set: (k, v) => { O[k] = v; buildPanel(); rebuild(); }, click, state: () => ({ scene: cur + 1, t, done, playing }) };
}
start();
