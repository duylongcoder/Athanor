// Phase 5b, task 5b-02: the cutscene streams as random emitters (before, git 40b8a469) and as loops (after, shipped 2026-11-13).
// Ports of the C#, frame by frame at a fixed step. Nothing here is new design: rates, lives, ranges, colours and snapping are copied.
//   before: SimpleCutscene.Update at 40b8a469 (lines 249-262), particles stepped by Sparks.Step / shaded by Sparks.Shade
//   after:  SimpleCutscene.Update now (:265-296) and SimpleCutscene.Loop (:301-358)
//   both drawn the way SparkMesh draws (Fx/SparkMesh.cs:92-99, Put): centre rounded with Mathf.Round, 1 px dot or 5x5 sparkle,
//   additive (ParticleGlow: SrcAlpha One), colours kept as floats so values above 1 reach the (approximate) bloom.
// Page modes: index.html                    live, real time, fixed-step simulation (controls in the bar)
//             index.html?hz=60&at=2.5        live, advanced to 2.5 s by fixed steps and paused (deterministic)
//             index.html?shot=embers&hz=30   one evidence sheet (rows of before|after frames + a count chart); shoot.mjs uses it
// Everything is seeded (mulberry32 for UnityEngine.Random) and fixed-step, so the same URL draws the same pixels.
(function () {
  'use strict';
  const W = 640, H = 360, CX = 320, T0 = 100;           // T0: Time.time when a timeline's t = 0 (the game has been running)
  const f32 = Math.fround;
  const clamp01 = (v) => (v < 0 ? 0 : v > 1 ? 1 : v);
  const lerp = (a, b, t) => a + (b - a) * t;
  /** Mathf.Round: half to even. */
  function roundEven(v) { const r = Math.round(v); return Math.abs(v % 1) === 0.5 && r % 2 !== 0 ? r - 1 : r; }
  /** Px.At: art space (y down from the top-left) to world (origin at the centre, y up). */
  const At = (x, y) => [f32(x - W / 2), f32(H / 2 - y)];
  /** World to canvas pixel: column and row of the pixel the rounded centre lights. */
  const pix = (wx, wy) => [roundEven(wx) + W / 2, H / 2 - roundEven(wy)];
  function mulberry32(seed) {
    let a = seed >>> 0;
    return () => { a = (a + 0x6D2B79F5) >>> 0; let t = a; t = Math.imul(t ^ (t >>> 15), t | 1); t ^= t + Math.imul(t ^ (t >>> 7), t | 61); return ((t ^ (t >>> 14)) >>> 0) / 4294967296; };
  }
  const range = (R, a, b) => a + (b - a) * R();       // UnityEngine.Random.Range(float, float)
  const hex = (h) => [1, 3, 5].map((i) => parseInt(h.slice(i, i + 2), 16) / 255);
  const ACCENT = ['#7cc0f8', '#6ee6c8', '#c486f4', '#ee6a3c', '#fff6b8'];   // GameFlow.Accent; Descent(next) uses Accent[next - 1]

  /** SimpleCutscene.Loop's hash, bit for bit (uint maths with Math.imul). */
  function lhash(i, c, k) {
    let h = (Math.imul(i, 0x9E3779B1) ^ Math.imul(c, 0x85EBCA77) ^ Math.imul(k, 0xC2B2AE3D)) >>> 0;
    h = (h ^ (h >>> 15)) >>> 0; h = Math.imul(h, 0x2C1B3C6D) >>> 0; h = (h ^ (h >>> 12)) >>> 0; h = Math.imul(h, 0x297A2D39) >>> 0; h = (h ^ (h >>> 15)) >>> 0;
    return (h & 0xFFFFFF) / 16777216;
  }

  // The three streams: the Loop constructor arguments of SimpleCutscene.cs:283-285 and the colours of :287-289.
  const STREAMS = {
    embers: { title: 'Embers', use: 'Intro and Ending: off the fire under the cauldron (order 310)', rate: 22, life: 1.5,
      x0: CX - 56, x1: CX + 56, y0: 286, y1: 286, vx0: -10, vx1: 10, vy0: 22, vy1: 60, g: -6, sparkle: 0.25, color: () => [2.4, 1.2, 0.4, 1], room: true },
    streaks: { title: 'Descent streaks', use: 'Descent: motes rushing up the screen while the room slides away (order O+1)', rate: 70, life: 1.1,
      x0: 0, x1: W, y0: H + 4, y1: H + 4, vx0: 0, vx1: 0, vy0: 380, vy1: 620, g: 0, sparkle: 0, color: (t) => [t[0] * 1.6, t[1] * 1.6, t[2] * 1.6, 0.75], room: false },
    rune: { title: 'Rune motes', use: 'Descent: motes rising off the rune circle under the depth name (order O+3)', rate: 26, life: 1.3,
      x0: CX - 140, x1: CX + 140, y0: 248, y1: 276, vx0: 0, vx1: 0, vy0: 20, vy1: 60, g: -4, sparkle: 0.3, color: (t) => [t[0] * 2, t[1] * 2, t[2] * 2, 1], room: false },
  };
  const IDS = ['embers', 'streaks', 'rune'];

  // ---- before: random Emit calls each frame (40b8a469 SimpleCutscene.Update), into the Sparks pool ------------------------------
  function particle(p, vx, vy, col, life, g, sp) { return { x: p[0], y: p[1], vx, vy, life, max: life, g, col, sp }; }
  /** Sparks.Step for every particle; the dead are dropped. */
  function stepAll(list, dt) {
    let n = 0;
    for (const p of list) {
      p.life -= dt;
      if (p.life <= 0) continue;
      p.vy -= p.g * dt; p.x += p.vx * dt; p.y += p.vy * dt;
      list[n++] = p;
    }
    list.length = n;
  }
  /** The emit lines of 40b8a469 for one stream at Time.time t with Time.deltaTime dt; returns how many were born. */
  function emitBefore(id, list, R, t, dt, s, col) {
    if (id === 'embers') {
      if (s > 0 && R() < 22 * dt) {
        const x = CX + range(R, -56, 56), vx = range(R, -10, 10), vy = range(R, 22, 60), sp = R() < 0.25;
        list.push(particle(At(x, 286), vx, vy, col, 1.5, -6, sp)); return 1;
      }
      return 0;
    }
    if (id === 'streaks') {
      const k = Math.floor(f32(f32(t * 70) * s)) - Math.floor(f32(f32(f32(t - dt) * 70) * s));
      for (let n = k; n > 0; n--) { const x = range(R, 0, W), vy = range(R, 380, 620); list.push(particle(At(x, H + 4), 0, vy, col, 1.1, 0, false)); }
      return Math.max(0, k);
    }
    if (s > 0 && R() < 26 * dt * s) {
      const x = CX + range(R, -140, 140), y = 262 + range(R, -14, 14), vy = range(R, 20, 60), sp = R() < 0.3;
      list.push(particle(At(x, y), 0, vy, col, 1.3, -4, sp)); return 1;
    }
    return 0;
  }
  /** Sparks.Shade: full until half the life is gone, then fading. */
  const shadeBefore = (p) => ({ x: p.x, y: p.y, r: p.col[0], g: p.col[1], b: p.col[2], a: p.col[3] * clamp01((p.life / p.max) * 2), sp: p.sp });

  // ---- after: SimpleCutscene.Loop ---------------------------------------------------------------------------------------------------
  class Loop {
    constructor(S) {
      this.S = S;
      this.n = Math.ceil(f32(S.rate * f32(S.life)));    // Mathf.CeilToInt(rate * life): 33, 77, 34
      this.cycle = new Array(this.n).fill(-Infinity);   // int.MinValue
      this.on = new Array(this.n).fill(false);
    }
    /** Loop.Draw: returns the particles drawn this frame and how many slots were reborn lit. */
    draw(t, strength, col) {
      const S = this.S, n = this.n, life = S.life, parts = [];
      let born = 0;
      for (let i = 0; i < n; i++) {
        const clock = f32(t / life + (i + 0.5 + (lhash(i, 0, 9) - 0.5) * 0.9) / n);
        const c = Math.floor(clock);
        if (this.cycle[i] !== c) { this.on[i] = this.cycle[i] === c - 1 && lhash(i, c, 0) < strength; this.cycle[i] = c; if (this.on[i]) born++; }
        if (!this.on[i]) continue;
        const a = (clock - c) * life;
        const p = At(lerp(S.x0, S.x1, lhash(i, c, 1)), lerp(S.y0, S.y1, lhash(i, c, 2)));
        const vx = lerp(S.vx0, S.vx1, lhash(i, c, 3)), vy = lerp(S.vy0, S.vy1, lhash(i, c, 4));
        parts.push({ x: p[0] + vx * a, y: p[1] + vy * a - 0.5 * S.g * a * a, r: col[0], g: col[1], b: col[2],
          a: col[3] * clamp01(((life - a) / life) * 2), sp: lhash(i, c, 5) < S.sparkle });
      }
      return { parts, born };
    }
  }

  // ---- one stream, both versions, advanced frame by frame -------------------------------------------------------------------------
  /**
   * A Unity frame: Sparks.Update steps the pool, then SimpleCutscene.Update (only while the story root is active) emits (before) and
   * draws its loop (after), then LateUpdate uploads the meshes. The emitter's particles live in Sparks, outside the root, so they
   * keep flying when the root is hidden; the loop's mesh is a child of the root and disappears with it.
   */
  class Sim {
    constructor(id, hz, seed, tint, preroll = 0.2) {
      this.id = id; this.S = STREAMS[id]; this.hz = hz; this.R = mulberry32(seed); this.col = this.S.color(tint);
      this.pre = Math.round(preroll * hz); this.base = -this.pre / hz; this.k = 0; this.t = this.base;
      this.before = []; this.loop = new Loop(this.S); this.after = []; this.strength = 0; this.root = true; this.bornB = 0; this.bornA = 0; this.dt = 1 / hz;
    }
    setTint(tint) { this.col = this.S.color(tint); }
    /** One frame; stall > 0 makes it a long frame of that many seconds. */
    frame(stall = 0) {
      const dt = stall > 0 ? stall : 1 / this.hz;
      if (stall > 0) this.base += stall - 1 / this.hz;
      this.k++; this.t = this.base + this.k / this.hz; this.dt = dt;
      const T = f32(T0 + this.t);
      stepAll(this.before, dt);
      this.bornB = 0; this.bornA = 0;
      if (this.root) {
        this.bornB = emitBefore(this.id, this.before, this.R, T, dt, this.strength, this.col);
        const r = this.loop.draw(T, this.strength, this.col); this.after = r.parts; this.bornA = r.born;
      } else this.after = [];
    }
    partsBefore() { return this.before.map(shadeBefore); }
    partsAfter() { return this.after; }
  }

  // ---- drawing ----------------------------------------------------------------------------------------------------------------------
  const SPARKLE = [[0, 0, 1], [-1, 0, 0.8], [1, 0, 0.8], [0, -1, 0.8], [0, 1, 0.8], [-2, 0, 0.35], [2, 0, 0.35], [0, -2, 0.35], [0, 2, 0.35]]; // tools/art/scene.mjs sparkleDot
  const ACC = new Float32Array(W * H * 3), BLACK = new Float32Array(W * H * 3);
  let ROOM = BLACK;
  const brightCanvas = document.createElement('canvas'); brightCanvas.width = W; brightCanvas.height = H;
  const bctx = brightCanvas.getContext('2d');
  const IMG = new ImageData(W, H), BIMG = new ImageData(W, H);
  /** The pixels a set of particles lights (for counts and comparisons). */
  function pixelsOf(parts) {
    const set = new Set();
    for (const p of parts) {
      const [c, r] = pix(p.x, p.y);
      const pts = p.sp ? SPARKLE : [[0, 0, 1]];
      for (const [dx, dy] of pts) { const x = c + dx, y = r + dy; if (x >= 0 && x < W && y >= 0 && y < H) set.add(y * W + x); }
    }
    return set;
  }
  const onScreen = (parts) => { let n = 0; for (const p of parts) { const [c, r] = pix(p.x, p.y); if (c >= 0 && c < W && r >= 0 && r < H) n++; } return n; };
  /** Additive draw over the backdrop (SrcAlpha One), clamp for the display, plus an approximate bloom of what is above 1. */
  function render(canvas, parts, bg, bloom) {
    ACC.set(bg);
    for (const p of parts) {
      const [c, r] = pix(p.x, p.y), k = p.a;
      const pts = p.sp ? SPARKLE : [[0, 0, 1]];
      for (const [dx, dy, w] of pts) {
        const x = c + dx, y = r + dy;
        if (x < 0 || x >= W || y < 0 || y >= H) continue;
        const o = (y * W + x) * 3, m = k * w;
        ACC[o] += p.r * m; ACC[o + 1] += p.g * m; ACC[o + 2] += p.b * m;
      }
    }
    const d = IMG.data, b = BIMG.data;
    for (let i = 0, o = 0; i < W * H; i++, o += 3) {
      const q = i * 4;
      d[q] = Math.min(255, ACC[o] * 255 + 0.5); d[q + 1] = Math.min(255, ACC[o + 1] * 255 + 0.5); d[q + 2] = Math.min(255, ACC[o + 2] * 255 + 0.5); d[q + 3] = 255;
      if (bloom) {
        // threshold 1.0 (SceneBuilder volume); stored at half scale so up to 3.0 fits in a byte, drawn twice
        b[q] = Math.min(255, Math.max(0, ACC[o] - 1) * 127.5); b[q + 1] = Math.min(255, Math.max(0, ACC[o + 1] - 1) * 127.5);
        b[q + 2] = Math.min(255, Math.max(0, ACC[o + 2] - 1) * 127.5); b[q + 3] = 255;
      }
    }
    const ctx = canvas.getContext('2d');
    ctx.putImageData(IMG, 0, 0);
    if (bloom) {
      bctx.putImageData(BIMG, 0, 0);
      ctx.save(); ctx.globalCompositeOperation = 'lighter';
      ctx.filter = 'blur(1.5px)'; ctx.drawImage(brightCanvas, 0, 0); ctx.drawImage(brightCanvas, 0, 0);
      ctx.filter = 'blur(5px)'; ctx.drawImage(brightCanvas, 0, 0); ctx.drawImage(brightCanvas, 0, 0);
      ctx.restore();
    }
  }
  function canvas640(cls) { const c = document.createElement('canvas'); c.width = W; c.height = H; c.className = 'scr ' + (cls || ''); return c; }

  // ---- the scripted timelines (the evidence sheets) ---------------------------------------------------------------------------------
  // Times are seconds after the stream is switched on, taken from the cutscene scripts (SimpleCutscene.cs:97-235).
  const PLANS = {
    embers: {
      T: 6.2, strength: (t) => (t >= 0 && t < 4.5 ? 1 : 0), root: () => true, stall: 2.5, steady: [2.0, 4.5],
      events: [[0, 'on'], [4.5, 'off']],
      rows: [['A', 'filling: 0.5 s after the stream turns on', 0.5], ['B', 'steady: 3.5 s after on', 3.5], ['S', 'stall: the first frame after one 250 ms frame (separate run, stall at 2.5 s)', 'stall'],
        ['C', 'draining: 0.6 s after the stream turns off', 5.1]],
      why: 'Intro (embers = 1 for the whole story, :103) and Ending (:216). Turning off is shown for the drain; in the game the Intro ends on a fade to black and then Sparks.Clear.',
    },
    streaks: {
      T: 6.0, strength: (t) => (t < 0 ? 0 : t < 4.05 ? 1 : 0.4), root: (t) => t < 4.95, stall: 2.0, steady: [1.5, 4.05],
      events: [[0, 'on 1.0'], [4.05, '0.4'], [4.95, 'root hidden']],
      rows: [['A', 'filling: 0.3 s after streaks = 1', 0.3], ['B', 'steady at 1.0: 3.0 s', 3.0], ['S', 'stall: the first frame after one 250 ms frame (separate run, stall at 2.0 s)', 'stall'],
        ['C', 'at 0.4: 0.75 s after the drop, the room rising back (:174)', 4.8], ['D', 'end of Descent: 0.1 s after the story root is hidden (:182)', 5.05]],
      why: 'Descent: streaks = 1 as the room leaves (:151), 0.4 while it rises back 4.05 s later (:174, after 0.9 + 0.45 + 0.3 + 2.4 s), root hidden 0.9 s after that (:182).',
    },
    rune: {
      T: 5.5, strength: (t) => (t < 0 ? 0 : t < 3.15 ? 1 : t < 3.6 ? 1 - (t - 3.15) / 0.45 : 0), root: (t) => t < 4.05, stall: 2.0, steady: [1.5, 3.15],
      events: [[0, 'on'], [3.15, 'tween 1 to 0'], [4.05, 'root hidden']],
      rows: [['A', 'filling: 0.4 s after runeOn = 1', 0.4], ['B', 'steady: 2.6 s', 2.6], ['S', 'stall: the first frame after one 250 ms frame (separate run, stall at 2.0 s)', 'stall'],
        ['C', 'runeOn tween to 0 just ended (0.45 s, :177)', 3.6], ['D', 'end of Descent: 0.1 s after the story root is hidden (:182)', 4.15]],
      why: 'Descent: runeOn = 1 with the depth name (:163), tweened to 0 over 0.45 s as the room rises (:177, 3.15 s later), root hidden 0.9 s after that (:182).',
    },
  };
  const SEED = 5202, DEPTH = 2;

  /** Runs a plan; calls onFrame(sim) after every frame. stallAt: one 250 ms frame at that time. */
  function runPlan(id, hz, onFrame, stallAt = null, tint = hex(ACCENT[DEPTH - 1])) {
    const P = PLANS[id], sim = new Sim(id, hz, SEED, tint);
    let stalled = stallAt == null;
    while (sim.t < P.T - 1e-9) {
      const next = sim.base + (sim.k + 1) / hz;
      const doStall = !stalled && next >= stallAt - 1e-9;
      // the scripts set the strengths; Update reads them at the frame's time
      const tNext = doStall ? sim.t + 0.25 : next;
      sim.strength = P.strength(tNext); sim.root = P.root(tNext);
      sim.frame(doStall ? 0.25 : 0);
      if (doStall) stalled = true;
      if (onFrame(sim, doStall) === false) break;
    }
    return sim;
  }
  const mean = (a) => a.reduce((s, v) => s + v, 0) / Math.max(1, a.length);
  const sd = (a) => { const m = mean(a); return Math.sqrt(mean(a.map((v) => (v - m) * (v - m)))); };

  /** Builds the sheet for one stream at one rate into #sheet; returns its numbers. */
  function sheet(id, hz) {
    const P = PLANS[id], S = STREAMS[id], el = document.getElementById('sheet'), bloom = true, bg = S.room ? ROOM : BLACK;
    el.innerHTML = '';
    const head = document.createElement('div'); head.className = 'sheet-head';
    head.innerHTML = `<h2>${S.title} at ${hz} Hz <span class="dim">fixed step ${(1000 / hz).toFixed(1)} ms, seed ${SEED}, depth ${DEPTH} tint ${ACCENT[DEPTH - 1]}, approximate bloom on${S.room ? ', cutscene room backdrop' : ', black backdrop'}</span></h2>
      <p class="dim">${S.use}. ${P.why}</p>
      <div class="cols"><div><b>BEFORE</b>: random <code>Sparks.I.Emit</code> each frame (40b8a469)</div><div><b>AFTER</b>: <code>Loop</code>, a function of the clock (shipped)</div></div>`;
    el.appendChild(head);
    const series = [], frames = {}, stats = { id, hz };
    const capture = (key, sim) => { frames[key] = { t: sim.t, before: sim.partsBefore(), after: sim.partsAfter().slice(), s: sim.strength, root: sim.root }; };
    let hideIdx = -1, prevRoot = true;
    runPlan(id, hz, (sim) => {
      const b = sim.partsBefore(), a = sim.partsAfter();
      series.push({ t: sim.t, b: onScreen(b), a: onScreen(a), bornB: sim.bornB, bornA: sim.bornA });
      if (prevRoot && !sim.root) hideIdx = series.length - 1;
      prevRoot = sim.root;
      for (const [k, , at] of P.rows) if (typeof at === 'number' && !frames[k] && sim.t >= at - 1e-9) capture(k, sim);
    });
    runPlan(id, hz, (sim, stalled) => { if (stalled) { capture('S', sim); stats.stall = { bornBefore: sim.bornB, bornAfter: sim.bornA, onBefore: onScreen(sim.partsBefore()), onAfter: onScreen(sim.partsAfter()) }; return false; } }, P.stall);
    const win = series.filter((f) => f.t >= P.steady[0] && f.t < P.steady[1]);
    stats.steady = { from: P.steady[0], to: P.steady[1], before: { mean: +mean(win.map((f) => f.b)).toFixed(2), sd: +sd(win.map((f) => f.b)).toFixed(2) },
      after: { mean: +mean(win.map((f) => f.a)).toFixed(2), sd: +sd(win.map((f) => f.a)).toFixed(2) } };
    // births per second over the steady window: the emitter is random, the loop is spread evenly
    stats.births = { before: +(win.reduce((s, f) => s + f.bornB, 0) / (P.steady[1] - P.steady[0])).toFixed(1), after: +(win.reduce((s, f) => s + f.bornA, 0) / (P.steady[1] - P.steady[0])).toFixed(1) };
    if (hideIdx > 0) {
      const pre = series[hideIdx - 1], post = series[hideIdx];
      const restB = series.slice(hideIdx).findIndex((f) => f.b === 0);
      stats.hide = { onBefore: pre.b, onAfter: pre.a, nextBefore: post.b, nextAfter: post.a, beforeLastsMs: restB < 0 ? null : Math.round((series[hideIdx + restB].t - series[hideIdx].t) * 1000) };
    }
    for (const [k, label] of P.rows) {
      const f = frames[k]; if (!f) continue;
      const row = document.createElement('div'); row.className = 'row';
      const cb = canvas640(), ca = canvas640();
      render(cb, f.before, bg, bloom); render(ca, f.after, bg, bloom);
      const nb = onScreen(f.before), na = onScreen(f.after);
      const extra = k === 'S' ? ` &middot; born in that frame: before ${stats.stall.bornBefore}, after ${stats.stall.bornAfter}` : '';
      row.innerHTML = `<div class="rl"><b>${k}</b> ${label} &middot; t = ${f.t.toFixed(3)} s &middot; strength ${f.s.toFixed(2)}${f.root ? '' : ' &middot; root hidden'} &middot; on screen: before <b>${nb}</b>, after <b>${na}</b>${extra}</div>`;
      const pair = document.createElement('div'); pair.className = 'pair2'; pair.append(cb, ca); row.appendChild(pair);
      el.appendChild(row);
    }
    el.appendChild(chart(series, P, stats));
    stats.rows = Object.fromEntries(Object.entries(frames).map(([k, f]) => [k, { t: +f.t.toFixed(4), before: onScreen(f.before), after: onScreen(f.after) }]));
    stats.afterSameAcrossHz = hzCheck(id).after;
    return stats;
  }

  /** Particles on screen over time: before solid with dots, after dashed; labelled at the line ends (not colour alone). */
  function chart(series, P, stats) {
    const c = document.createElement('canvas'), w = 1288, h = 190; c.width = w; c.height = h; c.className = 'chart';
    const g = c.getContext('2d'), L = 44, R = 16, T = 36, B = 26;
    const max = Math.max(4, ...series.map((f) => Math.max(f.b, f.a))) * 1.15, t0 = -0.2, t1 = P.T;
    const X = (t) => L + ((t - t0) / (t1 - t0)) * (w - L - R), Y = (v) => h - B - (v / max) * (h - T - B);
    g.fillStyle = '#120d16'; g.fillRect(0, 0, w, h);
    g.font = '11px system-ui, sans-serif'; g.textBaseline = 'middle';
    g.strokeStyle = '#3a2c3c'; g.lineWidth = 1; g.fillStyle = '#9a8a7a';
    for (let v = 0; v <= max; v += max > 60 ? 20 : 10) { g.beginPath(); g.moveTo(L, Y(v) + 0.5); g.lineTo(w - R, Y(v) + 0.5); g.stroke(); g.fillText(String(v), 8, Y(v)); }
    for (let t = 0; t <= t1; t += 1) { g.fillText(t + ' s', X(t) - 8, h - 10); }
    g.setLineDash([2, 3]); g.strokeStyle = '#8a7a6a';
    for (const [t, label] of P.events) { g.beginPath(); g.moveTo(X(t) + 0.5, T); g.lineTo(X(t) + 0.5, h - B); g.stroke(); g.fillStyle = '#d8c8a8'; g.fillText(label, X(t) + 4, T - 6); }
    for (const [k, , at] of P.rows) if (typeof at === 'number') { g.fillStyle = '#f0c050'; g.fillText(k, X(at) - 3, h - B - 8); }
    g.fillStyle = '#9a8a7a'; g.fillText('steady window', X(P.steady[0]) + 2, h - B + 2);
    g.setLineDash([]); g.strokeStyle = '#5a4a3a'; g.beginPath(); g.moveTo(X(P.steady[0]), h - B - 1.5); g.lineTo(X(P.steady[1]), h - B - 1.5); g.stroke();
    const line = (key, color, dash, width) => {
      g.setLineDash(dash); g.strokeStyle = color; g.lineWidth = width; g.beginPath();
      series.forEach((f, i) => (i ? g.lineTo(X(f.t), Y(f[key])) : g.moveTo(X(f.t), Y(f[key])))); g.stroke();
    };
    line('b', '#f0a050', [], 1.5); line('a', '#78c8f8', [6, 3], 2);
    g.setLineDash([]);
    // legend, top right: line pattern and name, so the two series do not rely on colour
    const lx = w - R - 250;
    g.lineWidth = 1.5; g.strokeStyle = '#f0a050'; g.beginPath(); g.moveTo(lx, 9); g.lineTo(lx + 28, 9); g.stroke();
    g.fillStyle = '#f0a050'; g.fillText('before: solid', lx + 34, 9);
    g.lineWidth = 2; g.setLineDash([6, 3]); g.strokeStyle = '#78c8f8'; g.beginPath(); g.moveTo(lx + 125, 9); g.lineTo(lx + 153, 9); g.stroke(); g.setLineDash([]);
    g.fillStyle = '#78c8f8'; g.fillText('after: dashed', lx + 159, 9);
    g.fillStyle = '#e8dcc8'; g.font = '12px system-ui, sans-serif';
    const s = stats.steady;
    g.fillText(`Particles on screen.  Steady window ${s.from}-${s.to} s: before mean ${s.before.mean}, sd ${s.before.sd}; after mean ${s.after.mean}, sd ${s.after.sd}.` +
      (stats.hide ? `  Root hidden: before ${stats.hide.onBefore} fly on for ${stats.hide.beforeLastsMs} ms, after ${stats.hide.onAfter} vanish.` : ''), L, 9);
    return c;
  }

  /** The loop is a function of the clock: at the same t its pixels are the same at 30, 60 and 144 Hz. Differing pixels per pair. */
  function hzCheck(id) {
    const at = 3.0, sets = { before: {}, after: {} };
    for (const hz of [30, 60, 144]) runPlan(id, hz, (sim) => { if (sim.t >= at - 1e-9) { sets.before[hz] = pixelsOf(sim.partsBefore()); sets.after[hz] = pixelsOf(sim.partsAfter()); return false; } });
    const diff = (a, b) => { let n = 0; for (const v of a) if (!b.has(v)) n++; for (const v of b) if (!a.has(v)) n++; return n; };
    const out = {};
    for (const k of ['before', 'after']) out[k] = { t: at, '30v60': diff(sets[k][30], sets[k][60]), '60v144': diff(sets[k][60], sets[k][144]), '30v144': diff(sets[k][30], sets[k][144]) };
    return out;
  }

  // ---- dust: Px.Snap then a SpriteRenderer (before) vs SparkMesh.Put's rounding (after) ----------------------------------------------
  const dustHash = (a, b) => { const v = f32(Math.abs(f32(f32(Math.sin(f32(f32(a * f32(127.1)) + f32(b * f32(311.7))))) * f32(43758.5453)))); return f32(v % 1); };
  function dustXY(i, t) {
    const y = f32(30 + f32(f32(dustHash(i, 3) * 300 + t * (5 + (i % 5) * 2.5)) % 220)), tt = f32((y - 20) / 250);
    const x = f32(320 - 10 * tt + (dustHash(i, 7) - 0.5) * 2 * (30 + 60 * tt) + f32(Math.sin(f32(t * 0.7 + i))) * 4);
    return [x, y];
  }
  const dustBefore = (x, y) => { const w = At(x, y); return [roundEven(w[0]), roundEven(w[1])]; };          // Px.Snap(Px.At(x, y)), 1 px dot centred there
  const dustAfter = (x, y) => { const q = At(x, y); return [roundEven(q[0]), roundEven(q[1])]; };           // SparkMesh.Put: Mathf.Round(q.pos.x / y)
  function dustCheck() {
    const out = {};
    for (const hz of [30, 60, 144]) {
      let frames = 0, differ = 0;
      for (let k = 1; k <= 20 * hz; k++) {
        const t = f32(T0 + k / hz);
        for (let i = 0; i < 46; i++) { const [x, y] = dustXY(i, t), b = dustBefore(x, y), a = dustAfter(x, y); if (b[0] !== a[0] || b[1] !== a[1]) differ++; }
        frames++;
      }
      out[hz] = { frames, motes: 46, differingPixels: differ };
    }
    return out;
  }

  // ---- live view ----------------------------------------------------------------------------------------------------------------------
  const live = { hz: 60, paused: false, strength: { embers: 1, streaks: 1, rune: 1 }, root: true, bloom: true, room: true, zoom: 1, depth: DEPTH, sims: {}, views: {}, acc: 0, last: 0, stall: false };
  function liveReset() {
    const tint = hex(ACCENT[live.depth - 1]);
    for (const id of IDS) { live.sims[id] = new Sim(id, live.hz, SEED, tint, 0); }
    live.acc = 0;
  }
  function liveFrame() {
    for (const id of IDS) { const s = live.sims[id]; s.strength = live.strength[id]; s.root = live.root; s.frame(live.stall ? 0.25 : 0); }
    live.stall = false;
  }
  function liveDraw() {
    for (const id of IDS) {
      const s = live.sims[id], v = live.views[id], bg = STREAMS[id].room && live.room ? ROOM : BLACK;
      const b = s.partsBefore(), a = s.partsAfter();
      render(v.b, b, bg, live.bloom); render(v.a, a, bg, live.bloom);
      v.nb.textContent = `${onScreen(b)} on screen`; v.na.textContent = `${onScreen(a)} on screen`;
    }
    document.getElementById('clock').textContent = `t = ${live.sims.embers.t.toFixed(2)} s at ${live.hz} Hz${live.paused ? ' (paused)' : ''}`;
  }
  function tick(now) {
    if (!live.paused) {
      live.acc += Math.min(0.25, (now - (live.last || now)) / 1000);
      const dt = 1 / live.hz;
      if (live.stall) { liveFrame(); live.acc = 0; }
      while (live.acc >= dt) { liveFrame(); live.acc -= dt; }
      liveDraw();
    }
    live.last = now;
    requestAnimationFrame(tick);
  }
  function buildLive() {
    const host = document.getElementById('live');
    for (const id of IDS) {
      const S = STREAMS[id], fig = document.createElement('section'); fig.className = 'stream';
      fig.innerHTML = `<h3>${S.title} <span class="dim">${S.use}. ${S.rate}/s, life ${S.life} s.</span></h3>`;
      const pair = document.createElement('div'); pair.className = 'pair2';
      const mk = (label) => { const f = document.createElement('figure'), c = canvas640(), cap = document.createElement('figcaption'), n = document.createElement('span'); cap.innerHTML = `<b>${label}</b> &middot; `; cap.appendChild(n); f.append(c, cap); pair.appendChild(f); return [c, n]; };
      const [b, nb] = mk('BEFORE: random Emit'), [a, na] = mk('AFTER: Loop');
      fig.appendChild(pair); host.appendChild(fig);
      live.views[id] = { b, a, nb, na };
    }
  }
  function bar() {
    const el = document.getElementById('bar');
    const btn = (text, on, fn, title) => { const b = document.createElement('button'); b.textContent = text; if (on) b.classList.add('on'); if (title) b.title = title; b.onclick = () => { fn(); bar(); liveDraw(); }; return b; };
    const group = (label, ...items) => { const s = document.createElement('span'); s.className = 'grp'; s.append(label, ...items); return s; };
    el.innerHTML = '';
    el.append(
      group('Rate ', ...[30, 60, 144].map((hz) => btn(`${hz} Hz`, live.hz === hz, () => { live.hz = hz; for (const s of Object.values(live.sims)) s.hz = hz, s.base = s.t - s.k / hz; }))),
      group('Embers ', btn(live.strength.embers ? 'on' : 'off', !!live.strength.embers, () => (live.strength.embers = live.strength.embers ? 0 : 1))),
      group('Streaks ', ...[1, 0.4, 0].map((v) => btn(v ? v.toFixed(1) : 'off', live.strength.streaks === v, () => (live.strength.streaks = v)))),
      group('Rune motes ', btn(live.strength.rune ? 'on' : 'off', !!live.strength.rune, () => (live.strength.rune = live.strength.rune ? 0 : 1))),
      group('Story root ', btn(live.root ? 'shown' : 'hidden', !live.root, () => (live.root = !live.root), 'Hide it as Descent ends: the loops vanish with it, emitted particles fly on')),
      group('', btn('Stall 250 ms', false, () => (live.stall = true), 'One long frame, as a GC pause or a busy tab gives')),
      group('', btn(live.paused ? 'Play' : 'Pause', live.paused, () => (live.paused = !live.paused)), btn('Step 1 frame', false, () => { liveFrame(); }), btn('Restart', false, () => liveReset())),
      group('Bloom ', btn(live.bloom ? 'on (approx.)' : 'off', live.bloom, () => (live.bloom = !live.bloom))),
      group('Ember backdrop ', btn(live.room ? 'room' : 'black', live.room, () => (live.room = !live.room))),
      group('Zoom ', ...[1, 2].map((z) => btn(`${z}x`, live.zoom === z, () => { live.zoom = z; document.body.style.setProperty('--z', z); }))),
      group('Depth tint ', ...[2, 3, 4, 5].map((d) => btn(String(d), live.depth === d, () => { live.depth = d; for (const s of Object.values(live.sims)) s.setTint(hex(ACCENT[d - 1])); }))),
    );
    const clock = document.createElement('span'); clock.id = 'clock'; clock.className = 'dim'; el.appendChild(clock);
  }

  async function main() {
    const img = new Image(); img.src = window.ROOM_PNG; await img.decode();
    const c = document.createElement('canvas'); c.width = W; c.height = H; const g = c.getContext('2d'); g.drawImage(img, 0, 0);
    const d = g.getImageData(0, 0, W, H).data; ROOM = new Float32Array(W * H * 3);
    for (let i = 0; i < W * H; i++) { ROOM[i * 3] = d[i * 4] / 255; ROOM[i * 3 + 1] = d[i * 4 + 1] / 255; ROOM[i * 3 + 2] = d[i * 4 + 2] / 255; }
    const q = new URLSearchParams(location.search);
    window.LOOPS = { sheet, hzCheck, dustCheck, STREAMS, PLANS, live };
    const dust = dustCheck();
    document.getElementById('dust').textContent = [30, 60, 144].map((hz) => `${hz} Hz: ${dust[hz].differingPixels} of ${dust[hz].frames * 46} mote positions differ`).join('; ') + '.';
    window.LOOPS.dust = dust;
    if (q.get('shot')) {
      document.body.classList.add('shot');
      const hz = +(q.get('hz') || 60);
      window.LOOPS.stats = sheet(q.get('shot'), hz);
      document.body.dataset.ready = '1';
      return;
    }
    if (q.get('hz')) live.hz = +q.get('hz');
    buildLive(); liveReset(); bar();
    if (q.get('at')) {
      const at = +q.get('at');
      while (live.sims.embers.t < at - 1e-9) liveFrame();
      live.paused = true; bar(); liveDraw();
      document.body.dataset.ready = '1';
      return;
    }
    liveDraw(); document.body.dataset.ready = '1';
    requestAnimationFrame(tick);
  }
  main().catch((e) => { console.error('loops.js: ' + (e && e.stack || e)); });
})();
