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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs:13-212`

> The full discovery sequence: two materials spiral into the cauldron, the rune circle lights, a flash, and the new material rises on a column of light with its name and potency. Everything is a function of the clock T, so skipping is just moving the clock forward.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Bind(GameObject[] others, FxRefs fx)` | 43 | others: everything that is switched off while the cutscene room is on screen (workshop, bench). Hides the stage. |
| method | `public void Setup(ReactionEvent e, ContentDb content, float hurry = 1)` | 58 | hurry above 1 shortens the gathering, for the second and later discoveries of one Ignite. |
| method | `public override IEnumerator Play()` | 84 |  |

## Uses
[[Anim]], [[Art]], [[ContentDb]], [[Cutscene]], [[Fx]], [[FxRefs]], [[HudView]], [[Label]], [[Loc]], [[Px]], [[ReactionEvent]], [[Sparks]], [[Spawn]]

## Used by
[[GameFlow]]

## Tested by
[[SceneBakeTests]]
