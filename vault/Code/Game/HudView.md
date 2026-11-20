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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/HudView.cs:8-339`

> The panels round the bench: depth and potency, catalysts, trials and redraws, the last-reaction note, journal and Ignite.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action IgniteClicked, JournalClicked, RedrawClicked, PauseClicked` | 10 |  |
| property | `public int HoveredCatalyst` | 12 | Catalyst socket index the pointer is over, or -1. |
| property | `public bool RedrawHovered` | 13 |  |
| property | `public bool PauseHovered` | 15 | The pointer is over the pause button's 44x44 hit box. |
| field | `public const int PauseOrder = 650` | 17 | The pause button's sort order. |
| property | `public int HoveredAspect` | 23 | Index of the held aspect the pointer is over (in the strip under the depth panel), or -1. |
| field | `public const int MaxCatalysts = 6, MaxAspects = 3` | 25 | The most catalysts the sockets can show: four from clearing depths and two from shrines. |
| method | `public void SetPauseShown(bool shown)` | 99 | Shows or hides the pause button (and its hit box): shown only while the bench is the player's. |
| property | `public bool PauseShown` | 105 |  |
| property | `public Vector3 PauseAnchor` | 106 |  |
| property | `public Rect RedrawRect` | 109 | The Redraw button in art space (top-left origin). |
| property | `public Rect PauseRect` | 111 | The pause button's picture in art space (top-left origin). |
| property | `public Vector3 PotencyPosition` | 112 |  |
| method | `public Vector3 CatalystAnchor(int i)` | 114 | Where a tooltip about a held catalyst or aspect points. |
| method | `public Vector3 AspectAnchor(int i)` | 115 |  |
| property | `public Vector3 RedrawAnchor` | 117 | Where the Redraw tooltip (and the tutorial's highlight) points: the button's centre. |
| method | `public static bool CanRedraw(RunState run)` | 119 | The Redraw button is live: a redraw is left and the bench is being played (not an offer, not a cleared depth). |
| method | `public static string Number(long n)` | 120 |  |
| method | `public void Bind()` | 125 | Hooks the hit boxes to the hover and click handlers. Once per instance: the pointer calls them, nothing else does. |
| method | `public void Show(RunState run, ContentDb db, Journal journal, bool inputEnabled, bool keepRedraw = false)` | 165 | Everything except the potency number, which is animated separately. keepRedraw leaves the Redraw button and its counter as they are: GameFlow.Resolve passes it while an Ignite plays out, so they do not show the outcome early. |
| method | `public void SetPotency(long value, bool instant = false)` | 213 |  |
| method | `public void Note(ReactionEvent e, ContentDb db)` | 216 | Writes one reaction on the note: what went in, what came of it and its potency. |
| method | `public void Hint(HintData h, ContentDb db)` | 240 |  |
| method | `public static string HintLine(HintData h, ContentDb db, bool withName = true)` | 247 | "Ember Coal stirs near a potion.", or for a recipe the bench was too dull for, "Ember Coal needs heat 14." |
| method | `public void Bump()` | 255 | A short kick of the potency number when points land on it. |
| method | `public void ClearNote()` | 257 |  |
| method | `public void Tick(float deltaTime)` | 266 | One frame of the HUD. Idle frames build no string and look up no sprite: the potency text is formatted only when the rounded number changes, the Ignite sprite and the two labels move only when the button's look changes, the bar is laid out only when its width changes. Only the Ignite glow, a continuous pulse, is written every frame (and only while lit). |

## Uses
[[Art]], [[Clickable]], [[ContentDb]], [[Journal]], [[Label]], [[Loc]], [[Palette]], [[Px]], [[ReactionEvent]], [[Row]], [[RunPhase]], [[RunState]]

## Used by
[[BenchView]], [[GoldenShots]], [[IgniteSequence]], [[JournalView]], [[ReactionCutscene]], [[TutorialController]]

## Tested by
[[HotPathTests]], [[HudTests]], [[LayoutPinTests]], [[PrefabBakeTests]]
