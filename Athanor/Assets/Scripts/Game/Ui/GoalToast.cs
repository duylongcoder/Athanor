using System.Collections.Generic;
using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// A small panel at the top of the screen that says a goal was completed and what it unlocked.
    /// Toasts queue; each stays 2.5 seconds or until clicked. It never blocks anything under it but itself.
    /// </summary>
    public sealed class GoalToast : MonoBehaviour
    {
        /// <summary>The toast's burst when it appears, how far past the top edge it hides, and its resting box (art px, y down), stored in GoalToast.prefab.</summary>
        [System.Serializable]
        public struct Layout { public BurstSpec burst; public float hideGap, x, y, width, height; }
        [SerializeField] Layout layout;
        /// <summary>Seconds a toast stays fully shown, and takes to slide in and again to slide out.</summary>
        [System.Serializable]
        public struct Timing { public float stay, slide; }
        [SerializeField] Timing timing;

        /// <summary>The age (seconds) at which a toast starts to slide out.</summary>
        public float SlideOutAt => timing.stay + timing.slide;

        struct Item { public string goal, reward, sprite; }

        readonly Queue<Item> queue = new Queue<Item>();
        [SerializeField] Transform panel;
        [SerializeField] SpriteRenderer icon;
        [SerializeField] Label title, line;
        [SerializeField] Clickable click;
        float age = -1;

        /// <summary>The goal line of the toast showing (or last shown).</summary>
        public Label Title => title;
        /// <summary>The reward line of the toast showing (or last shown).</summary>
        public Label Line => line;

        /// <summary>Hooks the click. GameManager calls it once at start; a test calls it after instantiating the prefab.</summary>
        public void Bind(FxRefs effects)
        {
            if (effects == null) throw new System.ArgumentNullException(nameof(effects));
            fx = effects;
            click.onClick = SkipToEnd;
        }

        FxRefs fx;

        // a click on a showing toast sends it away now
        void SkipToEnd() { if (age >= 0) age = Mathf.Max(age, SlideOutAt); }

        /// <summary>Queues one toast for each goal just completed.</summary>
        public void Add(IEnumerable<GoalReward> rewards, ContentDb db)
        {
            foreach (var r in rewards)
                queue.Enqueue(new Item
                {
                    goal = Loc.T("ui.toast.goal", ("goal", Loc.Upper(Wording.GoalText(r.Goal)))),
                    reward = r.Item == null ? Loc.T("ui.toast.nothing") : Loc.T("ui.toast.unlocked", ("item", Wording.ItemName(db, r.Item))),
                    sprite = r.Item == null ? null : Wording.ItemSprite(db, r.Item),
                });
        }

        /// <summary>Drops whatever is showing or waiting (the profile was reset).</summary>
        public void Clear() { queue.Clear(); age = -1; panel.gameObject.SetActive(false); }

        void Update()
        {
            if (age < 0)
            {
                if (queue.Count == 0) return;
                var next = queue.Dequeue();
                title.Text = next.goal; line.Text = next.reward;
                icon.enabled = next.sprite != null;
                if (next.sprite != null) icon.sprite = Art.Lib.Sprite(next.sprite);
                age = 0;
                panel.gameObject.SetActive(true);
                if (fx != null) layout.burst.Play(fx.Sparks, Px.Mid(layout.x, layout.y, layout.width, layout.height));
            }
            age += Time.deltaTime;
            // slides down from above the screen, waits, slides back up
            float slide = timing.slide, stay = timing.stay;
            float shown = age < slide ? Anim.OutCubic(age / slide) : age < slide + stay ? 1 : 1 - Anim.InCubic(Mathf.Clamp01((age - slide - stay) / slide));
            panel.position = Px.Snap(Px.Mid(layout.x, layout.y - (1 - shown) * (layout.height + layout.y + layout.hideGap), layout.width, layout.height));
            if (age >= stay + 2 * slide) { age = -1; panel.gameObject.SetActive(false); }
        }
    }
}
