using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// One cell of the journal's grid: the material's icon with its click. An Uncommon or better starting material also gets a
    /// star at the cell's corner; that row is made when the cell is set up (5 of the 81 cells), not baked into every cell.
    /// </summary>
    public sealed class JournalCell : MonoBehaviour
    {
        public SpriteRenderer icon;
        public Clickable click;
        [NonSerialized] public RarityStars stars;
    }
}
