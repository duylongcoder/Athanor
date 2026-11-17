using Athanor.Game;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// Line-box text (the Descent's depth name, SPEC (f)): the mockup sets it in a CSS line box as tall as the size, so the
    /// glyphs sit a half-leading below the box top; TMP puts the baseline one ascender down. For Crux that difference is
    /// 10.5 px at 48 (the VI name drew about 11 px high); in English nothing moves.
    /// </summary>
    public class LabelLineBoxTests
    {
        GameObject root;

        [SetUp] public void Build()
        {
            root = new GameObject("line box test");
        }

        [TearDown] public void Restore()
        {
            Object.DestroyImmediate(root);
            Loc.Set(Loc.English);
        }

        Label Name(int size)
        {
            var l = Make.Text(root.transform, "Bậc Thang Rỉ Nước", 320, 128, FontKind.Title, size, Color.white, 0, Align.Centre, Color.black);
            l.LineBox = true;
            return l;
        }

        [Test]
        public void HalfLeading_Crux48_IsElevenPixels()
        {
            var crux = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Fonts/Crux.asset");
            Assert.IsNotNull(crux, "run Athanor > Build Fonts");
            // (48 - (ascent 7 - descent -2) x 48/16) / 2 = 10.5, rounded up to a whole pixel
            Assert.AreEqual(11, Make.HalfLeading(crux.faceInfo, 48));
        }

        [Test]
        public void LineBoxTitle_InVietnamese_MovesFaceAndShadowDownByCruxHalfLeading()
        {
            Loc.Set(Loc.Vietnamese);
            var l = Name(48);
            Assert.AreEqual(-11f, l.main.transform.localPosition.y, 0.001f);
            Assert.AreEqual(-12f, l.shadow.transform.localPosition.y, 0.001f);
            Assert.AreEqual(1f, l.shadow.transform.localPosition.x, 0.001f);
        }

        [Test]
        public void LineBoxTitle_SizeSwitchTo32_FollowsTheNewSize()
        {
            Loc.Set(Loc.Vietnamese);
            var l = Name(48);
            l.SetSize(32);
            // (32 - 9 x 2) / 2 = 7
            Assert.AreEqual(-7f, l.main.transform.localPosition.y, 0.001f);
        }

        [Test]
        public void LineBoxTitle_InEnglish_DoesNotMove()
        {
            Loc.Set(Loc.English);
            var l = Name(48);
            Assert.AreEqual(0f, l.main.transform.localPosition.y, 0.001f);
            Assert.AreEqual(-1f, l.shadow.transform.localPosition.y, 0.001f);
        }

        [Test]
        public void PlainTitle_InVietnamese_DoesNotMove()
        {
            Loc.Set(Loc.Vietnamese);
            var l = Make.Text(root.transform, "Tạm dừng", 320, 88, FontKind.Title, 32, Color.white, 0, Align.Centre, Color.black);
            Assert.AreEqual(0f, l.main.transform.localPosition.y, 0.001f);
        }
    }
}
