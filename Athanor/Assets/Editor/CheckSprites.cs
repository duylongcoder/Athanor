using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class CheckSprites
{
    [MenuItem("Athanor/Check Sprites")]
    public static void Check()
    {
        var guids = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets/Art" });
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>();
            foreach (var s in sprites)
            {
                if (s.name.Contains("worldseed") || s.name.Contains("iron_ore"))
                    Debug.Log($"Sprite: {s.name} from {path}");
            }
        }
    }
}