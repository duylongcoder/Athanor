---
generated: true
type: code
layer: Editor
kind: class
namespace: Athanor.EditorTools
file: Athanor/Assets/Editor/TutorialStepsMigration.cs
tags: [code/editor, kind/class]
aliases: []
---

# TutorialStepsMigration

`public static class TutorialStepsMigration`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/TutorialStepsMigration.cs:16-128`

> Creates (or updates, by name) the tutorial step objects under the Tutorial object of Main.unity, from the table below. The table reproduces what Drive lit and which clicks reached the game before the steps were scene objects; after the first run the scene is the source and this tool is only needed to restore a step to that state. Menu: Athanor/Tutorial/Create Step Objects. Batch: unity run Athanor -- -executeMethod Athanor.EditorTools.TutorialStepsMigration.RunBatch -quit

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static void Run()` | 79 |  |
| method | `public static void RunBatch()` | 88 | From the command line (Unity closed). Exits 1 on any error. |

## Uses
[[AreaKind]], [[Bench]], [[Journal]], [[JournalCell]], [[Px]], [[ScriptTrial]], [[TutorialArea]], [[TutorialController]], [[TutorialStep]], [[ViewRef]]
