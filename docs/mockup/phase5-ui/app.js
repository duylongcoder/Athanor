// Athanor Phase 5a UI mockup. Everything is drawn in 640x360 art pixels; y is the TOP of a text line box.
const FONT = {
  en: { title: 'Alagard', name: 'PixOpB', body: 'BitPotion', tiny: 'PixOp8' },
  vi: { title: 'SvnCrux', name: 'SvnCrux', body: 'SvnCrux', tiny: 'SvnCrux' },
};
// [font size en, vi][line height en, vi]
const SZ = (lang, kind, size) => {
  const v = lang === 'vi';
  if (kind === 'title') return [size, size];
  if (kind === 'name') return [16, 16];
  if (kind === 'body') return [16, v ? 16 : 12];
  return [v ? 16 : 8, v ? 12 : 10];
};
// Strings: [en, vi]. vi for NEW keys is a DRAFT for the fit test only (localizer-vi owns vi.json).
const S = {
  'ui.title.begin': ['Begin', 'Bắt đầu'], 'ui.title.journal': ['Journal', 'Sổ tay'], 'ui.title.goals': ['Goals', 'Mục tiêu'],
  'ui.title.settings': ['Settings', 'Cài đặt'], 'ui.title.how_to_play': ['How to play', 'Cách chơi'],
  'ui.title.skip_tutorial': ['Skip tutorial', 'Bỏ qua hướng dẫn'], 'ui.title.subtitle': ["An alchemist's descent", 'Nhà giả kim xuống hầm sâu'],
  'ui.title.progress': ['JOURNAL: 22 OF 83 REACTIONS   -   GOALS: 3 OF 12', 'SỔ TAY: 22/83 PHẢN ỨNG   -   MỤC TIÊU: 3/12'],
  'ui.title.fonts': ["FONTS: ALAGARD, BITPOTION, PIXEL OPERATOR, CODER'S CRUX BY NAL", "PHÔNG: ALAGARD, SVN-CODER'S CRUX (NAL)"],
  'ui.title.reset': ['RESET PROGRESS', 'XÓA TIẾN TRÌNH'],
  'ui.title.watch_intro': ['Watch intro', 'Xem mở đầu'],
  'ui.title.skip_prompt_title': ['Skip the tutorial?', 'Bỏ qua hướng dẫn?'],
  'ui.title.skip_prompt_body1': ['Your journal already has entries.', 'Sổ tay của bạn đã có ghi chép.'],
  'ui.title.skip_prompt_body2': ['The tutorial is one short run.', 'Hướng dẫn chỉ là một chuyến ngắn.'],
  'ui.title.skip_prompt_play': ['Play it', 'Chơi thử'],
  'ui.settings.title': ['Settings', 'Cài đặt'], 'ui.settings.vibration': ['Vibration', 'Rung'], 'ui.settings.language': ['Language', 'Ngôn ngữ'],
  'ui.settings.on': ['On', 'Bật'], 'ui.settings.off': ['Off', 'Tắt'], 'ui.common.back': ['Back', 'Quay lại'], 'ui.common.cancel': ['Cancel', 'Hủy'],
  'ui.common.click_to_continue': ['CLICK TO CONTINUE', 'BẤM ĐỂ TIẾP TỤC'],
  'ui.pause.button': ['Pause (Esc)', 'Tạm dừng (Esc)'], 'ui.pause.title': ['Paused', 'Tạm dừng'], 'ui.pause.resume': ['Resume', 'Tiếp tục'],
  'ui.pause.vibration': ['Vibration: {state}', 'Rung: {state}'], 'ui.pause.how_to_play_locked': ['Only from the title', 'Chỉ có ở màn hình chính'],
  'ui.pause.quit': ['Quit to title', 'Về màn hình chính'], 'ui.pause.quit_title': ['Quit this run?', 'Thoát chuyến này?'],
  'ui.pause.quit_body1': ['The run will not count.', 'Chuyến này sẽ không được tính.'], 'ui.pause.quit_body2': ['Your journal is kept.', 'Sổ tay vẫn được giữ.'],
  'ui.pause.quit_confirm': ['Quit', 'Thoát'],
  'ui.bench.redraw': ['Redraw', 'Đổi bài'],
  'ui.rotate.title': ['Rotate your device', 'Hãy xoay thiết bị'],
  'ui.rotate.body': ['Turn your phone sideways to play.', 'Xoay điện thoại sang ngang để chơi.'],
  'rarity.common': ['Common', 'Thường'], 'rarity.uncommon': ['Uncommon', 'Ít gặp'], 'rarity.rare': ['Rare', 'Hiếm'], 'rarity.legendary': ['Legendary', 'Huyền thoại'],
  'ui.choice.catalyst_title': ['Choose a Catalyst', 'Chọn một Xúc tác'], 'ui.choice.catalyst_sub': ['It bends the rules for the rest of this descent.', 'Nó bẻ luật chơi cho tới hết chuyến này.'],
  'ui.journal.title': ['Journal', 'Sổ tay'], 'ui.journal.sub': ['ORE  -  TIER 1  -  REACTIVITY 7', 'QUẶNG  -  BẬC 1  -  CHỈ SỐ 7'],
  'cutscene.depth': ['DEPTH {n}', 'TẦNG {n}'],
  'depth.2.name': ['The Dripping Stair', 'Bậc Thang Rỉ Nước'], 'depth.4.name': ['The Root of Stone', 'Cội Đá'], 'depth.3.name': ['The Humming Dark', 'Bóng Tối Rì Rầm'],
  'depth.2.intro': ['Water finds its way down. So do you.', 'Nước tìm đường chảy xuống. Bạn cũng vậy.'],
  'depth.3.intro': ['Something below keeps a slow, warm note.', 'Có thứ gì dưới kia ngân một nốt chậm, ấm.'],
  'depth.4.intro': ['The roots here are thicker than the walls.', 'Rễ cây ở đây dày hơn cả tường.'],
  'depth.5.intro': ['No hum now. Only the place where seeds are planted.', 'Không còn tiếng ngân. Chỉ còn nơi gieo hạt.'],
  'tutorial.1_2': ['Click Ember Coal, then the lit tile.', 'Bấm Ember Coal, rồi bấm ô sáng.'],
  'tutorial.3_0': ['Redraw swaps the materials in your hand. Two per depth.', 'Đổi bài thay nguyên liệu trên tay. Hai lần mỗi tầng.'],
  'tutorial.1_6b': ['Potency: what you made, times one less than the materials used, doubled the first time, times the chain step.', 'Dược lực: giá trị món tạo ra, nhân với số nguyên liệu trừ một, gấp đôi lần đầu, nhân bậc chuỗi.'],
  'tutorial.5_2': ['Each one says what it does. Point at one to read it.', 'Mỗi cái đều ghi công dụng. Trỏ vào để đọc.'],
};
const tr = (lang, k, p) => { let s = S[k][lang === 'vi' ? 1 : 0]; for (const a in p || {}) s = s.replace('{' + a + '}', p[a]); return s; };

const RC = { Common: '#a8a0a8', Uncommon: '#8ad04a', Rare: '#7cc0f8', Legendary: '#ffa63a' };
const RDARK = { Common: '#5a5058', Uncommon: '#3a6a14', Rare: '#1c5a96', Legendary: '#8a4a08' };
const RANK = { Common: 1, Uncommon: 2, Rare: 3, Legendary: 4 };

// ---- primitives ----
const px = (n) => n + 'px';
function box(p, x, y, w, h, css, cls) { const d = document.createElement('div'); d.className = 'r ' + (cls || ''); Object.assign(d.style, { left: px(x), top: px(y), width: px(w), height: px(h) }, css || {}); p.appendChild(d); return d; }
function img(p, src, x, y, w, h, css) { const i = document.createElement('img'); i.src = 'img/' + src + '.png'; Object.assign(i.style, { left: px(x), top: px(y) }, w ? { width: px(w), height: px(h) } : {}, css || {}); p.appendChild(i); return i; }
function hit(p, x, y, w, h, name) { const d = box(p, x, y, w, h, null, 'hit'); d.dataset.hit = name || ''; return d; }
// text: kind title|name|body|tiny; o: {a:'l|c|r', w:wrap width, lines:max lines, fit:max width, sh:shadow colour, css}
function text(p, lang, str, x, y, kind, size, color, o) {
  o = o || {};
  const [fs, lh] = SZ(lang, kind, size);
  const d = document.createElement('div'); d.className = 't' + (o.w ? ' wrap' : ''); d.textContent = str;
  d.style.fontFamily = o.font || FONT[lang][kind]; d.style.fontSize = px(fs); d.style.lineHeight = px(lh); d.style.color = color;
  if (o.sh) d.style.textShadow = '1px 1px 0 ' + o.sh;
  const a = o.a || 'l', W = o.w || 800;
  d.style.width = px(W); d.style.textAlign = a === 'c' ? 'center' : a === 'r' ? 'right' : 'left';
  d.style.left = px(a === 'c' ? x - W / 2 : a === 'r' ? x - W : x); d.style.top = px(y);
  if (!o.w) { d.style.width = 'auto'; d.style.transform = a === 'c' ? 'translateX(-50%)' : a === 'r' ? 'translateX(-100%)' : ''; d.style.left = px(x); }
  if (o.fit) d.dataset.fit = o.fit; if (o.w && o.lines) { d.dataset.lines = o.lines; d.dataset.lh = lh; }
  Object.assign(d.style, o.css || {}); p.appendChild(d); return d;
}
function button(p, lang, label, x, y, o) {
  o = o || {};
  const b = img(p, 'button_up', x, y, 120, 26, o.off ? { filter: 'grayscale(.9) brightness(.55)' } : o.hover ? { filter: 'brightness(1.25) saturate(1.1)' } : {});
  text(p, lang, label, x + 60, y + (o.off ? 4 : 4), 'title', 16, o.off ? '#8a7a6a' : '#fff6b8', { a: 'c', sh: '#3b0d1e', fit: 108 });
  hit(p, x, y, 120, 26, label); return b;
}
function dialog(p) { box(p, 0, 0, 640, 360, { background: 'rgba(0,0,0,.7)' }); img(p, 'dialog', 160, 75); }
// 7x7 star glyph (mockup stand-in; the real sprite comes from pixel-artist). Rows top to bottom.
const STAR = ['...#...', '..###..', '#######', '.#####.', '..###..', '.##.##.', '.#...#.'];
(function () { const c = document.createElement('canvas'); c.width = c.height = 7; const g = c.getContext('2d'); g.fillStyle = '#000'; STAR.forEach((r, y) => [...r].forEach((ch, x) => { if (ch === '#') g.fillRect(x, y, 1, 1); })); document.documentElement.style.setProperty('--star', 'url(' + c.toDataURL() + ')'); })();
function pip(p, x, y, color, outline, round) { const d = document.createElement('div'); d.className = 'pipo'; d.style.left = px(x); d.style.top = px(y); const i = document.createElement('div'); i.className = 'pip' + (round ? ' round' : ''); i.style.background = color; i.style.left = '0'; i.style.top = '0'; d.appendChild(i); if (!outline) d.style.filter = 'none'; p.appendChild(d); return d; }
function pips(p, x, y, rar, outline) { for (let i = 0; i < RANK[rar]; i++) pip(p, x + i * 8, y, RC[rar], outline); }
const scr = (host, lang) => { const s = document.createElement('div'); s.className = 'scr'; s.dataset.lang = lang; host.appendChild(s); const l = document.createElement('div'); l.className = 'lang'; l.textContent = lang.toUpperCase(); s.appendChild(l); return s; };

// ---- backdrops ----
function titleBg(p) { img(p, 'bench', 0, 0); box(p, 0, 0, 640, 360, { background: 'rgba(8,4,6,.8)' }); }
function logo(p, lang) {
  const edge = '#2a0c14';
  for (const [dx, dy] of [[-3, 0], [3, 0], [0, -3], [0, 3]]) text(p, lang, 'Athanor', 320 + dx, 72 + dy, 'title', 48, edge, { a: 'c', font: 'Alagard', css: { transform: 'translateX(-50%) scale(2)', transformOrigin: '50% 50%' } });
  text(p, lang, 'Athanor', 320, 72, 'title', 48, '#fff', { a: 'c', font: 'Alagard', css: { transform: 'translateX(-50%) scale(2)', transformOrigin: '50% 50%', background: 'linear-gradient(#fffbe0 45%,#f08a2c 60%)', webkitBackgroundClip: 'text', backgroundClip: 'text', webkitTextFillColor: 'transparent' } });
  text(p, lang, tr(lang, 'ui.title.subtitle'), 320, 188, 'title', 16, '#f8d250', { a: 'c', sh: '#0c0706', fit: 400 });
}
function titleChrome(p, lang, o) {
  o = o || {};
  const L = (k) => tr(lang, k), v = lang === 'vi';
  button(p, lang, L('ui.title.begin'), 260, 214); button(p, lang, L('ui.title.journal'), 260, 244); button(p, lang, L('ui.title.goals'), 260, 274);
  button(p, lang, L('ui.title.how_to_play'), 130, 304); button(p, lang, L('ui.title.settings'), 260, 304, { hover: o.hoverSettings }); button(p, lang, L('ui.title.watch_intro'), 390, 304);
  text(p, lang, L('ui.title.progress'), 320, v ? 332 : 334, 'tiny', 8, '#c8ac84', { a: 'c', sh: '#0c0706', fit: 620 });
  text(p, lang, L('ui.title.fonts'), 320, v ? 346 : 348, 'tiny', 8, '#a08058', { a: 'c', sh: '#0c0706', fit: 620 });
}
function benchBg(p, lang, o) {
  o = o || {};
  img(p, 'bench', 0, 0);
  // left panel
  text(p, lang, 'DEPTH 1', 18, 14, 'tiny', 8, '#a88a6a'); text(p, lang, '120', 18, 26, 'title', 16, '#f8d250', { sh: '#3a1a08' });
  text(p, lang, '/ 300', 60, 30, 'tiny', 8, '#a88a6a');
  text(p, lang, 'TRIALS 3', 488, 14, 'tiny', 8, '#a88a6a'); text(p, lang, 'REDRAWS 2', 560, 14, 'tiny', 8, '#a88a6a');
  // Redraw button
  box(p, 556, 88, 76, 20, { background: o.redrawLit ? '#26706a' : '#1d5a52', border: '1px solid #0e2e2a' });
  text(p, lang, tr(lang, 'ui.bench.redraw'), 594, 88 + 3, 'tiny', 8, '#e6f6f0', { a: 'c', fit: 70, css: lang === 'vi' ? { fontSize: '16px' } : { fontSize: '10px' } });
  hit(p, 556, 88, 76, 20, 'Redraw');
  if (!o.noPause) pauseButton(p, lang);
}
function pauseButton(p, lang, o) {
  box(p, 8, 88, 28, 20, { background: '#1d1410', border: '2px solid #8a5a1e', boxShadow: 'inset 0 0 0 1px #3a2412' });
  box(p, 16, 93, 4, 10, { background: '#f0dcc0' }); box(p, 24, 93, 4, 10, { background: '#f0dcc0' });
  hit(p, 0, 76, 44, 44, 'Pause 44x44'); // hit box larger than the picture
}
const HAND = [['ember_coal', 'Ember Coal', 'ORE', 'Common'], ['salt_crystal', 'Salt Crystal', 'CRYSTAL', 'Uncommon'], ['lightning_sand', 'Lightning Sand', 'POWDER', 'Rare'], ['deep_tear', 'Deep Tear', 'POTION', 'Common'], ['glowmoss', 'Glowmoss', 'PLANT', 'Common']];
function hand(p, lang, mode, hov) {
  HAND.forEach((c, i) => {
    const x = 132 + i * 76, y = i === 2 ? 282 : 292;
    img(p, c[0], x + 25, y + 8, 24, 24);
    const words = c[1].split(' ');
    text(p, lang, words[0], x + 37, y + 33, 'name', 16, '#3a2212', { a: 'c', fit: 70 }); if (words[1]) text(p, lang, words[1], x + 37, y + 43, 'name', 16, '#3a2212', { a: 'c', fit: 70 });
    if (mode === 'before') pip(p, x + 3, y + 3, RC[c[3]], false, true);
    else pips(p, x + 3, y + 3, c[3], true);
  });
}
// ---- frames ----
const F = [];
const frame = (id, title, note, build, opt) => F.push(Object.assign({ id, title, note, build }, opt || {}));

frame('a1-title', '(a) Title: Settings, How to play, Watch intro', 'Begin 320,227 and Journal 320,257 unchanged. New third row y=304: How to play 130..250, Settings 260..380 (as decided), Watch intro 390..510. Progress and credit move down; in Vietnamese they sit 2 px higher (12 px rows).', (p, l) => { titleBg(p); logo(p, l); titleChrome(p, l, { hoverSettings: true }); });
frame('a2-skip', '(a) Title: one-time "Skip tutorial?" prompt', 'Shown once, when Begin is pressed, TutorialDone is false and the journal is not empty. Either choice sets the flag. A click outside does nothing (a choice is required). Same dialog sprite and block as "Erase progress?".', (p, l) => {
  titleBg(p); logo(p, l); titleChrome(p, l); dialog(p);
  text(p, l, tr(l, 'ui.title.skip_prompt_title'), 320, 75 + 18, 'title', 32, '#f8d250', { a: 'c', sh: '#3a1a08', fit: 280 });
  text(p, l, tr(l, 'ui.title.skip_prompt_body1'), 320, 75 + 70, 'body', 16, '#f0dcc0', { a: 'c', fit: 280 });
  text(p, l, tr(l, 'ui.title.skip_prompt_body2'), 320, 75 + 88, 'body', 16, '#a88a6a', { a: 'c', fit: 280 });
  button(p, l, tr(l, 'ui.title.skip_prompt_play'), 160 + 28, 75 + 150); button(p, l, tr(l, 'ui.title.skip_tutorial'), 160 + 172, 75 + 150);
});
function settings(p, l, o) {
  o = o || {};
  titleBg(p); dialog(p);
  const X = 160, Y = 75;
  text(p, l, tr(l, 'ui.settings.title'), 320, Y + 12, 'title', 32, '#f8d250', { a: 'c', sh: '#3a1a08', fit: 280 });
  box(p, X + 24, Y + 50, 272, 1, { background: '#7a5a3a' });
  let ry = Y + 58;
  const row = (label, value, kind, hover) => {
    text(p, l, label, X + 28, ry + 5, 'body', 16, '#f0dcc0', { fit: 110 });
    box(p, X + 140, ry, 150, 26, { background: hover ? '#4a2c14' : '#2a1810', border: '1px solid ' + (hover ? '#f8d250' : '#8a5a1e') });
    if (kind === 'toggle') { box(p, X + 150, ry + 8, 10, 10, { background: value ? '#f8d250' : 'transparent', border: '2px solid #f8d250' }); text(p, l, tr(l, value ? 'ui.settings.on' : 'ui.settings.off'), X + 168, ry + 5, 'title', 16, value ? '#f8d250' : '#a88a6a', { fit: 100 }); }
    else { text(p, l, '<', X + 152, ry + 5, 'title', 16, '#f8d250'); text(p, l, '>', X + 274, ry + 5, 'title', 16, '#f8d250'); text(p, l, l === 'vi' ? 'Tiếng Việt' : 'English', X + 215, ry + 5, 'title', 16, '#fff6b8', { a: 'c', fit: 100 }); }
    hit(p, X + 140, ry, 150, 26, label); ry += 32;
  };
  if (!o.noVib) row(tr(l, 'ui.settings.vibration'), o.vibOn !== false, 'toggle');
  row(tr(l, 'ui.settings.language'), 0, 'lang', o.hoverLang);
  const rst = text(p, l, tr(l, 'ui.title.reset'), 320, Y + 134, 'tiny', 8, '#a86a4a', { a: 'c', fit: 140 }); hit(p, 260, Y + 128, 120, 22, 'Reset');
  box(p, X + 24, Y + 126, 272, 1, { background: '#7a5a3a' });
  button(p, l, tr(l, 'ui.common.back'), 260, Y + 156);
}
frame('b1-settings', '(b) Settings panel: Vibration (phones), Language, Reset', 'Volume rows hidden. 150x26 value buttons (>= 26 art px tall). Vibration shows On/Off as text plus a filled/hollow square, never colour alone. Language: tap anywhere on the value cycles (two languages); < > are cues only.', (p, l) => settings(p, l, {}));
frame('b2-settings-desktop', '(b) Settings panel on desktop (no Vibration row), Language hovered', 'Row collapses; Reset and Back keep their place, the panel keeps its size. Hover = brighter fill + gold border.', (p, l) => settings(p, l, { noVib: true, hoverLang: true }));

function pauseDlg(p, l, vibOn) {
  benchBg(p, l, { noPause: false }); hand(p, l, 'after'); dialog(p);
  const X = 160, Y = 75;
  text(p, l, tr(l, 'ui.pause.title'), 320, Y + 10, 'title', 32, '#f8d250', { a: 'c', sh: '#3a1a08', fit: 280 });
  button(p, l, tr(l, 'ui.pause.resume'), 260, Y + 50);
  button(p, l, tr(l, 'ui.pause.vibration', { state: tr(l, vibOn ? 'ui.settings.on' : 'ui.settings.off') }), 260, Y + 80);
  button(p, l, tr(l, 'ui.title.how_to_play'), 260, Y + 110, { off: true });
  text(p, l, tr(l, 'ui.pause.how_to_play_locked'), 320, Y + 137, 'tiny', 8, '#c8ac84', { a: 'c', fit: 280 });
  button(p, l, tr(l, 'ui.pause.quit'), 260, Y + 152);
}
frame('c1-hud', '(c) Pause button on the HUD (640x360)', 'Picture 28x20 at (8,88), mirrored from Redraw (556,88); hit box 44x44 at (0,76). Two bars = shape cue. Desktop: tooltip "Pause (Esc)" on hover. Dashed boxes: toggle "Show hit boxes".', (p, l) => { benchBg(p, l); hand(p, l, 'after'); });
frame('c2-pause', '(c) Pause menu in a run', 'Time.timeScale = 0. How to play is greyed and says why. Language is not offered (title only). Esc or Resume closes; a click outside closes too.', (p, l) => pauseDlg(p, l, true));
frame('c3-quit', '(c) Quit to title: confirm', 'Same shape as "Erase progress?". Quit discards the run uncounted.', (p, l) => {
  benchBg(p, l); hand(p, l, 'after'); dialog(p);
  const Y = 75;
  text(p, l, tr(l, 'ui.pause.quit_title'), 320, Y + 18, 'title', 32, '#ee6a3c', { a: 'c', sh: '#3a1a08', fit: 280 });
  text(p, l, tr(l, 'ui.pause.quit_body1'), 320, Y + 70, 'body', 16, '#f0dcc0', { a: 'c', fit: 280 });
  text(p, l, tr(l, 'ui.pause.quit_body2'), 320, Y + 88, 'body', 16, '#a88a6a', { a: 'c', fit: 280 });
  button(p, l, tr(l, 'ui.common.cancel'), 188, Y + 150); button(p, l, tr(l, 'ui.pause.quit_confirm'), 332, Y + 150);
});
frame('c4-phone', '(c) Phone landscape 844x390 CSS px, DPR 2 and 3', 'Canvas fits at 1.083 CSS px per art px, so 44 CSS px = 40.6 art px. Dashed square = 44 CSS px target. The old 16 px arrows would be 17 CSS px: too small, hence 26 px rows and a 44 px pause target.', (p, l) => { benchBg(p, l); hand(p, l, 'after'); }, { phone: true, langs: ['en'] });

// ---- (g) rotate-device layer: HTML/CSS in the WebGL template, not drawn on the 640x360 canvas ----
// Icon: 48x18 art cells, 5 CSS px per cell (240x90). Portrait phone (dim) -> arrow -> landscape phone (gold).
function rotIcon() {
  const R = (x, y, w, h, c, cls) => `<rect x="${x}" y="${y}" width="${w}" height="${h}" fill="${c}"${cls ? ' class="' + cls + '"' : ''}/>`;
  const phone = (x, y, w, h, c, notch) => R(x, y, w, h, c) + R(x + 1, y + 1, w - 2, h - 2, '#2a1810') + notch(x, y);
  const a = [];
  a.push(phone(0, 0, 10, 18, '#a88a6a', (x, y) => R(x + 4, y + 1, 2, 1, '#a88a6a')));
  a.push('<g class="rot-arrow">' + R(14, 8, 8, 2, '#f0dcc0') + R(22, 5, 1, 8, '#f0dcc0') + R(23, 6, 1, 6, '#f0dcc0') + R(24, 7, 1, 4, '#f0dcc0') + R(25, 8, 1, 2, '#f0dcc0') + '</g>');
  a.push(phone(30, 4, 18, 10, '#f8d250', (x, y) => R(x + 1, y + 4, 1, 2, '#f8d250')));
  return '<svg class="rot-icon" viewBox="0 0 48 18" aria-hidden="true">' + a.join('') + '</svg>';
}
function rotate(p, lang) {
  const r = document.createElement('div'); r.className = 'rot'; r.lang = lang; r.setAttribute('role', 'alert');
  r.innerHTML = '<div class="rot-body">' + rotIcon() + '<div class="rot-title" role="heading" aria-level="1"></div><p class="rot-text"></p></div>';
  r.querySelector('.rot-title').textContent = tr(lang, 'ui.rotate.title'); r.querySelector('.rot-text').textContent = tr(lang, 'ui.rotate.body');
  p.appendChild(r);
}
frame('g1-rotate', '(g) Rotate-device layer: portrait phone 360x800 CSS px (DPR 2 and 3)', 'Full viewport, covers the canvas and the loading bar, blocks taps. Shown by the media query, hides itself when the phone is turned. Text is a two-entry table in the template (language from navigator.language). Not a canvas frame: sizes are CSS px, 1x = 1 CSS px; the screenshots are taken at DPR 2 and 3.', (p, l) => rotate(p, l), { portrait: [360, 800] });

// tutorial overlay
function tut(p, l, o) {
  benchBg(p, l, { redrawLit: o.redraw, noPause: false }); hand(p, l, 'after');
  const holes = o.holes;
  const svg = document.createElementNS('http://www.w3.org/2000/svg', 'svg'); svg.setAttribute('width', 640); svg.setAttribute('height', 360); svg.style.cssText = 'position:absolute;left:0;top:0';
  const id = 'm' + Math.random().toString(36).slice(2);
  svg.innerHTML = `<defs><mask id="${id}"><rect width="640" height="360" fill="#fff"/>${holes.map(h => `<rect x="${h[0]}" y="${h[1]}" width="${h[2]}" height="${h[3]}" fill="#000"/>`).join('')}</mask></defs><rect width="640" height="360" fill="rgba(0,0,0,.62)" mask="url(#${id})"/>`;
  p.appendChild(svg);
  holes.forEach(h => { box(p, h[0] - 2, h[1] - 2, h[2] + 4, h[3] + 4, { border: '2px solid #f8d250', boxShadow: '0 0 0 1px #3a1a08' }, 'pulse'); });
  const h0 = holes[0];
  if (o.arrow !== false) { const a = box(p, h0[0] + h0[2] / 2 - 5, h0[1] - 14, 10, 8, { background: '#f8d250', clipPath: 'polygon(0 0,100% 0,50% 100%)' }, 'bob'); if (h0[1] < 60) { a.style.top = px(h0[1] + h0[3] + 6); a.style.clipPath = 'polygon(50% 0,100% 100%,0 100%)'; } }
  const v = l === 'vi', pitch = v ? 16 : 12, W = 288, str = tr(l, o.key);
  const probe = o.lines || 2, bh = 12 + probe * pitch + (o.read ? 12 : 0);
  box(p, 168, 14, 304, bh, { background: '#1c120e', border: '2px solid #8a5a1e', boxShadow: 'inset 0 0 0 1px #3a2412' });
  text(p, l, str, 176, 21, 'body', 16, '#f0dcc0', { w: W, lines: 4, fit: W });
  if (o.read) text(p, l, tr(l, 'ui.common.click_to_continue'), 464, 14 + bh - 14, 'tiny', 8, '#c8ac84', { a: 'r', css: { animation: 'pulse 1s steps(2,end) infinite' }, fit: 200 });
  hit(p, 168, 14, 304, bh, 'Text box (click = next line)');
}
frame('d1-tut-action', '(d) Tutorial overlay, action step (1.2): dim, highlight, text box', 'Dim 0.62 black over the whole canvas except the holes. Hole = the card and the lit tile. Gold 2 px ring pulses, a bobbing arrow points at the first target. No "click to continue": the player must act. A click anywhere else flashes the ring once.', (p, l) => tut(p, l, { key: 'tutorial.1_2', holes: [[208, 292, 74, 68], [244, 188, 40, 28]], lines: 1 }));
frame('d2-tut-read', '(d) Tutorial overlay, read step (3.0): Redraw highlighted', 'Read steps show CLICK TO CONTINUE; a click on the text box advances. Redraw stays locked (it only points).', (p, l) => tut(p, l, { key: 'tutorial.3_0', holes: [[556, 88, 76, 20]], read: true, redraw: true, lines: 2 }));
frame('d3-tut-long', '(d) Tutorial overlay, longest line (1.6b)', 'Worst case: 95 characters in Vietnamese, 3 lines at a 16 px pitch. Box height is computed from the line count; it never passes y=100, clear of the table (y 130).', (p, l) => tut(p, l, { key: 'tutorial.1_6b', holes: [[18, 22, 120, 34]], read: true, lines: l === 'vi' ? 3 : 4 }));

// rarity
const RAR_C = [['cat_bellows', 'Bellows', 'Common', 'Reactions involving an Ore score double.'], ['cat_crooked_mirror', 'Crooked Mirror', 'Rare', 'Diagonal neighbours also react.'], ['cat_philosophers_fire', "Philosopher's Fire", 'Legendary', 'Every reaction scores double.']];
function offer(p, l, mode) {
  img(p, 'bench', 0, 0); box(p, 0, 0, 640, 360, { background: 'rgba(0,0,0,.8)' });
  text(p, l, tr(l, 'ui.choice.catalyst_title'), 320, 20, 'title', 32, '#f8d250', { a: 'c', sh: '#3a1a08', fit: 600 });
  text(p, l, tr(l, 'ui.choice.catalyst_sub'), 320, 62, 'body', 16, '#f0dcc0', { a: 'c', fit: 600 });
  const start = (640 - (3 * 150 + 2 * 18)) / 2;
  RAR_C.forEach((c, i) => {
    const x = start + i * 168, y = 112;
    img(p, 'choice', x + 1, y - 2 + 0);
    img(p, c[0], x + 63, y + 18, 24, 24);
    text(p, l, c[1], x + 75, y + 42, 'name', 16, '#3a2212', { a: 'c', fit: 140 });
    text(p, l, c[3], x + 14, y + 66, 'body', 16, '#3a2212', { w: 124, lines: 4 });
    const rk = c[2], rw = tr(l, 'rarity.' + rk.toLowerCase());
    if (mode === 'before') {
      pip(p, x + 6, y + 6, RC[rk], false, true);
      text(p, l, (l === 'vi' ? 'XÚC TÁC ' + rw.toUpperCase() : rw.toUpperCase() + ' CATALYST'), x + 75, y + 160, 'tiny', 8, '#a8854e', { a: 'c', fit: 140 });
    } else {
      pips(p, x + 6, y + 6, rk, true);
      box(p, x + 4, y + 158, 144, 20, { background: RDARK[rk] });
      pips(p, x + 8, y + 164, rk, true);
      text(p, l, rw.toUpperCase(), x + 146, y + 160, 'name', 16, '#fff6e0', { a: 'r', sh: '#1a0e08', fit: 100 });
    }
  });
}
frame('e1-offer-before', '(e) Catalyst offer: BEFORE (today)', 'A 7 px tinted pip in the corner and an 8 px kicker in tan #a8854e on parchment (contrast about 2.6:1). Rare and Common differ only by a colour dot.', (p, l) => offer(p, l, 'before'));
frame('e1-offer-after', '(e) Catalyst offer: AFTER', 'Tier = number of stars (1 to 4) + dark-outlined stars + a full-width tier strip with the tier word in 16 px bold, white on #3a6a14 / #1c5a96 / #8a4a08 / #5a5058 (all >= 5:1). Tier word never relies on colour.', (p, l) => offer(p, l, 'after'));
frame('e2-hand-before', '(e) Hand cards: BEFORE', 'One pip, colour only. Common, Uncommon, Rare, Common, Common.', (p, l) => { benchBg(p, l); hand(p, l, 'before'); });
frame('e2-hand-after', '(e) Hand cards: AFTER', 'n outlined stars, n = tier (1 Common to 4 Legendary), 7x7 star glyph, outline from four dark copies like the logo. Hover tooltip adds the tier word (Name font).', (p, l) => {
  benchBg(p, l); hand(p, l, 'after');
  img(p, 'tooltip', 232, 224, 176, 58); text(p, l, 'Lightning Sand', 320, 232, 'name', 16, '#3a2212', { a: 'c', fit: 160 });
  pips(p, 270, 250, 'Rare', true); text(p, l, tr(l, 'rarity.rare').toUpperCase(), 332, 247, 'name', 16, RDARK.Rare, { fit: 100 });
});
function journal(p, l, mode, jr) {
  img(p, 'journal', 20, 15); box(p, 0, 0, 640, 360, { background: 'rgba(0,0,0,.0)' });
  text(p, l, tr(l, 'ui.journal.title'), 38, 22, 'title', 16, '#f0dcc0', { sh: '#2a1608' });
  const mats = ['ember_coal', 'deep_tear', 'glowmoss', 'moondust', 'iron_ore', 'salt_crystal', 'copper_filings', 'lightning_sand', 'hollow_bone', 'bone_ash'], rars = ['Common', 'Common', 'Common', 'Common', 'Common', 'Uncommon', 'Uncommon', 'Rare', 'Rare', 'Common'];
  mats.forEach((m, i) => {
    const cx = 53 + i * 27, cy = 75; img(p, m, cx + 0, cy, 24, 24);
    if (mode === 'after' && rars[i] !== 'Common') pip(p, cx - 1, cy - 1, RC[rars[i]], true); // one star, top-left of the cell
  });
  const cx = 20 + 324 + 130;
  img(p, 'salt_crystal', cx - 24, 45 + 36 - 24, 48, 48);
  text(p, l, 'Salt Crystal', cx, 45 + 62, 'title', 32, '#3a2212', { a: 'c', fit: 240, css: { lineHeight: '32px' } });
  if (mode === 'before') { text(p, l, tr(l, 'ui.journal.sub').replace('ORE', 'CRYSTAL'), cx, 45 + 94, 'tiny', 8, '#7a5a3a', { a: 'c', fit: 240 }); }
  else {
    text(p, l, tr(l, 'ui.journal.sub').replace('ORE', l === 'vi' ? 'TINH THỂ' : 'CRYSTAL'), cx, 45 + 94, 'tiny', 8, '#6a4a2a', { a: 'c', fit: 240 });
    const rk = jr || 'Uncommon', w = tr(l, 'rarity.' + rk.toLowerCase()).toUpperCase(), px0 = 20 + 324 + 14;
    pips(p, px0, 45 + 8, rk, true); text(p, l, w, px0, 45 + 18, 'name', 16, RDARK[rk], { fit: 450 - 4 - px0 });
  }
  for (let k = 0; k < 10; k++) box(p, cx - 36 + k * 7, 45 + 104, 5, 3, { background: k < 7 ? '#c8501e' : 'rgba(184,152,96,.5)' });
}
frame('e3-journal-before', '(e) Journal: BEFORE', 'No rarity anywhere in the Journal.', (p, l) => journal(p, l, 'before'));
frame('e3-journal-after', '(e) Journal: AFTER', 'Grid: Uncommon and above get ONE star in the rarity colour at the top-left of the cell (Common none). Page: n stars (y 53) with the tier word under them (Name 16, dark tone, y 63) at the top-left of the right page; sub line unchanged.', (p, l) => journal(p, l, 'after'));
frame('e3-journal-legendary', '(e) Journal page, worst case: Legendary', 'Stars on one row, tier word on the row below, both at the top-left (x 358). Word budget 88 px, ends before the icon (x 450): LEGENDARY 72, HUYỀN THOẠI 67.', (p, l) => journal(p, l, 'after', 'Legendary'));

// descent
function descent(p, l, o) {
  img(p, 'cutscene_room', 0, 0); box(p, 0, 0, 640, 360, { background: 'rgba(6,3,10,.82)' });
  const n = o.n, nm = tr(l, 'depth.' + n + '.name');
  text(p, l, tr(l, 'cutscene.depth', { n }), 320, 104, 'title', 16, '#f8d250', { a: 'c', sh: '#0c0706', fit: 300 });
  const size = nm.length > 20 ? 32 : 48;
  text(p, l, nm, 320, 128, 'title', size, '#fff0c8', { a: 'c', sh: '#3a1a08', fit: 620 });
  text(p, l, tr(l, 'depth.' + n + '.intro'), 320, 186, 'body', 16, '#f0dcc0', { a: 'c', sh: '#0c0706', fit: 540 });
}
frame('f1-descent', '(f) Descent step 3: depth name + intro line (depth 2)', 'The intro line fades in 0.3 s after the name, same fade out, hold raised from 1.7 s to 2.4 s. Body 16, centred y=186, at most 56 characters, max width 540. Below it the screen stays dark: the window view change (5a-09) happens on step 4.', (p, l) => descent(p, l, { n: 2 }));
frame('f2-descent-long', '(f) Descent: longest intro (depth 5, 52 characters)', 'Fit check for the longest line.', (p, l) => descent(p, l, { n: 5 }));
S['depth.5.name'] = ['The Quiet Below', 'Đáy Lặng'];

// ---- render ----
const root = document.getElementById('frames');
const groups = { a: 'a. Title', b: 'b. Settings panel', c: 'c. Pause', d: 'd. Tutorial overlay', e: 'e. Rarity by tier', f: 'f. Depth intro line', g: 'g. Rotate-device layer (portrait phones)' };
let last = '';
F.forEach(f => {
  const g = f.id[0]; if (g !== last) { const h = document.createElement('h2'); h.id = 'g-' + g; h.textContent = groups[g]; root.appendChild(h); last = g; }
  const fig = document.createElement('figure'); fig.id = f.id; fig.innerHTML = '<h3></h3>'; fig.querySelector('h3').textContent = f.title;
  const pair = document.createElement('div'); pair.className = 'pair'; fig.appendChild(pair);
  (f.langs || ['en', 'vi']).forEach(l => {
    let host = pair, s;
    if (f.portrait) { const pp = document.createElement('div'); pp.className = 'pphone'; pp.dataset.shot = f.id + '-' + l; Object.assign(pp.style, { width: px(f.portrait[0]), height: px(f.portrait[1]) }); const lb = document.createElement('div'); lb.className = 'lang'; lb.textContent = l.toUpperCase(); pp.appendChild(lb); pair.appendChild(pp); f.build(pp, l); return; }
    if (f.phone) { const ph = document.createElement('div'); ph.className = 'phone'; ph.dataset.shot = f.id + '-' + l; pair.appendChild(ph); host = ph; }
    s = scr(host, l); s.dataset.shot = f.id + '-' + l; f.build(s, l);
    if (f.phone) { const gd = document.createElement('div'); gd.className = 'guide'; const k = 1.08333, ox = 75.33; Object.assign(gd.style, { left: px(ox + 4 * k - 2), top: px(88 * k + 10 - 22 + 0), width: '44px', height: '44px' }); gd.style.left = px(ox + 26 * k - 22); gd.style.top = px(98 * k - 22); gd.innerHTML = '<span>44 CSS px</span>'; host.appendChild(gd); }
  });
  const cap = document.createElement('figcaption'); cap.textContent = f.note; fig.appendChild(cap); root.appendChild(fig);
});

// ---- overflow check ----
function check() {
  const bad = [], sc = parseFloat(getComputedStyle(document.documentElement).getPropertyValue('--s')) || 1;
  document.querySelectorAll('.scr .t').forEach(t => {
    const sr = t.closest('.scr'); let over = false, why = '';
    if (t.dataset.fit) { const w = t.getBoundingClientRect().width / sc / (t.closest('.phone') ? 1.08333 : 1) / (t.style.transform.includes('scale(2)') ? 2 : 1); if (w > +t.dataset.fit + 0.5) { over = true; why = 'width ' + w.toFixed(0) + ' > ' + t.dataset.fit; } }
    if (t.dataset.lines) { const lines = Math.round(t.scrollHeight / +t.dataset.lh); if (lines > +t.dataset.lines) { over = true; why = lines + ' lines > ' + t.dataset.lines; } }
    const r = t.getBoundingClientRect(), R = sr.getBoundingClientRect(); const k = sc * (t.closest('.phone') ? 1.08333 : 1);
    if (r.right > R.right + 1 || r.left < R.left - 1 || r.bottom > R.bottom + 1) { over = true; why += ' off canvas'; }
    if (over) { t.classList.add('over'); bad.push(sr.dataset.shot + ' | ' + t.textContent.slice(0, 40) + ' | ' + why); }
  });
  window.__rot = [];
  document.querySelectorAll('.pphone').forEach(pp => {
    if (!pp.offsetParent) return;
    const k = sc, R = pp.getBoundingClientRect(), b = pp.querySelector('.rot-body').getBoundingClientRect(), ti = pp.querySelector('.rot-title'), tx = pp.querySelector('.rot-text');
    const top = (b.top - R.top) / k, bot = (R.bottom - b.bottom) / k, left = (b.left - R.left) / k;
    const lines = (e, lh) => Math.round(e.getBoundingClientRect().height / k / lh);
    const info = { shot: pp.dataset.shot, top: +top.toFixed(0), bottom: +bot.toFixed(0), left: +left.toFixed(0), titleLines: lines(ti, 30), bodyLines: lines(tx, 24), textW: +(tx.getBoundingClientRect().width / k).toFixed(0) };
    window.__rot.push(info);
    if (top < 16 || bot < 16 || tx.scrollWidth > tx.clientWidth + 1 || ti.scrollWidth > ti.clientWidth + 1) bad.push(pp.dataset.shot + ' | rotate block | free ' + top.toFixed(0) + '/' + bot.toFixed(0));
  });
  window.__overflow = bad;
  const out = document.getElementById('overflow'); out.textContent = bad.length ? bad.length + ' overflow(s):\n' + bad.join('\n') : 'No text overflow in any frame (EN and VI).';
  out.className = bad.length ? 'tag bad' : 'tag ok'; out.dataset.rot = JSON.stringify(window.__rot);
}
document.fonts.ready.then(() => setTimeout(check, 150));
const q = new URLSearchParams(location.search); if (q.get('shot')) { document.body.classList.add('bare'); document.querySelectorAll('.pphone').forEach(e => { if (e.dataset.shot !== q.get('shot')) e.style.display = 'none'; else { e.style.border = '0'; e.querySelector('.lang').style.display = 'none'; } }); } if (q.get('only')) document.querySelectorAll('figure').forEach(f => { if (f.id !== q.get('only')) f.style.display = 'none'; }), q.get('only') && document.querySelectorAll('#frames > h2').forEach(h => h.style.display = 'none'); if (q.get('s')) document.documentElement.style.setProperty('--s', q.get('s')); if (q.get('still')) document.body.classList.add('still');
document.querySelectorAll('[data-s]').forEach(b => b.onclick = () => { document.documentElement.style.setProperty('--s', b.dataset.s); document.querySelectorAll('[data-s]').forEach(x => x.classList.toggle('on', x === b)); setTimeout(check, 50); });
document.getElementById('hits').onclick = (e) => { document.body.classList.toggle('hits'); e.target.classList.toggle('on'); };
