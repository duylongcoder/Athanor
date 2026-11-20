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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/BenchView.cs:14-431`

> The main screen: nine tiles, the hand of cards, the rack of discoveries and the HUD. Materials are placed by dragging them onto a tile, or by clicking one and then a tile; clicking a placed one takes it back.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action IgniteRequested, RedrawRequested, JournalRequested` | 19 |  |
| property | `public bool InputEnabled` | 20 |  |
| property | `public HudView Hud` | 21 |  |
| property | `public int TileCount` | 23 | How many tiles the bench holds (nine). |
| property | `struct Pick` | 44 |  |
| method | `public void Bind(ContentDb content, FxRefs fx)` | 80 | Once, after the Bench prefab is in the scene: hands over the content, finishes the nested Hud and Tooltip and hooks the Hud's buttons. Run state comes later, with Bind(run, journal). |
| method | `public void Bind(RunState state, Journal book)` | 94 |  |
| method | `public Vector3 TileCentre(int i)` | 102 |  |
| method | `public Vector3 RackSlot(int i)` | 104 | World position of a rack well (clamped to the rack). |
| method | `public void PreviewOnRack(int i, string id)` | 106 | Shows a freshly made material in a rack well until the next Refresh makes it official. |
| method | `public TileView Tile(int i)` | 111 |  |
| method | `public void Refresh(bool deal = false)` | 114 | Redraws tiles, hand, rack, links and HUD from the run. deal slides the cards in from below. |
| method | `public void ClearLinks()` | 195 | Hides the hand-drawn links while reactions resolve, so the bench reads cleanly. |
| method | `public IEnumerator FloatText(string text, Vector3 from, Color color, float seconds = 0.8f, bool small = false)` | 198 | A number that rises from a point and fades. |
| method | `public static string PlaceLine(int tile)` | 248 | Development-build log lines of a card put on / taken off a tile (tools/web/mainhd2d.mjs waits for them). |
| method | `public static string UnplaceLine(int tile)` | 249 |  |
| method | `public void Tick(float deltaTime)` | 280 | One frame of the bench: the pointer's picks, the hover marks and the tooltip. A frame where nothing changed builds no string: the tooltip is made when the pointer settles on something (or the run, the language or the journal changes), and the selection is redrawn only when it changes. |

## Uses
[[Align]], [[Art]], [[Bench]], [[CardView]], [[ChainPlanner]], [[ContentDb]], [[Dev]], [[FontKind]], [[FxRefs]], [[HudView]], [[Journal]], [[Kind]], [[Label]], [[LinkView]], [[Loc]], [[Motion]], [[PointerInput]], [[Px]], [[Rarities]], [[RunState]], [[Spawn]], [[TileView]], [[Tooltip]], [[Wording]]

## Used by
[[DepthStage]], [[GameFlow]], [[GoldenShots]], [[IgniteSequence]]

## Tested by
[[DepthClickTests]], [[HotPathTests]], [[LayoutPinTests]], [[MainDepthTests]], [[MainDevHookTests]], [[PrefabBakeTests]]
