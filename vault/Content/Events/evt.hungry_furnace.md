---
generated: true
type: event
id: hungry_furnace
event_type: Choice
min_depth: 2
max_depth: 5
weight: 2
aliases: [A Keeper's Furnace]
tags: [content/event, event/choice]
---

# A Keeper's Furnace
_Choice_ · depth 2–5 · weight 2

An old furnace gapes, still warm after all this time.

## Option: Feed it a discovery  (requires Rack)
- Cost: LoseRack 1
- 100%: “It roars awake.” → FirstTriple 0

## Option: Walk on
- Cost: free
- 100%: “You leave it be.” → free

Back to [[Events]] · rules in [[RunState]] (event draw/apply, `RunStart.cs`)
