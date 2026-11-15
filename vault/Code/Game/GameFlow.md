---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Boot/GameFlow.cs
tags: [code/game, kind/class]
aliases: []
---

# GameFlow

`public sealed class GameFlow` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs:15-707`

> Owns the run and moves the game between its screens.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public ArtLibrary library` | 17 |  |
| field | `public GameObject workshop, cutsceneRoom` | 18 |  |
| field | `public Camera mainCamera` | 19 |  |
| property | `public static GameFlow I` | 21 |  |
| property | `public ContentDb Db` | 22 |  |
| property | `public Journal Journal` | 23 |  |
| property | `public Profile Profile` | 25 | What the player keeps between runs besides the journal: stats, goals done, items won. |
| property | `public RunState Run` | 26 |  |
| property | `public BenchView Bench` | 27 |  |
| property | `public FlowState State` | 28 |  |
| property | `public RunOverView RunOver` | 29 |  |
| field | `public bool Auto` | 31 | Debug: the scripted player plays and cutscenes skip themselves. F9 toggles it in the editor and in development builds. |
| field | `public const string AutoHook =` | 34 | The development-build log line of the F9 toggle, followed by "on" or "off" (tools/web/descents.mjs reads it). |
| method | `public void ToggleAuto()` | 37 | Turns the scripted player on or off (F9) and logs its new state in development builds. |
| property | `public JournalView JournalView` | 61 | The journal screen, for the tutorial to point at parts of a page. |
| method | `public static Journal JournalAfterQuit(bool sandbox, Journal current)` | 372 | The journal after a quit: what is stored, as if the page had been closed (a How-to-play sandbox keeps its throwaway). |
| method | `public static Profile ProfileAfterQuit(bool sandbox, Profile current, ContentDb db)` | 374 | The profile after a quit: what is stored. A run counts only at the Ignite that ends it, so nothing is added. |
| method | `public void Ignite()` | 414 | Spends a trial and plays out what happened. Ignored unless the bench is waiting for the player. |
| method | `public static IgniteOutcome SettleIgnite(RunState run, Journal journal, bool sandbox)` | 422 | Ignites and stores the journal at once, so a discovery is in storage before anything else can happen (Quit, closed page). A sandbox stores nothing. |

## Uses
[[Anim]], [[ArtLibrary]], [[AudioMuteHook]], [[AutoMove]], [[AutoPlayer]], [[Bench]], [[BenchView]], [[CatalystChoiceView]], [[ContentData]], [[ContentDb]], [[DepthLook]], [[Dev]], [[EventView]], [[FlowState]], [[Fx]], [[GoalReward]], [[GoalToast]], [[GoalsView]], [[Haptics]], [[HudView]], [[IgniteOutcome]], [[Journal]], [[JournalView]], [[LayoutData]], [[Loc]], [[Make]], [[Motion]], [[PauseMenu]], [[PointerInput]], [[Profile]], [[Px]], [[ReactionCutscene]], [[ReactionEvent]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[Save]], [[ScreenFx]], [[Settings]], [[SimpleCutscene]], [[Sparks]], [[TitleView]], [[TutorialController]], [[TutorialPlan]], [[TutorialRules]], [[TutorialScript]], [[Unlocks]], [[WindowView]]

## Used by
[[SceneBuilder]], [[TutorialController]]

## Tested by
[[AutoHookTests]], [[PauseTests]]
