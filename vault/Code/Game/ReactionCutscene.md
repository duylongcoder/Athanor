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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs:15-320`

> The full discovery sequence: two materials spiral into the cauldron, the rune circle lights, a flash, and the new material rises on a column of light with its name and potency. Everything is a function of the clock T, so skipping is just moving the clock forward.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public struct Opening` | 25 | Before T = 0: the screen fades out of the bench and the room fades in. |
| property | `public struct Gathering` | 29 | The gathering: the materials spiral in until end; the rune and the brew build up over ramp. |
| property | `public struct Bursting` | 33 | The burst at the end of the gathering: flash, shake, the sparks (their colour is multiplied by the result's glow) and the implosion sprite's sort order. |
| property | `public struct Revealing` | 37 | The result rises from riseAt (over rise seconds, fading in at resultFade a second); the words show from wordsAt, fading over wordsFade. |
| property | `public struct Tallying` | 41 | The points count up from at, over seconds. |
| property | `public struct Closing` | 45 | From settled the scene waits for a click; autoEnd ends it alone. Then the room fades out and the bench fades back. |
| method | `public void Bind(GameObject[] others, FxRefs fx)` | 139 | others: everything that is switched off while the cutscene room is on screen (workshop, bench). Hides the stage. |
| method | `public void Setup(ReactionEvent e, ContentDb content, float hurry = 1)` | 154 | hurry above 1 shortens the gathering, for the second and later discoveries of one Ignite. |
| method | `public override IEnumerator Play()` | 180 |  |

## Uses
[[Anim]], [[Art]], [[BurstSpec]], [[ContentDb]], [[Cutscene]], [[Fx]], [[FxRefs]], [[HudView]], [[Label]], [[Loc]], [[Palette]], [[Px]], [[ReactionEvent]], [[Sparks]], [[Spawn]]

## Used by
[[GameManager]], [[IgniteSequence]]

## Tested by
[[LayoutPinTests]], [[SceneBakeTests]]
