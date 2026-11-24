---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Satchel.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# Rng

`public sealed class Rng`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Satchel.cs:7-17`

> Small deterministic RNG so shuffles match between Unity and the dotnet test run.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| ctor | `public Rng(int seed)` | 10 |  |
| method | `public uint Next()` | 11 |  |
| method | `public int Range(int n)` | 12 |  |
| method | `public void Shuffle<T>(IList<T> list)` | 13 |  |

## Used by
[[ContentDb]], [[Profile]], [[RunState]], [[Satchel]], [[Unlocks]]

## Tested by
[[Phase3Tests]]
