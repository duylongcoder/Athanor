using System;
using UnityEngine;
using Athanor.Core;

namespace Athanor.Data
{
    /// <summary>Bench layout variants — defines per-tile modifiers and link pairs.</summary>
    [CreateAssetMenu(fileName = "BenchLayoutsSO.asset", menuName = "Athanor/Bench Layouts")]
    public class BenchLayoutsSO : ScriptableObject
    {
        [Serializable]
        public class BenchLayoutEntry
        {
            [Tooltip("Unique identifier for this layout variant.")]
            public string id = "";

            [Tooltip("Display name for UI.")]
            public string name = "";

            [Header("Tile Modifiers (9 entries for 3x3 bench)")]
            [Tooltip("Whether each tile is blocked (true = impassable).")]
            public bool[] Blocked = new bool[9];

            [Tooltip("Bonus RI added to each tile (index = tile position 0-8).")]
            public int[] RiBonus = new int[9];

            [Tooltip("Whether each tile is a Kiln tile.")]
            public bool[] Kiln = new bool[9];

            [Tooltip("Whether each tile is a Crossroads tile.")]
            public bool[] Crossroads = new bool[9];

            [Tooltip("Multiplier applied to hub scoring on each tile.")]
            public int[] HubMult = new int[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            [Tooltip("Whether each tile is Heavy.")]
            public bool[] Heavy = new bool[9];

            [Tooltip("Attuned material kind per tile (empty = none).")]
            public string[] AttunedKind = new string[9];

            [Tooltip("Bonus value for Seed effect per tile.")]
            public int[] SeedBonus = new int[9];

            [Header("Link Pairs")]
            [Tooltip("Extra connections between tiles (pairs of tile indices).")]
            public LinkPair[] Links = Array.Empty<LinkPair>();

            [Tooltip("Ri shift modifier for this layout.")]
            public int RiShift = 0;

            [Tooltip("Heat bonus added globally.")]
            public int HeatPlus = 0;

            [Tooltip("Pivot tile index (-1 = no pivot).")]
            public int Pivot = -1;

            [Tooltip("Whether diagonal adjacency counts.")]
            public bool Diagonals = false;
        }

        [SerializeField] private BenchLayoutEntry[] layouts;

        public BenchLayoutEntry[] Layouts => layouts;

        private void OnValidate()
        {
            layouts ??= Array.Empty<BenchLayoutEntry>();
            foreach (var layout in layouts)
            {
                if (layout == null) continue;
                ValidateArray(layout.Blocked, nameof(layout.Blocked), 9);
                ValidateArray(layout.RiBonus, nameof(layout.RiBonus), 9);
                ValidateArray(layout.Kiln, nameof(layout.Kiln), 9);
                ValidateArray(layout.Crossroads, nameof(layout.Crossroads), 9);
                ValidateArray(layout.HubMult, nameof(layout.HubMult), 9);
                ValidateArray(layout.Heavy, nameof(layout.Heavy), 9);
                ValidateArray(layout.AttunedKind, nameof(layout.AttunedKind), 9);
                ValidateArray(layout.SeedBonus, nameof(layout.SeedBonus), 9);
            }
        }

        private static void ValidateArray<T>(T[] arr, string name, int expected)
        {
            if (arr != null && arr.Length != expected)
                Debug.LogError($"[BenchLayoutsSO] {name} array length ({arr.Length}) != {expected}.", null);
        }
    }
}
