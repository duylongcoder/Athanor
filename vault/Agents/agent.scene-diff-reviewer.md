---
generated: true
type: agent
name: scene-diff-reviewer
model: sonnet
source: .claude/agents/scene-diff-reviewer.md
tags: [agent, agent/athanor]
---
# scene-diff-reviewer

Athanor read-only reviewer for .unity / .prefab diffs: separates real serialized changes from Editor noise (block reordering, float rounding, camera and light drift). Use after any batch scene or prefab run, before the owner commits.

- Definition: `.claude/agents/scene-diff-reviewer.md`
- Model: sonnet · tools: Read, Glob, Grep, Bash
