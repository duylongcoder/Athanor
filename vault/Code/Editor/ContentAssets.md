---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/Shared/ContentAssets.cs
tags: [code/editor, kind/class]
aliases: []
---

# ContentAssets

`public static class ContentAssets`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Shared/ContentAssets.cs:5-19`

> The content assets for Editor tools and tests (no scene, no Resources).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string Root = "Assets/Content", DatabasePath = Root + "/ContentDatabase.asset", RulesPath = Root +` | 7 |  |
| method | `public static ContentDatabase Database()` | 10 | Assets/Content/ContentDatabase.asset; throws when it is missing. |
| method | `public static ContentData Data()` | 18 | The content as the rules read it. |

## Uses
[[ContentData]], [[ContentDatabase]]

## Used by
[[BalanceReports]], [[ContentSnapshot]], [[GoldenShots]]

## Tested by
[[ContentAssetTests]], [[ContentValidationTests]], [[HotPathTests]], [[HudTests]], [[PauseTests]], [[PrefabBakeTests]], [[RunOverDepthTests]], [[SaveTests]], [[SceneBakeTests]], [[TutorialFlowTests]], [[TutorialStepsBakeTests]]
