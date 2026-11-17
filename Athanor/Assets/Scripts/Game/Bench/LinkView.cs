using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Motes of light travelling between neighbouring tiles whose reaction is already in the journal.</summary>
    public sealed class LinkView : MonoBehaviour
    {
        const int PerLink = 9;
        /// <summary>The colour of the motes (past 1 for glow), how fast they travel and how high they arc.</summary>
        public Color color = new Color(2.2f, 1.7f, 0.8f);
        public float speed = 0.35f, arc = 7, alpha = 0.9f;
        readonly List<(Vector3 a, Vector3 b)> links = new List<(Vector3, Vector3)>();
        readonly List<SpriteRenderer> motes = new List<SpriteRenderer>();

        public void Set(List<(Vector3 a, Vector3 b)> pairs)
        {
            links.Clear(); links.AddRange(pairs);
            while (motes.Count < links.Count * PerLink)
            {
                var m = Make.Sprite(transform, "dot", Vector3.zero, 20, Art.Lib.glow);
                m.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                motes.Add(m);
            }
            for (int i = 0; i < motes.Count; i++) motes[i].enabled = i < links.Count * PerLink;
        }

        void Update()
        {
            float t = Time.time;
            for (int l = 0; l < links.Count; l++)
                for (int k = 0; k < PerLink; k++)
                {
                    float f = (k / (float)PerLink + t * speed) % 1, s = Mathf.Sin(f * Mathf.PI);
                    var p = Vector3.Lerp(links[l].a, links[l].b, f) + new Vector3(0, 9 + s * arc, 0);
                    var m = motes[l * PerLink + k];
                    m.transform.position = Px.Snap(p);
                    m.color = new Color(color.r, color.g, color.b, alpha * s);
                }
        }
    }
}
