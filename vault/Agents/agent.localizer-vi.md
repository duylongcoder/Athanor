---
generated: true
type: agent
name: localizer-vi
model: sonnet
source: .claude/agents/localizer-vi.md
tags: [agent, agent/athanor]
---
# localizer-vi

English-Vietnamese localizer for Athanor: keeps lang/vi.json in parity with lang/en.json, translates new strings, checks fit in the pixel UI. Use whenever player-visible text was added or changed.

- Definition: `.claude/agents/localizer-vi.md`
- Model: sonnet · tools: Read, Glob, Grep, Write, Edit, Bash
