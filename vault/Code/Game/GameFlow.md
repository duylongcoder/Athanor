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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs:15-537`

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
| method | `public void Ignite()` | 263 | Spends a trial and plays out what happened. Ignored unless the bench is waiting for the player. |

## Uses
[[Anim]], [[ArtLibrary]], [[AutoMove]], [[AutoPlayer]], [[Bench]], [[BenchView]], [[CatalystChoiceView]], [[ContentData]], [[ContentDb]], [[EventView]], [[FlowState]], [[Fx]], [[GoalReward]], [[GoalToast]], [[GoalsView]], [[HudView]], [[Journal]], [[JournalView]], [[LayoutData]], [[Make]], [[Motion]], [[PointerInput]], [[Profile]], [[Px]], [[ReactionCutscene]], [[ReactionEvent]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[Save]], [[ScreenFx]], [[SimpleCutscene]], [[Sparks]], [[TitleView]], [[Unlocks]]

## Used by
[[SceneBuilder]]
