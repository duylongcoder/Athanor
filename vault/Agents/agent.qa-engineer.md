---
generated: true
type: agent
name: qa-engineer
model: inherit
source: .claude/agents/qa-engineer.md
tags: [agent, agent/athanor]
---
# qa-engineer

Athanor sign-off runner: executes the Unity tests (EditMode and PlayMode), the balance reports, headless Chrome web checks; reproduces bugs with a seed and a failing test. Use after any task and before a build is called done.

- Definition: `.claude/agents/qa-engineer.md`
- Model: inherit · tools: Read, Glob, Grep, Write, Edit, Bash
