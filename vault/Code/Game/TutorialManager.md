---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Tutorial/TutorialManager.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialManager

`public sealed class TutorialManager` : `Singleton<TutorialManager>`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Tutorial/TutorialManager.cs:34-459`

> Plays the scripted tutorial over the bench: a dark screen with lit holes and a pulsing gold rim, an arrow, a text box at the top, click locks, and popups held until read. It reads the game's state; it decides no rule. The dark screen is one sprite drawn "visible outside mask"; each lit hole is a SpriteMask (the inverse mask), see TutorialHole.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public TutorialSequence Sequence` | 49 | The tutorial as data (Assets/Content/Tutorial/Tutorial.asset). The tutorial as data. |
| property | `public int Trial` | 59 | The trial being taught: 0 reaction, 1 miss, 2 chain, 3 the catalyst trial of depth 2. |
| property | `public bool Finished` | 61 | True from the first line to the last (the real journal is never touched in a replay; see GameManager). |
| field | `public readonly List<long> Shown = new List<long>()` | 63 | The scores the popups showed, in order (checked by the tests against TutorialTests: 40 / 0 / 40 / 240 / ...). |
| field | `public bool AutoDrive` | 66 | Tests and debugging only: read lines advance by themselves and the scripted lays, Ignites and Journal moves are made for the player. |
| method | `public void Bind(GameManager flow)` | 69 | Gives the controller the game it reads; the lessons run in Drive. |
| method | `protected override void OnAwake()` | 71 |  |
| method | `public static Rect Union(Rect[] r)` | 82 | The box round the holes. No holes (a text-only step such as 2.3) is the whole canvas: a full dim, no rim, no arrow. |
| method | `public void Abort()` | 162 | Quit to title in the tutorial: stops the lessons and takes the overlay down. Does not mark the tutorial done. |
| method | `public TutorialStepDef Step(string id)` | 169 | The step with this id in the sequence. |
| method | `public Rect[] Holes(TutorialStepDef step, int hub = -1)` | 309 | The art-pixel rects a step lights right now, before the hole snap. For the tests and the Scene-view preview. |
| method | `public IEnumerator Drive()` | 321 | Runs the sequence's script from the first bench to its last line. The game plays its own Ignite, descents and choice between. |
| method | `public IEnumerator AfterEvent(ReactionEvent e, int index)` | 385 | After a reaction (or fizzle) has played: the held popups of the current trial. |
| method | `public IEnumerator AfterIgnite(IgniteOutcomeView o)` | 403 | After the Ignite has played and its materials are spent: the trial's lines, then the next scripted hand. |
| method | `public void AfterChoose(string catalystId, string name)` | 418 | The catalyst was taken: deal the scripted hand that shows it. |
| method | `public IEnumerator OnOffer()` | 427 | The lines of the catalyst offer; the choice itself is the game's own screen. |
| method | `public void OfferTaken()` | 448 |  |
| property | `public sealed class IgniteOutcomeView` | 458 | The stage the AfterIgnite hook needs from the game: what the Ignite made. |

## Uses
[[Align]], [[AreaKind]], [[Bench]], [[CatalystTrialDef]], [[Clickable]], [[Dev]], [[FlowState]], [[FontKind]], [[GameManager]], [[HudView]], [[Journal]], [[JournalCell]], [[JournalView]], [[Label]], [[Loc]], [[Px]], [[ReactionEvent]], [[RunPhase]], [[ScoreParts]], [[ScriptTrial]], [[Singleton]], [[Spawn]], [[Tooltip]], [[TutorialAction]], [[TutorialActionKind]], [[TutorialAreaDef]], [[TutorialHole]], [[TutorialRect]], [[TutorialRules]], [[TutorialSequence]], [[TutorialStepDef]], [[TutorialStyle]], [[TutorialTrial]], [[TutorialTrialDef]], [[ViewRef]]

## Used by
[[GameManager]], [[IgniteSequence]]

## Tested by
[[LayoutPinTests]], [[PauseTests]], [[SceneBakeTests]], [[TutorialFlowTests]], [[TutorialStepsBakeTests]]
