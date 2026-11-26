---
type: canvas
tags: [data, persistence]
structure: mind-map
author: athanor-devs
generated-from: Data Design.md
---

# Athanor Data Canvas (Persistence & Localization)

```
[Athanor Data Layer]
   └─→ ContentData.cs ([Serializable])

       ├─→ Lang Files[2 files]
       │    ├─→ lang/en.json
       │    │    ├─→ ui.* → hud/bench/title/runover/settings/choice/goals/toast
       │    │    ├─→ story.* → intro/cutscene/ending
       │    │    ├─→ tutorial.* → step-by-step
       │    │    ├─→ effect.* → reaction descriptions
       │    │    └─→ note.* → environmental lore
       │    │
       │    └─→ lang/vi.json ← mirror of en (no missing keys)
       │         │
       │         └─→ Validation: LangTests ensures parity
       │
       ├─→ content assets (Assets/Content)
       │    ├─→ materials[] → 81 types, 70+ distinct
       │    ├─→ recipes[] → 87 pairs (59 known + 28 new)
       │    ├─→ catalysts[] → 26 total (10 existing + 16 new)
       │    ├─→ aspects[] → 16 types (13 run-bench modifiers)
       │    └─→ depthTargets[] → 4 ints for 5 depths

       └─→ Save System(PlayerPrefs / localStorage)
            │
            ├─→ athanor.journal.v1
            │    └─→ JournalData {
            │         ├─→ known[] (recipe IDs)
            │         └─→ hints[{material, partnerKind, heat}]
            │    }
            │
            ├─→ athanor.profile.v1
            │    └─→ ProfileData {
            │         ├─→ version(int)
            │         ├─→ rewardSeed
            │         ├─→ runsStarted
            │         ├─→ runsWon
            │         ├─→ wonNoRedraw[bool]
            │         ├─→ depthReached(int)
            │         ├─→ ignites(count)
            │         ├─→ chainLongest
            │         ├─→ comboLargest
            │         ├─→ bestIgnite
            │         ├─→ completed[](goal IDs)
            │         ├─→ unlockedStarters[]
            │         ├─→ unlockedCatalysts[]
            │         └─→ rewards[]
            │    }
            │
            └─→ athanor.intro.v1 → Tutorial seen flag[0/1]

## Version Management Rule

When shape changes:
1. Bump save key version (v1 → v2)
2. Keep reader for old format loading
3. Load as empty/fresh if corrupted (SaveTests)

## Data Flow Diagram

```
┌───────── content assets ───┐
│  (All Game Content)      │
└────────────┬─────────────┘
             │
       JsonUtility/System.Text.Json
             │
             ↓
    ┌─────────┴─────────┐
    │                    │
    ▼                    ▼
[ContentDb]          [Lang]
   │                    │
   ├─→ material data    ├─→ key lookup
   ├─→ recipe lookup    └─→ fallback chain (EN → vi)
   └─→ run state

┌───────────────┐
│ athanor.v1 save  │
└─────┬───────────┘
      │
      ▼
PlayerPrefs / localStorage
```

## Key Validation Rules

- Every `en` key must exist in `vi` (checked by LangTests)
- Reward buckets cannot exceed locked items per goal type
- Journal/Profile use same versioning scheme