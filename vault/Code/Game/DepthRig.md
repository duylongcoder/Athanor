---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthRig.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthRig

`public sealed class DepthRig` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthRig.cs:12-165`

> The camera split of the HD-2D bench. The world camera is a perspective camera at distance D from the art plane; it renders the layers under this object into a RenderTexture of 640K x 360K pixels (K = whole display scale). The UI camera is orthographic (with the PixelPerfectCamera) and draws that texture on a full-screen quad under the HUD. Sway, shake and the world's post effects act on the world camera and its texture only; the HUD never moves.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int UiLayer = 5` | 15 | GameObject layer of everything the UI camera draws (Unity's built-in "UI" layer); the world camera draws the rest. |
| field | `public const int WorldLayer = 6` | 19 | GameObject layer of the bench in the main scene: the world camera draws this layer only, the UI camera every other one, so anything created later (a card, a float text, a spark) is UI unless it is made a child of the world. The depth test scene keeps its own convention (the world on layer 0, the HUD on UiLayer). |
| field | `public float distance = DepthMath.RestDistance` | 23 | D: the world camera's distance to the art plane (z = 0) at rest. |
| field | `public Camera worldCamera` | 27 | K: the world is rendered at 640K x 360K. A whole number, at least 2 (owner gate G2). The perspective camera that renders the layers into WorldTexture. |
| field | `public Camera uiCamera` | 29 | The orthographic camera that shows the world texture and the HUD. |
| field | `public MeshRenderer worldDisplay` | 31 | The full-screen quad (640 x 360 units, on the UI layer) that shows the world texture. |
| field | `public bool autoScale = true` | 33 | When playing, K follows the screen: the largest whole multiple of 640x360 that fits, at least 2. |
| field | `public float near = 50f, far = 2000f` | 35 | The world camera's clip planes (distance from the camera). |
| property | `public bool Flat` | 45 | Flat mode, for the cutscenes: the world camera and the world picture are off and the UI camera draws the whole frame at z = 0 with the bloom and vignette (post processing on it). No sway, no perspective, no depth effect. |
| method | `public static int ScreenScale()` | 59 | The whole multiple of 640x360 that fits the screen (at least 1); K = max(2, this). |
| property | `public RenderTexture WorldTexture` | 62 | What the world camera renders into: 640K x 360K, point filtered, created here and never saved. |
| property | `public Vector3 RestPosition` | 65 | The world camera's position at rest. Sway and shake offset the camera from here. |
| property | `public Vector2 CamOffset` | 68 | The world camera's travel from rest (art px at z = 0, world y up): the sway. |
| property | `public Vector2 ImageShift` | 70 | The shift of the whole world picture (art px, world y up): the anchor correction of the sway plus the shake. |
| property | `public float AnchorZ` | 72 | SPEC depth of the anchor row (art y 188), which the sway never moves. |
| method | `public void SetView(Vector2 cam, Vector2 shake)` | 78 | Moves the world camera by cam (the sway) and shifts the world picture by shake whole art px; the anchor row stays still. The HUD camera is not touched. |
| method | `public static Vector2 ViewportOf(Vector2 ui)` | 86 | Viewport point (0..1, y up) of the world texture under a point of the UI camera's world (640x360 units, origin at the centre). |
| method | `public Ray WorldRay(Vector2 ui)` | 89 | The world ray under a point of the UI camera's world, through the world texture and the world camera. |
| method | `public bool BenchPoint(Vector2 ui, out Vector2 flat)` | 92 | The flat (rest) position on the table under a point of the UI camera's world: what the bench's hit tests take. |
| method | `public void Apply()` | 123 | Sets up the cameras and the world texture, and places every DepthLayer and DepthTable below this object. |

## Uses
[[DepthLayer]], [[DepthMath]], [[DepthTable]], [[Px]]

## Used by
[[CameraSway]], [[DepthStage]], [[DepthTestControls]], [[PointerInput]], [[ScreenFx]], [[ViewCapture]]

## Tested by
[[DepthClickTests]], [[MainDepthTests]], [[StaticResetTests]]
