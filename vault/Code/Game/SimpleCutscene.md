---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs
tags: [code/game, kind/class]
aliases: []
---

# SimpleCutscene

`public sealed class SimpleCutscene` : `Cutscene`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs:14-351`

> The short story beats built from text and a few effects: the intro, the descent between depths, and the ending. A click finishes the line being written, then moves on.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Build(GameObject cutsceneRoom, GameObject workshopRoot, GameObject benchRoot)` | 30 |  |
| method | `public override IEnumerator Play()` | 59 |  |
| method | `public void Close()` | 76 | Puts the workshop back and hides everything this cutscene drew. |
| method | `public IEnumerator Intro()` | 110 | Starts and ends on a black screen. |
| method | `public IEnumerator Descent(int depth, string depthName, Color colour, Action midway)` | 146 | "Depth cleared" over the bench, the room slides away upward, the next depth is named, and the room rises back. midway runs while nothing is on screen (the flow uses it to change the lighting). Leaves the bench hidden. |
| method | `public IEnumerator Ending(MaterialData ultimate)` | 197 | The ultimate substance over the cauldron. Stays on screen afterwards (the summary goes on top) until Close. |

## Uses
[[Align]], [[Anim]], [[Cutscene]], [[FontKind]], [[Label]], [[Make]], [[Motion]], [[Px]], [[ScreenFx]], [[SparkMesh]], [[Sparks]]

## Used by
[[GameFlow]]
