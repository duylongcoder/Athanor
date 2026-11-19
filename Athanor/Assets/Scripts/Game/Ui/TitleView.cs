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

        [SerializeField] Label logo, sub, progress, credit;
        [SerializeField] Transform confirm, panel, skip;
        Action<bool> skipAnswer;
        Settings settings;
        bool wired;
        // the six buttons under the logo (tools/web/webtest.mjs clicks them by position), then the two dialogs' buttons
        [SerializeField] UiButton begin, journal, goals, howTo, settingsButton, watchIntro;
        [SerializeField] UiButton eraseNo, eraseYes, skipTutorial, skipPlay, back;
        // Settings panel parts
        [SerializeField] Label langValue, vibValue, swayLabel, swayValue, resetLabel, vibLabel, langLabel;
        [SerializeField] SpriteRenderer divider2, swayBox, swayFill, vibFill, vibBox;
        [SerializeField] SpriteRenderer[] cellEdge = new SpriteRenderer[3], cellFill = new SpriteRenderer[3];
        [SerializeField] Transform[] cell = new Transform[3];
        [SerializeField] Clickable[] cellHit = new Clickable[3];       // 0 Vibration, 1 Language, 2 Sway
        [SerializeField] Clickable settingsOutside, resetLink;
        readonly bool[] cellOver = new bool[3];
        [SerializeField] Label[] outline = new Label[4];
        static readonly Vector3[] Edge = { new Vector3(-2, 0, 0), new Vector3(2, 0, 0), new Vector3(0, 2, 0), new Vector3(0, -2, 0) };
        [SerializeField] SpriteRenderer glow, shade;
        float age;

        FxRefs fx;

        /// <summary>Takes the player's settings (the title edits them and raises SettingsClosed when the panel closes) and hooks every
        /// click. GameFlow calls it once at start.</summary>
        public void Bind(Settings playerSettings, FxRefs effects)
        {
            if (effects == null) throw new System.ArgumentNullException(nameof(effects));
            fx = effects;
            settings = playerSettings;
            if (wired) return;
            wired = true;
            // their Awake runs only when a hidden dialog first opens
            begin.Bind(); journal.Bind(); goals.Bind(); howTo.Bind(); settingsButton.Bind(); watchIntro.Bind();
            eraseNo.Bind(); eraseYes.Bind(); skipTutorial.Bind(); skipPlay.Bind(); back.Bind();
            begin.Clicked += PressBegin;
            journal.Clicked += PressJournal;
            goals.Clicked += PressGoals;
            howTo.Clicked += PressHowTo;
            settingsButton.Clicked += OpenSettings;
            watchIntro.Clicked += PressIntro;
            eraseNo.Clicked += CancelErase;
            eraseYes.Clicked += ConfirmErase;
            skipTutorial.Clicked += SkipTutorial;
            skipPlay.Clicked += PlayTutorial;
            back.Clicked += CloseSettings;
            settingsOutside.onClick = CloseSettings;                        // a click outside closes
            cellHit[0].onClick = ToggleVibration;
            cellHit[1].onClick = ToggleLanguage;
            cellHit[2].onClick = ToggleSway;
            for (int i = 0; i < cellHit.Length; i++)
            {
                int k = i;
                cellHit[k].onEnter = () => { cellOver[k] = true; PaintCell(k); };
                cellHit[k].onExit = () => { cellOver[k] = false; PaintCell(k); };
            }
            resetLink.onClick = AskErase;
            resetLink.onEnter = () => resetLabel.Color = Px.Hex("#ee6a3c");
            resetLink.onExit = () => resetLabel.Color = Px.Hex("#7a5a3a");
            Loc.Changed += Relayout;
        }

        void OnDestroy()
        {
            Loc.Changed -= Relayout;
            if (!wired) return;
            begin.Clicked -= PressBegin;
            journal.Clicked -= PressJournal;
            goals.Clicked -= PressGoals;
            howTo.Clicked -= PressHowTo;
            settingsButton.Clicked -= OpenSettings;
            watchIntro.Clicked -= PressIntro;
            eraseNo.Clicked -= CancelErase;
            eraseYes.Clicked -= ConfirmErase;
            skipTutorial.Clicked -= SkipTutorial;
            skipPlay.Clicked -= PlayTutorial;
            back.Clicked -= CloseSettings;
        }

        void PressBegin() { if (!Asking) BeginPressed = true; }
        void PressJournal() => JournalClicked?.Invoke();
        void PressGoals() => GoalsClicked?.Invoke();
        void PressHowTo() { if (!Asking) HowToPressed = true; }
        void PressIntro() { if (!Asking) IntroPressed = true; }
        void AskErase() => confirm.gameObject.SetActive(true);
        void CancelErase() => confirm.gameObject.SetActive(false);
        void ConfirmErase() { confirm.gameObject.SetActive(false); ResetConfirmed?.Invoke(); }
        void SkipTutorial() => AnswerSkip(false);
        void PlayTutorial() => AnswerSkip(true);

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

        void PaintCell(int i)
        {
            cellEdge[i].color = cellOver[i] ? Art.Palette.gold : Art.Palette.edge;
            cellFill[i].color = cellOver[i] ? Px.Hex("#4a2c14") : Px.Hex("#2a1810");
        }

        void PlaceCell(int i, float rowY)
        {
            cellEdge[i].transform.position = Px.Mid(CellX, rowY, CellW, CellH);
            cellFill[i].transform.position = Px.Mid(CellX + 1, rowY + 1, CellW - 2, CellH - 2);
            cellHit[i].transform.position = Px.Mid(CellX, rowY, CellW, CellH);
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
            if (fx != null && UnityEngine.Random.value < 9 * Time.deltaTime)
                fx.Sparks.Emit(Px.At(UnityEngine.Random.Range(120f, 520f), 300), new Vector2(UnityEngine.Random.Range(-6f, 6f), UnityEngine.Random.Range(18f, 42f)),
                    new Color(2.2f, 1.2f, 0.4f, 0.9f), 4f, O + 2, -2, UnityEngine.Random.value < 0.3f);
        }
    }
}
