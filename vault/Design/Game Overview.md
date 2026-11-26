---
type: design
tags: [design]
---
# Game Overview

**Athanor** — a run-based alchemy puzzle for the browser (Unity 6000.6.7f1, Universal 2D, 640x360 pixel-perfect). Lay materials on a 3x3 bench, **Ignite**, see what reacts. Nothing is labelled: recipes are found by trying and kept in the **Journal** across runs. Five depths, each demanding more potency, ending in one substance the game never names in advance ([[Depths]]).

## Core loop
1. A **hand** of starting materials is drawn from the [[Satchel]]; the player places them on the bench ([[BenchView]], [[Bench]]).
2. **Ignite** ([[IgniteResolver]], [[ChainPlanner]]): the liveliest tile (highest Reaction Index) reacts first with the biggest recipe its neighbours allow (up to 4 on the centre, 3 on an edge, 2 in a corner). The result stands in that tile and can react again: a **chain**; three or more reactions in one Ignite is a **combo**.
3. Potency is scored per reaction ([[ScoreParts]]: Base x Used x First x Step x Bonus). Reach the depth target before the **trials** run out ([[RunState]]).
4. Clear a depth → pick a **catalyst**; at the run start and depths 3 and 5 pick an **aspect**; most depths open with an **event**.
5. Across runs: [[Journal]] (recipes + hints) and [[Profile]] (stats, **goals** that unlock locked starters/catalysts).

## Pillars (from the phase specs)
- Discovery over instruction: recipes are never listed up front. The tutorial teaches the rules, not the recipes.
- Every placement is spent; the rack (12 places) keeps only what a chain left standing.
- Content is data (assets under `Assets/Content`); rules are plain C# models ([[Architecture]]).

Specs and decisions: [[Phases]].
