using System;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The panels round the bench: depth and potency, catalysts, trials and redraws, the last-reaction note, journal and Ignite.</summary>
    public sealed class HudView : MonoBehaviour
    {
        public event Action IgniteClicked, JournalClicked, RedrawClicked, PauseClicked;
        /// <summary>Catalyst socket index the pointer is over, or -1.</summary>
        public int HoveredCatalyst { get; private set; } = -1;
        public bool RedrawHovered { get; private set; }
        /// <summary>The pointer is over the pause button's 44x44 hit box.</summary>
        public bool PauseHovered { get; private set; }
        /// <summary>The pause button's sort order.</summary>
        public const int PauseOrder = 650;
        /// <summary>The side of the "dot" sprite the bar is stretched from.</summary>
        const float DotSize = 4;
        [SerializeField] GameObject pauseGroup;
        [SerializeField] SpriteRenderer pauseEdge;
        /// <summary>Index of the held aspect the pointer is over (in the strip under the depth panel), or -1.</summary>
        public int HoveredAspect { get; private set; } = -1;
        /// <summary>The most catalysts the sockets can show: four from clearing depths and two from shrines.</summary>
        public const int MaxCatalysts = 6, MaxAspects = 3;

        /// <summary>The HUD's layout numbers (art pixels, y down), stored in Hud.prefab.</summary>
        [Serializable]
        public struct Layout
        {
            /// <summary>The pitch of the small note rows, English and Vietnamese (Crux at 16 needs 12 px, Pixel Operator 8 keeps 10).</summary>
            public int rowEn, rowVi;
            /// <summary>Characters per line of the sputter note, English and Vietnamese.</summary>
            public int noteCharsEn, noteCharsVi;
            /// <summary>The Redraw button in art space: under the right panel, right below the redraws-left counter.</summary>
            public Rect redrawRect;
            /// <summary>The pause button's picture (28x20 at 12,152; its hit box is 44x44 at 4,140). SPEC item 11 deviation: the aspects shelf band is full, shown only while a pause can open.</summary>
            public Rect pauseRect;
            /// <summary>How far below the Redraw box's top the label is placed, so the word and its shadow sit centred in the fill.</summary>
            public float redrawLabelY;
            /// <summary>How far the Ignite and Redraw labels sink while pressed.</summary>
            public float pressedDy;
            /// <summary>Catalyst sockets: pitch while few enough fit side by side (that many), the width they spread over when more overlap, the first socket's top-left, its size, the icon's offset in it and its size.</summary>
            public float catalystPitch, catalystSpread;
            public int catalystSideBySide;
            public Vector2 socketOrigin, socketSize, iconOffset, iconSize;
            /// <summary>Where a tooltip about a held aspect points (first one) and the pitch between them; the potency number's tooltip point.</summary>
            public Vector2 aspectAnchor, potencyAnchor;
            public float aspectPitch;
            /// <summary>The depth notes: the left edge and the top of the first row with and without held aspects.</summary>
            public float noteX, noteTopAspects, noteTop;
            /// <summary>The potency number: left edge (and the tight one that replaces the word), top; the bar's top-left, full width and height.</summary>
            public float potencyX, potencyTightX, potencyY;
            public Vector2 barOrigin;
            public float barWidth, barHeight;
            /// <summary>A number long enough to run into the target takes the word's place when from + width + gap passes to - target width.</summary>
            public float tightFrom, tightGap, tightTo;
            /// <summary>The Ignite button's glow: colour and alpha (min, swing).</summary>
            public Color pulseColor;
            public Vector2 pulseAlpha;
            /// <summary>The scale of the bar's bright tip.</summary>
            public Vector3 barTipScale;
        }

        [SerializeField] Label depth, depthName, potency, potencyWord, target, trials, redraws, redrawsLabel, journalCount, igniteText, hintText;
        [SerializeField] Label aspectsLabel, redrawText;
        [SerializeField] Label[] note = new Label[4], depthNote = new Label[3];
        [SerializeField] SpriteRenderer barFill, barTip, ignite, ignitePulse, redrawFill;
        [SerializeField] SpriteRenderer[] socketLit = new SpriteRenderer[MaxCatalysts], catalystIcon = new SpriteRenderer[MaxCatalysts];
        [SerializeField] SpriteRenderer[] aspectSocket = new SpriteRenderer[MaxAspects], aspectIcon = new SpriteRenderer[MaxAspects];
        // the hit boxes: one per catalyst socket and held aspect, then Redraw, pause, the journal book and Ignite
        [SerializeField] Clickable[] socketHit = new Clickable[MaxCatalysts], aspectHit = new Clickable[MaxAspects];
        [SerializeField] Clickable redrawHit, pauseHit, journalHit, igniteHit;
        [SerializeField] Layout layout;
        /// <summary>The Redraw button's fill: at rest, pointed at, pressed and when no redraw is left.</summary>
        [SerializeField] Color redrawUp, redrawOver, redrawPressed, redrawOff;
        /// <summary>The potency bar's fill when the depth has no target.</summary>
        [SerializeField] Color noTargetBar;
        float catalystPitch = -1;   // the pitch of the sockets shown; -1 until Show
        Vector3 igniteRest; bool igniteRestSet;
        float shown, bump; long goal, targetValue; bool canIgnite, igniteDown, canRedraw, redrawDown, wired;
        // what Tick last wrote, so a frame that changes nothing writes nothing (-1 / null / false: not written yet)
        long shownRounded = long.MinValue;
        bool shownVi, measuredVi, tight, tightSet, potencyPlaced, potencyBright, potencyColored, barHasTarget;
        string measuredPotency, measuredTarget;
        int placedBumpDy, barW = -1, igniteLook = -1, redrawFillLook = -1, redrawTextLook = -1;

        static Color Gold => Art.Palette.gold;
        static Color PauseEdgeColor => Art.Palette.edge;

        /// <summary>The bench can be switched off under the pointer, which then never sends exit or release: forget them.</summary>
        void OnDisable()
        {
            HoveredCatalyst = HoveredAspect = -1;
            RedrawHovered = redrawDown = igniteDown = PauseHovered = false;
        }

        /// <summary>Shows or hides the pause button (and its hit box): shown only while the bench is the player's.</summary>
        public void SetPauseShown(bool shown)
        {
            if (pauseGroup == null || pauseGroup.activeSelf == shown) return;
            pauseGroup.SetActive(shown);
            if (!shown) PauseHovered = false;
        }
        public bool PauseShown => pauseGroup != null && pauseGroup.activeSelf;
        public Vector3 PauseAnchor => Px.Mid(layout.pauseRect.x, layout.pauseRect.y, layout.pauseRect.width, layout.pauseRect.height);

        /// <summary>The Redraw button in art space (top-left origin).</summary>
        public Rect RedrawRect => layout.redrawRect;
        /// <summary>The pause button's picture in art space (top-left origin).</summary>
        public Rect PauseRect => layout.pauseRect;
        public Vector3 PotencyPosition => Px.At(layout.potencyAnchor.x, layout.potencyAnchor.y);
        /// <summary>Where a tooltip about a held catalyst or aspect points.</summary>
        public Vector3 CatalystAnchor(int i) => Px.Mid(layout.socketOrigin.x + i * (catalystPitch < 0 ? layout.catalystPitch : catalystPitch), layout.socketOrigin.y, layout.socketSize.x, layout.socketSize.y);
        public Vector3 AspectAnchor(int i) => Px.At(layout.aspectAnchor.x + i * layout.aspectPitch, layout.aspectAnchor.y);
        /// <summary>Where the Redraw tooltip (and the tutorial's highlight) points: the button's centre.</summary>
        public Vector3 RedrawAnchor => Px.Mid(layout.redrawRect.x, layout.redrawRect.y, layout.redrawRect.width, layout.redrawRect.height);
        /// <summary>The Redraw button is live: a redraw is left and the bench is being played (not an offer, not a cleared depth).</summary>
        public static bool CanRedraw(RunState run) => run != null && run.Phase == RunPhase.Playing && run.RedrawsLeft > 0;
        public static string Number(long n) => Loc.Number(n);
        /// <summary>Pitch of the small rows: Crux at 16 needs 12 px, Pixel Operator 8 keeps 10.</summary>
        int Row => Loc.Vi ? layout.rowVi : layout.rowEn;

        /// <summary>Hooks the hit boxes to the hover and click handlers. Once per instance: the pointer calls them, nothing else does.</summary>
        public void Bind()
        {
            if (wired) return;
            wired = true;
            for (int i = 0; i < socketHit.Length; i++)
            {
                int index = i;
                socketHit[i].onEnter = () => HoveredCatalyst = index;
                socketHit[i].onExit = () => { if (HoveredCatalyst == index) HoveredCatalyst = -1; };
            }
            for (int i = 0; i < aspectHit.Length; i++)
            {
                int index = i;
                aspectHit[i].onEnter = () => HoveredAspect = index;
                aspectHit[i].onExit = () => { if (HoveredAspect == index) HoveredAspect = -1; };
            }
            redrawHit.onClick = PressRedraw; redrawHit.onEnter = EnterRedraw; redrawHit.onExit = ExitRedraw;
            redrawHit.onDown = DownRedraw; redrawHit.onUp = UpRedraw;
            pauseHit.onClick = PressPause; pauseHit.onEnter = EnterPause; pauseHit.onExit = ExitPause;
            journalHit.onClick = PressJournal;
            igniteHit.onClick = PressIgnite; igniteHit.onDown = DownIgnite; igniteHit.onUp = UpIgnite; igniteHit.onExit = UpIgnite;
        }

        void PressRedraw() { if (canRedraw) RedrawClicked?.Invoke(); }
        void EnterRedraw() => RedrawHovered = true;
        void ExitRedraw() { RedrawHovered = false; redrawDown = false; }
        void DownRedraw() => redrawDown = true;
        void UpRedraw() => redrawDown = false;
        void PressPause() => PauseClicked?.Invoke();
        void EnterPause() { PauseHovered = true; pauseEdge.color = Gold; }
        void ExitPause() { PauseHovered = false; pauseEdge.color = PauseEdgeColor; }
        void PressJournal() => JournalClicked?.Invoke();
        void PressIgnite() { if (canIgnite) IgniteClicked?.Invoke(); }
        void DownIgnite() => igniteDown = true;
        void UpIgnite() => igniteDown = false;

        /// <summary>
        /// Everything except the potency number, which is animated separately. keepRedraw leaves the Redraw button and its
        /// counter as they are: GameFlow.Resolve passes it while an Ignite plays out, so they do not show the outcome early.
        /// </summary>
        public void Show(RunState run, ContentDb db, Journal journal, bool inputEnabled, bool keepRedraw = false)
        {
            depth.Text = Loc.T("ui.hud.depth", ("n", run.Depth));
            depthName.Text = Loc.DepthName(db, run.Depth);
            targetValue = run.Target;
            target.Text = run.Target > 0 ? "/ " + Number(run.Target) : Loc.T("ui.hud.no_target");
            trials.Text = run.TrialsLeft.ToString();
            redraws.Text = run.RedrawsLeft.ToString();
            // the button follows the run, not inputEnabled: it stays lit while a reaction plays (clicks are gated in BenchView);
            // keepRedraw holds it as it was while an Ignite plays out, the run being settled already
            if (!keepRedraw)
            {
                canRedraw = CanRedraw(run);
                redraws.Alpha = redrawsLabel.Alpha = canRedraw ? 1 : 0.35f;
            }
            // four sockets side by side; a fifth or sixth (a shrine's gift) makes them overlap to fit the panel
            int held = Mathf.Min(run.Catalysts.Count, MaxCatalysts);
            catalystPitch = held <= layout.catalystSideBySide ? layout.catalystPitch : layout.catalystSpread / (held - 1);
            for (int i = 0; i < MaxCatalysts; i++)
            {
                bool has = i < held;
                socketLit[i].enabled = catalystIcon[i].enabled = has;
                socketLit[i].gameObject.SetActive(has);
                if (!has) continue;
                float x = Mathf.Round(layout.socketOrigin.x + i * catalystPitch);
                socketLit[i].transform.position = Px.Mid(x, layout.socketOrigin.y, layout.socketSize.x, layout.socketSize.y);
                catalystIcon[i].transform.position = Px.Mid(x + layout.iconOffset.x, layout.socketOrigin.y + layout.iconOffset.y, layout.iconSize.x, layout.iconSize.y);
                catalystIcon[i].sprite = Art.Lib.Sprite("cat_" + run.Catalysts[i].id);
            }
            aspectsLabel.gameObject.SetActive(run.Aspects.Count > 0);
            for (int i = 0; i < MaxAspects; i++)
            {
                bool has = i < run.Aspects.Count;
                aspectSocket[i].gameObject.SetActive(has); aspectIcon[i].enabled = has;
                if (has) aspectIcon[i].sprite = Art.Lib.Sprite("asp_" + run.Aspects[i].Data.id);
            }
            if (run.Aspects.Count == 0) HoveredAspect = -1;
            // what this depth's event left behind (shut and warm tiles show on the bench itself)
            int line = 0; float top = run.Aspects.Count > 0 ? layout.noteTopAspects : layout.noteTop;
            foreach (var l in depthNote) l.Text = "";
            void Say(string text) { if (line < depthNote.Length) { depthNote[line].Text = text; depthNote[line].transform.position = Px.At(layout.noteX, top + line * Row); line++; } }
            if (run.RiShift != 0) Say(Loc.T("ui.hud.depth_ri", ("n", run.RiShift)));
            if (run.HandDelta != 0) Say(Loc.P("ui.hud.depth_hand", run.HandDelta));
            if (run.TripleFirst) Say(Loc.T("ui.hud.next_triple"));
            journalCount.Text = journal.KnownPairs.Count + "/" + db.Recipes.Count;
            canIgnite = inputEnabled && run.CanIgnite;
        }

        public void SetPotency(long value, bool instant = false) { goal = value; if (instant) shown = value; }

        /// <summary>Writes one reaction on the note: what went in, what came of it and its potency.</summary>
        public void Note(ReactionEvent e, ContentDb db)
        {
            if (e.Sputter)
            {
                var words = Loc.T("ui.hud.sputter").Split(' '); int at = 0;
                for (int i = 0; i < 3; i++)
                {
                    string line = "";
                    while (at < words.Length && (line.Length == 0 || i == 2 || line.Length + 1 + words[at].Length <= (Loc.Vi ? layout.noteCharsVi : layout.noteCharsEn))) line += (line.Length > 0 ? " " : "") + words[at++];
                    note[i].Text = line;
                }
                note[3].Text = "";
                note[2].Color = Art.Palette.faded;
                return;
            }
            // a pair is written out; a combo names its hub and counts the rest (the journal has the full recipe)
            note[0].Text = db.Material(e.Items[0]).name;
            note[1].Text = e.Items.Length == 2 ? "+ " + db.Material(e.Items[1]).name : Loc.P("ui.hud.around", e.Items.Length - 1);
            note[2].Text = e.Result != null ? "= " + db.Material(e.Result).name : Loc.T("ui.hud.nothing_happens");
            note[2].Color = Px.Hex(e.Result != null ? "#11606a" : "#7a5430");
            note[3].Text = e.Result != null ? "+" + Number(e.Potency) + (e.Discovery ? "  " + Loc.T("ui.hud.new") : e.Chain > 1 ? "  " + Loc.T("ui.hud.chain", ("n", e.Chain)) : "") : "";
            if (e.Hint != null) Hint(e.Hint, db);
        }

        public void Hint(HintData h, ContentDb db)
        {
            if (h == null) { hintText.Text = ""; return; }
            hintText.Text = HintLine(h, db);
        }

        /// <summary>"Ember Coal stirs near a potion.", or for a recipe the bench was too dull for, "Ember Coal needs heat 14."</summary>
        public static string HintLine(HintData h, ContentDb db, bool withName = true)
        {
            if (h.heat > 0) return withName ? Loc.T("ui.hud.hint_heat", ("item", db.Material(h.material).name), ("n", h.heat)) : Loc.T("ui.hud.hint_heat_short", ("n", h.heat));
            string kind = Loc.Indef(h.partnerKind);
            return withName ? Loc.T("ui.hud.hint_kind", ("item", db.Material(h.material).name), ("kind", kind)) : Loc.T("ui.hud.hint_kind_short", ("kind", kind));
        }

        /// <summary>A short kick of the potency number when points land on it.</summary>
        public void Bump() => bump = 1;

        public void ClearNote() { foreach (var n in note) n.Text = ""; }

        void Update() => Tick(Time.deltaTime);

        /// <summary>
        /// One frame of the HUD. Idle frames build no string and look up no sprite: the potency text is formatted only when the
        /// rounded number changes, the Ignite sprite and the two labels move only when the button's look changes, the bar is laid
        /// out only when its width changes. Only the Ignite glow, a continuous pulse, is written every frame (and only while lit).
        /// </summary>
        public void Tick(float deltaTime)
        {
            // potency counts toward its goal: fast when far away, never slower than a few points a second
            if (shown != goal)
            {
                float step = Mathf.Max(30, Mathf.Abs(goal - shown) * 6) * deltaTime;
                shown = Mathf.MoveTowards(shown, goal, step);
            }
            long rounded = (long)Mathf.Round(shown);
            bool vi = Loc.Vi;
            if (rounded != shownRounded || vi != shownVi) { shownRounded = rounded; shownVi = vi; potency.Text = Number(rounded); }
            bump = Mathf.Max(0, bump - deltaTime * 6);
            // a number long enough to run into the target takes the place of the word before it; the widths change only with a text
            string potencyText = potency.Text, targetText = target.Text;
            if (!ReferenceEquals(potencyText, measuredPotency) || !ReferenceEquals(targetText, measuredTarget) || vi != measuredVi)
            {
                measuredPotency = potencyText; measuredTarget = targetText; measuredVi = vi;
                bool nowTight = layout.tightFrom + potency.Width + layout.tightGap > layout.tightTo - target.Width;
                if (nowTight != tight || !tightSet) { tight = nowTight; tightSet = true; potencyWord.gameObject.SetActive(!tight); }
                potencyPlaced = false;
            }
            int bumpDy = Mathf.CeilToInt(bump * 2);   // 2 px in the first half of the kick, 1 px in the second
            if (!potencyPlaced || bumpDy != placedBumpDy)
            {
                potencyPlaced = true; placedBumpDy = bumpDy;
                potency.transform.position = Px.At(tight ? layout.potencyTightX : layout.potencyX, layout.potencyY - bumpDy);
            }
            bool bright = bump > 0.4f;
            if (bright != potencyBright || !potencyColored) { potencyBright = bright; potencyColored = true; potency.Color = bright ? Color.white : Gold; }
            float fill = targetValue > 0 ? Mathf.Clamp01(shown / targetValue) : 1;
            int w = Mathf.RoundToInt(layout.barWidth * fill);
            bool hasTarget = targetValue > 0;
            if (w != barW || hasTarget != barHasTarget)
            {
                barW = w; barHasTarget = hasTarget;
                barFill.enabled = barTip.enabled = w > 0;
                barFill.transform.position = Px.Mid(layout.barOrigin.x, layout.barOrigin.y, w, layout.barHeight); barFill.transform.localScale = new Vector3(w / DotSize, 1, 1);
                barFill.color = hasTarget ? Color.white : noTargetBar;
                barTip.transform.position = Px.Mid(layout.barOrigin.x + Mathf.Max(0, w - 1), layout.barOrigin.y, 1, layout.barHeight); barTip.transform.localScale = layout.barTipScale;
            }

            // Ignite: 0 dark, 1 up, 2 held down; the sprite, the glow's reset and the label change only when it does
            int look = !canIgnite ? 0 : igniteDown ? 2 : 1;
            bool pressedIgnite = look == 2;
            if (look != igniteLook)
            {
                igniteLook = look;
                ignite.sprite = Art.Lib.Sprite(look == 0 ? "ignite_off" : look == 2 ? "ignite_down" : "ignite_up");
                if (look != 1) ignitePulse.color = Color.clear;
                igniteText.Alpha = canIgnite ? 1 : 0.45f;
                if (!igniteRestSet) { igniteRest = igniteText.transform.position; igniteRestSet = true; }   // the label's place is the prefab's
                igniteText.transform.position = igniteRest + new Vector3(0, pressedIgnite ? -layout.pressedDy : 0, 0);
            }
            if (look == 1)
            {
                float pulse = 0.5f + 0.5f * Mathf.Sin(Time.time * 3);
                ignitePulse.color = layout.pulseColor.Alpha(layout.pulseAlpha.x + layout.pulseAlpha.y * pulse);
            }
            int fillLook = !canRedraw ? 0 : redrawDown ? 1 : RedrawHovered ? 2 : 3;
            if (fillLook != redrawFillLook)
            {
                redrawFillLook = fillLook;
                redrawFill.color = fillLook == 0 ? redrawOff : fillLook == 1 ? redrawPressed : fillLook == 2 ? redrawOver : redrawUp;
            }
            int textLook = !canRedraw ? 0 : redrawDown ? 2 : 1;
            bool pressedRedraw = textLook == 2;
            if (textLook != redrawTextLook)
            {
                redrawTextLook = textLook;
                redrawText.Alpha = canRedraw ? 1 : 0.45f;
                redrawText.transform.position = Px.At(layout.redrawRect.center.x, layout.redrawRect.y + layout.redrawLabelY + (pressedRedraw ? layout.pressedDy : 0));
            }
        }
    }
}
