# RULES: quy luật bổ sung của dự án Athanor

`CLAUDE.md` là gốc: nó chứa các Hard rules, Collaboration Protocol, Graphify map và nạp `.claude/docs/coordination-rules.md` cùng
`.claude/docs/coding-standards.md` (commit, evidence, test, CI, đọc tài liệu theo mục, không đẩy lên repo vendor...). **RULES.md không chép lại những thứ đó**;
nó chỉ giữ quy luật chưa có ở đó, mỗi dòng ghi **ngày cập nhật** và **nguồn**. Nếu hai file mâu thuẫn, sửa cho khớp `CLAUDE.md` (hoặc đưa quy luật mới
vào `CLAUDE.md` rồi xoá khỏi đây).

## 0. Cách cập nhật RULES.md

- Mỗi lần thêm, sửa hoặc xoá một quy luật phải ghi rõ **ngày** (YYYY-MM-DD) và **nguồn sửa đổi** (owner nói trong chat, commit, ADR, `docs/ATHANOR.md` § 4, file memory...).
- Ghi cả vào bảng "Lịch sử cập nhật" ở cuối file, mới nhất ở trên cùng.
- Quy luật bị bỏ thì xoá khỏi danh sách và ghi việc xoá vào lịch sử; không để quy luật cũ nằm lại.
- Trước khi thêm một dòng, kiểm tra nó chưa có trong `CLAUDE.md` hay `.claude/docs/*`; nếu có rồi thì không thêm.

*(2026-11-26, owner yêu cầu trong chat)*

## 1. MEMORY.md

| Quy luật | Ngày | Nguồn |
|---|---|---|
| `MEMORY.md` (gốc repo) là nhật ký thay đổi; chỉ ghi sự thật có bằng chứng (commit, file, quyết định trong `docs/ATHANOR.md`). Mục "unverified" nghĩa là chưa tìm thấy bằng chứng. | 2026-11-26 | `MEMORY.md` đầu file |
| **Khi MEMORY.md khác code thực tế, tin code thực tế và sửa lại MEMORY.md** ngay trong cùng task. Không sửa code cho khớp ghi chú. | 2026-11-26 | Owner, chat |
| **Sau mỗi PR lớn, dọn MEMORY.md**: xoá Change log cũ, Current state và bảng trạng thái task đã xong, mục Gotchas không còn đúng. Chỉ giữ thứ còn đúng với phiên bản hiện tại (kiểm lại với code) và đưa quyết định lâu dài sang `docs/ATHANOR.md` § 4 trước khi xoá. Lịch sử chi tiết đã có trong git. | 2026-11-26 | Owner, chat |
| Sau mỗi task group: thêm khối có ngày ở đầu Change log (hash hoặc "uncommitted", vùng đã sửa, số test nếu đã chạy), sửa Current state. | 2026-11-26 | `MEMORY.md` § Update procedure |
| Memory của Claude (`~/.claude/projects/.../memory/`) cũng theo luật trên: khi thấy sai so với code thì sửa hoặc xoá. | 2026-11-26 | Owner, chat (suy ra từ luật trên) |

## 2. Unity và scene

| Quy luật | Ngày | Nguồn |
|---|---|---|
| Không sửa `.unity` / `.prefab` bằng text (hook `guard-scene-files.sh` chặn; ghi đè bằng `ATHANOR_ALLOW_SCENE_EDIT=1`). Di chuyển script bằng `git mv` cả `.cs` và `.meta`; field serialized đổi tên cần `[FormerlySerializedAs]` (UL301). | 2026-11-26 | `MEMORY.md` § Gotchas |
| Sau batch save `Main.unity`, chạy `git diff -U0` và hoàn nguyên camera (`y: 0.125`, `height: 0.75`) và vị trí 4 đèn. | 2026-11-24 | memory `unity-batch-scene-save-side-effects` |
| Không chạy `node tools/art/export.mjs` (re-encode PNG, đổi `layout.json`); dùng riêng `node tools/art/export-depth.mjs`. | 2026-11-18 | `production/session-state/active.md` (chưa kiểm lại) |
| Lệnh `unity test`/`unity run` truyền **đường dẫn tuyệt đối** của project (`/Users/bunker/Desktop/New Folder/Athanor`); tên trần `Athanor` có thể mở nhầm checkout và báo pass với 0 test. | 2026-11-26 | `MEMORY.md` § Gotchas |

## 3. Nhịp chạy test

| Quy luật | Ngày | Nguồn |
|---|---|---|
| Chạy full test suite **một lần cho mỗi task group** của plan refactor, không chạy sau từng task. Test ghi baseline của task thì ở lại trong task. | 2026-11-24 | memory `feedback-test-per-task-group` |

## Lịch sử cập nhật

| Ngày | Thay đổi | Nguồn |
|---|---|---|
| 2026-11-26 | Cắt các dòng trùng với `CLAUDE.md` và `.claude/docs/*` (hợp tác/commit, code/dữ liệu chung, CI, bằng chứng, vault, Graphify, phân việc, DOTween). `CLAUDE.md` là gốc, RULES.md chỉ giữ phần bổ sung. Thêm luật chống trùng ở mục 0. Gộp lại thành mục 0-3. | Owner trong chat |
| 2026-11-26 | Tạo file. Thêm mục 0 (ghi ngày + nguồn) và mục 1 (code thực tế thắng MEMORY.md; dọn MEMORY.md sau PR lớn). | Owner trong chat; `CLAUDE.md`, `.claude/docs/*`, `MEMORY.md`, memory của Claude |
