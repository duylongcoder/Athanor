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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs:13-797`

> Owns the run and moves the game between its screens.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public ArtLibrary library` | 16 | The art library (sprites and effect sheets); GameFlow publishes it as Art.Lib. |
| field | `public GameObject workshop, cutsceneRoom` | 19 | content.json and the two language files, set in the scene. Awake throws when one is empty. |
| field | `public Camera mainCamera` | 20 |  |
| field | `public DepthStage depthStage` | 22 | The HD-2D bench rig (null in a scene without it: the flat game). |
| property | `public ContentDb Db` | 24 |  |
| property | `public Journal Journal` | 25 |  |
| property | `public Profile Profile` | 27 | What the player keeps between runs besides the journal: stats, goals done, items won. |
| property | `public RunState Run` | 28 |  |
| property | `public BenchView Bench` | 30 | The bench (Prefabs/Views/Bench.prefab in Main.unity): tiles, hand, rack and the Hud. |
| property | `public FlowState State` | 32 | The screen now up (read-only; Flow moves it). |
| property | `public ScreenFlow Flow` | 34 | The screen state machine GameFlow drives. |
| property | `public RunOverView RunOver` | 38 | The run summary (a prefab in Main.unity). |
| field | `public bool Auto` | 40 | Debug: the scripted player plays and cutscenes skip themselves. F9 toggles it in the editor and in development builds. |
| field | `public const string AutoHook =` | 43 | The development-build log line of the F9 toggle, followed by "on" or "off" (tools/web/descents.mjs reads it). |
| method | `public void ToggleAuto()` | 46 | Turns the scripted player on or off (F9) and logs its new state in development builds. |
| method | `public static int NextMood(int mood)` | 55 | The depth mood after in the F6 cycle 1..5. |
| method | `public static string MoodLine(int mood)` | 56 |  |
| method | `public static string EffectsLine(bool on)` | 57 |  |
| property | `public JournalView JournalView` | 119 | The journal screen, for the tutorial to point at parts of a page. |
| method | `public static Journal JournalAfterQuit(bool sandbox, Journal current)` | 457 | The journal after a quit: what is stored, as if the page had been closed (a How-to-play sandbox keeps its throwaway). |
| method | `public static Profile ProfileAfterQuit(bool sandbox, Profile current, ContentDb db)` | 459 | The profile after a quit: what is stored. A run counts only at the Ignite that ends it, so nothing is added. |
| method | `public void Ignite()` | 499 | Spends a trial and plays out what happened. Ignored unless the bench is waiting for the player. |
| method | `public static IgniteOutcome SettleIgnite(RunState run, Journal journal, bool sandbox)` | 506 | Ignites and stores the journal at once, so a discovery is in storage before anything else can happen (Quit, closed page). A sandbox stores nothing. |

## Uses
[[Anim]], [[Art]], [[ArtLibrary]], [[AudioMuteHook]], [[AutoMove]], [[AutoPlayer]], [[Bench]], [[BenchView]], [[CatalystChoiceView]], [[ContentData]], [[ContentDb]], [[DepthLook]], [[DepthMath]], [[DepthStage]], [[Dev]], [[EventView]], [[FlowState]], [[Fx]], [[FxRefs]], [[GoalReward]], [[GoalToast]], [[GoalsView]], [[Haptics]], [[HudView]], [[IgniteOutcome]], [[Journal]], [[JournalView]], [[Loc]], [[Motion]], [[PauseMenu]], [[PointerInput]], [[Profile]], [[Px]], [[ReactionCutscene]], [[ReactionEvent]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[Save]], [[ScreenFlow]], [[ScreenFx]], [[Settings]], [[SimpleCutscene]], [[Sparks]], [[Spawn]], [[TitleView]], [[TutorialController]], [[TutorialPlan]], [[TutorialRules]], [[TutorialScript]], [[Unlocks]], [[WindowView]]

## Used by
[[TutorialController]]

## Tested by
[[AutoHookTests]], [[MainDepthTests]], [[MainDevHookTests]], [[PauseTests]], [[SceneBakeTests]]
