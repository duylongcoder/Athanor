using System.Collections;
using System.Linq;
using Athanor.Gameplay;
using DG.Tweening;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The choreography of one Ignite: the chain of reactions (flash, draw-in, burst, points), the discovery beat
    /// and cutscene, the flight of what was made to the rack, the spent tiles and the final potency.
    /// The Ignite is settled before this plays (<see cref="RunManager.SettleIgnite"/>); nothing here decides a rule.
    /// All pace, wait and look numbers are fields of <see cref="Timing"/> and <see cref="Look"/>, stored in Main.unity.
    /// </summary>
    public sealed class IgniteSequence : MonoBehaviour
    {
        /// <summary>Stops what this component started so nothing runs on into the next Play (domain reload off).</summary>
        void OnDestroy() { StopAllCoroutines(); Motion.StopOn(this); }

        /// <summary>Paces, waits and durations, in seconds unless the name says otherwise.</summary>
        [System.Serializable]
        public struct Timing
        {
            /// <summary>Pace of a link: fixed when hurried or auto, else max(paceFloor, 1 - n * paceDecay).</summary>
            public float paceHurry, paceFloor, paceDecay;
            /// <summary>Pace of the flights to the rack, hurried or auto, and not.</summary>
            public float endPaceHurry, endPace;
            /// <summary>The discovery cutscene's hurry factor: base + step * min(discoveries so far, cap).</summary>
            public float discoverHurryBase, discoverHurryStep;
            public int discoverHurryCap;
            /// <summary>The held breath before the discovery cutscene.</summary>
            public float discoverBeat;
            /// <summary>Fizzle: wait with and without a hint, and how long the hint text floats.</summary>
            public float fizzleHintWait, fizzleWait, fizzleHintText;
            /// <summary>Sputter: wait and text duration.</summary>
            public float sputterWait, sputterText;
            /// <summary>Reaction: draw-in streak, wait after it, wait at the end, the points mote, the flash and shake lengths.</summary>
            public float drawSeconds, drawWait, reactWait, pointsSeconds, reactShake, reactFlash;
            /// <summary>Floating "+n" text: base and the part that scales with pace; combo and chain text.</summary>
            public float pointsText, pointsTextPace, comboText;
            /// <summary>The discovery's shake length.</summary>
            public float discoverShake;
            /// <summary>Rack flight: pop, fly, hold between them (each times the end pace), stagger between flights.</summary>
            public float rackPop, rackFly, rackHold, rackStagger;
            /// <summary>The wait after the flights: with something kept (times end pace), and without.</summary>
            public float rackWaitKept, rackWaitNone;
            /// <summary>How long the "lost" text of a dropped item floats.</summary>
            public float lostText;
            /// <summary>Chance per frame of a trail spark on a flying icon.</summary>
            public float rackTrailChance;
        }

        /// <summary>Bursts, offsets, sizes and colours.</summary>
        [System.Serializable]
        public struct Look
        {
            public BurstSpec fizzleBurst, sputterBurst, discoverBurst, reactBurst, pointsLand, rackLand;
            /// <summary>Tile flash colours (alpha included): fizzle, sputter.</summary>
            public Color fizzleFlash, sputterFlash;
            /// <summary>Colours multiplied by the material's glow: the draw-in and rack trails; reactBurst.color and rackLand.color too.</summary>
            public Color drawTint, rackTrailTint, pointsTint, pointsTrailTint;
            /// <summary>The glow of a material that has none.</summary>
            public Color defaultGlow;
            /// <summary>Texts: "+n", combo, chain, new hint, spoiled, lost.</summary>
            public Color pointsText, comboText, chainText, hintText, spoiledText, lostText;
            /// <summary>Where a reaction shows relative to its hub, and the offsets of the texts and streaks from it.</summary>
            public Vector3 hubLift, hintTextLift, spoilTileLift, spoilTextLift, drawLift, pointsTextLift, comboTextLift, rackLift, rackUp;
            /// <summary>Where the "lost" text floats.</summary>
            public Vector2 lostAt;
            /// <summary>Burst growth: extra particles per chain link (capped at chainCap links) and per tile beyond a pair; extra speed per link.</summary>
            public int burstPerChain, burstChainCap, burstPerExtra;
            public float speedPerChain;
            /// <summary>Shake: base + perChain * chain + perExtra * (size - 2), capped.</summary>
            public float shakeBase, shakePerChain, shakePerExtra, shakeMax, discoverShake;
            /// <summary>Screen flash for a long chain: starts at this chain / size, strength per step, capped.</summary>
            public int flashChain, flashSize;
            public float flashPerStep, flashMax;
            /// <summary>Trail sparks: jitter and life of a draw-in streak and of the points mote (two layers), sort orders.</summary>
            public float drawJitter, drawLife, pointsJitter, pointsJitter2, pointsLife, pointsLife2, pointsHump, rackJitter, rackLife;
            public int drawOrder, pointsOrder, pointsOrder2, rackTrailOrder;
            /// <summary>The rack icon: halo size, halo alpha, pop scale, order of halo and icon, arc height.</summary>
            public float haloSize, haloAlpha, popScale, arcHeight;
            public int haloOrder, iconOrder;

        }

        [SerializeField] Timing timing;
        [SerializeField] Look look;

        BenchView bench;
        ReactionCutscene reaction;
        FxRefs fx;
        ScreenFlow flow;
        ContentDb db;
        RunState run;
        long shownPotency;

        /// <summary>True once the player pressed during the Ignite: the remaining links play fast. GameManager sets it.</summary>
        public bool Hurry { get; set; }

        /// <summary>Hands the sequence what it plays on.</summary>
        public void Bind(BenchView bench, ReactionCutscene reaction, FxRefs fx, ScreenFlow flow)
        {
            this.bench = bench; this.reaction = reaction; this.fx = fx; this.flow = flow;
        }

        /// <summary>
        /// Plays an Ignite that is already settled: the chain, the flights to the rack, the dropped items and the final
        /// potency, then the tutorial's lines. potencyBefore is the counter's value before the Ignite.
        /// </summary>
        public IEnumerator Play(IgniteOutcome outcome, RunState run, ContentDb db, long potencyBefore, TutorialManager tutorial, bool auto)
        {
            this.run = run; this.db = db;
            Hurry = false;
            shownPotency = potencyBefore;
            int n = 0, shows = 0;
            foreach (var e in outcome.Events)
            {
                float pace = Hurry || auto ? timing.paceHurry : Mathf.Max(timing.paceFloor, 1 - n * timing.paceDecay);
                if (e.Sputter) { bench.Hud.Note(e, db); yield return Sputter(e, pace); }
                else if (e.Result == null) { bench.Hud.Note(e, db); yield return Fizzle(e, pace); if (tutorial != null) yield return tutorial.AfterEvent(e, n); }
                else
                {
                    if (e.Discovery) yield return Discover(e, timing.discoverHurryBase + timing.discoverHurryStep * Mathf.Min(shows++, timing.discoverHurryCap));
                    bench.Hud.Note(e, db);
                    yield return React(e, pace, n);
                    if (tutorial != null) yield return tutorial.AfterEvent(e, n);
                    n++;
                }
            }
            // what the chain made and left standing flies to the rack; everything else on the bench is spent
            int kept = outcome.Made.Count(m => m);
            int slot = Mathf.Max(0, run.Hand.Count(IsDiscovery) - kept);
            float endPace = Hurry || auto ? timing.endPaceHurry : timing.endPace;
            for (int i = 0; i < Gameplay.Bench.TileCount; i++)
            {
                var tile = bench.Tile(i);
                if (tile.Material == null) continue;
                if (outcome.Made[i] && outcome.Final[i] != null)
                {
                    var m = db.Material(outcome.Final[i]);
                    tile.Clear();
                    StartCoroutine(ToRack(m, GlowOf(m), tile.centre + look.rackLift, slot++, endPace));
                    yield return Anim.Wait(timing.rackStagger);
                }
                else StartCoroutine(tile.Consume());
            }
            yield return Anim.Wait(kept > 0 ? timing.rackWaitKept * endPace : timing.rackWaitNone);
            foreach (var id in outcome.Dropped)
                StartCoroutine(bench.FloatText(Loc.T("ui.bench.lost", ("item", db.Material(id).name)), Px.At(look.lostAt.x, look.lostAt.y), look.lostText, timing.lostText));
            shownPotency = run.Potency;
            bench.Hud.SetPotency(shownPotency);
            if (tutorial != null) yield return tutorial.AfterIgnite(new TutorialManager.IgniteOutcomeView { Events = outcome.Events });
        }

        bool IsDiscovery(string id) => db.Material(id).tier > 0;

        Color GlowOf(MaterialData m) => string.IsNullOrEmpty(m.glow) ? look.defaultGlow : Px.Hex(m.glow);

        static Color Mul(Color a, Color b) => new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a);

        /// <summary>Where a reaction shows: over its hub; for a pair that did nothing, between the two.</summary>
        Vector3 Between(ReactionEvent e) => e.Result != null
            ? bench.TileCentre(e.Hub) + look.hubLift
            : (bench.TileCentre(e.Tiles[0]) + bench.TileCentre(e.Tiles[e.Tiles.Length - 1])) / 2 + look.hubLift;

        void FlashTiles(ReactionEvent e, Color c) { foreach (int t in e.Tiles) bench.Tile(t).Flash(c); }

        /// <summary>A pair with no recipe: a grey puff, and a hint if there was something new to say.</summary>
        IEnumerator Fizzle(ReactionEvent e, float pace)
        {
            var mid = Between(e);
            FlashTiles(e, look.fizzleFlash);
            look.fizzleBurst.Play(fx.Sparks, mid);
            Fx.Spawn("poof", mid, null, look.fizzleBurst.order);
            // a plain fizzle is over quickly; one that taught something lingers and says so
            if (e.Hint != null) StartCoroutine(bench.FloatText(Loc.T("ui.bench.new_hint"), mid + look.hintTextLift, look.hintText, timing.fizzleHintText, true));
            yield return Anim.Wait((e.Hint != null ? timing.fizzleHintWait : timing.fizzleWait) * pace);
        }

        /// <summary>A combo that stood ready and lost a material to a livelier reaction: a dull cough over its tiles.</summary>
        IEnumerator Sputter(ReactionEvent e, float pace)
        {
            FlashTiles(e, look.sputterFlash);
            foreach (int t in e.Tiles) look.sputterBurst.Play(fx.Sparks, bench.TileCentre(t) + look.spoilTileLift);
            StartCoroutine(bench.FloatText(Loc.T("ui.bench.spoiled"), bench.TileCentre(e.Hub) + look.spoilTextLift, look.spoiledText, timing.sputterText, true));
            yield return Anim.Wait(timing.sputterWait * pace);
        }

        /// <summary>A recipe nobody has seen before: a beat of held breath on the bench, then the full cutscene.</summary>
        IEnumerator Discover(ReactionEvent e, float hurry)
        {
            Haptics.Pulse(Haptics.Discovery);
            var mid = Between(e);
            FlashTiles(e, Color.white);
            fx.Screen.Shake(look.discoverShake, timing.discoverShake);
            look.discoverBurst.Play(fx.Sparks, mid);
            yield return Anim.Wait(timing.discoverBeat);
            reaction.Setup(e, db, hurry);
            flow.InCutscene = true;
            yield return reaction.Play();
            flow.InCutscene = false;
        }

        /// <summary>
        /// A reaction on the bench: the tiles flash, the neighbours are drawn into the hub, the new material
        /// stands up there (ready for the next link of the chain) and the points fly to the counter.
        /// </summary>
        IEnumerator React(ReactionEvent e, float pace, int chain)
        {
            var m = db.Material(e.Result);
            var colour = GlowOf(m);
            var mid = Between(e);
            int size = e.Tiles.Length;
            FlashTiles(e, colour);
            for (int k = 1; k < size; k++)
            {
                var from = bench.TileCentre(e.Tiles[k]) + look.drawLift;
                StartCoroutine(bench.Tile(e.Tiles[k]).Consume());
                StartCoroutine(Drawn(from, mid, colour, timing.drawSeconds * pace));
            }
            yield return Anim.Wait(timing.drawWait * pace);
            bench.Tile(e.Hub).Clear();
            bench.Tile(e.Hub).Show(m, true);
            var burst = look.reactBurst;
            int links = Mathf.Min(chain, look.burstChainCap);
            burst.count += links * look.burstPerChain + (size - 2) * look.burstPerExtra;
            burst.speed += links * look.speedPerChain;
            burst.color = Mul(colour, burst.color);
            burst.Play(fx.Sparks, mid);
            Fx.Spawn("collect-burst", mid, null, look.reactBurst.order);
            fx.Screen.Shake(Mathf.Min(look.shakeBase + chain * look.shakePerChain + (size - 2) * look.shakePerExtra, look.shakeMax), timing.reactShake);
            if (chain >= look.flashChain || size >= look.flashSize) fx.Screen.Flash(Mathf.Min(look.flashPerStep * (chain + size - 2), look.flashMax), timing.reactFlash);
            string label = "+" + HudView.Number(e.Potency);
            StartCoroutine(bench.FloatText(label, mid + look.pointsTextLift, look.pointsText, timing.pointsText + timing.pointsTextPace * pace));
            if (size >= 3) StartCoroutine(bench.FloatText(Loc.T("ui.bench.combo", ("n", size)), mid + look.comboTextLift, look.comboText, timing.comboText, true));
            else if (chain >= 1) StartCoroutine(bench.FloatText(Loc.T("ui.bench.chain", ("n", chain + 1)), mid + look.comboTextLift, look.chainText, timing.comboText, true));
            StartCoroutine(Points(mid, e.Potency, timing.pointsSeconds * pace));
            yield return Anim.Wait(timing.reactWait * pace);
        }

        /// <summary>A consumed material streaks into the hub.</summary>
        IEnumerator Drawn(Vector3 from, Vector3 to, Color colour, float seconds)
        {
            yield return Anim.Over(seconds, t =>
            {
                var p = Vector3.Lerp(from, to, Anim.InOut(t));
                fx.Sparks.Trail(p, Random.insideUnitCircle * look.drawJitter, Mul(colour, look.drawTint), look.drawLife, look.drawOrder, 0, true);
            });
        }

        /// <summary>A bright mote carries the points to the potency counter; they are added when it lands.</summary>
        IEnumerator Points(Vector3 from, long points, float seconds)
        {
            var to = bench.Hud.PotencyPosition;
            yield return Anim.Over(seconds, t =>
            {
                var p = Vector3.Lerp(from, to, Anim.InOut(t)) + new Vector3(0, Anim.Hump(t) * look.pointsHump, 0);
                fx.Sparks.Trail(p, Random.insideUnitCircle * look.pointsJitter, look.pointsTint, look.pointsLife, look.pointsOrder, 0, true);
                fx.Sparks.Trail(p, Random.insideUnitCircle * look.pointsJitter2, look.pointsTrailTint, look.pointsLife2, look.pointsOrder2);
            });
            shownPotency += points;
            bench.Hud.SetPotency(shownPotency);
            bench.Hud.Bump();
            look.pointsLand.Play(fx.Sparks, to);
        }

        /// <summary>The new material pops out between the two tiles, then arcs down into its well on the rack.</summary>
        IEnumerator ToRack(MaterialData m, Color colour, Vector3 from, int slot, float pace)
        {
            var halo = Spawn.Glow(bench.transform, from, look.haloSize, colour.Alpha(look.haloAlpha), look.haloOrder);
            var icon = Spawn.Sprite(bench.transform, m.id, from, look.iconOrder);
            float pop = timing.rackPop * pace, fly = timing.rackFly * pace;
            var up = from + look.rackUp;
            yield return Motion.Seq()
                .Group(Motion.Scale(icon.transform, 0, look.popScale, pop, Ease.OutBack))
                .Group(Motion.Move(icon.transform, from, up, pop))
                .Group(Motion.Move(halo.transform, from, up, pop))
                .ToYieldInstruction();
            yield return Anim.Wait(timing.rackHold * pace);
            Vector3 start = icon.transform.position, to = bench.RackSlot(slot);
            var flight = Motion.Seq()
                .Group(Motion.Arc(icon.transform, start, to, look.arcHeight, fly, Ease.InOutSine))
                .Group(Motion.Arc(halo.transform, start, to, look.arcHeight, fly, Ease.InOutSine))
                .Group(Motion.Scale(icon.transform, look.popScale, 1, fly, Ease.InOutSine))
                .Group(Motion.Fade(halo, look.haloAlpha, 0, fly));
            while (flight.IsActive())
            {
                if (Random.value < timing.rackTrailChance) fx.Sparks.Trail(icon.transform.position, Random.insideUnitCircle * look.rackJitter, Mul(colour, look.rackTrailTint), look.rackLife, look.rackTrailOrder);
                yield return null;
            }
            Destroy(icon.gameObject); Destroy(halo.gameObject);
            bench.PreviewOnRack(slot, m.id);
            var land = look.rackLand;
            land.color = Mul(colour, land.color);
            land.Play(fx.Sparks, to);
        }
    }
}
