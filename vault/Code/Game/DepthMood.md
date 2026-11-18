---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthMood.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthMood

`public sealed class DepthMood` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthMood.cs:13-101`

> The five depth presets of the HD-2D test scene (HD-2D mockup SPEC, "Depth presets", with the URP conversions of "Mapping mockup values to Unity"): ambient light, moonbeam, bloom, vignette, the fog sheets and the window view. Depth 0 is today's look (the main scene's single setting).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Preset[] presets = Defaults()` | 36 | Depth 1..5 (index 0..4). |
| field | `public int depth` | 38 | The depth showing: 0 is today's look, 1..5 a preset. |
| field | `public Light2D ambient, moon` | 39 |  |
| field | `public Volume volume` | 40 |  |
| field | `public SpriteRenderer backFog, frontFog` | 41 |  |
| field | `public DepthSwitch effects` | 43 | The effects switch whose sprite swap at windowSwap is the window view. |
| field | `public int windowSwap` | 44 |  |
| field | `public Sprite windowNight` | 46 | The flat window of today's game (depth 0), and the five depth views, sharp and pre-blurred. |
| field | `public WindowView window` | 48 | When set (the main scene), the window view is this one's job, not the sprite swap above. |
| field | `public Sprite[] windowSharp = new Sprite[0], windowSoft = new Sprite[0]` | 49 |  |
| method | `public static Preset[] Defaults()` | 53 | The SPEC's presets. Ambient, moon and accent are the game's arrays (GameFlow); bloom threshold = 1.0 + (bth - 0.80), bloom intensity = 1.1 * bin / 0.55, vignette = 0.34 * vi / 0.30. |
| method | `public void Set(int d)` | 73 | Shows depth d (0: today's look; 1..5: a preset). |
| method | `public void Set(int d, bool themedWindow)` | 76 | Shows depth d; themedWindow false keeps the night-sky window (title, tutorials) while lights, post and fog follow d. |

## Uses
[[DepthSwitch]], [[Px]], [[WindowView]]

## Used by
[[DepthStage]], [[DepthTestControls]]
