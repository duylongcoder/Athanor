---
type: canvas
tags: [design, architecture]
structure: mind-map
author: athanor-devs
generated-from: vault
---

# Athanor Architecture Canvas

```
[Athanor Vault Structure]

  ↓ Core Layer
[Athanor.Core (Unity-Free)]
  └─→ ContentDb
      ├─→ Recipes[87 pairs] ← Materials[81 types]
      ├─→ RunState
      │    └─→ Depth Targets[5 depths]
      ├─→ IgniteResolver / ChainPlanner
      ├─→ Satchel
      ├─→ Journal → Hints
      └─→ Lang (en.json + vi.json)

  ↓ Game Layer
[Athanor.Game (Unity Runtime)]
  └─→ GameFlow
     ├─→ BenchView ↔ TileView[Hover/Mark]
     ├─→ LinkView[Light Motes]
     ├─→ HudView ← RunState
     ├─→ Cutscenes
     │    ├─→ ReactionCutscene
     │    └─→ SimpleCutscene[Mood() depth colors]

  ↓ Data Layer
[Athanor.Data (Persistence)]
  ├─→ Save (PlayerPrefs / localStorage)
  │    ├─→ athanor.journal.v1
  │    ├─→ athanor.profile.v1
  │    └─→ Intro Flag[0/1]

  ↓ Editor & Tools
[Athanor.Editor] → Scene Builders + Atlas Packer
```

## Layer Rules

| Layer | Path | Rule |
|-------|-------|------|
| **Core** | `Athanor/Assets/Scripts/Core` | Deterministic (seeded RNG), no UnityEngine, compiled by tools/CoreTests |
| **Game** | `Athanor/Assets/Scripts/Game` | All rendering, input, tweens (PrimeTween), cutscenes; owns screen state machine in GameFlow |
| **Editor** | `Athanor/Assets/Editor` | Scene/font/atlas/web-build tools; menu Athanor |

## Run Lifecycle Flow

```
Boot → Title → Intro → Bench 
         ↓
Resolving ↔ Journal ↔ Descent
         ↓
CatalystChoice ← AspectOffer
         ↓
Event ← AspectChoice
         ↓
Goals (by reward rarity)
         ↓
RunOver → Won / Lost
```

## Core Conventions

- Game-layer code **never** decides rules — it asks Core. New rule → Core + test first.
- Player-visible text goes through `Lang` keys (`lang/*.json`)
- Pixel art: 640x360 canvas; positions from `layout.json`
- New material/catalyst/aspect → icon in `tools/art/icons.mjs`

## Flow State Enum

```csharp
enum RunPhase { Boot, Title, Intro, Bench, Resolving, Journal, Descent, 
               CatalystChoice, AspectChoice, Event, Goals, Ending }
```