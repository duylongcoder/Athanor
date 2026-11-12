using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Plays a FlipbookSheet on a SpriteRenderer.</summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Flipbook : MonoBehaviour
    {
        public FlipbookSheet sheet;
        public bool playOnEnable = true, destroyOnEnd;
        public event Action Finished;

        SpriteRenderer body;
        int frame;
        float left;
        public bool IsPlaying { get; private set; }

        void Awake() => body = GetComponent<SpriteRenderer>();
        void OnEnable() { if (playOnEnable && sheet != null) Play(); }

        public void Play(FlipbookSheet s = null)
        {
            if (s != null) sheet = s;
            if (sheet == null || sheet.frames == null || sheet.frames.Length == 0) return;
            if (body == null) body = GetComponent<SpriteRenderer>();
            frame = 0; left = Duration(0); body.sprite = sheet.frames[0]; IsPlaying = true;
        }

        float Duration(int i) => sheet.durations != null && i < sheet.durations.Length ? Mathf.Max(0.01f, sheet.durations[i]) : 1 / 30f;

        void Update()
        {
            if (!IsPlaying) return;
            left -= Time.deltaTime;
            while (left <= 0 && IsPlaying)
            {
                frame++;
                if (frame >= sheet.frames.Length)
                {
                    if (sheet.loop) frame = 0;
                    else
                    {
                        IsPlaying = false;
                        Finished?.Invoke();
                        if (destroyOnEnd) Destroy(gameObject);
                        return;
                    }
                }
                body.sprite = sheet.frames[frame];
                left += Duration(frame);
            }
        }
    }
}
