using System;
using System.Collections;
using System.Collections.Generic;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The main screen: nine tiles, the hand of cards, the rack of discoveries and the HUD.
    /// Materials are placed by dragging them onto a tile, or by clicking one and then a tile; clicking a placed one takes it back.
    /// </summary>
    public sealed class BenchView : MonoBehaviour
    {
        public event Action IgniteRequested, RedrawRequested, JournalRequested;
        public bool InputEnabled { get => inputEnabled; set { inputEnabled = value; if (run != null) Hud.Show(run, db, journal, value); if (!value) DropEverything(); } }
        public HudView Hud { get; private set; }

        const int RackX = 130, RackY = 256, RackFirst = 6, RackPitch = 31, RackSlots = 12, CardY = 290, HandX = 128, HandW = 386;

        enum From { None, Hand, Tile }
        struct Pick { public From from; public int handIndex, tile; public string id; public bool Is => from != From.None; }
        sealed class Slot { public SpriteRenderer icon, lit; public int handIndex = -1; public string id; public Vector3 pos; }

        RunState run; ContentDb db; Journal journal;
        bool inputEnabled;
        readonly TileView[] tiles = new TileView[Bench.TileCount];
        readonly List<CardView> cards = new List<CardView>();
        readonly Slot[] slots = new Slot[RackSlots];
        LinkView links, joins; Tooltip tip; Transform handRoot; SpriteRenderer ghost; Label rackFull;
        readonly string[] tileNote = new string[Bench.TileCount], tileNoteTitle = new string[Bench.TileCount];
        Pick press, selected; Vector2 pressAt; bool dragging;
        float hoverTime; string hoverKey;

        public void Build(LayoutData layout, ContentDb content, Transform parent)
        {
            db = content;
            transform.SetParent(parent, false);
            var tileRoot = Make.Root("Tiles", transform);
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new GameObject("Tile " + i).AddComponent<TileView>();
                tiles[i].Build(i, layout.tiles[i], tileRoot);
            }
            links = Make.Root("Links", transform).gameObject.AddComponent<LinkView>();
            // tiles an aspect has joined (a bridge, an open bench): a slow, steady line of their own colour
            joins = Make.Root("Joins", transform).gameObject.AddComponent<LinkView>();
            joins.color = new Color(1.6f, 1.25f, 0.45f); joins.speed = 0.08f; joins.arc = 3; joins.alpha = 0.7f;
            Hud = Make.Root("Hud", transform).gameObject.AddComponent<HudView>();
            Hud.Build(transform);
            Hud.IgniteClicked += () => { if (inputEnabled) IgniteRequested?.Invoke(); };
            Hud.JournalClicked += () => { if (inputEnabled) JournalRequested?.Invoke(); };
            Hud.RedrawClicked += () => { if (inputEnabled) RedrawRequested?.Invoke(); };

            var rackRoot = Make.Root("Rack", transform);
            Make.SpriteAt(rackRoot, "rack", RackX, RackY, 100);
            for (int i = 0; i < RackSlots; i++)
            {
                int x = RackX + RackFirst + i * RackPitch, y = RackY + 2;
                var s = slots[i] = new Slot { pos = Px.Mid(x, y, 26, 26) };
                s.lit = Make.SpriteAt(rackRoot, "rack_slot_lit", x, y, 101);
                s.icon = Make.Sprite(rackRoot, "dot", s.pos, 103);
                s.lit.enabled = s.icon.enabled = false;
            }
            rackFull = Make.Text(rackRoot, "", 320, 246, FontKind.Tiny, 8, Px.Hex("#ee6a3c"), 104, Align.Centre, Px.Hex("#0c0706"));
            handRoot = Make.Root("Hand", transform);
            ghost = Make.Sprite(transform, "dot", Vector3.zero, 160);
            ghost.enabled = false;
            tip = Make.Root("Tooltip", transform).gameObject.AddComponent<Tooltip>();
            tip.Build(transform);
        }

        public void Bind(RunState state, Journal book)
        {
            run = state; journal = book;
            selected = press = default; dragging = false;
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
            s.icon.sprite = Make.Lib.Sprite(id); s.icon.color = Color.white; s.icon.enabled = true;
        }
        public TileView Tile(int i) => tiles[i];

        /// <summary>Redraws tiles, hand, rack, links and HUD from the run. deal slides the cards in from below.</summary>
        public void Refresh(bool deal = false)
        {
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
                    s.handIndex = h; s.id = m.id; s.icon.sprite = Make.Lib.Sprite(m.id); s.icon.enabled = true; s.icon.color = Color.white;
                }
            }
            int n = starters.Count;
            float pitch = n <= 1 ? 0 : Mathf.Min(76, (HandW - CardView.W) / (float)(n - 1));
            float startX = HandX + (HandW - (CardView.W + pitch * (n - 1))) / 2f;
            for (int k = 0; k < n; k++)
            {
                var m = db.Material(run.Hand[starters[k]]);
                var card = new GameObject("Card " + m.id).AddComponent<CardView>();
                card.Build(m, handRoot, 110 + k * 4);
                card.Fan(k < n - 1 ? pitch : CardView.W, 110 + n * 4);     // every card but the last has the next one over its right
                card.handIndex = starters[k];
                card.home = Px.Snap(Px.Mid(startX + k * pitch, CardY, CardView.W, CardView.H));
                if (deal) card.DealIn(k * 0.05f); else card.transform.position = card.home;
                cards.Add(card);
            }
            int held = 0; foreach (var id in run.Hand) if (db.Material(id).tier > 0) held++;
            rackFull.Text = held >= RunState.RackSize ? "RACK FULL: THE WEAKEST WILL FALL" : "";

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
                tileNoteTitle[t] = "Warm Tile"; tileNote[t] = "A material here counts " + (run.Rules.RiBonus[t] > 0 ? run.Rules.RiBonus[t] : 3) + " more Reaction Index, this depth only.";
            }
            foreach (int t in run.DepthBlocked) { tileNoteTitle[t] = "Shut Tile"; tileNote[t] = "Nothing can be placed here until the next depth."; }
            for (int i = 0; i < tiles.Length; i++) tiles[i].SetState(marks[i], run.Rules.Blocked[i]);
            var joined = new List<(Vector3, Vector3)>();
            foreach (var (a, b) in run.Rules.Links) joined.Add((tiles[a].centre + new Vector3(0, -8, 0), tiles[b].centre + new Vector3(0, -8, 0)));
            joins.Set(joined);
        }

        /// <summary>Hides the hand-drawn links while reactions resolve, so the bench reads cleanly.</summary>
        public void ClearLinks() => links.Set(new List<(Vector3, Vector3)>());

        /// <summary>A number that rises from a point and fades.</summary>
        public IEnumerator FloatText(string text, Vector3 from, Color color, float seconds = 0.8f, bool small = false)
        {
            var label = Make.Text(transform, text, 0, 0, small ? FontKind.Tiny : FontKind.Title, small ? 8 : 16, color, 170, Align.Centre, Px.Hex("#0c0706"));
            yield return Anim.Over(seconds, t =>
            {
                label.transform.position = Px.Snap(from + new Vector3(0, 6 + Anim.OutCubic(t) * 22, 0));
                label.Alpha = t < 0.7f ? 1 : 1 - (t - 0.7f) / 0.3f;
            });
            Destroy(label.gameObject);
        }

        // ---- picking things up and putting them down ----------------------------------------------

        void DropEverything()
        {
            press = default; dragging = false; selected = default;
            if (ghost != null) ghost.enabled = false;
            if (tip != null) tip.Hide();
            foreach (var t in tiles) if (t != null) t.Hovered = t.Pinged = false;
            ShowSelection();
        }

        Pick ItemAt(Vector2 w)
        {
            for (int i = cards.Count - 1; i >= 0; i--)
                if (cards[i].Contains(w)) return new Pick { from = From.Hand, handIndex = cards[i].handIndex, id = cards[i].id, tile = -1 };
            foreach (var s in slots)
                if (s.handIndex >= 0 && Mathf.Abs(w.x - s.pos.x) <= 14 && Mathf.Abs(w.y - s.pos.y) <= 14)
                    return new Pick { from = From.Hand, handIndex = s.handIndex, id = s.id, tile = -1 };
            // front rows first: a material stands tall enough to overlap the tile behind it
            for (int i = tiles.Length - 1; i >= 0; i--)
            {
                if (tiles[i].Material == null) continue;
                var c = tiles[i].centre;
                if ((Mathf.Abs(w.x - c.x) <= 13 && w.y >= c.y - 3 && w.y <= c.y + 24) || tiles[i].Contains(w))
                    return new Pick { from = From.Tile, tile = i, id = tiles[i].Material, handIndex = -1 };
            }
            return default;
        }

        int FreeTileAt(Vector2 w)
        {
            for (int i = tiles.Length - 1; i >= 0; i--)
                if (tiles[i].Contains(w) && run.Bench.Tiles[i] == null && !run.Rules.Blocked[i]) return i;
            return -1;
        }

        bool Put(Pick p, int tile)
        {
            if (tile < 0 || !p.Is) return false;
            if (p.from == From.Hand) return run.Place(p.handIndex, tile);
            if (!run.Unplace(p.tile)) return false;       // goes to the end of the hand, then on to its new tile
            return run.Place(run.Hand.Count - 1, tile);
        }

        void Update()
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
                ghost.sprite = Make.Lib.Sprite(press.id); ghost.enabled = true;
                if (press.from == From.Tile) tiles[press.tile].Icon.color = Color.white.Alpha(0.3f);
            }
            if (dragging) ghost.transform.position = Px.Snap(new Vector3(w.x, w.y + 6, 0));
            if (PointerInput.Up && press.Is)
            {
                if (dragging)
                {
                    if (freeTile >= 0) changed = Put(press, freeTile);
                    else if (press.from == From.Tile && !tiles[press.tile].Contains(w)) changed = run.Unplace(press.tile);
                    if (!changed && press.from == From.Tile) tiles[press.tile].Icon.color = Color.white;
                }
                else if (press.from == From.Tile) changed = run.Unplace(press.tile);
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
            ShowSelection();
            ShowTooltip(over, w);
        }

        void ShowSelection()
        {
            foreach (var c in cards) c.Selected = selected.from == From.Hand && selected.handIndex == c.handIndex;
            foreach (var s in slots)
            {
                if (s.handIndex < 0) continue;
                s.lit.enabled = selected.from == From.Hand && selected.handIndex == s.handIndex;
                s.icon.color = Color.white.Alpha(dragging && press.from == From.Hand && press.handIndex == s.handIndex ? 0.3f : 1);
            }
        }

        void ShowTooltip(Pick over, Vector2 w)
        {
            string key = null, title = null, body = null; Vector2 anchor = w; float clear = 18;
            if (!dragging && Hud.HoveredCatalyst >= 0 && Hud.HoveredCatalyst < run.Catalysts.Count)
            {
                var c = run.Catalysts[Hud.HoveredCatalyst];
                key = "cat" + c.id; title = c.name + " (" + Rarities.Of(c.rarity) + ")"; body = c.text; anchor = Hud.CatalystAnchor(Hud.HoveredCatalyst);
            }
            else if (!dragging && Hud.HoveredAspect >= 0 && Hud.HoveredAspect < run.Aspects.Count)
            {
                var a = run.Aspects[Hud.HoveredAspect];
                key = "asp" + a.Data.id; title = a.Data.name; body = Wording.AspectRule(a); anchor = Hud.AspectAnchor(Hud.HoveredAspect); clear = 16;
            }
            else if (!dragging && Hud.RedrawHovered)
            {
                key = "redraw"; title = "Redraw"; anchor = Px.At(591, 46);
                body = run.RedrawsLeft > 0 ? "Swap the cards in your hand for new ones. Click to use one." : "None left at this depth.";
            }
            else if (!dragging && over.Is && (over.from == From.Tile || db.Material(over.id).tier > 0))
            {
                var m = db.Material(over.id);
                int known = 0; foreach (var r in db.RecipesContaining(m.id)) if (journal.IsKnown(r.items)) known++;
                key = over.from + over.id + over.tile + over.handIndex; title = m.name;
                // on the bench: the heat a recipe with this tile as its hub would have (itself and all it touches)
                string heat = over.from == From.Tile ? ", heat here " + ChainPlanner.Heat(run.Bench.Tiles, over.tile, db, run.Rules) : "";
                // two lines: the panel has no room for a third
                int ri = over.from == From.Tile ? run.Rules.Ri(db, over.tile, m.id) : m.reactivity;
                body = m.kind + ", tier " + m.tier + " - " + known + " known\nReaction Index " + ri + heat;
                if (over.from == From.Tile) { anchor = tiles[over.tile].centre + new Vector3(0, 12, 0); clear = 22; }
                else { foreach (var s in slots) if (s.handIndex == over.handIndex) anchor = s.pos; clear = 16; }
            }
            else if (!dragging && !over.Is)
            {
                // an empty tile that an aspect or this depth's event acts on says what it does
                for (int i = tiles.Length - 1; i >= 0; i--)
                {
                    if (tileNote[i] == null || !tiles[i].Contains(w)) continue;
                    key = "tile" + i; title = tileNoteTitle[i]; body = tileNote[i]; anchor = tiles[i].centre; clear = 16;
                    break;
                }
            }
            if (key != hoverKey) { hoverKey = key; hoverTime = 0; tip.Hide(); }
            if (key == null) return;
            hoverTime += Time.deltaTime;
            if (hoverTime > 0.3f) tip.Show(title, body, anchor, clear);
        }
    }
}
