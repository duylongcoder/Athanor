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
        float flashLeft, flashTime, flashPeak, shakeLeft, shakeTime, shakeSize;

        public void Build(Camera camera)
        {
            I = this;
            cam = camera.transform; camHome = cam.position;
            flash = Make.Rect(transform, 0, 0, Px.W, Px.H, Color.clear, 480);
            flash.name = "Flash"; flash.sharedMaterial = Make.Lib.glow;
            black = Make.Rect(transform, -8, -8, Px.W + 16, Px.H + 16, Color.clear, 500);
            black.name = "Fade";
        }

        public void Flash(float peak, float seconds) { flashPeak = peak; flashLeft = flashTime = seconds; }
        public void Shake(float pixels, float seconds) { shakeSize = pixels; shakeLeft = shakeTime = seconds; }

        public IEnumerator FadeOut(float seconds) => Anim.Over(seconds, t => black.color = new Color(0, 0, 0, t));
        public IEnumerator FadeIn(float seconds) => Anim.Over(seconds, t => black.color = new Color(0, 0, 0, 1 - t));
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
            if (shakeLeft > 0)
            {
                shakeLeft -= Time.deltaTime;
                float k = Mathf.Clamp01(shakeLeft / shakeTime), t = Time.time;
                cam.position = camHome + new Vector3(Mathf.Round(Mathf.Sin(t * 97) * shakeSize * k), Mathf.Round(Mathf.Cos(t * 71) * shakeSize * 0.8f * k), 0);
            }
            else cam.position = camHome;
        }
    }
}
