---
generated: true
type: agent
name: phase-lead
model: opus
source: .claude/agents/phase-lead.md
tags: [agent, agent/athanor]
---
# phase-lead

Athanor phase planner: reads the vault and `docs/ATHANOR.md`, summarises project state, and returns a dispatch plan of briefs per Athanor role. Plans only, never implements. Use at the start of a phase or for a goal spanning several roles.

- Definition: `.claude/agents/phase-lead.md`
- Model: opus · tools: Read, Glob, Grep, Write, Edit, Bash
