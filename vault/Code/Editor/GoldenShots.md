---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/Shared/GoldenShots.cs
tags: [code/editor, kind/class]
aliases: []
---

# GoldenShots

`public static class GoldenShots`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Shared/GoldenShots.cs:17-280`

> The golden shots: one picture of every view in a fixed, seeded state, in English and Vietnamese. Each view is built the way its prefab test builds it (instantiate, Bind, Show / Open), never through Update, so a shot holds no Time.time, no Random and no running animation: tweens are completed (Tween.CompleteAll) rather than waited for. Phase 7 compares every task against these.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static IEnumerable<string> Names()` | 27 | The shot names, in the order they are written. |
| method | `public static string PathOf(string name, string lang)` | 30 | /production/qa/evidence/phase7/baseline/golden/<name>.<lang>.png |
| method | `public static void WriteAll()` | 37 |  |
| method | `public static Texture2D Render(string name)` | 53 | Builds the view of a shot in the current language, draws it to 640x360 and removes it again. |

## Uses
[[AspectEffects]], [[AspectInstance]], [[BenchView]], [[CatalystChoiceView]], [[ChoiceCard]], [[Clickable]], [[ContentData]], [[ContentDb]], [[EditorGlobals]], [[EventView]], [[GoalReward]], [[GoalToast]], [[GoalsView]], [[HudView]], [[Journal]], [[JournalView]], [[Kind]], [[Loc]], [[PauseMenu]], [[Profile]], [[Rarities]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[Save]], [[Settings]], [[TitleView]], [[TutorialScript]], [[Unlocks]], [[ViewCapture]]
