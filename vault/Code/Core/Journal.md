---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/Journal.cs
tags: [code/core, kind/class]
aliases: []
---

# Journal

`public sealed class Journal`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Journal.cs:8-66`

> What the player has learned across runs: recipes that worked, and hints from ones that did not.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public IReadOnlyCollection<string> KnownPairs` | 13 |  |
| property | `public IReadOnlyList<HintData> Hints` | 14 |  |
| method | `public bool IsKnown(string a, string b)` | 16 |  |
| method | `public bool Learn(string a, string b)` | 17 |  |
| method | `public bool IsKnown(IEnumerable<string> items)` | 19 |  |
| method | `public bool Learn(IEnumerable<string> items)` | 20 |  |
| method | `public bool IsComplete(string material, ContentDb db)` | 23 | Every recipe this material is an ingredient of has been found. |
| method | `public IEnumerable<HintData> OpenHints(string material, ContentDb db)` | 27 | The hints about a material, for as long as it still has something unfound. |
| method | `public bool HasHint(string material, Kind partnerKind)` | 30 |  |
| method | `public bool AddHint(string material, Kind partnerKind)` | 33 |  |
| method | `public bool AddHeatHint(string material, int heat)` | 41 | A hint that this material has a recipe waiting on more heat round its hub. |
| method | `public JournalData ToData()` | 48 |  |
| method | `public static Journal FromData(JournalData d)` | 51 | Never throws: anything malformed is skipped. |

## Uses
[[ContentDb]], [[Kind]]

## Used by
[[BenchView]], [[FlowState]], [[GameFlow]], [[GoalsView]], [[GoldenShots]], [[HudView]], [[IgniteResolver]], [[JournalView]], [[Profile]], [[RunOverView]], [[RunState]], [[Save]], [[ScreenFlow]], [[TutorialController]]

## Tested by
[[HotPathTests]], [[HudTests]], [[IgniteTests]], [[JournalTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityReportTests]], [[RarityTests]], [[RoleSimTests]], [[RulesTests]], [[RunStateTests]], [[SaveTests]], [[ScreenFlowTests]], [[TutorialFlowTests]], [[TutorialTests]]
