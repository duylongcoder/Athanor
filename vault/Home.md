---
type: home
tags: [moc]
---
# Athanor — vault home

Single source of project knowledge: code structure, data design, content, events, tests, agents, phases.
Generated notes are rebuilt from the repo by `python3 tools/vault/build_vault.py`; hand-written notes (Design, Data, Workflow, Templates) are edited here.

## Start here
- [[Game Overview]] — what the game is and its core loop
- [[Architecture]] — layers, run lifecycle, who owns what
- [[Data Design]] — content assets, lang files, save keys, `layout.json`
- [[Vault Guide]] — how humans and agents query and update this vault
- [[Agent Workflow]] — which sub-agent does what; how a phase is split
- [[Phase 5 Kickoff]] — the open Phase 5 backlog and its task split

## Maps (generated)
- Code: [[Gameplay Index]] · [[Game Index]] · [[Editor Index]] · [[Tests Index]] · [[Tools Index]]
- Content: [[Materials]] · [[Recipes]] · [[Catalysts]] · [[Aspects]] · [[Events]] · [[Goals]] · [[Depths]] · [[Content Health]]
- Project: [[Phases]] · [[Agents Index]] · [[Skills Index]]

## Query it from the terminal
`python3 tools/vault/q.py type|member|uses|mat|recipe|list|find <arg>` — see [[Vault Guide]].
