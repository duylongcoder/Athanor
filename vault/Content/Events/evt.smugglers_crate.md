---
generated: true
type: event
id: smugglers_crate
event_type: Gamble
min_depth: 3
max_depth: 5
weight: 2
aliases: [A Buried Crate]
tags: [content/event, event/gamble]
---

# A Buried Crate
_Gamble_ · depth 3–5 · weight 2

A crate, half swallowed by the roots.

## Option: Open it
- Cost: free
- 70%: “Packed in straw: something useful.” → GiveDiscovery 1
- 30%: “It spills grit across the bench.” → BlockTile 0

## Option: Walk on
- Cost: free
- 100%: “You leave it be.” → free

Back to [[Events]] · rules in [[RunState]] (event draw/apply, `RunStart.cs`)
