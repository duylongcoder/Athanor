#!/usr/bin/env bash
# The one rule set for CI. The workflow and a developer both call this, so "green locally" means "green in CI".
#   tools/ci/run.sh lint      unity_lint (with baseline) + its own unit tests
#   tools/ci/run.sh repo      tools/ci/check_repo.py (build output, content, icons, lang parity, Core purity, .meta)
#   tools/ci/run.sh unity     EditMode + PlayMode in Unity (local; CI uses game-ci)
#   tools/ci/run.sh rotate    tools/web/rotate_sync.mjs (rotate-device text in the WebGL template = ui.rotate.* in en/vi.json)
#   tools/ci/run.sh commits <range>   commit messages in a range, e.g. origin/main..HEAD
#   tools/ci/run.sh all       lint + repo + rotate
# Any non-zero exit means CI fails and the merge is blocked.
set -euo pipefail
cd "$(dirname "$0")/../.."

LINT=(python3 tools/lint/unity_lint.py --root Athanor --config tools/lint/lint.config.json --baseline tools/lint/baseline.json)

lint() {
  python3 -m unittest discover -s tools/lint/tests -q
  "${LINT[@]}" --all --format "${LINT_FORMAT:-text}"
}

repo() { python3 tools/ci/check_repo.py; }

rotate() { node tools/web/rotate_sync.mjs; }   # exit 1 = template and lang differ, 2 = a file could not be read

# Local only: needs the unity CLI and a closed Editor. CI runs the same suites through game-ci (job unity-tests).
unitytests() {
  unity test Athanor --mode EditMode
  unity test Athanor --mode PlayMode
}

commits() {
  local range="${1:?usage: run.sh commits <range>}"
  "${LINT[@]}" --check-commits "$range"
}

case "${1:-all}" in
  lint) lint ;;
  repo) repo ;;
  unity) unitytests ;;
  rotate) rotate ;;
  commits) shift; commits "$@" ;;
  all) lint; repo; rotate ;;
  *) echo "unknown job: $1" >&2; exit 2 ;;
esac
