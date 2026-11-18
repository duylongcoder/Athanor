using System;

namespace Athanor.Game
{
    // The shape of one bench tile in Editor/layout.json (tools/art/export.mjs writes it). The Bench prefab bake reads it once; the
    // positions live in the prefab after that, so the game never loads the file. All numbers are art-space pixels.
    [Serializable] public class RectData { public string name; public int x, y, w, h; }
    [Serializable] public class TileData { public int x, y, w; public RectData face; }
}
