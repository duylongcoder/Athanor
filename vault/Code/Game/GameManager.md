---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Boot/GameManager.cs
tags: [code/game, kind/class]
aliases: []
---

# GameManager

`public sealed class GameManager` : `Singleton<GameManager>`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/GameManager.cs:13-601`

> Owns the run and moves the game between its screens.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public ArtLibrary library` | 16 | The art library (sprites and effect sheets); GameManager publishes it as Art.Lib. |
| field | `public GameObject workshop, cutsceneRoom` | 19 | The colours files share (Assets/Data/Palette.asset); GameManager publishes it as Art.Palette. |
| field | `public Camera mainCamera` | 20 |  |
| field | `public DepthStage depthStage` | 22 | The HD-2D bench rig (null in a scene without it: the flat game). |
| property | `public ContentDb Db` | 24 |  |
| property | `public Journal Journal` | 25 |  |
| property | `public Profile Profile` | 28 | What the player keeps between runs besides the journal: stats, goals done, items won. |
| property | `public RunState Run` | 29 |  |
| property | `public BenchView Bench` | 32 | The bench (Prefabs/Views/Bench.prefab in Main.unity): tiles, hand, rack and the Hud. |
| property | `public FlowState State` | 34 | The screen now up (read-only; Flow moves it). |
| property | `public ScreenFlow Flow` | 36 | The screen state machine GameManager drives. |
| property | `public RunOverView RunOver` | 40 | The run summary (a prefab in Main.unity). |
| field | `public bool Auto` | 42 | Debug: the scripted player plays and cutscenes skip themselves. F9 toggles it in the editor and in development builds. |
| field | `public const string AutoHook =` | 45 | The development-build log line of the F9 toggle, followed by "on" or "off" (tools/web/descents.mjs reads it). |
| method | `public void ToggleAuto()` | 48 | Turns the scripted player on or off (F9) and logs its new state in development builds. |
| method | `public static int NextMood(int mood)` | 57 | The depth mood after in the F6 cycle 1..5. |
| method | `public static string MoodLine(int mood)` | 58 |  |
| method | `public static string EffectsLine(bool on)` | 59 |  |
| property | `public JournalView JournalView` | 127 | The journal screen, for the tutorial to point at parts of a page. |
| method | `protected override void OnAwake()` | 151 |  |
| method | `public void RequestJournal()` | 233 | Opens the Journal as the bench's Journal button would (the tutorial's auto-drive). |
| method | `protected override void OnDestroy()` | 235 |  |
| method | `public void Ignite()` | 483 | Spends a trial and plays out what happened. Ignored unless the bench is waiting for the player. |

## Uses
[[Anim]], [[Art]], [[ArtLibrary]], [[AudioMuteHook]], [[AutoMove]], [[AutoPlayer]], [[Bench]], [[BenchView]], [[CatalystChoiceView]], [[ContentDb]], [[ContentManager]], [[DepthLook]], [[DepthMath]], [[DepthStage]], [[Dev]], [[EventView]], [[FlowState]], [[FxRefs]], [[GoalReward]], [[GoalToast]], [[GoalsView]], [[Haptics]], [[IgniteSequence]], [[Journal]], [[JournalView]], [[Loc]], [[Motion]], [[Palette]], [[PauseMenu]], [[PointerInput]], [[Profile]], [[ProgressManager]], [[Px]], [[ReactionCutscene]], [[RunManager]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[SaveManager]], [[ScreenFlow]], [[ScreenFx]], [[Settings]], [[SimpleCutscene]], [[Singleton]], [[Sparks]], [[TitleView]], [[TutorialGate]], [[TutorialManager]], [[TutorialPlan]], [[TutorialRules]], [[Unlocks]], [[WindowView]]

## Used by
[[TutorialManager]]

## Tested by
[[AutoHookTests]], [[MainDepthTests]], [[MainDevHookTests]], [[ManagerBootTests]], [[SceneBakeTests]]
