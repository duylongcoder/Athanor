---
name: log-change
description: "Record a finished task group in MEMORY.md, regenerate the vault and remind the graphify rebuild. Use at the end of every task group."
argument-hint: "<one-line summary of the change>"
user-invocable: true
disable-model-invocation: true
allowed-tools: Read, Edit, Bash
---

# Log a change

CLAUDE.md requires, after every task group: vault regenerated, graphify rebuilt. `MEMORY.md` (repo root) is the change log.

## Steps

1. Read the head of `MEMORY.md` (header, "Current state", first Change log entry) so the new entry matches its format.
2. Add a dated block at the **top** of the Change log: today's date, the summary from the argument, the commit hash (`git log -1 --format=%h`) or the word "uncommitted" with the touched areas (`git status --short`, grouped by folder). Facts only, from git and files you read.
3. Update "Current state" if a plan task changed status. Put a new decision in `docs/ATHANOR.md` § 4 first and point to it from `MEMORY.md`; never duplicate decision text.
4. Run `python3 tools/vault/build_vault.py`; report its `Content Health` result. Generated vault notes are never hand-edited.
5. Tell the owner to run `/graphify Athanor/Assets --update` (full `/graphify Athanor/Assets` after renames or moves, `--force` after deletions) and to record the node and edge counts. Do not run it yourself unless asked.
6. Do not commit.
