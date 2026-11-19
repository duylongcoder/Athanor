---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialController

`public sealed class TutorialController` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs:33-380`

> Plays the scripted tutorial over the bench: a dark screen with lit holes and a pulsing gold rim, an arrow, a text box at the top, click locks, and popups held until read. It reads the game's state; it decides no rule. The dark screen is one sprite drawn "visible outside mask"; each lit hole is a SpriteMask (the inverse mask), see TutorialHole.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public int Trial` | 55 | The trial being taught: 0 reaction, 1 miss, 2 chain, 3 the catalyst trial of depth 2. |
| property | `public bool Finished` | 57 | True from the first line to the last (the real journal is never touched in a replay; see GameFlow). |
| field | `public readonly List<long> Shown = new List<long>()` | 59 | The scores the popups showed, in order (checked by the tests against TutorialTests: 40 / 0 / 40 / 240 / ...). |
| method | `public void Bind(GameFlow flow)` | 62 | Gives the controller the game it reads; the lessons run in Drive. |
| method | `public static Rect Union(Rect[] r)` | 74 | The box round the holes. No holes (a text-only step such as 2.3) is the whole canvas: a full dim, no rim, no arrow. |
| method | `public void Abort()` | 146 | Quit to title in the tutorial: stops the lessons and takes the overlay down. Does not mark the tutorial done. |
| method | `public IEnumerator AfterEvent(ReactionEvent e, int index)` | 236 | After a reaction (or fizzle) has played: the held popups of the script. |
| method | `public IEnumerator AfterIgnite(IgniteOutcomeView o)` | 276 | After the Ignite has played and its materials are spent: the next scripted hand, and the lines that follow. |
| method | `public void AfterChoose(string catalystId, string name)` | 301 | The catalyst was taken: deal the scripted hand that shows it. |
| method | `public IEnumerator OnOffer()` | 311 | The lines of the catalyst offer; the choice itself is the game's own screen. |
| method | `public void OfferTaken()` | 318 |  |
| method | `public IEnumerator Drive()` | 329 | Runs the lessons from the first bench to line 5.5. The game plays its own Ignite, descents and choice between. |
| property | `public sealed class IgniteOutcomeView` | 379 | The stage the AfterIgnite hook needs from the game: what the Ignite made. |

## Uses
[[Align]], [[Bench]], [[Clickable]], [[Dev]], [[FlowState]], [[FontKind]], [[GameFlow]], [[HudView]], [[Journal]], [[JournalView]], [[Label]], [[Loc]], [[Px]], [[ReactionEvent]], [[RunPhase]], [[ScoreParts]], [[Spawn]], [[TutorialHole]], [[TutorialRules]], [[TutorialScript]], [[TutorialStyle]], [[TutorialTargets]], [[TutorialTrial]]

## Used by
[[GameFlow]]

## Tested by
[[PauseTests]], [[SceneBakeTests]], [[TutorialFlowTests]]
