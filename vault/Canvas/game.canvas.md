---
type: canvas
tags: [design, game-ui]
structure: mind-map
author: athanor-devs
generated-from: Game Index.md
---

# Athanor Game Layer Canvas

```
[Athanor.Game - Unity Runtime]
        ↓
[GameFlow: State Machine]
   └─→ Screen Views
       ├─→ [BootView] → TitleScreen
       ├─→ [TitleView] ← ResetButton → RunStats
       ├─→ [IntroView]
       │    └─→ Textured Page Fade(clicks bypass)
       ├─→ [BenchView][Main Gameplay]
       │    ├─→ [CardView] ← Satchel.Draw()[5 cards]
       │    ├─→ [TileView] ×9 (3x3 grid)
       │    │    └─→ [LinkView] → Light Motes
       │    │         ├─→ HoverHighlight
       │    │         └─→ MarkState
       │    ├─→ [HudView]
       │    │    ├─→ DepthCounter ← RunState
       │    │    ├─→ PotencyDisplay
       │    │    ├─→ TrialsRemaining ← RunState
       │    │    └─→ CatalystsHeld ← RunState
       │    └─→ [UiButton] → Standard[120x26px, Red]
       ├─→ [ReactionCutscene]
       │    └─→ Materials Spiral → Rune Circle → Flash
       ├─→ [SimpleCutscene]
       │    └─→ Mood() → Depth Colors
       │         ├─→ Blue(Depth 1)
       │         ├─→ Violet(Depth 2)
       │         ├─→ Red(Depth 3)
       │         ├─→ Gold(Depth 4)
       └─→ [CatalystChoiceView] → 3 Cards
   ```

## Screen-to-File Mapping

| View Class | File | Description |
|------------|------|-------------|
| **BenchView** | `Game/BenchView.md` | Main gameplay: drag-place materials, card hand, rack, HUD |
| **TileView** | `Game/TileView.md` | Individual tile: hover highlight, material icon, mark states |
| **HudView** | `Game/HudView.md` | Panels around bench: depth/potency counters, catalysts held, last reaction note |
| **CardView** | `Game/CardView.md` | Starting material cards in the 5-card hand |
| **LinkView** | `Game/LinkView.md` | Motes of light between neighbouring tiles with known recipes |
| **Cutscene** | `Game/Cutscene.md` | Generic cutscene framework, base sequence |
| **ReactionCutscene** | `Game/ReactionCutscene.md` | Full discovery: materials spiral → rune circle → flash → result rises |
| **SimpleCutscene** | `Game/SimpleCutscene.md` | Descent, intro, ending (short story beats) |
| **CatalystChoiceView** | `Game/CatalystChoiceView.md` | Three parchment cards, pick one after depth clearance |
| **EventView** | `Game/EventView.md` | Depth event: what found, options with costs/outcomes |
| **GoalsView** | `Game/GoalsView.md` | Goals page grouped by reward rarity (Uncommon/Rare/Legendary) |
| **JournalView** | `Game/JournalView.md` | Journal: discovered recipes + hints; silhouettes for unknowns |
| **RunOverView** | `Game/RunOverView.md` | Run summary: goals completed, won or lost |
| **TitleView** | `Game/TitleView.md` | Title screen with progress stats + reset button |
| **UiButton** | `Game/UiButton.md` | Standard 120x26 red button, hover/press states |
| **Tooltip** | `Game/Tooltip.md` | Small panel beside pointer: title + 2 lines of text |

## Pixel Art Specifications

- Canvas: 640×360 (from `layout.json`)
- Origin: top-left (y-axis down)
- All positions come from tile data in `layout.json`

## Key Implementation Rules

- Game-layer code never decides rules — it asks Core for decisions.
- Bench drag-to-place with click-and-drag fallback.
- 5-item combos are rare; informed player usually wins.