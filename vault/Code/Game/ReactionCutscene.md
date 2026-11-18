---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs
tags: [code/game, kind/class]
aliases: []
---

# ReactionCutscene

`public sealed class ReactionCutscene` : `Cutscene`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs:13-260`

> The full discovery sequence: two materials spiral into the cauldron, the rune circle lights, a flash, and the new material rises on a column of light with its name and potency. Everything is a function of the clock T, so skipping is just moving the clock forward.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Bind(GameObject[] others)` | 41 | others: everything that is switched off while the cutscene room is on screen (workshop, bench). Hides the stage. |
| method | `public void Build(GameObject cutsceneRoom)` | 52 | Bake source for the Reaction Cutscene object of Main.unity (ViewBaker.BakeReaction): the stage and its UI under the cutscene room, both hidden. The room and the objects it hides are left as they are; Bind hides the stage and the room at run time. |
| method | `public void Setup(ReactionEvent e, ContentDb content, float hurry = 1)` | 106 | hurry above 1 shortens the gathering, for the second and later discoveries of one Ignite. |
| method | `public override IEnumerator Play()` | 132 |  |

## Uses
[[Align]], [[Anim]], [[Art]], [[ContentDb]], [[Cutscene]], [[FontKind]], [[Fx]], [[HudView]], [[Label]], [[Loc]], [[Make]], [[Px]], [[ReactionEvent]], [[ScreenFx]], [[Sparks]]

## Used by
[[GameFlow]], [[ViewBaker]]

## Tested by
[[SceneBakeTests]]
