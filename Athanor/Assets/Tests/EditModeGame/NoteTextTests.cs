using Athanor.Gameplay;
using Athanor.Game;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>An event note (a key and its arguments from the core) reads as the English or Vietnamese sentence the regex-matched wording used to give.</summary>
    public class NoteTextTests
    {
        const string Item = "Iron Ore", Other = "Copper", Kind = "Metal";

        [TearDown] public void BackToEnglish() { EditorGlobals.Inject(); Loc.Set(Loc.English); }

        static string Text(string lang, EventNote note)
        {
            EditorGlobals.Inject();
            Loc.Set(lang);
            return Wording.NoteText(note);
        }

        static EventNote Plural(string key, int n) => new EventNote { key = key, n = n, plural = true };

        [TestCase("en", 1, "+1 trial")]
        [TestCase("en", -2, "-2 trials")]
        [TestCase("en", 0, "0 trials")]
        [TestCase("vi", 1, "+1 lượt")]
        [TestCase("vi", -2, "-2 lượt")]
        [TestCase("vi", 0, "0 lượt")]
        public void NoteText_Trials_MatchesTheOldWording(string lang, int n, string expected) =>
            Assert.AreEqual(expected, Text(lang, Plural("note.trials", n)));

        [TestCase("en", 1, "+1 card in hand this depth")]
        [TestCase("en", -2, "-2 cards in hand this depth")]
        [TestCase("en", 0, "0 cards in hand this depth")]
        [TestCase("vi", 1, "+1 lá trên tay ở tầng này")]
        [TestCase("vi", -2, "-2 lá trên tay ở tầng này")]
        [TestCase("vi", 0, "0 lá trên tay ở tầng này")]
        public void NoteText_Hand_MatchesTheOldWording(string lang, int n, string expected) =>
            Assert.AreEqual(expected, Text(lang, Plural("note.hand", n)));

        [TestCase("en", 1, "+1 redraw")]
        [TestCase("en", -2, "-2 redraws")]
        [TestCase("en", 0, "0 redraws")]
        [TestCase("vi", 1, "+1 đổi bài")]
        [TestCase("vi", -2, "-2 đổi bài")]
        [TestCase("vi", 0, "0 đổi bài")]
        public void NoteText_Redraws_MatchesTheOldWording(string lang, int n, string expected) =>
            Assert.AreEqual(expected, Text(lang, Plural("note.redraws", n)));

        [TestCase("en", 1, "Reaction Index +1 this depth")]
        [TestCase("en", -1, "Reaction Index -1 this depth")]
        [TestCase("en", 0, "Reaction Index 0 this depth")]
        [TestCase("vi", 1, "Chỉ số Phản ứng +1 ở tầng này")]
        [TestCase("vi", -1, "Chỉ số Phản ứng -1 ở tầng này")]
        [TestCase("vi", 0, "Chỉ số Phản ứng 0 ở tầng này")]
        public void NoteText_RiShift_MatchesTheOldWording(string lang, int n, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.ri_shift", n = n }));

        [TestCase("en", 2, "A tile is warm this depth: +2 Reaction Index")]
        [TestCase("vi", 2, "Một ô ấm ở tầng này: +2 Chỉ số Phản ứng")]
        public void NoteText_HeatTile_MatchesTheOldWording(string lang, int n, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.heat_tile", n = n }));

        [TestCase("en", "Found Iron Ore")]
        [TestCase("vi", "Tìm thấy Iron Ore")]
        public void NoteText_Found_MatchesTheOldWording(string lang, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.found", item = Item }));

        [TestCase("en", "Gave up Iron Ore")]
        [TestCase("vi", "Đã bỏ Iron Ore")]
        public void NoteText_GaveUp_MatchesTheOldWording(string lang, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.gave_up", item = Item }));

        // The one deliberate change: the core wrote "Lost a Iron Ore for this run"; English now reads "Lost Iron Ore for this run" (owner, 2026-11-19).
        [TestCase("en", "Lost Iron Ore for this run")]
        [TestCase("vi", "Mất Iron Ore trong chuyến này")]
        public void NoteText_Lost_MatchesTheOldWording(string lang, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.lost", item = Item }));

        [TestCase("en", "Received Iron Ore")]
        [TestCase("vi", "Nhận được Iron Ore")]
        public void NoteText_Received_MatchesTheOldWording(string lang, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.received", item = Item }));

        [TestCase("en", "Iron Ore reacts with something Metal")]
        [TestCase("vi", "Iron Ore phản ứng với một thứ kim loại")]
        public void NoteText_Hint_MatchesTheOldWording(string lang, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.hint", item = Item, kind = Kind }));

        [TestCase("en", "Iron Ore became Copper")]
        [TestCase("vi", "Iron Ore hóa thành Copper")]
        public void NoteText_Became_MatchesTheOldWording(string lang, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.became", item = Item, other = Other }));

        [TestCase("en", "Your next first reaction scores triple")]
        [TestCase("vi", "Phản ứng đầu kế tiếp được điểm gấp ba")]
        public void NoteText_FirstTriple_MatchesTheOldWording(string lang, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.first_triple" }));

        [TestCase("en", "A tile is shut this depth")]
        [TestCase("vi", "Một ô bị chặn ở tầng này")]
        public void NoteText_BlockTile_MatchesTheOldWording(string lang, string expected) =>
            Assert.AreEqual(expected, Text(lang, new EventNote { key = "note.block_tile" }));
    }
}
