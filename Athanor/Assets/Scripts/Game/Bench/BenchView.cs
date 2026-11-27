using System;
using System.Collections;
using System.Collections.Generic;
using Athanor.Gameplay;
using DG.Tweening;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The main screen: nine tiles, the hand of cards, the rack of discoveries and the HUD.
    /// Materials are placed by dragging them onto a tile, or by clicking one and then a tile; clicking a placed one takes it back.
    /// </summary>
    public sealed class BenchView : MonoBehaviour
    {
        /// <summary>Stops what this component started so nothing runs on into the next Play (domain reload off).</summary>
        void OnDestroy() { StopAllCoroutines(); Motion.StopOn(this); }

        public event Action IgniteRequested, RedrawRequested, JournalRequested;
        public bool InputEnabled { get => inputEnabled; set { inputEnabled = value; if (run != null) Hud.Show(run, db, journal, value); if (!value) DropEverything(); } }
        public HudView Hud => hud;
        /// <summary>How many tiles the bench holds (nine).</summary>
        public int TileCount => tiles.Length;

        /// <summary>How many wells the rack has (the array size of the prefab's slots).</summary>
        const int RackSlots = 12;

        /// <summary>Offsets and a colour of the bench, stored in Bench.prefab.</summary>
        [Serializable]
        public struct Layout
        {
            /// <summary>Where the hand-drawn links of a join start and end, from a tile's centre.</summary>
            public Vector3 joinLift;
            /// <summary>A floating number starts and ends this far above its point.</summary>
            public Vector3 floatLiftFrom, floatLiftTo;
            /// <summary>The floating number's outline.</summary>
            public Color floatShadow;
            /// <summary>The dragged material's ghost rides this far above the pointer.</summary>
            public float ghostLift;
            /// <summary>Where a tooltip points: above a tile's centre, above a card's top (half its height).</summary>
            public Vector3 tileTipLift, cardTipLift;
            /// <summary>The hand's strip: left edge, width, the cards' top, and the widest gap between two cards (art px).</summary>
            public float handX, handW, cardY, handMaxPitch;
            /// <summary>A rack well takes a click within slotHit of its centre; a placed material within tileHitHalfWidth sideways and from tileHitBelow under to tileHitAbove over its tile centre.</summary>
            public float slotHit, tileHitHalfWidth, tileHitBelow, tileHitAbove;
        }
        /// <summary>The deal's gap between two cards, and how long the pointer rests before a tooltip shows (seconds).</summary>
        [Serializable]
        public struct Timing { public float dealStagger, tipDelay; }
        [SerializeField] Timing timing;

        enum From { None, Hand, Tile }
        struct Pick { public From from; public int handIndex, tile; public string id; public bool Is => from != From.None; }
        [Serializable] sealed class Slot { public SpriteRenderer icon, lit; public Vector3 pos; [NonSerialized] public int handIndex = -1; [NonSerialized] public string id; }

        RunState run; ContentDb db; Journal journal;
        bool inputEnabled;
        // the parts of the Bench prefab: nine tiles, the rack wells, the two link layers, the nested Hud and Tooltip, the hand's root
        [SerializeField] TileView[] tiles = new TileView[Bench.TileCount];
        [SerializeField] Slot[] slots = new Slot[RackSlots];
        [SerializeField] LinkView links, joins;
        [SerializeField] HudView hud;
        [SerializeField] Tooltip tip;
        [SerializeField] CardView cardPrefab;
        [SerializeField] Transform handRoot;
        [SerializeField] SpriteRenderer ghost;
        [SerializeField] Label rackFull;
        [SerializeField] Layout layout;
        readonly List<CardView> cards = new List<CardView>();
        bool bound;
        readonly string[] tileNote = new string[Bench.TileCount], tileNoteTitle = new string[Bench.TileCount];
        Pick press, selected; Vector2 pressAt; bool dragging;
        float hoverTime;
        // the tooltip: what the pointer rests on (a value, so an idle frame builds no key string), and what the shown tip was built from
        enum TipKind { None, Catalyst, Aspect, Pause, Redraw, Material, TileNote }
        struct TipKey
        {
            public TipKind kind; public string id; public int a, b;
            public bool Equals(TipKey o) => kind == o.kind && a == o.a && b == o.b && id == o.id;
        }
        TipKey hoverKey;
        bool tipUp, tipVi; int tipRefresh = -1, tipKnown, tipRedraws, refreshes;
        int selFrom = -1, selHand, pressFrom, pressHand; bool selDragging;      // what ShowSelection last drew

        /// <summary>
        /// Once, after the Bench prefab is in the scene: hands over the content, finishes the nested Hud and Tooltip and hooks the
        /// Hud's buttons. Run state comes later, with Bind(run, journal).
        /// </summary>
        public void Bind(ContentDb content, FxRefs fx)
        {
            if (fx == null) throw new System.ArgumentNullException(nameof(fx));
            if (bound) return;
            bound = true;
            db = content;
            foreach (var t in tiles) if (t != null) t.Bind(fx);
            tip.Bind();
            hud.Bind();
            hud.IgniteClicked += () => { if (inputEnabled) IgniteRequested?.Invoke(); };
            hud.JournalClicked += () => { if (inputEnabled) JournalRequested?.Invoke(); };
            hud.RedrawClicked += () => { if (inputEnabled) RedrawRequested?.Invoke(); };
        }

        public void Bind(RunState state, Journal book)
        {
            run = state; journal = book;
            selected = press = default; dragging = false; tipUp = false;
            Hud.ClearNote(); Hud.Hint(null, db);
            Hud.SetPotency(run.Potency, true);
        }

        public Vector3 TileCentre(int i) => tiles[i].centre;
        /// <summary>World position of a rack well (clamped to the rack).</summary>
        public Vector3 RackSlot(int i) => slots[Mathf.Clamp(i, 0, RackSlots - 1)].pos;
        /// <summary>Shows a freshly made material in a rack well until the next Refresh makes it official.</summary>
        public void PreviewOnRack(int i, string id)
        {
            var s = slots[Mathf.Clamp(i, 0, RackSlots - 1)];
            s.icon.sprite = Art.Lib.Sprite(id); s.icon.color = Color.white; s.icon.enabled = true;
        }
        public TileView Tile(int i) => tiles[i];

        /// <summary>Redraws tiles, hand, rack, links and HUD from the run. deal slides the cards in from below.</summary>
        public void Refresh(bool deal = false)
        {
            refreshes++;
            for (int i = 0; i < tiles.Length; i++)
            {
                var id = run.Bench.Tiles[i];
                if (id == null) tiles[i].Clear(); else tiles[i].Show(db.Material(id), tiles[i].Material != id);
            }
            ShowTileStates();
            foreach (var c in cards) Destroy(c.gameObject);
            cards.Clear();
            foreach (var s in slots) { s.handIndex = -1; s.id = null; s.icon.enabled = s.lit.enabled = false; }

            var starters = new List<int>(); int rack = 0;
            for (int h = 0; h < run.Hand.Count; h++)
            {
                var m = db.Material(run.Hand[h]);
                if (m.tier == 0) starters.Add(h);
                else if (rack < RackSlots)
                {
                    var s = slots[rack++];
                    s.handIndex = h; s.id = m.id; s.icon.sprite = Art.Lib.Sprite(m.id); s.icon.enabled = true; s.icon.color = Color.white;
                }
            }
            int n = starters.Count;
            float pitch = n <= 1 ? 0 : Mathf.Min(layout.handMaxPitch, (layout.handW - CardView.W) / (float)(n - 1));
            float startX = layout.handX + (layout.handW - (CardView.W + pitch * (n - 1))) / 2f;
            for (int k = 0; k < n; k++)
            {
                var m = db.Material(run.Hand[starters[k]]);
                var card = Instantiate(cardPrefab, handRoot, false);
                card.name = "Card " + m.id;
                card.Show(m, 110 + k * 4);
                card.Fan(k < n - 1 ? pitch : CardView.W, 110 + n * 4);     // every card but the last has the next one over its right
                card.handIndex = starters[k];
                card.home = Px.Snap(Px.Mid(startX + k * pitch, layout.cardY, CardView.W, CardView.H));
                if (deal) card.DealIn(k * timing.dealStagger); else card.transform.position = card.home;
                cards.Add(card);
            }
            int held = 0; foreach (var id in run.Hand) if (db.Material(id).tier > 0) held++;
            rackFull.Text = held >= db.Rules.rackSize ? Loc.T("ui.bench.rack_full") : "";

            // links show what the bench will do, hub to neighbours, for as far as the player already knows the recipes
            var pairs = new List<(Vector3, Vector3)>();
            foreach (var step in ChainPlanner.Plan(run.Bench.Tiles, db, run.Rules, false).Steps)
            {
                if (!journal.IsKnown(step.Items)) break;
                for (int k = 1; k < step.Tiles.Length; k++) pairs.Add((tiles[step.Hub].centre, tiles[step.Tiles[k]].centre));
            }
            links.Set(pairs);

            if (selected.from == From.Hand && (selected.handIndex >= run.Hand.Count || run.Hand[selected.handIndex] != selected.id)) selected = default;
            if (selected.from == From.Tile) selected = default;
            Hud.Show(run, db, journal, inputEnabled);
            ShowSelection();
        }

        /// <summary>Markers for the tiles an aspect or this depth's event acts on, lids on the shut ones, lines between joined ones.</summary>
        void ShowTileStates()
        {
            var marks = new Color?[tiles.Length];
            for (int i = 0; i < tiles.Length; i++) tileNote[i] = tileNoteTitle[i] = null;
            foreach (var a in run.Aspects)
                foreach (int t in a.Tiles)
                {
                    marks[t] = Wording.AspectColor(a.Data.effect);
                    tileNoteTitle[t] = a.Data.name; tileNote[t] = Wording.AspectRule(a);
                }
            foreach (int t in run.DepthHeated)
            {
                marks[t] = Wording.Warm;
                tileNoteTitle[t] = Loc.T("ui.bench.warm_tile"); tileNote[t] = Loc.T("ui.bench.warm_tile_text", ("n", run.Rules.RiBonus[t] > 0 ? run.Rules.RiBonus[t] : 3));
            }
            foreach (int t in run.DepthBlocked) { tileNoteTitle[t] = Loc.T("ui.bench.shut_tile"); tileNote[t] = Loc.T("ui.bench.shut_tile_text"); }
            for (int i = 0; i < tiles.Length; i++) tiles[i].SetState(marks[i], run.Rules.Blocked[i]);
            var joined = new List<(Vector3, Vector3)>();
            foreach (var (a, b) in run.Rules.Links) joined.Add((tiles[a].centre + layout.joinLift, tiles[b].centre + layout.joinLift));
            joins.Set(joined);
        }

        /// <summary>Hides the hand-drawn links while reactions resolve, so the bench reads cleanly.</summary>
        public void ClearLinks() => links.Set(new List<(Vector3, Vector3)>());

        /// <summary>A number that rises from a point and fades.</summary>
        public IEnumerator FloatText(string text, Vector3 from, Color color, float seconds, bool small = false)
        {
            var label = Spawn.Text(transform, text, 0, 0, small ? FontKind.Tiny : FontKind.Title, small ? 8 : 16, color, 170, Align.Centre, layout.floatShadow);
            // it rises the whole time and fades over the last 30 %
            yield return Motion.Seq()
                .Group(Motion.Move(label.transform, from + layout.floatLiftFrom, from + layout.floatLiftTo, seconds, Ease.OutCubic))
                .Insert(0.7f * seconds, Motion.Fade(label, 1, 0, 0.3f * seconds))
                .ToYieldInstruction();
            Destroy(label.gameObject);
        }

        // ---- picking things up and putting them down ----------------------------------------------

        void DropEverything()
        {
            press = default; dragging = false; selected = default;
            if (ghost != null) ghost.enabled = false;
            if (tip != null) tip.Hide();
            tipUp = false;
            foreach (var t in tiles) if (t != null) t.Hovered = t.Pinged = false;
            ShowSelection();
        }

        Pick ItemAt(Vector2 w)
        {
            Vector2 tw = PointerInput.Bench;      // tiles are hit through the world ray
            for (int i = cards.Count - 1; i >= 0; i--)
                if (cards[i].Contains(w)) return new Pick { from = From.Hand, handIndex = cards[i].handIndex, id = cards[i].id, tile = -1 };
            foreach (var s in slots)
                if (s.handIndex >= 0 && Mathf.Abs(w.x - s.pos.x) <= layout.slotHit && Mathf.Abs(w.y - s.pos.y) <= layout.slotHit)
                    return new Pick { from = From.Hand, handIndex = s.handIndex, id = s.id, tile = -1 };
            // front rows first: a material stands tall enough to overlap the tile behind it
            for (int i = tiles.Length - 1; i >= 0; i--)
            {
                if (tiles[i].Material == null) continue;
                var c = tiles[i].centre;
                if ((Mathf.Abs(tw.x - c.x) <= layout.tileHitHalfWidth && tw.y >= c.y - layout.tileHitBelow && tw.y <= c.y + layout.tileHitAbove) || tiles[i].Contains(tw))
                    return new Pick { from = From.Tile, tile = i, id = tiles[i].Material, handIndex = -1 };
            }
            return default;
        }

        int FreeTileAt(Vector2 w)
        {
            for (int i = tiles.Length - 1; i >= 0; i--)
                if (tiles[i].Contains(PointerInput.Bench) && run.Bench.Tiles[i] == null && !run.Rules.Blocked[i]) return i;
            return -1;
        }

        /// <summary>Development-build log lines of a card put on / taken off a tile (tools/web/mainhd2d.mjs waits for them).</summary>
        public static string PlaceLine(int tile) => "[bench] place " + tile;
        public static string UnplaceLine(int tile) => "[bench] unplace " + tile;

        bool PlaceCard(int hand, int tile)
        {
            if (!run.Place(hand, tile)) return false;
            Dev.Hook(PlaceLine(tile));
            return true;
        }

        bool UnplaceCard(int tile)
        {
            if (!run.Unplace(tile)) return false;
            Dev.Hook(UnplaceLine(tile));
            return true;
        }

        bool Put(Pick p, int tile)
        {
            if (tile < 0 || !p.Is) return false;
            if (p.from == From.Hand) return PlaceCard(p.handIndex, tile);
            if (!UnplaceCard(p.tile)) return false;       // goes to the end of the hand, then on to its new tile
            return PlaceCard(run.Hand.Count - 1, tile);
        }

        void Update() => Tick(Time.deltaTime);

        /// <summary>
        /// One frame of the bench: the pointer's picks, the hover marks and the tooltip. A frame where nothing changed builds no
        /// string: the tooltip is made when the pointer settles on something (or the run, the language or the journal changes),
        /// and the selection is redrawn only when it changes.
        /// </summary>
        public void Tick(float deltaTime)
        {
            if (run == null || !inputEnabled) return;
            Vector2 w = PointerInput.World;
            var over = ItemAt(w);
            int freeTile = FreeTileAt(w);
            bool changed = false;

            if (PointerInput.Down)
            {
                if (over.Is) { press = over; pressAt = w; dragging = false; }
                else if (selected.Is && freeTile >= 0) { changed = Put(selected, freeTile); selected = default; }
                else selected = default;
            }
            if (press.Is && PointerInput.Held && !dragging && Vector2.Distance(w, pressAt) > 4)
            {
                dragging = true; selected = default;
                ghost.sprite = Art.Lib.Sprite(press.id); ghost.enabled = true;
                if (press.from == From.Tile) tiles[press.tile].Icon.color = Color.white.Alpha(0.3f);
            }
            if (dragging) ghost.transform.position = Px.Snap(new Vector3(w.x, w.y + layout.ghostLift, 0));
            if (PointerInput.Up && press.Is)
            {
                if (dragging)
                {
                    if (freeTile >= 0) changed = Put(press, freeTile);
                    else if (press.from == From.Tile && !tiles[press.tile].Contains(PointerInput.Bench)) changed = UnplaceCard(press.tile);
                    if (!changed && press.from == From.Tile) tiles[press.tile].Icon.color = Color.white;
                }
                else if (press.from == From.Tile) changed = UnplaceCard(press.tile);
                else selected = selected.Is && selected.handIndex == press.handIndex ? default : press;
                press = default; dragging = false; ghost.enabled = false;
            }
            if (!PointerInput.Held && press.Is) { press = default; dragging = false; ghost.enabled = false; }

            if (changed) Refresh();
            bool carrying = dragging || selected.Is;
            // the aspect under the pointer in the HUD shows where it sits
            int shown = Hud.HoveredAspect >= 0 && Hud.HoveredAspect < run.Aspects.Count ? Hud.HoveredAspect : -1;
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].Hovered = carrying && i == freeTile;
                tiles[i].Pinged = shown >= 0 && System.Array.IndexOf(run.Aspects[shown].Tiles, i) >= 0;
            }
            foreach (var c in cards)
            {
                c.Lifted = !dragging && over.from == From.Hand && over.handIndex == c.handIndex;
                c.Dimmed = dragging && press.from == From.Hand && press.handIndex == c.handIndex;
            }
            if ((int)selected.from != selFrom || selected.handIndex != selHand || dragging != selDragging || (int)press.from != pressFrom || press.handIndex != pressHand)
                ShowSelection();
            ShowTooltip(over, w, deltaTime);
        }

        void ShowSelection()
        {
            selFrom = (int)selected.from; selHand = selected.handIndex; selDragging = dragging; pressFrom = (int)press.from; pressHand = press.handIndex;
            foreach (var c in cards) c.Selected = selected.from == From.Hand && selected.handIndex == c.handIndex;
            foreach (var s in slots)
            {
                if (s.handIndex < 0) continue;
                s.lit.enabled = selected.from == From.Hand && selected.handIndex == s.handIndex;
                s.icon.color = Color.white.Alpha(dragging && press.from == From.Hand && press.handIndex == s.handIndex ? 0.3f : 1);
            }
        }

        void ShowTooltip(Pick over, Vector2 w, float deltaTime)
        {
            // what the pointer rests on: a cheap value, the same order of precedence as the text below
            var key = default(TipKey);
            if (!dragging && Hud.HoveredCatalyst >= 0 && Hud.HoveredCatalyst < run.Catalysts.Count)
                key = new TipKey { kind = TipKind.Catalyst, a = Hud.HoveredCatalyst, id = run.Catalysts[Hud.HoveredCatalyst].id };
            else if (!dragging && Hud.HoveredAspect >= 0 && Hud.HoveredAspect < run.Aspects.Count)
                key = new TipKey { kind = TipKind.Aspect, a = Hud.HoveredAspect, id = run.Aspects[Hud.HoveredAspect].Data.id };
            else if (!dragging && Hud.PauseHovered && !Application.isMobilePlatform) key = new TipKey { kind = TipKind.Pause };
            else if (!dragging && Hud.RedrawHovered) key = new TipKey { kind = TipKind.Redraw };
            // a hand card of a starting material tells its rarity (SPEC 23), so it has a tip too; a made material in the rack always had one
            else if (!dragging && over.Is && (over.from == From.Tile || over.from == From.Hand || db.Material(over.id).tier > 0))
                key = new TipKey { kind = TipKind.Material, id = over.id, a = over.tile, b = over.handIndex };
            else if (!dragging && !over.Is)
            {
                // an empty tile that an aspect or this depth's event acts on says what it does
                for (int i = tiles.Length - 1; i >= 0; i--)
                {
                    if (tileNote[i] == null || !tiles[i].Contains(PointerInput.Bench)) continue;
                    key = new TipKey { kind = TipKind.TileNote, a = i };
                    break;
                }
            }
            if (!key.Equals(hoverKey)) { hoverKey = key; hoverTime = 0; tip.Hide(); tipUp = false; }
            if (key.kind == TipKind.None) return;
            hoverTime += deltaTime;
            if (hoverTime <= timing.tipDelay) return;
            // the tip stays as built until what it was built from changes
            int known = journal.KnownPairs.Count;
            if (tipUp && tipRefresh == refreshes && tipKnown == known && tipRedraws == run.RedrawsLeft && tipVi == Loc.Vi) return;
            tipUp = true; tipRefresh = refreshes; tipKnown = known; tipRedraws = run.RedrawsLeft; tipVi = Loc.Vi;
            BuildTip(key, over, w);
        }

        /// <summary>Writes the tooltip for what the pointer rests on.</summary>
        void BuildTip(TipKey key, Pick over, Vector2 w)
        {
            string title = null, body = null, rarity = null; Vector2 anchor = w; float clear = 18;
            switch (key.kind)
            {
                case TipKind.Catalyst:
                {
                    var c = run.Catalysts[key.a];
                    title = Loc.T("ui.bench.catalyst_title", ("name", c.name), ("rarity", Loc.Rarity(c.rarity))); body = Loc.C("catalyst." + c.id + ".text", c.text); anchor = Hud.CatalystAnchor(key.a);
                    break;
                }
                case TipKind.Aspect:
                {
                    var a = run.Aspects[key.a];
                    title = a.Data.name; body = Wording.AspectRule(a); anchor = Hud.AspectAnchor(key.a); clear = 16;
                    break;
                }
                case TipKind.Pause:
                    title = Loc.T("ui.pause.button"); anchor = Hud.PauseAnchor; body = "";
                    break;
                case TipKind.Redraw:
                    title = Loc.T("ui.bench.redraw"); anchor = Hud.RedrawAnchor;
                    body = Loc.T(run.RedrawsLeft > 0 ? "ui.bench.redraw_text" : "ui.bench.redraw_none");
                    break;
                case TipKind.Material:
                {
                    var m = db.Material(over.id);
                    int known = 0; foreach (var r in db.RecipesContaining(m.id)) if (journal.IsKnown(r.items)) known++;
                    title = m.name;
                    // on the bench: the heat a recipe with this tile as its hub would have (itself and all it touches)
                    string heat = over.from == From.Tile ? Loc.T("ui.bench.heat_here", ("n", ChainPlanner.Heat(run.Bench.Tiles, over.tile, db, run.Rules))) : "";
                    // two lines: the panel has no room for a third
                    int ri = over.from == From.Tile ? run.Rules.Ri(db, over.tile, m.id) : m.reactivity;
                    body = Loc.T("ui.bench.material", ("kind", Loc.Kind(m.kind)), ("tier", m.tier), ("known", known), ("ri", ri)) + heat;
                    if (over.from == From.Tile) { anchor = tiles[over.tile].centre + layout.tileTipLift; clear = 22; }
                    else
                    {
                        foreach (var s in slots) if (s.handIndex == over.handIndex) anchor = s.pos;
                        foreach (var c in cards) if (c.handIndex == over.handIndex) anchor = c.home + layout.cardTipLift;   // above the card
                        clear = 16;
                        if (m.tier == 0) rarity = Rarities.Of(m.rarity);
                    }
                    break;
                }
                case TipKind.TileNote:
                    title = tileNoteTitle[key.a]; body = tileNote[key.a]; anchor = tiles[key.a].centre; clear = 16;
                    break;
            }
            tip.Show(title, body, anchor, clear, rarity);
        }
    }
}
