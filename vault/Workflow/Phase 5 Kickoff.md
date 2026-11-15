---
type: workflow
tags: [workflow, phase5]
status: planned - 5a ready to dispatch, 5b behind owner gates
---
# Phase 5 Kickoff

Scope decided by the owner on 2026-11-15. Decisions: `docs/devlogs/phase-5-prep.md`. Plan and briefs:
`docs/superpowers/plans/2026-11-15-athanor-phase5.md`. Dispatch per [[Agent Workflow]].

## 5a: foundation and content (4 waves)

| Wave | Tasks (agent) |
|---|---|
| 0 | 5a-01 size baseline (web-release-engineer) · 5a-02 depth intro lines + lore audit (lore-writer) · 5a-03 UI mockups (ui-ux-designer) |
| gate | **G1** owner approves the 5a-03 mockups |
| 1 | 5a-04 rarity-weighted item draws, 5a-05 Core Settings (rules-engineer) · 5a-06 package cleanup + size cuts (web-release-engineer) · 5a-07 flipbook export (pixel-artist) · 5a-08 en/vi keys (localizer-vi) |
| 2 | 5a-10 Lang in Game → 5a-11 Settings/haptics/mute → 5a-12 tutorial controller, How to play, Watch intro → 5a-13 pause menu → 5a-14 depth intro, window swap, rarity display (unity-gameplay-engineer, in order) · parallel: 5a-09 window views (pixel-artist), 5a-15 rarity weights + BalanceMatrix (content-designer) |
| 3 | 5a-16 sign-off (qa-engineer) → 5a-17 release size (web-release-engineer) → vault rebuild |

## 5b: visuals and new design (owner gate at each step)

| Step | Tasks |
|---|---|
| 1 | 5b-01 HD-2D mockup, 5b-02 Dust/ember loop mockup (ui-ux-designer, no code) · 5b-03 endings/Worldseed lore frame (lore-writer) |
| gate | **G2** owner approves the HD-2D mockup |
| 2 | 5b-04 layer art (pixel-artist) → 5b-05 DepthTest prototype (unity-gameplay-engineer) → 5b-06 WebGL measure (web-release-engineer) |
| gate | **G3** owner approves the prototype |
| 3 | 5b-07 integrate → 5b-08 sign-off and size |

Size ceiling: +1 MB over the 5a-01 baseline for 5a and 5b together.

## Needs design (no tasks)

- Endless mode after depth 5: the owner redesigns it. Old questions: scoring and difficulty, saved record, replaces or sits beside the ending.
- Currency: no purpose yet.
- Several Ultimates / endings: only the 5b-03 lore frame and questions (equal endings, by the Worldseed made; no true/good/bad).
- Sound: a separate plan outside Phase 5 (FMOD dropped).

## Blocks release

- Nothing from fonts: SVN-Determination Sans was deleted at G1. Coder's Crux (CC BY 3.0) needs its credit.
