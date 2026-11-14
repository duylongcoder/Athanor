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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/HudView.cs:9-248`

> The panels round the bench: depth and potency, catalysts, trials and redraws, the last-reaction note, journal and Ignite.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action IgniteClicked, JournalClicked, RedrawClicked` | 12 |  |
| property | `public int HoveredCatalyst` | 14 | Catalyst socket index the pointer is over, or -1. |
| property | `public bool RedrawHovered` | 15 |  |
| property | `public int HoveredAspect` | 17 | Index of the held aspect the pointer is over (in the strip under the depth panel), or -1. |
| field | `public const int MaxCatalysts = 6, MaxAspects = 3` | 19 | The most catalysts the sockets can show: four from clearing depths and two from shrines. |
| field | `public const int RedrawX = 556, RedrawY = 88, RedrawW = 76, RedrawH = 20` | 21 | The Redraw button in art space: under the right panel, right below the redraws-left counter. |
| property | `public Vector3 PotencyPosition` | 46 |  |
| method | `public Vector3 CatalystAnchor(int i)` | 48 | Where a tooltip about a held catalyst or aspect points. |
| method | `public Vector3 AspectAnchor(int i)` | 49 |  |
| property | `public Vector3 RedrawAnchor` | 51 | Where the Redraw tooltip (and the tutorial's highlight) points: the button's centre. |
| method | `public static bool CanRedraw(RunState run)` | 53 | The Redraw button is live: a redraw is left and the bench is being played (not an offer, not a cleared depth). |
| method | `public static string Number(long n)` | 54 |  |
| method | `public void Build(Transform parent)` | 56 |  |
| method | `public void Show(RunState run, ContentDb db, Journal journal, bool inputEnabled, bool keepRedraw = false)` | 130 | Everything except the potency number, which is animated separately. keepRedraw leaves the Redraw button and its counter as they are: GameFlow.Resolve passes it while an Ignite plays out, so they do not show the outcome early. |
| method | `public void SetPotency(long value, bool instant = false)` | 178 |  |
| method | `public void Note(ReactionEvent e, ContentDb db)` | 181 | Writes one reaction on the note: what went in, what came of it and its potency. |
| method | `public void Hint(HintData h, ContentDb db)` | 198 |  |
| method | `public static string HintLine(HintData h, ContentDb db, bool withName = true)` | 205 | "Ember Coal stirs near a potion.", or for a recipe the bench was too dull for, "Ember Coal needs heat 14." |
| method | `public void Bump()` | 213 | A short kick of the potency number when points land on it. |
| method | `public void ClearNote()` | 215 |  |

## Uses
[[Align]], [[ContentDb]], [[FontKind]], [[Journal]], [[Label]], [[Make]], [[Px]], [[ReactionEvent]], [[RunPhase]], [[RunState]]

## Used by
[[BenchView]], [[GameFlow]], [[JournalView]], [[ReactionCutscene]]

## Tested by
[[HudTests]]
