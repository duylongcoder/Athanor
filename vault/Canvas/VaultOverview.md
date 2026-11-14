---
type: canvas
tags: [overview, home]
structure: mind-map
author: athanor-devs
source-files:
  - Home.md
  - Architecture.md
  - Design/Architecture.md
  - Data/Data Design.md
generated-from: vault/
---

# Athanor Vault Master Overview Canvas

```
[Athanor Project Vault]

   └─→ Core Systems (Unity-Free Layer)
        ├─→ ContentDb
        │    ├─→ Material lookup[81 types]
        │    ├─→ Recipe lookup[87 pairs]
        │    └─→ Validation rules
        │
        ├─→ RunState
        │    ├─→ IgniteResolver/ChainPlanner
        │    ├─→ ScoreParts[Base×Used×First×Step×Bonus]
        │    ├─→ Depth Targets[5 depths]
        │    └─→ Trials / Combos tracking
        │
        ├─→ Satchel → Hand draw → 5 cards
        ├─→ Journal ← Recipe hints discovered
        └─→ Lang[EN/Vi parity]

   └─→ Game Systems (Unity Runtime)
        ├─→ GameFlow (State Machine)
        │    ├─→ Screen Views[14 types]
        │    ├─→ Cutscenes[3 types]
        │    └─→ Save plumbing
        │
        ├─→ BenchView ×9 (3x3 grid)
        ├─→ LinkView → Light Motes
        └─→ UI/HUD/Tooltip

   └─→ Content Data (content.json)
        ├─→ Materials[81+ types] ← 70+ distinct
        ├─→ Recipes[87 pairs] ← 59 known + 28 new
        ├─→ Catalysts[26 total] ← 10 existing + 16 new
        ├─→ Aspects[16 types] ← 13 modifiers
        ├─→ Events[19 types] ← per-depth + extras
        └─→ Goals[22 total]

   └─→ Data Persistence
        ├─→ athanor.journal.v1 → known[] + hints[]
        ├─→ athanor.profile.v1 → all stats tracking
        └─→ lang/en.json, vi.json ← mirror parity

   └─→ Testing Coverage
        ├─→ Core Tests[dotnet/Unity-free] → 41+ tests
        ├─→ Unity Tests → Integration coverage
        ├─→ Lang Tests → EN/Vi parity check
        └─→ Automated Rules: Determinism + Isolation

   └─→ Agent Team Structure
        ├─→ phase-lead → Summarise → decide → split → dispatch
        ├─→ rules-engineer → Core rules + tests
        ├─→ unity-gameplay-engineer → UI + GameFlow
        ├─→ content-designer → Balance tuning
        ├─→ ui-ux-designer → Screen layouts
        └─→ qa-engineer → Sign-off & releases

## Layer Organization Summary

| Domain | Files | Description |
|--------|-------|-------------|
| **Core** | 14 files | Unity-free rules, C# tests |
| **Game** | ~50 files | Rendering, UI views, cutscenes |
| **Editor** | ~25 files | Scene/atlas/font builders |
| **Tests** | 39 docs | NUnit coverage per system |
| **Content** | vault/Content/ | All data types documented |

## Navigation Paths

- Home → Game Overview → Architecture → Data Design
- Content: Materials → Recipes → Catalysts → Aspects → Events → Goals
- Code: Core → Game → Editor → Tests → Tools
- Workflow: Agent → Phase → Devlog

## Generated Vault Updates

Run `python3 tools/vault/build_vault.py` after any content change to regenerate notes.