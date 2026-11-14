---
type: canvas
tags: [content, balance]
structure: mind-map
author: athanor-devs
generated-from: Content Index.md
---

# Athanor Content Canvas

```
[Athanor Content Data]
   └─→ content.json (All Shapes in ContentData.cs)

       ├─→ Materials[81 types]
       │    ├─→ [Stone] ← Tier 0 (starting)
       │    ├─→ [Grit]
       │    ├─→ [Sand]
       │    ├─→ [Gold] → Rarest starting material?
       │    └─→ [...] (70+ types total)
       │         │
       │         └─→ Reactions: 87 Pairs
       │             │
       │             └─→ minReactivity = heat required
       │                 │
       │                 └─→ Result substance in recipe

       ├─→ Catalysts[26]
       │    ├─→ Effect ∈ Effects enum
       │    ├─→ Kind (e.g., [DeepSatchel, TinkersEye])
       │    ├─→ Amount[int]
       │    └─→ Rarity{Common, Uncommon, Rare}

       ├─→ Aspects[16]
       │    ├─→ Effect ∈ AspectEffects enum
       │    ├─→ Scope: {Tile, Link, Bench}
       │    ├─→ Group: {Cheap, Engine}
       │    └─→ TradeOff (mutually exclusive bonuses)

       ├─→ Events[19 types]
       │    └─→ Type ∈ Ops.Types:
       │         ├─→ Choice → 3 Options{label + requires}
       │         ├─→ Gamble → Win/Loss with odds
       │         ├─→ Find → Fixed outcome
       │         ├─→ Shrine → [Quiet, Sealed] locations
       │         └─→ Hazard/Boon/Lore

       └─→ Goals[22 total]
            └─→ StatId{unlocks}
                 └─→ RewardRarity bucket
```

## Balance Rules Enforced by Tests

- **Path to ultimate exists** — game must be winnable
- **Tiers add up** — potency scaling is consistent
- **Reaction Index spread** — 1-10 distribution matters
- **Informed player usually wins** — good strategy = success
- **5-item combos stay rare** — intentional difficulty curve
- **Hazard/Gamble ≥70% event-free wins** — fair balance

## Content File Structure

```json
{
  "materials": [{id, name, kind, tier, reactivity, glow, rarity}],
  "recipes": [{items[2..5], minReactivity, result}],
  "catalysts": [{id, name, text, effect, kind, amount, rarity, locked}],
  "aspects": [{id, name, text, effect, scope, group, amount, tradeOff}]
}
```

## Data Flow

1. **Materials** → define `kind` and `reactivity` (Reaction Index)
2. **Recipes** → unordered pairs; `minReactivity` = heat threshold needed
3. **Catalysts** → modify reaction outcomes via `Effect` enum
4. **Aspects** → scope effects (Tile/Link/Bench); group by Cheap/Engine
5. **Events** → random modifiers at depth event times with weighted outcomes
6. **Goals** → stat targets unlock locked starter materials/catalysts

## Tuning Knobs Identified

- Material reactivity (1-10 scale)
- Recipe `minReactivity` heat thresholds
- Catalyst effects and their potency levels
- Aspect trade-offs (what you give up for the bonus)