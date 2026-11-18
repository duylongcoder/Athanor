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
[[Json]], [[Lang]]

## Tests
- `LangFiles_WhenParsed_HoldTheirLanguageAndName`
- `SystemTextJson_WhenReadingBothFiles_ReadsTheSameEntries`
- `LangFiles_WhenGroupedByKey_HaveNoDuplicateKeys`
- `LangFiles_WhenCheckingEntries_HaveNoEmptyText`
- `EnglishFile_ComparedWithContentKeys_HoldsNoContentKeys`
- `VietnameseKeys_WhenLookedUpInEnglish_AreAnEnglishKeyOrAContentKey`
- `Vietnamese_ComparedWithEnglishKeys_HasEveryUiKey`
- `Vietnamese_HasEveryContentText_AndNoItemNames`
- `Vietnamese_ComparedWithCurrentEnglish_IsNotStale`
- `Placeholders_InEveryVietnameseEntry_MatchTheEnglish`
- `VietnameseText_WhenCheckedAgainstTheFontSet_UsesOnlyCharactersInBothFonts`
- `Vietnamese_WhenNormalizedToFormC_IsUnchangedAndPrecomposed`
- `VietnameseEventTexts_AgainstEnglishLimits_FitTheEventWindow`
- `FontSet_WhenCounted_HasThe134VietnameseLetters`
- `RealLookups_InVietnameseAndEnglish_ReturnTheShippedText`
- `Fill_NamedPlaceholders_SignAndFormat`
- `Fallback_WhenTextIsMissing_GoesLanguageThenEnglishThenSuppliedThenKey`
- `Plural_OneAndOther_AndOtherOnlyLanguages`
- `Missing_WithOtherFormPresent_CountsOtherAsCoveringOne`
- `Stale_WhenTheEnglishChanged_FlagsTheEntry`
- `Parse_WithEscapesUnknownFieldsAndBom_ReadsTheEntries`
- `ContentEnglish_ForShippedContent_KeysFollowTheScheme`
