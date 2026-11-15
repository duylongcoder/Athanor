---
type: canvas
tags: [obsidian-tech, graph]
structure: mind-map
author: athanor-devs
source: Internal Obsidian Graph View mechanism
---

# How Obsidian Graph View Builds Its Graph

## Data Sources for Graph

Obsidian's Graph View reads data from **3 sources** in this order:

```
[Athanor Vault → Graph Data]

   └─→ PRIMARY SOURCE: `[[links]]` embedded in content
        │
        ├─→ Each file is scanned line-by-line
        │    └─→ Regex pattern: `^\[\[.*?\]\]\s*$`
        │         │
        │         └─→ Extract all links per file
        │                │
        │                └─→ Create node entries
        │                     │
        │                     ├─→ File name → [[file-link]]
        │                     │
        │                     └─→ Link text → [[link-name]]
        │
        ├─→ SECONDARY: Metadata (frontmatter)
        │    └─→ type field determines graph node role
        │         ├─→ `type: home` → hub node
        │         ├─→ `type: design` → specialized node
        │         ├─→ `type: test` → data node
        │         └─→ `tags[]` → metadata attributes
        │
        └─→ TERTIARY: File content relationships
             └─→ References within text (not `[[links]]`)
                  └─→ Not indexed for graph; only `[[links]]` matter

   ↓ Graph Algorithm

[Mixing & Weighting]
   └─→ Distance from last edit determines link strength
        │
        ├─→ Files edited in same session = higher weight
        │    └─→ Edges are thicker (visually)
        │
        ├─→ Cross-session connections = lower weight
        │    └─→ Thinner edges, more prone to removal
        │
        └─→ Direct links override inferred links
             └─→ Explicit [[A → B]] beats A indirectly → B

   ↓ Graph Output

[Graph Elements]
   ├─→ Nodes (from `[[links]]`)
   │    ├─→ Each unique `[[link]]` becomes one node
   │    │    ├─→ If [[ContentDb]] appears in 5 files → large node
   │    │    └─→ type field from frontmatter determines role
   │    │
   │    └─→ Node sizes:
   │         ├─→ High degree (many links) → Larger
   │         ├─→ Low degree → Smaller
   │         └─→ Default = 100px, scaled by degree

   └─→ Links (edges between nodes)
        └─→ Each `[[link]]` in file creates one edge
             └─→ Weighted by recency (last edit date)
                  │
                  └─→ Thicker if edited within 7 days of other node
```

## Athanor Vault Graph Structure

Based on vault content, graph will contain:

### Core Nodes (high degree files)
- `[[ContentDb]]` — referenced in EVERY file about gameplay rules
- `[[RunState]]` — central to run mechanics docs
- `[[BenchView]]` — core screen referenced across game docs
- `[[GameFlow]]` — state machine appears in all flow docs

### Content Nodes (medium degree)
- `[[Materials]]`, `[[Recipes]]` — 50+ files reference these
- `[[Catalysts]]`, `[[Aspects]]` — ~26 each with related files
- `[[Events]]`, `[[Goals]]` — ~19 and 22 files respectively

### Test Nodes (distributed)
- Each test system file: `[[ContentTests]]`, `[[LoreTests]]`, etc.
- Connected to their target systems via references

### Metadata Nodes (low degree but important)
- `[[Type]]` fields in frontmatter
- `[[Tag]]` fields from tags[] arrays

## Graph Query Commands

Obsidian's `/q.py` creates dynamic subgraphs:

```bash
# Find nodes by tag
python3 tools/vault/q.py mat → Shows [[Materials]] node + neighbors

# Find usage paths  
python3 tools/vault/q.py uses ←→ [[ContentDb]] → Path analysis graph

# Filter by type
python3 tools/vault/q.py type="core" → Core layer subgraph only
```

## What Gets Visualized

| File Type | Graph Node Size | Link Density |
|-----------|-----------------|--------------|
| Architecture docs | Large (hub) | High |
| Code index files | Medium-high | Very high |
| Content files | Medium | High |
| Test documentation | Small-medium | Low-moderate |

### Connection Rules in Graph

1. **Direct links** → Strong edge (thick, darker)
2. **Same-day edits** → Higher weight edge
3. **Cross-category links** → Lower weight but shows integration

## Key Insight: `[[links]]` = Graph Edges

The entire graph is **just the set of all `[[links]]`** in all files. No external graph structure exists — Obsidian builds it dynamically by scanning every file's markdown content for link patterns.

When you see a cluster around `[[ContentDb]]` → `[[RunState]]` → `[[BenchView]]`:

```
[[ContentDb]]
  │    ↙↘
  │     ↓     [[BenchView]]
  └───→ [[RunState]]
           │
           └───→ [[IgniteResolver]]
                     │
                     └──→ [[ChainPlanner]]
```

This structure emerges because those files all reference `[[ContentDb]]` as a core dependency. The graph is **static file-based**, not dynamic runtime.