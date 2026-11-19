---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs
tags: [code/game, kind/class]
aliases: []
---

# IgniteSequence

`public sealed class IgniteSequence` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs:15-295`

> The choreography of one Ignite: the chain of reactions (flash, draw-in, burst, points), the discovery beat and cutscene, the flight of what was made to the rack, the spent tiles and the final potency. The Ignite is settled before this plays (GameFlow.SettleIgnite); nothing here decides a rule. All pace, wait and look numbers are fields of Timing and Look, stored in Main.unity.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public bool Hurry` | 96 | True once the player pressed during the Ignite: the remaining links play fast. GameFlow sets it. |
| method | `public void Bind(BenchView bench, ReactionCutscene reaction, FxRefs fx, ScreenFlow flow)` | 99 | Hands the sequence what it plays on. |
| method | `public IEnumerator Play(IgniteOutcome outcome, RunState run, ContentDb db, long potencyBefore, TutorialController tutorial, bool auto)` | 108 | Plays an Ignite that is already settled: the chain, the flights to the rack, the dropped items and the final potency, then the tutorial's lines. potencyBefore is the counter's value before the Ignite. |

## Uses
[[Anim]], [[Bench]], [[BenchView]], [[BurstSpec]], [[ContentDb]], [[Fx]], [[FxRefs]], [[Haptics]], [[HudView]], [[IgniteOutcome]], [[Loc]], [[Motion]], [[Px]], [[ReactionCutscene]], [[ReactionEvent]], [[RunState]], [[ScreenFlow]], [[Sparks]], [[Spawn]], [[TutorialController]]

## Used by
[[GameFlow]]

## Tested by
[[LayoutPinTests]]
