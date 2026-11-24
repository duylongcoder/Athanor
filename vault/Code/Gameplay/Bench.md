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

# Bench

`public sealed class Bench`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Ignite.cs:7-13`

> The 3x3 bench: tile index = row * 3 + column, null where empty.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int Size = 3, TileCount = 9, Centre = 4` | 9 |  |
| field | `public readonly string[] Tiles = new string[TileCount]` | 10 |  |
| property | `public int Count` | 11 |  |
| method | `public void Clear()` | 12 |  |

## Used by
[[AspectScope]], [[AutoPlayer]], [[BenchRules]], [[BenchView]], [[Card]], [[CatalystChoiceView]], [[FlowState]], [[GameFlow]], [[IgniteResolver]], [[IgniteSequence]], [[Newcomer]], [[PointerInput]], [[RunState]], [[ScreenFlow]], [[Sim]], [[TutorialArea]], [[TutorialController]], [[TutorialScript]], [[TutorialStepsMigration]], [[ViewRef]]

## Tested by
[[ChainTests]], [[ContentTests]], [[IgniteTests]], [[MainDepthTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RunStateTests]], [[SceneBakeTests]], [[ScreenFlowTests]], [[TutorialFlowTests]], [[TutorialTests]]
