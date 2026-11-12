using TMPro;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>A line of pixel text with an optional one-pixel drop shadow.</summary>
    public sealed class Label : MonoBehaviour
    {
        public TextMeshPro main, shadow;

        public string Text
        {
            get => main.text;
            set { if (main.text == value) return; main.text = value; if (shadow != null) shadow.text = value; }
        }

        public Color Color { get => main.color; set => main.color = value; }

        public float Alpha
        {
            set
            {
                main.color = main.color.Alpha(value);
                if (shadow != null) shadow.color = shadow.color.Alpha(value);
            }
        }

        public int Order
        {
            set { main.sortingOrder = value; if (shadow != null) shadow.sortingOrder = value - 1; }
        }

        public float Width => main.preferredWidth;
    }
}
