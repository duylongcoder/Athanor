using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// A line of pixel text with an optional one-pixel drop shadow. A label made from a Lang key (Make.TextKey) rewrites itself when
    /// the language changes; every label swaps its font to the language's.
    /// </summary>
    public sealed class Label : MonoBehaviour
    {
        public TextMeshPro main, shadow;
        /// <summary>The kind and the size (English points) it was made with, kept for font swaps.</summary>
        public FontKind kind;
        public int size;
        [SerializeField] string key;
        (string name, object value)[] args;           // arguments are set at run time by SetKey; never baked
        static readonly (string name, object value)[] NoArgs = new (string, object)[0];
        [SerializeField] int lines = 1;

        /// <summary>Name-role text: how many lines the name has. Crux sits higher in its box than the English face, so Vietnamese names are nudged down 5 px (two lines) or 1 px (one) to land on the English rows.</summary>
        public int Lines { get => lines; set { lines = value; ApplyBias(); } }

        [SerializeField] bool lineBox;
        /// <summary>
        /// Display text placed as the mockup places it, in a line box as tall as its size (the Descent's depth name, SPEC (f)).
        /// Crux's ascent line is short for its size, so in Vietnamese the face moves down by Crux's half-leading at the current
        /// size (11 px at 48, 7 at 32). English is left alone: Alagard's half-leading is under 2 px and the English name already
        /// matches the mock.
        /// </summary>
        public bool LineBox { get => lineBox; set { lineBox = value; ApplyBias(); } }

        /// <summary>Moves the face (and its shadow) down by the font's vertical bias; nothing outside Vietnamese Name and line-box text.</summary>
        public void ApplyBias()
        {
            if (main == null) return;
            bool crux = Loc.Vi && Art.Lib != null && Art.Lib.crux != null;
            float bias = kind == FontKind.Name && crux ? (lines > 1 ? 5 : 1) : 0;
            if (lineBox && crux && main.font == Art.Lib.crux) bias += Make.HalfLeading(Art.Lib.crux.faceInfo, main.fontSize);
            main.transform.localPosition = new Vector3(0, -bias, 0);
            if (shadow != null) shadow.transform.localPosition = new Vector3(1, -1 - bias, 0);
        }

        // Subscribe while enabled, and catch up on enabling: a label under a hidden view never ran Awake/saw Loc.Changed, so it kept
        // the font (PixelOperator8, no Vietnamese) it was built with when the language was switched meanwhile.
        void OnEnable()
        {
            Loc.Changed += Relabel;
            if (main != null) Relabel();
        }

        void OnDisable()
        {
            Loc.Changed -= Relabel;
        }

        /// <summary>Ties the label to a key: the text is rewritten whenever the language changes.</summary>
        public void Bind(string textKey, params (string name, object value)[] textArgs) { key = textKey; args = textArgs; }

        /// <summary>Sets the key and its text together.</summary>
        public void SetKey(string textKey, params (string name, object value)[] textArgs) { Bind(textKey, textArgs); Text = Loc.T(textKey, textArgs); }

        /// <summary>Changes the size (in English points); the font follows the language.</summary>
        public void SetSize(int points)
        {
            size = points;
            main.fontSize = Make.SizeFor(kind, points);
            if (shadow != null) shadow.fontSize = main.fontSize;
            if (lineBox) ApplyBias();
        }

        /// <summary>A view may set text from its own Loc.Changed handler before this label has swapped its font; swap first, so no glyph is looked up in the old one.</summary>
        void SyncFont()
        {
            var font = Make.FontFor(kind);
            if (main.font == font) return;
            main.font = font; if (shadow != null) shadow.font = font;
            main.fontSize = Make.SizeFor(kind, size); if (shadow != null) shadow.fontSize = main.fontSize;
            if (lineBox) ApplyBias();
        }

        void Relabel()
        {
            if (main == null) return;
            var font = Make.FontFor(kind);
            if (main.font != font) main.font = font;
            if (shadow != null && shadow.font != font) shadow.font = font;
            SetSize(size); ApplyBias();
            if (!string.IsNullOrEmpty(key)) Text = Loc.T(key, args ?? NoArgs);
        }

        public string Text
        {
            get => main.text;
            set { if (main.text == value) return; SyncFont(); main.text = value; if (shadow != null) shadow.text = value; }
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
