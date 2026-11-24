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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs:13-644`

> Owns the run and moves the game between its screens.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public ArtLibrary library` | 16 | The art library (sprites and effect sheets); GameFlow publishes it as Art.Lib. |
| field | `public GameObject workshop, cutsceneRoom` | 23 | The colours files share (Assets/Data/Palette.asset); GameFlow publishes it as Art.Palette. The two language files, set in the scene. Awake throws when one is empty. The game's content (Assets/Content/ContentDatabase.asset). |
| field | `public Camera mainCamera` | 24 |  |
| field | `public DepthStage depthStage` | 26 | The HD-2D bench rig (null in a scene without it: the flat game). |
| property | `public ContentDb Db` | 28 |  |
| property | `public Journal Journal` | 29 |  |
| property | `public Profile Profile` | 31 | What the player keeps between runs besides the journal: stats, goals done, items won. |
| property | `public RunState Run` | 32 |  |
| property | `public BenchView Bench` | 34 | The bench (Prefabs/Views/Bench.prefab in Main.unity): tiles, hand, rack and the Hud. |
| property | `public FlowState State` | 36 | The screen now up (read-only; Flow moves it). |
| property | `public ScreenFlow Flow` | 38 | The screen state machine GameFlow drives. |
| property | `public RunOverView RunOver` | 42 | The run summary (a prefab in Main.unity). |
| field | `public bool Auto` | 44 | Debug: the scripted player plays and cutscenes skip themselves. F9 toggles it in the editor and in development builds. |
| property | `public int SeedOverride` | 48 | 0: each run gets a fresh seed. Any other value: every run uses it (PlayMode traces, bug reproduction). |
| field | `public const string AutoHook =` | 51 | The development-build log line of the F9 toggle, followed by "on" or "off" (tools/web/descents.mjs reads it). |
| method | `public void ToggleAuto()` | 54 | Turns the scripted player on or off (F9) and logs its new state in development builds. |
| method | `public static int NextMood(int mood)` | 63 | The depth mood after in the F6 cycle 1..5. |
| method | `public static string MoodLine(int mood)` | 64 |  |
| method | `public static string EffectsLine(bool on)` | 65 |  |
| property | `public JournalView JournalView` | 135 | The journal screen, for the tutorial to point at parts of a page. |
| method | `public void RequestJournal()` | 254 | Opens the Journal as the bench's Journal button would (the tutorial's auto-drive). |
| method | `public static Journal JournalAfterQuit(bool sandbox, Journal current)` | 478 | The journal after a quit: what is stored, as if the page had been closed (a How-to-play sandbox keeps its throwaway). |
| method | `public static Profile ProfileAfterQuit(bool sandbox, Profile current, ContentDb db)` | 480 | The profile after a quit: what is stored. A run counts only at the Ignite that ends it, so nothing is added. |
| method | `public void Ignite()` | 517 | Spends a trial and plays out what happened. Ignored unless the bench is waiting for the player. |
| method | `public static IgniteOutcome SettleIgnite(RunState run, Journal journal, bool sandbox)` | 524 | Ignites and stores the journal at once, so a discovery is in storage before anything else can happen (Quit, closed page). A sandbox stores nothing. |

## Uses
[[Anim]], [[Art]], [[ArtLibrary]], [[AudioMuteHook]], [[AutoMove]], [[AutoPlayer]], [[Bench]], [[BenchView]], [[CatalystChoiceView]], [[ContentDatabase]], [[ContentDb]], [[DepthLook]], [[DepthMath]], [[DepthStage]], [[Dev]], [[EventView]], [[FlowState]], [[FxRefs]], [[GoalReward]], [[GoalToast]], [[GoalsView]], [[Haptics]], [[IgniteOutcome]], [[IgniteSequence]], [[Journal]], [[JournalView]], [[Loc]], [[Motion]], [[Palette]], [[PauseMenu]], [[PointerInput]], [[Profile]], [[Px]], [[ReactionCutscene]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[Save]], [[ScreenFlow]], [[ScreenFx]], [[Settings]], [[SimpleCutscene]], [[Sparks]], [[TitleView]], [[Tooltip]], [[TutorialController]], [[TutorialPlan]], [[TutorialRules]], [[TutorialScript]], [[Unlocks]], [[WindowView]]

## Used by
[[ContentFieldMigration]], [[TutorialController]]

## Tested by
[[AutoHookTests]], [[MainDepthTests]], [[MainDevHookTests]], [[PauseTests]], [[SceneBakeTests]]
