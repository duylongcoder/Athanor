#!/usr/bin/env python3
"""Query the vault index without opening dozens of notes. Agents: use this before grepping the C# tree.

  q.py type  <Name>        where a type lives, its public members (file:line)
  q.py member <text>       every public member whose name or signature contains <text>
  q.py uses  <Name>        types that mention <Name> (reverse dependencies) and tests that cover it
  q.py mat   <id|name>     a material with the recipes that make and use it
  q.py recipe <id|name>    every recipe that involves a material
  q.py list  <what>        types|materials|catalysts|aspects|events|goals
  q.py find  <text>        full-text search over vault note names and bodies (generated and hand-written)
Run `python3 tools/vault/build_vault.py` first if the code or content changed.
"""
import json, re, sys
from pathlib import Path

V = Path(__file__).resolve().parents[2] / "vault"
def load():
    text = (V / "_index" / "Symbols.md").read_text(encoding="utf-8")
    secs, cur = {}, None
    for line in text.split("\n"):
        if line.startswith("## "): cur = line[3:].strip(); secs[cur] = []
        elif cur and line.startswith("|") and not line.startswith("|---"):
            secs[cur].append([c.strip().replace("\\|", "|") for c in re.split(r"(?<!\\)\|", line.strip()[1:-1])])
        elif cur == "Lists" and line.startswith("- "):
            k, _, v = line[2:].partition(": "); secs[cur].append([k, v])
    rows = lambda n: secs.get(n, [])[1:]
    types = [dict(name=r[0], note=r[1], layer=r[2], kind=r[3], file=r[4], line=r[5], doc=r[6], members=[]) for r in rows("Types")]
    by = {t["name"]: t for t in types}
    for r in rows("Members"):
        if r[0] in by: by[r[0]]["members"].append(dict(kind=r[1], name=r[2], line=r[3], sig=r[4]))
    lists = {r[0]: [x for x in r[1].split(", ") if x] for r in secs.get("Lists", [])}
    return dict(types=types,
                materials=[dict(id=r[0], name=r[1], kind=r[2], tier=r[3], reactivity=r[4]) for r in rows("Materials")],
                recipes=[dict(items=r[0].split(", "), minReactivity=int(r[1]), result=r[2]) for r in rows("Recipes")], **lists)


IDX = load()


def types(name):
    n = name.lower()
    return [t for t in IDX["types"] if t["name"].lower() == n or t["note"].lower() == n] or [t for t in IDX["types"] if n in t["name"].lower()]


def mat(q):
    q = q.lower().replace(" ", "_")
    return next((m for m in IDX["materials"] if m["id"] == q or m["name"].lower().replace(" ", "_") == q), None)


def main(a):
    if len(a) < 2: print(__doc__); return 1
    cmd, arg = a[0], " ".join(a[1:])
    if cmd == "type":
        for t in types(arg):
            print(f"{t['name']} [{t['layer']} {t['kind']}] {t['file']}:{t['line']}\n  {t['doc']}")
            for m in t["members"]: print(f"  :{m['line']:<4} {m['sig']}")
    elif cmd == "member":
        for t in IDX["types"]:
            for m in t["members"]:
                if arg.lower() in m["sig"].lower(): print(f"{t['file']}:{m['line']}  {t['name']}.{m['name']}  {m['sig']}")
    elif cmd == "uses":
        note = next((t for t in IDX["types"] if t["name"].lower() == arg.lower()), None)
        if not note: print("no such type"); return 1
        text = (V / "Code" / note["layer"] / (note["note"] + ".md")).read_text(encoding="utf-8")
        for sec in ("Used by", "Tested by", "Uses"):
            m = re.search(rf"## {sec}\n(.*?)(?:\n\n|$)", text, re.S)
            print(f"{sec}: {m[1] if m else '-'}")
    elif cmd in ("mat", "recipe"):
        m = mat(arg)
        if not m: print("no such material"); return 1
        print(f"{m['name']} ({m['id']}) {m['kind']} tier {m['tier']} RI {m['reactivity']}")
        for r in IDX["recipes"]:
            items = r.get("items") or [r.get("a"), r.get("b")]
            tag = "makes" if r["result"] == m["id"] else "uses " if m["id"] in items else None
            if tag: print(f"  {tag} {' + '.join(items)} -> {r['result']}" + (f" (heat>={r['minReactivity']})" if r.get("minReactivity") else ""))
    elif cmd == "list":
        if arg == "types": print("\n".join(f"{t['layer']:<7}{t['name']}" for t in IDX["types"]))
        elif arg == "materials": print("\n".join(f"t{m['tier']} {m['kind']:<8}{m['id']} (RI {m['reactivity']})" for m in IDX["materials"]))
        elif arg in IDX: print("\n".join(IDX[arg]))
        else: print("types|materials|catalysts|aspects|events|goals")
    elif cmd == "find":
        rx = re.compile(re.escape(arg), re.I)
        for p in sorted(V.rglob("*.md")):
            if "_index" in p.parts: continue
            for i, line in enumerate(p.read_text(encoding="utf-8").split("\n"), 1):
                if rx.search(line): print(f"{p.relative_to(V)}:{i}: {line.strip()[:140]}")
    else:
        print(__doc__); return 1


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
