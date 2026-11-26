---
type: canvas
tags: [testing, quality]
structure: mind-map
author: athanor-devs
generated-from: Tests Index.md
---

# Athanor Testing Canvas

```
[Testing Strategy]

   ├─→ Rule Tests (EditMode, Athanor.Gameplay.Tests)
   │    ├─→ ContentTests → 41 tests
   │    │    ├─→ Material validation
   │    │    ├─→ Recipe validity check
   │    │    └─→ Balance rules enforcement
   │    │
   │    ├─→ TutorialTests → 16 tests
   │    │    └─→ Step-by-step teaching flow
   │    │
   │    ├─→ LoreTests → 9 tests
   │    │    └─→ Environmental text consistency
   │    │
   │    ├─→ SparksTests → Tests by system
   │    │    ├─→ Chain tests
   │    │    ├─→ Ignite resolution
   │    │    └─→ Motion detection
   │    │
   │    ├─→ RunStateTests → 16 tests
   │    │    ├─→ Scoring rules verification
   │    │    └─→ Combo calculation correctness
   │    │
   │    ├─→ SatchelTests → Tests by system
   │    │    └─→ Hand draw validation
   │    │
   │    └─→ LangTests → 9 tests
   │         └─→ EN/vi parity check

   ├─→ Integration Tests (Unity) ← Unity-dependent
   │    ├─→ BenchView integration
   │    ├─→ HUD visibility checks
   │    ├─→ Cutscene flow testing
   │    └─→ Save/load round-trip tests
   │
   └─→ Automated Test Rules

       ├─→ Naming: Per engine standard
       ├─→ Determinism: No random seeds in assertions
       ├─→ Isolation: Set/tear down own state
       ├─→ No hardcoded: Factory functions over inline magic
       └─→ Independence: No external API DB/file I/O

   └─→ What NOT to Automate

       ├─→ Visual fidelity (shader output, appearance)
       ├─→ "Feel" qualities (responsiveness, weight, timing)
       ├─→ Platform-specific rendering
       └─→ Full gameplay sessions (playtest > automation)

   └─→ CI/CD Rules

       ├─→ Runs on every push to main
       ├─→ Blocks PR merge if tests fail
       ├─→ Never disable/skip failing tests — fix root issue

   └─→ Test Evidence Gates

       │    └─→ Logic → Automated test (BLOCKING)
       │    ├─→ Integration → Integration test or documented playtest
       │    ├─→ Visual → Retained screenshot + sign-off (BLOCKING)
       │    └─→ UI → Screenshot of each screen touched (BLOCKING)
```

## Test Evidence by Story Type

| Story Type | Required Evidence | Location | Gate Level |
|------------|-------------------|----------|------------|
| Logic (formulas, AI, state) | Automated unit test | `tests/unit/[system]/` | BLOCKING |
| Integration (multi-system) | Integration test OR documented playtest | `tests/integration/[system]/` | BLOCKING |
| Visual/Feel (animation, VFX) | Retained screenshot + lead sign-off | `production/qa/evidence/` | BLOCKING |
| UI (menus, HUD, screens) | Retained screenshot of each screen touched | `production/qa/evidence/` | BLOCKING |
| Config/Data (balance tuning) | Smoke check pass | `production/qa/smoke-[date].md` | ADVISORY |

## Core Test Coverage by System

- **ContentTests**: 41 tests for materials/recipes balance
- **TutorialTests**: 16 tests for teaching flow
- **LoreTests**: 9 tests for environmental text consistency
- **RunStateTests**: Tests for scoring and run mechanics

## Key Principles

1. **Determinism**: Tests produce same result every run — no random seeds
2. **Isolation**: Each test sets up/tears down own state independently
3. **No magic numbers**: Test fixtures use constant files or factory functions
4. **Full parse check**: Every story-change must be launched and observed before closure