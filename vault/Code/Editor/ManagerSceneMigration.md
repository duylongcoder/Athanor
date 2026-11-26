---
generated: true
type: code
layer: Editor
kind: class
namespace: Athanor.EditorTools
file: Athanor/Assets/Editor/Migration/ManagerSceneMigration.cs
tags: [code/editor, kind/class]
aliases: []
---

# ManagerSceneMigration

`public static class ManagerSceneMigration`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Migration/ManagerSceneMigration.cs:15-63`

> Creates Main.unity › Managers and its components, and fills their asset fields (database, english, vietnamese) from their asset paths when empty. Idempotent: run it again after a task adds a manager. Menu: Athanor/Migrate/Add Managers to Main.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static void Run()` | 20 |  |
| method | `public static void RunBatch()` | 28 |  |
| method | `public static void Apply(Scene scene)` | 34 |  |

## Uses
[[ContentManager]], [[LocalizationManager]], [[ProgressManager]], [[RunManager]], [[SaveManager]]
