using System;

namespace Athanor.Game
{
    // Shapes of Resources/layout.json, written by tools/art/export.mjs. All numbers are art-space pixels.
    [Serializable] public class RectData { public string name; public int x, y, w, h; }
    [Serializable] public class TileData { public int x, y, w; public RectData face; }
    [Serializable] public class PointData { public int x, y; }

    [Serializable]
    public class LayoutData
    {
        public int w, h;
        public TileData[] tiles;
        public RectData[] placed;
        public PointData[] candles;

        /// <summary>Where a cropped full-frame layer (such as "workshop_table") sits.</summary>
        public RectData Placed(string name)
        {
            foreach (var r in placed) if (r.name == name) return r;
            throw new ArgumentException("layout has no entry for " + name);
        }
    }
}
