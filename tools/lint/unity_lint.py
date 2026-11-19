#!/usr/bin/env python3
"""unity_lint - luật cứng cho dự án Unity/C#. Chỉ dùng thư viện chuẩn của Python.

Chế độ:
  --all                  quét toàn bộ project + kiểm tra cấp project (dùng trong CI)
  --hook                 đọc JSON của Claude Code hook (stdin), lint file vừa sửa
  <paths...>             lint các file chỉ định
  --diff-base REF        luật dựa trên diff (đổi tên field serialized, UL301)
  --check-commits RANGE  kiểm tra commit message, ví dụ origin/main..HEAD
  --check-message MSG    kiểm tra một message (ví dụ tiêu đề PR)
  --make-meta PATH...    tạo .meta cho file/thư mục mới
  --list-rules           in danh sách luật

Mã thoát: 0 = sạch, 1 = có lỗi (CLI), 2 = có lỗi (--hook, để Claude đọc stderr).
Chi tiết luật: docs/lint-rules.md
"""
from __future__ import annotations

import argparse
import bisect
import functools
import json
import os
import re
import subprocess
import sys
import uuid
from collections import Counter, defaultdict
from dataclasses import dataclass
from pathlib import Path

# --------------------------------------------------------------------------
# Danh mục luật: id -> (mô tả, gợi ý sửa)
# --------------------------------------------------------------------------
RULES: dict[str, tuple[str, str]] = {
    "UL000": ("lint-ignore không hợp lệ",
              "Cú pháp: // lint-ignore: ULxxx <lý do ít nhất 10 ký tự>. Rule id phải tồn tại."),
    "UL001": ("Find*/GetComponent* trong Update/FixedUpdate/LateUpdate",
              "Cache kết quả trong Awake hoặc Start rồi dùng field."),
    "UL002": ("Cấp phát bộ nhớ trong hot path",
              "Dùng lại buffer/list (UnityEngine.Pool), tránh LINQ, string concat/format mỗi frame."),
    "UL003": ("catch nuốt exception",
              "Log bằng Debug.LogException/LogError hoặc throw lại."),
    "UL004": ("?. / ?? / ??= trên UnityEngine.Object",
              "Dùng `if (obj != null)` hoặc `obj == null`; toán tử ?. và ?? bỏ qua override == của Unity."),
    "UL005": ("Event đăng ký nhưng không hủy đối xứng",
              "OnEnable <-> OnDisable, Awake/Start <-> OnDestroy; dùng method group thay vì lambda."),
    "UL006": ("async void", "Trả về Task/UniTask (hoặc ignore có lý do cho event handler)."),
    "UL007": ("Secret bị hard-code", "Đưa ra biến môi trường/secret store; thu hồi key đã lộ."),
    "UL008": ("Logic thuần C# dùng UnityEngine/UnityEditor",
              "Tách phần phụ thuộc Unity ra adapter ngoài pure_logic_paths."),
    "UL009": ("Màu hard-code trong lớp trình bày",
              "Lấy màu từ theme/layout dữ liệu (palette), không viết Px.Hex(\"#...\") hay new Color(số) trong Scripts/Game."),
    "UL010": ("Toạ độ/kích thước hard-code trong lớp trình bày",
              "Đưa số vào layout.json hoặc hằng có tên; chỉ cho phép 0 và 1 trong Px.At/Px.Mid/new Rect/new Vector2/3."),
    "UL011": ("static có thể ghi trong logic thuần",
              "Giữ trạng thái trong đối tượng run/state được truyền vào; chỉ dùng static readonly/const."),
    "UL012": ("static có thể ghi trong lớp trình bày",
              "Dùng instance/ScriptableObject; nếu thật sự cần, thêm lớp vào static_allowlist và reset bằng RuntimeInitializeLoadType.SubsystemRegistration."),
    "UL013": ("Thread/Task.Run/.Result/.Wait()/Timer không dùng được trên WebGL",
              "WebGL chạy một luồng: dùng coroutine hoặc async/await trên main thread, không chặn bằng .Result/.Wait()."),
    "UL014": ("#if nền tảng ngoài danh sách cho phép",
              "Chỉ các file trong platform_define_allowlist (bridge) được dùng #if UNITY_WEBGL/STANDALONE/ANDROID/IOS."),
    "UL100": ("Tên test không theo Method_Condition_Result",
              "Ví dụ TakeDamage_WhenHealthReachesZero_RaisesDeathEvent."),
    "UL101": ("Test không có assertion", "Thêm Assert.* (hoặc helper Assert*/Verify*/Expect*)."),
    "UL102": ("Thread.Sleep trong test", "Chờ điều kiện bằng vòng lặp có timeout (yield return null)."),
    "UL103": ("Vô hiệu hóa/retry test", "Sửa test hoặc code; không dùng [Ignore]/[Explicit]/[Retry]."),
    "UL104": ("Test phụ thuộc Random/thời gian thực",
              "Truyền System.Random(seed) hoặc clock giả vào; không dùng DateTime.Now/Time.time."),
    "UL105": ("Test tạo GameObject nhưng không có TearDown",
              "Thêm [TearDown]/[UnityTearDown] để Destroy object đã tạo."),
    "UL200": ("Thiếu .meta hoặc .meta mồ côi",
              "Chạy: python3 tools/lint/unity_lint.py --make-meta <đường dẫn> (hoặc mở Unity Editor). Di chuyển file bằng git mv kèm .meta."),
    "UL201": ("GUID trùng giữa các .meta", "Giữ một GUID, tạo lại .meta còn lại (xóa .meta rồi --make-meta)."),
    "UL202": ("ProjectSettings sai", "Project Settings > Editor: Asset Serialization = Force Text, Version Control = Visible Meta Files."),
    "UL203": ("Thiếu mục trong .gitignore", "Thêm mục còn thiếu vào .gitignore."),
    "UL204": ("File bị cấm đang được git theo dõi", "git rm --cached <file> rồi thêm vào .gitignore."),
    "UL300": ("Commit message sai định dạng", "Dạng: <type>(<scope>): <mô tả>, ví dụ test(combat): add shield cases."),
    "UL301": ("Đổi tên field serialized thiếu FormerlySerializedAs",
              'Thêm [FormerlySerializedAs("tenCu")] để không mất dữ liệu trong scene/prefab.'),
}

# --------------------------------------------------------------------------
# Cấu hình mặc định (ghi đè bằng tools/lint/lint.config.json)
# --------------------------------------------------------------------------
DEFAULT_CONFIG: dict = {
    "lint_globs": ["Assets/**/*.cs", "Packages/**/*.cs"],
    "exclude": [
        "Assets/Plugins/**", "Assets/ThirdParty/**", "Assets/TextMesh Pro/**",
        "Assets/Samples/**", "**/Generated/**", "**/*.g.cs", "**/*.Designer.cs",
    ],
    "secret_globs": ["Assets/**/*.cs", "Assets/**/*.json", "Packages/**/*.json",
                     "**/*.yml", "**/*.yaml"],
    "secret_allowlist": [],
    "hot_methods": ["Update", "FixedUpdate", "LateUpdate"],
    "catch_ok_patterns": [r"\bthrow\b", r"\bDebug\s*\.\s*Log\w*", r"\bDebug\s*\.\s*unityLogger\b"],
    "catch_allow_types": ["OperationCanceledException", "TaskCanceledException"],
    "unity_object_types": [],
    "pure_logic_paths": ["Assets/_Game/Core"],
    "presentation_paths": ["Assets/Scripts/Game"],
    "static_allowlist": [],
    "platform_define_allowlist": [],
    "test_path_regex": r"(^|/)(Tests?|EditMode|PlayMode)(/|$)",
    "test_name_regex": r"^[A-Za-z0-9]+(?:_[A-Za-z0-9]+){2,}$",
    "assertion_regex": r"\b(?:Assert|CollectionAssert|StringAssert|LogAssert|Assume)\b|\b(?:Assert|Verify|Expect|Should)\w*\s*\(",
    "min_ignore_reason_chars": 10,
    "meta_roots": ["Assets"],
    "meta_exclude": [],
    "must_be_ignored": ["Library/x", "Temp/x", "Logs/x", "obj/x", "UserSettings/x", "x.csproj", "x.sln"],
    "forbidden_tracked": ["Library/**", "Temp/**", "Logs/**", "obj/**", "UserSettings/**", "*.csproj", "*.sln"],
    "commit_regex": r"^(?:feat|fix|refactor|test|docs|chore|perf|build|ci|style|revert)\([a-z0-9._/-]+\)!?: \S.{0,80}$",
    "commit_skip_regex": r"^(?:Merge |Revert \")",
    "baseline": "tools/lint/baseline.json",
    "severity": {},
}

UNITY_BUILTIN_TYPES = [
    "GameObject", "Transform", "RectTransform", "Component", "MonoBehaviour", "Behaviour",
    "ScriptableObject", "Rigidbody", "Rigidbody2D", "Collider", "Collider2D", "BoxCollider",
    "BoxCollider2D", "SphereCollider", "CapsuleCollider", "CircleCollider2D", "MeshCollider",
    "MeshRenderer", "SkinnedMeshRenderer", "SpriteRenderer", "Renderer", "Camera", "Animator",
    "AudioSource", "AudioClip", "Material", "Texture", "Texture2D", "Sprite", "Canvas",
    "CanvasGroup", "Image", "RawImage", "Text", "Button", "Slider", "Toggle", "TMP_Text",
    "TextMeshProUGUI", "TextMeshPro", "ParticleSystem", "Light", "NavMeshAgent",
    "CharacterController", "LineRenderer", "TrailRenderer", "Mesh", "Shader", "Object",
]

SKIP_DIRS = {".git", "Library", "Temp", "Logs", "obj", "UserSettings", "node_modules", "Builds"}


# --------------------------------------------------------------------------
# Tiện ích glob
# --------------------------------------------------------------------------
@functools.lru_cache(maxsize=None)
def _glob_rx(pat: str) -> re.Pattern:
    out, i = "", 0
    while i < len(pat):
        c = pat[i]
        if c == "*":
            if pat[i:i + 3] == "**/":
                out += "(?:.*/)?"
                i += 3
                continue
            if pat[i:i + 2] == "**":
                out += ".*"
                i += 2
                continue
            out += "[^/]*"
        elif c == "?":
            out += "[^/]"
        else:
            out += re.escape(c)
        i += 1
    return re.compile("^" + out + "$")


def match_any(path: str, patterns) -> bool:
    base = path.rsplit("/", 1)[-1]
    for pat in patterns:
        rx = _glob_rx(pat)
        if rx.match(path) or ("/" not in pat and rx.match(base)):
            return True
    return False


# --------------------------------------------------------------------------
# Che comment/string để regex không bắt nhầm
# --------------------------------------------------------------------------
def mask_source(src: str) -> str:
    """Thay nội dung comment/string/char bằng khoảng trắng; giữ nguyên độ dài, xuống dòng và dấu nháy."""
    out: list[str] = []
    i, n = 0, len(src)
    blank = lambda seg: re.sub(r"[^\n]", " ", seg)
    while i < n:
        c = src[i]
        nxt = src[i + 1] if i + 1 < n else ""
        if c == "/" and nxt == "/":
            j = src.find("\n", i)
            j = n if j == -1 else j
            out.append(" " * (j - i))
            i = j
        elif c == "/" and nxt == "*":
            j = src.find("*/", i + 2)
            j = n if j == -1 else j + 2
            out.append(blank(src[i:j]))
            i = j
        elif c == '"' or (c in "$@" and re.match(r'[$@]{1,2}"', src[i:i + 3])):
            k = i
            while src[k] != '"':
                k += 1
            prefix = src[i:k]
            verbatim = "@" in prefix
            out.append(prefix + '"')
            j = k + 1
            while j < n:
                ch = src[j]
                if verbatim:
                    if ch == '"':
                        if j + 1 < n and src[j + 1] == '"':
                            out.append("  ")
                            j += 2
                            continue
                        break
                    out.append("\n" if ch == "\n" else " ")
                else:
                    if ch == "\\" and j + 1 < n:
                        out.append("  ")
                        j += 2
                        continue
                    if ch == '"':
                        break
                    if ch == "\n":  # chuỗi không đóng: dừng để không nuốt cả file
                        break
                    out.append(" ")
                j += 1
            if j < n and src[j] == '"':
                out.append('"')
                j += 1
            i = j
        elif c == "'":
            m = re.match(r"'(?:\\.[^']{0,8}|[^'\\\n])'", src[i:i + 12])
            if m:
                out.append("'" + " " * (m.end() - 2) + "'")
                i += m.end()
            else:
                out.append(c)
                i += 1
        else:
            out.append(c)
            i += 1
    return "".join(out)


def match_brace(masked: str, open_idx: int) -> int:
    depth = 0
    for i in range(open_idx, len(masked)):
        ch = masked[i]
        if ch == "{":
            depth += 1
        elif ch == "}":
            depth -= 1
            if depth == 0:
                return i
    return len(masked) - 1


def match_bracket(masked: str, open_idx: int, op: str, cl: str) -> int:
    depth = 0
    for i in range(open_idx, len(masked)):
        if masked[i] == op:
            depth += 1
        elif masked[i] == cl:
            depth -= 1
            if depth == 0:
                return i
    return len(masked) - 1


class Src:
    def __init__(self, raw: str):
        self.raw = raw
        self.masked = mask_source(raw)
        self.lines = raw.split("\n")
        self._nl = [m.start() for m in re.finditer("\n", raw)]

    def pos(self, idx: int) -> tuple[int, int]:
        line = bisect.bisect_left(self._nl, idx)
        start = self._nl[line - 1] + 1 if line else 0
        return line + 1, idx - start + 1


@dataclass
class Violation:
    rule: str
    path: str
    line: int
    col: int
    message: str
    text: str = ""

    def key(self) -> tuple[str, str, str]:
        return (self.rule, self.path, self.text or self.message)


# --------------------------------------------------------------------------
# Project: cấu hình + dữ liệu dùng chung
# --------------------------------------------------------------------------
class Project:
    def __init__(self, root: Path, cfg: dict):
        self.root = root
        self.cfg = cfg

    @functools.cached_property
    def unity_types(self) -> set[str]:
        """Các kiểu kế thừa UnityEngine.Object: built-in + class trong project (đệ quy)."""
        known = set(UNITY_BUILTIN_TYPES) | set(self.cfg["unity_object_types"])
        classes: dict[str, list[str]] = {}
        rx = re.compile(r"\bclass\s+(\w+)\s*(?:<[^>{]*>)?\s*:\s*([^{]+)\{")
        for rel in iter_files(self, self.cfg["lint_globs"]):
            try:
                text = mask_source((self.root / rel).read_text(encoding="utf-8-sig", errors="replace"))
            except OSError:
                continue
            for m in rx.finditer(text):
                bases = [re.sub(r"<.*", "", b).strip().split(".")[-1] for b in m.group(2).split(",")]
                classes[m.group(1)] = bases
        changed = True
        while changed:
            changed = False
            for name, bases in classes.items():
                if name not in known and any(b in known for b in bases):
                    known.add(name)
                    changed = True
        return known


def load_config(path: Path | None) -> dict:
    cfg = json.loads(json.dumps(DEFAULT_CONFIG))
    if path and path.is_file():
        user = json.loads(path.read_text(encoding="utf-8"))
        for k, v in user.items():
            if k.startswith("_"):
                continue
            if k not in cfg:
                print(f"unity-lint: cảnh báo - khóa cấu hình không biết: {k}", file=sys.stderr)
            if isinstance(v, dict) and isinstance(cfg.get(k), dict):
                cfg[k].update(v)
            else:
                cfg[k] = v
    return cfg


def iter_files(project: Project, globs) -> list[str]:
    res: list[str] = []
    root = project.root
    for dirpath, dirnames, filenames in os.walk(root):
        dirnames[:] = sorted(d for d in dirnames if d not in SKIP_DIRS)
        for f in sorted(filenames):
            rel = (Path(dirpath) / f).relative_to(root).as_posix()
            if match_any(rel, globs) and not match_any(rel, project.cfg["exclude"]):
                res.append(rel)
    return res


# --------------------------------------------------------------------------
# Tìm method / test
# --------------------------------------------------------------------------
def find_methods(s: Src, names) -> list[tuple[str, int, int]]:
    rx = re.compile(r"\b(?:void|IEnumerator|Task|UniTask|UniTaskVoid)\s+(" + "|".join(map(re.escape, names))
                    + r")\s*\([^)]*\)\s*(\{|=>)")
    res = []
    for m in rx.finditer(s.masked):
        if m.group(2) == "{":
            b0 = m.end() - 1
            res.append((m.group(1), b0 + 1, match_brace(s.masked, b0)))
        else:
            b0 = m.end()
            b1 = s.masked.find(";", b0)
            res.append((m.group(1), b0, len(s.masked) if b1 == -1 else b1))
    return res


TEST_ATTR_RX = re.compile(r"\[\s*(?:NUnit\.Framework\.)?(?:Test|UnityTest|TestCase|TestCaseSource|Theory)\b")


def find_test_methods(s: Src) -> list[tuple[str, int, int, int]]:
    """Trả về (tên, vị trí tên, bắt đầu body, kết thúc body)."""
    seen: set[int] = set()
    res = []
    m_text = s.masked
    for m in TEST_ATTR_RX.finditer(m_text):
        i = m.start()
        while True:  # bỏ qua chuỗi attribute liên tiếp
            while i < len(m_text) and m_text[i].isspace():
                i += 1
            if i < len(m_text) and m_text[i] == "[":
                i = match_bracket(m_text, i, "[", "]") + 1
            else:
                break
        sig = re.compile(r"[^;{}()=\[\]]*?\b(\w+)\s*\(").match(m_text, i)
        if not sig or sig.start(1) in seen:
            continue
        seen.add(sig.start(1))
        p_close = match_bracket(m_text, sig.end() - 1, "(", ")")
        brace = re.compile(r"\s*\{").match(m_text, p_close + 1)
        if not brace:
            continue
        b0 = brace.end() - 1
        res.append((sig.group(1), sig.start(1), b0 + 1, match_brace(m_text, b0)))
    return res


# --------------------------------------------------------------------------
# Các luật cho file C#
# --------------------------------------------------------------------------
UL001_RX = re.compile(
    r"\bGameObject\s*\.\s*Find\w*\s*\("
    r"|\b(?:FindObjectsOfTypeAll|FindObjectsOfType|FindObjectOfType|FindObjectsByType|FindAnyObjectByType|FindFirstObjectByType)\b"
    r"|\b(?:Try)?GetComponent\w*\s*(?:<[^<>()]*>)?\s*\(")

LINQ_RX = re.compile(
    r"\.\s*(?:Select|SelectMany|Where|OrderBy|OrderByDescending|ThenBy|GroupBy|ToList|ToArray|ToDictionary|"
    r"ToHashSet|Any|All|First|FirstOrDefault|Last|LastOrDefault|Count|Aggregate|Distinct|Concat|Skip|Take|"
    r"Zip|Cast|OfType)\s*(?:<[^<>()]*>)?\s*\(")
NEW_COLL_RX = re.compile(
    r"\bnew\s+(?:List|Dictionary|HashSet|Queue|Stack|LinkedList|SortedList|SortedDictionary|SortedSet|StringBuilder)\b")
NEW_ARRAY_RX = re.compile(r"\bnew\s+[\w.]+(?:<[^<>]*>)?\s*\[|\bnew\s*\[\s*\]")
STRING_RX = re.compile(
    r"(?:\$@?|@\$)\"|\b[sS]tring\s*\.\s*(?:Format|Concat|Join)\s*\(|\"\s*\+(?!\+)|\+\s*(?:\$@?|@\$|@)?\"")

HOT_PATTERNS = [
    (UL001_RX, "UL001", "tìm object/component"),
    (LINQ_RX, "UL002", "LINQ (cấp phát)"),
    (NEW_COLL_RX, "UL002", "tạo collection mới"),
    (NEW_ARRAY_RX, "UL002", "tạo array mới"),
    (STRING_RX, "UL002", "nối/định dạng string"),
]


def check_hot(s: Src, cfg, emit):
    for name, a, b in find_methods(s, cfg["hot_methods"]):
        body = s.masked[a:b]
        for rx, rule, what in HOT_PATTERNS:
            for m in rx.finditer(body):
                emit(rule, a + m.start(), f"{what} trong {name}()")


def check_catch(s: Src, cfg, emit):
    rx = re.compile(r"\bcatch\b\s*(\([^)]*\))?\s*(?:when\s*\([^)]*\))?\s*\{")
    for m in rx.finditer(s.masked):
        if m.group(1):
            ctype = m.group(1).strip("() ").split()[0].split(".")[-1] if m.group(1).strip("() ") else ""
            if ctype in cfg["catch_allow_types"]:
                continue
        b0 = m.end() - 1
        body = s.masked[b0 + 1:match_brace(s.masked, b0)]
        if not any(re.search(p, body) for p in cfg["catch_ok_patterns"]):
            emit("UL003", m.start(), "catch không log và không throw")


def check_unity_null(s: Src, project: Project, emit):
    text = s.masked
    if "?" not in text:
        return
    cond = [
        r"\b(?:transform|gameObject)\?[.\[]",
        r"\bCamera\s*\.\s*main\s*\?[.\[]",
        r"\b(?:Try)?GetComponent\w*\s*(?:<[^<>()]*>)?\s*\([^()]*\)\s*(?:\?[.\[]|\?\?)",
    ]
    types = "|".join(sorted(map(re.escape, project.unity_types), key=len, reverse=True))
    names = set(re.findall(r"\b(?:" + types + r")\s*(?:\[\s*\])?\s+(\w+)\s*(?=[=;,)\{])", text))
    if names:
        alt = "|".join(map(re.escape, names))
        cond.append(r"\b(?:" + alt + r")\s*(?:\?[.\[]|\?\?)")
    for pat in cond:
        for m in re.finditer(pat, text):
            emit("UL004", m.start(), "toán tử null-conditional/coalescing trên UnityEngine.Object")


def check_events(s: Src, emit):
    text = s.masked
    bodies: dict[str, list[tuple[int, int]]] = defaultdict(list)
    for name, a, b in find_methods(s, ["OnEnable", "OnDisable", "Awake", "Start", "OnDestroy"]):
        bodies[name].append((a, b))
    local = set(re.findall(r"\b(\w+)\s*\([^;{}()]*\)\s*(?:\{|=>)", text))
    norm = lambda x: re.sub(r"\s+|\bthis\.", "", x)
    pairs = [("OnEnable", "OnDisable"), ("Awake", "OnDestroy"), ("Start", "OnDestroy")]
    for setup, tear in pairs:
        tear_body = "".join(norm(text[a:b]) for a, b in bodies.get(tear, []))
        for a, b in bodies.get(setup, []):
            body = text[a:b]
            for m in re.finditer(r"([\w.]+(?:\[[^\]]*\])?)\s*\+=\s*([^;]+);", body):
                lhs, rhs = norm(m.group(1)), m.group(2).strip()
                if "=>" in rhs or rhs.startswith("delegate"):
                    emit("UL005", a + m.start(), f"lambda gán vào event trong {setup}() không thể hủy trong {tear}()")
                    continue
                rhs_n = norm(rhs)
                if re.fullmatch(r"\w+", rhs_n) and rhs_n in local and f"{lhs}-={rhs_n};" not in tear_body:
                    emit("UL005", a + m.start(), f"{lhs} += {rhs_n} trong {setup}() thiếu `-=` trong {tear}()")
            for m in re.finditer(r"([\w.]+)\s*\.\s*AddListener\s*\(\s*(?:this\s*\.\s*)?(\w+)\s*\)", body):
                lhs, rhs = norm(m.group(1)), m.group(2)
                if rhs in local and f"{lhs}.RemoveListener({rhs});" not in tear_body:
                    emit("UL005", a + m.start(), f"{lhs}.AddListener({rhs}) trong {setup}() thiếu RemoveListener trong {tear}()")


def check_async_void(s: Src, emit):
    for m in re.finditer(r"\basync\s+void\b", s.masked):
        emit("UL006", m.start(), "async void")


def check_pure(s: Src, rel: str, cfg, emit):
    if not any(rel == p.rstrip("/") or rel.startswith(p.rstrip("/") + "/") for p in cfg["pure_logic_paths"]):
        return
    for m in re.finditer(r"^\s*using\s+Unity(?:Engine|Editor)\b|\bUnity(?:Engine|Editor)\s*\.", s.masked, re.M):
        emit("UL008", m.start() + len(m.group(0)) - len(m.group(0).lstrip()), "tham chiếu UnityEngine/UnityEditor trong thư mục logic thuần")


def _under(rel: str, paths) -> bool:
    return any(rel == p.rstrip("/") or rel.startswith(p.rstrip("/") + "/") for p in paths)


NUM_RX = re.compile(r"(?<![\w.])(?:0[xX][0-9a-fA-F]+|\d+\.?\d*(?:[eE][+-]?\d+)?|\.\d+)[fFdDmMuUlL]*")
HEX_CALL_RX = re.compile(r"\bPx\s*\.\s*Hex\s*\(\s*[$@]*\"")
COLOR_RX = re.compile(r"\bnew\s+Color(?:32)?\s*\(")
LAYOUT_CALL_RX = re.compile(r"\bPx\s*\.\s*(?:At|Mid)\s*\(|\bnew\s+(?:Rect|Vector2|Vector3)\s*\(")


def _args(masked: str, open_idx: int) -> str:
    close = match_bracket(masked, open_idx, "(", ")")
    return masked[open_idx + 1:close if close > open_idx else len(masked)]


def _number_ok_in_layout(tok: str) -> bool:
    body = tok.rstrip("fFdDmMuUlL")
    if body.lower().startswith("0x"):
        return False
    try:
        return float(body) in (0.0, 1.0)
    except ValueError:
        return False


def check_literals(s: Src, rel: str, cfg, emit):
    """UL009 (màu hard-code) và UL010 (toạ độ hard-code) trong presentation_paths."""
    if not _under(rel, cfg["presentation_paths"]):
        return
    text = s.masked
    for m in HEX_CALL_RX.finditer(text):
        emit("UL009", m.start(), "Px.Hex với chuỗi màu hard-code")
    for m in COLOR_RX.finditer(text):
        if NUM_RX.search(_args(text, m.end() - 1)):
            emit("UL009", m.start(), "new Color/Color32 với số hard-code")
    for m in LAYOUT_CALL_RX.finditer(text):
        for n in NUM_RX.finditer(_args(text, m.end() - 1)):
            if not _number_ok_in_layout(n.group(0)):
                emit("UL010", m.start(), f"số hard-code {n.group(0)} trong {m.group(0).strip()[:-1].strip()}")
                break


STATIC_DECL_RX = re.compile(r"(?<![\w.])static\s+((?:[\w.<>,\[\]?]+\s*)+?)\s*(=>|[;={(])")
STATIC_SKIP_WORDS = {"class", "struct", "interface", "enum", "delegate", "operator", "implicit", "explicit", "record"}
STATIC_MODIFIERS = {"public", "private", "protected", "internal", "new", "unsafe", "volatile", "extern",
                    "readonly", "const", "event", "partial", "async", "override"}
COLLECTION_TYPE_RX = re.compile(r"^(?:System\s*\.\s*Collections\s*\.\s*Generic\s*\.\s*)?(?:List|Dictionary|HashSet|Queue|Stack)\b")
CLASS_RX = re.compile(r"\b(?:class|struct|record)\s+(\w+)[^{;]*\{")


def find_statics(text: str):
    """Trả về [(idx, kind, owner_class)], kind: mutable | collection."""
    classes = []
    for m in CLASS_RX.finditer(text):
        o = m.end() - 1
        classes.append((m.group(1), o, match_brace(text, o)))
    out = []
    for m in STATIC_DECL_RX.finditer(text):
        if re.search(r"\busing\s+$", text[max(0, m.start() - 12):m.start()]):
            continue
        words = re.findall(r"[\w.<>,\[\]?]+", m.group(1))
        term = m.group(2)
        if term == "(" or term == "=>" or any(w in STATIC_SKIP_WORDS for w in words):
            continue
        body = [w for w in words if w not in STATIC_MODIFIERS]
        if len(body) < 2 and "event" not in words:
            continue
        kind = None
        if "event" in words:
            kind = "mutable"
        elif term == "{":
            close = match_brace(text, m.end() - 1)
            if re.search(r"\bset\b", text[m.end():close]):
                kind = "mutable"
        elif "const" in words:
            kind = None
        elif "readonly" not in words:
            kind = "mutable"
        elif COLLECTION_TYPE_RX.match(" ".join(body[:-1]).replace(" ", "")):
            kind = "collection"
        if kind:
            owner = None
            for name, o, c in classes:
                if o < m.start() < c and (owner is None or o > owner[1]):
                    owner = (name, o)
            out.append((m.start(), kind, owner[0] if owner else None))
    return out


def check_statics(s: Src, rel: str, cfg, emit):
    """UL011 (pure_logic_paths) và UL012 (presentation_paths)."""
    text = s.masked
    if _under(rel, cfg["pure_logic_paths"]):
        for idx, kind, _ in find_statics(text):
            if kind == "mutable":
                emit("UL011", idx, "static có thể ghi trong Scripts/Core")
    if _under(rel, cfg["presentation_paths"]):
        allow = set(cfg["static_allowlist"])
        for idx, kind, owner in find_statics(text):
            if owner not in allow:
                emit("UL012", idx, "static có thể ghi (hoặc collection static) ngoài static_allowlist")
        if "SubsystemRegistration" not in text:
            for m in CLASS_RX.finditer(text):
                if m.group(1) in allow:
                    emit("UL012", m.start(), f"lớp {m.group(1)} trong static_allowlist thiếu reset SubsystemRegistration")


THREAD_PATTERNS = [
    (re.compile(r"\bTask\s*\.\s*Run\b"), "Task.Run"),
    (re.compile(r"\bnew\s+(?:System\s*\.\s*Threading\s*\.\s*)?Thread\s*\("), "new Thread"),
    (re.compile(r"\)\s*\.\s*Result\b"), ".Result trên Task"),
    (re.compile(r"\.\s*Wait\s*\(\s*\)"), ".Wait()"),
    (re.compile(r"\bSystem\s*\.\s*Threading\s*\.\s*Timer\b"), "System.Threading.Timer"),
]
PLATFORM_DEFINE_RX = re.compile(r"^[ \t]*#[ \t]*(?:if|elif)\b[^\n]*", re.M)
PLATFORM_SYMBOL_RX = re.compile(r"\bUNITY_(?:WEBGL|STANDALONE\w*|ANDROID|IOS)\b")


def check_threads_and_platform(s: Src, rel: str, cfg, emit):
    """UL013 và UL014 trong Assets/Scripts (không gồm Editor/ và test)."""
    if not _under(rel, ["Assets/Scripts"]) or re.search(r"(^|/)Editor/", rel) or re.search(cfg["test_path_regex"], rel):
        return
    text = s.masked
    for rx, what in THREAD_PATTERNS:
        for m in rx.finditer(text):
            emit("UL013", m.start(), f"{what}: WebGL chỉ có một luồng")
    if rel not in cfg["platform_define_allowlist"]:
        for m in PLATFORM_DEFINE_RX.finditer(text):
            sym = PLATFORM_SYMBOL_RX.search(m.group(0))
            if sym:
                emit("UL014", m.start() + sym.start(), f"#if {sym.group(0)} ngoài platform_define_allowlist")


UNITY_RANDOM_RX = re.compile(
    r"\b(?:UnityEngine\s*\.\s*)?Random\s*\.\s*(?!InitState\b)(?:Range|value|insideUnit\w*|onUnit\w*|rotation\w*|ColorHSV)\b")
NONDET_PATTERNS = [
    (UNITY_RANDOM_RX, "UnityEngine.Random không seed"),
    (re.compile(r"\bnew\s+(?:System\s*\.\s*)?Random\s*\(\s*\)"), "System.Random không seed"),
    (re.compile(r"\bDateTime(?:Offset)?\s*\.\s*(?:Now|UtcNow|Today)\b"), "DateTime thời gian thực"),
    (re.compile(r"\bTime\s*\.\s*(?:time|realtimeSinceStartup|unscaledTime)\b"), "Time.time thời gian thực"),
]


def check_tests(s: Src, rel: str, cfg, emit):
    text = s.masked
    name_rx = re.compile(cfg["test_name_regex"])
    assert_rx = re.compile(cfg["assertion_regex"])
    for name, name_pos, a, b in find_test_methods(s):
        if not name_rx.match(name):
            emit("UL100", name_pos, f"tên test `{name}` không theo Method_Condition_Result")
        if not assert_rx.search(text[a:b]):
            emit("UL101", name_pos, f"test `{name}` không có assertion")
    for m in re.finditer(r"\bThread\s*\.\s*Sleep\b", text):
        emit("UL102", m.start(), "Thread.Sleep trong test")
    disable_rx = (r"\[\s*(?:[\w.]+(?:\([^\]]*?\))?\s*,\s*)*(?:NUnit\.Framework\.)?(?:Ignore|Explicit|Retry)\b"
                  r"|\bAssert\s*\.\s*Ignore\b")
    for m in re.finditer(disable_rx, text):
        emit("UL103", m.start(), "vô hiệu hóa hoặc retry test")
    for rx, what in NONDET_PATTERNS:
        for m in rx.finditer(text):
            emit("UL104", m.start(), f"test không xác định: {what}")
    create = re.search(r"\bnew\s+GameObject\s*\(|\bInstantiate\s*[(<]|\bGameObject\s*\.\s*CreatePrimitive\s*\(", text)
    if create and not re.search(r"\[\s*(?:UnityTearDown|TearDown|OneTimeTearDown)\s*\]", text):
        emit("UL105", create.start(), "tạo GameObject trong test nhưng file không có TearDown")


SECRET_PATTERNS = [
    (re.compile(r'(?i)\b(?:api[_-]?key|secret(?:[_-]?key)?|passw(?:or)?d|private[_-]?key|access[_-]?token|'
                r'auth[_-]?token|client[_-]?secret)\w*"?\s*[:=]\s*"[^"\s]{8,}"'), "chuỗi nhạy cảm gán cứng"),
    (re.compile(r"\bAKIA[0-9A-Z]{16}\b"), "AWS access key"),
    (re.compile(r"\bgh[pousr]_[A-Za-z0-9]{36,}\b"), "GitHub token"),
    (re.compile(r"\bsk-[A-Za-z0-9_-]{20,}"), "API key dạng sk-"),
    (re.compile(r"\bAIza[0-9A-Za-z_-]{35}\b"), "Google API key"),
    (re.compile(r"\bxox[baprs]-[0-9A-Za-z-]{10,}"), "Slack token"),
    (re.compile(r"-----BEGIN (?:RSA |EC |OPENSSH |DSA )?PRIVATE KEY-----"), "private key"),
]


def check_secrets(s: Src, cfg, emit):
    allow = [re.compile(p) for p in cfg["secret_allowlist"]]
    for rx, what in SECRET_PATTERNS:
        for m in rx.finditer(s.raw):
            line, _ = s.pos(m.start())
            if any(a.search(s.lines[line - 1]) for a in allow):
                continue
            emit("UL007", m.start(), f"{what} (đã che giá trị)", raw=True)


# --------------------------------------------------------------------------
# lint-ignore
# --------------------------------------------------------------------------
IGNORE_RX = re.compile(r"//\s*lint-ignore(-file)?\s*:\s*(UL\d{3}(?:\s*,\s*UL\d{3})*)\b(.*)$")


def collect_ignores(s: Src, cfg):
    line_ign: dict[int, set[str]] = defaultdict(set)
    file_ign: set[str] = set()
    bad: list[tuple[int, str]] = []
    for i, text in enumerate(s.lines, 1):
        if not re.search(r"//\s*lint-ignore", text):
            continue
        m = IGNORE_RX.search(text)
        if not m:
            bad.append((i, "cú pháp lint-ignore sai"))
            continue
        rules = {r.strip() for r in m.group(2).split(",")}
        reason = m.group(3).strip(" -—:\t\r")
        unknown = rules - set(RULES)
        if unknown or "UL000" in rules:
            bad.append((i, f"rule không tồn tại hoặc không được bỏ qua: {', '.join(sorted(unknown | ({'UL000'} & rules)))}"))
            continue
        if len(reason) < cfg["min_ignore_reason_chars"]:
            bad.append((i, f"lint-ignore thiếu lý do (tối thiểu {cfg['min_ignore_reason_chars']} ký tự)"))
            continue
        if m.group(1):
            file_ign |= rules
        else:
            target = i if text[:m.start()].strip() else i + 1
            line_ign[target] |= rules
    return line_ign, file_ign, bad


def lint_source(rel: str, raw: str, project: Project) -> list[Violation]:
    cfg = project.cfg
    s = Src(raw)
    vios: list[Violation] = []

    def emit(rule: str, idx: int, msg: str, raw: bool = False):
        line, col = s.pos(idx)
        text = s.lines[line - 1].strip()
        if rule == "UL007":
            text = re.sub(r'"[^"]{4,}"', '"***"', text)
        vios.append(Violation(rule, rel, line, col, msg, text))

    is_cs = rel.endswith(".cs")
    if is_cs and not match_any(rel, cfg["exclude"]) and match_any(rel, cfg["lint_globs"]):
        check_hot(s, cfg, emit)
        check_catch(s, cfg, emit)
        check_unity_null(s, project, emit)
        check_events(s, emit)
        check_async_void(s, emit)
        check_pure(s, rel, cfg, emit)
        if not (re.search(cfg["test_path_regex"], rel) or re.search(r"(^|/)Editor/", rel)):
            check_literals(s, rel, cfg, emit)
            check_statics(s, rel, cfg, emit)
        check_threads_and_platform(s, rel, cfg, emit)
        if re.search(cfg["test_path_regex"], rel) or "NUnit.Framework" in raw:
            check_tests(s, rel, cfg, emit)
    if match_any(rel, cfg["secret_globs"]) and not match_any(rel, cfg["exclude"]):
        check_secrets(s, cfg, emit)

    line_ign, file_ign, bad = (collect_ignores(s, cfg) if is_cs else ({}, set(), []))
    kept: list[Violation] = []
    seen: set[tuple[str, int]] = set()
    for v in vios:
        if v.rule in file_ign or v.rule in line_ign.get(v.line, ()):
            continue
        if (v.rule, v.line) in seen:
            continue
        seen.add((v.rule, v.line))
        kept.append(v)
    for line, msg in bad:
        kept.append(Violation("UL000", rel, line, 1, msg, s.lines[line - 1].strip()))
    kept.sort(key=lambda v: (v.line, v.col, v.rule))
    return kept


def lint_file(rel: str, project: Project) -> list[Violation]:
    try:
        raw = (project.root / rel).read_text(encoding="utf-8-sig", errors="replace")
    except OSError as e:
        return [Violation("UL000", rel, 1, 1, f"không đọc được file: {e}")]
    return lint_source(rel, raw, project)


# --------------------------------------------------------------------------
# Kiểm tra cấp project
# --------------------------------------------------------------------------
def _git(project: Project, *args: str) -> subprocess.CompletedProcess | None:
    try:
        return subprocess.run(["git", "-C", str(project.root), *args], capture_output=True, text=True)
    except OSError:
        return None


def _is_git_repo(project: Project) -> bool:
    r = _git(project, "rev-parse", "--is-inside-work-tree")
    return bool(r and r.returncode == 0)


def unity_ignores(name: str) -> bool:
    return name.startswith(".") or name.endswith("~") or name.lower() == "cvs" or name.lower().endswith(".tmp")


def check_meta(project: Project) -> list[Violation]:
    res: list[Violation] = []
    guids: dict[str, list[str]] = defaultdict(list)
    for mroot in project.cfg["meta_roots"]:
        base = project.root / mroot
        if not base.is_dir():
            continue
        for dirpath, dirnames, filenames in os.walk(base):
            names = set(dirnames) | set(filenames)
            dirnames[:] = sorted(d for d in dirnames if not unity_ignores(d))
            for name in sorted(dirnames + filenames):
                if unity_ignores(name):
                    continue
                rel = (Path(dirpath) / name).relative_to(project.root).as_posix()
                if match_any(rel, project.cfg["meta_exclude"]):
                    continue
                if name.endswith(".meta"):
                    if name[:-5] not in names:
                        res.append(Violation("UL200", rel, 0, 0, ".meta mồ côi (không có file/thư mục tương ứng)"))
                    m = re.search(r"^guid:\s*([0-9a-f]{32})\s*$",
                                  (Path(dirpath) / name).read_text(encoding="utf-8", errors="replace"), re.M)
                    if m:
                        guids[m.group(1)].append(rel)
                elif name + ".meta" not in names:
                    res.append(Violation("UL200", rel, 0, 0, "thiếu file .meta"))
    for guid, paths in guids.items():
        if len(paths) > 1:
            for p in paths[1:]:
                res.append(Violation("UL201", p, 0, 0, f"GUID {guid} trùng với {paths[0]}"))
    return res


def check_meta_for_file(project: Project, rel: str) -> list[Violation]:
    """Dùng trong hook: file vừa sửa (và thư mục cha mới) phải có .meta."""
    res: list[Violation] = []
    parts = Path(rel).parts
    if not parts or parts[0] not in project.cfg["meta_roots"]:
        return res
    cur = Path(parts[0])
    for part in parts[1:]:
        cur = cur / part
        r = cur.as_posix()
        if unity_ignores(part) or match_any(r, project.cfg["meta_exclude"]):
            break
        if not (project.root / (r + ".meta")).exists() and (project.root / r).exists():
            res.append(Violation("UL200", r, 0, 0, "thiếu file .meta"))
    return res


def check_project_settings(project: Project) -> list[Violation]:
    res: list[Violation] = []
    if not (project.root / "Assets").is_dir():
        return res
    p = project.root / "ProjectSettings" / "EditorSettings.asset"
    rel = "ProjectSettings/EditorSettings.asset"
    if not p.is_file():
        return [Violation("UL202", rel, 0, 0, "thiếu ProjectSettings/EditorSettings.asset")]
    txt = p.read_text(encoding="utf-8", errors="replace")
    if not re.search(r"^\s*m_SerializationMode:\s*2\s*$", txt, re.M):
        res.append(Violation("UL202", rel, 0, 0, "Asset Serialization chưa là Force Text (m_SerializationMode: 2)"))
    if not re.search(r"^\s*m_ExternalVersionControlSupport:\s*Visible Meta Files\s*$", txt, re.M):
        res.append(Violation("UL202", rel, 0, 0, "Version Control chưa là Visible Meta Files"))
    return res


def check_git_hygiene(project: Project) -> list[Violation]:
    res: list[Violation] = []
    if not _is_git_repo(project):
        return res
    for sample in project.cfg["must_be_ignored"]:
        r = _git(project, "check-ignore", "-q", "--no-index", sample)
        if r is not None and r.returncode == 1:
            res.append(Violation("UL203", ".gitignore", 0, 0, f".gitignore không loại trừ `{sample}`"))
    r = _git(project, "ls-files", "-z")
    if r and r.returncode == 0:
        for f in filter(None, r.stdout.split("\0")):
            if match_any(f, project.cfg["forbidden_tracked"]):
                res.append(Violation("UL204", f, 0, 0, "file bị cấm đang được git theo dõi"))
    return res


def check_commit_message(project: Project, subject: str, label: str = "") -> list[Violation]:
    subject = subject.strip().splitlines()[0] if subject.strip() else ""
    if re.search(project.cfg["commit_skip_regex"], subject):
        return []
    if re.match(project.cfg["commit_regex"], subject):
        return []
    return [Violation("UL300", label or "commit", 0, 0, f"tiêu đề không đúng định dạng: «{subject}»", subject)]


def check_commits(project: Project, rng: str) -> list[Violation]:
    r = _git(project, "log", "--no-merges", "--format=%h%x1f%s", rng)
    if r is None or r.returncode != 0:
        return [Violation("UL300", rng, 0, 0, f"không đọc được git log {rng}: {(r.stderr if r else '').strip()}")]
    res: list[Violation] = []
    for line in filter(None, r.stdout.splitlines()):
        sha, _, subj = line.partition("\x1f")
        res += check_commit_message(project, subj, f"commit {sha}")
    return res


SER_FIELD_RXS = [
    re.compile(r"\[\s*(?:SerializeField|SerializeReference)\s*\][^;{}=()]*?\b([\w.<>\[\],?]+)\s+(\w+)\s*(?:=[^;]*)?;"),
    re.compile(r"\bpublic\s+(?!static\b|const\b|readonly\b|event\b|class\b|struct\b|enum\b|abstract\b|override\b|"
               r"virtual\b|void\b|delegate\b|interface\b)([\w.<>\[\],?]+)\s+(\w+)\s*(?:=[^;]*)?;"),
]


def serialized_fields(s: Src) -> dict[str, tuple[str, int]]:
    """name -> (type, vị trí tên)"""
    res: dict[str, tuple[str, int]] = {}
    for rx in SER_FIELD_RXS:
        for m in rx.finditer(s.masked):
            res.setdefault(m.group(2), (m.group(1), m.start(2)))
    return res


def check_diff(project: Project, base: str) -> list[Violation]:
    res: list[Violation] = []
    r = _git(project, "diff", "--name-status", "-M", base, "--", "*.cs")
    if r is None or r.returncode != 0:
        return [Violation("UL301", base, 0, 0, f"git diff {base} thất bại: {(r.stderr if r else '').strip()}")]
    for line in filter(None, r.stdout.splitlines()):
        cols = line.split("\t")
        status = cols[0]
        if status[0] == "M":
            old_path = new_path = cols[1]
        elif status[0] == "R":
            old_path, new_path = cols[1], cols[2]
        else:
            continue
        if not match_any(new_path, project.cfg["lint_globs"]) or match_any(new_path, project.cfg["exclude"]):
            continue
        old = _git(project, "show", f"{base}:{old_path}")
        try:
            new_raw = (project.root / new_path).read_text(encoding="utf-8-sig", errors="replace")
        except OSError:
            continue
        if old is None or old.returncode != 0:
            continue
        old_s, new_s = Src(old.stdout.lstrip("﻿")), Src(new_raw)
        old_f, new_f = serialized_fields(old_s), serialized_fields(new_s)
        removed = {n: t for n, (t, _) in old_f.items() if n not in new_f}
        added = {n: (t, p) for n, (t, p) in new_f.items() if n not in old_f}
        taken: set[str] = set()
        file_vios: list[Violation] = []
        for old_name, otype in removed.items():
            cand = next((n for n, (t, _) in added.items() if t == otype and n not in taken), None)
            if cand is None:
                continue
            if re.search(r"FormerlySerializedAs\s*\(\s*\"" + re.escape(old_name) + r"\"", new_s.masked + new_raw):
                taken.add(cand)
                continue
            taken.add(cand)
            line_no, col = new_s.pos(added[cand][1])
            file_vios.append(Violation(
                "UL301", new_path, line_no, col,
                f'field `{old_name}` -> `{cand}` ({otype}) thiếu [FormerlySerializedAs("{old_name}")]',
                new_s.lines[line_no - 1].strip()))
        line_ign, file_ign, _ = collect_ignores(new_s, project.cfg)
        res += [v for v in file_vios if "UL301" not in file_ign and "UL301" not in line_ign.get(v.line, ())]
    return res


# --------------------------------------------------------------------------
# --make-meta
# --------------------------------------------------------------------------
_META_BODY = {
    ".cs": "MonoImporter:\n  externalObjects: {{}}\n  serializedVersion: 2\n  defaultReferences: []\n  executionOrder: 0\n"
           "  icon: {{instanceID: 0}}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n",
    ".asmdef": "AssemblyDefinitionImporter:\n  externalObjects: {{}}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n",
}
_TEXT_EXT = {".txt", ".json", ".md", ".xml", ".csv", ".yml", ".yaml"}


def make_meta(project: Project, targets: list[str]) -> list[str]:
    created: list[str] = []

    def one(p: Path):
        meta = Path(str(p) + ".meta")
        if meta.exists():
            return
        guid = uuid.uuid4().hex
        head = f"fileFormatVersion: 2\nguid: {guid}\n"
        if p.is_dir():
            body = head + "folderAsset: yes\nDefaultImporter:\n  externalObjects: {}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n"
        elif p.suffix in _META_BODY:
            body = head + _META_BODY[p.suffix].replace("{{", "{").replace("}}", "}")
        elif p.suffix in _TEXT_EXT:
            body = head + "TextScriptImporter:\n  externalObjects: {}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n"
        else:
            body = head + "DefaultImporter:\n  externalObjects: {}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n"
        meta.write_text(body, encoding="utf-8", newline="\n")
        created.append(meta.relative_to(project.root.resolve()).as_posix())

    for t in targets:
        p = (project.root / t).resolve() if not os.path.isabs(t) else Path(t).resolve()
        if not p.exists():
            raise SystemExit(f"unity-lint: không tìm thấy {t}")
        rel = p.relative_to(project.root.resolve())
        if not rel.parts or rel.parts[0] not in project.cfg["meta_roots"]:
            raise SystemExit(f"unity-lint: {t} nằm ngoài {project.cfg['meta_roots']}")
        walk = [p]
        if p.is_dir():
            walk += [q for q in p.rglob("*") if not any(unity_ignores(x) for x in q.relative_to(p).parts) and q.suffix != ".meta"]
        for q in walk:
            one(q)
        cur = p.parent
        while cur != project.root.resolve() and cur != cur.parent and cur.name not in project.cfg["meta_roots"]:
            one(cur)
            cur = cur.parent
    return created


# --------------------------------------------------------------------------
# Baseline
# --------------------------------------------------------------------------
def load_baseline(path: Path) -> Counter:
    c: Counter = Counter()
    if path.is_file():
        for e in json.loads(path.read_text(encoding="utf-8")).get("entries", []):
            c[(e["rule"], e["path"], e["text"])] += e.get("count", 1)
    return c


def write_baseline(path: Path, vios: list[Violation]):
    c = Counter(v.key() for v in vios)
    entries = [{"rule": r, "path": p, "text": t, "count": n} for (r, p, t), n in sorted(c.items())]
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps({"version": 1, "entries": entries}, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def apply_baseline(vios: list[Violation], base: Counter) -> list[Violation]:
    remaining = Counter(base)
    kept = []
    for v in vios:
        if remaining[v.key()] > 0:
            remaining[v.key()] -= 1
        else:
            kept.append(v)
    return kept


# --------------------------------------------------------------------------
# Xuất kết quả
# --------------------------------------------------------------------------
def fmt_text(v: Violation, sev: str, hint: bool) -> str:
    loc = f"{v.path}:{v.line}:{v.col}" if v.line else v.path
    out = f"{loc}: {sev}[{v.rule}] {v.message}"
    if v.text and v.line:
        out += f"\n    | {v.text[:160]}"
    if hint:
        out += f"\n    -> {RULES[v.rule][1]}"
    return out


def fmt_github(v: Violation, sev: str) -> str:
    esc = lambda x: x.replace("%", "%25").replace("\r", "%0D").replace("\n", "%0A")
    kind = "error" if sev == "error" else "warning"
    loc = f"file={v.path}" + (f",line={v.line},col={v.col}" if v.line else "")
    return f"::{kind} {loc},title={v.rule}::{esc(v.message + ' - ' + RULES[v.rule][1])}"


# --------------------------------------------------------------------------
# main
# --------------------------------------------------------------------------
def to_rel(project: Project, p: str) -> str | None:
    path = Path(p)
    if not path.is_absolute():
        path = Path.cwd() / path
    try:
        return path.resolve().relative_to(project.root.resolve()).as_posix()
    except ValueError:
        return None


def main(argv: list[str] | None = None) -> int:
    ap = argparse.ArgumentParser(description="Luật cứng cho dự án Unity/C#")
    ap.add_argument("paths", nargs="*")
    ap.add_argument("--all", action="store_true")
    ap.add_argument("--hook", action="store_true")
    ap.add_argument("--root")
    ap.add_argument("--config")
    ap.add_argument("--baseline")
    ap.add_argument("--write-baseline")
    ap.add_argument("--format", choices=["text", "github"], default="text")
    ap.add_argument("--diff-base")
    ap.add_argument("--check-commits")
    ap.add_argument("--check-message")
    ap.add_argument("--make-meta", nargs="+")
    ap.add_argument("--list-rules", action="store_true")
    args = ap.parse_args(argv)

    if args.list_rules:
        for rid, (title, _) in RULES.items():
            print(f"{rid}  {title}")
        return 0

    default_root = Path(__file__).resolve().parents[2]
    if args.root:
        root = Path(args.root)
    elif args.hook and os.environ.get("CLAUDE_PROJECT_DIR"):
        root = Path(os.environ["CLAUDE_PROJECT_DIR"])
    else:
        root = default_root
    cfg_path = Path(args.config) if args.config else root / "tools" / "lint" / "lint.config.json"
    project = Project(root.resolve(), load_config(cfg_path))

    if args.make_meta:
        for m in make_meta(project, args.make_meta):
            print(f"đã tạo {m}")
        return 0

    vios: list[Violation] = []
    if args.hook:
        try:
            payload = json.load(sys.stdin)
        except (json.JSONDecodeError, ValueError):
            return 0
        fp = (payload.get("tool_input") or {}).get("file_path")
        if not fp:
            return 0
        rel = to_rel(project, fp)
        if rel is None or not (project.root / rel).is_file():
            return 0
        vios += lint_file(rel, project)
        vios += check_meta_for_file(project, rel)
    else:
        if args.all:
            files = sorted(set(iter_files(project, project.cfg["lint_globs"] + project.cfg["secret_globs"])))
            for rel in files:
                vios += lint_file(rel, project)
            vios += check_meta(project)
            vios += check_project_settings(project)
            vios += check_git_hygiene(project)
        for p in args.paths:
            rel = to_rel(project, p)
            if rel is None:
                print(f"unity-lint: bỏ qua file ngoài project: {p}", file=sys.stderr)
                continue
            if not args.all:
                vios += lint_file(rel, project)
        if args.diff_base:
            vios += check_diff(project, args.diff_base)
        if args.check_commits:
            vios += check_commits(project, args.check_commits)
        if args.check_message is not None:
            vios += check_commit_message(project, args.check_message, "tiêu đề PR")
        if not (args.all or args.paths or args.diff_base or args.check_commits or args.check_message is not None):
            ap.print_help()
            return 2

    if args.write_baseline:
        write_baseline(Path(args.write_baseline), vios)
        print(f"đã ghi baseline {len(vios)} mục vào {args.write_baseline}")
        return 0

    base_path = Path(args.baseline) if args.baseline else root / project.cfg["baseline"]
    vios = apply_baseline(vios, load_baseline(base_path))

    sev_map = project.cfg["severity"]
    errors = warnings = 0
    lines: list[str] = []
    for v in vios:
        sev = sev_map.get(v.rule, "error")
        if sev == "off":
            continue
        if sev == "error":
            errors += 1
        else:
            warnings += 1
        lines.append(fmt_github(v, sev) if args.format == "github" else fmt_text(v, sev, hint=args.hook))

    if args.hook:
        if errors:
            print(f"unity-lint: {errors} lỗi trong file vừa sửa. Sửa ngay trước khi làm tiếp.", file=sys.stderr)
            print("\n".join(lines), file=sys.stderr)
            return 2
        return 0

    if lines:
        print("\n".join(lines))
    if args.format == "text":
        print(f"\nunity-lint: {errors} lỗi, {warnings} cảnh báo" if lines else "unity-lint: sạch")
    return 1 if errors else 0


if __name__ == "__main__":
    sys.exit(main())
