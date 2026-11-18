# Athanor Phase 6 — Unity-native refactor and cleanup: implementation plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Close Phase 5. Turn the Game layer into scene + prefabs + Inspector references. Empty the lint baseline. Delete outdated
material. Merge the project docs into `docs/ATHANOR.md`. Regenerate the graphify map of `Athanor/`.

**Architecture:** `Athanor.Core` is untouched. Each Game view is baked once from its existing `Build(...)` into a prefab with a
temporary Editor tool driven by the `unity` CLI. After that, the view only gets `[SerializeField]` references and one `Bind(...)`
entry point that hooks clicks, and its `Build(...)` is deleted. `Main.unity` becomes the hand-edited source of truth.
Spec: `docs/superpowers/specs/2026-11-17-athanor-phase6-unity-refactor-design.md`.

**Tech Stack:** Unity 6000.6.7f1 (URP 2D), C#, NUnit (Unity Test Framework + `dotnet test tools/CoreTests`), PrimeTween, TextMeshPro,
`unity` CLI 1.0.0, Node 22 headless-Chrome checks in `tools/web/`, Python 3 lint (`tools/lint/unity_lint.py`).

---

## Ground rules for every task

- **The owner commits. Agents never commit.** Each task ends with a *Checkpoint*: stop, report, and give the suggested
  Conventional Commit message (body: `Story: P6-<task number>`).
- **Unity batch commands need the Editor closed** (project lock). Every `unity run` / `unity test` / `unity build` below assumes that.
- Shell: the repo root is `/Users/bunker/Desktop/New Folder`. Core tests need `DOTNET_ROLL_FORWARD=Major` on this machine
  (`tools/ci/run.sh` exports it).
- `node tools/art/export.mjs` must **not** be run (art drift, see `production/session-state/active.md`).
- No new lint findings at any time. `tools/ci/run.sh lint` uses the baseline, so a new finding fails at once.

### Commands used throughout

| Name | Command | Pass when |
|---|---|---|
| CI | `tools/ci/run.sh all` | exit 0 |
| EditMode | `unity test Athanor --mode EditMode` | 0 failed |
| Bake | `unity run Athanor -- -executeMethod ViewBaker.<Method>` | log line `[phase6] baked <Name>`, exit 0 |
| Compare | `unity run Athanor -- -executeMethod ViewBaker.Compare<Name>` | log line `[phase6] <Name> 0.0xx % px differ`, exit 0 (it throws above 0.1 %) |
| DevBuild | `unity build Athanor --target WebGL --execute-method WebBuild.BuildDev --allow-dirty-build` | exit 0 |
| Serve | `python3 -m http.server 8080 -d Athanor/Build/web-dev` (background) | |
| WebTest | `node tools/web/webtest.mjs http://localhost:8080/ production/qa/evidence/phase6/<task>/web` | exit 0 |
| HD2D | `node tools/web/mainhd2d.mjs http://localhost:8080/ production/qa/evidence/phase6/<task>/hd2d 640x360 hud,tiles` | 0 console errors and every non-shake check passes (sway 27/27, rest, hud). Shake is recorded, not gated: it is the 5b-08 accepted gap (harness latency, proven by `DepthClickTests` in EditMode) and varies run to run on the same code (Task 0: 20/27, Task 2: 18/27, different failing points). Exit code is 1 because of shake; read `report.json`. |

**Full verification** = CI + EditMode + DevBuild + WebTest + HD2D, all passing.

---

## File map

**Created**

| Path | Responsibility | Lifetime |
|---|---|---|
| `Athanor/Assets/Editor/ViewBaker.cs` | Bake a view from its `Build(...)` into a prefab, place it in `Main.unity`, set the `GameFlow` field, compare with the reference capture | deleted in Task 16 |
| `Athanor/Assets/Editor/ViewCapture.cs` | Render a GameObject to a 640x360 PNG with a throwaway orthographic camera; percent of differing pixels | kept (evidence tool) |
| `Athanor/Assets/Editor/ArtLibraryBuilder.cs` | `Athanor > Refresh Art Library` (the `BuildLibrary` part of `SceneBuilder`) | kept |
| `Athanor/Assets/Prefabs/Views/*.prefab` | one per view | kept |
| `Athanor/Assets/Prefabs/Parts/{Card,JournalCell,GoalRow}.prefab` | data-driven repeats | kept |
| `Athanor/Assets/Scripts/Game/Ui/JournalCell.cs`, `Ui/GoalRow.cs` | components of the two data-driven parts | kept |
| `Athanor/Assets/Scripts/Game/Support/Art.cs` | the one static handle on the `ArtLibrary` (fonts, sprites by content id) | kept |
| `Athanor/Assets/Tests/EditModeGame/PrefabFixture.cs` | load + instantiate a prefab in a test, destroy in teardown | kept |
| `Athanor/Assets/Tests/EditModeGame/GameTestSetup.cs` | `[SetUpFixture]`: `Loc.Use` and `Art.Lib` for the whole Game test assembly | kept |
| `Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs` | the round-trip tests for `Label` and `UiButton`, and one test per baked view | kept |
| `Athanor/Assets/Tests/EditMode/Sim.cs` | the simulation helpers shared by the tests and the reports | kept |
| `tools/CoreReports/CoreReports.csproj`, `tools/CoreReports/Program.cs` | the 6 balance/trace reports as a console program | kept |
| `docs/ATHANOR.md` | the single project doc | kept |

**Modified (main ones):** every view in `Scripts/Game/{Ui,Bench,Cutscene,Fx}`, `Boot/GameFlow.cs`, `Support/{Label,Make,Loc,PointerInput}.cs`,
`Ui/UiButton.cs`, `Boot/Save.cs`, `Fx/SparkMesh.cs`, `Fx/Sparks.cs`, `Editor/WebBuild.cs`, the 7 Game tests that call `Build(...)`,
the 14 test files with UL100 names, `CLAUDE.md`, `README.md`, `tools/vault/build_vault.py`, the agents that cite deleted paths.

**Deleted:** `Editor/SceneBuilder.cs`, `Editor/DepthTestBuilder.cs`, `Editor/DepthTestEvidence.cs`, `Editor/MainDepthEvidence.cs`
(its capture moves to `ViewCapture`), `Scenes/DepthTest.unity`, `Settings/DepthTest/`, `Resources/layout.json`,
`Support/LayoutData.cs`, the factories in `Make.cs`, plus the step-4 and step-5 lists of the spec.

---

## Phase A — baseline and Phase 5 close

### Task 0: Branch and baseline numbers

**Files:** none changed. Output: `production/qa/evidence/phase6/baseline/`.

- [ ] **Step 1: Create the branch**

```bash
cd "/Users/bunker/Desktop/New Folder"
git switch -c feature/phase6-unity-refactor
```

- [ ] **Step 2: Record the numbers this phase must keep**

```bash
mkdir -p production/qa/evidence/phase6/baseline
tools/ci/run.sh all 2>&1 | tail -5 | tee production/qa/evidence/phase6/baseline/ci.txt
unity test Athanor --mode EditMode 2>&1 | tail -5 | tee production/qa/evidence/phase6/baseline/editmode.txt
for r in BalanceMatrix EventReport RoleReport BalanceReport; do
  DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --filter "FullyQualifiedName~$r" -l "console;verbosity=detailed" \
    > production/qa/evidence/phase6/baseline/$r.txt 2>&1
done
```

Expected: CI exit 0; EditMode `458 / 452 passed / 0 failed / 6 skipped` (or the current figure, which is then the reference).
The four report files hold the numbers that Task 18 must reproduce exactly.

- [ ] **Step 3: Record a dev-build web run before any change**

Run DevBuild, Serve, then WebTest and HD2D into `production/qa/evidence/phase6/baseline/{web,hd2d}`. Both exit 0.

- [ ] **Checkpoint.** Nothing to commit (the baseline folder is committed in Task 17 with the other evidence).

### Task 1: Close Phase 5

**Files:**
- Create: `docs/devlogs/phase-5-close.md`
- Modify: `production/session-state/active.md`

- [ ] **Step 1: Write the close devlog**

```markdown
# Phase 5 close (2026-11-18)

Phase 5 is closed as it stands. Nothing below is decided by closing it.

## Shipped
- 5a: see `production/qa/phase5a-signoff.md`.
- 5b-01 HD-2D mockup, 5b-04 depth layer art, 5b-05 DepthTest prototype, 5b-06 WebGL measurements, 5b-07 HD-2D in Main
  (sway setting, dev hooks, `tools/web/mainhd2d.mjs`), 5b-08 QA sign-off PASS with accepted gaps.
- Gates: G2 approved 2026-11-16 (11 SPEC questions as recommended), G3 passed 2026-11-17 (owner; numbers not recorded).
- Final figures (5b-08): release 10,917,289 B (ceiling 11,824,672 B, headroom 907,383 B); Core 331/331; EditMode 458 / 452 pass /
  0 fail / 6 skip; `tools/ci/run.sh all` exit 0; BalanceMatrix = baseline.

## Carried over (undecided)
- 5b-02 loops mockup: owner review (keep or revert the shipped loops).
- 5b-03 endings lore (`phase-5-endings-lore.md`): frame Option 1 "Roots of worlds", the three seeds, win at any depth vs depth 5
  only, shared first/last ending lines.
- Delete remote `origin/feature/2.5D_test-phase-4` (merged; owner's command only).
- G3 carry-overs: 7-scene storyboard, wider `LineBox` use, VI text pass; reduced-effects switch (not built).
- Version string and release notes for the Phase 5 build: not done; `project.yaml` stays 0.4.0.
- Known issue: HUD small text garbled at 1x in the DepthTest player only (prototype removed in Phase 6).
```

- [ ] **Step 2: Point the session checkpoint at Phase 6**

Replace the body of `production/session-state/active.md` with: branch `feature/phase6-unity-refactor`, plan path (this file),
spec path, "Phase 5 closed, see `docs/devlogs/phase-5-close.md`", current task = Task 2, and keep its *Constraints* and
*Art generator drift* sections unchanged.

- [ ] **Step 3: Verify** — `tools/ci/run.sh repo` exit 0.

- [ ] **Checkpoint.** Suggested commit: `docs: close Phase 5, carry-overs to Phase 6` / `Story: P6-1`.

---

## Phase B — refactor the Game layer (spec §2)

### Task 2: Inject the JSON and the art library instead of `Resources.Load` and `Make.Lib`

The JSON files stay where they are (`Assets/Resources/`), because `tools/CoreTests` and the Core test helpers read them from
there. Only the way the game gets them changes.

**Files:**
- Create: `Athanor/Assets/Scripts/Game/Support/Art.cs`, `Athanor/Assets/Tests/EditModeGame/GameTestSetup.cs`
- Modify: `Support/Loc.cs:28-45`, `Support/Make.cs:14` and every `Make.Lib` use, `Boot/GameFlow.cs:16-21,127-133`,
  `Fx/SparkMesh.cs:58`, `Fx/Sparks.cs`
- Test: `Athanor/Assets/Tests/EditModeGame/LocTests.cs`

- [ ] **Step 1: Write the failing test** (add to `LocTests.cs`)

```csharp
[Test]
public void Use_WithInjectedFiles_ServesTheirText()
{
    var en = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/en.json");
    var vi = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/vi.json");
    Loc.Use(en, vi);
    Loc.Set(Loc.Vietnamese);
    Assert.AreEqual(Lang.Parse(vi.text).Get("ui.runover.again"), Loc.T("ui.runover.again"));
    Loc.Set(Loc.English);
}
```

- [ ] **Step 2: Run it.** EditMode, expected: compile error `Loc does not contain a definition for 'Use'`.

- [ ] **Step 3: Implement**

`Support/Loc.cs`: replace the `Resources.Load` in `Load` with a lookup in two injected assets.

```csharp
static TextAsset enFile, viFile;

/// <summary>Gives Loc its language files. GameFlow calls it first thing; the Game tests call it from GameTestSetup.</summary>
public static void Use(TextAsset english, TextAsset vietnamese)
{
    enFile = english; viFile = vietnamese;
    current = null;                       // the next Ensure() parses the new files
}

static Lang Load(string c, Lang fallback)
{
    var asset = c == Vietnamese ? viFile : enFile;
    if (asset == null) throw new System.InvalidOperationException("Loc.Use was not called before the first text lookup");
    // the rest of the old Load body, unchanged, from the line after the Resources.Load call
}
```

`Support/Art.cs`:

```csharp
namespace Athanor.Game
{
    /// <summary>The art library the running game uses. GameFlow sets it from its serialized field; the Game tests set it in GameTestSetup.</summary>
    public static class Art
    {
        public static ArtLibrary Lib;
    }
}
```

Replace `Make.Lib` with `Art.Lib` everywhere (`grep -rn "Make\.Lib" Athanor/Assets` must return nothing), and delete the
`public static ArtLibrary Lib;` line in `Make.cs`.

`Boot/GameFlow.cs`: add the fields and use them in `Awake`.

```csharp
[SerializeField] TextAsset contentJson, langEn, langVi;
[SerializeField] Material particleGlow;
```

```csharp
Art.Lib = library;
Loc.Use(langEn, langVi);
settings = Save.LoadSettings();
Loc.InitFromPlayer(settings.language);
...
Db = new ContentDb(JsonUtility.FromJson<ContentData>(contentJson.text));
```

`Fx/SparkMesh.cs:58`: the base material comes from `Sparks` (`[SerializeField] Material particleGlow;` on `Sparks`, passed to
`SparkMesh` where `Sparks` creates it) instead of `Resources.Load<Material>("ParticleGlow")`.

`Tests/EditModeGame/GameTestSetup.cs`:

```csharp
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>Once per run of the Game tests: the language files and the art library the game would get from GameFlow.</summary>
    [SetUpFixture]
    public sealed class GameTestSetup
    {
        [OneTimeSetUp]
        public void Inject()
        {
            Loc.Use(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/en.json"),
                    AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/vi.json"));
            Art.Lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>("Assets/Art/ArtLibrary.asset");
        }
    }
}
```

Remove the per-test `Make.Lib = ...` set/restore pairs in the 7 Game tests (they now come from `GameTestSetup`).

- [ ] **Step 4: Wire the scene.** Create the temporary `Athanor/Assets/Editor/WireData.cs` (deleted in Step 6) with a batch method
  that sets the new fields and saves the scene:

```csharp
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class WireData
{
public const string ScenePath = "Assets/Scenes/Main.unity";
public static void Run()
{
    var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
    var so = new SerializedObject(Object.FindFirstObjectByType<GameFlow>());
    so.FindProperty("contentJson").objectReferenceValue = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/content.json");
    so.FindProperty("langEn").objectReferenceValue = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/en.json");
    so.FindProperty("langVi").objectReferenceValue = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/vi.json");
    so.FindProperty("particleGlow").objectReferenceValue = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/ParticleGlow.mat");
    so.ApplyModifiedPropertiesWithoutUndo();
    var sparks = new SerializedObject(Object.FindFirstObjectByType<Sparks>());
    sparks.FindProperty("particleGlow").objectReferenceValue = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/ParticleGlow.mat");
    sparks.ApplyModifiedPropertiesWithoutUndo();
    EditorSceneManager.SaveScene(scene);
    Debug.Log("[phase6] data wired");
}
}
```

Run `unity run Athanor -- -executeMethod WireData.Run`. Expected: `[phase6] data wired`.

- [ ] **Step 5: Verify.** `grep -rn "Resources.Load" Athanor/Assets/Scripts` returns only `GameFlow.cs`'s `layout` line (removed in
  Task 14). Full verification passes.

- [ ] **Step 6:** Delete `Editor/WireData.cs` (the scene keeps the references). EditMode passes.

- [ ] **Checkpoint.** Suggested commit: `refactor: inject language files, content and art library through GameFlow` / `Story: P6-2`.

### Task 3: Bake and capture tooling

**Files:**
- Create: `Athanor/Assets/Editor/ViewBaker.cs`, `Athanor/Assets/Editor/ViewCapture.cs`, `Athanor/Assets/Tests/EditModeGame/PrefabFixture.cs`

- [ ] **Step 1: `ViewCapture.cs`**

```csharp
using System.IO;
using Athanor.Game;
using TMPro;
using UnityEngine;

/// <summary>Renders a GameObject alone (in an otherwise empty scene) to a 640x360 image, the way the UI camera draws it.</summary>
public static class ViewCapture
{
    public static Texture2D Render(GameObject root)
    {
        bool wasActive = root.activeSelf;
        root.SetActive(true);
        foreach (var t in root.GetComponentsInChildren<TMP_Text>(true)) t.ForceMeshUpdate();
        var camGo = new GameObject("phase6 capture camera");
        var cam = camGo.AddComponent<Camera>();
        cam.orthographic = true; cam.orthographicSize = Px.H / 2f;
        cam.transform.position = new Vector3(0, 0, -10);
        cam.clearFlags = CameraClearFlags.SolidColor; cam.backgroundColor = Color.black;
        cam.cullingMask = ~(1 << DepthRig.WorldLayer);
        var rt = RenderTexture.GetTemporary(Px.W, Px.H, 24, RenderTextureFormat.ARGB32);
        cam.targetTexture = rt; cam.Render(); cam.targetTexture = null;
        var previous = RenderTexture.active; RenderTexture.active = rt;
        var tex = new Texture2D(Px.W, Px.H, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, Px.W, Px.H), 0, 0); tex.Apply();
        RenderTexture.active = previous; RenderTexture.ReleaseTemporary(rt);
        Object.DestroyImmediate(camGo);
        root.SetActive(wasActive);
        return tex;
    }

    public static void Save(Texture2D tex, string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllBytes(path, tex.EncodeToPNG());
    }

    public static Texture2D Load(string path)
    {
        var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        tex.LoadImage(File.ReadAllBytes(path));
        return tex;
    }

    /// <summary>Percent of pixels whose RGBA differs at all.</summary>
    public static float Differ(Texture2D a, Texture2D b)
    {
        var pa = a.GetPixels32(); var pb = b.GetPixels32();
        if (pa.Length != pb.Length) return 100f;
        int n = 0;
        for (int i = 0; i < pa.Length; i++)
            if (pa[i].r != pb[i].r || pa[i].g != pb[i].g || pa[i].b != pb[i].b || pa[i].a != pb[i].a) n++;
        return 100f * n / pa.Length;
    }
}
```

- [ ] **Step 2: `ViewBaker.cs`**

```csharp
using System;
using System.IO;
using Athanor.Core;
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Phase 6 only (deleted in Task 16): builds a view the old way once, keeps a reference picture, saves it as a prefab,
/// puts the prefab into Main.unity and sets the GameFlow field. Then Compare checks the prefab draws the same picture.
/// unity run Athanor -- -executeMethod ViewBaker.Bake&lt;Name&gt;
/// </summary>
public static class ViewBaker
{
    public const string ScenePath = "Assets/Scenes/Main.unity", ViewDir = "Assets/Prefabs/Views", PartDir = "Assets/Prefabs/Parts";
    public const string EvidenceDir = "../production/qa/evidence/phase6/views";
    const float MaxDiffer = 0.1f;

    static void Prepare()
    {
        Art.Lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>("Assets/Art/ArtLibrary.asset");
        Loc.Use(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/en.json"),
                AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/vi.json"));
        Loc.Set(Loc.English);
    }

    public static ContentDb Db() =>
        new ContentDb(JsonUtility.FromJson<ContentData>(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/content.json").text));

    /// <summary>Old Build in an empty scene, the "before" picture, the prefab; then the prefab into Main and onto GameFlow.flowField.</summary>
    public static void Bake<T>(string name, string flowField, Action<T> build) where T : MonoBehaviour
    {
        Prepare();
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var go = new GameObject(name);
        build(go.AddComponent<T>());
        ViewCapture.Save(ViewCapture.Render(go), EvidenceDir + "/" + name + "/before.png");
        Directory.CreateDirectory(ViewDir);
        var prefab = PrefabUtility.SaveAsPrefabAsset(go, ViewDir + "/" + name + ".prefab");

        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        var old = GameObject.Find(name);
        if (old != null) Object.DestroyImmediate(old);                    // a second bake replaces the first
        var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
        var so = new SerializedObject(Object.FindFirstObjectByType<GameFlow>());
        so.FindProperty(flowField).objectReferenceValue = instance.GetComponent<T>();
        so.ApplyModifiedPropertiesWithoutUndo();
        EditorSceneManager.SaveScene(scene);
        Debug.Log("[phase6] baked " + name);
    }

    /// <summary>A part (one repeated piece) built by its own function, saved into Prefabs/Parts. No scene change.</summary>
    public static void BakePart<T>(string name, Func<T> build) where T : Component
    {
        Prepare();
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var part = build();
        part.gameObject.name = name;
        Directory.CreateDirectory(PartDir);
        PrefabUtility.SaveAsPrefabAsset(part.gameObject, PartDir + "/" + name + ".prefab");
        Debug.Log("[phase6] baked part " + name);
    }

    /// <summary>
    /// For views that build into scene objects (Bench with the HD-2D stage, cutscenes, window, screen effects): the old Build runs
    /// inside Main.unity under a new root; with a prefab name the root is saved as a connected prefab, without one it stays a plain
    /// scene object. The GameFlow field is set and the scene saved. Their "before" picture is the browser baseline (Task 0).
    /// </summary>
    public static void BakeInScene<T>(string name, string flowField, Action<T> build, bool asPrefab) where T : MonoBehaviour
    {
        Prepare();
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        var old = GameObject.Find(name);
        if (old != null) Object.DestroyImmediate(old);
        var go = new GameObject(name);
        var view = go.AddComponent<T>();
        build(view);
        if (asPrefab)
        {
            Directory.CreateDirectory(ViewDir);
            PrefabUtility.SaveAsPrefabAssetAndConnect(go, ViewDir + "/" + name + ".prefab", InteractionMode.AutomatedAction);
        }
        var so = new SerializedObject(Object.FindFirstObjectByType<GameFlow>());
        so.FindProperty(flowField).objectReferenceValue = view;
        so.ApplyModifiedPropertiesWithoutUndo();
        EditorSceneManager.SaveScene(scene);
        Debug.Log("[phase6] baked " + name + " in the scene");
    }

    /// <summary>
    /// The prefab alone in an empty scene against the "before" picture. Throws (non-zero exit) above 0.1 %. prepare runs on the
    /// instance before the picture (e.g. Bind(Db()) for a view whose rows come from content).
    /// </summary>
    public static void Compare(string name, Action<GameObject> prepare = null)
    {
        Prepare();
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(ViewDir + "/" + name + ".prefab");
        var go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        prepare?.Invoke(go);
        var after = ViewCapture.Render(go);
        ViewCapture.Save(after, EvidenceDir + "/" + name + "/after.png");
        float p = ViewCapture.Differ(ViewCapture.Load(EvidenceDir + "/" + name + "/before.png"), after);
        Debug.Log("[phase6] " + name + " " + p.ToString("0.000") + " % px differ");
        if (p > MaxDiffer) throw new Exception("[phase6] " + name + " differs by " + p.ToString("0.000") + " %, limit " + MaxDiffer);
    }

    // One pair of entry points per view is added by its task, e.g.
    //   public static void BakeRunOver() => Bake<RunOverView>("RunOver", "runOver", v => v.Build());
    //   public static void CompareRunOver() => Compare("RunOver");
}
```

- [ ] **Step 3: `PrefabFixture.cs`**

```csharp
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>A copy of a prefab for one test. Call Dispose (or Destroy) in [TearDown].</summary>
    public sealed class PrefabFixture
    {
        public readonly GameObject Root;

        public PrefabFixture(string prefabPath)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            Assert.IsNotNull(prefab, "no prefab at " + prefabPath);
            Root = Object.Instantiate(prefab);
        }

        public T Get<T>() where T : Component => Root.GetComponent<T>();

        public void Destroy() { if (Root != null) Object.DestroyImmediate(Root); }
    }
}
```

- [ ] **Step 4: Verify.** CI (lint: no new findings) and EditMode pass. These files only add code.

- [ ] **Checkpoint.** Suggested commit: `chore: Phase 6 bake and capture editor tools` / `Story: P6-3`.

### Task 4: Make `Label` and `UiButton` survive a prefab round trip

Problems this fixes, found while planning: `Label.key`, `lines` and `lineBox` are private and not serialized, so a baked label
forgets its Lang key and stops following the language. `UiButton.body`/`label` are private, and the click lambdas live in
`Clickable.onClick` (an `Action`, never serialized), so a baked button does nothing. A serialized `string` comes back as `""`, not
`null`, so `Relabel`'s `if (key != null)` would blank every label that has no key.

**Files:**
- Modify: `Support/Label.cs:17-18,95-103`, `Ui/UiButton.cs:7-40`
- Test: `Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs` (new)

- [ ] **Step 1: Write the failing tests**

```csharp
using System.Reflection;
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>Baked objects must behave like the ones Build made: a label keeps its key, a button still clicks.</summary>
    public class PrefabBakeTests
    {
        const string Temp = "Assets/Tests/EditModeGame/Temp_Bake.prefab";
        GameObject made, copy;

        [TearDown] public void Clean()
        {
            if (made != null) Object.DestroyImmediate(made);
            if (copy != null) Object.DestroyImmediate(copy);
            AssetDatabase.DeleteAsset(Temp);
            Loc.Set(Loc.English);
        }

        T RoundTrip<T>(GameObject go) where T : Component
        {
            made = go;
            var prefab = PrefabUtility.SaveAsPrefabAsset(go, Temp);
            copy = Object.Instantiate(prefab);
            return copy.GetComponent<T>();
        }

        static void Relabel(Label l) => typeof(Label).GetMethod("Relabel", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(l, null);

        [Test]
        public void Relabel_KeyedLabelAfterPrefabRoundTrip_FollowsTheLanguage()
        {
            var label = RoundTrip<Label>(Make.TextKey(null, "ui.runover.again", 0, 0, FontKind.Title, 16, Color.white, 0).gameObject);
            Loc.Set(Loc.Vietnamese);
            Relabel(label);
            Assert.AreEqual(Loc.T("ui.runover.again"), label.Text);
        }

        [Test]
        public void Relabel_PlainLabelAfterPrefabRoundTrip_KeepsItsText()
        {
            var label = RoundTrip<Label>(Make.Text(null, "abc", 0, 0, FontKind.Body, 16, Color.white, 0).gameObject);
            Relabel(label);
            Assert.AreEqual("abc", label.Text);
        }

        [Test]
        public void Click_ButtonAfterPrefabRoundTrip_RaisesClicked()
        {
            var button = RoundTrip<UiButton>(UiButton.Create(null, "ui.runover.again", 0, 0, 0).gameObject);
            button.Bind();
            int clicks = 0;
            button.Clicked += () => clicks++;
            button.GetComponent<Clickable>().onClick();
            Assert.AreEqual(1, clicks);
        }

        [Test]
        public void Click_DisabledButtonAfterPrefabRoundTrip_DoesNothing()
        {
            var button = RoundTrip<UiButton>(UiButton.Create(null, "ui.runover.again", 0, 0, 0).gameObject);
            button.Bind();
            button.Disabled = true;
            int clicks = 0;
            button.Clicked += () => clicks++;
            button.GetComponent<Clickable>().onClick();
            Assert.AreEqual(0, clicks);
        }
    }
}
```

- [ ] **Step 2: Run.** EditMode, expected: compile errors (`UiButton.Bind`, `UiButton.Clicked` do not exist; `Create` needs 6 args).

- [ ] **Step 3: Implement `Label`**

```csharp
[SerializeField] string key;
(string name, object value)[] args;           // arguments are set at run time by SetKey; never baked
static readonly (string name, object value)[] NoArgs = new (string, object)[0];
[SerializeField] int lines = 1;
[SerializeField] bool lineBox;
```

In `Relabel`: `if (!string.IsNullOrEmpty(key)) Text = Loc.T(key, args ?? NoArgs);`

- [ ] **Step 4: Implement `UiButton`**

```csharp
[SerializeField] SpriteRenderer body;
[SerializeField] Label label;
[SerializeField] bool disabled;
bool over, down;
/// <summary>Raised on a click while the button is enabled.</summary>
public event Action Clicked;

/// <summary>key: the Lang key of the label, which follows the language. x, y: art-space top-left corner. onClick is optional;
/// a view that is baked hooks Clicked in its own Bind instead.</summary>
public static UiButton Create(Transform parent, string key, float x, float y, int order, Action onClick = null)
{
    var go = Make.Root("Button " + key, parent).gameObject;
    go.transform.position = Px.Mid(x, y, W, H);
    var b = go.AddComponent<UiButton>();
    b.body = Make.Sprite(go.transform, "button_up", go.transform.position, order);
    b.label = Make.TextKey(go.transform, key, 0, 0, FontKind.Title, 16, Px.Hex("#fff6b8"), order + 2, Align.Centre, Px.Hex("#3b0d1e"));
    Make.Click(go, new Vector2(W, H), order);
    b.Bind();
    if (onClick != null) b.Clicked += onClick;
    b.Update();
    return b;
}

void Awake() { if (GetComponent<Clickable>() != null) Bind(); }

/// <summary>Hooks this button's Clickable to it. Safe to call more than once: it assigns, it does not add.</summary>
public void Bind()
{
    var c = GetComponent<Clickable>();
    c.onClick = () => { if (!disabled) Clicked?.Invoke(); };
    c.onEnter = () => over = !disabled;
    c.onExit = () => { over = false; down = false; };
    c.onDown = () => down = !disabled;
    c.onUp = () => down = false;
}
```

`Update()` changes `Make.Lib` to `Art.Lib` (already done in Task 2). Existing callers that pass a lambda as the 6th argument
keep working until their view is baked.

- [ ] **Step 5: Run.** EditMode, expected: the 4 new tests PASS and nothing else changes.

- [ ] **Step 6: Verify.** Full verification.

- [ ] **Checkpoint.** Suggested commit: `fix: Label and UiButton keep their key and clicks through a prefab round trip` / `Story: P6-4`.

### Task 5: Bake `RunOverView` (the worked example every later view follows)

**Files:**
- Modify: `Ui/RunOverView.cs`, `Boot/GameFlow.cs:164-165`, `Editor/ViewBaker.cs`, `Tests/EditModeGame/RunOverDepthTests.cs`,
  `Tests/EditModeGame/PrefabBakeTests.cs`
- Create: `Assets/Prefabs/Views/RunOver.prefab` (by the bake)

- [ ] **Step 1: Write the failing test** (append to `PrefabBakeTests`)

```csharp
[Test]
public void ShowDepth_RunOverPrefab_WritesTheDepthRow()
{
    var f = new PrefabFixture("Assets/Prefabs/Views/RunOver.prefab");
    try
    {
        var view = f.Get<RunOverView>();
        view.Bind();
        view.ShowDepth(true, 4);
        Assert.AreEqual("Planted at depth 4", view.DepthName.Text);
    }
    finally { f.Destroy(); }
}
```

- [ ] **Step 2: Run.** EditMode, expected: FAIL `no prefab at Assets/Prefabs/Views/RunOver.prefab`.

- [ ] **Step 3: Serialize the view and add `Bind`.** In `RunOverView.cs` replace the field block and `Build` with:

```csharp
[SerializeField] Label heading, sub, goalsLine;
[SerializeField] Label[] names = new Label[4], values = new Label[4];
[SerializeField] SpriteRenderer shade;
[SerializeField] Transform body;
[SerializeField] Clickable goalsClick;
[SerializeField] UiButton again, toTitle;
bool done, wired;

/// <summary>Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab.</summary>
public void Bind()
{
    if (wired) return;
    wired = true;
    goalsClick.onClick = OnGoals;
    again.Clicked += PickAgain;
    toTitle.Clicked += PickTitle;
}

void OnDestroy()
{
    if (!wired) return;
    again.Clicked -= PickAgain;
    toTitle.Clicked -= PickTitle;
}

void OnGoals() { if (gameObject.activeSelf && !done) GoalsClicked?.Invoke(); }
void PickAgain() => Pick(true);
void PickTitle() => Pick(false);

/// <summary>Phase 6 bake source only; deleted in Step 6.</summary>
public void Build()
{
    var t = transform;
    shade = Make.Rect(t, 0, 0, Px.W, Px.H, Color.clear, O - 1);
    Make.Click(shade.gameObject, new Vector2(Px.W, Px.H), O - 1);      // nothing underneath can be clicked
    body = Make.Root("Dialog", t);
    Make.SpriteAt(body, "dialog", X, Y, O);
    heading = Make.Text(body, "", 320, Y + 14, FontKind.Title, 32, Px.Hex("#f8d250"), O + 2, Align.Centre, Px.Hex("#3a1a08"));
    sub = Make.Text(body, "", 320, Y + 50, FontKind.Body, 16, Px.Hex("#f0dcc0"), O + 2, Align.Centre);
    Make.Rect(body, X + 24, Y + 72, 272, 1, Px.Hex("#6a4428"), O + 1);
    string[] rows = { "ui.runover.depth_reached", "ui.runover.discoveries", "ui.runover.journal", "ui.runover.goals" };
    for (int i = 0; i < rows.Length; i++)
    {
        names[i] = Make.TextKey(body, rows[i], X + 28, Y + 78 + i * 17, FontKind.Body, 16, Px.Hex("#a88a6a"), O + 2);
        values[i] = Make.Text(body, "", X + 292, Y + 76 + i * 17, FontKind.Title, 16, Px.Hex("#f0dcc0"), O + 2, Align.Right, Px.Hex("#0c0706"));
    }
    goalsLine = Make.Text(body, "", 320, Y + 147, FontKind.Tiny, 8, Px.Hex("#6ee6c8"), O + 2, Align.Centre);
    goalsClick = Make.Click(goalsLine.gameObject, new Vector2(272, 12), O + 2);
    goalsClick.offset = new Vector2(0, -4);
    Make.Rect(body, X + 24, Y + 160, 272, 1, Px.Hex("#6a4428"), O + 1);
    again = UiButton.Create(body, "ui.runover.again", X + 28, Y + 170, O + 2);
    toTitle = UiButton.Create(body, "ui.runover.title", X + 172, Y + 170, O + 2);
    gameObject.SetActive(false);
}
```

In `Show(...)` replace `var body = transform.GetChild(1);` with nothing (the field `body` is used).

- [ ] **Step 4: Point `GameFlow` at the scene object**

```csharp
[SerializeField] RunOverView runOver;
public RunOverView RunOver => runOver;
```

In `Awake` replace `RunOver = Make.Root("Run Over").gameObject.AddComponent<RunOverView>(); RunOver.Build();` with
`runOver.Bind();`. Delete the old `public RunOverView RunOver { get; private set; }`. Keep `RunOver.GoalsClicked += OpenGoals;`
and add `RunOver.GoalsClicked -= OpenGoals;` to `OnDestroy` (this also clears that UL005 baseline entry).

- [ ] **Step 5: Bake.** Add to `ViewBaker`:

```csharp
public static void BakeRunOver() => Bake<RunOverView>("RunOver", "runOver", v => v.Build());
public static void CompareRunOver() => Compare("RunOver");
```

Run Bake (`ViewBaker.BakeRunOver`). Expected: `[phase6] baked RunOver`, `Assets/Prefabs/Views/RunOver.prefab` exists,
`production/qa/evidence/phase6/views/RunOver/before.png` exists.

- [ ] **Step 6: Delete the bake source.** Delete `RunOverView.Build()` and `ViewBaker.BakeRunOver` (keep `CompareRunOver`).

- [ ] **Step 7: Compare.** Run Compare (`ViewBaker.CompareRunOver`). Expected: `[phase6] RunOver 0.000 % px differ` (≤ 0.100).

- [ ] **Step 8: Move the old test to the prefab.** In `RunOverDepthTests.SetUp` replace the `new GameObject` + `AddComponent` +
  `Build()` with:

```csharp
fixture = new PrefabFixture("Assets/Prefabs/Views/RunOver.prefab");
view = fixture.Get<RunOverView>();
view.Bind();
```

and `TearDown` with `fixture.Destroy();` (field `PrefabFixture fixture;` replaces `GameObject root;`).

- [ ] **Step 9: Run.** EditMode: the new test and the 4 `RunOverDepthTests` PASS.

- [ ] **Step 10: Verify.** Full verification. In the WebTest run, a lost run must still show the Run Over dialog and its two buttons
  must work (scenario A plays to a run over).

- [ ] **Checkpoint.** Suggested commit: `refactor: RunOverView is a prefab wired in Main.unity` / `Story: P6-5`.

### The per-view recipe (Tasks 6–15)

Every remaining view follows Task 5's ten steps exactly. For each one:

1. **Test first:** one `PrefabBakeTests` test that instantiates the prefab, calls `Bind(...)`, calls one public method, asserts one
   visible result. Run it, see it fail on the missing prefab.
2. **Serialize:** every field `Build` assigns becomes `[SerializeField]` (the lists in each task). Plain classes that hold refs
   (`Row`, `Card`, `Slot`) get `[System.Serializable]` and public fields. Arrays sized in a field initializer keep it.
3. **Move every click out of `Build`:** each `Make.Click(go, size, order, <lambda>)` becomes `x = Make.Click(go, size, order)` with a
   new `[SerializeField] Clickable x`, and the lambda's body moves verbatim into a named method assigned in `Bind`
   (`x.onClick = Method;`). Each `UiButton.Create(..., <lambda>)` drops the lambda; `Bind` does `button.Bind();` then
   `button.Clicked += Method;`, and `OnDestroy` does `-=`. (Found in Task 5: a baked `UiButton` hooks its `Clickable` only in
   its own `Awake`, which never runs in EditMode and at runtime only when a hidden view first opens; the view's `Bind` must not
   depend on that. `PrefabBakeTests.Click_RunOverPrefabButton_PicksItsChoice` fails without it.)
   Each view's test also clicks one button through its `Clickable` and asserts the effect.
4. **Runtime-only objects stay runtime:** anything made with `Sprite.Create`, `new Texture2D`, `new RenderTexture`, `new Mesh` or
   `new Material` is created in `Bind` (or lazily on first use), never baked. Known cases: `Tooltip.cs:24` (9-slice panel sprite),
   `RarityStars.cs:51` (hanging star sprite), `WindowView.cs:160-161` (root texture/sprite), `SparkMesh`, `DepthTable`, `DepthRig`.
5. **GameFlow:** `[SerializeField]` field for the view, `x.Bind(...)` in `Awake` replacing the `Make.Root(...).AddComponent<X>(); x.Build(...)`
   line; every `+=` on the view's events gets its `-=` in `OnDestroy`.
6. **Bake → delete `Build` and the bake entry → Compare (≤ 0.1 %)** → move old tests to `PrefabFixture` → Full verification → Checkpoint.

`Bind` parameters replace `Build` parameters that are data (`ContentDb`, `Settings`); parameters that were scene objects
(`Transform parent`, `GameObject cutsceneRoom`, ...) become `[SerializeField]` references set once in the scene by the bake.

### Task 6: `GoalToast`

- Fields: `Transform panel; SpriteRenderer icon; Label title, line;` + one `Clickable` (the toast click). `Queue<Item> queue` stays
  runtime (not serialized).
- Bind: `Bind()`. Bake: `Bake<GoalToast>("GoalToast", "toast", v => v.Build())`.
- Test: `Add_GoalToastPrefab_ShowsTheGoalName` — call `Add` with one `GoalReward` from `ViewBaker.Db()`-style content
  (`new ContentDb(TestContent.Load())`), assert `title.Text` (expose `public Label Title => title;`).
- Checkpoint: `refactor: GoalToast is a prefab` / `Story: P6-6`.

### Task 7: `EventView`

- Fields: `Row[] rows` (`Row` → `[Serializable]`), `Label[] notes`, `SpriteRenderer shade, icon`, `Label heading, kind, text, outcome`,
  `UiButton proceed`, `Transform body`, and the 3 option `Clickable`s inside each `Row` (add `public Clickable click;` to `Row`).
- 6 lambdas move to named methods (`PickOption(int)` is called by a per-row method `OnRow0/1/2` or by a closure created in `Bind`;
  closures created in `Bind` are fine because they are assigned to `Clickable.onClick`, not `+=` to an event).
- Keep the three `Dev.Hook("[event] ...")` lines unchanged (`webtest.mjs` waits for them).
- Bind: `Bind()`. Bake: `Bake<EventView>("Event", "eventView", v => v.Build())`.
- Test: `Show_EventPrefab_ListsTheOptions` (first event in content; assert the first row's label is not empty).
- Checkpoint: `refactor: EventView is a prefab` / `Story: P6-7`.

### Task 8: `CatalystChoiceView`

- Fields: `Card[] cards` (`Card` → `[Serializable]`, add a `Clickable click`), `SpriteRenderer shade`, `Label heading, sub`.
  The inner loop over `Bench.TileCount` (line 75) builds a fixed 9-cell mini grid per card: bake it.
- `ChoiceHookTests` (4 `Build` calls) move to `PrefabFixture`.
- Bind: `Bind()`. Bake: `Bake<CatalystChoiceView>("CatalystChoice", "choice", v => v.Build())`.
- Test: `Show_CatalystChoicePrefab_FillsThreeCards`.
- Checkpoint: `refactor: CatalystChoiceView is a prefab` / `Story: P6-8`.

### Task 9: `PauseMenu`

- Fields: `Transform menu, confirm`, `UiButton resume, vibration, howTo, quit` (+ the confirm dialog's two buttons, give them fields
  `quitYes, quitNo`), `Label howToHint`. `Settings settings` is runtime: `Bind(Settings playerSettings)`.
- `PauseTests` (5 `Build` calls) move to `PrefabFixture` + `Bind(new Settings())`.
- Bake: `Bake<PauseMenu>("Pause", "pause", v => v.Build(new Settings()))`.
- Test: `Open_PausePrefab_ShowsTheMenu`.
- Checkpoint: `refactor: PauseMenu is a prefab` / `Story: P6-9`.

### Task 10: `GoalsView` (data-driven rows)

The rows are one per goal in `content.json`, so they must not be baked (a new goal would be missing).

- Create `Ui/GoalRow.cs`:

```csharp
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>One row of the Goals page: the goal and its count, then the reward and its progress bar.</summary>
    public sealed class GoalRow : MonoBehaviour
    {
        public Label text, count, reward;
        public SpriteRenderer track, fill, icon;
    }
}
```

- Move the body of the `for (int i = 0; i < db.Goals.Count; i++)` loop (`GoalsView.cs:47-61`) into a static
  `GoalsView.BuildRow(Transform parent)` that returns a `GoalRow` (same `Make.*` calls, `root` = the new GameObject with the
  component). Bake **one** row as `Assets/Prefabs/Parts/GoalRow.prefab` with a second bake entry
  (`BakePart<GoalRow>("GoalRow", () => GoalsView.BuildRow(null))`; add this generic to `ViewBaker`: same as `Bake` without the
  scene part, saving into `PartDir`).
- `GoalsView` fields: `[SerializeField] GoalRow rowPrefab; [SerializeField] Label[] heads; [SerializeField] Label found;` plus its
  `Clickable`s (3). `Bind(ContentDb content)` instantiates `db.Goals.Count` rows under the view with `Instantiate(rowPrefab, transform)`
  and positions them exactly where the old loop placed them (the old loop's position code moves into `Bind`).
- Bake the view without rows: its `Build` skips the loop. Compare: the "before" picture of the view had rows with empty text; make the
  Compare entry call `Bind(Db())` on the instance before `Render` so both pictures have rows (add an optional
  `Action<GameObject> prepare` parameter to `Compare`).
- Test: `Bind_GoalsPrefab_MakesOneRowPerGoal` — assert `GetComponentsInChildren<GoalRow>(true).Length == db.Goals.Count`.
- Checkpoint: `refactor: GoalsView is a prefab, goal rows from a part prefab` / `Story: P6-10`.

### Task 11: `JournalView` (data-driven cells) and `Tooltip`, `RarityStars`

- Create `Ui/JournalCell.cs` (`public SpriteRenderer icon; public Clickable click; public RarityStars stars;`). Same method as
  Task 10: the per-material loop body (`JournalView.cs:62-76`) becomes `BuildCell`, one cell is baked as `Parts/JournalCell.prefab`,
  `Bind(ContentDb)` instantiates one per material, sets `icon.sprite = Art.Lib.Sprite(m.id)`, positions it with `CellCentre(i)`,
  and sets `click.onClick` to a closure calling `Select(index)`.
- `RarityStars` (plain class) → `[System.Serializable]`, fields public; its `hanging` sprite (`RarityStars.cs:51`) is created on first
  `Show` when null.
- `Tooltip` is built by `BenchView.cs:70` and baked inside the Bench prefab in Task 14; in this task only make it bake-safe:
  `Tooltip.cs:24` (9-slice panel sprite made with `Sprite.Create`) moves into a `Bind()` that creates it when null, and its fields
  `Label title, body, tier; SpriteRenderer panel; RarityStars stars;` become `[SerializeField]`.
- Fields: `Label pageLabel; Label[] hints, lines; SpriteRenderer pick, bigIcon; RarityStars pageStars; Label pageTier;
  SpriteRenderer[] pips; Label found, materialsCount, title, sub;` and the 5 `Clickable`s.
- Bake: `Bake<JournalView>("Journal", "journalView", v => v.Build(ViewBaker.Db()))` (Build skips the cell loop).
- Test: `Bind_JournalPrefab_MakesOneCellPerMaterial`.
- Checkpoint: `refactor: JournalView is a prefab, cells from a part prefab` / `Story: P6-11`.

### Task 12: `TitleView`

- Fields (from `Build`): `Label logo, sub, progress, credit, langValue, vibValue, swayLabel, swayValue, resetLabel, vibLabel,
  langLabel; Label[] outline; Transform confirm, panel, skip; Transform[] cell; SpriteRenderer[] cellEdge; SpriteRenderer glow, shade;`
  and the 12 click targets (buttons → `UiButton` fields, the rest → `Clickable` fields). `Action<bool> skipAnswer` and
  `Settings settings` stay runtime: `Bind(Settings playerSettings)`.
- 9 lambdas → named methods. Keep `Dev.Hook("[loc] " + next)` unchanged (`webtest.mjs` scenario D).
- Bake: `Bake<TitleView>("Title", "title", v => v.Build(new Settings()))`.
- Test: `Bind_TitlePrefab_ShowsTheLogoText`.
- Checkpoint: `refactor: TitleView is a prefab` / `Story: P6-12`.

### Task 13: `HudView`

- `HudView` is built by `BenchView.Build` (`Hud` property) with `Build(Transform parent)`. Bake it as its own prefab nested in the
  Bench prefab in Task 14; in this task bake `Parts/Hud.prefab` with `BakePart<HudView>("Hud", () => { var h = new GameObject("Hud").AddComponent<HudView>(); h.Build(null); return h; })`.
- Fields seen while planning: `GameObject pauseGroup; Label depth, depthName, potency, potencyWord, target, trials, redraws,
  redrawsLabel, journalCount, igniteText, hintText; Label[] note, depthNote; SpriteRenderer barFill, barTip, ignite, ignitePulse;
  SpriteRenderer[] socketLit, aspectSocket;`. The survey's line was cut off after `aspectSocket`, so before editing, list every
  assignment in `Build` with `awk 'NR>=76 && NR<=155' Athanor/Assets/Scripts/Game/Bench/HudView.cs | grep -oE "^\s+[a-zA-Z]+(\[[a-z]\])? = "`
  and serialize each field it names. 6 click targets → fields; 17 lambdas → named methods.
- `HudTests` (Resources content) and `LabelLineBoxTests` (3 `Build` calls) move to the prefab.
- Test: `Bind_HudPrefab_ShowsTheDepthNumber`.
- Checkpoint: `refactor: HudView is a prefab` / `Story: P6-13`.

### Task 14: `BenchView`, `TileView`, `CardView`; drop `layout.json`

- `TileView` (9, fixed): baked inside the Bench prefab. Its fields `glow`/`flashColor` are `Color` → `[SerializeField]`.
- `CardView` is made at run time per hand card (`BenchView.cs:123`): bake one as `Parts/Card.prefab` (`BakePart` with a
  `CardView.Build(firstMaterial, null, 0)` call), then replace `new GameObject("Card " + m.id).AddComponent<CardView>(); card.Build(m, parent, order)`
  with `Instantiate(cardPrefab, parent)` + a new `card.Show(MaterialData m, int order)` that holds the per-material part of the old
  `Build` (icon sprite, lettering, rarity stars, sorting order).
- `BenchView` fields: `TileView[] tiles; Slot[] slots (Serializable); LinkView links, joins; HudView hud (the nested Hud prefab);
  Tooltip tip; CardView cardPrefab;` `RunState run`, `List<CardView> cards`, `Pick press, selected` stay runtime.
- `Build(LayoutData layout, ContentDb content, Transform parent)` reads `layout.tiles` and rack positions: the bake runs it once with
  `JsonUtility.FromJson<LayoutData>(File.ReadAllText("Assets/Resources/layout.json"))`, so the positions are now in the prefab. Then:
  delete `layout.json`, `LayoutData.cs`, the `layout` line in `GameFlow.Awake`, and the `layout.json` write in `tools/art/export.mjs`
  (edit the file only; do not run it).
- HD-2D: `GameFlow` calls `depthStage.PlaceTiles(Bench, layout.tiles.Length)`; change it to `depthStage.PlaceTiles(Bench, Bench.TileCount)`
  (`Athanor.Core.Bench.TileCount`).
- `DepthClickTests`, `MainDepthTests` (2 + 2 `Build` calls) move to the prefab / the scene.
- Bake in the scene (not a separate empty scene) because `PlaceTiles` needs `DepthStage`:
  `ViewBaker.BakeInScene<BenchView>("Bench", "bench", v => { v.Build(layout, ViewBaker.Db(), null); Object.FindFirstObjectByType<DepthStage>().PlaceTiles(v, Athanor.Core.Bench.TileCount); }, asPrefab: true)`
  (`GameFlow` gets `[SerializeField] BenchView bench; public BenchView Bench => bench;`). The "before"
  picture for Bench is the `mainhd2d.mjs hud` scenario from Task 0's baseline; the after picture is the same scenario now.
  Pass = `mainhd2d.mjs` exit 0 and its `hud` crops identical to baseline by eye (record the comparison in the evidence README).
- Test: `Refresh_BenchPrefab_HasNineTiles`.
- Checkpoint: `refactor: Bench, tiles and cards are prefabs; layout.json removed` / `Story: P6-14`.

### Task 15: Cutscenes, `WindowView`, `TutorialController`, `ScreenFx`

These build into scene objects (`cutsceneRoom`, `workshop`, the camera), not into their own root, so they are baked **into
`Main.unity`** with `BakeInScene(..., asPrefab: false)`, each component on its own GameObject (`Reaction Cutscene`, `Story Cutscene`, `Window`,
`Tutorial`, `Screen Fx`) instead of on `GameFlow`'s GameObject. Check each class for `gameObject`/`GetComponent` uses that assumed
it sat on `GameFlow`'s object (grep `GetComponent<GameFlow>`, `gameObject.`) and pass the reference through `Bind` instead.

- `ReactionCutscene`: fields `GameObject room; Transform stage; SpriteRenderer rune, liquid, liquidGlow, pillar, result, resultGlow;
  SpriteRenderer[] mats, rings; Transform[] rays; Light2D[] matLight; Light2D runeLight, liquidLight, resultLight; Label headerLeft,
  headerRight, skipHint, banner, title(...)`; `Color[] matCol` and `Sprite[] liquidFrames` → serialized. `Bind(GameObject[] others)`.
- `SimpleCutscene`: fields `GameObject room, workshop, bench; Transform root; SpriteRenderer shade, glow, ring, rune, icon, iconGlow;
  SpriteRenderer[] bars; Transform[] rays; Label[] lines; Label small, big, banner, prompt, intro; Light2D iconLight;` `Loop`s stay runtime.
  Fix UL001 here: cache `rays[k].GetChild(0).GetComponent<SpriteRenderer>()` into a serialized `SpriteRenderer[] rayFaces` set by `Build`.
- `WindowView`: fields `SpriteRenderer pane, glow, point; SpriteRenderer[] motes, drips;` `rootTex`/`rootSprite` created in `Bind`.
  `depthStage.window = window` stays in `GameFlow`.
- `TutorialController`: `Build()` is 9 lines; fields `Transform root, cells; Label line; List<SpriteRenderer> rim; List<Label> extra;`
  `GameFlow f` becomes a `Bind(GameFlow flow)` parameter. `TutorialFlowTests` adapts.
- `ScreenFx`: fields `SpriteRenderer flash, black;` `Bind(Camera camera, CameraSway worldSway)`. `ScreenFx.I` and `Sparks.I` stay for
  now (out of scope: they are scene singletons the effects call; listed as a carry-over in `ATHANOR.md`).
- Pass: `webtest.mjs` (plays a reaction, the tutorial, the window), `mainhd2d.mjs moods,shake` exit 0; EditMode green.
- Checkpoint: `refactor: cutscenes, window, tutorial and screen effects are scene objects` / `Story: P6-15`.

### Task 16: Remove the builders and the singleton

**Files:** delete `Editor/SceneBuilder.cs` (after moving `BuildLibrary`/`RefreshLibrary` into `Editor/ArtLibraryBuilder.cs`,
menu `Athanor/Refresh Art Library`, batch `ArtLibraryBuilder.RefreshBatch`), `Editor/ViewBaker.cs`; `Editor/MainDepthEvidence.cs`
(superseded by `ViewCapture`); in `Make.cs` delete `Root`, `Sprite`, `SpriteAt`, `Rect`, `Glow`, `Text`, `TextKey`, `Light`, `Click`,
`Tmp` and rename the file/class to `Fonts` keeping `FontFor`, `SizeFor`, `HalfLeading` (update `Label` and callers).

The remaining `Make.*` callers are bake sources: `UiButton.Create`, `GoalsView.BuildRow`, `JournalView.BuildCell`, the `CardView`
part bake. Each view task deleted its own `Build`. This task deletes these four too, after checking that all prefabs and parts exist
(`ls Athanor/Assets/Prefabs/Views Athanor/Assets/Prefabs/Parts`).

- [ ] **Step 1:** `grep -rnE "Make\.(Root|Sprite|SpriteAt|Rect|Glow|Text|TextKey|Light|Click)|\.Build\(|UiButton\.Create|SceneBuilder|ViewBaker" Athanor/Assets`
  → expected: nothing, except `WebBuild` (`Build()` method names) and `FontBuilder.Build`.
- [ ] **Step 2:** Remove `GameFlow.I` (no reader outside `GameFlow.cs`, checked while planning) and the `I = this;` line.
- [ ] **Step 3:** `DepthTestBuilder` is still referenced by `SceneBuilder` only; both go now, together with `DepthTest.unity` and
  `Settings/DepthTest/` — **but first** move `DepthTable.mat`, `DepthDisplay.mat`, `Fog.mat`, `TiltShift.mat` from
  `DepthTestBuilder.Dir` to `Assets/Art/Depth/Materials/` with `AssetDatabase.MoveAsset` (keeps GUIDs, so `Main.unity` keeps its
  references). Run it as a one-off batch method in `ArtLibraryBuilder` (`MoveDepthMaterials`), then delete that method. Remove
  `WebBuild.BuildDepthTest` and `DepthOutput`. Fix the `TileView.cs:14` comment.
- [ ] **Step 4:** Full verification, plus `unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build` and
  the release size (macOS has no `du -b`):
  `find Athanor/Build/web/Build Athanor/Build/web/index.html -type f -exec stat -f%z {} + | awk '{s+=$1} END {print s}'`
  **< 11,824,672 B**. Record the number.
- [ ] **Checkpoint.** `refactor: remove SceneBuilder, Make factories and GameFlow.I` / `Story: P6-16`.

### Task 17: Step 2 sign-off

- [ ] Write `production/qa/evidence/phase6/README.md`: per view the Compare percentage, the web check exit codes, EditMode and Core
  counts, release size vs Task 0. Keep the baseline folder.
- [ ] Spec "Done when" bullet 1: `grep -rn "GameFlow\.I\b\|layout\.json\|SceneBuilder" Athanor/Assets tools` → nothing.
- [ ] **Checkpoint.** `test: Phase 6 refactor evidence` / `Story: P6-17`.

---

## Phase C — lint baseline to zero (spec §3)

### Task 18: The 6 `[Explicit]` reports become a console program

These six (`BalanceMatrix`, `EventReport`, `RoleReport`, `NewcomerTrace`, `BalanceReport`, `TraceRun`) are the balance reports that
`CLAUDE.md` requires for content changes; they print numbers and assert nothing, which is why lint flags them (UL101 + UL103).
They are not deleted: they move out of the test suite.

**Files:**
- Create: `tools/CoreReports/CoreReports.csproj`, `tools/CoreReports/Program.cs`, `Athanor/Assets/Tests/EditMode/Sim.cs`
- Modify: `Tests/EditMode/BalanceMatrixTests.cs`, `RoleSimTests.cs`, `RunStateTests.cs`, `.gitignore`, `tools/ci/check_repo.py`
  (BUILD-OUTPUT paths), `.claude/agents/{content-designer,qa-engineer,rules-engineer}.md`

- [ ] **Step 1:** Move the shared static helpers (`With`, `Autoplay`, `Line`, `RandomPlay` from `BalanceMatrixTests`; `Career`,
  `Play`, `Pct`, `Describe` from `RoleSimTests`) into `public static class Sim` in `Tests/EditMode/Sim.cs` (namespace unchanged).
  Tests that used them call `Sim.X`. Run `tools/ci/run.sh core` → same pass count as Task 0.
- [ ] **Step 2:** `tools/CoreReports/CoreReports.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>disable</Nullable>
    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include="Program.cs" />
    <Compile Include="../../Athanor/Assets/Scripts/Core/**/*.cs" />
    <Compile Include="../../Athanor/Assets/Tests/EditMode/Sim.cs" />
    <Compile Include="../../Athanor/Assets/Tests/EditMode/TestContent.cs" />
    <None Include="../../Athanor/Assets/Resources/content.json" CopyToOutputDirectory="Always" />
    <None Include="../../Athanor/Assets/Resources/lang/*.json" Link="lang/%(Filename)%(Extension)" CopyToOutputDirectory="Always" />
  </ItemGroup>
</Project>
```

  If `TestContent.cs` or `Sim.cs` reference `NUnit`, remove that dependency from them (they must compile without NUnit).
- [ ] **Step 3:** `Program.cs`: a `switch (args[0])` over `balance-matrix | events | roles | newcomer | balance | trace`; each case is
  the body of the old `[Explicit]` method with `TestContext.Progress.WriteLine` replaced by `Console.WriteLine`. `trace` keeps reading
  `ATHANOR_SEED`. Unknown or missing argument → usage line, exit 2.
- [ ] **Step 4:** Delete the six `[Test, Explicit]` methods.
- [ ] **Step 5: Verify the numbers did not move**

```bash
DOTNET_ROLL_FORWARD=Major dotnet run --project tools/CoreReports -- balance-matrix > /tmp/bm.txt
```

  Compare the report lines with `production/qa/evidence/phase6/baseline/BalanceMatrix.txt` (the numbers, not the NUnit framing):
  every win rate identical. Same for `events`, `roles`, `balance`.
- [ ] **Step 6:** Add `tools/CoreReports/bin/` and `obj/` to `.gitignore` and to `check_repo.py`'s build-output list. Update the three
  agents' commands to `dotnet run --project tools/CoreReports -- <report>`.
- [ ] **Step 7:** CI. Core pass count ≥ 250 (`MIN_TESTS`).
- [ ] **Checkpoint.** `refactor: balance reports run from tools/CoreReports, not as explicit tests` / `Story: P6-18`.

### Task 19: UL100 — rename 131 test methods

Rule (`unity_lint.py:90`): `^[A-Za-z0-9]+(?:_[A-Za-z0-9]+){2,}$` — at least three parts.

- [ ] **Step 1:** List them: `python3 tools/lint/unity_lint.py --root Athanor --config tools/lint/lint.config.json --all | grep UL100`
  (no baseline, so all show). Files: `Phase3Tests` 28, `ContentTests` 20, `LangTests` 19, `ChainTests` 17, `RunStateTests` 13,
  `IgniteTests` 11, `SatchelTests` 5, `LoreTests` 4, `RoleSimTests` 4, `SaveTests` 4, `BalanceMatrixTests` 2, `TutorialTests` 2,
  `HudTests` 1, `MotionTests` 1 (the 2 in `BalanceMatrixTests` are gone after Task 18).
- [ ] **Step 2:** Rename each to `Method_Condition_Result`, keeping the meaning: the existing two parts are usually
  `Subject_Result`; insert the real condition from the test body (e.g. `Centre_HostsFiveItems` → `Centre_WithFiveNeighbours_HostsFiveItems`
  if that is what it sets up). Use `_Always_` only when the test has no setup that varies. One file at a time; after each file
  `tools/ci/run.sh core` (Core files) or EditMode (Game files) with the same pass count.
- [ ] **Step 3:** No test name may be duplicated inside its class; no logic change (diff shows only method-name lines).
- [ ] **Checkpoint.** `test: rename tests to Method_Condition_Result (UL100)` / `Story: P6-19`.

### Task 20: The remaining code findings

Most are gone after Phase B; check with the no-baseline run from Task 19 Step 1, filtered to `UL00`.

- [ ] **UL003** `Boot/Save.cs` (2): `catch (Exception e) { Debug.LogWarning("[save] journal unreadable, starting empty: " + e.Message); return new Journal(); }`
  and the same for the profile. `webtest.mjs` scenario C (damaged save) must still pass and must not count a warning as a console error
  (it fails on `error` only; check `webtest.mjs` before relying on it).
- [ ] **UL004** `Support/PointerInput.cs` (2): `Hover?.onDown?.Invoke()` → `if (Hover != null && Hover.onDown != null) Hover.onDown();`
  and the same for `top?.onEnter`.
- [ ] **UL002** `GameFlow.cs` `reaction.Skip(); story.Skip();`: read the lint message for the exact allocation it found on that line
  and remove it (most likely a lambda or LINQ in the same statement range).
- [ ] **UL005** `GameFlow.cs` (9): every `+=` in `Awake` gets its `-=` in `OnDestroy`; the `RedrawRequested` lambda becomes a method
  `void OnRedraw() { if (State == FlowState.Bench && Run.Redraw()) Bench.Refresh(true); }`.
- [ ] **UL001** `SimpleCutscene.cs`: done in Task 15; confirm gone.
- [ ] Test-first where behaviour can change: add `SaveTests.LoadJournal_CorruptData_ReturnsEmptyJournalAndWarns` with
  `LogAssert.Expect(LogType.Warning, new Regex("journal unreadable"))` before changing `Save.cs`.
- [ ] Full verification.
- [ ] **Checkpoint.** `fix: clear remaining lint findings (UL001-UL005)` / `Story: P6-20`.

### Task 21: Empty baseline

- [ ] `python3 tools/lint/unity_lint.py --root Athanor --config tools/lint/lint.config.json --all` → **0 findings**.
- [ ] `python3 tools/lint/unity_lint.py --root Athanor --config tools/lint/lint.config.json --all --write-baseline tools/lint/baseline.json`
  → `entries: []`.
- [ ] CI exit 0. EditMode: 0 failed, **0 skipped**.
- [ ] **Checkpoint.** `chore: lint baseline is empty` / `Story: P6-21`.

---

## Phase D — delete outdated content (spec §4)

Before each deletion: list what will go (`git ls-files <path> | wc -l`, `du -sh`), then delete, then `grep -rn "<path>"` over
`CLAUDE.md README.md docs .claude tools vault Athanor/Assets` and fix every hit (the doc hits are rewritten in Phase E anyway).

### Task 22: Evidence, mockups, shots

- [ ] `git rm -r --quiet production/qa/evidence/phase*` **except** `production/qa/evidence/phase6` (do `ls production/qa/evidence`
  first; delete each other entry by name).
- [ ] `git rm -r docs/mockup docs/devlogs/phase-2-shots docs/devlogs/phase-3-shots docs/devlogs/phase-4-shots`.
- [ ] `tools/ci/run.sh repo` exit 0.
- [ ] **Checkpoint.** `chore: remove Phase 1-5 evidence, mockups and shots (kept in git history)` / `Story: P6-22`.

### Task 23: Framework extras

- [ ] `git rm -r studio-tests graphify-out Map.canvas`.
- [ ] Check `.claude/` and `tools/` for readers of `studio-tests` (`grep -rn studio-tests .claude tools`); a skill that runs them
  (`skill-test`, `skill-improve`) is framework: leave the skill, note in `ATHANOR.md` *Carry-overs* that its fixtures were removed.
- [ ] **Checkpoint.** `chore: remove studio-tests, old graphify output and Map.canvas` / `Story: P6-23`.

### Task 24: Nested repos and the fx export tool

- [ ] Confirm both are clean (`git -C pixelfx status --short`, `git -C pixel-effects status --short` → empty) and note their
  remotes in `ATHANOR.md` *Data / art pipeline*.
- [ ] `git rm --cached pixelfx pixel-effects` then `rm -rf pixelfx pixel-effects`.
- [ ] `git rm tools/fx/export.sh tools/fx/shrink.mjs` (and `tools/fx/` if empty). Update `.claude/agents/pixel-artist.md` (effects:
  "sheets live in `Athanor/Assets/Art/Fx`; to re-export, clone pixelfx from `<remote>` first") and the README fx section.
- [ ] `tools/ci/run.sh repo` (`node --check` over art/web scripts) exit 0.
- [ ] **Checkpoint.** `chore: drop pixelfx/pixel-effects clones and the fx export scripts` / `Story: P6-24`.

### Task 25: `refs/` (irreversible)

- [ ] **Ask the owner** in the main session, with `du -sh "refs/2.5D Reference"` and the file count: "Delete permanently? There is no
  other copy." Only on an explicit yes: `rm -rf refs`. On no: add `refs/` to `.gitignore` and record that in the checkpoint.
- [ ] **Checkpoint.** Nothing tracked changes (refs was untracked), unless `.gitignore` changed: `chore: ignore refs/`.

---

## Phase E — one project doc (spec §5)

### Task 26: Write `docs/ATHANOR.md`

**Inputs (read by section, per coordination rule 6):** `README.md`, `docs/devlogs/*.md` (including `phase-5-close.md`),
`docs/superpowers/specs/*.md`, `docs/superpowers/plans/*.md`, `production/qa/phase5{a,b}-signoff.md`, `docs/CI.md`,
`vault/Design/Architecture.md`, `vault/Data/Data Design.md`, and the code after Phase B.

- [ ] Write these sections, each describing **the state now** (not history):
  1. *The game* — premise, run loop, bench/ignite/chain rules, scoring, depth 1–5, content kinds (from specs + devlogs 1–4)
  2. *Architecture* — Core / Game split, `Main.unity` + prefabs, `GameFlow` and its views with `Bind`, HD-2D rig (`DepthStage`,
     `DepthRig`, `CameraSway`), save, localization (`Loc.Use`), dev hooks and keys
  3. *Data* — `content.json` schema summary (point to `vault/Data/Data Design.md` for detail), `lang/*.json`, icons in
     `tools/art/icons.mjs`, `tools/art` export (and the drift warning), fx sheets
  4. *Decisions in force* — one line each, dated, only the ones still true (e.g. G2's 11 SPEC answers in one line each, owner answers
     from phase-5-prep, the win-before-depth-5 rule, sway setting). Superseded ones are left out.
  5. *CI and rules* — the merge rule and the failure-to-check table from `docs/CI.md`, updated (no DepthTest, baseline empty, reports
     in `tools/CoreReports`)
  6. *Build, test, release* — the command table from this plan, size ceiling 11,824,672 B and the Task 16 figure
  7. *Carry-overs* — from `phase-5-close.md`, plus `ScreenFx.I`/`Sparks.I`, studio-tests fixtures removed
  8. *Lore and text voice* — the binding parts of `phase-4-lore.md`, `phase-5-lore.md`, `phase-4-tutorial-script.md`,
     `phase-5-endings-lore.md` (the draft is marked undecided)
- [ ] Every number in it comes from a file or a command run in this phase (no figures from memory).
- [ ] **Checkpoint.** `docs: ATHANOR.md, the single project doc` / `Story: P6-26`.

### Task 27: Point everything at `ATHANOR.md`

- [ ] `CLAUDE.md`: hard rule "Devlog per phase in `docs/devlogs/phase-N-*.md`; a decision is written there before it is relied on" →
  "A decision is written in *Decisions in force* of `docs/ATHANOR.md` before it is relied on". CI hard rule: "Rules and the
  case-to-check map: `docs/ATHANOR.md` § CI and rules". Project layout table: drop `docs/devlogs`, `docs/superpowers/{specs,plans}`,
  `docs/mockup`; add `docs/ATHANOR.md`.
- [ ] `README.md`: shorten to the game in two sentences + build/run commands + "Everything else: `docs/ATHANOR.md`".
- [ ] `grep -rln "docs/devlogs\|docs/superpowers\|docs/mockup\|docs/CI.md\|phase5b-signoff\|phase5a-signoff" .claude CLAUDE.md README.md tools vault --include=*.md --include=*.py --include=*.sh --include=*.mjs`
  → fix every hit to the matching `ATHANOR.md` section. (Framework docs under `.claude/docs/` that describe the framework's own
  generic `docs/` layout are not project links: leave those.)
- [ ] **Checkpoint.** `docs: point rules, agents and tools at ATHANOR.md` / `Story: P6-27`.

### Task 28: Vault generator

- [ ] Read `tools/vault/build_vault.py` for its devlog/phase inputs (`grep -n "devlogs\|Phases" tools/vault/build_vault.py`).
  Change it to build the Phase/Decision notes from `docs/ATHANOR.md` sections 4 and 7 instead.
- [ ] `python3 tools/vault/build_vault.py` then `git status vault` — the regenerated notes reflect the new code (no `SceneBuilder`,
  `Make` factories, `DepthTestBuilder`; new `ViewCapture`, `ArtLibraryBuilder`, `Art`, `GoalRow`, `JournalCell`).
- [ ] `tools/ci/run.sh repo` (its vault-is-current check) exit 0.
- [ ] **Checkpoint.** `chore: vault built from ATHANOR.md and the refactored code` / `Story: P6-28`.

### Task 29: Delete the merged docs

- [ ] `git rm docs/devlogs/*.md docs/superpowers/plans/*.md docs/superpowers/specs/*.md production/qa/phase5a-signoff.md production/qa/phase5b-signoff.md docs/CI.md`
  — this plan and its spec included (their content is in `ATHANOR.md`).
- [ ] Re-run the grep from Task 27 → nothing. `tools/ci/run.sh all` exit 0.
- [ ] Update `production/session-state/active.md` to point at `docs/ATHANOR.md`.
- [ ] **Checkpoint.** `docs: remove devlogs, plans, specs and sign-offs merged into ATHANOR.md` / `Story: P6-29`.

---

## Phase F — graphify (spec §6)

### Task 30: Graph of `Athanor/`

- [ ] Invoke the `graphify` skill with the input `Athanor/`, excluding `Athanor/Library`, `Athanor/Build`, `Athanor/Logs`,
  `Athanor/UserSettings`, `Athanor/Data`, `Athanor/Temp`, `Athanor/obj`, `Athanor/*.csproj`, `Athanor/*.slnx`. Output `graphify-out/`.
  Read nothing outside `Athanor/`.
- [ ] Report the god nodes and communities it finds (expected hubs: `GameFlow`, `ContentDb`, `RunState`, `Label`).
- [ ] **Checkpoint.** `docs: graphify map of Athanor/` / `Story: P6-30`.

---

## Contract and source checks

- **Unity API (6000.6):** `PrefabUtility.SaveAsPrefabAsset`, `InstantiatePrefab`, `SaveAsPrefabAssetAndConnect`, `AssetDatabase.MoveAsset`,
  `Object.FindFirstObjectByType`, `EditorSceneManager.NewScene/OpenScene/SaveScene`. Check `docs/engine-reference/unity/` for the
  pinned version before Task 3 and note any API marked changed.
- **Serialization:** Unity does not serialize `Action`, tuples, `Dictionary`, `Queue`, or plain classes without `[Serializable]`;
  a serialized `string` deserializes as `""`. Each view task must account for every field `Build` sets.
- **No new dependency.** `tools/CoreReports` uses only the SDK and the repo's own sources.
- **ADR:** `modes.workflow: standard` needs critical ADRs only. "Main.unity + prefabs are the source of truth; no scene generator"
  is expensive to reverse: record it as a line in `ATHANOR.md` *Decisions in force* (Task 26). The owner can ask for
  `/architecture-decision` instead.
