---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Fx/Sparks.cs
tags: [code/game, kind/class]
aliases: []
---

# Sparks

`public sealed class Sparks` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/Sparks.cs:13-164`

> Glowing pixel particles: bursts, trails and rising embers. Particles are plain structs integrated here; each sorting order is one lane drawn by one SparkMesh (one renderer, at most two draws), so a burst of 90 costs one object instead of 90. Allocates only when a lane grows past its most particles so far. Building with ATHANOR_SPARK_PARTICLES draws through ParticleSystems instead (kept to measure the engine module's size; see docs/ATHANOR.md §2).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const float TrailRate = 60` | 16 | Trail emitters call Trail once per frame; it emits this many particles per second, whatever the frame rate. |
| property | `public int LiveCount` | 40 | Particles alive now, over all lanes. |
| property | `public int LaneCount` | 55 | Objects drawing sparks (one per sorting order used so far). |
| method | `public SparkMesh LaneMesh(int order)` | 58 | The mesh drawing one sorting order, or null if nothing was emitted at it yet (for tests). |
| method | `public void Emit(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0)` | 61 | One particle. gravity pulls down (negative floats up); sparkle uses the cross-shaped sprite. |
| method | `public void Trail(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0)` | 78 | A trail behind something moving, called once per frame: the same density at 30, 60 or 144 Hz (TrailRate per second; exactly one per frame at 60 Hz). Extra particles of a long frame start part-way through their life, so they spread out as if emitted during the frame. |
| method | `public static int TrailCount(float dt, float r)` | 90 | How many trail particles a frame of dt seconds emits; r is uniform in [0, 1). |
| method | `public void Burst(Vector2 pos, int count, Color color, float speed, float life, int order, float gravity = 0, float squash = 1)` | 93 | A ring of particles flying outward. |
| method | `public void Clear()` | 102 |  |
| method | `public static bool Step(ref Particle p, float dt)` | 111 | Advances one particle by dt; false once it has died. The integrator the SpriteRenderer pool used. |
| method | `public static Color Shade(in Particle p)` | 122 | Its colour this frame: full until half its life is gone, then fading out. |
| method | `public void Advance(float dt)` | 139 | Moves every particle on by dt and drops the dead (Update calls it; public for tests). |
| method | `public void Draw()` | 150 | Uploads every lane's mesh (LateUpdate calls it; public for tests). |

## Uses
[[Art]], [[SparkMesh]], [[SparkParticles]]

## Used by
[[BurstSpec]], [[CatalystChoiceView]], [[FxRefs]], [[GameFlow]], [[GoalToast]], [[GoldenShots]], [[IgniteSequence]], [[ReactionCutscene]], [[SimpleCutscene]], [[SparkParticles]], [[TileView]], [[TitleView]]

## Tested by
[[HotPathTests]], [[SceneBakeTests]], [[SparksTests]]
