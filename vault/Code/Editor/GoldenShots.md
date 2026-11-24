---
generated: true
type: code
layer: Editor
kind: class
namespace: Athanor.EditorShared
file: Athanor/Assets/Editor/Shared/GoldenShots.cs
tags: [code/editor, kind/class]
aliases: []
---

# GoldenShots

`public static class GoldenShots`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Shared/GoldenShots.cs:19-299`

> The golden shots: one picture of every view in a fixed, seeded state, in English and Vietnamese. Each view is built the way its prefab test builds it (instantiate, Bind, Show / Open), never through Update, so a shot holds no Time.time, no Random and no running animation: tweens are completed (Tween.CompleteAll) rather than waited for. Phase 7 compares every task against these.

## Uses
[[AspectEffects]], [[AspectInstance]], [[BenchView]], [[CatalystChoiceView]], [[ChoiceCard]], [[Clickable]], [[ContentAssets]], [[ContentDb]], [[EditorGlobals]], [[EventView]], [[Fx]], [[FxRefs]], [[GoalReward]], [[GoalToast]], [[GoalsView]], [[HudView]], [[Journal]], [[JournalView]], [[Kind]], [[Loc]], [[PauseMenu]], [[Profile]], [[Rarities]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[Save]], [[ScreenFx]], [[Settings]], [[Sparks]], [[TitleView]], [[TutorialScript]], [[Unlocks]], [[ViewCapture]]
