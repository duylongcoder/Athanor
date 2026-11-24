using System;
using Athanor.Gameplay;
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

        /// <summary>Where the lower buttons sit (art px, y down): the dialog's top edge, the buttons' left edge and tops below the dialog's top, the hint's centre and top, and how far they move up without Vibration.</summary>
        [Serializable]
        public struct Layout { public float dialogTop, buttonX, howToDy, quitDy, hintX, hintDy, noVibrationUp; }
        [SerializeField] Layout layout;
        [SerializeField] Transform menu, confirm;
        [SerializeField] UiButton resume, vibration, howTo, quit, quitYes, quitNo;
        [SerializeField] Label howToHint;
        [SerializeField] Clickable outside;
        Settings settings;
        bool wired;

        /// <summary>Takes the player's settings (Vibration writes to them) and hooks the clicks. GameFlow calls it once at start.</summary>
        public void Bind(Settings playerSettings)
        {
            settings = playerSettings;
            if (wired) return;
            wired = true;
            outside.onClick = Close;                        // a click outside resumes
            resume.Bind(); vibration.Bind(); howTo.Bind(); quit.Bind(); quitYes.Bind(); quitNo.Bind();   // their Awake runs only when the menu first opens
            resume.Clicked += Close;
            vibration.Clicked += ToggleVibration;
            quit.Clicked += AskQuit;
            quitNo.Clicked += BackToMenu;
            quitYes.Clicked += ConfirmQuit;
        }

        /// <summary>Lays the buttons out: without Vibration the rest move up 30 px.</summary>
        void PlaceButtons()
        {
            bool vib = Haptics.Supported;
            vibration.gameObject.SetActive(vib);
            if (vib) vibration.Text.SetKey("ui.pause.vibration", ("state", Loc.T(settings.haptics ? "ui.settings.on" : "ui.settings.off")));
            float up = vib ? 0 : layout.noVibrationUp;
            howTo.transform.position = Px.Mid(layout.buttonX, layout.dialogTop + layout.howToDy - up, UiButton.W, UiButton.H);
            howToHint.transform.position = Px.At(layout.hintX, layout.dialogTop + layout.hintDy - up);
            quit.transform.position = Px.Mid(layout.buttonX, layout.dialogTop + layout.quitDy - up, UiButton.W, UiButton.H);
        }

        /// <summary>Opens the menu and stops time. False when a pause is not allowed now or one is already open.</summary>
        public bool Open()
        {
            if (IsOpen || (CanOpen != null && !CanOpen())) return false;
            PlaceButtons();
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
            if (Confirming) BackToMenu();
            else if (IsOpen) Close();
            else Open();
        }

        void AskQuit() { menu.gameObject.SetActive(false); confirm.gameObject.SetActive(true); }

        void BackToMenu() { confirm.gameObject.SetActive(false); PlaceButtons(); menu.gameObject.SetActive(true); }

        void ToggleVibration()
        {
            settings.haptics = !settings.haptics;
            Haptics.Enabled = settings.haptics;
            if (settings.haptics) Haptics.Pulse(Haptics.Discovery);
            PlaceButtons();
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
        void OnDestroy()
        {
            if (Time.timeScale == 0f && menu != null) Time.timeScale = 1f;
            if (!wired) return;
            resume.Clicked -= Close;
            vibration.Clicked -= ToggleVibration;
            quit.Clicked -= AskQuit;
            quitNo.Clicked -= BackToMenu;
            quitYes.Clicked -= ConfirmQuit;
        }
    }
}
