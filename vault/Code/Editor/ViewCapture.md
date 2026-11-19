---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/ViewCapture.cs
tags: [code/editor, kind/class]
aliases: []
---

# ViewCapture

`public static class ViewCapture`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/ViewCapture.cs:7-75`

> Renders a GameObject alone (in an otherwise empty scene) to a 640x360 image, the way the UI camera draws it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Texture2D Render(GameObject root, params string[] keepHidden)` | 14 | Draws root to a 640x360 texture. Root and every hidden object under it are made active for the shot, then restored: a view that hides a part until it is needed (the goal toast's panel) is still compared whole. Objects named in keepHidden stay hidden (a view with several dialogs that never show together). |
| method | `public static void Save(Texture2D tex, string path)` | 51 | Writes tex as a PNG at path, creating the folder. |
| method | `public static Texture2D Load(string path)` | 58 | Reads a PNG written by Save back into a texture. |
| method | `public static float Differ(Texture2D a, Texture2D b)` | 66 | Percent of pixels whose RGBA differs at all. |

## Uses
[[DepthRig]], [[Px]], [[Save]]
