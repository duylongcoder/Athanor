# Phase 5: Lore (depth intros, window views, event audit)

Status: **Draft (2026-11-15), brief 5a-02.** English only; `localizer-vi` translates later (5a-08). Canon:
[phase-4-lore.md](phase-4-lore.md); owner decisions: [phase-5-prep.md](phase-5-prep.md).

Rules followed: the world is a stack of dungeons (cellar layers grown by earlier seeds, older the deeper); the Ultimate
is the Worldseed, with no true, good or bad ending; "The Keeper" is never named in these lines; no recipe is explained;
the final substance is not named. Default staging: the view outside the window changes and one effect plays; bench and
slots stay the same.

## Depth intro lines

Key `depth.<n>.intro` in `lang/en.json`, shown under the depth name in the Descent banner. Limit 56 characters.

| Key | Depth name (content.json) | Line | Chars |
|---|---|---|---:|
| depth.1.intro | The Shallow Cellar | The air smells of cold stone and old candle. | 44 |
| depth.2.intro | The Dripping Stair | Water finds its way down. So do you. | 36 |
| depth.3.intro | The Humming Dark | Something below keeps a slow, warm note. | 40 |
| depth.4.intro | The Root of Stone | The roots here are thicker than the walls. | 42 |
| depth.5.intro | The Quiet Below | No hum now. Only the place where seeds are planted. | 51 |

Notes: the lines follow the arc of the world (newest layer to oldest): a workshop's cellar, the water that gets in,
the first hum of a seed, roots that have taken the stone, then the silence at the bottom where each Keeper planted.
Line 5 says "seeds" and "planted" but never names the substance.

## Window view per depth (art notes for `pixel-artist`)

The window is the arched pane (glass radius 40, `tools/art/scene.mjs` `windowPane()`), drawn unlit so it glows. Today it
shows a night sky at every depth. Underground there is no sky, so the pane shows what the layer "sees": a shaft, a drip,
a glow. One looping effect each. Palette: stay inside the game palette; no new hues.

| Depth | Seen through the window | Effect |
|---|---|---|
| 1 | A pale slice of the workshop floor far above, a shaft of grey light going up | Dust motes drifting slowly up the shaft |
| 2 | Wet black stone, a stair step edge | Drips falling down the glass, one every second or two |
| 3 | A deep blue-black with a warm amber glow far off | The glow pulses slowly, like a breath |
| 4 | Pale root tips pressing against the glass from outside | Roots creep a pixel at a time across the pane edge, then hold |
| 5 | Near black; one faint point of light low in the glass | Stillness; the point flickers once in a long while |

The intro line and the window change at the same moment (the Descent banner), then the view stays for the whole depth.

## Audit: Phase 4 event lore vs `content.json`

Source: lore table `phase-4-lore.md:182-210`; limits name 22 / text 56 / outcome 52 (all counted by hand; the longest is
`pinned_note` text at exactly 56). "Unchanged" rows in the lore table were checked against the current text.

| # | id | Name | Text | Outcome(s) | Result |
|---:|---|---|---|---|---|
| 1 | cracked_altar | A Keepers' Altar | match | "The altar drinks it in. Time slows." / "You leave it be." | match |
| 2 | tilted_workbench | An Old Workbench | match | "More room, less time." / "You wedge it level." / "You leave it be." | match |
| 3 | old_ledger | A Keeper's Ledger | match | "One line is still legible." / "You leave it be." | match |
| 4 | hungry_furnace | A Keeper's Furnace | match | "It roars awake." / "You leave it be." | match |
| 5 | dripping_well | A Dripping Well | match (unchanged) | unchanged | match |
| 6 | shifting_bench | A Shifting Bench | match | "A tile settles over a warm root." / "A tile jams shut." | match |
| 7 | smugglers_crate | A Buried Crate | match | unchanged | match |
| 8 | forgotten_satchel | A Forgotten Satchel | match | "Someone's work, half finished." | match |
| 9 | glinting_seam | A Glinting Seam | match | unchanged | match |
| 10 | sealed_shrine | A Sealed Shrine | match | unchanged | match |
| 11 | quiet_shrine | A Quiet Shrine | match (unchanged) | unchanged | match |
| 12 | damp_air | Damp Air | match | unchanged | match |
| 13 | falling_grit | Falling Grit | match | unchanged | match |
| 14 | cold_draft | A Cold Draft | match | unchanged | match |
| 15 | warm_stone | A Warm Stone | match | unchanged | match |
| 16 | quiet_room | A Keeper's Room | match | unchanged | match |
| 17 | names_in_wall | Names in the Wall | match | "The last one is fresh. There is room below it." | match |
| 18 | warm_roots | Warm Roots | match | "You feel the hum in your teeth." | match |
| 19 | pinned_note | A Pinned Note | match | "You fold it away." | match |

Result: **19 of 19 match**, 0 differ. Depth ranges of the three Lore events also match the lore table (`names_in_wall` 1-2, `warm_roots` 3-4,
`pinned_note` 5-5).

Fit with the Phase 5 decisions: "dungeons", the Worldseed and the Keepers are consistent with all 19 texts. The
"first Keeper" in `sealed_shrine` is not "The Keeper" (that is the last one), so no conflict. Nothing here implies a
true, good or bad ending.

## Decided (new canon, 2026-11-15)

1. Each depth has one intro line (`depth.<n>.intro`), shown under the depth name on the Descent banner.
2. The window shows a layer-specific view and one effect per depth (table above); bench and slots do not change.
