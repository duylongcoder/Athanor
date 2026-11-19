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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs:14-340`

> The short story beats built from text and a few effects: the intro, the descent between depths, and the ending. A click finishes the line being written, then moves on.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Bind(FxRefs effects)` | 37 | Takes the effect players. GameFlow calls it once at start. |
| method | `public override IEnumerator Play()` | 44 |  |
| method | `public void Close()` | 61 | Puts the workshop back and hides everything this cutscene drew. |
| method | `public IEnumerator Intro()` | 80 | Starts and ends on a black screen. |
| method | `public IEnumerator Descent(int depth, string depthName, Color colour, Action midway)` | 116 | "Depth cleared" over the bench, the room slides away upward, the next depth is named, and the room rises back. midway runs while nothing is on screen (the flow uses it to change the lighting). Leaves the bench hidden. |
| method | `public IEnumerator DepthIntro(string key)` | 175 | The depth's line over the bench at the start of a run (depth 1 has no descent): fades in, holds, fades out, about 2.2 s at most; a click shortens the hold. Leaves the bench as it was; the caller keeps its input off meanwhile. |
| method | `public IEnumerator Ending(MaterialData ultimate)` | 191 | The ultimate substance over the cauldron. Stays on screen afterwards (the summary goes on top) until Close. |

## Uses
[[Anim]], [[Art]], [[Cutscene]], [[DepthLook]], [[FxRefs]], [[Label]], [[Loc]], [[Motion]], [[Px]], [[SparkMesh]], [[Sparks]]

## Used by
[[GameFlow]]

## Tested by
[[SceneBakeTests]]
