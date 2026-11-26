---
type: workflow
tags: [workflow, vault]
---
# Vault Guide

## Layout
| Folder | Kind | Source of truth |
|---|---|---|
| `Design/`, `Data/`, `Workflow/`, `Templates/`, `Home.md` | hand-written | this vault |
| `Code/{Core,Game,Editor}/` | generated: one note per type (members, file:line, Uses / Used by / Tested by) | C# under `Athanor/Assets` |
| `Tests/` | generated: one note per test class, what it covers | `Assets/Tests` |
| `Content/` | generated: one note per material, catalyst, aspect, event + goal/recipe/depth tables | `tools/content/snapshot.json` (written from the content assets) |
| `Agents/`, `Tools/`, `Phases/` | generated indexes | `.claude/`, `tools/`, `docs/ATHANOR.md` §4 and §7 |
| `_index/Symbols.md` | generated flat tables used by `q.py` (Markdown, like everything else) | all of the above |

`docs/ATHANOR.md` is also inside the vault, so `[[docs/ATHANOR]]` links work. Open the **repo root** as the vault in Obsidian; `.obsidian/app.json` hides Unity's `Library`, `Build`, `node_modules` and other build output.

## Keeping it fresh
`python3 tools/vault/build_vault.py` (stdlib only, ~1 s). Run it after any change to C#, the content assets (then `Athanor > Content > Write Snapshot`), agents or `docs/ATHANOR.md` §4 / §7. It deletes and rewrites only the generated folders; hand-written notes are never touched. Generated notes say `generated: true` in frontmatter: never edit them.

## Querying (agents and humans)
```
python3 tools/vault/q.py type RunState        # file:line + public members
python3 tools/vault/q.py member Ignite        # every member matching a name
python3 tools/vault/q.py uses ChainPlanner    # who uses it, who tests it
python3 tools/vault/q.py mat ember_coal       # recipes that make / use a material
python3 tools/vault/q.py list events
python3 tools/vault/q.py find "trial"         # text search across the vault
```
In Obsidian: open a type note and use the **Backlinks** and **Graph** panes (local graph, depth 2) to see impact; filter the graph by `tag:#code/core`, `#content/event`, `#agent/athanor`.
Frontmatter `type`, `layer`, `kind`, `tier`, `reactivity`, `event_type`, `min_depth` are queryable with Bases or Dataview.

## Writing hand notes
Use the templates in `Templates/`. Link to types with `[[TypeName]]` and to content with `[[mat.ember_coal]]`. A decision goes to *Decisions in force* in `docs/ATHANOR.md` first, then a one-line note here only if it changes the structure or the data design.

## Extending the generator
`tools/vault/build_vault.py` has one function per section (`gen_code`, `gen_content`, `gen_agents`, …). To expose a new thing (say, lang keys per screen), add a `gen_*` function writing under a generated folder and add the folder to `GEN_DIRS`.
