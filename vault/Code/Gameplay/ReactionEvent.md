---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Ignite.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# ReactionEvent

`public sealed class ReactionEvent`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Ignite.cs:21-34`

> One thing that happened during an Ignite, in the order it should be shown. A reaction: Tiles[0] is the hub, where the result now stands; the other tiles were consumed. A sputter: a combo that stood ready and was spoiled by the reaction before it. A fizzle (no Result, not a Sputter): two neighbours left on the bench that do nothing together.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int Hub` | 23 |  |
| field | `public int[] Tiles` | 24 |  |
| field | `public string[] Items` | 25 |  |
| field | `public string Result` | 26 |  |
| field | `public RecipeData Recipe` | 27 |  |
| field | `public bool Discovery` | 28 |  |
| field | `public bool FirstThisRun` | 29 |  |
| field | `public bool Sputter` | 30 |  |
| field | `public int Chain` | 31 |  |
| field | `public long Potency` | 32 |  |
| field | `public HintData Hint` | 33 |  |

## Used by
[[HudView]], [[IgniteOutcome]], [[IgniteResolver]], [[IgniteSequence]], [[ReactionCutscene]], [[ResolveResult]], [[RunState]], [[ScoreParts]], [[TutorialController]]

## Tested by
[[IgniteTests]], [[TutorialTests]]
