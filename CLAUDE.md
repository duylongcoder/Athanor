# Athanor — Claude Code Game Studios

Athanor is a run-based alchemy puzzle for the browser. This repo runs on the
[Claude Code Game Studios](https://github.com/donchitos/claude-code-game-studios) framework (v1.3.0, MIT; licence in
`.claude/LICENSE-CCGS`), adapted to a Unity project that already exists. Read `README.md` for the game and how to build it.

## Technology Stack

- **Engine**: Unity 6000.6.7f1, Universal 2D (URP), Pixel Perfect Camera, 640x360 canvas
- **Language**: C# — rules in `Athanor.Core` (no Unity dependency), screens in `Athanor.Game`
- **Target**: WebGL (browser), mouse primary, touch partial
- **Tests**: NUnit — `dotnet test tools/CoreTests` (rules, no Unity) and `unity test Athanor --mode EditMode`
- **Config**: `project.yaml` (engine, code root, commands, modes). Per-developer overrides in `project.local.yaml` (gitignored)

## Project layout

| Path | What |
|---|---|
| `Athanor/Assets/Scripts/Core`, `.../Game`, `Athanor/Assets/Editor` | code (project.yaml `engine.code_root`) |
| `Athanor/Assets/Tests`, `tools/CoreTests` | tests |
| `Athanor/Assets/Resources/` | `content.json`, `lang/en.json`, `lang/vi.json`, `layout.json` |
| `tools/` | `art` (sprite generator), `fx`, `web` (headless checks), `CoreTests`, `CoreReports` (balance reports: `dotnet run --project tools/CoreReports -- balance-matrix|events|roles|newcomer|balance|trace`), `vault` (Obsidian generator + query) |
| `vault/` | **Obsidian vault** — read this before grepping code (see below) |
| `docs/devlogs`, `docs/superpowers/{specs,plans}`, `docs/mockup` | decisions, designs, plans, approved mockups |
| `.claude/` | agents, skills, hooks, rules (framework) |
| `design/`, `production/` | framework outputs (GDDs, sprints, QA, session state — `production/session-state/` is gitignored) |

Framework folders named `src/` or `tests/` in its docs mean the code/test roots above for this project.

## The vault is the project's memory

`vault/` holds the code structure (one note per type with members, file:line, Uses / Used by / Tested by), the data design,
every material/recipe/catalyst/aspect/event/goal, tests, agents, phases. Open the repo root in Obsidian.

- **Query before you search:** `python3 tools/vault/q.py type|member|uses|mat|recipe|list|find <arg>`; architecture in
  `vault/Design/Architecture.md`, schemas in `vault/Data/Data Design.md`. Full guide: `vault/Workflow/Vault Guide.md`.
- **Keep it true:** after changing C#, `content.json`, agents or devlogs, run `python3 tools/vault/build_vault.py`.
  Generated notes (`generated: true`) are never hand-edited.

## Agents and how work is split

From Phase 5, work follows **summarise → decide → split → dispatch → verify → integrate** (`vault/Workflow/Agent Workflow.md`).
The main session dispatches; sub-agents cannot ask the owner questions or spawn agents, so they return *Handoffs* and *Open questions*.

| Need | Agent |
|---|---|
| Summarise the phase, split into briefs | `phase-lead` |
| Core rules, scoring, run state, Core tests | `rules-engineer` |
| Screens, views, GameFlow, save plumbing, Editor tools | `unity-gameplay-engineer` |
| content.json, balance | `content-designer` |
| Story, event and cutscene text (English) | `lore-writer` |
| Vietnamese text, key parity, glossary | `localizer-vi` |
| Sprites, icons, effects | `pixel-artist` |
| Screen specs, mockups, layout | `ui-ux-designer` |
| Sound and music (when in scope) | `audio-designer` |
| Sign-off: tests, balance reports, web checks, bugs | `qa-engineer` |
| WebGL build, size, release notes | `web-release-engineer` |
| Vault regeneration, link repair, structure queries | `vault-librarian` |

The other agents in `.claude/agents/` (creative-director, technical-director, producer, lead-programmer, qa-lead, unity-specialist, …) are the
framework's review and gate consultants, used by skills such as `/gate-check`, `/design-review`, `/code-review`, `/team-*`. Details: `.claude/docs/agent-roster.md`.

Rules for dispatching: one owner per file per wave; parallel only for disjoint paths; content and rules first, art/UI next, QA last;
a brief names the agent, objective, inputs (vault notes, file:line), acceptance command and out-of-scope.

## Collaboration Protocol

**User-driven collaboration, not autonomous execution.** Question -> Options -> Decision -> Draft -> Approval.
Agents ask before writing outside their own lane, show drafts for multi-file changes, and never commit without a user instruction.
Design decisions belong to the owner: undecided questions are listed with a recommendation, never resolved silently.
See `docs/COLLABORATIVE-DESIGN-PRINCIPLE.md`.

## Hard rules

- No game rules in the Game layer; no `UnityEngine` in Core. New rule → test first.
- Every player-visible string is a `Lang` key with both `en.json` and `vi.json` entries (or content text localized per the settings devlog).
- A new material, catalyst or aspect needs an icon with the same id in `tools/art/icons.mjs`.
- Content changes require `dotnet test tools/CoreTests` plus the balance report (`tools/CoreReports`); quote the numbers.
- `tools/CoreTests/bin`, `tools/CoreReports/bin`, `obj` and `Athanor/Build` are build output: never hand-edit or stage by accident.
- **CI is the merge gate.** `ci-gate` red means no merge, for everyone. Run `tools/ci/run.sh all` before pushing. Never weaken a check to get green. Rules and the case-to-check map: `docs/CI.md`.
- Devlog per phase in `docs/devlogs/phase-N-*.md`; a decision is written there before it is relied on.

## Coordination, standards, context

@.claude/docs/coordination-rules.md

@.claude/docs/coding-standards.md

`production/session-state/active.md` is the session checkpoint; read it first after a compaction or `/clear`.
Reference docs (read on demand): `.claude/docs/context-management.md`, `.claude/docs/directory-structure.md`, `docs/WORKFLOW-GUIDE.md`.
