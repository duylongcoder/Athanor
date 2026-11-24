---
generated: true
type: code
layer: Gameplay
kind: struct
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/LinkPair.cs
tags: [code/gameplay, kind/struct]
aliases: []
---

# LinkPair

`public struct LinkPair` : `IEquatable<LinkPair>`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/LinkPair.cs:7-25`

> Named pair for bench link coordinates — used as an alternative to value tuples so Unity serialization is reliable.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int a` | 9 |  |
| field | `public int b` | 10 |  |
| ctor | `public LinkPair(int a, int b)` | 12 |  |
| method | `public bool Equals(LinkPair other)` | 14 |  |
| method | `public override bool Equals(object obj)` | 16 |  |
| method | `public override int GetHashCode()` | 18 |  |
| property | `public static bool operator ==(LinkPair left, LinkPair right)` | 20 |  |
| property | `public static bool operator !=(LinkPair left, LinkPair right)` | 21 |  |
| method | `public void Deconstruct(out int a, out int b)` | 24 | Supports tuple deconstruction so `foreach (var (a, b) in links)` works. |

## Used by
[[BenchRules]], [[RunState]]

## Tested by
[[Phase3Tests]]
