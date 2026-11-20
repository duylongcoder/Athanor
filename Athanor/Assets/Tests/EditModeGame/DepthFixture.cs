using Athanor.Game;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// The HD-2D rig of the main scene as test data: the numbers Main.unity holds (LayoutPinTests.Depth_MainScene_KeepsTodaysRigSwayAndTable
    /// pins the scene against the same literals). The game code has no copy of them.
    /// </summary>
    public static class DepthFixture
    {
        public const float D = 600f, Front = 660f, Back = 838.2f, TopY = 132f, Rows = 122f, AnchorY = 188f, Near = 50f, Far = 2000f;

        public static TableShape Table => new TableShape(Front, Back, TopY, Rows);

        /// <summary>Adds a DepthRig and a DepthTable (no picture) to root and sets it up the way Main.unity does.</summary>
        public static DepthRig AddRig(GameObject root, Camera worldCamera)
        {
            var table = root.AddComponent<DepthTable>();
            table.distance = D; table.front = Front; table.back = Back; table.topY = TopY; table.rows = Rows;
            var rig = root.AddComponent<DepthRig>();
            rig.worldCamera = worldCamera;
            rig.distance = D; rig.near = Near; rig.far = Far;
            rig.table = table; rig.anchorY = AnchorY;
            return rig;
        }
    }
}
