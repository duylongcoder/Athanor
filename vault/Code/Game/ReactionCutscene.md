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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs:13-246`

> The full discovery sequence: two materials spiral into the cauldron, the rune circle lights, a flash, and the new material rises on a column of light with its name and potency. Everything is a function of the clock T, so skipping is just moving the clock forward.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Build(GameObject cutsceneRoom, params GameObject[] others)` | 39 | others: everything that is switched off while the cutscene room is on screen (workshop, bench). |
| method | `public void Setup(ReactionEvent e, ContentDb content, float hurry = 1)` | 92 | hurry above 1 shortens the gathering, for the second and later discoveries of one Ignite. |
| method | `public override IEnumerator Play()` | 118 |  |

## Uses
[[Align]], [[Anim]], [[ContentDb]], [[Cutscene]], [[FontKind]], [[Fx]], [[HudView]], [[Label]], [[Make]], [[Px]], [[ReactionEvent]], [[ScreenFx]], [[Sparks]]

## Used by
[[GameFlow]]
