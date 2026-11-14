---
type: canvas
tags: [content, statistics]
structure: mind-map
author: athanor-devs
generated-from: Content Tests Index
---

# Athanor Content Statistics Canvas

```
[Athanor Content Inventory]

   └─→ Materials[81+ types]
        └─→ 70+ distinct (79 listed in Core Index.md)
             │
             └─→ [Starting Tiers][Tier 0]
                  ├─→ Stone
                  ├─→ Grit
                  ├─→ Sand
                  └─→ Gold? (rarest?)

   └─→ Recipes[87 pairs]
        └─→ 59 known + 28 new
             │
             └─→ Old {a, b} syntax still read

   └─→ Catalysts[26 total]
        ├─→ Existing:10 (bellows, TinkersEye, etc.)
        └─→ New planned:16
             │
             └─→ Effect ∈ Effects enum

   └─→ Aspects[16 types]
        ├─→ 13 run-bench modifiers
        └─→ Grouped as Cheap vs Engine

   └─→ Events[19 types]
        ├─→ Per-depth (5 base events)
        │    └─→ One per depth + extras
        │         └─→ [quiet_shrine][sealed_shrine][cold_draft]
        │
        └─→ Event Types ∈ Ops.Types:
             ├─→ Choice → 3 Options{label, requires}
             ├─→ Gamble → Win/Loss with odds
             ├─→ Find → Fixed outcome
             ├─→ Shrine → Quiet/Sealed locations
             └─→ Hazard/Boon/Lore

   └─→ Goals[22 total]
        └─→ Per locked item in content.json
             │
             └─→ RewardRarity bucket:
                  ├─→ Uncommon
                  ├─→ Rare
                  └─→ Legendary

## Balance Rules Enforced

1. Path to ultimate exists (playable win condition)
2. Tiers add up (potency scales correctly)
3. Reaction Index spread 1-10 balanced
4. Informed player usually wins
5. 5-item combos stay rare (hard ceiling)
6. Hazard/Gamble ≥70% event-free wins

## Content Files Mapping

| Document | Vault Path | Source Count |
|----------|------------|---------------|
| Materials | Code/Core Index.md + Symbols.md | 81+ types |
| Recipes | vault/Code/Core | 87 pairs |
| Catalysts | vault/Content/Catalysts/ | 26 total |
| Aspects | vault/Content/Aspects/ | 16 types |
| Events | vault/Content/Events/ | 19 types |
| Goals | content.json goals[] | 22 total |

## Content Health Check

Run `python3 tools/vault/build_vault.py` after any changes to regenerate.
Changes to balance values → read BalanceMatrix and rebuild.