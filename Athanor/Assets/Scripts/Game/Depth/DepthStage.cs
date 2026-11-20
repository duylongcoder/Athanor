using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The HD-2D bench of the main scene as the game sees it: the rig (world camera, UI camera), the sway, the per-depth
    /// moods and the effects switch, all objects of Main.unity. GameFlow talks to this and nothing else of the depth layer.
    /// Cutscenes stay flat (z = 0): <see cref="SetFlat"/> turns the world camera off and lets the UI camera draw the frame.
    /// </summary>
    public sealed class DepthStage : MonoBehaviour
    {
        public DepthRig rig;
        public CameraSway sway;
        public DepthMood mood;
        public DepthSwitch effects;
        public WindowView window;

        /// <summary>Puts the nine tiles at their row's depth on the table and on the world layer.</summary>
        public void PlaceTiles(BenchView bench, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var t = bench.Tile(i);
                foreach (var tr in t.GetComponentsInChildren<Transform>(true)) tr.gameObject.layer = DepthRig.WorldLayer;
                float artY = Px.H / 2f - t.centre.y;
                DepthLayer.AddGroup(t.gameObject, DepthMath.TableZ(artY, rig.distance, rig.table.Shape) + 0.2f).Apply(rig.distance);
            }
        }

        /// <summary>Flat mode for a cutscene (true) and back to the bench (false).</summary>
        public void SetFlat(bool flat) { rig.Flat = flat; }

        /// <summary>Lighting, post effects, fog and window view for a depth (1..5).</summary>
        public void ShowMood(int depth, bool themedWindow)
        {
            if (mood != null) mood.Set(Mathf.Clamp(depth, 1, 5), themedWindow);
        }

        /// <summary>The look of a depth (1..5; others are clamped): ambient colour, moon, accent.</summary>
        public DepthMood.Preset Look(int depth) => mood.presets[Mathf.Clamp(depth, 1, mood.presets.Length) - 1];

        /// <summary>The Settings switch: the idle sway on or off (the shake stays).</summary>
        public bool SwayOn { get => sway.swayOn; set => sway.swayOn = value; }
    }
}
