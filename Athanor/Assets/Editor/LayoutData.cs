using System;
using Athanor.Game;

/// <summary>
/// The shapes of Editor/layout.json, written by tools/art/export.mjs: the nine tiles and where the workshop art sits. Editor only,
/// so the game build carries no layout file. All numbers are art-space pixels.
/// </summary>
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

    public const string Path = "Assets/Editor/layout.json";
}
