---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Managers/ContentManager.cs
tags: [code/game, kind/class]
aliases: []
---

# ContentManager

`public sealed class ContentManager` : `Singleton<ContentManager>`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Managers/ContentManager.cs:8-23`

> The game's content: the ContentDatabase asset, validated and indexed once at start-up.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public ContentDatabase Database` | 14 | The asset the Inspector points at. |
| property | `public ContentDb Db` | 16 | The validated content the rules read. |
| method | `protected override void OnAwake()` | 18 |  |

## Uses
[[ContentDatabase]], [[ContentDb]], [[Singleton]], [[Tooltip]]

## Used by
[[GameManager]], [[ManagerSceneMigration]], [[ProgressManager]], [[RunManager]]

## Tested by
[[ManagerBootTests]]
