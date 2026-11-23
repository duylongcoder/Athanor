#!/usr/bin/env bash
# The one rule set for CI. The workflow and a developer both call this, so "green locally" means "green in CI".
#   tools/ci/run.sh lint      unity_lint (with baseline) + its own unit tests
#   tools/ci/run.sh repo      tools/ci/check_repo.py (build output, content, icons, lang parity, Core purity, .meta)
#   tools/ci/run.sh core      dotnet test tools/CoreTests, fails on any failure or if too few tests ran
#   tools/ci/run.sh rotate    tools/web/rotate_sync.mjs (rotate-device text in the WebGL template = ui.rotate.* in en/vi.json)
#   tools/ci/run.sh commits <range>   commit messages in a range, e.g. origin/main..HEAD
#   tools/ci/run.sh all       lint + repo + rotate + core
# Any non-zero exit means CI fails and the merge is blocked.
set -euo pipefail
cd "$(dirname "$0")/../.."

MIN_TESTS="${MIN_TESTS:-250}"   # guards against a filter or a compile quirk that silently drops tests
LINT=(python3 tools/lint/unity_lint.py --root Athanor --config tools/lint/lint.config.json --baseline tools/lint/baseline.json)

lint() {
  python3 -m unittest discover -s tools/lint/tests -q
  "${LINT[@]}" --all --format "${LINT_FORMAT:-text}"
}

repo() { python3 tools/ci/check_repo.py; }

rotate() { node tools/web/rotate_sync.mjs; }   # exit 1 = template and lang differ, 2 = a file could not be read

core() {
  local log; log="$(mktemp)"
  # CoreTests targets net8.0; on a machine with only a newer runtime (e.g. .NET 10) let it roll forward instead of aborting.
  # Same tests, same MIN_TESTS guard; only the runtime that hosts them changes. Override by exporting the variable.
  export DOTNET_ROLL_FORWARD="${DOTNET_ROLL_FORWARD:-Major}"
  dotnet test tools/CoreTests --nologo 2>&1 | tee "$log"
  local passed; passed="$(grep -Eo 'Passed: +[0-9]+' "$log" | tail -1 | grep -Eo '[0-9]+' || echo 0)"
  if [[ "$passed" -lt "$MIN_TESTS" ]]; then
    echo "CI: only $passed tests passed, expected at least $MIN_TESTS (tests were dropped?)" >&2
    exit 1
  fi
}

commits() {
  local range="${1:?usage: run.sh commits <range>}"
  "${LINT[@]}" --check-commits "$range"
}

case "${1:-all}" in
  lint) lint ;;
  repo) repo ;;
  core) core ;;
  rotate) rotate ;;
  commits) shift; commits "$@" ;;
  all) lint; repo; rotate; core ;;
  *) echo "unknown job: $1" >&2; exit 2 ;;
esac
