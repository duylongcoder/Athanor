using System;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The title, shown over the candlelit workshop.</summary>
    public sealed class TitleView : MonoBehaviour
    {
        const int O = 420;
        public bool BeginPressed { get; private set; }
        /// <summary>How to play or Watch intro was pressed (GameFlow reads them while the title waits).</summary>
        public bool HowToPressed { get; private set; }
        public bool IntroPressed { get; private set; }
        /// <summary>Forgets a Begin that was answered by the skip question.</summary>
        public void ClearBegin() => BeginPressed = false;
        public event Action JournalClicked, GoalsClicked, ResetConfirmed, SettingsClosed;
        /// <summary>The Settings panel or the "erase progress?" question is up: the buttons under it do nothing.</summary>
        public bool Asking => (confirm != null && confirm.gameObject.activeSelf) || (skip != null && skip.gameObject.activeSelf) || (panel != null && panel.gameObject.activeSelf);

        Label logo, sub, progress, credit;
        Transform confirm, panel, skip;
        Action<bool> skipAnswer;
        Settings settings;
        // Settings panel parts
        Label langValue, vibValue, swayLabel, swayValue, resetLabel; SpriteRenderer divider2, swayBox, swayFill; SpriteRenderer vibFill, vibBox;
        readonly SpriteRenderer[] cellEdge = new SpriteRenderer[3], cellFill = new SpriteRenderer[3];
        readonly bool[] cellOver = new bool[3];
        readonly Label[] outline = new Label[4];
        static readonly Vector3[] Edge = { new Vector3(-2, 0, 0), new Vector3(2, 0, 0), new Vector3(0, 2, 0), new Vector3(0, -2, 0) };
        SpriteRenderer glow, shade;
        float age;

        /// <summary>settings: the player's settings (the title edits them and raises SettingsClosed when the panel closes).</summary>
        public void Build(Settings playerSettings)
        {
            settings = playerSettings;
            var t = transform;
            shade = Make.Rect(t, 0, 0, Px.W, Px.H, new Color(0, 0, 0, 0.56f), O);
            glow = Make.Glow(t, Px.At(320, 146), 150, Color.clear, O + 1);
            glow.transform.localScale = new Vector3(330 / 32f, 70 / 32f, 1);
            // the logo: a dark outline (four offset copies), then the face with a candle-flame gradient
            var edge = Px.Hex("#2a0c14");
            for (int k = 0; k < outline.Length; k++)
            {
                outline[k] = Make.TextKey(t, "ui.title.logo", 320, 96, FontKind.Logo, 48, edge, O + 3, Align.Centre);
                outline[k].transform.localScale = new Vector3(2, 2, 1);
            }
            logo = Make.TextKey(t, "ui.title.logo", 320, 96, FontKind.Logo, 48, Color.white, O + 5, Align.Centre, Px.Hex("#12060a"));
            logo.transform.localScale = new Vector3(2, 2, 1);
            logo.shadow.transform.localPosition = new Vector3(1.5f, -2, 0);
            logo.main.enableVertexGradient = true;
            logo.main.colorGradient = new TMPro.VertexGradient(Px.Hex("#fffbe0"), Px.Hex("#fffbe0"), Px.Hex("#f08a2c"), Px.Hex("#f08a2c"));
            sub = Make.TextKey(t, "ui.title.subtitle", 320, 196, FontKind.Title, 16, Px.Hex("#f8d250"), O + 4, Align.Centre, Px.Hex("#0c0706"));
            // (tools/web/webtest.mjs clicks these buttons by position)
            UiButton.Create(t, "ui.title.begin", 260, 214, O + 4, () => { if (!Asking) BeginPressed = true; });
            UiButton.Create(t, "ui.title.journal", 260, 244, O + 4, () => JournalClicked?.Invoke());
            UiButton.Create(t, "ui.title.goals", 260, 274, O + 4, () => GoalsClicked?.Invoke());
            UiButton.Create(t, "ui.title.how_to_play", 130, 304, O + 4, () => { if (!Asking) HowToPressed = true; });
            UiButton.Create(t, "ui.title.settings", 260, 304, O + 4, OpenSettings);
            UiButton.Create(t, "ui.title.watch_intro", 390, 304, O + 4, () => { if (!Asking) IntroPressed = true; });
            progress = Make.Text(t, "", 320, ProgressY, FontKind.Tiny, 8, Px.Hex("#c8ac84"), O + 4, Align.Centre, Px.Hex("#0c0706"));

            // "erase progress?": a dialog over everything else on the title
            const int C = O + 60, X = 160, Y = 75;
            confirm = Make.Root("Confirm", t);
            var block = Make.Rect(confirm, 0, 0, Px.W, Px.H, new Color(0, 0, 0, 0.7f), C - 1);
            Make.Click(block.gameObject, new Vector2(Px.W, Px.H), C - 1);
            Make.SpriteAt(confirm, "dialog", X, Y, C);
            Make.TextKey(confirm, "ui.title.erase_title", 320, Y + 20, FontKind.Title, 32, Px.Hex("#ee6a3c"), C + 2, Align.Centre, Px.Hex("#3a1a08"));
            Make.TextKey(confirm, "ui.title.erase_body", 320, Y + 70, FontKind.Body, 16, Px.Hex("#f0dcc0"), C + 2, Align.Centre);
            Make.TextKey(confirm, "ui.title.erase_keep", 320, Y + 86, FontKind.Body, 16, Px.Hex("#a88a6a"), C + 2, Align.Centre);
            UiButton.Create(confirm, "ui.common.cancel", X + 28, Y + 150, C + 2, () => confirm.gameObject.SetActive(false));
            UiButton.Create(confirm, "ui.title.erase", X + 172, Y + 150, C + 2, () => { confirm.gameObject.SetActive(false); ResetConfirmed?.Invoke(); });
            confirm.gameObject.SetActive(false);
            // "Skip the tutorial?": asked once, on Begin, when the journal has entries and the tutorial was never finished
            skip = Make.Root("Skip", t);
            var sblock = Make.Rect(skip, 0, 0, Px.W, Px.H, new Color(0, 0, 0, 0.7f), C - 1);
            Make.Click(sblock.gameObject, new Vector2(Px.W, Px.H), C - 1);
            Make.SpriteAt(skip, "dialog", X, Y, C);
            Make.TextKey(skip, "ui.title.skip_prompt_title", 320, Y + 20, FontKind.Title, 32, Px.Hex("#f8d250"), C + 2, Align.Centre, Px.Hex("#3a1a08"));
            Make.TextKey(skip, "ui.title.skip_prompt_body1", 320, Y + 70, FontKind.Body, 16, Px.Hex("#f0dcc0"), C + 2, Align.Centre);
            Make.TextKey(skip, "ui.title.skip_prompt_body2", 320, Y + 86, FontKind.Body, 16, Px.Hex("#a88a6a"), C + 2, Align.Centre);
            UiButton.Create(skip, "ui.title.skip_tutorial", X + 28, Y + 150, C + 2, () => AnswerSkip(false));
            UiButton.Create(skip, "ui.title.skip_prompt_play", X + 172, Y + 150, C + 2, () => AnswerSkip(true));
            skip.gameObject.SetActive(false);
            credit = Make.TextKey(t, "ui.title.fonts", 320, CreditY, FontKind.Tiny, 8, Px.Hex("#7a5a3a"), O + 4, Align.Centre, Px.Hex("#0c0706"));
            BuildSettings(t);
            Loc.Changed += Relayout;
            gameObject.SetActive(false);
        }

        void OnDestroy() => Loc.Changed -= Relayout;

        /// <summary>Asks "Skip the tutorial?"; answer(true) = play it, answer(false) = skip.</summary>
        public void AskSkip(Action<bool> answer) { skipAnswer = answer; skip.gameObject.SetActive(true); }

        void AnswerSkip(bool play) { skip.gameObject.SetActive(false); var a = skipAnswer; skipAnswer = null; a?.Invoke(play); }

        // Vietnamese rows are 12 px, so the lines under the buttons sit 2 px higher (the 360 px edge leaves 2 px).
        static int ProgressY => Loc.Vi ? 332 : 334;
        static int CreditY => Loc.Vi ? 346 : 348;

        /// <summary>The language changed: move what depends on it and rewrite the Settings values.</summary>
        void Relayout()
        {
            progress.transform.position = Px.At(320, ProgressY);
            credit.transform.position = Px.At(320, CreditY);
            RefreshSettings();
        }

        // ---- Settings panel ----------------------------------------------------------------------

        const int P = O + 20, PX = 160, PY = 75, CellX = 300, CellW = 150, CellH = 26;

        void BuildSettings(Transform t)
        {
            panel = Make.Root("Settings", t);
            var block = Make.Rect(panel, 0, 0, Px.W, Px.H, new Color(0, 0, 0, 0.7f), P - 1);
            Make.Click(block.gameObject, new Vector2(Px.W, Px.H), P - 1, CloseSettings);    // a click outside closes
            Make.SpriteAt(panel, "dialog", PX, PY, P);
            var body = Make.Root("dialog hit", panel); body.position = Px.Mid(PX, PY, 320, 210);
            Make.Click(body.gameObject, new Vector2(320, 210), P);                          // a click inside does not
            Make.TextKey(panel, "ui.settings.title", 320, PY + 12, FontKind.Title, 32, Px.Hex("#f8d250"), P + 2, Align.Centre, Px.Hex("#3a1a08"));
            Make.Rect(panel, PX + 24, PY + 50, 272, 1, Px.Hex("#8a5a1e"), P + 2);
            divider2 = Make.Rect(panel, PX + 24, PY + 126, 272, 1, Px.Hex("#8a5a1e"), P + 2);

            // rows: Vibration (only where the device can), Language. Their y is set in RefreshSettings.
            vibLabel = Make.TextKey(panel, "ui.settings.vibration", 188, 0, FontKind.Body, 16, Px.Hex("#f0dcc0"), P + 2);
            langLabel = Make.TextKey(panel, "ui.settings.language", 188, 0, FontKind.Body, 16, Px.Hex("#f0dcc0"), P + 2);
            MakeCell(0, "vibration", ToggleVibration);
            MakeCell(1, "language", ToggleLanguage);
            swayLabel = Make.TextKey(panel, "ui.settings.sway", 188, 0, FontKind.Body, 16, Px.Hex("#f0dcc0"), P + 2);
            MakeCell(2, "sway", ToggleSway);
            swayBox = Make.Rect(panel, 0, 0, 10, 10, Px.Hex("#8a5a1e"), P + 5);
            swayFill = Make.Rect(panel, 0, 0, 8, 8, Px.Hex("#f8d250"), P + 6);
            swayValue = Make.TextKey(panel, "ui.settings.on", 0, 0, FontKind.Title, 16, Px.Hex("#fff6b8"), P + 6, Align.Left, Px.Hex("#3b0d1e"));
            vibBox = Make.Rect(panel, 0, 0, 10, 10, Px.Hex("#8a5a1e"), P + 5);
            vibFill = Make.Rect(panel, 0, 0, 8, 8, Px.Hex("#f8d250"), P + 6);
            vibValue = Make.TextKey(panel, "ui.settings.on", 0, 0, FontKind.Title, 16, Px.Hex("#fff6b8"), P + 6, Align.Left, Px.Hex("#3b0d1e"));
            langValue = Make.Text(panel, "", 0, 0, FontKind.Title, 16, Px.Hex("#fff6b8"), P + 6, Align.Centre, Px.Hex("#3b0d1e"));

            var reset = resetLabel = Make.TextKey(panel, "ui.title.reset", 320, PY + 134, FontKind.Tiny, 8, Px.Hex("#7a5a3a"), P + 2, Align.Centre, Px.Hex("#0c0706"));
            var rc = Make.Click(reset.gameObject, new Vector2(120, 22), P + 3, () => confirm.gameObject.SetActive(true));
            rc.offset = new Vector2(0, -4);
            rc.onEnter = () => reset.Color = Px.Hex("#ee6a3c"); rc.onExit = () => reset.Color = Px.Hex("#7a5a3a");
            UiButton.Create(panel, "ui.common.back", 260, 231, P + 3, CloseSettings);
            panel.gameObject.SetActive(false);
        }

        Label vibLabel, langLabel;
        readonly Transform[] cell = new Transform[3];

        /// <summary>A 150x26 value button: fill #2a1810 in a #8a5a1e border, gold border and lighter fill under the pointer.</summary>
        void MakeCell(int i, string name, Action onClick)
        {
            var go = Make.Root("cell " + name, panel); cell[i] = go;
            cellEdge[i] = Make.Rect(go, 0, 0, CellW, CellH, Px.Hex("#8a5a1e"), P + 3);
            cellFill[i] = Make.Rect(go, 1, 1, CellW - 2, CellH - 2, Px.Hex("#2a1810"), P + 4);
            go.position = Vector3.zero;
            var hit = Make.Root("hit", go);
            var c = Make.Click(hit.gameObject, new Vector2(CellW, CellH), P + 5, onClick);
            c.onEnter = () => { cellOver[i] = true; PaintCell(i); };
            c.onExit = () => { cellOver[i] = false; PaintCell(i); };
            cellHit[i] = hit;
        }
        readonly Transform[] cellHit = new Transform[3];

        void PaintCell(int i)
        {
            cellEdge[i].color = cellOver[i] ? Px.Hex("#f8d250") : Px.Hex("#8a5a1e");
            cellFill[i].color = cellOver[i] ? Px.Hex("#4a2c14") : Px.Hex("#2a1810");
        }

        void PlaceCell(int i, float rowY)
        {
            cellEdge[i].transform.position = Px.Mid(CellX, rowY, CellW, CellH);
            cellFill[i].transform.position = Px.Mid(CellX + 1, rowY + 1, CellW - 2, CellH - 2);
            cellHit[i].position = Px.Mid(CellX, rowY, CellW, CellH);
        }

        /// <summary>Lays out the rows (Camera sway, Vibration where the device can, Language) and writes the values.</summary>
        void RefreshSettings()
        {
            if (panel == null) return;
            bool vib = Haptics.Supported;
            float pitch = vib ? 28 : 32, row = PY + (vib ? 54 : 58);
            // the third row pushes the lower divider and the erase link down
            divider2.transform.position = Px.Mid(PX + 24, PY + (vib ? 140 : 126), 272, 1);
            resetLabel.transform.position = Px.At(320, PY + (vib ? 144 : 134));
            PlaceRow(2, row, swayLabel, swayBox, swayFill, swayValue, settings.sway);
            row += pitch;
            cell[0].gameObject.SetActive(vib); vibLabel.gameObject.SetActive(vib); vibBox.gameObject.SetActive(vib);
            vibFill.gameObject.SetActive(vib && settings.haptics); vibValue.gameObject.SetActive(vib);
            if (vib)
            {
                PlaceRow(0, row, vibLabel, vibBox, vibFill, vibValue, settings.haptics);
                row += pitch;
            }
            langLabel.transform.position = Px.At(188, row + 4);
            PlaceCell(1, row);
            langValue.transform.position = Px.At(CellX + CellW / 2f, row + 3);
            langValue.Text = Loc.NativeName;
            for (int i = 0; i < 3; i++) PaintCell(i);
        }

        /// <summary>One on/off row: label, cell, check box and its On or Off word.</summary>
        void PlaceRow(int i, float row, Label label, SpriteRenderer box, SpriteRenderer fill, Label value, bool on)
        {
            label.transform.position = Px.At(188, row + 4);
            PlaceCell(i, row);
            box.transform.position = Px.Mid(CellX + 8, row + 8, 10, 10);
            fill.transform.position = Px.Mid(CellX + 9, row + 9, 8, 8);
            fill.gameObject.SetActive(on);
            value.transform.position = Px.At(CellX + 26, row + 3);
            value.SetKey(on ? "ui.settings.on" : "ui.settings.off");
        }

        void ToggleSway() { settings.sway = !settings.sway; RefreshSettings(); }

        void OpenSettings() { RefreshSettings(); panel.gameObject.SetActive(true); }

        void CloseSettings()
        {
            if (confirm.gameObject.activeSelf) return;
            panel.gameObject.SetActive(false);
            SettingsClosed?.Invoke();
        }

        void ToggleVibration()
        {
            settings.haptics = !settings.haptics;
            Haptics.Enabled = settings.haptics;
            if (settings.haptics) Haptics.Pulse(Haptics.Discovery);      // the toggle pulses once when turned on
            RefreshSettings();
        }

        /// <summary>A tap anywhere on the value: English and Vietnamese take turns. Applies at once.</summary>
        void ToggleLanguage()
        {
            string next = Loc.Vi ? Loc.English : Loc.Vietnamese;
            settings.language = next;
            Dev.Hook("[loc] " + next);
            Loc.Set(next);                  // Relayout (a Loc.Changed handler) refreshes the panel
        }

        public void Open(int known, int total, int goalsDone, int goals)
        {
            BeginPressed = HowToPressed = IntroPressed = false; age = 0;
            skip.gameObject.SetActive(false);
            confirm.gameObject.SetActive(false); panel.gameObject.SetActive(false);
            Progress(known, total, goalsDone, goals);
            gameObject.SetActive(true);
            Update();
        }

        /// <summary>The line under the buttons: how much of the journal and of the goals is done.</summary>
        public void Progress(int known, int total, int goalsDone, int goals)
        {
            // bound to its key, so a language switch rewrites it after the font swap (an unbound label kept Vietnamese text under the English font)
            if (known > 0 || goalsDone > 0) progress.SetKey("ui.title.progress", ("known", known), ("total", total), ("done", goalsDone), ("goals", goals));
            else { progress.Bind(null); progress.Text = ""; }
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
