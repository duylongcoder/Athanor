---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/BenchView.cs
tags: [code/game, kind/class]
aliases: []
---

# BenchView

`public sealed class BenchView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/BenchView.cs:14-371`

> The main screen: nine tiles, the hand of cards, the rack of discoveries and the HUD. Materials are placed by dragging them onto a tile, or by clicking one and then a tile; clicking a placed one takes it back.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action IgniteRequested, RedrawRequested, JournalRequested` | 16 |  |
| property | `public bool InputEnabled` | 17 |  |
| property | `public HudView Hud` | 18 |  |
| property | `struct Pick` | 23 |  |
| property | `sealed class Slot` | 24 |  |
| method | `public void Build(LayoutData layout, ContentDb content, Transform parent)` | 36 |  |
| method | `public void Bind(RunState state, Journal book)` | 74 |  |
| method | `public Vector3 TileCentre(int i)` | 82 |  |
| method | `public Vector3 RackSlot(int i)` | 84 | World position of a rack well (clamped to the rack). |
| method | `public void PreviewOnRack(int i, string id)` | 86 | Shows a freshly made material in a rack well until the next Refresh makes it official. |
| method | `public TileView Tile(int i)` | 91 |  |
| method | `public void Refresh(bool deal = false)` | 94 | Redraws tiles, hand, rack, links and HUD from the run. deal slides the cards in from below. |
| method | `public void ClearLinks()` | 173 | Hides the hand-drawn links while reactions resolve, so the bench reads cleanly. |
| method | `public IEnumerator FloatText(string text, Vector3 from, Color color, float seconds = 0.8f, bool small = false)` | 176 | A number that rises from a point and fades. |
| method | `public static string PlaceLine(int tile)` | 225 | Development-build log lines of a card put on / taken off a tile (tools/web/mainhd2d.mjs waits for them). |
| method | `public static string UnplaceLine(int tile)` | 226 |  |

## Uses
[[Align]], [[Bench]], [[CardView]], [[ChainPlanner]], [[ContentDb]], [[Dev]], [[FontKind]], [[HudView]], [[Journal]], [[Kind]], [[Label]], [[LayoutData]], [[LinkView]], [[Loc]], [[Make]], [[Motion]], [[PointerInput]], [[Px]], [[Rarities]], [[RunState]], [[TileView]], [[Tooltip]], [[Wording]]

## Used by
[[DepthStage]], [[GameFlow]], [[MainDepthEvidence]]

## Tested by
[[MainDevHookTests]]
