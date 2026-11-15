using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Every sprite, effect sheet, font and material the game looks up by name. Built by Athanor > Build Scene.</summary>
    public sealed class ArtLibrary : ScriptableObject
    {
        public Sprite[] sprites;
        public FlipbookSheet[] sheets;
        public TMP_FontAsset title, body, label, tiny;
        /// <summary>SVN-Coder's Crux, baked at 16: all four text kinds in Vietnamese.</summary>
        public TMP_FontAsset crux;
        public Material lit, unlit, glow;

        Dictionary<string, Sprite> spriteByName;
        Dictionary<string, FlipbookSheet> sheetByName;

        public Sprite Sprite(string name)
        {
            if (spriteByName == null)
            {
                spriteByName = new Dictionary<string, Sprite>();
                foreach (var s in sprites) if (s != null) spriteByName[s.name] = s;
            }
            if (spriteByName.TryGetValue(name, out var sprite)) return sprite;
            Debug.LogError("ArtLibrary has no sprite named " + name);
            return null;
        }

        /// <summary>An exported effect sheet, or null when that effect has not been exported.</summary>
        public FlipbookSheet Sheet(string name)
        {
            if (sheetByName == null)
            {
                sheetByName = new Dictionary<string, FlipbookSheet>();
                if (sheets != null) foreach (var s in sheets) if (s != null) sheetByName[s.name] = s;
            }
            return sheetByName.TryGetValue(name, out var sheet) ? sheet : null;
        }
    }
}
