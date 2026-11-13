// Athanor depth mockup: the workshop + bench rebuilt from the real Unity PNGs as planes at different depths.
// A: layered parallax 2D (orthographic, per-layer offset, pixel-snapped). B: perspective 2.5D (sprite planes at real
// depths). C: a descent down a stair through the depth layers. Everything renders at 640x360 and is upscaled with
// nearest-neighbour by CSS. The HUD is a separate flat 640x360 canvas on top that never moves.
// URL options (for screenshots): ?v=A|B|C  &pose=x,y (-1..1)  &still=1 (frozen time, no sway)  &t=0..1 (descent)
'use strict';
window.__depth = { ready: false, errors: [] };
window.addEventListener('error', (e) => window.__depth.errors.push(String(e.message)));

(async function main() {
  const THREE = await import('https://cdn.jsdelivr.net/npm/three@0.160.0/build/three.module.js');
  const ART = window.ART, LAY = ART.layout;
  const W = 640, H = 360;
  const D = 600;                                       // rest camera distance to the reference plane z = 0 (1 unit = 1 art px there)
  const FOV = 2 * Math.atan(H / 2 / D) * 180 / Math.PI;
  const q = new URLSearchParams(location.search);

  // ---- the game's per-depth mood (GameFlow.Ambient / Moon / Accent) -------------------------------------------------
  const AMBIENT = [[0.52, 0.52, 1], [0.40, 0.62, 0.86], [0.62, 0.42, 0.95], [0.86, 0.40, 0.46], [0.78, 0.74, 0.58]];
  const MOON = [0.62, 0.5, 0.4, 0.3, 0.5];
  const DEPTH_NAMES = ['The Workshop', 'The Cellar', 'The Undercroft', 'The Furnace Deep', 'The Last Vault'];
  const ROMAN = ['I', 'II', 'III', 'IV', 'V'];

  // ---- live settings ------------------------------------------------------------------------------------------------
  const S = {
    variant: (q.get('v') || 'B').toUpperCase(),
    still: q.get('still') === '1',
    mouse: true, sway: true,
    pose: q.get('pose') ? q.get('pose').split(',').map(Number) : null,
    amp: 26, ampY: 12,           // camera travel in px at the reference plane
    strength: 1,                 // A: parallax multiplier
    snap: true,                  // A: round each layer's offset to whole pixels
    tilt: 1,                     // B: 1 = orbit around the bench (bench stays put), 0 = pure slide
    dolly: 0,                    // B: camera forward (+) / back (-) in px
    zK: 1,                       // B/C: how much depth counts in light falloff (A always 0, like Light2D)
    fogOn: true, fogAmt: 0.5, fogNear: 650, fogFar: 1050,
    sheets: true, sheetAlpha: 0.55,
    shaftAlpha: 0.12,
    tableTilt: 1,                // 0 = the table is one flat card, 1 = tilted to match its drawn perspective
    depth: 1,
    hud: true,
    descent: q.get('t') ? Number(q.get('t')) : 0, playing: false,
  };

  // ---- layers (the panel lists these; z is distance in front of (+) or behind (-) the reference plane) ---------------
  const LAYERS = [
    { id: 'wall', label: 'Workshop wall', z: -400, min: -1200, max: -150 },
    { id: 'window', label: 'Window + moon', z: -398, min: -2400, max: -150 },
    { id: 'sconce', label: 'Wall candles', z: -392, min: -1200, max: -100 },
    { id: 'shaft', label: 'Moonbeam volume', z: -150, min: -380, max: 0, hint: 'near end; far end at the window' },
    { id: 'fogBack', label: 'Fog sheet, back', z: -300, min: -700, max: -60 },
    { id: 'table', label: 'Table + slab', z: -60, min: -300, max: 150, hint: 'front edge; back follows the tilt' },
    { id: 'bench', label: 'Bench: materials', z: null, hint: 'stands on the table rows' },
    { id: 'candles', label: 'Table candles', z: null, hint: 'stand on the table rows' },
    { id: 'dust', label: 'Dust in the beam', z: -70, min: -300, max: 100, hint: 'near bound; far bound at the wall' },
    { id: 'fogFront', label: 'Fog sheet, front', z: 40, min: -60, max: 300 },
    { id: 'near', label: 'Near motes (foreground)', z: 260, min: 0, max: 480 },
    { id: 'stair', label: 'Descent stair (C)', z: null },
  ];
  const layer = Object.fromEntries(LAYERS.map((l) => [l.id, Object.assign(l, { on: true })]));

  // ---- renderer -----------------------------------------------------------------------------------------------------
  const view = document.getElementById('view'), hudCanvas = document.getElementById('hud');
  const renderer = new THREE.WebGLRenderer({ canvas: view, antialias: false, alpha: false, preserveDrawingBuffer: true });
  renderer.setPixelRatio(1);
  renderer.setSize(W, H, false);
  renderer.outputColorSpace = THREE.LinearSRGBColorSpace;   // pass the art's values through untouched, like a 2D sprite
  renderer.setClearColor(0x0d0a12, 1);
  const scene = new THREE.Scene();
  const persp = new THREE.PerspectiveCamera(FOV, W / H, 1, 6000);
  const ortho = new THREE.OrthographicCamera(-W / 2, W / 2, H / 2, -H / 2, -6000, 6000);
  ortho.position.set(0, 0, 1000);

  // ---- art ----------------------------------------------------------------------------------------------------------
  const loadImg = (src) => new Promise((res, rej) => { const i = new Image(); i.onload = () => res(i); i.onerror = rej; i.src = src; });
  const IMG = {};
  await Promise.all(Object.entries(ART.img).map(async ([k, v]) => { IMG[k] = await loadImg(v); }));
  for (const [name, b64] of Object.entries(ART.font)) {
    const bin = Uint8Array.from(atob(b64), (c) => c.charCodeAt(0));
    const f = new FontFace(name, bin.buffer); await f.load(); document.fonts.add(f);
  }
  const texCache = new Map();
  function tex(src, wrap) {
    const key = (typeof src === 'string' ? src : src.__id || (src.__id = 'c' + Math.random())) + (wrap || '');
    if (texCache.has(key)) return texCache.get(key);
    const t = new THREE.Texture(typeof src === 'string' ? IMG[src] : src);
    t.magFilter = t.minFilter = THREE.NearestFilter; t.generateMipmaps = false; t.colorSpace = THREE.NoColorSpace;
    t.wrapS = t.wrapT = wrap === 'mirror' ? THREE.MirroredRepeatWrapping : wrap === 'repeat' ? THREE.RepeatWrapping : THREE.ClampToEdgeWrapping;
    t.needsUpdate = true; texCache.set(key, t); return t;
  }
  const white = document.createElement('canvas'); white.width = white.height = 1;
  white.getContext('2d').fillStyle = '#fff'; white.getContext('2d').fillRect(0, 0, 1, 1);

  // procedural pixel fog: tileable value noise, 4 alpha steps with a 4x4 Bayer dither (no smooth gradients)
  function fogCanvas(w, h, seed) {
    const c = document.createElement('canvas'); c.width = w; c.height = h;
    const g = c.getContext('2d'), id = g.createImageData(w, h);
    const hash = (x, y) => { const s = Math.sin(x * 127.1 + y * 311.7 + seed * 74.7) * 43758.5453; return s - Math.floor(s); };
    const noise = (x, y, p) => {
      const xi = Math.floor(x), yi = Math.floor(y), fx = x - xi, fy = y - yi, sx = fx * fx * (3 - 2 * fx), sy = fy * fy * (3 - 2 * fy);
      const v = (a, b) => hash(((a % p) + p) % p, b);
      return (v(xi, yi) * (1 - sx) + v(xi + 1, yi) * sx) * (1 - sy) + (v(xi, yi + 1) * (1 - sx) + v(xi + 1, yi + 1) * sx) * sy;
    };
    const bayer = [0, 8, 2, 10, 12, 4, 14, 6, 3, 11, 1, 9, 15, 7, 13, 5];
    for (let y = 0; y < h; y++) for (let x = 0; x < w; x++) {
      let n = 0, amp = 0.55, fq = 1;
      for (let o = 0; o < 4; o++) { const p = 6 * fq; n += amp * noise((x / w) * p, (y / h) * 3 * fq + o * 7, p); amp *= 0.5; fq *= 2; }
      const band = Math.sin(Math.PI * y / h);                 // thick in the middle, gone at the top and bottom edge
      let v = Math.max(0, n * 1.25 - 0.3) * band * band;
      v = Math.floor(v * 4 + bayer[(y % 4) * 4 + (x % 4)] / 16) / 4;
      const i = (y * w + x) * 4; id.data[i] = id.data[i + 1] = id.data[i + 2] = 255; id.data[i + 3] = Math.min(255, v * 255);
    }
    g.putImageData(id, 0, 0); return c;
  }
  // stone for the stair: brick rows cut from the real wall
  function wallCrop(x, y, w, h) {
    const c = document.createElement('canvas'); c.width = w; c.height = h;
    c.getContext('2d').drawImage(IMG.workshop_wall, x, y, w, h, 0, 0, w, h); return c;
  }

  // ---- shared shader --------------------------------------------------------------------------------------------------
  const MAXL = 8;
  const U = {   // uniforms shared by every sprite material
    lp: { value: Array.from({ length: MAXL }, () => new THREE.Vector4()) },   // world xyz, radius
    lc: { value: Array.from({ length: MAXL }, () => new THREE.Vector4()) },   // colour * intensity, vertical squash
    zK: { value: 1 },
    ambient: { value: new THREE.Vector3() },
    moonMap: { value: tex('moon_shaft') },
    moonCol: { value: new THREE.Vector3() },
    fogCol: { value: new THREE.Vector3() },
  };
  const VS = `
    attribute vec2 q; uniform vec4 rect; varying vec2 vUv; varying vec2 vArt; varying vec3 vWorld;
    void main() {
      vUv = uv; vArt = rect.xy + vec2(q.x, 1.0 - q.y) * rect.zw;
      vec4 w = modelMatrix * vec4(position, 1.0); vWorld = w.xyz;
      gl_Position = projectionMatrix * viewMatrix * w;
    }`;
  const FS = `
    uniform sampler2D map; uniform vec4 tint; uniform float lit, moonOn, emit, fog, add;
    uniform vec4 lp[${MAXL}]; uniform vec4 lc[${MAXL}]; uniform float zK;
    uniform vec3 ambient, moonCol, fogCol; uniform sampler2D moonMap;
    varying vec2 vUv; varying vec2 vArt; varying vec3 vWorld;
    void main() {
      vec4 t = texture2D(map, vUv) * tint;
      if (t.a < 0.004) discard;
      vec3 c = t.rgb;
      if (lit > 0.5) {
        vec3 L = ambient + vec3(emit);
        for (int i = 0; i < ${MAXL}; i++) {
          vec3 d = vWorld - lp[i].xyz; d.y /= max(lc[i].w, 0.1); d.z *= zK;
          float a = clamp(1.0 - length(d) / max(lp[i].w, 1.0), 0.0, 1.0);
          L += lc[i].rgb * pow(a, 1.25);
        }
        if (moonOn > 0.5 && vArt.x >= 0.0 && vArt.x <= 640.0 && vArt.y >= 0.0 && vArt.y <= 360.0) {
          vec4 m = texture2D(moonMap, vec2(vArt.x / 640.0, 1.0 - vArt.y / 360.0));
          L += moonCol * m.r * m.a;
        }
        c *= L;
      }
      if (add > 0.5) { gl_FragColor = vec4(c * t.a * (1.0 - fog), 1.0); }
      else { gl_FragColor = vec4(mix(c, fogCol, fog), t.a); }
    }`;
  function quadGeo(u0, v0, u1, v1) {
    const g = new THREE.BufferGeometry();
    g.setAttribute('position', new THREE.BufferAttribute(new Float32Array([-0.5, -0.5, 0, 0.5, -0.5, 0, 0.5, 0.5, 0, -0.5, 0.5, 0]), 3));
    g.setAttribute('uv', new THREE.BufferAttribute(new Float32Array([u0, v0, u1, v0, u1, v1, u0, v1]), 2));
    g.setAttribute('q', new THREE.BufferAttribute(new Float32Array([0, 0, 1, 0, 1, 1, 0, 1]), 2));
    g.setIndex([0, 1, 2, 0, 2, 3]); return g;
  }
  const unitGeo = quadGeo(0, 0, 1, 1);

  // ---- objects --------------------------------------------------------------------------------------------------------
  // o: { layer, img, rect {x,y,w,h} in art px (top-left), z() depth, uv [u0,v0,u1,v1], lit, add, moon, emit, tint [r,g,b,a] }
  const OBJ = [];
  function add(o) {
    o.tint = o.tint || [1, 1, 1, 1];
    const t = o.texture || tex(o.img, o.wrap);
    const mat = new THREE.ShaderMaterial({
      vertexShader: VS, fragmentShader: FS, transparent: true, depthTest: false, depthWrite: false, side: THREE.DoubleSide,
      blending: o.add ? THREE.AdditiveBlending : THREE.NormalBlending,
      uniforms: Object.assign({}, U, {
        map: { value: t }, tint: { value: new THREE.Vector4(...o.tint) }, rect: { value: new THREE.Vector4(o.rect.x, o.rect.y, o.rect.w, o.rect.h) },
        lit: { value: o.lit ? 1 : 0 }, moonOn: { value: o.moon ? 1 : 0 }, emit: { value: o.emit || 0 }, fog: { value: 0 }, add: { value: o.add ? 1 : 0 },
      }),
    });
    if (o.world) { mat.depthTest = true; mat.depthWrite = !o.add; }   // the stair is real 3D geometry: it needs the depth buffer
    o.mat = mat; o.mesh = new THREE.Mesh(o.uv ? quadGeo(...o.uv) : unitGeo, mat); o.mesh.frustumCulled = false;
    o.seq = OBJ.length; scene.add(o.mesh); OBJ.push(o); return o;
  }
  const lz = (id) => () => layer[id].z;

  // table depth: screen rows of a ground plane are linear in 1/distance. The drawn table is 343 px wide at the back
  // and 436 px at the front, so the back edge is 1.27x further than the front edge.
  const TABLE = { x: 102, y: 132, w: 436, h: 145, topRows: 123 };
  function tableZ(artY) {
    const dF = D - layer.table.z, ratio = 1 + 0.27 * S.tableTilt, dB = dF * ratio;
    const r = Math.min(1, Math.max(0, (artY - TABLE.y) / (TABLE.topRows - 1)));
    const inv = 1 / dB + (1 / dF - 1 / dB) * r;
    return D - 1 / inv;
  }
  const benchAnchorZ = () => tableZ(188);   // the middle row of tiles

  // wall, overscanned with mirrored edges so camera moves never show its border
  const PADX = 96, PADY = 54;
  add({ layer: 'wall', img: 'workshop_wall', wrap: 'mirror', rect: { x: -PADX, y: -PADY, w: W + PADX * 2, h: H + PADY * 2 },
    uv: [-PADX / W, -PADY / H, 1 + PADX / W, 1 + PADY / H], z: lz('wall'), lit: true, moon: true });
  const win = LAY.placed.find((p) => p.name === 'window');
  add({ layer: 'window', img: 'window', rect: { x: win.x, y: win.y, w: win.w, h: win.h }, z: lz('window') });

  // moonbeam volume: the cookie cut into rows, each row a step nearer from the window down to the table
  const SHAFT_ROWS = 36;
  for (let i = 0; i < SHAFT_ROWS; i++) {
    const y0 = 20 + i * (262 / SHAFT_ROWS), h = 262 / SHAFT_ROWS;
    add({ layer: 'shaft', img: 'moon_shaft', rect: { x: 0, y: y0, w: W, h }, uv: [0, 1 - (y0 + h) / H, 1, 1 - y0 / H], add: true,
      z: () => layer.window.z + 2 + (layer.shaft.z - layer.window.z - 2) * (i / (SHAFT_ROWS - 1)), shaft: true });
  }

  // fog sheets (the sprite-fog alternative to AERO): pixel mist, lit by the same lights, scrolled in whole texels
  const fogA = fogCanvas(320, 90, 1), fogB = fogCanvas(320, 70, 2);
  add({ layer: 'fogBack', texture: tex(fogA, 'repeat'), rect: { x: -PADX, y: 130, w: W + 2 * PADX, h: 150 }, uv: [0, 0, 2.6, 1],
    z: lz('fogBack'), lit: true, emit: 0.35, moon: true, fogSheet: { w: 320, speed: 4 } });
  add({ layer: 'fogFront', texture: tex(fogB, 'repeat'), rect: { x: -PADX, y: 255, w: W + 2 * PADX, h: 120 }, uv: [0, 0, 2.6, 1],
    z: lz('fogFront'), lit: true, emit: 0.35, moon: true, fogSheet: { w: 320, speed: -6 }, sheetK: 0.55 });

  // table: one row of pixels per strip so each row can sit at its own depth; the front apron is upright
  const STRIP = 1;
  for (let r = 0; r < TABLE.h; r += STRIP) {
    // each strip also carries one row above and below: when neighbouring rows drift apart, the nearer one (drawn later)
    // covers the overlap, so no gap opens between them
    const h = Math.min(STRIP, TABLE.h - r), y = TABLE.y + r, r0 = Math.max(0, r - 1), r1 = Math.min(TABLE.h, r + h + 1);
    add({ layer: 'table', img: 'workshop_table', rect: { x: TABLE.x, y: TABLE.y + r0, w: TABLE.w, h: r1 - r0 }, uv: [0, 1 - r1 / TABLE.h, 1, 1 - r0 / TABLE.h],
      z: () => tableZ(Math.min(y, TABLE.y + TABLE.topRows - 1)), lit: true, moon: true });
  }

  // candles: two on the wall sconces, three on the table; each a candle sprite, a flipbook flame, a halo and a light
  const LIGHTS = [];
  LAY.candles.forEach((c, i) => {
    const onWall = c.y < 120, lay = onWall ? 'sconce' : 'candles';
    const z = onWall ? lz('sconce') : () => tableZ(c.y + 19) + 0.5;
    add({ layer: lay, img: 'candle', rect: { x: c.x, y: c.y, w: 12, h: 20 }, z, lit: true, moon: true });
    const flame = add({ layer: lay, img: 'flame_small_0', rect: { x: c.x, y: c.y - 8, w: 13, h: 10 }, z: () => z() + 0.1, add: true, tint: [1.6, 1.6, 1.6, 1], flame: i * 1.3 });
    flame.frames = [0, 1, 2, 3, 4, 5, 6, 7].map((k) => tex('flame_small_' + k));
    const halo = add({ layer: lay, img: 'glow', rect: { x: c.x + 6 - 15, y: c.y - 3 - 15, w: 30, h: 30 }, z: () => z() + 0.2, add: true, tint: [1, 0.6, 0.23, 0.55], halo: i * 1.3 });
    LIGHTS.push({ layer: lay, x: c.x + 6, y: c.y - 1, z, r: onWall ? 170 : 210, col: [1, 0.63, 0.33], I: onWall ? 1.2 : 1.55, squash: 0.8, phase: i * 1.3 });
    halo.base = 0.55;
  });

  // bench: materials standing on six tiles (shadow, glow ring, halo, icon)
  const PLACE = [[0, 'salt_crystal', '#bfe8ff'], [2, 'ember_coal', '#ff8a3c'], [3, 'brine', '#6ee6c8'], [4, 'quicksilver', '#d8e0f0'], [6, 'mandrake', '#a0e070'], [8, 'glowmoss', '#9cff6a']];
  for (const [ti, id, hex] of PLACE) {
    const t = LAY.tiles[ti], row = Math.floor(ti / 3), z = () => tableZ(t.y);
    const col = [parseInt(hex.slice(1, 3), 16) / 255, parseInt(hex.slice(3, 5), 16) / 255, parseInt(hex.slice(5, 7), 16) / 255];
    const ring = [[41, 19], [45, 21], [49, 23]][row];
    add({ layer: 'bench', img: 'tile_ring_' + row, rect: { x: t.x - ring[0] / 2, y: t.y - ring[1] / 2, w: ring[0], h: ring[1] }, z: () => z() + 0.1, add: true, tint: [col[0] * 1.6, col[1] * 1.6, col[2] * 1.6, 0.6], pulse: ti });
    add({ layer: 'bench', img: 'glow', rect: { x: t.x - 9, y: t.y + 3 - 3.5, w: 18, h: 7 }, z: () => z() + 0.2, tint: [0, 0, 0, 0.6] });
    add({ layer: 'bench', img: 'glow', rect: { x: t.x - 24, y: t.y - 8 - 24, w: 48, h: 48 }, z: () => z() + 0.3, add: true, tint: [...col, 0.35] });
    add({ layer: 'bench', img: id, rect: { x: t.x - 12, y: t.y - 21, w: 24, h: 24 }, z: () => z() + 0.4, lit: true, emit: 0.55, bob: ti });
  }

  // dust in the beam (Dust.cs, with a depth per mote) and big soft motes right in front of the camera
  const hash = (a, b) => { const s = Math.abs(Math.sin(a * 127.1 + b * 311.7) * 43758.5453); return s % 1; };
  for (let i = 0; i < 46; i++)
    add({ layer: 'dust', texture: tex(white), rect: { x: 0, y: 0, w: 1, h: 1 }, add: true, tint: [0.7, 0.85, 1, 0.5], mote: i,
      z: () => layer.window.z + 8 + (layer.dust.z - layer.window.z - 8) * hash(i, 11) });
  for (let i = 0; i < 12; i++)
    add({ layer: 'near', img: 'glow', rect: { x: 0, y: 0, w: 7, h: 7 }, add: true, tint: [0.8, 0.85, 1, 0.22], nearMote: i,
      z: () => layer.near.z + (hash(i, 5) - 0.5) * 120 });

  // ---- the descent stair (variant C): real 3D geometry below and behind the workshop, lit by three torches ----------
  const stair = [];
  {
    const riserTex = tex(wallCrop(0, 0, 240, 18), 'repeat'), treadTex = tex(wallCrop(0, 18, 240, 36), 'repeat'), sideTex = tex(wallCrop(0, 0, 160, 90), 'repeat');
    const mk = (texture, w, h, pos, rotX, rotY, tint, uvRep) => {
      const o = add({ layer: 'stair', texture, rect: { x: -9999, y: -9999, w: 1, h: 1 }, uv: [0, 0, uvRep[0], uvRep[1]], lit: true, tint, world: true });
      o.mesh.position.set(...pos); o.mesh.scale.set(w, h, 1); o.mesh.rotation.set(rotX, rotY, 0); o.worldZ = pos[2];
      stair.push(o); return o;
    };
    for (let k = 0; k < 30; k++) {
      const y = -250 - k * 26, z = -40 - k * 46;
      mk(riserTex, 240, 26, [0, y - 13, z], 0, 0, [0.8, 0.75, 0.85, 1], [1, 1]);                         // riser
      mk(treadTex, 240, 46, [0, y, z - 23], -Math.PI / 2, 0, [1, 0.95, 1, 1], [1, 1]);                  // tread
    }
    for (const sx of [-120, 120]) for (let k = 0; k < 10; k++)
      mk(sideTex, 180, 260, [sx, -330 - k * 78, -100 - k * 140], 0, sx < 0 ? Math.PI / 2 : -Math.PI / 2, [0.7, 0.66, 0.8, 1], [1.2, 2.7]);
    for (let k = 0; k < 3; k++) {
      const pz = -170 - k * 260, p = { x: k % 2 ? 108 : -108, y: -250 - 26 * ((-40 - pz - 23) / 46) + 50, z: pz };   // on the side wall, 50 px above the steps
      LIGHTS.push({ layer: 'stair', world: p, r: 260, col: [1, 0.6, 0.3], I: 1.6, squash: 1, phase: k * 2.1 });
      const g = add({ layer: 'stair', img: 'glow', rect: { x: -9999, y: -9999, w: 1, h: 1 }, add: true, tint: [1, 0.6, 0.23, 0.7], world: true, halo: k });
      g.mesh.position.set(p.x, p.y, p.z); g.mesh.scale.set(40, 40, 1); g.worldZ = p.z; g.base = 0.7; stair.push(g);
      const f = add({ layer: 'stair', img: 'flame_small_0', rect: { x: -9999, y: -9999, w: 1, h: 1 }, add: true, tint: [1.6, 1.6, 1.6, 1], world: true, flame: k * 2.1 });
      f.frames = [0, 1, 2, 3, 4, 5, 6, 7].map((n) => tex('flame_small_' + n)); f.mesh.position.set(p.x, p.y + 4, p.z + 1); f.mesh.scale.set(13, 10, 1); f.worldZ = p.z + 1;
    }
  }

  // ---- placement ------------------------------------------------------------------------------------------------------
  const smooth = (a, b, x) => { const t = Math.min(1, Math.max(0, (x - a) / (b - a))); return t * t * (3 - 2 * t); };
  const lerp = (a, b, t) => a + (b - a) * t;
  let shift = [0, 0], camPos = new THREE.Vector3(0, 0, D);

  // A: offset of a layer at depth z for a camera shift, relative to the bench (the bench never moves, so clicks stay put)
  function parallax(z) {
    const d = D - z, dA = D - benchAnchorZ();
    const k = -S.strength * (D / d - D / dA);
    let ox = shift[0] * k, oy = shift[1] * k;
    if (S.snap) { ox = Math.round(ox); oy = Math.round(oy); }
    return [ox, oy];
  }
  // B/C: a plane at depth z is scaled by its distance so that from the rest camera it covers exactly its art rect
  function placeArt(cx, cy, z) {
    if (S.variant === 'A') { const [ox, oy] = parallax(z); return [cx - W / 2 + ox, H / 2 - cy + oy, z, 1]; }
    const s = (D - z) / D; return [(cx - W / 2) * s, (H / 2 - cy) * s, z, s];
  }

  // ---- frame ----------------------------------------------------------------------------------------------------------
  let mouse = [0, 0], t0 = performance.now(), descentStart = 0;
  view.parentElement.addEventListener('pointermove', (e) => {
    const r = view.getBoundingClientRect();
    mouse = [((e.clientX - r.left) / r.width) * 2 - 1, -(((e.clientY - r.top) / r.height) * 2 - 1)];
  });
  view.parentElement.addEventListener('pointerleave', () => { mouse = [0, 0]; });

  function frame(now) {
    const time = S.still ? 2.0 : (now - t0) / 1000;
    const v = S.variant;
    if (S.playing) { S.descent = Math.min(1, (now - descentStart) / 6500); if (S.descent >= 1) { S.playing = false; S.descent = 0; syncPanel(); } }
    const desc = v === 'C' ? S.descent : 0;

    // camera intent: mouse + idle sway, or a fixed pose
    let mx = 0, my = 0;
    if (S.pose) [mx, my] = S.pose;
    else {
      if (S.mouse) { mx += mouse[0]; my += mouse[1]; }
      if (S.sway && !S.still) { mx += 0.35 * Math.sin(time * 0.31); my += 0.25 * Math.sin(time * 0.23 + 1); }
    }
    if (desc > 0) { mx = my = 0; }
    shift = [mx * S.amp, my * S.ampY];

    // mood: the depth's ambient and moon; the descent hands over to the next depth at its fogged midpoint
    let depth = S.depth, fogBoost = 0;
    if (desc > 0) { fogBoost = desc < 0.5 ? smooth(0.18, 0.46, desc) : 1 - smooth(0.54, 0.86, desc); if (desc >= 0.5) depth = S.depth % 5 + 1; }
    const amb = AMBIENT[depth - 1], moonI = MOON[depth - 1];
    U.ambient.value.set(amb[0] * 0.25, amb[1] * 0.25, amb[2] * 0.25);
    U.moonCol.value.set(0.42 * moonI, 0.6 * moonI, 1 * moonI);
    U.fogCol.value.set(amb[0] * 0.09, amb[1] * 0.09, amb[2] * 0.11);
    U.zK.value = v === 'A' ? 0 : S.zK;
    renderer.setClearColor(new THREE.Color(lerp(0x0d / 255, U.fogCol.value.x, fogBoost), lerp(0x0a / 255, U.fogCol.value.y, fogBoost), lerp(0x12 / 255, U.fogCol.value.z, fogBoost)), 1);

    // camera
    let cam;
    if (v === 'A') { cam = ortho; camPos.set(0, 0, D); }
    else {
      cam = persp;
      const za = benchAnchorZ();
      if (desc > 0) {
        if (desc < 0.5) {
          const e = smooth(0, 0.5, desc);
          persp.position.set(0, lerp(0, -330, e), lerp(D, 150, e));
          persp.lookAt(0, lerp(0, -560, e), lerp(za, -700, e));
        } else {
          const e = smooth(0.5, 1, desc);
          persp.position.set(0, lerp(150, 0, e), lerp(D + 40, D, e));
          persp.lookAt(0, lerp(-30, 0, e), za);
        }
      } else {
        persp.position.set(shift[0], shift[1], D - S.dolly);
        persp.lookAt(shift[0] * (1 - S.tilt), shift[1] * (1 - S.tilt), za);
      }
      camPos.copy(persp.position);
    }

    // lights
    const used = LIGHTS.filter((l) => layer[l.layer].on && (l.layer !== 'stair' || (v === 'C' && desc < 0.5)));
    for (let i = 0; i < MAXL; i++) {
      const l = used[i];
      if (!l) { U.lc.value[i].set(0, 0, 0, 1); U.lp.value[i].set(0, 0, -99999, 1); continue; }
      const flick = 1 + 0.08 * Math.sin(time * 9.1 + l.phase) + 0.05 * Math.sin(time * 23.7 + l.phase * 3);
      if (l.world) { U.lp.value[i].set(l.world.x, l.world.y, l.world.z, l.r); }
      else { const [x, y, z, s] = placeArt(l.x, l.y, l.z()); U.lp.value[i].set(x, y, z, l.r * s); }
      U.lc.value[i].set(l.col[0] * l.I * flick, l.col[1] * l.I * flick, l.col[2] * l.I * flick, l.squash);
    }

    // objects
    for (const o of OBJ) {
      const L = layer[o.layer];
      let visible = L.on && (o.layer !== 'stair' || (v === 'C' && desc < 0.5));
      if (o.fogSheet) visible = visible && S.sheets;
      if (o.layer === 'near' && v === 'A') visible = visible && true;
      o.mesh.visible = visible;
      if (!visible) continue;
      const u = o.mat.uniforms;
      let z, dist;
      if (o.world) { z = o.worldZ; dist = camPos.distanceTo(o.mesh.position); }
      else {
        let r = o.rect;
        if (o.mote !== undefined) {      // Dust.cs, in art space
          const i = o.mote, y = 30 + (hash(i, 3) * 300 + time * (5 + (i % 5) * 2.5)) % 220, tt = (y - 20) / 250;
          const x = 320 - 10 * tt + (hash(i, 7) - 0.5) * 2 * (30 + 60 * tt) + Math.sin(time * 0.7 + i) * 4;
          r = { x: Math.round(x), y: Math.round(y), w: 1, h: 1 };
          const s = Math.sin(time * 2 + i * 2.1); u.tint.value.w = 0.3 + 0.4 * s * s;
        }
        if (o.nearMote !== undefined) {
          const i = o.nearMote, x = (hash(i, 1) * 760 - 60 + time * (3 + hash(i, 2) * 4)) % 760 - 60, y = 40 + hash(i, 3) * 300 + Math.sin(time * 0.4 + i) * 10;
          r = { x: Math.round(x), y: Math.round(y), w: 7, h: 7 };
        }
        if (o.bob !== undefined) r = { x: o.rect.x, y: o.rect.y + Math.round(Math.sin(time * 1.6 + o.bob) * 1), w: o.rect.w, h: o.rect.h };
        z = o.z();
        const [x, y, zz, s] = placeArt(r.x + r.w / 2, r.y + r.h / 2, z);
        o.mesh.position.set(x, y, zz); o.mesh.scale.set(r.w * s, r.h * s, 1);
        if (v === 'A') {   // keep the top-left corner on a whole pixel
          o.mesh.position.x = Math.round(x - r.w / 2) + r.w / 2; o.mesh.position.y = Math.round(y + r.h / 2) - r.h / 2;
        }
        u.rect.value.set(r.x, r.y, r.w, r.h);
        dist = v === 'A' ? D - z : camPos.distanceTo(o.mesh.position);
      }
      o.sortZ = z;
      // per-layer depth fog (distance from the camera), plus the descent's fog-out
      let f = S.fogOn ? S.fogAmt * smooth(S.fogNear, S.fogFar, dist) : 0;
      f = Math.max(f, fogBoost * (o.layer === 'stair' ? smooth(250, 1100, dist) : 1));
      u.fog.value = Math.min(1, f);
      if (o.shaft) { u.tint.value.set(0.42, 0.6, 1, S.shaftAlpha * moonI / 0.62); }
      if (o.fogSheet) {
        const a = S.sheetAlpha * (o.sheetK || 1) * (0.35 + 1.4 * (S.fogOn ? S.fogAmt : 0.3)) + fogBoost * 0.5;
        u.tint.value.set(0.55 + amb[0] * 0.45, 0.55 + amb[1] * 0.45, 0.55 + amb[2] * 0.45, Math.min(1, a));
        const step = Math.floor(time * o.fogSheet.speed) / o.fogSheet.w;     // whole texels only
        const g = o.mesh.geometry.attributes.uv; const base = [0, 2.6, 2.6, 0];
        for (let k = 0; k < 4; k++) g.setX(k, base[k] + step); g.needsUpdate = true;
      }
      if (o.frames) { const fi = Math.floor(time * 10 + o.flame * 3) % 8; u.map.value = o.frames[(fi + 8) % 8]; }
      if (o.halo !== undefined) u.tint.value.w = o.base * (0.85 + 0.15 * Math.sin(time * 7 + o.halo));
      if (o.pulse !== undefined) u.tint.value.w = 0.55 + 0.25 * Math.sin(time * 3 + o.pulse);
    }
    // painter's order: far to near (ties keep creation order)
    const vis = OBJ.filter((o) => o.mesh.visible).sort((a, b) => a.sortZ - b.sortZ || a.seq - b.seq);
    vis.forEach((o, i) => { o.mesh.renderOrder = i; });

    renderer.render(scene, cam);
    drawHud(depth, desc);
    requestAnimationFrame(frame);
  }

  // ---- HUD: a flat 640x360 overlay drawn with the real UI sprites; it never moves ---------------------------------------
  const hud = hudCanvas.getContext('2d'); hud.imageSmoothingEnabled = false;
  const scratch = document.createElement('canvas'); scratch.width = 640; scratch.height = 80;
  const sg = scratch.getContext('2d', { willReadFrequently: true });
  function text(str, x, y, font, size, color, align = 'left', shadow = null) {
    sg.clearRect(0, 0, 640, 80); sg.font = `${size}px ${font}`; sg.textBaseline = 'top';
    const w = Math.ceil(sg.measureText(str).width) + 2;
    const pass = (col, dx, dy) => { sg.fillStyle = col; sg.fillText(str, 1 + dx, 1 + dy); };
    if (shadow) { pass(shadow, 1, 1); pass(shadow, 0, 1); }
    pass(color, 0, 0);
    const id = sg.getImageData(0, 0, w + 2, size + 8);
    for (let i = 3; i < id.data.length; i += 4) id.data[i] = id.data[i] > 110 ? 255 : 0;   // hard pixel edges
    sg.putImageData(id, 0, 0);
    const ax = align === 'left' ? x : align === 'right' ? x - w : x - Math.round(w / 2);
    hud.drawImage(scratch, 0, 0, w + 2, size + 8, Math.round(ax) - 1, Math.round(y) - 1, w + 2, size + 8);
  }
  const HAND = ['brine', 'sulfur', 'moonpetal', 'spark_quartz', 'iron_ore'];
  const NAMES = { brine: 'Brine', sulfur: 'Sulfur', moonpetal: 'Moonpetal', spark_quartz: 'Quartz', iron_ore: 'Iron Ore' };
  function drawHud(depth, desc) {
    hud.clearRect(0, 0, W, H);
    if (!S.hud) return;
    const pale = '#f4e6c8', dim = '#a89070', dark = '#0c0706';
    const I = (n, x, y) => hud.drawImage(IMG[n], x, y);
    I('hud_left', 8, 8);
    text('Depth ' + ROMAN[depth - 1], 16, 9, 'Alagard', 16, pale, 'left', dark);
    text(DEPTH_NAMES[depth - 1], 17, 30, 'BitPotion', 16, dim);
    text('POTENCY', 17, 52, 'PixOp8', 8, dim);
    text('140', 70, 49, 'Alagard', 16, '#f0c050', 'left', '#3a1a08');
    text('/ 300', 153, 52, 'PixOp8', 8, dim, 'right');
    hud.drawImage(IMG.bar_fill, 0, 0, 4, 5, 18, 70, 60, 5);
    I('hud_right', 480, 8);
    text('CATALYSTS', 489, 14, 'PixOp8', 8, dim);
    text('TRIALS', 489, 63, 'PixOp8', 8, dim); text('3', 532, 58, 'Alagard', 16, '#ee6a3c', 'left', dark);
    text('REDRAWS', 556, 63, 'PixOp8', 8, dim); text('2', 610, 58, 'Alagard', 16, '#6ee6c8', 'left', dark);
    I('rack', 130, 256);
    I('note', 4, 262);
    text('LAST REACTION', 10, 266, 'PixOp8', 8, '#7a5430');
    text('Brine + Salt Crystal', 10, 276, 'BitPotion', 16, '#2a1a10');
    text('HINT', 10, 318, 'PixOp8', 8, '#7a5430');
    text('Something stirs below.', 10, 328, 'BitPotion', 16, '#4a1f7a');
    const pitch = Math.round((386 - 74) / 4);
    HAND.forEach((id, k) => {
      const x = 128 + k * pitch, y = 290 - (k === 2 ? 8 : 0);
      I(k === 2 ? 'card_sel' : 'card', x, y);
      I(id, x + 25, y + 10);
      text(NAMES[id], x + 37, y + 40, 'PixOp8', 8, '#3a2414', 'center');
    });
    I('corner', 514, 284); I('book', 520, 290);
    text('Journal', 542, 287, 'Alagard', 16, pale);
    I('ignite_up', 519, 310);
    text('Ignite', 575, 316, 'Alagard', 16, '#fff6b8', 'center', '#3b0d1e');
    if (desc > 0) {   // during the descent the bench UI would be hidden in the game; keep only the depth panel
      hud.clearRect(0, 80, W, H - 80); hud.clearRect(470, 0, 170, 80);
    }
  }

  // ---- panel ----------------------------------------------------------------------------------------------------------
  const panel = document.getElementById('panel'), caption = document.getElementById('caption');
  const CAPTIONS = {
    A: '<b>A. Layered parallax 2D.</b> Orthographic camera, every layer is a flat sprite moved by its own parallax offset (rounded to whole pixels). The bench layer is the anchor and never moves, so clicks map exactly as today. The table is cut into 1 px rows that each scroll by their own amount (SNES-style line scroll), which reads as a tilted surface. Lights are planar like Light2D.',
    B: '<b>B. Perspective 2.5D.</b> Perspective camera; every sprite is a plane at a real depth, scaled so the rest view matches the 2D art pixel for pixel. The camera orbits the bench. Rendered at 640x360 and upscaled nearest-neighbour. Depth fog per layer and light falloff in depth.',
    C: '<b>C. Descent.</b> Variant B\'s scene plus real 3D stair geometry below the workshop. The camera dives down the stair into the fog; at the fogged midpoint the mood switches to the next depth, and the camera settles back onto the bench from above.',
  };
  function rowG(label, key, min, max, step, fmt = (x) => x) {
    return `<div class="row g"><label>${label}</label><input type="range" data-k="${key}" min="${min}" max="${max}" step="${step}" value="${S[key]}"><output data-o="${key}">${fmt(S[key])}</output></div>`;
  }
  const chk = (label, key) => `<div class="row c"><label><input type="checkbox" data-c="${key}" ${S[key] ? "checked" : ""}> ${label}</label></div>`;
  function buildPanel() {
    let h = '<h2>Variant</h2><div class="variants">' + ['A', 'B', 'C'].map((k) => `<button data-v="${k}" class="${S.variant === k ? 'on' : ''}">${k === 'A' ? 'A. Parallax 2D' : k === 'B' ? 'B. Perspective' : 'C. Descent'}</button>`).join('') + '</div>';
    h += '<div id="descentBox"' + (S.variant === 'C' ? '' : ' hidden') + '><p class="note">Play the descent, or scrub it.</p><div class="row g"><button class="btn" id="play">Play descent</button><span></span><span></span></div>' + rowG('Progress', 'descent', 0, 1, 0.01, (x) => Number(x).toFixed(2)) + '</div>';
    h += '<h2>Camera</h2>' + chk('Follow the mouse', 'mouse') + chk('Idle sway', 'sway') + rowG('Travel x (px)', 'amp', 0, 80, 1) + rowG('Travel y (px)', 'ampY', 0, 60, 1);
    h += '<div data-only="A">' + rowG('Parallax strength', 'strength', 0, 3, 0.05) + chk('Snap layers to whole px', 'snap') + '</div>';
    h += '<div data-only="BC">' + rowG('Orbit vs slide', 'tilt', 0, 1, 0.05) + rowG('Dolly in (px)', 'dolly', -300, 400, 5) + rowG('Light depth falloff', 'zK', 0, 2, 0.05) + '</div>';
    h += rowG('Table tilt', 'tableTilt', 0, 3, 0.05);
    h += '<h2>Mood and fog</h2>' + rowG('Depth (GameFlow.Mood)', 'depth', 1, 5, 1) + chk('Depth fog per layer', 'fogOn') + rowG('Fog amount', 'fogAmt', 0, 1, 0.01) + rowG('Fog starts at', 'fogNear', 300, 1400, 10) + rowG('Fog full at', 'fogFar', 400, 2000, 10) + chk('Fog sheets (sprite fog)', 'sheets') + rowG('Sheet opacity', 'sheetAlpha', 0, 1, 0.01) + rowG('Moonbeam volume', 'shaftAlpha', 0, 0.5, 0.01);
    h += '<h2>Layers</h2><p class="note">Depth: negative is behind the bench plane, positive is towards the camera.</p>';
    for (const l of LAYERS) {
      h += `<div class="row"><input type="checkbox" data-l="${l.id}" ${l.on ? 'checked' : ''}><label title="${l.hint || ''}">${l.label}</label>`;
      h += l.z === null ? '<span class="note">follows</span><span></span>' : `<input type="range" data-z="${l.id}" min="${l.min}" max="${l.max}" step="1" value="${l.z}"><output data-zo="${l.id}">${l.z}</output>`;
      h += '</div>';
    }
    h += chk('HUD (flat overlay)', 'hud');
    panel.innerHTML = h;
    caption.innerHTML = CAPTIONS[S.variant];
    panel.querySelectorAll('[data-only]').forEach((e) => { e.hidden = !e.dataset.only.includes(S.variant === 'C' ? 'C' : S.variant); });
  }
  function syncPanel() {
    panel.querySelectorAll('[data-k]').forEach((e) => { e.value = S[e.dataset.k]; panel.querySelector(`[data-o="${e.dataset.k}"]`).textContent = e.dataset.k === 'descent' ? Number(S.descent).toFixed(2) : S[e.dataset.k]; });
  }
  panel.addEventListener('input', (e) => {
    const t = e.target;
    if (t.dataset.k) { S[t.dataset.k] = Number(t.value); panel.querySelector(`[data-o="${t.dataset.k}"]`).textContent = t.value; if (t.dataset.k === 'descent') S.playing = false; }
    if (t.dataset.c) S[t.dataset.c] = t.checked;
    if (t.dataset.l) layer[t.dataset.l].on = t.checked;
    if (t.dataset.z) { layer[t.dataset.z].z = Number(t.value); panel.querySelector(`[data-zo="${t.dataset.z}"]`).textContent = t.value; }
  });
  panel.addEventListener('click', (e) => {
    const b = e.target.closest('button'); if (!b) return;
    if (b.dataset.v) { S.variant = b.dataset.v; S.descent = 0; S.playing = false; buildPanel(); }
    if (b.id === 'play') { S.playing = true; descentStart = performance.now(); }
  });

  // integer upscale of the 640x360 picture (fractional only when the window is narrower than 640)
  function fit() {
    const stage = document.getElementById('stage'), wrap = document.getElementById('stageWrap');
    const aw = wrap.clientWidth, ah = window.innerWidth > 900 ? window.innerHeight - 90 : window.innerHeight * 0.7;
    let s = Math.floor(Math.min(aw / W, ah / H)); if (s < 1) s = Math.min(1, aw / W);
    stage.style.width = W * s + 'px'; stage.style.height = H * s + 'px';
  }
  window.addEventListener('resize', fit);
  buildPanel(); fit();
  requestAnimationFrame(frame);
  window.__depth.ready = true;
  window.__depth.S = S; window.__depth.layer = layer; window.__depth.OBJ = OBJ; window.__depth.cam = persp;
})().catch((e) => { window.__depth.errors.push(String(e && e.stack || e)); console.error(e); });
