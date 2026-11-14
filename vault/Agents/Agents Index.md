---
generated: true
type: moc
tags: [moc/agents]
---

# Agents
48 sub-agents in `.claude/agents/`. Athanor's own roles are marked ★ (see [[Agent Workflow]]).

| Agent | Model | Description |
|---|---|---|
| [[agent.accessibility-specialist|accessibility-specialist]] | inherit | Accessibility standards and assistive features — remapping, text scaling, colorblind modes, screen reader support. |
| [[agent.ai-programmer|ai-programmer]] | inherit | Implements game AI — behavior trees, state machines, pathfinding, perception, decision-making, NPC and enemy behavior, AI debugging. |
| [[agent.analytics-engineer|analytics-engineer]] | inherit | Telemetry and analytics — event tracking, player behavior, A/B test frameworks, dashboard specification, data pipelines. |
| [[agent.art-director|art-director]] | inherit | Owns visual identity — style guides, art bible, asset standards, color palettes, UI visual direction. |
| ★ [[agent.audio-designer|audio-designer]] | sonnet | Sound and music designer for Athanor (sound is not in the game yet): audio direction, event list for reactions/UI/descent/ending, sourcing or synthesi |
| [[agent.audio-director|audio-director]] | inherit | Owns sonic identity — music direction, sound palette, audio implementation strategy, mix balance. |
| [[agent.community-manager|community-manager]] | sonnet | Player-facing communication — patch notes, social posts, community updates, feedback collection, player bug triage, crisis communication. |
| ★ [[agent.content-designer|content-designer]] | inherit | Owner of Athanor's game data and balance: materials, recipes, catalysts, aspects, events, goals, depth targets in content.json. Use to add or tune con |
| [[agent.creative-director|creative-director]] | opus | Highest creative authority — vision, tone, aesthetic direction. Use when a decision affects game identity or department leads deadlock. |
| [[agent.devops-engineer|devops-engineer]] | sonnet | Build pipelines, CI/CD configuration, version control workflow, branching strategy, automated test pipelines, deployment infrastructure. |
| [[agent.economy-designer|economy-designer]] | inherit | Resource economies — loot tables, sink/faucet analysis, progression curve calibration, economic balance. |
| [[agent.engine-programmer|engine-programmer]] | inherit | Core engine systems — rendering pipeline, physics, memory, resource loading, scene management, performance-critical framework code. |
| [[agent.game-designer|game-designer]] | inherit | Mechanical and systems design — core loops, progression, combat, economy, player-facing rules. Mechanics-level questions. |
| [[agent.gameplay-programmer|gameplay-programmer]] | inherit | Implements designed mechanics as code — player systems, combat, interactive features, gameplay system code. |
| [[agent.lead-programmer|lead-programmer]] | sonnet | Lead programmer — code-level architecture, coding standards, code review, API design, refactoring strategy, assigning work to specialist programmers. |
| [[agent.level-designer|level-designer]] | inherit | Spatial design for levels and areas — encounter layouts, pacing, difficulty, layout planning, environmental storytelling. |
| [[agent.live-ops-designer|live-ops-designer]] | inherit | Post-launch content — seasonal events, battle passes, content cadence, retention, engagement analytics. Keeps the game fresh. |
| [[agent.localization-lead|localization-lead]] | inherit | Internationalization architecture — i18n system design, string extraction workflows, locale testing, locale-specific issues, translation pipeline. |
| ★ [[agent.localizer-vi|localizer-vi]] | sonnet | English-Vietnamese localizer for Athanor: keeps lang/vi.json in parity with lang/en.json, translates new strings, checks fit in the pixel UI. Use when |
| ★ [[agent.lore-writer|lore-writer]] | inherit | Narrative writer for Athanor: world and Keepers lore, the opening/descent/ending cutscene text, event and Lore-event text, material and catalyst flavo |
| [[agent.narrative-director|narrative-director]] | inherit | Story architecture, world-building, character design, dialogue strategy. Narrative structure and direction rather than writing individual lines. |
| [[agent.performance-analyst|performance-analyst]] | inherit | Performance analyst — profiling, bottlenecks, memory analysis, frame time investigation, recommends optimizations, tracks metrics over time. |
| ★ [[agent.phase-lead|phase-lead]] | opus | Athanor phase planner. Use at the start of a phase or when a goal spans several roles: summarises where the project stands from the vault and devlogs, |
| ★ [[agent.pixel-artist|pixel-artist]] | sonnet | Pixel artist and VFX artist for Athanor: generates sprites and icons from tools/art, exports pixelfx effect sheets from tools/fx, keeps palette and 64 |
| [[agent.producer|producer]] | opus | Production concerns — sprint planning, milestone tracking, risk, scope, cross-department coordination when multiple departments must synchronize. |
| [[agent.prototyper|prototyper]] | sonnet | Throwaway builds — concept prototypes after brainstorm to test an idea is fun before GDDs; vertical slices pre-production. Speed over standards. |
| ★ [[agent.qa-engineer|qa-engineer]] | inherit | QA and balance verifier for Athanor: runs the dotnet and Unity tests, balance/role/event reports and headless-Chrome web checks, reproduces bugs, writ |
| [[agent.qa-lead|qa-lead]] | inherit | Test strategy and process — test plan creation, bug severity assessment, regression planning, release quality gates, readiness evaluation. |
| [[agent.qa-tester|qa-tester]] | inherit | Writes test cases, bug reports, and checklists — test case generation, regression checklist creation, execution documentation. |
| [[agent.release-manager|release-manager]] | inherit | Release pipeline — certification checklists, store submissions and page prep, platform requirements, version numbering, release-day coordination. |
| ★ [[agent.rules-engineer|rules-engineer]] | inherit | C# engineer for Athanor's Unity-free rules in Athanor.Core: ignite/chain resolution, scoring, run state, profile/unlocks, journal, satchel, tutorial s |
| [[agent.security-engineer|security-engineer]] | inherit | Protects against cheating, exploits, breaches — anti-cheat measures, secure save data and network comms, player privacy compliance. |
| [[agent.sound-designer|sound-designer]] | inherit | SFX specifications and spec sheets — audio event documentation, mixing parameters, sound category definitions. |
| [[agent.systems-designer|systems-designer]] | inherit | Detailed subsystem mechanics — combat formulas, progression curves, crafting recipes, status effect interactions, mathematical modeling of a mechanic. |
| [[agent.technical-artist|technical-artist]] | inherit | Technical artist bridging art and engineering — shaders, VFX, rendering optimization, art pipeline tools, art-to-engine issues. |
| [[agent.technical-director|technical-director]] | opus | High-level technical decisions — engine architecture, technology choices, performance strategy, technical risk, cross-system conflicts. |
| [[agent.tools-programmer|tools-programmer]] | inherit | Internal dev tools — editor extensions, content authoring tools, debug utilities, pipeline automation, editor workflow. |
| [[agent.ui-programmer|ui-programmer]] | inherit | Implements user interface systems — menus, HUDs, inventory screens, dialogue boxes, UI framework code, screen flow. |
| ★ [[agent.ui-ux-designer|ui-ux-designer]] | inherit | UI/UX designer for Athanor's 640x360 pixel UI: screen layouts, HUD, journal and goals pages, readability, tutorial pacing, mockups. Use before changin |
| ★ [[agent.unity-gameplay-engineer|unity-gameplay-engineer]] | inherit | Unity C# engineer for Athanor's on-screen layer (Athanor.Game): GameFlow state machine, bench/tile/card/HUD views, UI screens, cutscene code, save plu |
| [[agent.unity-shader-specialist|unity-shader-specialist]] | sonnet | Unity rendering — Shader Graph, custom HLSL, VFX Graph, render pipeline customization (URP/HDRP), post-processing within performance budgets. |
| [[agent.unity-specialist|unity-specialist]] | inherit | Authority on Unity-specific patterns and APIs — guides MonoBehaviour vs DOTS/ECS, Unity subsystems including Addressables and Input System. |
| [[agent.unity-ui-specialist|unity-ui-specialist]] | sonnet | Unity UI implementation — UI Toolkit (UXML/USS), UGUI Canvas, data binding, runtime UI performance, cross-platform adaptation. |
| [[agent.ux-designer|ux-designer]] | inherit | User experience flows, interaction design, information architecture — user flow mapping, interaction pattern design, onboarding, accessibility require |
| ★ [[agent.vault-librarian|vault-librarian]] | sonnet | Keeper of Athanor's Obsidian vault: regenerates the code/content notes, maintains hand-written design and data notes, fixes broken links, answers stru |
| ★ [[agent.web-release-engineer|web-release-engineer]] | sonnet | Build and release engineer for Athanor's WebGL build: unity build commands, build size, serving and headless checks, versioning and changelog, publish |
| [[agent.world-builder|world-builder]] | inherit | World builder for lore — factions, cultures, history, geography, ecology, world rules. Faction design, lore consistency checks. |
| [[agent.writer|writer]] | inherit | Writer of player-facing text — dialogue, lore entries, item and ability descriptions, environmental text. |
