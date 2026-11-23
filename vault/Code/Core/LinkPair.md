---
generated: true
type: code
layer: Core
kind: struct
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/LinkPair.cs
tags: [code/core, kind/struct]
aliases: []
---

# LinkPair

`public struct LinkPair` : `IEquatable<LinkPair>`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/LinkPair.cs:7-25`

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
