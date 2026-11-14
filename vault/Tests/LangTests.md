---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/LangTests.cs
tags: [test]
count: 22
---

# LangTests
`Athanor/Assets/Tests/EditMode/LangTests.cs` — 22 tests

## Covers
[[Json]], [[Lang]], [[Make]]

## Tests
- `BothFilesParse_WithTheirLanguageAndName`
- `SystemTextJson_ReadsTheSameEntries`
- `NoDuplicateKeys`
- `NoEmptyText`
- `EnglishFile_HoldsNoContentKeys`
- `EveryVietnameseKey_IsAnEnglishKeyOrAContentKey`
- `Vietnamese_HasEveryUiKey`
- `Vietnamese_HasEveryContentText_AndNoItemNames`
- `Vietnamese_IsNotStale`
- `Placeholders_MatchTheEnglish`
- `EveryVietnameseCharacter_IsInBothFonts`
- `Vietnamese_IsPrecomposed`
- `VietnameseEventTexts_FitTheEventWindow`
- `FontSet_HasThe134VietnameseLetters`
- `RealLookups`
- `Fill_NamedPlaceholders_SignAndFormat`
- `Fallback_LanguageThenEnglishThenSuppliedThenKey`
- `Plural_OneAndOther_AndOtherOnlyLanguages`
- `Missing_CountsOtherAsCoveringOne`
- `Stale_FlagsChangedEnglish`
- `Parse_EscapesUnknownFieldsAndBom`
- `ContentEnglish_KeysFollowTheScheme`
