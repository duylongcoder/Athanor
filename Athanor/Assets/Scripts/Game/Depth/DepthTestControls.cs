using UnityEngine;
using UnityEngine.InputSystem;

namespace Athanor.Game
{
    /// <summary>
    /// Play-mode controls of the depth test scene only: the pointer hovers the nine tiles through the world camera
    /// (DepthRig.BenchPoint), 1..5 pick a depth preset (0 today's look), E toggles the effects, S the sway, K shakes.
    /// Not part of the game: the test scene has no PointerInput.
    /// </summary>
    public sealed class DepthTestControls : MonoBehaviour
    {
        public DepthRig rig;
        public CameraSway sway;
        public DepthSwitch effects;
        public DepthMood mood;
        public TileView[] tiles = new TileView[0];
        /// <summary>The tile under the pointer, or -1.</summary>
        public int Hovered { get; private set; } = -1;

        void Update()
        {
            var k = Keyboard.current;
            if (k != null)
            {
                for (int d = 0; d <= 5; d++) if (k[Key.Digit0 + d].wasPressedThisFrame && mood != null) mood.Set(d);
                if (k.eKey.wasPressedThisFrame && effects != null) effects.Set(!effects.effectsOn);
                if (k.sKey.wasPressedThisFrame && sway != null) sway.swayOn = !sway.swayOn;
                if (k.kKey.wasPressedThisFrame && sway != null) sway.Shake(4, 0.4f);
            }
            var p = Pointer.current;
            Hovered = -1;
            if (p != null && rig != null && rig.uiCamera != null)
            {
                Vector2 ui = rig.uiCamera.ScreenToWorldPoint(p.position.ReadValue());
                if (rig.BenchPoint(ui, out var flat))
                    for (int i = 0; i < tiles.Length; i++) if (tiles[i] != null && tiles[i].Contains(flat)) { Hovered = i; break; }
            }
            for (int i = 0; i < tiles.Length; i++) if (tiles[i] != null) tiles[i].Hovered = i == Hovered;
        }
    }
}
