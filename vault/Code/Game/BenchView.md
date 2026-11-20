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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/BenchView.cs:14-440`

> The main screen: nine tiles, the hand of cards, the rack of discoveries and the HUD. Materials are placed by dragging them onto a tile, or by clicking one and then a tile; clicking a placed one takes it back.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action IgniteRequested, RedrawRequested, JournalRequested` | 19 |  |
| property | `public bool InputEnabled` | 20 |  |
| property | `public HudView Hud` | 21 |  |
| property | `public int TileCount` | 23 | How many tiles the bench holds (nine). |
| property | `public struct Timing` | 49 | The deal's gap between two cards, and how long the pointer rests before a tooltip shows (seconds). |
| property | `struct Pick` | 53 |  |
| method | `public void Bind(ContentDb content, FxRefs fx)` | 89 | Once, after the Bench prefab is in the scene: hands over the content, finishes the nested Hud and Tooltip and hooks the Hud's buttons. Run state comes later, with Bind(run, journal). |
| method | `public void Bind(RunState state, Journal book)` | 103 |  |
| method | `public Vector3 TileCentre(int i)` | 111 |  |
| method | `public Vector3 RackSlot(int i)` | 113 | World position of a rack well (clamped to the rack). |
| method | `public void PreviewOnRack(int i, string id)` | 115 | Shows a freshly made material in a rack well until the next Refresh makes it official. |
| method | `public TileView Tile(int i)` | 120 |  |
| method | `public void Refresh(bool deal = false)` | 123 | Redraws tiles, hand, rack, links and HUD from the run. deal slides the cards in from below. |
| method | `public void ClearLinks()` | 204 | Hides the hand-drawn links while reactions resolve, so the bench reads cleanly. |
| method | `public IEnumerator FloatText(string text, Vector3 from, Color color, float seconds, bool small = false)` | 207 | A number that rises from a point and fades. |
| method | `public static string PlaceLine(int tile)` | 257 | Development-build log lines of a card put on / taken off a tile (tools/web/mainhd2d.mjs waits for them). |
| method | `public static string UnplaceLine(int tile)` | 258 |  |
| method | `public void Tick(float deltaTime)` | 289 | One frame of the bench: the pointer's picks, the hover marks and the tooltip. A frame where nothing changed builds no string: the tooltip is made when the pointer settles on something (or the run, the language or the journal changes), and the selection is redrawn only when it changes. |

## Uses
[[Align]], [[Art]], [[Bench]], [[CardView]], [[ChainPlanner]], [[ContentDb]], [[Dev]], [[FontKind]], [[FxRefs]], [[HudView]], [[Journal]], [[Kind]], [[Label]], [[LinkView]], [[Loc]], [[Motion]], [[PointerInput]], [[Px]], [[Rarities]], [[RunState]], [[Spawn]], [[TileView]], [[Tooltip]], [[Wording]]

## Used by
[[DepthStage]], [[DevLiveEdit]], [[GameFlow]], [[GoldenShots]], [[IgniteSequence]]

## Tested by
[[DepthClickTests]], [[HotPathTests]], [[LayoutPinTests]], [[MainDepthTests]], [[MainDevHookTests]], [[PrefabBakeTests]]
