# Agent Roster

The following agents are available. Each has a dedicated definition file in
`.claude/agents/`. Use the agent best suited to the task at hand. When a task
spans multiple domains, the coordinating agent (usually `producer` or the
domain lead) should delegate to specialists.

Which model an agent runs on is set by the `model:` line in its own file;
`model-tiers.md` lists the agents that pin one. Every other agent runs on the
session's model.

## Tier 1 -- Leadership Agents
| Agent | Domain | When to Use |
|-------|--------|-------------|
| `creative-director` | High-level vision | Major creative decisions, pillar conflicts, tone/direction |
| `technical-director` | Technical vision | Architecture decisions, tech stack choices, performance strategy |
| `producer` | Production management | Sprint planning, milestone tracking, risk management, coordination |

## Tier 2 -- Department Lead Agents
| Agent | Domain | When to Use |
|-------|--------|-------------|
| `game-designer` | Game design | Mechanics, systems, progression, economy, balancing |
| `lead-programmer` | Code architecture | System design, code review, API design, refactoring |
| `art-director` | Visual direction | Style guides, art bible, asset standards, UI/UX direction |
| `audio-director` | Audio direction | Music direction, sound palette, audio implementation strategy |
| `narrative-director` | Story and writing | Story arcs, world-building, character design, dialogue strategy |
| `qa-lead` | Quality assurance | Test strategy, bug triage, release readiness, regression planning |
| `release-manager` | Release pipeline | Build management, versioning, changelogs, deployment, rollbacks |
| `localization-lead` | Internationalization | String externalization, translation pipeline, locale testing |

## Tier 3 -- Specialist Agents
| Agent | Domain | When to Use |
|-------|--------|-------------|
| `systems-designer` | Systems design | Specific mechanic implementation, formula design, loops |
| `level-designer` | Level design | Level layouts, pacing, encounter design, flow |
| `economy-designer` | Economy/balance | Resource economies, loot tables, progression curves |
| `gameplay-programmer` | Gameplay code | Feature implementation, gameplay systems code |
| `engine-programmer` | Engine systems | Core engine, rendering, physics, memory management |
| `ai-programmer` | AI systems | Behavior trees, pathfinding, NPC logic, state machines |
| `network-programmer` | Networking | Netcode, replication, lag compensation, matchmaking |
| `tools-programmer` | Dev tools | Editor extensions, pipeline tools, debug utilities |
| `ui-programmer` | UI implementation | UI framework, screens, widgets, data binding |
| `technical-artist` | Tech art | Shaders, VFX, optimization, art pipeline tools |
| `sound-designer` | Sound design | SFX design docs, audio event lists, mixing notes |
| `writer` | Dialogue/lore | Dialogue writing, lore entries, item descriptions |
| `world-builder` | World/lore design | World rules, faction design, history, geography |
| `qa-tester` | Test execution | Writing test cases, bug reports, test checklists |
| `performance-analyst` | Performance | Profiling, optimization recs, memory analysis |
| `devops-engineer` | Build/deploy | CI/CD, build scripts, version control workflow |
| `analytics-engineer` | Telemetry | Event tracking, dashboards, A/B test design |
| `ux-designer` | UX flows | User flows, wireframes, accessibility, input handling |
| `prototyper` | Rapid prototyping | Throwaway prototypes, mechanic testing, feasibility validation |
| `security-engineer` | Security | Anti-cheat, exploit prevention, save encryption, network security |
| `accessibility-specialist` | Accessibility | WCAG compliance, colorblind modes, remapping, text scaling |
| `live-ops-designer` | Live operations | Seasons, events, battle passes, retention, live economy |
| `community-manager` | Community | Patch notes, player feedback, crisis comms, community health |

## Engine-Specific Agents (use the set matching your engine)

### Engine Leads

| Agent | Engine | When to Use |
| ---- | ---- | ---- |
| `unreal-specialist` | Unreal Engine 5 | Blueprint vs C++, GAS overview, UE subsystems, Unreal optimization |
| `unity-specialist` | Unity | MonoBehaviour vs DOTS, Addressables, URP/HDRP, Unity optimization |
| `godot-specialist` | Godot 4 | GDScript patterns, node/scene architecture, signals, Godot optimization |

### Unreal Engine Sub-Specialists

| Agent | Subsystem | When to Use |
| ---- | ---- | ---- |
| `ue-gas-specialist` | Gameplay Ability System | Abilities, gameplay effects, attribute sets, tags, prediction |
| `ue-blueprint-specialist` | Blueprint Architecture | BP/C++ boundary, graph standards, naming, BP optimization |
| `ue-replication-specialist` | Networking/Replication | Property replication, RPCs, prediction, relevancy, bandwidth |
| `ue-umg-specialist` | UMG/CommonUI | Widget hierarchy, data binding, CommonUI input, UI performance |

### Unity Sub-Specialists

| Agent | Subsystem | When to Use |
| ---- | ---- | ---- |
| `unity-dots-specialist` | DOTS/ECS | Entity Component System, Jobs, Burst compiler, hybrid renderer |
| `unity-shader-specialist` | Shaders/VFX | Shader Graph, VFX Graph, URP/HDRP customization, post-processing |
| `unity-addressables-specialist` | Asset Management | Addressable groups, async loading, memory, content delivery |
| `unity-ui-specialist` | UI Toolkit/UGUI | UI Toolkit, UXML/USS, UGUI Canvas, data binding, cross-platform input |

### Godot Sub-Specialists

| Agent | Subsystem | When to Use |
| ---- | ---- | ---- |
| `godot-gdscript-specialist` | GDScript | Static typing, design patterns, signals, coroutines, GDScript performance |
| `godot-csharp-specialist` | C# / .NET | .NET patterns, [Signal] delegates, async, nullable types, type-safe node access |
| `godot-shader-specialist` | Shaders/Rendering | Godot shading language, visual shaders, particles, post-processing |
| `godot-gdextension-specialist` | GDExtension | C++/Rust bindings, native performance, custom nodes, build systems |

## Athanor Project Agents

Twelve project-specific agents added for Athanor (marked `project: athanor` in their frontmatter). They **implement**; the framework
agents above remain the **consultants, reviewers and gate owners**. When both could take a task, the Athanor agent does the work and the
framework agent is consulted only for the question named in the right-hand column.

| Agent | Owns | Consult (framework) | Not for |
| ---- | ---- | ---- | ---- |
| `phase-lead` | phase plans, briefs | `producer` | writing code or content |
| `rules-engineer` | `Scripts/Core`, EditMode tests | `lead-programmer` | anything with `UnityEngine` |
| `unity-gameplay-engineer` | `Scripts/Game`, `Editor` | `unity-specialist`, `unity-ui-specialist`, `unity-shader-specialist` | game rules, data |
| `content-designer` | content assets (`Assets/Content`), balance | `game-designer`, `systems-designer`, `economy-designer` | new mechanics, story text |
| `lore-writer` | English story text, lore canon (`docs/ATHANOR.md` §8) | `narrative-director`, `world-builder` | `vi.json`, event structure |
| `localizer-vi` | `lang/vi.json`, glossary | `localization-lead` | i18n architecture |
| `pixel-artist` | `tools/art`, generated art | `art-director`, `technical-artist` | style or palette changes |
| `ui-ux-designer` | screen layout specs, `Editor/layout.json`, UX specs | `ux-designer`, `art-director` | C# code |
| `audio-designer` | `design/audio`, audio assets | `audio-director`, `sound-designer` | work before sound is in scope |
| `qa-engineer` | new tests, `production/qa`, `tools/web` | `qa-lead`, `qa-tester` | fixing feature code |
| `web-release-engineer` | `WebBuild`, `tools/web`, changelog | `release-manager`, `devops-engineer` | publishing |
| `vault-librarian` | `vault/`, `tools/vault` | none | editing generated notes |

Dispatch flow and hand-off rules: `vault/Workflow/Agent Workflow.md`.
