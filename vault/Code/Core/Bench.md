---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/Ignite.cs
tags: [code/core, kind/class]
aliases: []
---

# Bench

`public sealed class Bench`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Ignite.cs:7-13`

> The 3x3 bench: tile index = row * 3 + column, null where empty.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int Size = 3, TileCount = 9, Centre = 4` | 9 |  |
| field | `public readonly string[] Tiles = new string[TileCount]` | 10 |  |
| property | `public int Count` | 11 |  |
| method | `public void Clear()` | 12 |  |

## Used by
[[AutoPlayer]], [[BenchRules]], [[BenchView]], [[Card]], [[CatalystChoiceView]], [[FlowState]], [[GameFlow]], [[IgniteResolver]], [[IgniteSequence]], [[PointerInput]], [[RunState]], [[ScreenFlow]], [[TutorialController]], [[TutorialScript]]

## Tested by
[[ChainTests]], [[ContentTests]], [[IgniteTests]], [[MainDepthTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RunStateTests]], [[SceneBakeTests]], [[ScreenFlowTests]], [[TutorialFlowTests]], [[TutorialTests]]
