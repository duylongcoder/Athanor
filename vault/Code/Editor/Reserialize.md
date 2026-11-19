---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/Shared/Reserialize.cs
tags: [code/editor, kind/class]
aliases: []
---

# Reserialize

`public static class Reserialize`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Shared/Reserialize.cs:11-27`

> Rewrites every view prefab and the data assets in the current serializer format, so a field added to a component is stored with its value. Phase 7 uses it after a number moves from code to a prefab. Main.unity is left out on purpose: on the untouched project a reserialize rewrote a camera viewport rect (a value change) and crashed the Editor, so scene-owned values are set by hand or by a dedicated editor script and checked with a diff.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static void All()` | 14 |  |
