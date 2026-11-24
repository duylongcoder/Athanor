using System.Collections.Generic;
using System.Linq;

namespace Athanor.Gameplay
{
    /// <summary>Small deterministic RNG so shuffles match between Unity and the dotnet test run.</summary>
    public sealed class Rng
    {
        uint s;
        public Rng(int seed) { s = (uint)seed * 2654435761u + 0x9E3779B9u; if (s == 0) s = 1; }
        public uint Next() { s ^= s << 13; s ^= s >> 17; s ^= s << 5; return s; }
        public int Range(int n) => n <= 0 ? 0 : (int)(Next() % (uint)n);
        public void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--) { int j = Range(i + 1); var t = list[i]; list[i] = list[j]; list[j] = t; }
        }
    }

    /// <summary>The player's deck of starting materials: a draw pile, a discard pile, and the cards currently out.</summary>
    public sealed class Satchel
    {
        readonly List<string> draw = new List<string>();   // last element is the top
        readonly List<string> discard = new List<string>();
        readonly List<string> outCards = new List<string>();
        readonly Rng rng;

        public Satchel(IEnumerable<string> cards, int seed)
        {
            rng = new Rng(seed);
            draw.AddRange(cards);
            rng.Shuffle(draw);
        }

        public int Total => draw.Count + discard.Count + outCards.Count;

        public List<string> Draw(int n)
        {
            var hand = new List<string>();
            while (hand.Count < n)
            {
                if (draw.Count == 0)
                {
                    if (discard.Count == 0) break;
                    draw.AddRange(discard); discard.Clear(); rng.Shuffle(draw);
                }
                var top = draw[draw.Count - 1]; draw.RemoveAt(draw.Count - 1);
                hand.Add(top); outCards.Add(top);
            }
            return hand;
        }

        /// <summary>A card that is out (in the hand or on the bench) leaves the satchel for good.</summary>
        public void Lose(string id) => outCards.Remove(id);

        public void Discard(IEnumerable<string> ids)
        {
            foreach (var id in ids.ToList()) { outCards.Remove(id); discard.Add(id); }
        }

        /// <summary>Copies of a card in the draw and discard piles (not the ones out).</summary>
        public int Available(string id) => draw.Count(c => c == id) + discard.Count(c => c == id);

        /// <summary>
        /// Takes a named card out of the draw pile, or else the discard pile, as if it had been drawn (the tutorial's
        /// scripted hands). False, and nothing changed, when neither pile has a copy.
        /// </summary>
        public bool Take(string id)
        {
            var pile = draw.Contains(id) ? draw : discard.Contains(id) ? discard : null;
            if (pile == null) return false;
            pile.RemoveAt(pile.LastIndexOf(id));
            outCards.Add(id);
            return true;
        }
    }
}
