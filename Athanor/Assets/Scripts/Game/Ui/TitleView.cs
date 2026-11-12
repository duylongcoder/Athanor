using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The title, shown over the candlelit workshop.</summary>
    public sealed class TitleView : MonoBehaviour
    {
        const int O = 420;
        public bool BeginPressed { get; private set; }
        public event Action JournalClicked;

        Label logo, sub, progress;
        readonly Label[] outline = new Label[4];
        static readonly Vector3[] Edge = { new Vector3(-2, 0, 0), new Vector3(2, 0, 0), new Vector3(0, 2, 0), new Vector3(0, -2, 0) };
        SpriteRenderer glow, shade;
        float age;

        public void Build()
        {
            var t = transform;
            shade = Make.Rect(t, 0, 0, Px.W, Px.H, new Color(0, 0, 0, 0.56f), O);
            glow = Make.Glow(t, Px.At(320, 146), 150, Color.clear, O + 1);
            glow.transform.localScale = new Vector3(330 / 32f, 70 / 32f, 1);
            // the logo: a dark outline (four offset copies), then the face with a candle-flame gradient
            var edge = Px.Hex("#2a0c14");
            for (int k = 0; k < outline.Length; k++)
            {
                outline[k] = Make.Text(t, "Athanor", 320, 96, FontKind.Title, 48, edge, O + 3, Align.Centre);
                outline[k].transform.localScale = new Vector3(2, 2, 1);
            }
            logo = Make.Text(t, "Athanor", 320, 96, FontKind.Title, 48, Color.white, O + 5, Align.Centre, Px.Hex("#12060a"));
            logo.transform.localScale = new Vector3(2, 2, 1);
            logo.shadow.transform.localPosition = new Vector3(1.5f, -2, 0);
            logo.main.enableVertexGradient = true;
            logo.main.colorGradient = new TMPro.VertexGradient(Px.Hex("#fffbe0"), Px.Hex("#fffbe0"), Px.Hex("#f08a2c"), Px.Hex("#f08a2c"));
            sub = Make.Text(t, "An alchemist's descent", 320, 196, FontKind.Title, 16, Px.Hex("#f8d250"), O + 4, Align.Centre, Px.Hex("#0c0706"));
            UiButton.Create(t, "Begin", 260, 226, O + 4, () => BeginPressed = true);
            UiButton.Create(t, "Journal", 260, 258, O + 4, () => JournalClicked?.Invoke());
            progress = Make.Text(t, "", 320, 292, FontKind.Tiny, 8, Px.Hex("#c8ac84"), O + 4, Align.Centre, Px.Hex("#0c0706"));
            Make.Text(t, "FONTS: ALAGARD, BITPOTION, PIXEL OPERATOR", 320, 345, FontKind.Tiny, 8, Px.Hex("#7a5a3a"), O + 4, Align.Centre, Px.Hex("#0c0706"));
            gameObject.SetActive(false);
        }

        public void Open(int known, int total)
        {
            BeginPressed = false; age = 0;
            progress.Text = known > 0 ? "JOURNAL: " + known + " OF " + total + " REACTIONS FOUND" : "";
            gameObject.SetActive(true);
            Update();
        }

        public void Close() => gameObject.SetActive(false);

        void Update()
        {
            age += Time.deltaTime;
            float k = Mathf.Clamp01(age / 1.2f), pulse = 0.5f + 0.5f * Mathf.Sin(Time.time * 1.7f);
            logo.Alpha = k; sub.Alpha = Mathf.Clamp01((age - 0.6f) / 0.8f);
            var at = Px.At(320, 96 + Mathf.Round((1 - Anim.OutCubic(k)) * 10));
            logo.transform.position = at;
            for (int i = 0; i < outline.Length; i++) { outline[i].transform.position = at + Edge[i]; outline[i].Alpha = k; }
            glow.color = new Color(1f, 0.55f, 0.2f, k * (0.28f + 0.12f * pulse));
            // embers drifting up past the title
            if (Sparks.I != null && UnityEngine.Random.value < 9 * Time.deltaTime)
                Sparks.I.Emit(Px.At(UnityEngine.Random.Range(120f, 520f), 300), new Vector2(UnityEngine.Random.Range(-6f, 6f), UnityEngine.Random.Range(18f, 42f)),
                    new Color(2.2f, 1.2f, 0.4f, 0.9f), 4f, O + 2, -2, UnityEngine.Random.value < 0.3f);
        }
    }
}
