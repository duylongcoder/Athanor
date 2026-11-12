# Athanor: design

Date: 2026-11-11. Working title "Athanor" (my call; the user has not named the game).

## Intent

A small casual puzzle game about curiosity. The player keeps experimenting with fantasy materials on an
alchemy bench, learns from what happens, and feels the thrill of finding the right combination after a
string of trials. There is always a deeper layer, ending in one mythical substance the game never names
in advance.

The user asked for an MVP in scope but top-tier presentation: striking pixel art with a 2.5D retro look,
glow, impactful feedback, fluid animation, and cutscenes.

## Goals

- Discovery through experiment: recipes are never shown up front; failed mixes still teach something.
- Run-based play borrowing ideas (not content) from Balatro and 9 Kings: a small hand, placement on a
  3x3 grid where neighbours matter, permanent rule-benders, escalating numbers with loud feedback.
- A candlelit fantasy workshop look, approved in the browser mockup (see "Approved look").
- Reaction cutscenes that make watching a result a show.
- Runs in a browser via Unity WebGL; everything is ordinary Unity assets and scripts the user can adjust.

## Non-goals (MVP)

- Monetization, accounts, cloud saves, timers, leaderboards.
- Mobile-specific layout or touch tuning (mouse first; nothing should prevent touch later).
- Sound and music (my call: no audio assets were asked for; leave clean hooks so it can be added).
- Localization.

## Anti-goals

- Cyberpunk or neon styling. The first mockup was rejected for this.
- Basic, flat pixel art. The bar is 16/32-bit polish: shaded, outlined sprites, real light and shadow.
- Recreating Balatro, 9 Kings or any existing alchemy game: no names, art, cards or layouts from them.
- Tedious result screens. A known reaction must resolve in about a second.

## What the user decided

- Engine: Unity, deployed as WebGL. Editor 6000.6.7f1 (already installed with the Web module).
- Structure: runs (fail, restart, get further), not one endless lab.
- Run rules as listed under "How a run plays".
- Look: the second mockup (fantasy workshop, perspective bench, strong light/dark contrast).
- Lettering: option C. Headings in Alagard, reading text in BitPotion.
- Effects: use pixelfx (MIT), the library behind https://patch.gg/patch/pixel-effects.
- Materials: a wide range across potions, powders, ores, ingots, plants, flowers and beast parts.

## How a run plays

- A run descends through 5 depths. Depths 1 to 4 each have a potency target and 4 trials to reach it.
- A trial: draw 5 materials from the satchel, place any number on empty tiles of the 3x3 bench, press
  Ignite. Each pair of orthogonal neighbours reacts once, in reading order. Then the bench clears.
- A pair with a recipe produces a result material, scores potency, and (first time ever) is a discovery
  recorded in the journal. A pair with no recipe fizzles and reveals a hint.
- Clearing a depth: descent cutscene, then choose 1 of 3 catalysts (permanent for the run).
- Running out of trials below the target ends the run. The journal persists between runs.
- Depth 5 has no potency target: the run is won by creating the one ultimate substance.
- 2 redraws per depth: discard the whole hand and draw 5 again.

### Details I settled (my calls, tune freely)

- Satchel: starts with 2 copies of each of the 8 starting materials. Placed materials go to a discard
  pile; hand leftovers too. When the satchel is empty the discard is shuffled back in. Every reaction
  result adds one copy of that material to the discard, capped at 3 copies per material.
- Recipes are unordered pairs of specific materials. Results have a tier 1 to 5; starters are tier 0.
  Base potency by tier: 20, 60, 180, 500 for tiers 1 to 4.
- Potency of a reaction = base x 2 if it is a first-ever discovery this run, then catalysts apply.
  A trial's reactions also chain: the nth reaction in one Ignite is multiplied by n.
- Depth targets: 300, 900, 2500, 7000.
- Hints: a fizzle reveals, for one of the two materials, the kind of a partner it does react with
  ("Ember Coal stirs near a POTION"). Hints already given are kept in the journal.
- Content size: 8 starters, about 30 discoveries, 1 ultimate (tier 5), 7 kinds (ORE, METAL, CRYSTAL,
  POWDER, POTION, PLANT, BEAST). The tier 5 recipe needs two tier 4 materials.
- 10 catalysts, each one sentence, keyed to kinds, tiles or order. Examples: reactions involving a
  PLANT score double; the centre tile's reactions fire twice; the first reaction of each trial scores
  triple; diagonal neighbours also react; +1 trial per depth; +1 hand size.
- Journal persistence: PlayerPrefs (works in WebGL), storing discovered recipes and hints.

## Screens and flow

Title -> intro cutscene (first launch only, skippable) -> bench -> reaction cutscene -> bench ... ->
descent cutscene -> catalyst choice -> next depth ... -> ending cutscene or run-over summary -> title.

- Bench (main screen): workshop background; 3x3 slab in perspective; hand of 5 cards along the bottom;
  top left depth name, potency and progress bar; top right catalyst sockets, trials and redraws;
  bottom left "last reaction" parchment with the latest hint; bottom right journal button and Ignite.
  Links glow between placed neighbours that have a known recipe. Unknown pairs show no link.
- Journal: overlay listing discovered materials with their recipes, undiscovered ones as silhouettes,
  and collected hints.
- Reaction cutscene: for each reaction in an Ignite. First-ever discovery gets the full sequence
  (about 5 s: materials spiral into the cauldron, rune circle lights, flash and shake, result rises on a
  column of light, name, potency counts up). Known reactions get a short version (about 1 s). Fizzles
  get a short puff with the hint. Click skips.
- Descent, intro and ending cutscenes: short, built from the same pieces (background, effects, text).

## Approved look

The reference is the browser mockup, kept at `docs/mockup/` (copied from the brainstorm session) with
its source painter. Key properties to carry into Unity:

- 640x360 base resolution, integer scaling, point filtering.
- Dark room; warm candle light pools, cool moonlight shaft from an arched window, coloured glow from
  the materials themselves; bloom on emissive pixels.
- Oak table and stone slab in one-point perspective; raised tiles with visible front faces.
- UI: dark wood panels with brass studs, parchment for notes and cards.
- Fonts: Alagard (16/32/48 px) for headings and numbers; BitPotion 16 px for reading text; Pixel
  Operator (CC0) for tiny labels and card names. Alagard needs a credit line (CC BY 3.0 per
  OpenGameArt). BitPotion's licence is unconfirmed: verify before shipping, else use Pixel Operator.
- 25 material icons exist in the mockup (24x24). About 15 more are needed, plus the ultimate.

## How it gets built (my calls unless marked)

- Unity 6000.6.7f1, Universal 2D template, URP 2D renderer with Light2D, a global Volume for bloom,
  Pixel Perfect Camera at 640x360. (Unity and WebGL are the user's decision.)
- Art is generated by code: the mockup's JavaScript painter becomes a small Node tool in `tools/art/`
  that writes PNG sprites (icons, props, background layers, UI pieces) into the Unity project. Art
  stays reproducible and easy to tweak.
- Effects: pixelfx sprite sheets (PNG + JSON) exported with its `tools/render.mjs`, played in Unity by
  a small flipbook component. Running the exporter needs the user's permission in this environment.
  Shortlist: magic circle cast, mana burst, implosion, holy pillar, sunburst, rune circle, collect
  burst, spirit flame, candle, torch, god rays, fireflies, burn-away, iris, quest complete, level up.
- Game rules live in plain C# with no Unity dependencies (materials, recipes, satchel, scoring,
  catalysts, run state) so they can be covered by edit-mode tests. Content is data (JSON), not code.
- UI framework chosen at build time following the Unity UI guidance; never OnGUI.
- Repository root is this folder: `Athanor/` (Unity project), `tools/`, `docs/`. The user's
  `pixel-effects/` clone is left alone and ignored by git.

## Left to the builder

- The full recipe graph, material names and catalyst list (must satisfy the sizes above and be
  winnable: a path to the ultimate must exist using starters only).
- Exact timings, easing and particle counts for cutscenes.
- Balance numbers beyond those listed.

## Success

- A WebGL build that loads in a browser and plays a full run from title to ending or run-over.
- Edit-mode tests pass for the rules.
- Side by side with the mockup, the Unity bench and cutscene read as the same game or better.
