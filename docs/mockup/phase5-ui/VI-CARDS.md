# Vietnamese bench: hand cards and the top-left HUD (spec, no code)

Evidence looked at (all 1280x720 = 2x of the 640x360 canvas; px below are 1x): `5a-12/vi_06`, `vi_30`, `a_06`, `a_30`; `5a-10/web/c05_bench_after_damage.png` (current build, VI).

## What is actually off

### 1. Card names (VI) sit 5 px too high and clip into the art window
Hand cards: top y 291, bottom y 361 with shadow (card H 70, art window bottom y ~325, kind strip y 348..357). Same box in EN and VI: **the card itself is not moved by the language** (EN a_06 and VI vi_06 have identical frames). What differs is the text:

| Item | EN glyph centre (1x) | VI glyph centre (1x) | Delta |
|---|---|---|---|
| one-line name ("Sunbloom") | y 332 | y 331 | -1 |
| two-line name, line 1 ("Iron / Ore", "Deep / Tear") | y 331 | y 326 | **-5** |
| two-line name, line 2 | y 341 | y 336 | **-5** |
| kind strip text | y 352 | y 352 | 0 |

Crux 16 has a higher glyph box than Pixel Operator Bold at the same Label y (`CardView.cs:40-41`: line1 y 2, line2 y -8, one-line y -3). Line 1 of a two-line name lands on y 326 +/- 4, i.e. on the art window's bottom edge (y 325): the top row of "Deep", "Wyrm", "Bone" is cut by the window. This is the "overlap the card art". Names are still English content text in VI ("Deep Tear"), so the two-word names wrap in VI exactly as in EN; they are no wider, only higher.

### 2. "Strip at the very bottom edge"
True in both languages: strip text centre y 352, glyph bottom y ~357, frame edge 360, so 3 px under the strip text (VI capitals with marks: "DƯỢC THỦY", "THẢO MỘC" reach y 357-358). Not a VI regression, but VI has no slack for marks. The old shots vi_06/vi_30 also show "B T" / "THẢO M C": **Ộ missing from the baked Tiny font**. c05 (current) shows BỘT / THẢO MỘC correctly, so that part is fixed; keep c05 as the reference.

### 3. HUD "Bậc Thang Rỉ Nước" is not overflowing its panel
Panel `hud_left` is x 8..158; the name starts x 17 and is ~110 px wide (ends ~x 127), budget 17..153 = 136 px. Longest VI depth name (17 chars) fits with 26 px spare. What cuts it is the **tutorial text box**: `TutorialController.cs:57` draws it at **x 120..520 (400 wide), y 6..56**, but SPEC change 17 says **x 168..472 (304 wide), y 14, text x 176, wrap 288**. The code box covers x 120..158 of the left panel (depth name tail, "/ 300" target) and x 480..520 of the right panel (catalysts/trials). EN looks fine only because "The Shallow Cellar" ends at x 98. So the HUD fix is the tutorial box, not HudView.

## Verdict

- **No separate VI card variant.** One fix, per font, in the card text layout.
- **No HudView change.** Fix the tutorial box to the SPEC rectangle.
- layout.json: `card` w 74 / h 70 is unchanged and consistent (frame 74x70 + 3x4 shadow). It has no text boxes, so nothing in it changes. Hand tiles/rack are not affected.

## Proposed boxes (1x px; card-local y is up from the card centre, as in `CardView.Text`)

Card (both languages): frame 74x70, art window bottom y(screen) 325, kind strip box y 348..357, unchanged.

Name block, window for text = screen y 327..347 (20 px), x within card inner width 66.

| Case | EN (unchanged) | VI (new, Crux 16) |
|---|---|---|
| one word / one line | local y -3 | local y -3 (centre 331, within 1 px of EN; no change) |
| two lines, line 1 | local y 2 | **local y -3** (glyph centre screen 331) |
| two lines, line 2 | local y -8 | **local y -13** (glyph centre screen 341, pitch stays 10) |
| kind strip | local y -24 | -24 (no change) |

I.e. in VI, two-line names move down 5 px so they occupy the same screen rows as EN (331 / 341). Ink then spans y ~327..346, clear of the window edge (325) and 2 px above the strip (348). A name with a mark above capitals (Ă, Ô: not in current names) needs 3 px more: if localizer-vi ever translates names with such capitals on a two-line name, shrink to one line is impossible, so cap them at one line (see open question 2).

Implementation shape (for the engineer): a per-font vertical bias for the Name role in `Make.Text`/`Label` for Crux (+5 for two-line, +1 for one-line), instead of a VI branch in `CardView`. Same bias check for any other Name-role text sharing the box.

Tutorial box (VI and EN): restore SPEC. Edge rect x 168, y 14, w 304; fill x 169, y 15, w 302; text x 176, y 21, wrap 288; line pitch 12 EN / 16 VI; height = 12 + lines x pitch (+12 read steps); max 4 EN / 3 VI lines; never lower than y 100. At 304 wide, the right edge is x 472, 8 px clear of the right panel (x 480) and 10 px clear of the left (x 158). Depth name / potency in the left panel (x 8..158, y 8..80) are then never covered.

Check against code: `TutorialController.cs:57-58, :114` use 120/6/400/50 and scale 400/4. `HandRect` (126,284,392,72) covers the hand with a 1 px margin: cards (x 208..432 for 3, y 291..361) are inside x, but the rect bottom y 356 is **5 px short of the card bottom (361)**; the spotlight cuts the shadow and strip. Suggest h 80 (y 284..364, clamp to 360).

## Mock
Not produced: the mockup frames use the HTML fonts, which do not reproduce the Crux ascent difference, so a frame would show the right result by construction and prove nothing. Verify in the build instead (below).

## Acceptance (screenshots into `production/qa/evidence/phase5/5a-03/`)
1. VI bench with Deep Tear, Wyrm Fang, Bone Ash: no row of name ink above y 327; line 1/2 centres 331/341 (+/- 1).
2. VI tutorial step 1_6 and 5_4c: box spans x 168..472; "Bậc Thang Rỉ Nước" and "/ 1.500" fully visible.
3. EN unchanged: pixel diff of EN bench = 0.
