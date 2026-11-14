#!/usr/bin/env python3
"""Repo guard for Athanor CI. Standard library only. Exit 1 on any finding.

Each check maps to a hard rule in CLAUDE.md. Findings print as `CHECK: message`.
  python3 tools/ci/check_repo.py            run every check
  python3 tools/ci/check_repo.py --list     list the checks
"""
from __future__ import annotations

import json
import re
import subprocess
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
RES = ROOT / "Athanor/Assets/Resources"
CONTENT = RES / "content.json"
LANGS = {"en": RES / "lang/en.json", "vi": RES / "lang/vi.json"}
ICONS = ROOT / "tools/art/icons.mjs"
CORE = ROOT / "Athanor/Assets/Scripts/Core"

# Build output that must never be tracked (CLAUDE.md: bin, obj and Athanor/Build are output).
FORBIDDEN_TRACKED = [
    r"^tools/CoreTests/(bin|obj)/",
    r"^Athanor/(Build|Builds|Library|Temp|Obj|Logs|UserSettings)/",
    r"(^|/)test-results.*\.xml$",
]
CONTENT_PREFIXES = ("aspect.", "catalyst.", "depth.", "event.", "goal.", "material.", "recipe.")
PLACEHOLDER = re.compile(r"\{[A-Za-z0-9_]+\}")


def load_json(path: Path, problems: list[str], check: str):
    try:
        return json.loads(path.read_text(encoding="utf-8"))
    except FileNotFoundError:
        problems.append(f"{check}: missing {path.relative_to(ROOT)}")
    except json.JSONDecodeError as e:
        problems.append(f"{check}: {path.relative_to(ROOT)} is not valid JSON ({e})")
    return None


def check_tracked_output() -> list[str]:
    out = subprocess.run(["git", "-C", str(ROOT), "ls-files"], capture_output=True, text=True, check=True).stdout
    bad = [f for f in out.splitlines() if any(re.search(p, f) for p in FORBIDDEN_TRACKED)]
    return [f"BUILD-OUTPUT: tracked build output {f} (git rm --cached, then ignore it)" for f in bad[:20]] + (
        [f"BUILD-OUTPUT: ...and {len(bad) - 20} more"] if len(bad) > 20 else [])


def check_content() -> list[str]:
    problems: list[str] = []
    data = load_json(CONTENT, problems, "CONTENT")
    if data is None:
        return problems
    for coll in ("materials", "recipes", "catalysts", "aspects", "events", "goals"):
        items = data.get(coll)
        keyed = coll != "recipes"          # recipes are keyed by their items, not an id
        if not isinstance(items, list) or not items:
            problems.append(f"CONTENT: `{coll}` is missing or empty")
            continue
        if not keyed:
            continue
        ids = [i.get("id") for i in items if isinstance(i, dict)]
        if any(not i for i in ids):
            problems.append(f"CONTENT: `{coll}` has an entry without an id")
        dup = sorted({i for i in ids if i and ids.count(i) > 1})
        if dup:
            problems.append(f"CONTENT: `{coll}` has duplicate ids {dup}")
    return problems


def check_icons() -> list[str]:
    problems: list[str] = []
    data = load_json(CONTENT, problems, "ICONS")
    if data is None or not ICONS.exists():
        return problems + ([] if ICONS.exists() else ["ICONS: tools/art/icons.mjs is missing"])
    src = ICONS.read_text(encoding="utf-8")
    for coll in ("materials", "catalysts", "aspects"):
        for item in data.get(coll, []):
            i = item.get("id", "")
            if i and not re.search(r"(?<![A-Za-z0-9_])" + re.escape(i) + r"(?![A-Za-z0-9_])", src):
                problems.append(f"ICONS: {coll[:-1]} `{i}` has no icon in tools/art/icons.mjs")
    return problems


def check_lang() -> list[str]:
    problems: list[str] = []
    tables: dict[str, dict[str, str]] = {}
    for code, path in LANGS.items():
        d = load_json(path, problems, "LANG")
        if d is None:
            continue
        keys = [e.get("key") for e in d.get("entries", [])]
        dup = sorted({k for k in keys if keys.count(k) > 1})
        if dup:
            problems.append(f"LANG: {code}.json has duplicate keys {dup[:5]}")
        tables[code] = {e["key"]: e.get("text", "") for e in d.get("entries", []) if "key" in e}
        for k, t in tables[code].items():
            if not str(t).strip():
                problems.append(f"LANG: {code}.json key `{k}` is empty")
    if len(tables) == 2:
        en, vi = tables["en"], tables["vi"]
        for k in sorted(set(en) - set(vi)):
            if not k.endswith(".one"):     # Vietnamese has no singular/plural split
                problems.append(f"LANG: `{k}` is in en.json but not vi.json")
        for k in sorted(set(vi) - set(en)):
            # content text lives in content.json (English) and is localized by vi.json only
            if not k.startswith(CONTENT_PREFIXES):
                problems.append(f"LANG: `{k}` is in vi.json but not en.json")
        for k in sorted(set(en) & set(vi)):
            if sorted(PLACEHOLDER.findall(en[k])) != sorted(PLACEHOLDER.findall(vi[k])):
                problems.append(f"LANG: `{k}` placeholders differ between en and vi")
    return problems


def check_core_purity() -> list[str]:
    problems: list[str] = []
    for f in sorted(CORE.rglob("*.cs")):
        if re.search(r"^\s*using\s+Unity(Engine|Editor)\b", f.read_text(encoding="utf-8-sig"), re.M):
            problems.append(f"CORE-PURITY: {f.relative_to(ROOT)} uses UnityEngine/UnityEditor (Core must not)")
    return problems


def check_meta_pairs() -> list[str]:
    """Every Scripts/Tests .cs has a .meta, so Unity cannot regenerate GUIDs on the CI runner."""
    problems: list[str] = []
    for base in ("Athanor/Assets/Scripts", "Athanor/Assets/Tests", "Athanor/Assets/Editor"):
        for f in (ROOT / base).rglob("*.cs"):
            if not Path(str(f) + ".meta").exists():
                problems.append(f"META: {f.relative_to(ROOT)} has no .meta")
    return problems


CHECKS = {
    "BUILD-OUTPUT": check_tracked_output,
    "CONTENT": check_content,
    "ICONS": check_icons,
    "LANG": check_lang,
    "CORE-PURITY": check_core_purity,
    "META": check_meta_pairs,
}


def main() -> int:
    if "--list" in sys.argv:
        print("\n".join(CHECKS))
        return 0
    problems: list[str] = []
    for name, fn in CHECKS.items():
        found = fn()
        print(f"{'FAIL' if found else 'ok  '} {name}")
        problems += found
    for p in problems:
        print(p)
    print(f"\ncheck_repo: {len(problems)} problem(s)")
    return 1 if problems else 0


if __name__ == "__main__":
    sys.exit(main())
