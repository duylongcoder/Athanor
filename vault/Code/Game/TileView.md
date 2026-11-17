---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/TileView.cs
tags: [code/game, kind/class]
aliases: []
---

# TileView

`public sealed class TileView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/TileView.cs:10-129`

> One of the nine bench tiles: its hover highlight and, when occupied, the material standing on it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int index` | 12 |  |
| field | `public Vector3 centre` | 13 |  |
| property | `public string Material` | 24 |  |
| property | `public bool Hovered` | 25 |  |
| property | `public bool Pinged` | 27 | Lit softly in its marker's colour, to show which tile an aspect in the HUD sits on. |
| property | `public bool Shut` | 29 | Nothing can be placed here (a cold tile, or grit from an event). |
| property | `public bool Marked` | 30 |  |
| property | `public SpriteRenderer Icon` | 31 |  |
| method | `public void Build(int i, TileData data, Transform parent)` | 33 |  |
| property | `public Vector2 FaceCentre` | 59 | The face the tile is hit-tested on: centre and size in flat world units. |
| property | `public Vector2 FaceSize` | 60 |  |
| method | `public bool Contains(Vector2 world)` | 62 |  |
| method | `public void SetState(Color? marker, bool shut)` | 66 | The marker's colour (null: no marker) and whether the tile is shut. |
| method | `public void Show(MaterialData m, bool pop)` | 76 |  |
| method | `public void Clear()` | 89 |  |
| method | `public void Flash(Color c)` | 97 | A quick bright pulse of the whole tile face. |
| method | `public IEnumerator Consume()` | 100 | The material sinks into the tile in a puff of its own colour. |

## Uses
[[Anim]], [[Make]], [[Motion]], [[Px]], [[Sparks]]

## Used by
[[BenchView]], [[DepthTestBuilder]], [[DepthTestControls]], [[DepthTestEvidence]], [[MainDepthEvidence]]

## Tested by
[[DepthClickTests]], [[MainDepthTests]]
