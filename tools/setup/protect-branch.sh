#!/usr/bin/env bash
# Bật branch protection: bắt buộc PR + status check "ci-gate" xanh (gom lint, repo-guard, core-tests) + nhánh phải cập nhật,
# áp dụng cả cho admin (solo dev là admin nên cần enforce_admins), cấm force-push và xóa nhánh.
#
# Mặc định chỉ IN ra payload và lệnh sẽ chạy (dry-run). Đặt APPLY=1 để thực thi.
#   tools/setup/protect-branch.sh owner/repo [branch]
#   APPLY=1 tools/setup/protect-branch.sh owner/repo main
#
# Cần: gh đã đăng nhập (gh auth login), quyền admin trên repo.
# Lưu ý: repo private trên gói miễn phí không hỗ trợ branch protection.
set -euo pipefail

REPO="${1:?usage: protect-branch.sh owner/repo [branch]}"
BRANCH="${2:-main}"

PAYLOAD='{
  "required_status_checks": { "strict": true, "contexts": ["ci-gate"] },
  "enforce_admins": true,
  "required_pull_request_reviews": { "required_approving_review_count": 0 },
  "restrictions": null,
  "allow_force_pushes": false,
  "allow_deletions": false
}'

echo "Repo: $REPO  Branch: $BRANCH"
echo "$PAYLOAD"

if [[ "${APPLY:-0}" != "1" ]]; then
  echo
  echo "Dry-run. Chạy lại với APPLY=1 để áp dụng."
  exit 0
fi

echo "$PAYLOAD" | gh api -X PUT "repos/$REPO/branches/$BRANCH/protection" --input -
echo "Đã bật branch protection cho $REPO@$BRANCH"
