using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.U2D;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

/// <summary>
/// Two art steps that run after tools/art/export.mjs, both idempotent:
/// 1. Sprite sheets: the numbered frames of each animation (Background/flame_small_0..7.png ...) are merged into one
///    Multiple-mode sheet beside them (Background/flame_small.png). Sprite names stay flame_small_0..7, so the art
///    library and Flame keep finding them by name. Scene and asset references to the old frames are moved to the
///    sheet's sprites, and the frame files are deleted only after the sheet is checked pixel for pixel.
/// 2. Sprite atlases: one Assets/Art/Atlases/NAME.spriteatlasv2 per group in <see cref="Groups"/>, uncompressed,
///    point filtered, no mips, 2 px padding, no rotation, no tight packing, included in the build.
/// Menu: Athanor > Build Sprite Sheets and Atlases. Command line: -executeMethod AtlasBuilder.BuildBatch
/// </summary>
public static class AtlasBuilder
{
    public const string ArtDir = "Assets/Art", AtlasDir = "Assets/Art/Atlases", LibraryPath = "Assets/Art/ArtLibrary.asset";
    public const int Padding = 2, MaxSize = 2048, SheetGap = 2;

    /// <summary>The HD-2D depth layers (Phase 5b). They stay out of every atlas: they are placed as plain sprites
    /// in the HD-2D bench of Main.unity.</summary>
    public const string DepthDir = "Assets/Art/Art/Depth";

    /// <summary>True for an asset under <see cref="DepthDir"/>.</summary>
    public static bool IsDepthArt(string assetPath) => assetPath.StartsWith(DepthDir + "/");

    /// <summary>Numbered frame sets that are animations (merged into one sheet each). tile_face_N and tile_ring_N are
    /// numbered too, but they are one picture per tile and row, not frames, so they stay separate sprites.</summary>
    public static readonly string[] Animations = { "flame_small", "flame_big", "liquid" };

    /// <summary>True for a merged animation sheet: SpriteImportRules leaves it in Multiple mode.</summary>
    public static bool IsSpriteSheet(string assetPath) => Animations.Contains(Path.GetFileNameWithoutExtension(assetPath));

    /// <summary>Textures that stay out of every atlas.</summary>
    public static readonly Dictionary<string, string> Standalone = new Dictionary<string, string>
    {
        { "moon_shaft", "Light2D sprite cookie (Moonbeam), not drawn as a sprite" },
    };

    static readonly string[] BenchUi = { "rack", "rack_slot_lit", "cross", "mark" };

    /// <summary>Atlas groups by what the game uses the art for. Each texture under Assets/Art goes to the first group
    /// whose rule matches (folder relative to Assets/Art, texture name).</summary>
    public static readonly (string name, string rule, Func<string, string, bool> match)[] Groups =
    {
        ("Bench", "Fx/tile_face_*, Fx/tile_ring_*, and the bench UI: rack, rack_slot_lit, cross, mark",
            (dir, n) => (dir == "Fx" && (n.StartsWith("tile_face_") || n.StartsWith("tile_ring_"))) || (dir == "Art/UI" && BenchUi.Contains(n))),
        ("UI", "the rest of Art/UI (HUD, buttons, cards, panels, journal, goals, dialogs)", (dir, n) => dir == "Art/UI"),
        ("CatalystIcons", "Icons/cat_*", (dir, n) => dir == "Art/Icons" && n.StartsWith("cat_")),
        ("AspectIcons", "Icons/asp_*", (dir, n) => dir == "Art/Icons" && n.StartsWith("asp_")),
        ("EventIcons", "Icons/evt_*", (dir, n) => dir == "Art/Icons" && n.StartsWith("evt_")),
        ("ItemIcons", "the rest of Art/Icons (one per material)", (dir, n) => dir == "Art/Icons"),
        ("Fx", "the rest of Art/Fx (dot, glow, sparkle, rune, rings, rays, pillar, liquid sheet), the flame sheets, Fx/Sheets/*",
            (dir, n) => dir == "Fx" || dir == "Fx/Sheets" || (dir == "Background" && n.StartsWith("flame_"))),
        ("Cutscene", "cutscene room and its props: cutscene_room, cauldron, logs",
            (dir, n) => dir == "Background" && (n == "cutscene_room" || n == "cauldron" || n == "logs")),
        ("Background", "the rest of Art/Background (workshop_wall, window, workshop_table, candle)", (dir, n) => dir == "Background"),
    };

    [MenuItem("Athanor/Build Sprite Sheets and Atlases")]
    public static string BuildAll()
    {
        var report = new StringBuilder();
        report.AppendLine(BuildSpriteSheets());
        var lib = ArtLibraryBuilder.RefreshLibrary();
        report.AppendLine("Art library: " + lib.sprites.Length + " sprites.");
        report.AppendLine(BuildAtlases());
        Debug.Log(report.ToString());
        return report.ToString();
    }

    /// <summary>From the command line (Unity closed): -executeMethod AtlasBuilder.BuildBatch. Exits 1 on any error.</summary>
    public static void BuildBatch()
    {
        try { BuildAll(); }
        catch (Exception e) { Debug.LogException(e); if (Application.isBatchMode) EditorApplication.Exit(1); }
    }

    /// <summary>Packs every atlas for the active build target and reports each atlas's page sizes and fill.</summary>
    [MenuItem("Athanor/Pack Sprite Atlases")]
    public static string PackAndReport()
    {
        SpriteAtlasUtility.PackAllAtlases(EditorUserBuildSettings.activeBuildTarget, false);
        var lines = new List<string>();
        foreach (var path in AtlasPaths())
        {
            var atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(path);
            var pages = PreviewTextures(atlas);
            var sprites = new Sprite[atlas.spriteCount];
            atlas.GetSprites(sprites);
            long used = sprites.Sum(s => (long)(s.rect.width * s.rect.height));
            long area = pages.Sum(p => (long)p.width * p.height);
            lines.Add(Path.GetFileNameWithoutExtension(path) + ": " + atlas.spriteCount + " sprites, " +
                (pages.Length == 0 ? "no page" : string.Join(" + ", pages.Select(p => p.width + "x" + p.height + " " + p.format))) +
                (area > 0 ? ", fill " + (100 * used / area) + "%" : ""));
        }
        string text = "Packed " + lines.Count + " atlases:\n" + string.Join("\n", lines);
        Debug.Log(text);
        return text;
    }

    public static void PackBatch()
    {
        try { PackAndReport(); }
        catch (Exception e) { Debug.LogException(e); if (Application.isBatchMode) EditorApplication.Exit(1); }
    }

    static IEnumerable<string> AtlasPaths() => Groups.Select(g => AtlasDir + "/" + g.name + ".spriteatlasv2").Where(File.Exists);

    static Texture2D[] PreviewTextures(SpriteAtlas atlas)
    {
        var m = typeof(SpriteAtlasExtensions).GetMethod("GetPreviewTextures", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        return m != null ? (Texture2D[])m.Invoke(null, new object[] { atlas }) ?? new Texture2D[0] : new Texture2D[0];
    }

    // ---------------------------------------------------------------- sprite sheets

    /// <summary>Merges each animation's frame PNGs into one sheet. Nothing to do when no loose frames are left.</summary>
    public static string BuildSpriteSheets()
    {
        AssetDatabase.Refresh();
        var done = new List<string>();
        var frameRx = new Regex(@"^(.+)_(\d+)$");
        var sets = Directory.GetFiles(ArtDir, "*.png", SearchOption.AllDirectories)
            .Select(p => p.Replace('\\', '/'))
            .Select(p => (path: p, m: frameRx.Match(Path.GetFileNameWithoutExtension(p))))
            .Where(x => x.m.Success && Animations.Contains(x.m.Groups[1].Value))
            .GroupBy(x => Path.GetDirectoryName(x.path).Replace('\\', '/') + "/" + x.m.Groups[1].Value);
        foreach (var set in sets)
        {
            string anim = Path.GetFileName(set.Key), sheetPath = set.Key + ".png";
            var frames = set.Select(x => (x.path, index: int.Parse(x.m.Groups[2].Value))).OrderBy(x => x.index).ToArray();
            if (!frames.Select(f => f.index).SequenceEqual(Enumerable.Range(0, frames.Length)))
                throw new Exception(anim + ": frames are not numbered 0.." + (frames.Length - 1));
            done.Add(MergeSheet(anim, sheetPath, frames.Select(f => f.path).ToArray()));
        }
        return done.Count == 0 ? "Sprite sheets: up to date (no loose animation frames)." : "Sprite sheets:\n" + string.Join("\n", done);
    }

    static Texture2D ReadPng(string path)
    {
        var t = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        if (!t.LoadImage(File.ReadAllBytes(path))) throw new Exception("Cannot read " + path);
        return t;
    }

    static string MergeSheet(string anim, string sheetPath, string[] framePaths)
    {
        var images = framePaths.Select(ReadPng).ToArray();
        int w = images[0].width, h = images[0].height, n = images.Length;
        if (images.Any(t => t.width != w || t.height != h)) throw new Exception(anim + ": frames differ in size");

        // one row, frame k at x = k * (w + gap), transparent gaps so no frame touches another
        int sheetW = n * w + (n - 1) * SheetGap;
        var sheet = new Texture2D(sheetW, h, TextureFormat.RGBA32, false);
        sheet.SetPixels32(new Color32[sheetW * h]);
        for (int k = 0; k < n; k++) sheet.SetPixels32(k * (w + SheetGap), 0, w, h, images[k].GetPixels32());
        sheet.Apply();
        File.WriteAllBytes(sheetPath, sheet.EncodeToPNG());
        AssetDatabase.ImportAsset(sheetPath, ImportAssetOptions.ForceUpdate);

        // slice, keeping the sprite IDs of an earlier run so references to the sheet's sprites stay valid
        var importer = (TextureImporter)AssetImporter.GetAtPath(sheetPath);
        importer.spriteImportMode = SpriteImportMode.Multiple;
        var factory = new SpriteDataProviderFactories();
        factory.Init();
        var provider = factory.GetSpriteEditorDataProviderFromObject(importer);
        provider.InitSpriteEditorDataProvider();
        var oldIds = provider.GetSpriteRects().GroupBy(r => r.name).ToDictionary(g => g.Key, g => g.First().spriteID);
        var rects = Enumerable.Range(0, n).Select(k => new SpriteRect
        {
            name = anim + "_" + k,
            spriteID = oldIds.TryGetValue(anim + "_" + k, out var id) ? id : GUID.Generate(),
            rect = new Rect(k * (w + SheetGap), 0, w, h),
            alignment = SpriteAlignment.Center,
            pivot = new Vector2(0.5f, 0.5f),
        }).ToArray();
        provider.SetSpriteRects(rects);
        provider.GetDataProvider<ISpriteNameFileIdDataProvider>().SetNameFileIdPairs(rects.Select(r => new SpriteNameFileIdPair(r.name, r.spriteID)));
        provider.Apply();
        importer.SaveAndReimport();

        // check the sheet before anything is deleted: names, rects, pivots and every pixel
        var sprites = AssetDatabase.LoadAllAssetsAtPath(sheetPath).OfType<Sprite>().ToDictionary(s => s.name);
        var onDisk = ReadPng(sheetPath);
        for (int k = 0; k < n; k++)
        {
            string name = anim + "_" + k;
            if (!sprites.TryGetValue(name, out var s)) throw new Exception(sheetPath + " has no sprite " + name);
            if (s.rect != rects[k].rect || Mathf.Abs(s.pivot.x - w / 2f) > 0.01f || Mathf.Abs(s.pivot.y - h / 2f) > 0.01f || s.pixelsPerUnit != 1)
                throw new Exception(name + ": rect, pivot or pixels per unit is wrong in " + sheetPath);
            var a = onDisk.GetPixels32();
            var b = images[k].GetPixels32();
            int x0 = k * (w + SheetGap);
            for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                var p = a[y * sheetW + x0 + x]; var q = b[y * w + x];
                if (p.r != q.r || p.g != q.g || p.b != q.b || p.a != q.a) throw new Exception(name + ": pixel " + x + "," + y + " differs in " + sheetPath);
            }
        }
        if (sprites.Count != n) throw new Exception(sheetPath + " has " + sprites.Count + " sprites, expected " + n);

        // move references from the frame textures' sprites to the sheet's sprites, then delete the frames
        var map = new Dictionary<Sprite, Sprite>();
        for (int k = 0; k < n; k++)
        {
            var old = AssetDatabase.LoadAssetAtPath<Sprite>(framePaths[k]);
            if (old != null) map[old] = sprites[anim + "_" + k];
        }
        int moved = Retarget(map);
        foreach (var p in framePaths) if (!AssetDatabase.DeleteAsset(p)) throw new Exception("Could not delete " + p);
        return "  " + anim + ": " + n + " frames of " + w + "x" + h + " -> " + sheetPath + " (" + sheetW + "x" + h + "), " + moved + " references moved";
    }

    /// <summary>Points every reference to an old sprite at its new sprite: in loaded scenes through SerializedObject,
    /// in scene, prefab and asset files on disk by swapping the GUID and file ID. The art library is rebuilt
    /// afterwards by ArtLibraryBuilder.RefreshLibrary, so it is left alone here.</summary>
    static int Retarget(Dictionary<Sprite, Sprite> map)
    {
        int moved = 0;
        var loaded = new HashSet<string>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (!scene.isLoaded) continue;
            loaded.Add(scene.path);
            bool wasDirty = scene.isDirty;
            int here = 0;
            foreach (var root in scene.GetRootGameObjects())
            foreach (var c in root.GetComponentsInChildren<Component>(true))
            {
                if (c == null) continue;
                var so = new SerializedObject(c);
                var it = so.GetIterator();
                bool changed = false;
                while (it.Next(true))
                    if (it.propertyType == SerializedPropertyType.ObjectReference && it.objectReferenceValue is Sprite s && map.TryGetValue(s, out var to))
                    { it.objectReferenceValue = to; changed = true; here++; }
                if (changed) so.ApplyModifiedPropertiesWithoutUndo();
            }
            if (here > 0)
            {
                EditorSceneManager.MarkSceneDirty(scene);
                if (!wasDirty && !string.IsNullOrEmpty(scene.path)) EditorSceneManager.SaveScene(scene);
                else Debug.LogWarning(scene.path + " had unsaved changes: " + here + " sprite references were moved in memory; save the scene to keep them.");
            }
            moved += here;
        }

        var swaps = new List<(string from, string to)>();
        foreach (var kv in map)
        {
            if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(kv.Key, out string og, out long oid) ||
                !AssetDatabase.TryGetGUIDAndLocalFileIdentifier(kv.Value, out string ng, out long nid)) continue;
            swaps.Add(("fileID: " + oid + ", guid: " + og, "fileID: " + nid + ", guid: " + ng));
        }
        var files = Directory.GetFiles("Assets", "*.*", SearchOption.AllDirectories)
            .Where(p => p.EndsWith(".unity") || p.EndsWith(".prefab") || p.EndsWith(".asset") || p.EndsWith(".mat") || p.EndsWith(".anim") || p.EndsWith(".controller"))
            .Select(p => p.Replace('\\', '/'))
            .Where(p => p != LibraryPath && !loaded.Contains(p));
        foreach (var f in files)
        {
            string text = File.ReadAllText(f), was = text;
            foreach (var (from, to) in swaps)
            {
                int count = (text.Length - text.Replace(from, "").Length) / from.Length;
                if (count > 0) { text = text.Replace(from, to); moved += count; }
            }
            if (text != was) { File.WriteAllText(f, text); AssetDatabase.ImportAsset(f); }
        }
        return moved;
    }

    // ---------------------------------------------------------------- atlases

    /// <summary>The atlas group of each texture under Assets/Art (null when standalone). Throws on art no rule takes.</summary>
    public static Dictionary<string, List<string>> Assign()
    {
        var result = Groups.ToDictionary(g => g.name, g => new List<string>());
        var unassigned = new List<string>();
        foreach (var guid in AssetDatabase.FindAssets("t:Texture2D", new[] { ArtDir }))
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (IsDepthArt(path)) continue;
            if (!(AssetImporter.GetAtPath(path) is TextureImporter ti) || ti.textureType != TextureImporterType.Sprite) continue;
            string dir = Path.GetDirectoryName(path).Replace('\\', '/').Substring(ArtDir.Length).TrimStart('/');
            // Remove redundant 'Art/' prefix for files under Assets/Art/Art/* (commit 95c8be51 moved UI/Fx/etc one level deeper)
            if (dir.StartsWith("Art/")) dir = dir.Substring("Art/".Length);
            string name = Path.GetFileNameWithoutExtension(path);
            if (Standalone.ContainsKey(name)) continue;
            var g = Groups.FirstOrDefault(x => x.match(dir, name));
            if (g.name == null) unassigned.Add(path); else result[g.name].Add(path);
        }
        if (unassigned.Count > 0) throw new Exception("No atlas group for: " + string.Join(", ", unassigned) + ". Add a rule in AtlasBuilder.Groups or list it in Standalone.");
        foreach (var l in result.Values) l.Sort(StringComparer.Ordinal);
        return result;
    }

    public static string BuildAtlases()
    {
        Directory.CreateDirectory(AtlasDir);
        var assignment = Assign();
        var lines = new List<string>();
        foreach (var g in Groups)
        {
            string path = AtlasDir + "/" + g.name + ".spriteatlasv2";
            var textures = assignment[g.name].Select(AssetDatabase.LoadAssetAtPath<Texture2D>).Where(t => t != null).ToArray();
            if (textures.Length == 0) { lines.Add("  " + g.name + ": empty, skipped"); continue; }

            // the packable list is rewritten from the rules every time, so a rerun gives the same file
            var asset = new SpriteAtlasAsset();
            asset.Add(textures.Cast<Object>().ToArray());
            SpriteAtlasAsset.Save(asset, path);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            var importer = (SpriteAtlasImporter)AssetImporter.GetAtPath(path);
            importer.includeInBuild = true;
            importer.packingSettings = new SpriteAtlasPackingSettings
            {
                blockOffset = 1, padding = Padding, enableRotation = false, enableTightPacking = false, enableAlphaDilation = false,
            };
            importer.textureSettings = new SpriteAtlasTextureSettings
            {
                readable = false, generateMipMaps = false, sRGB = true, filterMode = FilterMode.Point, anisoLevel = 0,
            };
            foreach (var platform in new[] { "DefaultTexturePlatform", "WebGL", "Standalone" })
            {
                var ps = importer.GetPlatformSettings(platform);
                ps.name = platform;
                ps.overridden = platform != "DefaultTexturePlatform";
                ps.maxTextureSize = MaxSize;
                ps.format = TextureImporterFormat.RGBA32;
                ps.textureCompression = TextureImporterCompression.Uncompressed;
                ps.crunchedCompression = false;
                importer.SetPlatformSettings(ps);
            }
            importer.SaveAndReimport();
            lines.Add("  " + g.name + ": " + textures.Length + " textures");
        }
        AssetDatabase.SaveAssets();
        return "Sprite atlases in " + AtlasDir + ":\n" + string.Join("\n", lines) +
            "\n  standalone: " + string.Join(", ", Standalone.Keys);
    }
}
