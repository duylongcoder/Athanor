using System;
using Athanor.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Athanor.Game
{
    /// <summary>
    /// The in-run pause: Resume, Vibration (only where the device can), How to play (greyed: title only) and Quit to title with a
    /// confirm. Time.timeScale is 0 while it is open; it is put back on every way out (Resume, Esc, a click outside, Quit, disable,
    /// destroy). Its own animation uses nothing that reads scaled time, and it never touches AudioListener (AudioMute owns that).
    /// </summary>
    public sealed class PauseMenu : MonoBehaviour
    {
        const int P = 800, X = 160, Y = 75;
        /// <summary>The player confirmed "Quit this run?". The owner discards the run; the menu has already restored time.</summary>
        public event Action QuitConfirmed;
        /// <summary>Raised when the Vibration button changes the setting, so the owner can save it.</summary>
        public event Action VibrationChanged;
        /// <summary>Raised when the menu opens and when it closes, so the owner can lock and unlock the bench under it.</summary>
        public event Action Opened, Closed;
        /// <summary>Whether a pause may open now (the bench is the player's, no cutscene; the tutorial can be paused and left). Null means yes.</summary>
        public Func<bool> CanOpen;

        public bool IsOpen => menu != null && (menu.gameObject.activeSelf || confirm.gameObject.activeSelf);
        public bool Confirming => confirm != null && confirm.gameObject.activeSelf;

        Settings settings;
        Transform menu, confirm;
        UiButton resume, vibration, howTo, quit;
        Label howToHint;

        /// <summary>The state of a pause: may it open, given the flow state and what is playing.</summary>
        public static bool Allowed(FlowState state, bool benchInput, bool inCutscene) =>
            state == FlowState.Bench && benchInput && !inCutscene;

        public void Build(Settings playerSettings)
        {
            settings = playerSettings;
            menu = Make.Root("Pause menu", transform);
            var block = Make.Rect(menu, 0, 0, Px.W, Px.H, new Color(0, 0, 0, 0.7f), P - 1);
            Make.Click(block.gameObject, new Vector2(Px.W, Px.H), P - 1, Close);                  // a click outside resumes
            Make.SpriteAt(menu, "dialog", X, Y, P);
            var body = Make.Root("dialog hit", menu); body.position = Px.Mid(X, Y, 320, 210);
            Make.Click(body.gameObject, new Vector2(320, 210), P);
            Make.TextKey(menu, "ui.pause.title", 320, Y + 10, FontKind.Title, 32, Px.Hex("#f8d250"), P + 2, Align.Centre, Px.Hex("#3a1a08"));
            resume = UiButton.Create(menu, "ui.pause.resume", 260, Y + 50, P + 2, Close);
            vibration = UiButton.Create(menu, "ui.pause.vibration", 260, Y + 80, P + 2, ToggleVibration);
            howTo = UiButton.Create(menu, "ui.title.how_to_play", 260, Y + 110, P + 2, null);
            howTo.Disabled = true;
            howToHint = Make.TextKey(menu, "ui.pause.how_to_play_locked", 320, Y + 137, FontKind.Tiny, 8, Px.Hex("#c8ac84"), P + 2, Align.Centre, Px.Hex("#0c0706"));
            quit = UiButton.Create(menu, "ui.pause.quit", 260, Y + 152, P + 2, () => { menu.gameObject.SetActive(false); confirm.gameObject.SetActive(true); });

            confirm = Make.Root("Quit confirm", transform);
            var cblock = Make.Rect(confirm, 0, 0, Px.W, Px.H, new Color(0, 0, 0, 0.7f), P - 1);
            Make.Click(cblock.gameObject, new Vector2(Px.W, Px.H), P - 1);
            Make.SpriteAt(confirm, "dialog", X, Y, P);
            Make.TextKey(confirm, "ui.pause.quit_title", 320, Y + 18, FontKind.Title, 32, Px.Hex("#ee6a3c"), P + 2, Align.Centre, Px.Hex("#3a1a08"));
            Make.TextKey(confirm, "ui.pause.quit_body1", 320, Y + 70, FontKind.Body, 16, Px.Hex("#f0dcc0"), P + 2, Align.Centre);
            Make.TextKey(confirm, "ui.pause.quit_body2", 320, Y + 88, FontKind.Body, 16, Px.Hex("#a88a6a"), P + 2, Align.Centre);
            UiButton.Create(confirm, "ui.common.cancel", 188, Y + 150, P + 2, () => { confirm.gameObject.SetActive(false); Layout(); menu.gameObject.SetActive(true); });
            UiButton.Create(confirm, "ui.pause.quit_confirm", 332, Y + 150, P + 2, ConfirmQuit);
            menu.gameObject.SetActive(false); confirm.gameObject.SetActive(false);
        }

        /// <summary>Lays the buttons out: without Vibration the rest move up 30 px.</summary>
        void Layout()
        {
            bool vib = Haptics.Supported;
            vibration.gameObject.SetActive(vib);
            if (vib) vibration.Text.SetKey("ui.pause.vibration", ("state", Loc.T(settings.haptics ? "ui.settings.on" : "ui.settings.off")));
            int up = vib ? 0 : 30;
            howTo.transform.position = Px.Mid(260, Y + 110 - up, UiButton.W, UiButton.H);
            howToHint.transform.position = Px.At(320, Y + 137 - up);
            quit.transform.position = Px.Mid(260, Y + 152 - up, UiButton.W, UiButton.H);
        }

        /// <summary>Opens the menu and stops time. False when a pause is not allowed now or one is already open.</summary>
        public bool Open()
        {
            if (IsOpen || (CanOpen != null && !CanOpen())) return false;
            Layout();
            menu.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Opened?.Invoke();
            return true;
        }

        /// <summary>Closes whatever is up and puts time back.</summary>
        public void Close()
        {
            if (menu == null) return;
            bool was = IsOpen;
            menu.gameObject.SetActive(false); confirm.gameObject.SetActive(false);
            Time.timeScale = 1f;
            if (was) Closed?.Invoke();
        }

        /// <summary>Esc: opens; closes the menu; or backs out of the quit question to the menu.</summary>
        public void Toggle()
        {
            if (Confirming) { confirm.gameObject.SetActive(false); Layout(); menu.gameObject.SetActive(true); }
            else if (IsOpen) Close();
            else Open();
        }

        void ToggleVibration()
        {
            settings.haptics = !settings.haptics;
            Haptics.Enabled = settings.haptics;
            if (settings.haptics) Haptics.Pulse(Haptics.Discovery);
            Layout();
            VibrationChanged?.Invoke();
        }

        void ConfirmQuit()
        {
            Close();
            QuitConfirmed?.Invoke();
        }

        void Update()
        {
            var kb = Keyboard.current;
            if (kb != null && kb.escapeKey.wasPressedThisFrame) Toggle();
        }

        void OnDisable() { if (IsOpen) Close(); else if (Time.timeScale == 0f) Time.timeScale = 1f; }
        void OnDestroy() { if (Time.timeScale == 0f && menu != null) Time.timeScale = 1f; }
    }
}
