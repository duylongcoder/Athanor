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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs:14-367`

> The short story beats built from text and a few effects: the intro, the descent between depths, and the ending. A click finishes the line being written, then moves on.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Build(GameObject cutsceneRoom, GameObject workshopRoot, GameObject benchRoot)` | 38 | Bake source for the Story Cutscene object of Main.unity (ViewBaker.BakeStory): the story stage, hidden, under this object. The three scene objects are only remembered; the baked scene keeps them as references. |
| method | `public override IEnumerator Play()` | 71 |  |
| method | `public void Close()` | 88 | Puts the workshop back and hides everything this cutscene drew. |
| method | `public IEnumerator Intro()` | 107 | Starts and ends on a black screen. |
| method | `public IEnumerator Descent(int depth, string depthName, Color colour, Action midway)` | 143 | "Depth cleared" over the bench, the room slides away upward, the next depth is named, and the room rises back. midway runs while nothing is on screen (the flow uses it to change the lighting). Leaves the bench hidden. |
| method | `public IEnumerator DepthIntro(string key)` | 202 | The depth's line over the bench at the start of a run (depth 1 has no descent): fades in, holds, fades out, about 2.2 s at most; a click shortens the hold. Leaves the bench as it was; the caller keeps its input off meanwhile. |
| method | `public IEnumerator Ending(MaterialData ultimate)` | 218 | The ultimate substance over the cauldron. Stays on screen afterwards (the summary goes on top) until Close. |

## Uses
[[Align]], [[Anim]], [[Art]], [[Cutscene]], [[DepthLook]], [[FontKind]], [[Label]], [[Loc]], [[Make]], [[Motion]], [[Px]], [[ScreenFx]], [[SparkMesh]], [[Sparks]]

## Used by
[[GameFlow]], [[ViewBaker]]

## Tested by
[[SceneBakeTests]]
