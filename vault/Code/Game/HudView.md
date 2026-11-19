---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/HudView.cs
tags: [code/game, kind/class]
aliases: []
---

# HudView

`public sealed class HudView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/HudView.cs:8-242`

> The panels round the bench: depth and potency, catalysts, trials and redraws, the last-reaction note, journal and Ignite.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action IgniteClicked, JournalClicked, RedrawClicked, PauseClicked` | 10 |  |
| property | `public int HoveredCatalyst` | 12 | Catalyst socket index the pointer is over, or -1. |
| property | `public bool RedrawHovered` | 13 |  |
| property | `public bool PauseHovered` | 15 | The pointer is over the pause button's 44x44 hit box. |
| field | `public const int PauseX = 12, PauseY = 152, PauseW = 28, PauseH = 20, PauseOrder = 650` | 19 | The pause button's picture (28x20 at 12,152) and its 44x44 hit box (4,140); SPEC item 11 deviation: the aspects shelf band is full, shown only while a pause can open. |
| property | `public int HoveredAspect` | 23 | Index of the held aspect the pointer is over (in the strip under the depth panel), or -1. |
| field | `public const int MaxCatalysts = 6, MaxAspects = 3` | 25 | The most catalysts the sockets can show: four from clearing depths and two from shrines. |
| field | `public const int RedrawX = 556, RedrawY = 88, RedrawW = 76, RedrawH = 20` | 27 | The Redraw button in art space: under the right panel, right below the redraws-left counter. |
| method | `public void SetPauseShown(bool shown)` | 55 | Shows or hides the pause button (and its hit box): shown only while the bench is the player's. |
| property | `public bool PauseShown` | 61 |  |
| property | `public Vector3 PauseAnchor` | 62 |  |
| property | `public Vector3 PotencyPosition` | 64 |  |
| method | `public Vector3 CatalystAnchor(int i)` | 66 | Where a tooltip about a held catalyst or aspect points. |
| method | `public Vector3 AspectAnchor(int i)` | 67 |  |
| property | `public Vector3 RedrawAnchor` | 69 | Where the Redraw tooltip (and the tutorial's highlight) points: the button's centre. |
| method | `public static bool CanRedraw(RunState run)` | 71 | The Redraw button is live: a redraw is left and the bench is being played (not an offer, not a cleared depth). |
| method | `public static string Number(long n)` | 72 |  |
| method | `public void Bind()` | 77 | Hooks the hit boxes to the hover and click handlers. Once per instance: the pointer calls them, nothing else does. |
| method | `public void Show(RunState run, ContentDb db, Journal journal, bool inputEnabled, bool keepRedraw = false)` | 117 | Everything except the potency number, which is animated separately. keepRedraw leaves the Redraw button and its counter as they are: GameFlow.Resolve passes it while an Ignite plays out, so they do not show the outcome early. |
| method | `public void SetPotency(long value, bool instant = false)` | 165 |  |
| method | `public void Note(ReactionEvent e, ContentDb db)` | 168 | Writes one reaction on the note: what went in, what came of it and its potency. |
| method | `public void Hint(HintData h, ContentDb db)` | 192 |  |
| method | `public static string HintLine(HintData h, ContentDb db, bool withName = true)` | 199 | "Ember Coal stirs near a potion.", or for a recipe the bench was too dull for, "Ember Coal needs heat 14." |
| method | `public void Bump()` | 207 | A short kick of the potency number when points land on it. |
| method | `public void ClearNote()` | 209 |  |

## Uses
[[Art]], [[Clickable]], [[ContentDb]], [[Journal]], [[Label]], [[Loc]], [[Px]], [[ReactionEvent]], [[Row]], [[RunPhase]], [[RunState]]

## Used by
[[BenchView]], [[GoldenShots]], [[IgniteSequence]], [[JournalView]], [[ReactionCutscene]], [[TutorialController]]

## Tested by
[[HudTests]], [[PrefabBakeTests]]
