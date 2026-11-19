---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/Shared/ViewCapture.cs
tags: [code/editor, kind/class]
aliases: []
---

# ViewCapture

`public static class ViewCapture`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Shared/ViewCapture.cs:8-98`

> Renders a GameObject alone (in an otherwise empty scene) to a 640x360 image, the way the UI camera draws it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Texture2D Render(GameObject root, params string[] keepHidden)` | 15 | Draws root to a 640x360 texture. Root and every hidden object under it are made active for the shot, then restored: a view that hides a part until it is needed (the goal toast's panel) is still compared whole. Objects named in keepHidden stay hidden (a view with several dialogs that never show together). |
| method | `public static Texture2D RenderAsShown(GameObject root)` | 64 | Renders root exactly as it stands: it is made active, and every object under it that is inactive now stays inactive (the view's own Show / Open decided what is visible). Golden shots use this; Render forces hidden parts on. |
| method | `public static void Save(Texture2D tex, string path)` | 73 | Writes tex as a PNG at path, creating the folder. |
| method | `public static Texture2D Load(string path)` | 80 | Reads a PNG written by Save back into a texture; null when there is no such file. |
| method | `public static float Differ(Texture2D a, Texture2D b)` | 89 | Percent of pixels whose RGBA differs at all. |

## Uses
[[DepthRig]], [[Px]], [[Save]]

## Used by
[[GoldenShots]]
