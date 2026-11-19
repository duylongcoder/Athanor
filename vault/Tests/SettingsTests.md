---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/SettingsTests.cs
tags: [test]
count: 11
---

# SettingsTests
`Athanor/Assets/Tests/EditMode/SettingsTests.cs` — 11 tests

## Covers
[[Settings]]

## Tests
- `New_NoArguments_HasDefaultValues`
- `New_NoArguments_SwayIsOn`
- `ToJson_ThenFromJson_RoundTripsSwayOff`
- `FromJson_OldSettingsWithoutSway_KeepsSwayOn`
- `FromJson_SwayAsNumber_GivesDefaults`
- `ToJson_ThenFromJson_RoundTripsValues`
- `FromJson_JsonUtilityAndSystemTextJsonShape_ReadsFields`
- `FromJson_BrokenJson_GivesDefaults`
- `FromJson_MissingFields_KeepDefaults`
- `FromJson_OutOfRangeValues_ClampsVolumesAndLanguage`
- `RoundTrip_LanguageWithEscapes_Survives`
