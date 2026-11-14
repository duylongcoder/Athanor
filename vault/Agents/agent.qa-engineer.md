---
generated: true
type: agent
name: qa-engineer
model: inherit
source: .claude/agents/qa-engineer.md
tags: [agent, agent/athanor]
---
# qa-engineer

QA and balance verifier for Athanor: runs the dotnet and Unity tests, balance/role/event reports and headless-Chrome web checks, reproduces bugs, writes failing tests, signs off a task. Use after any task and before a build is called done.

- Definition: `.claude/agents/qa-engineer.md`
- Model: inherit · tools: Read, Glob, Grep, Write, Edit, Bash
