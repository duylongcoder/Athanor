using System.Collections;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Whole-screen effects: a white flash, camera shake, and fading to and from black.</summary>
    public sealed class ScreenFx : MonoBehaviour
    {
        public static ScreenFx I { get; private set; }
        SpriteRenderer flash, black;
        Transform cam;
        Vector3 camHome;
        CameraSway world;
        DepthRig rig;
        float flashLeft, flashTime, flashPeak, shakeLeft, shakeTime, shakeSize;

        /// <summary>camera is the camera the shake moves, unless a depth rig is given: then the shake moves the world only
        /// (owner gate G2 #6), except in a cutscene, which is flat and shakes with its camera.</summary>
        public void Build(Camera camera, CameraSway worldSway = null)
        {
            I = this;
            world = worldSway;
            rig = worldSway != null ? worldSway.GetComponent<DepthRig>() : null;
            cam = camera.transform; camHome = cam.position;
            flash = Make.Rect(transform, 0, 0, Px.W, Px.H, Color.clear, 480);
            flash.name = "Flash"; flash.sharedMaterial = Art.Lib.glow;
            black = Make.Rect(transform, -8, -8, Px.W + 16, Px.H + 16, Color.clear, 500);
            black.name = "Fade";
        }

        public void Flash(float peak, float seconds) { flashPeak = peak; flashLeft = flashTime = seconds; }
        public void Shake(float pixels, float seconds) { shakeSize = pixels; shakeLeft = shakeTime = seconds; }

        public IEnumerator FadeOut(float seconds) => Motion.Fade(black, 0, 1, seconds).ToYieldInstruction();
        public IEnumerator FadeIn(float seconds) => Motion.Fade(black, 1, 0, seconds).ToYieldInstruction();
        public void SetBlack(float a) => black.color = new Color(0, 0, 0, a);

        void LateUpdate()
        {
            if (flashLeft > 0)
            {
                flashLeft -= Time.deltaTime;
                float k = Mathf.Clamp01(flashLeft / flashTime);
                flash.color = new Color(1, 1, 1, flashPeak * k * k);
            }
            else flash.color = Color.clear;
            Vector3 offset = Vector3.zero;
            if (shakeLeft > 0)
            {
                shakeLeft -= Time.deltaTime;
                float k = Mathf.Clamp01(shakeLeft / shakeTime), t = Time.time;
                offset = new Vector3(Mathf.Round(Mathf.Sin(t * 97) * shakeSize * k), Mathf.Round(Mathf.Cos(t * 71) * shakeSize * 0.8f * k), 0);
            }
            bool onWorld = world != null && !rig.Flat;
            if (world != null) world.external = onWorld ? (Vector2)offset : Vector2.zero;
            cam.position = camHome + (onWorld ? Vector3.zero : offset);
        }
    }
}
