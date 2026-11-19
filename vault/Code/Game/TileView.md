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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/TileView.cs:10-114`

> One of the nine bench tiles: its hover highlight and, when occupied, the material standing on it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int index` | 16 |  |
| method | `public void Bind(FxRefs effects)` | 19 | Takes the effect players (BenchView calls it). |
| field | `public Vector3 centre` | 25 |  |
| property | `public string Material` | 34 |  |
| property | `public bool Hovered` | 35 |  |
| property | `public bool Pinged` | 37 | Lit softly in its marker's colour, to show which tile an aspect in the HUD sits on. |
| property | `public bool Shut` | 39 | Nothing can be placed here (a cold tile, or grit from an event). |
| property | `public bool Marked` | 40 |  |
| property | `public SpriteRenderer Icon` | 41 |  |
| property | `public Vector2 FaceCentre` | 44 | The face the tile is hit-tested on: centre and size in flat world units. |
| property | `public Vector2 FaceSize` | 45 |  |
| method | `public bool Contains(Vector2 world)` | 47 |  |
| method | `public void SetState(Color? marker, bool shut)` | 51 | The marker's colour (null: no marker) and whether the tile is shut. |
| method | `public void Show(MaterialData m, bool pop)` | 61 |  |
| method | `public void Clear()` | 74 |  |
| method | `public void Flash(Color c)` | 82 | A quick bright pulse of the whole tile face. |
| method | `public IEnumerator Consume()` | 85 | The material sinks into the tile in a puff of its own colour. |

## Uses
[[Anim]], [[Art]], [[FxRefs]], [[Motion]], [[Px]], [[Sparks]]

## Used by
[[BenchView]], [[DepthTestControls]]

## Tested by
[[DepthClickTests]], [[MainDepthTests]]
