---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/Satchel.cs
tags: [code/core, kind/class]
aliases: []
---

# Rng

`public sealed class Rng`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Satchel.cs:7-17`

> Small deterministic RNG so shuffles match between Unity and the dotnet test run.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| ctor | `public Rng(int seed)` | 10 |  |
| method | `public uint Next()` | 11 |  |
| method | `public int Range(int n)` | 12 |  |
| method | `public void Shuffle<T>(IList<T> list)` | 13 |  |

## Used by
[[Profile]], [[RunState]], [[Satchel]], [[Unlocks]]

## Tested by
[[Phase3Tests]]
