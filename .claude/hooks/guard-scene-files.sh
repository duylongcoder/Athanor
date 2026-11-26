#!/bin/bash
# Hook: guard-scene-files.sh
# Event: PreToolUse (Write|Edit)
# Purpose: block text edits to Unity .unity / .prefab files. The plan and docs/ATHANOR.md say scenes and
#   prefabs change through Editor scripts (`unity run ... -executeMethod`) or by hand in the Editor, never as text.
# Escape hatch: ATHANOR_ALLOW_SCENE_EDIT=1 (for a reviewed one-line revert, e.g. undoing batch-save camera drift).
set +e

INPUT=$(cat)
case "$INPUT" in
  *'"file_path"'*) ;;
  *) exit 0 ;;
esac

_P=${INPUT#*'"file_path"'}; _P=${_P#*'"'}; _P=${_P%%'"'*}
case "$_P" in
  *.unity|*.prefab) ;;
  *) exit 0 ;;
esac

[ "${ATHANOR_ALLOW_SCENE_EDIT:-}" = "1" ] && exit 0

echo "guard-scene-files: refusing a text edit to $_P" >&2
echo "  Scenes and prefabs change through Editor scripts (unity run <project> -- -executeMethod ...) or in the Editor." >&2
echo "  For a deliberate one-line revert set ATHANOR_ALLOW_SCENE_EDIT=1. See MEMORY.md > Gotchas." >&2
exit 2
