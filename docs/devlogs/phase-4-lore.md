# Phase 4: Lore (rough draft)

Status: **Draft for owner review.** Nothing is implemented. Part of [phase-4-prep.md](phase-4-prep.md).
Owner direction (2026-11-13): the opening cutscene is enough as a format, but expand it for a broader story context;
where possible, adjust the in-game events to match the lore. Endings and several Ultimates are deferred.

Owner decisions (2026-11-13, second pass):

- The last Keeper is named **"The Keeper"**. No line changes: the nine approved lines stay word for word. The name is
  only shown if names under the Keepers are chosen (choice 2), under the lantern Keeper in scene 5.
- **Depth intros are postponed** (not in this pass; see "Not decided", now decided).
- **A Lore event is marked as seen** so it is not shown twice in a row: accepted. Plan: Task 13 in
  [2026-11-13-athanor-phase4-tutorial-lore.md](../superpowers/plans/2026-11-13-athanor-phase4-tutorial-lore.md)
  (the profile remembers the last Lore event; balance-neutral).
- An HTML mockup of the opening cutscene to review the storyboard and the 11 choices live:
  [docs/mockup/cutscene/index.html](../mockup/cutscene/index.html) (see "Mockup" below).

## The world in five lines

1. Under the old workshop is a cellar that **grows**. Nobody dug it.
2. It grows because of the **Keepers**. Each one brewed down to the bottom, made the Worldseed, and planted it. Each seed
   added a depth. The rooms, tools and shrines on the way down are what earlier Keepers left behind.
3. The **last Keeper** went down and did not come back up. The player takes over their bench.
4. The **Book** (the Journal) is the one thing that comes back up every time. It remembers what the hands forget, which is
   why discoveries are kept across runs and nothing else is.
5. A failed run is a climb back up the stair at dawn. A won run plants a seed, and the cellar gets a little deeper.

Everything already in the game fits: the intro ("Nothing down here is labelled"), the ending ("Plant it, and the cellar
will grow a little deeper"), the depth names (The Root of Stone, The Quiet Below), the Worldseed that "hums with every
reaction". Nothing in the rules changes.

| Game thing | In the story |
|---|---|
| Run | One descent |
| Journal | The Book, passed from Keeper to Keeper |
| Catalysts | Tools of earlier Keepers, found on the way |
| Aspects | The bench settling into the depth: tiles warm, crack, take root |
| Events | Rooms and traces of earlier Keepers along the stair |
| Depths | Layers grown by earlier seeds; older the deeper you go |
| Worldseed | What each Keeper makes at the bottom and plants |

## Opening cutscene (storyboard)

Seven scenes, about 44 s, plus the wait on "CLICK TO BEGIN". The nine approved lines are kept word for word: one line
for each of scenes 1 to 6, and the last three together in scene 7, as the intro does now. **No wording changes.**

Rules for every scene:

- Letterbox on throughout: black bars at y 0 to 30 and 330 to 360, with the brass 1 px edge. The picture is the 640x300
  between them.
- Text is written out in the top slots, as now: one line at y 38 in scenes 1 to 6; three lines at y 38, 54 and 70 in
  scene 7. BitPotion 16 px, cream `#f0dcc0` with a dark shadow. The art keeps y 31 to 90 dark enough to read over.
- Clicks work as now: a click finishes the line being written, the next click ends the wait. A click during a move
  (a scroll or a walk) jumps to the end of that move.
- Coordinates are pixels from the top-left of the 640x360 screen.
- The Worldseed is never shown. Scene 4 shows only a light hidden in two hands. The ending stays the reveal.

### Summary

| # | Time (s) | On screen | Camera / motion | Text | Sound idea | Art |
|---|---|---|---|---|---|---|
| 1 | 5 | The workshop at night: wall, arched window with moonlight, empty table, lit candles. No tiles, no UI. | Still. At the end the whole room slides up out of frame, as in the descent. | 1. Under the old workshop, the cellar keeps going down. | Candles, wind at the window | EXISTS: `workshop_wall`, `window`, `moon_shaft`, `workshop_table`, `candle` + `flame_small_*` |
| 2 | 5 | The cellar stair, seen from the side: a switchback stair cut in rock, roots in the walls, darker as it goes down. Motes drift up. | Camera scrolls down the stair, 360 px in 4 s. | 2. No one dug it. It grew, one Keeper at a time. | Low rumble, slow heartbeat of the roots | NEW: `cellar_stair`. EXISTS: motes (the descent streaks) |
| 3 | 7 | Lower half of the stair. Three Keepers, black silhouettes, walk down one after another. Each carries a satchel and a small glowing Book. | Camera still. Keepers walk the diagonal, 1.5 s apart, and fade out into the dark. | 3. Each went down with a satchel, a bench, and a Book. | Footsteps on stone, one set after another | NEW: `keeper_walk`. EXISTS: `glow` (the Book's light) |
| 4 | 7 | The bottom: a rock floor, roots overhead. A Keeper kneels and presses a hidden light into the floor. Rune circle and rings pulse, roots spread, the floor sinks. | Still, then a short shake. The floor drops 24 px; dark opens below. | 4. At the bottom they made something, and planted it. | A hum, then a deep crack | NEW: `cellar_bottom`, `keeper_kneel`, `roots_grow`. EXISTS: `rune_circle`, `floor_ring`, `glow`, Light2D, flash, shake |
| 5 | 6 | Upper half of the stair. One Keeper with a lantern goes down alone, looks back once, goes on. | Camera still. The lantern light shrinks to a point and goes out. | 5. The last Keeper went down and did not come back up. | One set of footsteps, fading; silence | NEW: `keeper_last`. EXISTS: `cellar_stair` (from scene 2), `glow`, Light2D |
| 6 | 5 | The workshop again, the same frame as scene 1, but the candles are low and one is out. Then a cut to the Book, open, with nothing in it. | Still. Cut at 2 s from the room to the Book. | 6. The bench is yours now. The Book is empty. | A page turning; quiet | EXISTS: the scene 1 pieces; `journal` (empty, as on a first run) |
| 7 | 9 (+ wait) | The cauldron room, as now: brick wall, floor ring, the cauldron over a fire, embers rising. | Still. Embers loop. | 7. Each depth asks for a stronger brew. Nothing down here is labelled. / 8. The Book will remember what your hands forget. / 9. So mix. Watch what happens. Remember. Then "CLICK TO BEGIN". | Fire, bubbling, the music's first full phrase | EXISTS: `cutscene_room`, `cauldron`, `logs`, `flame_big_*`, embers |
| | **44** | | | | | 6 NEW, the rest EXISTS |

### Scene 1: The workshop above (5 s)

- **Composition.** The bench scene with nothing on the table: `workshop_wall` full screen, `window` at x 280, y 16
  (its top is under the bar), `moon_shaft` falling from it onto the table, `workshop_table` at x 102, y 132, the candles
  where `layout.json` puts them. The line sits over the dark window and wall.
- **Motion.** 0.9 s fade in from black. The line types out (about 1.3 s), then holds 1.2 s. The candles flicker.
- **Transition.** The room slides up 400 px in 0.9 s (the same move as the descent), with motes rushing upward. It
  reads as "we go down".
- **The player should understand:** this is the workshop of the title screen, and there is something under it.

### Scene 2: The stair going down (5 s)

- **Composition.** `cellar_stair` is two screens tall (640x720). A stair in switchbacks down a rock shaft, seen from the
  side. The top half has worked stone and an old iron rail; the bottom half is rougher, with roots through the walls and
  older, darker rock. The top 60 px of each half stay dark for the text.
- **Motion.** The camera scrolls from the top half to the bottom half, 360 px in 4 s, ease in and out. Motes drift up
  slowly. The line types out during the scroll.
- **Transition.** The scroll settles on the bottom half; scene 3 starts on the same frame with no cut.
- **The player should understand:** the cellar is deep, old and not man-made all the way. It grows.

### Scene 3: The Keepers (7 s)

- **Composition.** The bottom half of `cellar_stair`. The stair runs from top-left (x 70, y 120) to bottom-right
  (x 570, y 310), where it goes into the dark. Keepers are 32x56 silhouettes, pure dark with a thin warm rim light on one
  side. Each carries a satchel (in the sprite) and a Book: a 3 px warm point with a small `glow` at hip height.
- **Motion.** Three Keepers walk down the diagonal, about 70 px/s, starting 1.5 s apart. Each fades in at the top-left
  and fades out into the dark at the bottom-right. Each has a different outline (hood, hat, bare head) so they read as
  three people. The line types out as the first one appears.
- **Transition.** After the third one is gone, fade to black, 0.4 s.
- **The player should understand:** many people came before. They all went down, with the same tools as the player.

### Scene 4: The planting (7 s)

- **Composition.** `cellar_bottom`: a rock floor across the lower third (y 250 to 330), roots hanging from the top
  edge, darkness at the sides. A kneeling Keeper at x 304, y 262 (32x56). `rune_circle` centred at x 320, y 300, under
  the Keeper.
- **Motion.** Fade in 0.5 s. The Keeper kneels (frame 1), a small warm light sits between the hands, covered, never more
  than 6 px across (`glow` + Light2D, warm gold, not the Worldseed's colour). The hands press down (frame 2): the light
  goes into the floor, `rune_circle` lights, two `floor_ring`s widen out, `roots_grow` spreads across the floor. Short
  shake, then the floor and the Keeper sink 24 px and a band of new dark opens at the bottom.
- **Transition.** A soft flash, then cut to black, 0.3 s.
- **The player should understand:** at the bottom something is made and planted, and that is what makes the cellar
  deeper. Not what the thing is.

### Scene 5: The last Keeper (6 s)

- **Composition.** The top half of `cellar_stair`, still and darker than in scene 2. One Keeper with a lantern starts
  at the top-left landing (x 80, y 130). The lantern is the only light: `glow` radius about 60 px plus a Light2D.
- **Motion.** Fade in 0.4 s. The Keeper walks down three steps, stops, turns back for 0.6 s (the look-back frame), and
  goes on into the dark. The lantern light shrinks to a point and goes out.
- **Transition.** Black for 0.6 s, then fade in on scene 6.
- **The player should understand:** this one did not come back, and nobody came after. Until now.

### Scene 6: The empty bench and the Book (5 s)

- **Composition.** First 2 s: the same frame as scene 1, but the candles are smaller and the one at x 226, y 136 is out.
  The moonlight is weaker. Then a cut to the Book: the `journal` panel, centred (600x330, so it fills the picture), with
  every cell empty and no page text, as it looks on a first run.
- **Motion.** Still. The line types out on the Book shot.
- **Transition.** Fade through black, 0.6 s, into the cauldron room.
- **The player should understand:** the player is the new Keeper. The Book is the Journal they will fill.

### Scene 7: The cauldron room (9 s, then the wait)

- **Composition.** As now: `cutscene_room` full screen, `cauldron` at x 220, y 118 (rim at x 320, y 161), the fire at
  y 290, embers rising from it. Lines at y 38, 54, 70. "CLICK TO BEGIN" in the bottom bar at y 341.
- **Motion.** Embers loop. The three lines type out one after another, 0.8 s apart, as the current intro does.
- **Transition.** "CLICK TO BEGIN" waits up to 8 s or a click, then fade out 0.4 s to the bench, as now.
- **The player should understand:** the rules of play: go deeper, mix without labels, the Book keeps what is found.

### New art

Sizes are per frame. All sprites follow the spec's look: shaded, outlined, warm light against cool dark.

| Name | Size (px) | Frames | Used in | Notes |
|---|---|---|---|---|
| `cellar_stair` | 640x720 | 1 | 2, 3, 5 | Two screens: worked stone above, roots and old rock below. Dark band at the top of each half for text. |
| `keeper_walk` | 32x56 | 4 x 3 outlines = 12 | 3 | Silhouette walk down a stair, with satchel. Outlines: hood, hat, bare head. |
| `keeper_kneel` | 32x56 | 3 | 4 | Kneel, press down, still. Hands cupped; nothing visible in them. |
| `keeper_last` | 32x56 | 5 | 5 | Walk (4) plus a look-back. Holds a lantern; lit side warm. |
| `cellar_bottom` | 640x360 | 1 | 4 | Rock floor, hanging roots, dark sides. Room below the floor for the 24 px sink. |
| `roots_grow` | 160x48 | 6 | 4 | Roots spreading out from the centre across the floor. |

Count: **6 new assets, 30 frames.** Everything else is existing art or code effects (`glow`, `rune_circle`,
`floor_ring`, motes, embers, flash, shake, Light2D).

### Owner input

Fill in the box (x, or a short answer). The proposed answer is what this storyboard assumes. Every choice can be
switched live in the [mockup](../mockup/cutscene/index.html); its "Copy my choices" button gives a text summary to paste
back here.

| Choice | Proposed | Owner |
|---|---|---|
| Show the Keepers' faces? | No, silhouettes only | [ ] |
| Names under the Keepers in scene 3 (Tiny font, fading)? If yes, give three names. | No | [ ] |
| How many Keepers in scene 3? | 3 | [ ] |
| Does the last Keeper look back in scene 5? | Yes, once | [ ] |
| Scene 6 close-up: reuse the empty `journal` panel, or draw a new open blank book (about 200x130)? | Reuse `journal` | [ ] |
| Colour of the light in the hands in scene 4 | Plain warm gold, not the Worldseed's colour | [ ] |
| Scene 6: same night, or dawn light through the window? | Same night, candles burnt low | [ ] |
| A way to skip the whole cutscene at once (hold click, or Esc)? | Not now; per line only | [ ] |
| Play on first launch only (as the spec says), or also from the title screen? | First launch, plus a title button later | [ ] |
| Music: one cue across all scenes, or a change at scene 5? | One cue, drops out in scene 5, back in scene 7 | [ ] |
| More scenes to add (where, and what)? | | [ ] |

## Events (rewritten for the lore)

Rules, ids, types, depths and weights are unchanged; only `name`, `text` and outcome texts change. Limits (from the event
window): name at most 22 characters, text at most 56, outcome at most 52. The two "Walk on" outcomes stay "You leave it be."

| id | Name | Text | Outcome texts |
|---|---|---|---|
| cracked_altar | A Keepers' Altar | A cracked altar to the Keepers asks for an offering. | The altar drinks it in. Time slows. |
| tilted_workbench | An Old Workbench | Another Keeper's bench. Tilted, but roomy. | More room, less time. / You wedge it level. |
| old_ledger | A Keeper's Ledger | A damp ledger lists what another Keeper tried. | One line is still legible. |
| hungry_furnace | A Keeper's Furnace | An old furnace gapes, still warm after all this time. | It roars awake. |
| dripping_well | A Dripping Well | *(unchanged)* Something glints at the bottom of a dripping well. | *(unchanged)* |
| shifting_bench | A Shifting Bench | Roots shift under the floor. The bench grinds. | A tile settles over a warm root. / A tile jams shut. |
| smugglers_crate | A Buried Crate | A crate, half swallowed by the roots. | *(unchanged)* |
| forgotten_satchel | A Forgotten Satchel | A satchel like yours, left on the stair. | *(unchanged)* Someone's work, half finished. |
| glinting_seam | A Glinting Seam | An old brew has set into the wall like ore. | *(unchanged)* |
| sealed_shrine | A Sealed Shrine | A shrine to the first Keeper hums at your catalysts. | *(unchanged)* |
| quiet_shrine | A Quiet Shrine | *(unchanged)* | *(unchanged)* |
| damp_air | Damp Air | Water seeps down from the stair. Everything cools. | *(unchanged)* |
| falling_grit | Falling Grit | Grit falls from the roots onto the bench. | *(unchanged)* |
| cold_draft | A Cold Draft | A cold draft comes up the stair, through your pockets. | *(unchanged)* |
| warm_stone | A Warm Stone | A stone in the wall is warm. Something grows behind it. | *(unchanged)* |
| quiet_room | A Keeper's Room | A cot, a cup, a name scratched out. Dust turns slowly. | *(unchanged)* |

### Three new Lore events

The story is told mostly by these: one "Go on" option, no effect (like `quiet_room`), weight 1, icon `evt_lore`.
Together they say the story in order: the Keepers before you, the cellar is alive, what to do at the bottom.

| id | Depths | Name | Text | Outcome |
|---|---|---|---|---|
| names_in_wall | 1 to 2 | Names in the Wall | Names cut into the wall, one under another. | The last one is fresh. There is room below it. |
| warm_roots | 3 to 4 | Warm Roots | Roots push through the ceiling. They are warm, and hum. | You feel the hum in your teeth. |
| pinned_note | 5 | A Pinned Note | A note pinned to a bench: 'Plant it. Do not climb back.' | You fold it away. |

Balance: these add 3 to the event weight pool and do nothing, so they slightly thin the other events. The plan checks
the win rate with the balance report before and after; the cut-off is a change of no more than 3 points.

### Mockup

[docs/mockup/cutscene/index.html](../mockup/cutscene/index.html): open the file in a browser (works from disk, no server).
All seven scenes play in order with the storyboard's timings, the typed lines (a click finishes a line, the next ends
the wait, a click in a scroll or walk jumps to its end) and "CLICK TO BEGIN". Scene buttons 1 to 7, pause, restart,
speed (¼x to 2x), zoom. The 11 choices sit beside the picture and change the playback at once; "Copy my choices" copies
a summary. Placeholder art (drawn in code, for composition and motion only): `cellar_stair`, `keeper_walk`,
`keeper_kneel`, `keeper_last`, `cellar_bottom`, `roots_grow`, and the "new blank book" option.

It also shows four pixelfx flipbooks, as real sprite-sheet exports from `pixelfx/tools/render.mjs --sheet` (with a
game-palette version of three): implosion or holy pillar at the planting (scene 4), poof when a Keeper leaves (scene 3),
collect burst when the Book appears (scene 6), switchable under "Effects to try", plus a looping gallery.

What the mockup showed about the storyboard (for the owner):

- Scene 6: the cream line at y 38 sits on the journal's parchment and is hard to read. Either darken the panel's top,
  move the line into the bar, or use the new book (which leaves dark around it).
- Scene 4: after the 24 px sink the kneeling Keeper's feet go under the bottom bar (y 330). Kneel 10 px higher or sink less.
- Scene 3: at the stated 70 px/s a Keeper needs about 7.6 s for the whole flight; the mockup walks each one in 3.6 s
  (about 150 px/s) and fades them out three quarters of the way, so three fit in 7 s 1.5 s apart. For 4 or 5 Keepers
  the gap shrinks so the scene stays 7 s.
- Scene 7 before "CLICK TO BEGIN" is about 6 s with the current intro's 0.8 s gaps, not 9 s; the total before the wait
  is about 41 s, not 44.

## Decided (were "Not decided")

1. Depth intros (one line under the depth name in the descent): **postponed**, not in this pass.
2. A Lore event is marked as seen in the profile so it is not shown twice in a row: **accepted** (plan Task 13).
3. The name of the last Keeper: **"The Keeper"**.

## Log

- 2026-11-13: Event texts and three Lore events shipped in content.json. BalanceMatrix after (baseline in phase-4-prep.md):
  - fresh                                        wins 132/200 (66%), avg depth 3,76, lost at 1..5: 7 4 23 33 1
  - fresh + all catalysts                        wins 164/200 (82%), avg depth 3,81, lost at 1..5: 7 3 10 16 0
  - fresh + all starters                         wins 134/200 (67%), avg depth 3,83, lost at 1..5: 4 3 25 34 0
  - half                                         wins 164/200 (82%), avg depth 3,91, lost at 1..5: 3 1 11 21 0
  - full                                         wins 166/200 (83%), avg depth 3,87, lost at 1..5: 4 2 10 18 0
  - fresh +aspects+events                        wins 129/200 (64%), avg depth 3,96, lost at 1..5: 1 2 20 47 1
  - fresh + all catalysts +aspects+events        wins 173/200 (86%), avg depth 3,97, lost at 1..5: 1 4 9 13 0
  - fresh + all starters +aspects+events         wins 140/200 (70%), avg depth 4,01, lost at 1..5: 0 1 17 40 2
  - half +aspects+events                         wins 163/200 (81%), avg depth 4,04, lost at 1..5: 0 2 12 22 1
  - full +aspects+events                         wins 169/200 (84%), avg depth 3,97, lost at 1..5: 0 1 13 17 0
  - fresh, aspects only                          wins 144/200 (72%), avg depth 3,90, lost at 1..5: 3 3 25 25 0
  - fresh, events only                           wins 129/200 (64%), avg depth 3,83, lost at 1..5: 5 4 23 37 2
