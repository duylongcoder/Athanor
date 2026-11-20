using System;
using System.Collections;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Whole-screen effects: a white flash, camera shake, and fading to and from black.</summary>
    public sealed class ScreenFx : MonoBehaviour
    {
        /// <summary>Stops what this component started so nothing runs on into the next Play (domain reload off).</summary>
        void OnDestroy() { StopAllCoroutines(); Motion.StopOn(this); }

        [SerializeField] SpriteRenderer flash, black;

        /// <summary>The camera shake's two frequencies (radians per second) and how much of the x travel the y axis gets; held by Main.unity.</summary>
        [Serializable]
        public struct ShakeTiming
        {
            public float rateX, rateY, yScale;
        }
        [SerializeField] ShakeTiming shake;
        Transform cam;
        Vector3 camHome;
        CameraSway world;
        DepthRig rig;
        float flashLeft, flashTime, flashPeak, shakeLeft, shakeTime, shakeSize;
        bool flashLit = true;          // the flash may be showing: written clear once when it ends, then left alone

        /// <summary>camera is the camera the shake moves, unless a depth rig is given: then the shake moves the world only
        /// (owner gate G2 #6), except in a cutscene, which is flat and shakes with its camera.</summary>
        public void Bind(Camera camera, CameraSway worldSway = null)
        {
            world = worldSway;
            rig = worldSway != null ? worldSway.GetComponent<DepthRig>() : null;
            cam = camera.transform; camHome = cam.position;
        }

        public void Flash(float peak, float seconds) { flashPeak = peak; flashLeft = flashTime = seconds; }
        public void Shake(float pixels, float seconds) { shakeSize = pixels; shakeLeft = shakeTime = seconds; }

        public IEnumerator FadeOut(float seconds) => Motion.Fade(black, 0, 1, seconds).ToYieldInstruction();
        public IEnumerator FadeIn(float seconds) => Motion.Fade(black, 1, 0, seconds).ToYieldInstruction();
        public void SetBlack(float a) => black.color = Color.black.Alpha(a);

        void LateUpdate() => Tick(Time.deltaTime);

        /// <summary>One frame of the screen effects. With no flash running the flash sprite is not written.</summary>
        public void Tick(float deltaTime)
        {
            if (cam == null) return;                      // not bound yet (a scene opened in the editor)
            if (flashLeft > 0)
            {
                flashLeft -= deltaTime;
                float k = Mathf.Clamp01(flashLeft / flashTime);
                flash.color = Color.white.Alpha(flashPeak * k * k);
                flashLit = true;
            }
            else if (flashLit) { flash.color = Color.clear; flashLit = false; }
            Vector3 offset = Vector3.zero;
            if (shakeLeft > 0)
            {
                shakeLeft -= deltaTime;
                float k = Mathf.Clamp01(shakeLeft / shakeTime), t = Time.time;
                float x = Mathf.Round(Mathf.Sin(t * shake.rateX) * shakeSize * k);
                float y = Mathf.Round(Mathf.Cos(t * shake.rateY) * shakeSize * shake.yScale * k);
                offset = new Vector3(x, y, 0);
            }
            bool onWorld = world != null && !rig.Flat;
            if (world != null) world.external = onWorld ? (Vector2)offset : Vector2.zero;
            cam.position = camHome + (onWorld ? Vector3.zero : offset);
        }
    }
}
