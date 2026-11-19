using System.Collections;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>A sequence that plays to its end unless the player clicks to skip ahead.</summary>
    public abstract class Cutscene : MonoBehaviour
    {
        protected bool Skipped { get; private set; }
        protected bool Playing { get; private set; }

        public abstract IEnumerator Play();

        public void RequestSkip() { if (Playing) Skipped = true; }

        /// <summary>Call at the start of Play and whenever a skip has been dealt with.</summary>
        protected void Begin() { Skipped = false; Playing = true; }
        protected void ClearSkip() => Skipped = false;
        protected void End() => Playing = false;

        protected virtual void OnEnable() => PointerInput.AnyPress += RequestSkip;
        protected virtual void OnDisable() => PointerInput.AnyPress -= RequestSkip;
    }
}
