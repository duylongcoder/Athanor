---
type: workflow
tags: [workflow, phase5]
status: draft - owner decides scope
---
# Phase 5 Kickoff

Phase 4 (tutorial, lore, settings/localization research, depth layers, packages) is in [[Phases]]. Phase 5's scope is **not decided**; this note collects what Phase 4 left open so the owner can pick, and shows how each candidate would be split across [[Agent Workflow]]. Source: `docs/devlogs/phase-4-*.md` open-question lists.

| Candidate | Open owner decisions | Roles (in order) |
|---|---|---|
| Vietnamese build + Settings screen | font choice (option C recommended in the settings devlog), where settings live, pause menu on mobile | ui-ux-designer → unity-gameplay-engineer → localizer-vi → qa-engineer |
| Sound (FMOD or plain Unity audio) | is sound in scope; FMOD licence/ownership; click-to-start for WebGL autoplay; volume controls | audio-designer → unity-gameplay-engineer → web-release-engineer (size) → qa-engineer |
| Depth layers / fog (2.5D look) | drop AERO for a sprite fog layer; background only or in front of the bench | ui-ux-designer → pixel-artist → unity-gameplay-engineer → qa-engineer |
| Depth intros (one line per depth) | postponed in Phase 4: bring back? | lore-writer → localizer-vi → unity-gameplay-engineer |
| Content depth (more recipes/events) | target counts, new Lore events | content-designer ↔ lore-writer → localizer-vi → qa-engineer (BalanceMatrix) |
| Tutorial polish / first-launch rules | tutorial from title screen too? | unity-gameplay-engineer → qa-engineer |
| Repository hygiene | `tools/CoreTests/bin,obj` are tracked build output; package licences (PrimeTween, FMOD, AERO) and repo privacy | web-release-engineer |

## How to start
1. Owner picks scope from the table (or adds rows).
2. Main session invokes `phase-lead` with the chosen rows; it writes the plan to `docs/superpowers/plans/` and the briefs.
3. Dispatch per [[Agent Workflow]].
