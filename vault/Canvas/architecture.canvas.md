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
[Athanor.Gameplay (plain C# models)]
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
  └─→ GameManager
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
| **Gameplay** | `Athanor/Assets/Scripts/Gameplay` | Deterministic (seeded RNG) plain C# models in the Unity assembly Athanor.Gameplay |
| **Game** | `Athanor/Assets/Scripts/Game` | All rendering, input, tweens (DOTween), cutscenes; managers and the screen loop (GameManager) |
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