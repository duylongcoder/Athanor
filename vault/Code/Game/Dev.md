---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Dev.cs
tags: [code/game, kind/class]
aliases: []
---

# Dev

`public static class Dev`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Dev.cs:9-12`

> Log lines that exist only as the hook of tools/web/webtest.mjs ([tut], [drive], [loc]). The calls, and the strings they build, are compiled out of a release build; a development build and the editor keep them.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static void Hook(string line)` | 11 |  |

## Used by
[[BenchView]], [[CatalystChoiceView]], [[EventView]], [[GameManager]], [[TitleView]], [[TutorialManager]]
