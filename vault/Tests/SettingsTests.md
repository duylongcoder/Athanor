---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/SettingsTests.cs
tags: [test]
count: 7
---

# SettingsTests
`Athanor/Assets/Tests/EditMode/SettingsTests.cs` — 7 tests

## Covers
[[Settings]]

## Tests
- `New_NoArguments_HasDefaultValues`
- `ToJson_ThenFromJson_RoundTripsValues`
- `FromJson_JsonUtilityAndSystemTextJsonShape_ReadsFields`
- `FromJson_BrokenJson_GivesDefaults`
- `FromJson_MissingFields_KeepDefaults`
- `FromJson_OutOfRangeValues_ClampsVolumesAndLanguage`
- `RoundTrip_LanguageWithEscapes_Survives`
