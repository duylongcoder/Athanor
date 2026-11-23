---
generated: true
type: agent
name: localizer-vi
model: sonnet
source: .claude/agents/localizer-vi.md
tags: [agent, agent/athanor]
---
# localizer-vi

English to Vietnamese translator for Athanor: keeps lang/vi.json key-for-key with en.json, maintains the glossary, flags strings that overflow the pixel UI. Use whenever player-visible text was added or changed.

- Definition: `.claude/agents/localizer-vi.md`
- Model: sonnet · tools: Read, Glob, Grep, Write, Edit, Bash
