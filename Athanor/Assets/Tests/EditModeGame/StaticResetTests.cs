using System.Collections;
using System.Reflection;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// With domain reload off, statics survive Stop and Play. Every allowlisted class has a ResetStatics that
    /// [RuntimeInitializeOnLoadMethod(SubsystemRegistration)] runs before the first scene loads; these tests call it by hand.
    /// </summary>
    public class StaticResetTests
    {
        [TearDown] public void Restore() => EditorGlobals.Inject();

        static object Field(System.Type t, string name) =>
            t.GetField(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).GetValue(null);

        [Test]
        public void ResetStatics_Loc_DropsChangedHandlersAndGoesBackToEnglish()
        {
            int calls = 0;
            void OnChanged() => calls++;
            Loc.Changed += OnChanged;
            Loc.Set(Loc.Vietnamese);
            Assert.AreEqual(1, calls);
            Loc.ResetStatics();
            EditorGlobals.Inject();
            Assert.AreEqual(Loc.English, Loc.Code);
            Loc.Set(Loc.Vietnamese);
            Assert.AreEqual(1, calls, "the old handler was dropped");
        }

        [Test]
        public void ResetStatics_PointerInput_ClearsRigHoverAndAnyPress()
        {
            var go = new GameObject("rig");
            try
            {
                PointerInput.Use(go.AddComponent<DepthRig>());
                int presses = 0;
                PointerInput.AnyPress += () => presses++;
                Assert.IsNotNull(PointerInput.Rig);
                PointerInput.ResetStatics();
                Assert.IsNull(PointerInput.Rig);
                Assert.IsNull(PointerInput.Hover);
                Assert.IsFalse(PointerInput.Down || PointerInput.Up || PointerInput.Held);
                var press = typeof(PointerInput).GetField("AnyPress", BindingFlags.Static | BindingFlags.NonPublic);
                Assert.IsNull(press.GetValue(null), "no AnyPress handler is left");
                Assert.AreEqual(0, ((ICollection)Field(typeof(PointerInput), "all")).Count);
            }
            finally { Object.DestroyImmediate(go); }
        }

        [Test]
        public void ResetStatics_Haptics_RestoresEnabledAndClearsSink()
        {
            Haptics.Enabled = false; Haptics.ForceSupported = true; Haptics.Sink = _ => { };
            Haptics.ResetStatics();
            Assert.IsTrue(Haptics.Enabled);
            Assert.IsNull(Haptics.Sink);
            Assert.IsNull(Haptics.ForceSupported);
        }

        [Test]
        public void ResetStatics_AudioMute_RestoresUnmuted()
        {
            AudioMute.Sink = _ => { };
            AudioMute.SetHidden(true);
            Assert.IsTrue(AudioMute.Muted);
            AudioMute.ResetStatics();
            Assert.IsFalse(AudioMute.Muted);
            Assert.IsNull(AudioMute.Sink);
            Assert.IsFalse(AudioMute.ShouldMute(false, false, true));
            AudioMute.Sink = m => Assert.IsFalse(m, "hidden flag was cleared");
            AudioMute.SetFocus(true);
            AudioMute.Sink = null;
        }

        [Test]
        public void ResetStatics_SparkMesh_EmptiesTheMaterialCache()
        {
            SparkMesh.ResetStatics();           // other tests left materials in the cache
            EditorGlobals.Inject();
            var tex = new Texture2D(2, 2) { name = "reset test" };
            try
            {
                var first = SparkMesh.MaterialFor(tex);
                Assert.AreEqual(1, ((ICollection)Field(typeof(SparkMesh), "materials")).Count);
                SparkMesh.ResetStatics();
                Assert.AreEqual(0, ((ICollection)Field(typeof(SparkMesh), "materials")).Count);
                Assert.IsTrue(first == null, "the cached material was destroyed");
                Assert.IsNull(SparkMesh.BaseMaterial);
            }
            finally { Object.DestroyImmediate(tex); }
        }

        [Test]
        public void ResetStatics_TiltShift_ForgetsCameras()
        {
            var go = new GameObject("tilt");
            try
            {
                var cam = go.AddComponent<Camera>();
                go.AddComponent<TiltShift>();
                Assert.IsTrue(TiltShift.IsOn(cam));
                TiltShift.ResetStatics();
                Assert.IsFalse(TiltShift.IsOn(cam));
            }
            finally { Object.DestroyImmediate(go); }
        }

        [Test]
        public void ResetStatics_RarityStars_DropsTheCachedGlyph()
        {
            var go = new GameObject("stars");
            try
            {
                new RarityStars(go.transform, 0).Show(1, Color.white);
                Assert.IsTrue(RarityStars.GlyphCached);
                RarityStars.ResetStatics();
                Assert.IsFalse(RarityStars.GlyphCached);
            }
            finally { Object.DestroyImmediate(go); }
        }

        [Test]
        public void ResetStatics_Art_ClearsTheLibrary()
        {
            Assert.IsNotNull(Art.Lib);
            Art.ResetStatics();
            Assert.IsNull(Art.Lib);
        }
    }
}
