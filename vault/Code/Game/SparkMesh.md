---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Fx/SparkMesh.cs
tags: [code/game, kind/class]
aliases: []
---

# SparkMesh

`public sealed class SparkMesh` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/SparkMesh.cs:13-165`

> Many 1 px dots and 5x5 sparkles drawn as one mesh at one sorting order: one renderer and at most two draws (one per texture) instead of a SpriteRenderer each. Fill it every frame with Begin, Add, End. Centres are rounded to whole pixels here, and colours stay floats so HDR glow (above 1) reaches the bloom. Needs no engine module beyond what the game already uses (no ParticleSystem).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `struct Vert` | 15 |  |
| property | `struct Quad` | 16 |  |
| field | `public const float DotScale = 0.25f` | 19 | A dot is drawn at a quarter of its 4x4 sprite (1 px), as the SpriteRenderers did; a sparkle at full size. |
| property | `public int Count` | 41 |  |
| method | `public static SparkMesh Create(Transform parent, string name, int order, Sprite dot, Sprite sparkle)` | 44 | A child of parent at its origin; positions passed to Add are in parent's space. sparkle may be null. |
| method | `public static Material MaterialFor(Texture texture)` | 54 | The material for a texture: the shared "ParticleGlow" material (Resources) with that texture. |
| method | `public void Begin()` | 90 |  |
| method | `public void Add(Vector2 pos, Color color, bool sparkle)` | 92 |  |
| method | `public void End()` | 99 | Uploads what was added since Begin. Allocates only when the most quads so far grows. |
| property | `public Mesh Mesh` | 164 | For tests: the vertex positions and colours uploaded by the last End. |

## Used by
[[Dust]], [[Lane]], [[Loop]], [[SimpleCutscene]], [[SparkParticles]], [[Sparks]]

## Tested by
[[SparksTests]]
