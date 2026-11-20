# Devlog #1: plan and draft

## Plan

**Goal.** Introduce the game to people who land on the page, explain one run in under a minute of reading, and ask for
feedback on the first build.

**Readers.** itch.io browsers who have never heard of the game. No dev jargon (no "Core", "CI", "Phase").

**Post type.** itch.io devlog, type *Game release* (or *Announcement* if the build is not public yet).

**Title options.**
1. Athanor: nothing down here is labelled *(recommended: it uses the game's own opening line)*
2. The first build of Athanor is up
3. Devlog #1: a cellar that grows

**Structure (about 350 words).**

| # | Section | Image |
|---|---|---|
| 1 | Hook: what the game is, in two sentences | `cover.png` as the post thumbnail |
| 2 | How a run works: bench, Ignite, chains | `screenshot_1_bench.png` |
| 3 | Discovering things: the Journal, hints | `screenshot_2_discovery.png`, `screenshot_5_journal.png` |
| 4 | Going deeper: depths, catalysts, aspects, events | `screenshot_3_descent.png`, `screenshot_4_catalyst.png` |
| 5 | What's in this build (numbers) | none |
| 6 | What's next and a request for feedback | none |

**Spoiler rules (from the lore rules, keep them).** Never name the final substance, never explain a recipe, never show
the ending screen. `screenshot_2_discovery.png` shows a mid-game result, which is fine.

**Before posting.**
- [ ] Check the build on the page is the one this post describes (English and Vietnamese, pause menu, settings).
- [ ] Set the itch.io page title to match the game: the page says "Crucible" and the game says "Athanor" (or say why in the post).
- [ ] Choose the post type and attach the images in the order above.
- [ ] Tags: puzzle, alchemy, roguelite, pixel-art, browser.

---

## Final post (paste below this line)

Facts checked on 2026-11-20 against `content.json`, `docs/ATHANOR.md` § 1 and `Athanor/Build/web` (10.6 MB). Changes
from the first draft: the last depth works differently (6 trials, no target), so the post says "the first four"; basic
materials return to your bag rather than being used up; redraws and "one of three" catalysts are added.
The `[Image: …]` lines show where each image goes. Insert the image there and delete the line. itch.io's editor does not
read Markdown, so paste the headings and bold text as formatting, or use the editor's Markdown/HTML mode.

---

# Athanor: nothing down here is labelled

[Image: cover.png, set as the post thumbnail]

**Athanor** is a small alchemy puzzle you play in your browser. You lay strange materials on a 3x3 bench, press
**Ignite**, and see what reacts. No recipe is written down anywhere. You find every one of them by trying.

## How a run works

[Image: screenshot_1_bench.png]

Each turn you draw a hand of five materials. Place as many as you like on the bench and press Ignite. The most reactive
material looks at its neighbours, and if together they make something, they react. The result appears on that tile and
can react again with whatever sits next to it. One Ignite can set off a chain, and each link in the chain is worth more
than the one before.

Whatever you place is spent. Your basic materials come back in later hands, but something new you made is gone once you
put it down, so pick your moment. Anything the chain makes and leaves standing goes to your rack for later. Twice per
depth you can redraw a hand you don't like.

## Learning by trying

[Image: screenshot_2_discovery.png]

A mix that does nothing still teaches you something: neighbours that fizzle can leave a hint. Every reaction you find is
written into your **Journal**, and the Journal is the one thing you keep between runs. Each run you know a little more
than the last.

[Image: screenshot_5_journal.png]

## Going deeper

[Image: screenshot_3_descent.png]

A run goes down through five depths under an old workshop, from the Shallow Cellar to the Quiet Below. The first four
each set a potency target to reach in four trials, and each asks for more than the one before. Clear one and you choose
one of three **catalysts**, tools left by the alchemists who came before you, which change the rules for the rest of the
run. Along the way you also pick **aspects** that change the bench itself, and you come across rooms and traces of the
people who went down before you (**events**).

[Image: screenshot_4_catalyst.png]

What waits at the bottom is never named. You will have to make it to find out.

## In this build

- 81 materials and 87 recipes to discover
- 26 catalysts, 16 aspects and 19 events
- 22 goals that unlock new catalysts and starting materials
- A short tutorial in your first run (you can skip it once your Journal has entries), and "How to play" to replay it
- English and Vietnamese
- About an 11 MB download. Best with a mouse; phones held sideways are partly supported

## What's next

There is no sound yet. That comes next, along with more balancing on the deeper depths.

If you play, I'd love to hear from you in the comments: how deep did you get, which reaction surprised you, and where
you got stuck.

Thanks for reading, and good luck down there.
