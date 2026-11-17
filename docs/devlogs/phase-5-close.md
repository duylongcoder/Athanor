# Phase 5 close (2026-11-18)

Phase 5 is closed as it stands. Nothing below is decided by closing it.

## Shipped
- 5a: see `production/qa/phase5a-signoff.md`.
- 5b-01 HD-2D mockup, 5b-04 depth layer art, 5b-05 DepthTest prototype, 5b-06 WebGL measurements, 5b-07 HD-2D in Main
  (sway setting, dev hooks, `tools/web/mainhd2d.mjs`), 5b-08 QA sign-off PASS with accepted gaps.
- Gates: G2 approved 2026-11-16 (11 SPEC questions as recommended), G3 passed 2026-11-17 (owner; numbers not recorded).
- Final figures (5b-08): release 10,917,289 B (ceiling 11,824,672 B, headroom 907,383 B); Core 331/331; EditMode 458 / 452 pass /
  0 fail / 6 skip; `tools/ci/run.sh all` exit 0; BalanceMatrix = baseline.

## Carried over (undecided)
- 5b-02 loops mockup: owner review (keep or revert the shipped loops).
- 5b-03 endings lore (`phase-5-endings-lore.md`): frame Option 1 "Roots of worlds", the three seeds, win at any depth vs depth 5
  only, shared first/last ending lines.
- Delete remote `origin/feature/2.5D_test-phase-4` (merged; owner's command only).
- G3 carry-overs: 7-scene storyboard, wider `LineBox` use, VI text pass; reduced-effects switch (not built).
- Version string and release notes for the Phase 5 build: not done; `project.yaml` stays 0.4.0.
- Known issue: HUD small text garbled at 1x in the DepthTest player only (prototype removed in Phase 6).
