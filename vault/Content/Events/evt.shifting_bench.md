---
generated: true
type: event
id: shifting_bench
event_type: Gamble
min_depth: 2
max_depth: 5
weight: 2
aliases: [A Shifting Bench]
tags: [content/event, event/gamble]
---

# A Shifting Bench
_Gamble_ · depth 2–5 · weight 2

Roots shift under the floor. The bench grinds.

## Option: Let it settle
- Cost: free
- 50%: “A tile settles over a warm root.” → HeatTile 3
- 50%: “A tile jams shut.” → BlockTile 0

## Option: Walk on
- Cost: free
- 100%: “You leave it be.” → free

Back to [[Events]] · rules in [[RunState]] (event draw/apply, `RunStart.cs`)
