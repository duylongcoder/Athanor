# 5a-03 UI specs, mockup and change list (G1 approved; revised after the owner's G1 changes)

Mockup: `docs/mockup/phase5-ui/index.html` (opens from disk; each frame EN + VI; scale 1x/1.5x/2x; "Show hit boxes").
Screenshots: `production/qa/evidence/phase5/5a-03/` (`<frame>-<lang>-<scale>x.png`; phone frame at DPR 2 and 3); the rotate layer in `5a-03/rotate/`.
Vietnamese for NEW keys below is a draft for the fit test only; `localizer-vi` owns vi.json (5a-08).
Coordinates are art pixels on 640x360, y = top of the box. Text kinds: Title / Name / Body / Tiny as in `FontKind`.

**Font roles (after G1; SVN-Determination Sans is deleted).** English: Title = Alagard, Name = Pixel Operator Bold, Body = BitPotion, Tiny = Pixel Operator 8. Vietnamese: all four roles = SVN-Coder's Crux (CC BY 3.0, credit stays), Title/Name/Body at 16 px (Title 32 at 32 px), Tiny at 16 px with 12 px rows. The "Athanor" logo stays Alagard in both languages. Crux has no en dash. Crux is wider than the old Vietnamese Title/Name faces: re-checked, no overflow in any frame (below).

## Rules for every screen

- Smallest new text: Tiny 8 EN (12 px row, Crux 16, VI) and only for non-essential hints. Anything that carries information
  (rarity word, tutorial line, buttons) is Body 16 / Name 16 / Title 16 or larger.
- No information by colour alone: toggles = text + filled/hollow square; rarity = star count + word + colour; disabled = grey + reason text;
  highlight = ring + arrow + dim.
- Touch: mouse primary. A phone in landscape (844x390 CSS px) shows the canvas at 1.083 CSS px per art px. Targets >= 26 art px tall
  (rows, buttons) and the pause button hit box 44x44. The 16 px `<`/`>` arrows of the old Settings sketch would be 17 CSS px: not used.
- Overflow check (done in the page at 1x, EN and VI, every text with a width budget): **0 overflows** in all 40 frames at 1x and 2x (EN and VI, Crux in all four Vietnamese roles).

## Frames and change list (numbered, for `unity-gameplay-engineer`)

### (a) Title (frames a1, a2)
1. Add `UiButton` Settings at (260,304); How to play at (130,304); Watch intro at (390,304) (all 120x26). Begin (260,214), Journal (260,244), Goals (260,274) do not move (webtest clicks 320,227 and 320,257).
2. Progress line y 334 EN / 332 VI; font credit y 348 EN / 346 VI (VI rows are 12 px; the 360 px edge leaves 2 px). Credit text gains "CODER'S CRUX BY NAL" (licence).
3. Move RESET PROGRESS out of the title into Settings (b).
4. Skip prompt (a2): dialog sprite at (160,75), black block 0.7, title Title 32 gold y+18, body Body 16 at y+70 and y+88, buttons "Play it" (188,225) and "Skip tutorial" (332,225). Shown once when Begin is pressed, TutorialDone false and journal non-empty; both choices set the flag; a click outside does nothing; the block catches clicks (`TitleView.Asking`).
5. Watch intro: plays the 3-page intro, returns to the title. How to play: the throwaway-profile tutorial, returns to the title.
States: hover = brighten (as now); pressed = `button_down`; no disabled states on the title.

### (b) Settings panel (b1, b2)
6. Dialog (160,75), title Title 32 y+12, rule y+50 (272 wide), rows from y+58, pitch 32; label Body 16 at x=188; value button 150x26 at x=300 (fill #2a1810, border #8a5a1e; hover fill #4a2c14, border gold).
7. Vibration row: square 10x10 (filled gold = On, hollow = Off) + "On"/"Off" Title 16; whole value button toggles; pulse 20 ms once when turned on. Row absent where `Haptics.Supported` is false; Language then moves up, Reset and Back stay put.
8. Language row: `<` name `>` (cues); a tap anywhere on the value cycles; name in its own language ("English", "Tiếng Việt", from the lang file `name`). Changes apply at once; allowed only on the title.
9. Rule y+126; RESET PROGRESS Tiny, centred y+134, hit 120x22, opens the existing Erase dialog above it (order O+60). Back button at (260,231); a click outside also closes; file written on close.
10. Volume rows stay hidden (no sound in Phase 5).

### (c) Pause (c1 to c4)
11. HUD pause button: picture 28x20 at (12,152) (deviation: the aspects shelf band y 88..125 is full; the button sits on the empty floor left of the table) (fill #1d1410, border 2 px #8a5a1e, two bars 4x10), hit box 44x44 at (4,140). Esc on desktop. Tooltip on hover (desktop only) `ui.pause.button`. Hidden during cutscenes, offers, descent.
12. Pause menu: dialog + block, `Time.timeScale = 0`. Title Title 32 y+10; buttons 120x26 at x=260: Resume y+50, Vibration y+80 ("Vibration: On", toggles, hidden if unsupported, others shift up 30), How to play y+110 (disabled: grey, hint Tiny at y+137 "Only from the title"), Quit to title y+152. Language not offered.
13. Quit confirm (c3): like Erase: title orange Title 32, body Body 16 at y+70/y+88, Cancel (188,225), Quit (332,225). Quit discards the run uncounted. Engineer to confirm "journal is kept" is true (see Open questions).
14. Resume on Esc, Resume button, or click outside.

### (d) Tutorial overlay (d1 to d3)
15. Dim: full-canvas black 0.62 with rectangular holes (mask) over the highlighted target(s); everything outside blocks clicks.
16. Highlight: 2 px gold ring (#f8d250, 1 px dark outer), 2 px outside the hole, pulsing 1 Hz; bobbing arrow 10x8 above the first hole (below it if the hole is in the top 60 px).
17. Text box: x 168..472 (304 wide), y 14, fill #1c120e, border 2 px #8a5a1e + inner 1 px #3a2412; text Body 16, x 176, y 21, wrap width 288. Line pitch 12 EN / 16 VI; height = 12 + lines x pitch (+12 on read steps). Max 4 EN lines or 3 VI lines (worst case tutorial.1_6b, 95 chars VI = 3 lines). Never lower than y 100.
18. Read steps: show `ui.common.click_to_continue` (Tiny, bottom right of the box, blinking); a click on the box advances. Action steps: no hint; ring + arrow only.
19. Click on a locked area: the ring flashes once (0.15 s); no sound, no text.
20. Tutorial can be left only through the pause menu's Quit to title.

### (e) Rarity by tier (e1 to e3)
21. The rarity glyph is now a **star**, 7x7 (the old round pip is replaced everywhere rarity shows: catalyst strip, hand card, tooltip, journal); counts unchanged: n = 1 Common, 2 Uncommon, 3 Rare, 4 Legendary, 8 px pitch, tinted `Wording.RarityColor`, with a dark (#1a0e08) outline made of four offset dark copies (as the logo). Hand card: row at (x+3, y+3). Catalyst card: row at (x+6, y+6).
22. Catalyst offer card: remove the 8 px kicker for catalysts; add a strip 144x20 at (x+4, y+158) filled with the tier's dark tone (Common #5a5058, Uncommon #3a6a14, Rare #1c5a96, Legendary #8a4a08), stars at its left, tier word `rarity.*` uppercased, Name 16 #fff6e0, right-aligned. Aspect cards keep their kicker.
23. Hand card hover tooltip: adds stars + tier word in Name 16 (dark tone) under the name.
24. Journal (G1): the rarity mark sits at the **top-left** in both views. Grid cell: **one** star in the rarity colour (`Wording.RarityColor`, dark outline), Uncommon and above only, at (cell x-1, cell y-1) over the corner of the icon; Common shows none. Right page: n stars at (page x+14, y+8 of the page, i.e. 358,53) and the tier word in Name 16, dark tone, on the row below (358,63; budget 88 px up to the icon at x 450: LEGENDARY 72, HUYỀN THOẠI 67). Sub line and icon do not move. Glyph: star 7x7 (mockup stand-in drawn in the page; the real sprite is the pixel-artist's). Frames e3-journal-after, e3-journal-legendary (worst case).
Information is never colour alone: the Journal grid star is colour-only for tier (one star), so the tier is always readable on the page (n stars + word). Contrast: strip white on dark tones >= 5:1; the old tan kicker on parchment is about 2.6:1.

### (f) Depth intro line (f1, f2)
25. In the Descent, step 3: after the name, `depth.<n>.intro` Body 16 centred y 186, colour #f0dcc0 with shadow, max width 540 (56 chars max); fades in 0.3 s after the name, out with it; hold 1.7 s becomes 2.4 s. Banner positions: "DEPTH n" y 104, name y 128 (48, or 32 over 20 chars).

### (g) Rotate-device layer for portrait phones (frame g1; W2-07 step 1)

Frame `g1-rotate` (EN + VI), 360x800 CSS px, the common Android portrait phone. Not a canvas frame: sizes are CSS px, the layer is HTML/CSS in the WebGL template, not Unity (owner decision, phase-5-prep, answer 5). Screenshots `production/qa/evidence/phase5/5a-03/rotate/g1-rotate-<lang>-<2|3>x.png` (360x800 at DPR 2 and 3). The `.rot*` rules in `style.css` are the CSS the template copies under `#rotate`; the icon SVG is `rotIcon()` in `app.js`.

26. **Rotate-device layer** in `Athanor/Assets/WebGLTemplates/Athanor/index.html` (step 2, `web-release-engineer`). No change to C#, scene, `layout.json` or `Lang`.
    - **Trigger (owner, phase-5-prep answer 5a).** `#rotate { display: none }` and `@media (orientation: portrait) and (max-width: 639px) and (pointer: coarse) { #rotate { display: flex } }`. Why: the layer has no dismiss button, so it must not catch anyone who can still play. 639 px is the widest portrait window where the 640x360 canvas falls below 1x (Tiny 8 px text unreadable); portrait tablets (768 to 834 px) play fine letterboxed and stay out; `pointer: coarse` keeps a narrow desktop window out (a monitor cannot be turned). The first value in the brief, `max-width: 900px` alone, would have blocked tablets and narrow desktop windows. Turning the phone flips `orientation`, the layer hides at once, no script; the template's `fit()` on `resize` re-fits the canvas.
    - **Box.** `position: fixed; inset: 0; z-index: 10` (above `#unity-canvas` and `#loading`), fill `#0c0706`, `touch-action: none` (blocks taps). Frame line 8 px inset: 2 px `#8a5a1e` plus inner 1 px `#3a2412` (the dialog look). Content block centred, shifted 24 px up (`padding-bottom: 48px`), `max-width: 376px`, side padding 28 px (text width 304 px at 360).
    - **Order (360x800).** Icon 240x90 CSS px, block top y 272; 28 px gap; title 24 px / line 30 px bold `#f8d250` with `text-shadow: 0 2px 0 #3a1a08`, centred; 12 px gap; body 16 px / line 24 px `#f0dcc0`, centred. EN and VI: title 1 line, body 2 lines; block 272 px free above, 320 px below (measured).
    - **Type (owner, answer 5b).** The template's system monospace, no embedded font: `font-family: ui-monospace, Menlo, Consolas, "Roboto Mono", monospace` (the template's loading text is already monospace; no font bytes; covers Vietnamese). Coder's Crux is not embedded: an extra `svn-crux.ttf` would be about 80 KB against the 11,824,672 B size ceiling. `text-wrap: balance` on title and body, `overflow-wrap: break-word`, no `nowrap`, no fixed heights. Smallest text 16 CSS px; contrast on `#0c0706`: title 13.7:1, body 15.0:1.
    - **Icon.** Inline `<svg viewBox="0 0 48 18">`, `shape-rendering: crispEdges`, 5 CSS px per cell, `aria-hidden`: portrait phone 10x18 in `#a88a6a`, arrow 12x8 in `#f0dcc0`, landscape phone 18x10 in `#f8d250`; the notch marks the old "up" side. Meaning is carried by shape and arrow, not by colour. The arrow nudges 1 cell right at 1 Hz in 2 steps; `prefers-reduced-motion` stops it. No raster asset.
    - **Text.** A two-entry table in the template script, `{ en: {title, body}, vi: {title, body} }`, copied from the key table below. Language: first of `navigator.languages` that starts with `vi` gives vi, else en; set `<html lang>` and the layer's `lang`. The saved in-game language cannot be read before Unity loads: an English-choosing player on a Vietnamese phone sees Vietnamese here until they turn the phone. Accepted.
    - **Behaviour.** No close or "play anyway" button; Unity keeps loading and running under the layer. `role="alert"`, title `role="heading" aria-level="1"`. No orientation lock (works only in fullscreen, not on iOS).
    - **Sync check (step 2).** A line in `tools/web` that fails when the template table differs from `en.json` / `vi.json` for the two keys.

| Key (also the table key in the template) | English | Vietnamese (proposal for `localizer-vi`, not final) |
|---|---|---|
| ui.rotate.title | Rotate your device | Hãy xoay thiết bị |
| ui.rotate.body | Turn your phone sideways to play. | Xoay điện thoại sang ngang để chơi. |

Length at 360: EN 18 + 33 characters, VI 17 + 35. Monospace 24 px = 14.4 px per character: 21 characters per title line (text width 304); 16 px = 9.6 px: 31 per body line. Both titles fit one line, both bodies two. At 320 wide (text width 264, 18 and 27 characters per line) the EN title (18) still fits one line and the bodies wrap to at most 2 lines; the layout has no fixed height, so a wider-than-assumed monospace only adds a line (computed from the widths above, not shown in a frame; Android's own monospace not provable in DevTools).

Overflow (measured in the page, Chrome, 1x): **0 overflows**; block free space above and below never under 16 px (check threshold).

Test (step 2, `evidence/phase5/5a-rotate/`): Chrome DevTools mobile emulation (touch on) 360x800 portrait EN and VI (`navigator.language`), the same device rotated to 800x360 (layer gone, canvas visible), 390x844 portrait; a tablet 768x1024 portrait with touch (layer absent); a desktop window 500x800 without touch (layer absent); a desktop 1280x720 (layer absent); `webtest.mjs` desktop run unchanged.

## New and changed Lang keys (for 5a-08)

| Key | English | Vietnamese draft (fit test only) |
|---|---|---|
| ui.title.watch_intro | Watch intro | Xem mở đầu |
| ui.title.skip_prompt_title | Skip the tutorial? | Bỏ qua hướng dẫn? |
| ui.title.skip_prompt_body1 | Your journal already has entries. | Sổ tay của bạn đã có ghi chép. |
| ui.title.skip_prompt_body2 | The tutorial is one short run. | Hướng dẫn chỉ là một chuyến ngắn. |
| ui.title.skip_prompt_play | Play it | Chơi thử |
| ui.title.fonts (CHANGED) | FONTS: ALAGARD, BITPOTION, PIXEL OPERATOR, CODER'S CRUX BY NAL | PHÔNG: ALAGARD, SVN-CODER'S CRUX (NAL) (CHANGED: drops SVN-Determination) |
| ui.pause.button | Pause (Esc) | Tạm dừng (Esc) |
| ui.pause.title | Paused | Tạm dừng |
| ui.pause.resume | Resume | Tiếp tục |
| ui.pause.vibration | Vibration: {state} | Rung: {state} |
| ui.pause.how_to_play_locked | Only from the title | Chỉ có ở màn hình chính |
| ui.pause.quit | Quit to title | Về màn hình chính |
| ui.pause.quit_title | Quit this run? | Thoát chuyến này? |
| ui.pause.quit_body1 | The run will not count. | Chuyến này sẽ không được tính. |
| ui.pause.quit_body2 | Your journal is kept. | Sổ tay vẫn được giữ. |
| ui.pause.quit_confirm | Quit | Thoát |
| ui.rotate.title | Rotate your device | Hãy xoay thiết bị |
| ui.rotate.body | Turn your phone sideways to play. | Xoay điện thoại sang ngang để chơi. |

The two `ui.rotate.*` keys (W2-07, change 26) are shown by the template before Unity loads; `Lang` never reads them. They are added to `en.json` / `vi.json` as the source and for the sync check.

Reused existing keys: ui.title.settings, how_to_play, skip_tutorial, reset; ui.settings.*; ui.common.back/cancel/click_to_continue; ui.bench.redraw; rarity.*; depth.n.intro (5a-02; vi still missing, drafts used in f1/f2); tutorial.*. `ui.choice.catalyst_kicker` becomes unused for catalysts after change 22.
