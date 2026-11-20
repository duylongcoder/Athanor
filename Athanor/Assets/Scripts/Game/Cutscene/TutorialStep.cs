using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// One tutorial step, a child of the Tutorial object. Holds the text key, the dim and lock options, and its areas (child objects).
    /// The step id is the object's name (e.g. "1.4", "5.4i").
    /// </summary>
    public sealed class TutorialStep : MonoBehaviour
    {
        [Tooltip("Lang key of the text, e.g. tutorial.1_4. Two steps may share a key (5.4 and 5.4i).")]
        public string textKey = "";
        [Tooltip("Dim the screen outside the areas.")] public bool dark = true;
        [Tooltip("The lock pad is on: clicks on the dark pulse the rim, and clicks in pass-through areas reach the game.")] public bool lockPad = true;
        [Tooltip("Read steps only: a click in a non-pass area advances the text. Off: it only pulses the rim.")] public bool clickAdvances = true;
        [Tooltip("Optional text box position for this step (art pixels, top-left). Off = TutorialStyle.boxPos.")] public bool useBoxPos;
        public Vector2 boxPos;

        /// <summary>The areas in child order; the arrow sits over the last one.</summary>
        public List<TutorialArea> Areas()
        {
            var list = new List<TutorialArea>();
            foreach (Transform c in transform)
            {
                var a = c.GetComponent<TutorialArea>();
                if (a != null && a.isActiveAndEnabled) list.Add(a);
            }
            return list;
        }
    }
}
