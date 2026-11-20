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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/TileView.cs:10-136`

> One of the nine bench tiles: its hover highlight and, when occupied, the material standing on it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int index` | 16 |  |
| method | `public void Bind(FxRefs effects)` | 19 | Takes the effect players (BenchView calls it). |
| field | `public Vector3 centre` | 25 |  |
| property | `public string Material` | 50 |  |
| property | `public bool Hovered` | 51 |  |
| property | `public bool Pinged` | 53 | Lit softly in its marker's colour, to show which tile an aspect in the HUD sits on. |
| property | `public bool Shut` | 55 | Nothing can be placed here (a cold tile, or grit from an event). |
| property | `public bool Marked` | 56 |  |
| property | `public SpriteRenderer Icon` | 57 |  |
| property | `public Vector2 FaceCentre` | 60 | The face the tile is hit-tested on: centre and size in flat world units. |
| property | `public Vector2 FaceSize` | 61 |  |
| method | `public bool Contains(Vector2 world)` | 63 |  |
| method | `public void SetState(Color? marker, bool shut)` | 67 | The marker's colour (null: no marker) and whether the tile is shut. |
| method | `public void Show(MaterialData m, bool pop)` | 77 |  |
| method | `public void Clear()` | 91 |  |
| method | `public void Flash(Color c)` | 99 | A quick bright pulse of the whole tile face. |
| method | `public IEnumerator Consume()` | 102 | The material sinks into the tile in a puff of its own colour. |
| method | `public void Tick(float deltaTime)` | 118 | One frame of the tile. An empty tile nobody points at writes nothing; a material's ring pulse is the only thing written every frame. |

## Uses
[[Anim]], [[Art]], [[FxRefs]], [[Motion]], [[Palette]], [[Px]], [[Sparks]]

## Used by
[[BenchView]]

## Tested by
[[DepthClickTests]], [[HotPathTests]], [[LayoutPinTests]], [[MainDepthTests]]
