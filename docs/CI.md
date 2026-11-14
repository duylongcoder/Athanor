# CI: the merge rule

**If `ci-gate` is not green, the change is not merged.** Branch protection requires it, including for admins
(`APPLY=1 tools/setup/protect-branch.sh duylongcoder/Crucible_Fable main`). Reproduce any failure locally with the
same command CI runs: `tools/ci/run.sh all` (or `lint`, `repo`, `core`).

| Failure in practice | Caught by | Job |
|---|---|---|
| Hard-rule code bugs: `Find*` in `Update`, swallowed `catch`, `?.` on a Unity object, event never unsubscribed, hard-coded secret | `unity_lint` UL001–UL007 | lint |
| `UnityEngine` inside `Scripts/Core` | UL008 and `check_repo` CORE-PURITY | lint, repo-guard |
| Bad tests: no assertion, `[Ignore]`/`[Explicit]`/`[Retry]`, `Thread.Sleep`, unseeded random or clock, name not `Method_Condition_Result` | UL100–UL105 | lint |
| Missing or duplicate `.meta` (Unity regenerates GUIDs and breaks references), wrong `ProjectSettings` | UL200–UL203 | lint |
| Build output staged (`bin`, `obj`, `Athanor/Build`, `test-results.xml`) | `check_repo` BUILD-OUTPUT, UL204 | repo-guard |
| Renamed serialized field loses scene/prefab data | UL301 (`FormerlySerializedAs`) | lint |
| Bad PR title (becomes the squash commit) | UL300 | lint |
| Broken or duplicate ids in `content.json` | CONTENT | repo-guard |
| New material, catalyst or aspect without an icon in `tools/art/icons.mjs` | ICONS | repo-guard |
| String missing in `vi.json`, empty text, placeholder `{x}` that differs between en and vi | LANG | repo-guard |
| Rules regress or a test is silently dropped (fewer than `MIN_TESTS` pass) | `dotnet test tools/CoreTests` | core-tests |
| `vault/` stale after a code or content change | `build_vault.py` then no diff | repo-guard |
| Art/web scripts do not parse | `node --check` | repo-guard |
| A job fails, is cancelled or is skipped | `ci-gate` needs every job `success` | ci-gate |
| Release build broken, over its size budget, console error in the browser, run stopped early | Unity EditMode, WebGL build, `webtest.mjs` (non-zero exit on error) | release-gate (tags and manual) |

## Rules for everyone

- **Never** weaken a check to get green. Fix the code, or record the debt (below).
- Existing findings are frozen in `tools/lint/baseline.json` (158 today: mostly UL100 test names). The baseline only
  shrinks. A new finding fails CI. Regenerate it only when you fixed some:
  `python3 tools/lint/unity_lint.py --root Athanor --config tools/lint/lint.config.json --all --write-baseline tools/lint/baseline.json`
- A new rule goes in `tools/lint/unity_lint.py` or `tools/ci/check_repo.py` with a test, and a row in the table above.
- `release-gate` needs the secrets `UNITY_LICENSE`, `UNITY_EMAIL`, `UNITY_PASSWORD`. Without them it fails on purpose:
  no unchecked release.

## Not covered by CI (still on a human)

Visual and UI review: screenshots in `production/qa/evidence/` (see `.claude/docs/run-and-observe.md`). Balance
numbers: `BalanceMatrix` and `EventReport` print figures to read; they do not assert.
