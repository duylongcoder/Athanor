#!/usr/bin/env bash
# The one rule set for CI. The workflow and a developer both call this, so "green locally" means "green in CI".
#   tools/ci/run.sh lint      unity_lint (with baseline) + its own unit tests
#   tools/ci/run.sh repo      tools/ci/check_repo.py (build output, content, icons, lang parity, Core purity, .meta)
#   tools/ci/run.sh core      dotnet test tools/CoreTests, fails on any failure or if too few tests ran
#   tools/ci/run.sh commits <range>   commit messages in a range, e.g. origin/main..HEAD
#   tools/ci/run.sh all       lint + repo + core
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

core() {
  local log; log="$(mktemp)"
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
  commits) shift; commits "$@" ;;
  all) lint; repo; core ;;
  *) echo "unknown job: $1" >&2; exit 2 ;;
esac
