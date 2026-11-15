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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs:31-398`

> Plays the scripted tutorial over the bench: a dark screen with lit holes and a pulsing gold rim, an arrow, a text box at the top, click locks, and popups held until read. It reads the game's state; it decides no rule.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public int Trial` | 48 | The trial being taught: 0 reaction, 1 miss, 2 chain, 3 the catalyst trial of depth 2. |
| property | `public bool Finished` | 50 | True from the first line to the last (the real journal is never touched in a replay; see GameFlow). |
| field | `public readonly List<long> Shown = new List<long>()` | 52 | The scores the popups showed, in order (checked by the tests against TutorialTests: 40 / 0 / 40 / 240 / ...). |
| method | `public void Build()` | 54 |  |
| method | `public static Rect Union(Rect[] r)` | 69 | The box round the holes. No holes (a text-only step such as 2.3) is the whole canvas: a full dim, no rim, no arrow. |
| method | `public void Abort()` | 169 | Quit to title in the tutorial: stops the lessons and takes the overlay down. Does not mark the tutorial done. |
| method | `public IEnumerator AfterEvent(ReactionEvent e, int index)` | 253 | After a reaction (or fizzle) has played: the held popups of the script. |
| method | `public IEnumerator AfterIgnite(IgniteOutcomeView o)` | 293 | After the Ignite has played and its materials are spent: the next scripted hand, and the lines that follow. |
| method | `public void AfterChoose(string catalystId, string name)` | 318 | The catalyst was taken: deal the scripted hand that shows it. |
| method | `public IEnumerator OnOffer()` | 328 | The lines of the catalyst offer; the choice itself is the game's own screen. |
| method | `public void OfferTaken()` | 335 |  |
| method | `public IEnumerator Drive(GameFlow flow)` | 346 | Runs the lessons from the first bench to line 5.5. The game plays its own Ignite, descents and choice between. |
| property | `public sealed class IgniteOutcomeView` | 397 | The stage the AfterIgnite hook needs from the game: what the Ignite made. |

## Uses
[[Align]], [[Bench]], [[Dev]], [[FlowState]], [[FontKind]], [[GameFlow]], [[HudView]], [[Journal]], [[JournalView]], [[Label]], [[Loc]], [[Make]], [[Px]], [[ReactionEvent]], [[RunPhase]], [[ScoreParts]], [[TutorialRules]], [[TutorialScript]], [[TutorialTrial]]

## Used by
[[GameFlow]]

## Tested by
[[PauseTests]], [[TutorialFlowTests]]
