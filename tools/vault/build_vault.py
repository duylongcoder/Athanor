#!/usr/bin/env python3
"""Regenerate the generated half of the Obsidian vault (vault/) from the repo.

Reads : Athanor/Assets/Scripts, Editor, Tests (C#), Resources/content.json, .claude/agents + skills,
        docs/devlogs, docs/superpowers, tools/*.
Writes (all Markdown): vault/Code, vault/Content, vault/Tests, vault/Agents, vault/Tools, vault/Phases, vault/_index.
        Hand-written notes (vault/Home.md, Design, Data, Workflow, Templates) are never touched.

Usage : python3 tools/vault/build_vault.py          (stdlib only; run from anywhere)
Every generated note carries `generated: true` in its frontmatter. Do not edit those: edit the source and rerun.
"""
import json, os, re, shutil, sys
from collections import defaultdict
from pathlib import Path

REPO = Path(__file__).resolve().parents[2]
UNITY = REPO / "Athanor" / "Assets"
VAULT = REPO / "vault"
GEN_DIRS = ["Code", "Content", "Tests", "Agents", "Tools", "Phases", "_index"]
TODAY = os.environ.get("VAULT_DATE", "")  # empty: no date stamp, so reruns are diff-clean


def w(rel, text):
    p = VAULT / rel
    p.parent.mkdir(parents=True, exist_ok=True)
    p.write_text(text, encoding="utf-8")


def fm(**kv):
    out = ["---", "generated: true"]
    for k, v in kv.items():
        if isinstance(v, (list, tuple)):
            out.append(f"{k}: [{', '.join(str(x) for x in v)}]")
        elif isinstance(v, str) and (":" in v or v.startswith(("#", "[", "{")) or v == ""):
            out.append(f"{k}: {json.dumps(v)}")
        else:
            out.append(f"{k}: {v}")
    out.append("---")
    return "\n".join(out) + "\n"


def link(name, label=None):
    return f"[[{name}|{label}]]" if label and label != name else f"[[{name}]]"


# ---------------------------------------------------------------- C# parsing

TYPE_RE = re.compile(
    r"^(?P<ind>[ \t]*)(?P<mods>(?:(?:public|internal|private|protected|static|sealed|abstract|partial|readonly|unsafe)\s+)*)"
    r"(?P<kind>class|struct|enum|interface|record)\s+(?P<name>\w+)(?:<[^>\n]*>)?\s*(?::\s*(?P<base>[^\n{]+?))?\s*(?:where [^\n{]+)?\s*$")
MEMBER_MODS = ("public", "internal", "protected")


def strip_for_braces(s):
    """Blank out comments, strings and chars so brace counting is safe; keeps length and newlines."""
    out, i, n = [], 0, len(s)
    while i < n:
        c = s[i]
        if s.startswith("//", i):
            j = s.find("\n", i)
            j = n if j < 0 else j
            out.append(" " * (j - i)); i = j
        elif s.startswith("/*", i):
            j = s.find("*/", i + 2)
            j = n if j < 0 else j + 2
            out.append("".join("\n" if ch == "\n" else " " for ch in s[i:j])); i = j
        elif c == '"':
            verbatim = i > 0 and s[i - 1] == "@"
            j = i + 1
            while j < n:
                if verbatim and s.startswith('""', j): j += 2; continue
                if not verbatim and s[j] == "\\": j += 2; continue
                if s[j] == '"': break
                j += 1
            out.append("".join("\n" if ch == "\n" else " " for ch in s[i:j + 1])); i = j + 1
        elif c == "'":
            j = i + 1
            while j < n and s[j] != "'":
                j += 2 if s[j] == "\\" else 1
            out.append(" " * (j - i + 1)); i = j + 1
        else:
            out.append(c); i += 1
    return "".join(out)


def clean_doc(lines):
    t = " ".join(l.strip()[3:].strip() for l in lines)
    t = re.sub(r"</?(summary|remarks|returns|para)>", " ", t)
    t = re.sub(r"<see cref=\"([^\"]+)\"\s*/>", r"\1", t)
    t = re.sub(r"<[^>]+>", "", t).replace("&lt;", "<").replace("&gt;", ">").replace("&amp;", "&")
    return re.sub(r"\s+", " ", t).strip()


def parse_cs(path, layer):
    raw = path.read_text(encoding="utf-8", errors="replace")
    lines = raw.split("\n")
    clean = strip_for_braces(raw).split("\n")
    rel = path.relative_to(REPO).as_posix()
    ns = (re.search(r"^\s*namespace\s+([\w.]+)", raw, re.M) or [None, ""])[1]
    types = []
    for i, line in enumerate(clean):
        m = TYPE_RE.match(line)
        if not m or "=" in line:
            continue
        ind = len(m["ind"].replace("\t", "    "))
        # find the opening brace, then the matching close
        depth, j, start = 0, i, None
        end = None
        while j < len(clean):
            for ch in clean[j]:
                if ch == "{":
                    depth += 1
                    if start is None: start = j
                elif ch == "}":
                    depth -= 1
                    if depth == 0 and start is not None:
                        end = j; break
            if end is not None: break
            j += 1
        if end is None: continue
        # attributes / doc above
        k, doc, attrs = i - 1, [], []
        while k >= 0 and (lines[k].strip().startswith(("[", "///")) or lines[k].strip() == ""):
            s = lines[k].strip()
            if s.startswith("///"): doc.insert(0, s)
            elif s.startswith("["): attrs.insert(0, s)
            elif s == "": break
            k -= 1
        mods = m["mods"].split()
        t = dict(name=m["name"], kind=m["kind"], mods=mods, base=(m["base"] or "").strip(), ns=ns, layer=layer,
                 file=rel, line=i + 1, end=end + 1, indent=ind, doc=clean_doc(doc), attrs=attrs, members=[], parent=None)
        types.append(t)
    for i, line in enumerate(clean):
        m = re.match(r"^(\s*)((?:public|internal)\s+)?enum\s+(\w+)\s*\{([^}]*)\}", lines[i]) if "enum" in line else None
        if m:
            types.append(dict(name=m[3], kind="enum", mods=(m[2] or "").split(), base="", ns=ns, layer=layer, file=rel, line=i + 1, end=i + 1,
                              indent=len(m[1]), doc="", attrs=[], parent=None,
                              members=[dict(kind="value", name=x.strip().split("=")[0].strip(), sig=x.strip(), doc="", line=i + 1) for x in m[4].split(",") if x.strip()]))
    # nesting
    for t in types:
        for o in types:
            if o is not t and o["line"] < t["line"] and o["end"] >= t["end"] and (t["parent"] is None or o["line"] > next(x for x in types if x["name"] == t["parent"])["line"]):
                t["parent"] = o["name"]
    # members
    for t in types:
        body_ind = t["indent"] + 4
        inner = [(x["line"], x["end"]) for x in types if x["parent"] == t["name"] and x["file"] == t["file"]]
        i = t["line"]  # 0-based index of line after the declaration line
        while i < t["end"] - 1:
            ln = i + 1  # 1-based number of lines[i]
            if any(a <= ln <= b for a, b in inner):
                i += 1; continue
            line = clean[i]
            raw_line = lines[i]
            ind = len(line) - len(line.lstrip(" "))
            s = line.strip()
            if ind != body_ind or not s or s[0] in "{}[/" or s.startswith("#"):
                i += 1; continue
            if t["kind"] == "enum":
                for nm in re.findall(r"(\w+)\s*(?:=\s*[^,]+)?(?:,|$)", s):
                    if nm and not nm[0].isdigit(): t["members"].append(dict(kind="value", name=nm, sig=nm, doc="", line=ln))
                i += 1; continue
            # gather the statement head (until ; { =>) across lines; `head` is cleaned (for tests), `rawhead` the same text unblanked
            def seg(k):
                cl = clean[k].rstrip(); lead = len(cl) - len(cl.lstrip())
                return cl[lead:], lines[k][:len(cl)][lead:]
            head, rawhead = seg(i); j = i
            while not re.search(r"[;{]|=>", head) and j + 1 < t["end"] - 1:
                j += 1; a_, b_ = seg(j); head += " " + a_; rawhead += " " + b_
            vis = next((mm for mm in re.findall(r"\b(public|internal|protected|private)\b", head.split("(")[0].split("=")[0]) ), None)
            is_public = vis in MEMBER_MODS or (t["kind"] == "interface")
            if not is_public:
                i = j + 1 if j > i else i + 1; continue
            cut = re.search(r"\s*(?:=>|\{|;)", head)
            sig = re.sub(r"\s+", " ", rawhead[:cut.start()] if cut else rawhead).strip()
            if re.match(r"^\w[\w<>\[\],\s?.]*\s+\w+\s*\(", sig) is None and "(" in sig and not sig.startswith(MEMBER_MODS):
                pass
            core = sig.split("=")[0].strip() if "(" not in sig.split("=")[0] else sig
            if "(" in core and not re.search(r"\b(operator)\b", core):
                nm = re.search(r"(\w+)\s*(?:<[^>]*>)?\s*\(", core)
                mk = "ctor" if nm and nm[1] == t["name"] else "method"
                name = nm[1] if nm else "?"
            elif re.search(r"\b(const)\b", sig) or ("{" not in head and "=>" not in head):
                names = re.findall(r"(\w+)\s*(?:=|,|$)", core.split("=")[0] if "," not in core else core)
                mk, name = "field", (names[-1] if names else core.split()[-1])
            else:
                nm = re.search(r"(\w+)\s*$", core)
                mk, name = "property", (nm[1] if nm else core)
            # doc above
            k, d = i - 1, []
            while k >= 0 and lines[k].strip().startswith(("///", "[")):
                if lines[k].strip().startswith("///"): d.insert(0, lines[k].strip())
                k -= 1
            t["members"].append(dict(kind=mk, name=name, sig=sig, doc=clean_doc(d), line=ln))
            i = j + 1 if j > i else i + 1
    return types, raw, rel


def collect_code():
    roots = [(UNITY / "Scripts" / "Core", "Core"), (UNITY / "Scripts" / "Game", "Game"),
             (UNITY / "Editor", "Editor"), (UNITY / "Tests", "Tests")]
    types, bodies = [], {}
    for root, layer in roots:
        for p in sorted(root.rglob("*.cs")):
            ts, raw, rel = parse_cs(p, layer)
            lines = raw.split("\n")
            for t in ts:
                t["body"] = "\n".join(lines[t["line"] - 1:t["end"]])
                t["tests"] = re.findall(r"\[(?:Test|UnityTest|TestCase\([^\]]*\))\]\s*(?:\[[^\]]*\]\s*)*public\s+(?:void|IEnumerator|async\s+\w+)\s+(\w+)", t["body"])
            types.extend(ts)
    # partial classes: one note, members from every part
    merged, seen = [], {}
    for t in types:
        k = (t["name"], t["layer"], t["parent"])
        if k in seen:
            o = seen[k]
            o["members"] += t["members"]; o["body"] += "\n" + t["body"]; o["tests"] += t["tests"]
            o["doc"] = o["doc"] or t["doc"]
            o.setdefault("extra", []).append(f"{t['file']}:{t['line']}")
        else:
            seen[k] = t; merged.append(t)
    count = defaultdict(int)
    for t in merged: count[(t["name"], t["layer"])] += 1
    for t in merged:
        if count[(t["name"], t["layer"])] > 1 and t["parent"]:
            t["name"] = f"{t['parent']}.{t['name']}"
    return merged


def note_name(t, dup):
    return f"{t['name']} ({t['layer']})" if t["name"] in dup else t["name"]


def gen_code(types):
    names = defaultdict(list)
    for t in types: names[t["name"]].append(t)
    dup = {n for n, v in names.items() if len(v) > 1}
    for t in types: t["note"] = note_name(t, dup)
    idx = {t["name"]: t for t in types if t["name"] not in dup}
    prod = [t for t in types if t["layer"] in ("Core", "Game", "Editor")]
    tests = [t for t in types if t["layer"] == "Tests"]
    use, usedby, tested = defaultdict(set), defaultdict(set), defaultdict(set)
    pat = {n: re.compile(r"\b" + re.escape(n) + r"\b") for n in idx}
    for t in types:
        body = strip_for_braces(t["body"])
        for n, rx in pat.items():
            if n == t["name"] or idx[n]["parent"] == t["name"] or t["parent"] == n: continue
            if rx.search(body):
                (tested if t["layer"] == "Tests" else use)[t["note"]].add(n) if False else None
                if t["layer"] == "Tests":
                    if t["tests"]: tested[n].add(t["note"])
                else: use[t["note"]].add(n); usedby[n].add(t["note"])
    for t in types:
        if t["layer"] == "Tests": continue
        lines = [fm(type="code", layer=t["layer"], kind=t["kind"], namespace=t["ns"] or "-", file=t["file"],
                    tags=[f"code/{t['layer'].lower()}", f"kind/{t['kind']}"], aliases=[t["name"]] if t["note"] != t["name"] else []),
                 f"# {t['name']}", "",
                 f"`{' '.join(t['mods'] + [t['kind'], t['name']])}`" + (f" : `{t['base']}`" if t["base"] else ""),
                 f"- Layer: **{t['layer']}** · Source: `{t['file']}:{t['line']}-{t['end']}`" + (f" (+ partial: {', '.join('`' + x + '`' for x in t['extra'])})" if t.get("extra") else "") + (f" · Nested in {link(next(x['note'] for x in types if x['name'] == t['parent']))}" if t["parent"] else ""),
                 ""]
        if t["doc"]: lines += [f"> {t['doc']}", ""]
        if t["members"]:
            lines.append("## Members")
            if t["kind"] == "enum":
                lines.append(", ".join(f"`{m['name']}`" for m in t["members"]))
            else:
                lines += ["| Kind | Signature | Line | Note |", "|---|---|---|---|"]
                for m in t["members"]:
                    lines.append(f"| {m['kind']} | `{m['sig'][:150].replace('|', chr(92) + '|')}` | {m['line']} | {m['doc'].replace('|', '/')} |")
            lines.append("")
        if use[t["note"]]:
            lines += ["## Uses", ", ".join(link(idx[n]["note"], n) for n in sorted(use[t["note"]])), ""]
        if usedby[t["name"]]:
            lines += ["## Used by", ", ".join(link(x) for x in sorted(usedby[t["name"]])), ""]
        if tested[t["name"]]:
            lines += ["## Tested by", ", ".join(link(x) for x in sorted(tested[t["name"]])), ""]
        w(f"Code/{t['layer']}/{t['note']}.md", "\n".join(lines))
    # index per layer
    by_layer = defaultdict(list)
    for t in prod: by_layer[t["layer"]].append(t)
    for layer, ts in by_layer.items():
        files = defaultdict(list)
        for t in ts: files[t["file"]].append(t)
        L = [fm(type="moc", tags=["moc/code"]), f"# Code: {layer}", f"{len(ts)} types in {len(files)} files. Regenerated by `tools/vault/build_vault.py`.", ""]
        for f in sorted(files):
            L.append(f"## `{f.split('/', 3)[-1]}`")
            for t in files[f]:
                L.append(f"- {link(t['note'])} _{t['kind']}_ — {t['doc'][:110]}")
            L.append("")
        w(f"Code/{layer} Index.md", "\n".join(L))
    # tests
    tfiles = defaultdict(list)
    for t in tests: tfiles[t["file"]].append(t)
    allt = [fm(type="moc", tags=["moc/tests"]), "# Tests", f"{sum(len(t['tests']) for t in tests)} test methods in {len(tfiles)} files. Run: `dotnet test tools/CoreTests` (rules) or `unity test Athanor --mode EditMode`.", ""]
    for f in sorted(tfiles):
        for t in tfiles[f]:
            if not t["tests"]: continue
            tgt = sorted(n for n in idx if pat[n].search(strip_for_braces(t["body"])) and idx[n]["layer"] != "Tests" and n != t["name"])
            body = [fm(type="test", file=f, tags=["test"], count=len(t["tests"])), f"# {t['name']}", f"`{f}` — {len(t['tests'])} tests", "",
                    "## Covers", ", ".join(link(idx[n]["note"], n) for n in tgt) or "-", "", "## Tests"] + [f"- `{x}`" for x in t["tests"]]
            w(f"Tests/{t['note']}.md", "\n".join(body) + "\n")
            allt.append(f"- {link(t['note'])} — {len(t['tests'])} tests · covers {', '.join(link(idx[n]['note'], n) for n in tgt[:8])}")
    w("Tests/Tests Index.md", "\n".join(allt) + "\n")
    return types


# ---------------------------------------------------------------- content

def gen_content():
    d = json.loads((UNITY / "Resources" / "content.json").read_text(encoding="utf-8"))
    mats = {m["id"]: m for m in d["materials"]}
    made, used = defaultdict(list), defaultdict(list)
    for i, r in enumerate(d["recipes"]):
        items = r.get("items") or [r.get("a"), r.get("b")]
        for it in set(items): used[it].append(i)
        made[r["result"]].append(i)
    ml = lambda mid: link(f"mat.{mid}", mats[mid]["name"]) if mid in mats else f"`{mid}`"
    rtxt = lambda r: " + ".join(ml(x) for x in (r.get("items") or [r.get("a"), r.get("b")])) + f" → {ml(r['result'])}" + (f" _(heat ≥ {r['minReactivity']})_" if r.get("minReactivity") else "")
    ult = d.get("ultimate", "")
    # materials
    for m in d["materials"]:
        L = [fm(type="material", id=m["id"], kind=m["kind"], tier=m["tier"], reactivity=m["reactivity"], rarity=(m.get("rarity") or "Common") if m["tier"] == 0 else "-",
                locked=str(m.get("locked", False)).lower(), aliases=[m["name"]], tags=["content/material", f"kind/{m['kind']}", f"tier/{m['tier']}"]),
             f"# {m['name']}", f"`{m['id']}` · **{m['kind']}** · tier {m['tier']} · reaction index {m['reactivity']} · glow `{m['glow']}`"
             + (f" · starting material ({m.get('rarity') or 'Common'}{', locked: won from a goal' if m.get('locked') else ''})" if m["tier"] == 0 else "")
             + (" · **THE ULTIMATE SUBSTANCE**" if m["id"] == ult else ""), ""]
        L += ["## Made by"] + ([f"- {rtxt(d['recipes'][i])}" for i in made[m["id"]]] or ["- (not made by any recipe)"]) + ["", "## Used in"]
        L += [f"- {rtxt(d['recipes'][i])}" for i in used[m["id"]]] or ["- (not used by any recipe)"]
        L += ["", "Back to " + link("Materials")]
        w(f"Content/Materials/mat.{m['id']}.md", "\n".join(L) + "\n")
    bykind = defaultdict(list)
    for m in d["materials"]: bykind[m["kind"]].append(m)
    L = [fm(type="moc", tags=["moc/content"]), "# Materials", f"{len(mats)} materials, {len(d['recipes'])} recipes. Ultimate: {ml(ult) if ult in mats else ult}.", "",
         "| Tier | Materials |", "|---|---|"]
    for tier in sorted({m["tier"] for m in d["materials"]}):
        L.append(f"| {tier} | " + ", ".join(ml(m["id"]) + f" ({m['reactivity']})" for m in d["materials"] if m["tier"] == tier) + " |")
    L += ["", "## By kind"] + [f"- **{k}** ({len(v)}): " + ", ".join(ml(m["id"]) for m in v) for k, v in sorted(bykind.items())]
    L += ["", "## Starting materials", ", ".join(ml(m["id"]) + (" 🔒" if m.get("locked") else "") for m in d["materials"] if m["tier"] == 0)]
    w("Content/Materials.md", "\n".join(L) + "\n")
    # recipes
    L = [fm(type="moc", tags=["moc/content"]), "# Recipes", f"{len(d['recipes'])} recipes. Result tier is the tier of the product.", "", "| # | Recipe | Result tier |", "|---|---|---|"]
    for i, r in enumerate(d["recipes"]):
        L.append(f"| {i} | {rtxt(r)} | {mats.get(r['result'], {}).get('tier', '?')} |")
    w("Content/Recipes.md", "\n".join(L) + "\n")
    # catalysts
    L = [fm(type="moc", tags=["moc/content"]), "# Catalysts", "", "| Catalyst | Effect | Kind | Amount | Rarity | Locked | Text |", "|---|---|---|---|---|---|---|"]
    for c in d["catalysts"]:
        L.append(f"| {link('cat.' + c['id'], c['name'])} | `{c['effect']}` | {c.get('kind') or '-'} | {c.get('amount')} | {c.get('rarity', 'Common')} | {'🔒' if c.get('locked') else ''} | {c['text']} |")
        w(f"Content/Catalysts/cat.{c['id']}.md", fm(type="catalyst", id=c["id"], effect=c["effect"], rarity=c.get("rarity", "Common"), locked=str(c.get("locked", False)).lower(), aliases=[c["name"]], tags=["content/catalyst"])
          + f"# {c['name']}\n\n{c['text']}\n\n- effect `{c['effect']}` · kind `{c.get('kind') or '-'}` · amount {c.get('amount')} · {c.get('rarity', 'Common')}{' · locked (won from a goal)' if c.get('locked') else ''}\n- Implemented in {link('RunState')} / {link('IgniteResolver')} (search `Effects.{c['effect']}`)\n\nBack to {link('Catalysts')}\n")
    w("Content/Catalysts.md", "\n".join(L) + "\n")
    # aspects
    L = [fm(type="moc", tags=["moc/content"]), "# Aspects", "", "| Aspect | Effect | Scope | Group | Trade-off | Text |", "|---|---|---|---|---|---|"]
    for a in d["aspects"]:
        L.append(f"| {link('asp.' + a['id'], a['name'])} | `{a['effect']}` | {a['scope']} | {a['group']} | {'yes' if a.get('tradeOff') else ''} | {a['text']} |")
        w(f"Content/Aspects/asp.{a['id']}.md", fm(type="aspect", id=a["id"], effect=a["effect"], scope=a["scope"], group=a["group"], aliases=[a["name"]], tags=["content/aspect"])
          + f"# {a['name']}\n\n{a['text']}\n\n- effect `{a['effect']}` · scope {a['scope']} · group {a['group']} · amount {a.get('amount')} · trade-off: {a.get('tradeOff')}\n- Needs icon `asp_{a['id']}` in `tools/art/icons.mjs`\n- See {link('BenchRules')}\n\nBack to {link('Aspects')}\n")
    w("Content/Aspects.md", "\n".join(L) + "\n")
    # events
    L = [fm(type="moc", tags=["moc/content"]), "# Events", f"{len(d['events'])} events; `noEventWeight` = {d.get('noEventWeight')}.", "", "| Event | Type | Depths | Weight | Options |", "|---|---|---|---|---|"]
    fmt = lambda es: "; ".join(f"{e['op']}{('(' + e['arg'] + ')') if e.get('arg') else ''} {e['amount']}" for e in es) or "free"
    for e in d["events"]:
        L.append(f"| {link('evt.' + e['id'], e['name'])} | {e['type']} | {e['minDepth']}–{e['maxDepth']} | {e['weight']} | {len(e['options'])} |")
        B = [fm(type="event", id=e["id"], event_type=e["type"], min_depth=e["minDepth"], max_depth=e["maxDepth"], weight=e["weight"], aliases=[e["name"]], tags=["content/event", f"event/{e['type'].lower()}"]),
             f"# {e['name']}", f"_{e['type']}_ · depth {e['minDepth']}–{e['maxDepth']} · weight {e['weight']}", "", e["text"], ""]
        for o in e["options"]:
            B += [f"## Option: {o['label']}" + (f"  (requires {o['requires']})" if o.get("requires") else ""), f"- Cost: {fmt(o['cost'])}"]
            tw = sum(x["weight"] for x in o["outcomes"]) or 1
            for x in o["outcomes"]:
                B.append(f"- {round(100 * x['weight'] / tw)}%: “{x['text']}” → {fmt(x['gain'])}")
            B.append("")
        B.append(f"Back to {link('Events')} · rules in {link('RunState')} (event draw/apply, `RunStart.cs`)")
        w(f"Content/Events/evt.{e['id']}.md", "\n".join(B) + "\n")
    w("Content/Events.md", "\n".join(L) + "\n")
    # goals
    L = [fm(type="moc", tags=["moc/content"]), "# Goals", "Goals pay one random locked item of the reward kind/rarity (see " + link("Profile") + ").", "", "| Goal | Stat | Target | Reward |", "|---|---|---|---|"]
    for g in d["goals"]:
        L.append(f"| `{g['id']}` {g['text']} | `{g['stat']}` | {g['target']} | {g['rewardKind']} · {g['rewardRarity']} |")
    w("Content/Goals.md", "\n".join(L) + "\n")
    # depths
    tg = d["depthTargets"]
    L = [fm(type="moc", tags=["moc/content"]), "# Depths", "", "| Depth | Name | Potency target |", "|---|---|---|"]
    for i, n in enumerate(d["depthNames"]):
        L.append(f"| {i + 1} | {n} | {tg[i] if i < len(tg) else 'ultimate'} |")
    L += ["", f"Ultimate substance: {ml(ult) if ult in mats else ult}", ""]
    w("Content/Depths.md", "\n".join(L) + "\n")
    stats = dict(materials=len(mats), recipes=len(d["recipes"]), catalysts=len(d["catalysts"]), aspects=len(d["aspects"]), events=len(d["events"]), goals=len(d["goals"]), depths=len(d["depthNames"]))
    # content health (cheap static checks the agents can lean on)
    issues, info = [], []
    for r in d["recipes"]:
        for x in (r.get("items") or []) + [r["result"]]:
            if x not in mats: issues.append(f"recipe references unknown material `{x}`")
    for m in d["materials"]:
        if not made[m["id"]] and m["tier"] != 0: issues.append(f"`{m['id']}` (tier {m['tier']}) is not a starter and no recipe makes it")
        if not used[m["id"]] and m["id"] != ult: info.append(f"`{m['id']}` (tier {m['tier']}) is a dead end: no recipe uses it")
    w("Content/Content Health.md", fm(type="report", tags=["report"]) + "# Content health\n\nStatic checks only; the real rules live in `tools/CoreTests` (`ContentTests`).\n\n" + ("## Problems\n" + ("\n".join(f"- ⚠ {i}" for i in issues) if issues else "- none") + "\n\n## Dead ends (fine if intended)\n" + ("\n".join(f"- {i}" for i in info) or "- none") + "\n"))
    return d, stats, mats


# ---------------------------------------------------------------- agents, tools, phases

def front(path):
    t = path.read_text(encoding="utf-8")
    m = re.match(r"^---\n(.*?)\n---\n", t, re.S)
    out = {}
    if m:
        for line in m[1].split("\n"):
            k = re.match(r"^(\w[\w-]*):\s*(.*)$", line)
            if k: out[k[1]] = k[2].strip().strip('"')
    return out


def gen_agents():
    ag = sorted((REPO / ".claude" / "agents").glob("*.md"))
    L = [fm(type="moc", tags=["moc/agents"]), "# Agents", f"{len(ag)} sub-agents in `.claude/agents/`. Athanor's own roles are marked ★ (see {link('Agent Workflow')}).", "",
         "| Agent | Model | Description |", "|---|---|---|"]
    for p in ag:
        f = front(p)
        own = "★ " if f.get("project") == "athanor" else ""
        L.append(f"| {own}{link('agent.' + p.stem, p.stem)} | {f.get('model', '-')} | {f.get('description', '')[:150]} |")
        w(f"Agents/agent.{p.stem}.md", fm(type="agent", name=p.stem, model=f.get("model", "inherit"), source=f".claude/agents/{p.name}", tags=["agent"] + (["agent/athanor"] if own else []))
          + f"# {p.stem}\n\n{f.get('description', '')}\n\n- Definition: `.claude/agents/{p.name}`\n- Model: {f.get('model', 'inherit')} · tools: {f.get('tools', 'all')}\n")
    w("Agents/Agents Index.md", "\n".join(L) + "\n")
    sk = sorted((REPO / ".claude" / "skills").glob("*/SKILL.md"))
    L = [fm(type="moc", tags=["moc/skills"]), "# Skills (slash commands)", f"{len(sk)} skills in `.claude/skills/`.", ""]
    for p in sk:
        L.append(f"- `/{p.parent.name}` — {front(p).get('description', '')[:160]}")
    w("Agents/Skills Index.md", "\n".join(L) + "\n")


def gen_tools():
    L = [fm(type="moc", tags=["moc/tools"]), "# Tools", "Scripts outside Unity (art, fx, web tests, rule tests, this vault).", ""]
    for p in sorted((REPO / "tools").rglob("*")):
        if p.suffix not in (".mjs", ".js", ".py", ".sh", ".cs") or any(x in p.parts for x in ("bin", "obj", "node_modules", "vendor")): continue
        head = ""
        for line in p.read_text(encoding="utf-8", errors="replace").split("\n")[:12]:
            s = line.strip()
            if s.startswith(("//", "#", "/*", "*")) and not s.startswith("#!"):
                head = s.lstrip("/#* ").strip();
                if head: break
        L.append(f"- `{p.relative_to(REPO).as_posix()}` — {head}")
    w("Tools/Tools Index.md", "\n".join(L) + "\n")


def gen_phases():
    L = [fm(type="moc", tags=["moc/phases"]), "# Phases", "Devlogs record decisions and results; specs/plans hold the designs. All of these live in `docs/` and are part of the vault.", ""]
    readme = REPO / "docs" / "devlogs" / "README.md"
    if readme.exists():
        for line in readme.read_text(encoding="utf-8").split("\n"):
            m = re.match(r"^\|\s*(\d+)\s*\|\s*\[([^\]]+)\]\(([^)]+)\)\s*\|\s*(.*?)\s*\|?$", line)
            if m: L.append(f"- Phase {m[1]}: [[docs/devlogs/{m[3].replace('.md', '')}|{m[2]}]] — {m[4]}")
    for sub, title in (("specs", "Specs"), ("plans", "Plans")):
        L += ["", f"## {title}"] + [f"- [[docs/superpowers/{sub}/{p.stem}|{p.stem}]]" for p in sorted((REPO / "docs" / "superpowers" / sub).glob("*.md"))]
    w("Phases/Phases.md", "\n".join(L) + "\n")


def gen_index(types, d, stats):
    """Machine index as Markdown tables (Obsidian shows .md only); tools/vault/q.py parses these tables."""
    e = lambda x: str(x).replace("|", "\\|").replace("\n", " ")
    L = [fm(type="index", tags=["index"]), "# Symbol index", "",
         "Flat tables behind `tools/vault/q.py`. For browsing use the per-type notes in `Code/` and `Content/`.", "",
         "## Stats", "| key | count |", "|---|---|"] + [f"| {k} | {v} |" for k, v in stats.items()]
    L += ["", "## Types", "| name | note | layer | kind | file | line | doc |", "|---|---|---|---|---|---|---|"]
    L += [f"| {e(t['name'])} | {e(t['note'])} | {t['layer']} | {t['kind']} | {t['file']} | {t['line']} | {e(t['doc'][:200])} |" for t in types]
    L += ["", "## Members", "| type | kind | name | line | sig |", "|---|---|---|---|---|"]
    L += [f"| {e(t['name'])} | {m['kind']} | {e(m['name'])} | {m['line']} | {e(m['sig'][:200])} |" for t in types for m in t["members"]]
    L += ["", "## Materials", "| id | name | kind | tier | reactivity |", "|---|---|---|---|---|"]
    L += [f"| {m['id']} | {e(m['name'])} | {m['kind']} | {m['tier']} | {m['reactivity']} |" for m in d["materials"]]
    L += ["", "## Recipes", "| items | minReactivity | result |", "|---|---|---|"]
    L += [f"| {', '.join(r.get('items') or [r.get('a'), r.get('b')])} | {r.get('minReactivity', 0)} | {r['result']} |" for r in d["recipes"]]
    L += ["", "## Lists"] + [f"- {k}: {', '.join(x['id'] for x in d[k])}" for k in ("catalysts", "aspects", "events", "goals")]
    w("_index/Symbols.md", "\n".join(L) + "\n")


def main():
    for g in GEN_DIRS:
        shutil.rmtree(VAULT / g, ignore_errors=True)
    VAULT.mkdir(exist_ok=True)
    types = gen_code(collect_code())
    d, stats, _ = gen_content()
    gen_agents(); gen_tools(); gen_phases(); gen_index(types, d, stats)
    nt = sum(1 for t in types if t["layer"] != "Tests")
    files = sum(1 for _ in VAULT.rglob("*.md"))
    print(f"vault: {nt} code types, {sum(len(t['tests']) for t in types)} tests, content {stats}, {files} notes total")


if __name__ == "__main__":
    sys.exit(main())
