---
name: scene-refresh
description: "Refresh Main.unity and the view prefabs to the latest setup through the Unity CLI, revert batch-save drift, and prove nothing a player sees changed. Use after a manager, content or view change."
argument-hint: "[--no-tests]"
user-invocable: true
disable-model-invocation: true
allowed-tools: Read, Glob, Grep, Edit, Bash
---

# Scene refresh

Brings `Athanor/Assets/Scenes/Main.unity` and `Athanor/Assets/Prefabs/Views/*.prefab` up to date with the batch tools, then checks the result.
Scenes have no generator (`docs/ATHANOR.md` § Main.unity and prefabs); this skill only drives the existing Editor tools.

## Steps

1. **Editor closed?** `pgrep -fl "Unity.app/Contents/MacOS/Unity "` must print nothing. If it prints a process, stop and ask the owner to close the Editor.
2. **Snapshot** `Main.unity` and the prefabs to the scratchpad directory (`cp`), so the diff after the run is exact.
3. **Scene:** `unity run "<repo>/Athanor" -- -executeMethod Athanor.EditorTools.ManagerSceneMigration.RunBatch` (absolute project path; no `-quit`, the wrapper adds it).
4. **Prefabs:** `unity run "<repo>/Athanor" -- -executeMethod Reserialize.All` (it leaves `Main.unity` alone on purpose).
5. **Revert drift in `Main.unity`:** `git diff -U0 Athanor/Assets/Scenes/Main.unity` against the snapshot. A batch save rewrites Camera `y: 0.125` / `height: 0.75` and four light `m_LocalPosition` values; restore those lines (set `ATHANOR_ALLOW_SCENE_EDIT=1` for the edit, because `guard-scene-files.sh` blocks text edits to scenes). Keep only the intended serialized fields.
6. **Prove it** (skip with `--no-tests`): `unity test "<repo>/Athanor" --mode EditMode` with the filter `SceneBakeTests|GoldenShotTests|LayoutPinTests|PrefabBakeTests|MainDepthTests`. `GoldenShotTests` must show 0 % difference. Never run `Athanor > Write Golden Shots` here: the golden images are the regression gate.
7. **Evidence:** copy or capture the touched views into `production/qa/evidence/scene-update-<date>/` (EN and VI).
8. **Report** the commands with exit codes, the diff stat before and after, the reverted lines, the test counts and the evidence list. Do not commit; the owner commits.

## Never

- Edit `.unity` / `.prefab` as text beyond step 5. Run `node tools/art/export.mjs`. Touch `obj`, `Athanor/Build`.
