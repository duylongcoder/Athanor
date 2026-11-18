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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs:31-406`

> Plays the scripted tutorial over the bench: a dark screen with lit holes and a pulsing gold rim, an arrow, a text box at the top, click locks, and popups held until read. It reads the game's state; it decides no rule.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public int Trial` | 50 | The trial being taught: 0 reaction, 1 miss, 2 chain, 3 the catalyst trial of depth 2. |
| property | `public bool Finished` | 52 | True from the first line to the last (the real journal is never touched in a replay; see GameFlow). |
| field | `public readonly List<long> Shown = new List<long>()` | 54 | The scores the popups showed, in order (checked by the tests against TutorialTests: 40 / 0 / 40 / 240 / ...). |
| method | `public void Bind(GameFlow flow)` | 57 | Gives the controller the game it reads; the lessons run in Drive. |
| method | `public void Build()` | 61 | Bake source for the Tutorial object of Main.unity (ViewBaker.BakeTutorial): the text box, the arrow and the (empty) cell root. |
| method | `public static Rect Union(Rect[] r)` | 77 | The box round the holes. No holes (a text-only step such as 2.3) is the whole canvas: a full dim, no rim, no arrow. |
| method | `public void Abort()` | 177 | Quit to title in the tutorial: stops the lessons and takes the overlay down. Does not mark the tutorial done. |
| method | `public IEnumerator AfterEvent(ReactionEvent e, int index)` | 261 | After a reaction (or fizzle) has played: the held popups of the script. |
| method | `public IEnumerator AfterIgnite(IgniteOutcomeView o)` | 301 | After the Ignite has played and its materials are spent: the next scripted hand, and the lines that follow. |
| method | `public void AfterChoose(string catalystId, string name)` | 326 | The catalyst was taken: deal the scripted hand that shows it. |
| method | `public IEnumerator OnOffer()` | 336 | The lines of the catalyst offer; the choice itself is the game's own screen. |
| method | `public void OfferTaken()` | 343 |  |
| method | `public IEnumerator Drive()` | 354 | Runs the lessons from the first bench to line 5.5. The game plays its own Ignite, descents and choice between. |
| property | `public sealed class IgniteOutcomeView` | 405 | The stage the AfterIgnite hook needs from the game: what the Ignite made. |

## Uses
[[Align]], [[Bench]], [[Dev]], [[FlowState]], [[FontKind]], [[GameFlow]], [[HudView]], [[Journal]], [[JournalView]], [[Label]], [[Loc]], [[Make]], [[Px]], [[ReactionEvent]], [[RunPhase]], [[ScoreParts]], [[TutorialRules]], [[TutorialScript]], [[TutorialTrial]]

## Used by
[[GameFlow]], [[ViewBaker]]

## Tested by
[[PauseTests]], [[SceneBakeTests]], [[TutorialFlowTests]]
