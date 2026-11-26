---
type: canvas
tags: [agent, workflow]
structure: mind-map
author: athanor-devs
generated-from: Agents Index
---

# Athanor Agent Canvas

```
[Agent Team Hierarchy]

   └─→ Phase 5 Kickoff → Task Split Workstream
        │
        ├─→ Primary Owner: phase-lead
        │    └─→ Summarise → decide → split → dispatch
        │         ├─→ Briefs to department leads
        │         └─→ Acceptance: sign-off from qa-engineer
        │
        ├─→ Rules Engineer Layer
        │    ├─→ rules-engineer (primary)
        │    │    ├─→ Core rules, scoring, run state
        │    │    └─→ Core tests + validation
        │    │
        │    ├─→ quality-gates (escalation)
        │    │    ├─→ creative-director: design conflicts
        │    │    └─→ technical-director: technical conflicts
        │
        ├─→ Unity Gameplay Engineer Layer
        │    ├─→ unity-gameplay-engineer (primary)
        │    │    ├─→ Screens, views, GameManager
        │    │    ├─→ Save plumbing, cutscenes
        │    │    └─→ Editor tools
        │    │
        │    ├─→ engine-programmer / unity-specialist
        │    │    ├─→ Rendering pipeline, physics
        │    │    └─→ Memory, scene management
        │
        ├─→ Content Designer Layer
        │    ├─→ content-designer (primary)
        │    │    ├─→ Balancing, tuning, values
        │    │    └─→ Icon consistency in tools/art/icons.mjs
        │    │
        │    ├─→ economy-designer (secondary)
        │    │    └─→ Loot tables, sink/faucet analysis
        │    │
        │    └─→ systems-designer (support)
        │         └─→ Mathematical modeling, formulas
        │
        ├─→ UI/UX Designer Layer
        │    ├─→ ui-ux-designer (primary)
        │    │    ├─→ Mockups, screen layouts
        │    │    └─→ 640x360 pixel specs
        │    │
        │    ├─→ art-director / pixel-artist
        │    │    ├─→ Style guides, art bible
        │    │    └─→ Color palettes, icon standards
        │    │
        │    └─→ localization-lead (support)
        │         └─→ i18n system design, translation pipeline
        │
        ├─→ QA & Release Layer
        │    ├─→ qa-engineer (primary)
        │    │    ├─→ Balance reports, web build checks
        │    │    └─→ Bug triage, release sign-off
        │    │
        │    ├─→ qa-lead / qa-tester
        │    │    ├─→ Test strategy creation
        │    │    └─→ Regression checklist
        │    │
        │    └─→ web-release-engineer (release)
        │         └─→ Build pipelines, certification checklists
        │
        ├─→ Developer Layer
        │    ├─→ lead-programmer (architecture + review)
        │    │    ├─→ Code architecture decisions
        │    │    └─→ API design, refactoring strategy
        │    │
        │    ├─→ technical-director
        │    │    ├─→ Engine architecture, tech choices
        │    │    └─→ Performance strategy, technical risk
        │    │
        │    └─→ devops-engineer (CI/CD)
        │         └─→ Build pipelines, version control

   └─→ Supporting Agents

       ├─→ lore-writer / writer
       │    ├─→ Narrative + player-facing text
       │    ├─→ World building + character design
       │    └─→ Environmental storytelling
       │
       ├─→ narrative-director
       │    └─→ Story architecture, cutscene structure
       │
       ├─→ audio-designer / sound-designer
       │    ├─→ Event documentation, SFX specs
       │    └─→ Music direction, mix balance
       │
       └─→ world-builder (support)
            └─→ Faction design, culture, history

## Agent Specialization Matrix

| Role | Primary Focus | Secondary Tasks |
|------|---------------|-----------------|
| phase-lead | Summarise → decide → split → dispatch | Task splitting |
| rules-engineer | Core rule changes + tests | Scoring formula validation |
| unity-gameplay-engineer | UI screens + GameManager state | Save/load plumbing |
| content-designer | Balance tuning + icon consistency | Recipes/catalyst tuning |
| ui-ux-designer | Mockups + screen layouts | 640x360 pixel specs |

## Coordination Rules

1. **Vertical Delegation**: Leadership → department leads → specialists. Never skip tier.
2. **Horizontal Consultation**: Same-tier agents may consult, no binding decisions outside domain.
3. **Conflict Resolution**: Escalate to creative-director (design) or technical-director (tech).
4. **Nothing Goes Upstream**: No PRs/issues in engine/vendor repos — draft only.

## Agent Workflow Pattern

Summarise → Decide → Split → Dispatch → Verify → Integrate
```