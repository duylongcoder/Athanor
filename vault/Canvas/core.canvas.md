---
type: canvas
tags: [core, rules-engineering]
structure: mind-map
author: athanor-devs
generated-from: Core Index.md
---

# Athanor.Core Canvas (Unity-Free Rules)

```
[Athanor.Core]
   └─→ ContentDb
       │
       ├─→ [RecipeData] ← [MaterialData]
       │     ├─→ MaterialKind enum
       │     ├─→ ReactivityIndex[1-10]
       │     ├─→ Tier[0=starting material]
       │     └─→ Rarity{Common, Uncommon, Rare}
       │
       ├─→ RunState
       │    ├─→ IgniteResolver
       │    │    ├─→ PickLiviestTile(highest Reaction Index)
       │    │    └─→ FindBestRecipe(neighbours, minReactivity)
       │    │
       │    ├─→ ChainPlanner
       │    │    └─→ 3+ reactions = Combo
       │    │
       │    ├─→ ScoreParts
       │    │    ├─→ Base × Used × First × Step × Bonus
       │    │    └─→ Target = DepthTarget[5 depths]
       │    │
       │    └─→ RunState.Fields:
       │         ├─→ materials[81] (from content.json)
       │         ├─→ recipes[87 pairs]
       │         ├─→ catalysts[26]
       │         ├─→ aspects[16]
       │         └─→ depthTargets[5 ints]
       │
       ├─→ Satchel
       │    ├─→ DrawHand() → 5 cards
       │    └─→ LockedRewards[] (from goals)
       │
       ├─→ Journal
       │    ├─→ knownRecipes[]
       │    └─→ Hints[material + partnerKind + heat]
       │
       └─→ Lang
            └─→ Key Prefixes:
                 ├─→ ui.* → hud/journal/bench/title/runover/settings/choice/goals/toast
                 ├─→ story.* → intro/ending/cutscene
                 ├─→ tutorial.* → step-by-step instructions
                 ├─→ effect.* → reaction descriptions
                 └─→ note.* → environmental lore
```

## Core Systems

| System | Responsibility | Implementation |
|--------|---------------|----------------|
| **ContentDb** | Index content.json + validation rules | C# JSON deserialization with JsonUtility |
| **RunState** | Track run state per depth, scoring, combos | Seed-based RNG for deterministic behavior |
| **IgniteResolver** | Pick liveliest tile → resolve first reaction | Up to 4 neighbours (3 edge, 2 corner) |
| **ChainPlanner** | Sequentially chain reactions; score combo | Max 5-item combos rare by design |

## Gameplay Rules (Data-Driven)

1. **Potency Score**: `Base × Used × First × Step × Bonus`
2. **Reaction Index**: `Reactivity[1-10]` from materials → determines reaction order
3. **Recipe Matching**: Unordered items; old `{a,b}` syntax still read for backwards compat
4. **Depth Targets**: 5 depths, each with target potency (from `content.json`)

## Test Strategy

- Runs without Unity dependency: `dotnet test tools/CoreTests`
- Determinism required: same seeds → same results every run
- No hardcoded values: all numbers from content.json