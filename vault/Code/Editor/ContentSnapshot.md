---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/Shared/ContentSnapshot.cs
tags: [code/editor, kind/class]
aliases: []
---

# ContentSnapshot

`public static class ContentSnapshot`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Shared/ContentSnapshot.cs:9-31`

> Writes the content assets as tools/content/snapshot.json (the shape the content assets had) for the tools that are not Unity: tools/ci/check_repo.py, tools/vault/build_vault.py, tools/art/export.mjs. The game never reads it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public static string FilePath` | 12 | Absolute path of tools/content/snapshot.json. |
| method | `public static string Text()` | 15 | The snapshot text for the current assets. |
| method | `public static void Write()` | 18 |  |
| method | `public static void RunBatch()` | 26 | Batch entry: unity run Athanor -- -executeMethod ContentSnapshot.RunBatch. |

## Uses
[[ContentAssets]]

## Tested by
[[ContentSnapshotTests]]
