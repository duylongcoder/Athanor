import json
import os
import subprocess
import sys
import tempfile
import textwrap
import unittest
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import unity_lint as ul  # noqa: E402

SCRIPT = Path(ul.__file__)


def rules_of(vios):
    return sorted({v.rule for v in vios})


class Base(unittest.TestCase):
    def setUp(self):
        self._tmp = tempfile.TemporaryDirectory()
        self.root = Path(self._tmp.name)
        self.addCleanup(self._tmp.cleanup)

    def write(self, rel, text):
        p = self.root / rel
        p.parent.mkdir(parents=True, exist_ok=True)
        p.write_text(textwrap.dedent(text), encoding="utf-8")
        return p

    def project(self, **over):
        cfg = ul.load_config(None)
        cfg.update(over)
        return ul.Project(self.root, cfg)

    def lint(self, src, rel="Assets/Game/Foo.cs", **over):
        return ul.lint_source(rel, textwrap.dedent(src), self.project(**over))


class MaskTests(unittest.TestCase):
    def test_length_and_lines_preserved(self):
        src = 'a = "x // y"; // c\n/* m\nn */ b = @"q ""r"" //"; c = \'"\';\n'
        m = ul.mask_source(src)
        self.assertEqual(len(m), len(src))
        self.assertEqual(m.count("\n"), src.count("\n"))
        self.assertNotIn("//", m)
        self.assertIn("b =", m)
        self.assertIn("c =", m)


class HotPathTests(Base):
    def test_getcomponent_in_update(self):
        v = self.lint("""
            class A : MonoBehaviour {
                void Update() { var rb = GetComponent<Rigidbody>(); }
            }""")
        self.assertEqual(rules_of(v), ["UL001"])

    def test_find_in_fixedupdate_and_late(self):
        v = self.lint("""
            class A : MonoBehaviour {
                void FixedUpdate() { GameObject.Find("x"); }
                void LateUpdate() { var p = FindObjectOfType<Player>(); }
            }""")
        self.assertEqual([x.rule for x in v], ["UL001", "UL001"])

    def test_getcomponent_in_awake_ok(self):
        v = self.lint("""
            class A : MonoBehaviour {
                Rigidbody rb;
                void Awake() { rb = GetComponent<Rigidbody>(); }
                void Update() { rb.AddForce(Vector3.up); }
            }""")
        self.assertEqual(v, [])

    def test_comment_and_string_not_flagged(self):
        v = self.lint("""
            class A : MonoBehaviour {
                void Update() {
                    // GetComponent<Rigidbody>() is slow
                    Debug.Log("GameObject.Find(x)");
                }
            }""")
        self.assertNotIn("UL001", rules_of(v))

    def test_allocations(self):
        for body in ['var l = new List<int>();', 'var a = items.Where(x => x > 1);', 'var s = $"hp {hp}";',
                     'var s = "a" + hp;', 'var b = new int[4];', 'var s = string.Format("{0}", hp);']:
            with self.subTest(body=body):
                v = self.lint("class A : MonoBehaviour { void Update() { %s } }" % body)
                self.assertEqual(rules_of(v), ["UL002"])

    def test_math_max_not_linq(self):
        v = self.lint("class A : MonoBehaviour { void Update() { x = Mathf.Max(a, b); y = list.Count; } }")
        self.assertEqual(v, [])

    def test_expression_bodied(self):
        v = self.lint("class A : MonoBehaviour { void Update() => GetComponent<Foo>().Bar(); }")
        self.assertEqual(rules_of(v), ["UL001"])


class CatchTests(Base):
    def test_empty_catch(self):
        v = self.lint("class A { void F() { try { X(); } catch (Exception) { } } }")
        self.assertEqual(rules_of(v), ["UL003"])

    def test_bare_catch_with_comment_only(self):
        v = self.lint("class A { void F() { try { X(); } catch { /* ignore */ } } }")
        self.assertEqual(rules_of(v), ["UL003"])

    def test_catch_logs_or_throws(self):
        v = self.lint("""
            class A { void F() {
                try { X(); } catch (Exception e) { Debug.LogException(e); }
                try { X(); } catch (Exception) { throw; }
            } }""")
        self.assertEqual(v, [])

    def test_cancellation_allowed(self):
        v = self.lint("class A { async Task F() { try { await X(); } catch (OperationCanceledException) { } } }")
        self.assertEqual(v, [])


class UnityNullTests(Base):
    def test_declared_unity_field(self):
        v = self.lint("""
            class A : MonoBehaviour {
                Transform target;
                void F() {
                    var p = target?.position;
                    var q = target ?? transform;
                }
            }""")
        self.assertEqual(rules_of(v), ["UL004"])
        self.assertEqual(len(v), 2)

    def test_getcomponent_chain(self):
        v = self.lint("class A : MonoBehaviour { void F() { GetComponent<Foo>()?.Bar(); } }")
        self.assertEqual(rules_of(v), ["UL004"])

    def test_project_monobehaviour_type(self):
        self.write("Assets/Game/Enemy.cs", "public class Enemy : MonoBehaviour { }")
        v = self.lint("class A { Enemy e; void F() { e?.Hit(); } }")
        self.assertEqual(rules_of(v), ["UL004"])

    def test_plain_csharp_class_ok(self):
        self.write("Assets/Game/Plain.cs", "public class Plain { }")
        v = self.lint("class A { Plain p; void F() { p?.Go(); var s = name ?? \"x\"; } }")
        self.assertEqual(v, [])

    def test_ternary_not_flagged(self):
        v = self.lint("class A : MonoBehaviour { Transform t; void F() { var x = ok ? t.position : Vector3.zero; } }")
        self.assertEqual(v, [])


class EventTests(Base):
    def test_missing_unsubscribe(self):
        v = self.lint("""
            class A : MonoBehaviour {
                void OnEnable() { Bus.OnWin += HandleWin; }
                void HandleWin() { }
            }""")
        self.assertEqual(rules_of(v), ["UL005"])

    def test_paired_ok(self):
        v = self.lint("""
            class A : MonoBehaviour {
                void OnEnable() { Bus.OnWin += HandleWin; }
                void OnDisable() { Bus.OnWin -= HandleWin; }
                void Awake() { Bus.OnLose += this.HandleWin; }
                void OnDestroy() { Bus.OnLose -= HandleWin; }
                void HandleWin() { }
            }""")
        self.assertEqual(v, [])

    def test_awake_paired_with_ondisable_is_wrong(self):
        v = self.lint("""
            class A : MonoBehaviour {
                void Awake() { Bus.OnWin += HandleWin; }
                void OnDisable() { Bus.OnWin -= HandleWin; }
                void HandleWin() { }
            }""")
        self.assertEqual(rules_of(v), ["UL005"])

    def test_lambda_subscription(self):
        v = self.lint("class A : MonoBehaviour { void OnEnable() { Bus.OnWin += () => Foo(); } void OnDisable() { } }")
        self.assertEqual(rules_of(v), ["UL005"])

    def test_numeric_plus_equals_ignored(self):
        v = self.lint("class A : MonoBehaviour { int n; int k; void Awake() { n += 1; n += k; } }")
        self.assertEqual(v, [])

    def test_addlistener_pair(self):
        bad = self.lint("""
            class A : MonoBehaviour {
                void OnEnable() { btn.onClick.AddListener(Click); }
                void Click() { }
            }""")
        ok = self.lint("""
            class A : MonoBehaviour {
                void OnEnable() { btn.onClick.AddListener(Click); }
                void OnDisable() { btn.onClick.RemoveListener(Click); }
                void Click() { }
            }""")
        self.assertEqual(rules_of(bad), ["UL005"])
        self.assertEqual(ok, [])


class MiscRuleTests(Base):
    def test_async_void(self):
        self.assertEqual(rules_of(self.lint("class A { async void F() { } }")), ["UL006"])
        self.assertEqual(self.lint("class A { async Task F() { } }"), [])

    def test_secrets(self):
        v = self.lint('class A { string apiKey = "abcd1234efgh5678"; }')
        self.assertEqual(rules_of(v), ["UL007"])
        self.assertNotIn("abcd1234efgh5678", v[0].text)
        self.assertEqual(rules_of(self.lint('class A { string k = "AKIAABCDEFGHIJKLMNOP"; }')), ["UL007"])
        self.assertEqual(self.lint('class A { string label = "Enter your password here"; }'), [])

    def test_secret_in_json_and_allowlist(self):
        j = '{"clientSecret": "supersecretvalue1"}'
        p = self.project()
        self.assertEqual(rules_of(ul.lint_source("Assets/Data/cfg.json", j, p)), ["UL007"])
        p2 = self.project(secret_allowlist=["supersecret"])
        self.assertEqual(ul.lint_source("Assets/Data/cfg.json", j, p2), [])

    def test_pure_logic(self):
        src = "using UnityEngine;\nclass Rules { }"
        self.assertEqual(rules_of(self.lint(src, rel="Assets/_Game/Core/Rules.cs")), ["UL008"])
        self.assertEqual(self.lint(src, rel="Assets/_Game/View/Rules.cs"), [])

    def test_excluded_path_skipped(self):
        v = self.lint("class A : MonoBehaviour { void Update() { GetComponent<X>(); } }",
                      rel="Assets/Plugins/Vendor/A.cs")
        self.assertEqual(v, [])


TEST_OK = """
using NUnit.Framework;
public class HealthTests {
    GameObject go;
    [SetUp] public void SetUp() { go = new GameObject(); }
    [TearDown] public void TearDown() { Object.DestroyImmediate(go); }
    [Test] public void TakeDamage_WhenHealthReachesZero_RaisesDeathEvent() { Assert.IsTrue(true); }
}
"""


class TestRuleTests(Base):
    REL = "Assets/Tests/EditMode/HealthTests.cs"

    def run_rel(self, src):
        return self.lint(src, rel=self.REL)

    def test_good_file_clean(self):
        self.assertEqual(self.run_rel(TEST_OK), [])

    def test_bad_name(self):
        v = self.run_rel("using NUnit.Framework;\nclass T { [Test] public void Works() { Assert.True(true); } }")
        self.assertEqual(rules_of(v), ["UL100"])

    def test_no_assert(self):
        v = self.run_rel("using NUnit.Framework;\nclass T { [Test] public void A_B_C() { var x = 1; } }")
        self.assertEqual(rules_of(v), ["UL101"])

    def test_testcase_attribute_with_array(self):
        v = self.run_rel("using NUnit.Framework;\nclass T { [TestCase(new[] {1, 2})] public void A_B_C(int[] x) { Assert.AreEqual(2, x.Length); } }")
        self.assertEqual(v, [])

    def test_unitytest_with_helper_assert(self):
        v = self.run_rel("using NUnit.Framework;\nclass T { [UnityTest] public IEnumerator A_B_C() { yield return null; AssertAlive(); } }")
        self.assertEqual(v, [])

    def test_sleep_ignore_retry(self):
        v = self.run_rel("""
            using NUnit.Framework;
            class T {
                [Test, Ignore("flaky")] public void A_B_C() { Thread.Sleep(10); Assert.True(true); }
                [Test] [Retry(3)] public void D_E_F() { Assert.True(true); }
            }""")
        self.assertEqual(rules_of(v), ["UL102", "UL103"])

    def test_disabling_attributes_each_form(self):
        forms = ['[Ignore("x")]', '[Test, Ignore("flaky")]', '[TestCase(1), Retry(2)]', '[Test][Explicit]',
                 '[NUnit.Framework.Ignore]', '[Test, Category("a"), Ignore]']
        for attr in forms:
            with self.subTest(attr=attr):
                v = self.run_rel("using NUnit.Framework;\nclass T { %s public void A_B_C() { Assert.True(true); } }" % attr)
                self.assertEqual(rules_of(v), ["UL103"])

    def test_non_disabling_attributes_ok(self):
        v = self.run_rel('using NUnit.Framework;\nclass T { [Test, Category("Ignored")] [TestCase(1)] public void A_B_C(int x) { Assert.True(true); } }')
        self.assertEqual(v, [])

    def test_nondeterminism(self):
        for expr in ["UnityEngine.Random.Range(0, 3)", "Random.value", "new System.Random()", "DateTime.Now", "Time.time"]:
            with self.subTest(expr=expr):
                v = self.run_rel("using NUnit.Framework;\nclass T { [Test] public void A_B_C() { var x = %s; Assert.NotNull(x); } }" % expr)
                self.assertEqual(rules_of(v), ["UL104"])

    def test_seeded_random_ok(self):
        v = self.run_rel("using NUnit.Framework;\nclass T { [Test] public void A_B_C() { var r = new System.Random(42); Random.InitState(7); Assert.NotNull(r); } }")
        self.assertEqual(v, [])

    def test_gameobject_without_teardown(self):
        v = self.run_rel("using NUnit.Framework;\nclass T { [Test] public void A_B_C() { var g = new GameObject(); Assert.NotNull(g); } }")
        self.assertEqual(rules_of(v), ["UL105"])

    def test_rules_not_applied_to_runtime_code(self):
        self.assertEqual(self.lint("class A { void F() { var t = Time.time; Thread.Sleep(1); } }"), [])


class IgnoreTests(Base):
    BAD = "class A : MonoBehaviour {\n%s\n}"

    def test_trailing_ignore_with_reason(self):
        v = self.lint(self.BAD % "void Update() { GetComponent<X>(); } // lint-ignore: UL001 chỉ chạy một lần khi debug")
        self.assertEqual(v, [])

    def test_previous_line_ignore(self):
        v = self.lint(self.BAD % "// lint-ignore: UL001 editor-only tool, không chạy trong build\nvoid Update() { GetComponent<X>(); }")
        self.assertEqual(v, [])

    def test_ignore_without_reason_fails(self):
        v = self.lint(self.BAD % "void Update() { GetComponent<X>(); } // lint-ignore: UL001")
        self.assertEqual(rules_of(v), ["UL000", "UL001"])

    def test_unknown_rule_fails(self):
        v = self.lint(self.BAD % "// lint-ignore: UL999 lý do đủ dài nhưng rule không tồn tại\nvoid F() { }")
        self.assertEqual(rules_of(v), ["UL000"])

    def test_malformed_ignore(self):
        v = self.lint(self.BAD % "// lint-ignore please\nvoid F() { }")
        self.assertEqual(rules_of(v), ["UL000"])

    def test_file_level_ignore(self):
        src = "// lint-ignore-file: UL001 file này là công cụ editor chạy offline\n" + self.BAD % "void Update() { GetComponent<X>(); }"
        self.assertEqual(self.lint(src), [])

    def test_ignore_only_targets_listed_rule(self):
        v = self.lint(self.BAD % "void Update() { GetComponent<X>(); var s = $\"{a}\"; } // lint-ignore: UL001 cần cho trường hợp đặc biệt này")
        self.assertEqual(rules_of(v), ["UL002"])


class ProjectCheckTests(Base):
    def test_meta_missing_and_orphan(self):
        self.write("Assets/A.cs", "class A {}")
        self.write("Assets/B.cs", "class B {}")
        self.write("Assets/B.cs.meta", "guid: " + "b" * 32 + "\n")
        self.write("Assets/Ghost.cs.meta", "guid: " + "c" * 32 + "\n")
        self.write("Assets/.hidden", "x")
        self.write("Assets/tmp~/x.cs", "x")
        v = ul.check_meta(self.project())
        self.assertEqual({(x.rule, x.path) for x in v},
                         {("UL200", "Assets/A.cs"), ("UL200", "Assets/Ghost.cs.meta")})

    def test_folder_needs_meta(self):
        self.write("Assets/Dir/A.cs", "class A {}")
        self.write("Assets/Dir/A.cs.meta", "guid: " + "a" * 32 + "\n")
        v = ul.check_meta(self.project())
        self.assertEqual([(x.rule, x.path) for x in v], [("UL200", "Assets/Dir")])

    def test_duplicate_guid(self):
        for n in ("A", "B"):
            self.write(f"Assets/{n}.cs", "x")
            self.write(f"Assets/{n}.cs.meta", "fileFormatVersion: 2\nguid: " + "d" * 32 + "\n")
        v = ul.check_meta(self.project())
        self.assertEqual(rules_of(v), ["UL201"])

    def test_make_meta_fixes_everything(self):
        self.write("Assets/New/Sub/Thing.cs", "class Thing {}")
        p = self.project()
        created = ul.make_meta(p, ["Assets/New/Sub/Thing.cs"])
        self.assertEqual(len(created), 3)
        self.assertEqual(ul.check_meta(p), [])
        self.assertIn("MonoImporter", (self.root / "Assets/New/Sub/Thing.cs.meta").read_text())
        self.assertEqual(ul.make_meta(p, ["Assets/New/Sub/Thing.cs"]), [])

    def test_meta_for_file_hook_check(self):
        self.write("Assets/New/Thing.cs", "class Thing {}")
        v = ul.check_meta_for_file(self.project(), "Assets/New/Thing.cs")
        self.assertEqual(sorted(x.path for x in v), ["Assets/New", "Assets/New/Thing.cs"])
        ul.make_meta(self.project(), ["Assets/New/Thing.cs"])
        self.assertEqual(ul.check_meta_for_file(self.project(), "Assets/New/Thing.cs"), [])

    def test_project_settings(self):
        (self.root / "Assets").mkdir()
        self.assertEqual(rules_of(ul.check_project_settings(self.project())), ["UL202"])
        self.write("ProjectSettings/EditorSettings.asset",
                   "  m_SerializationMode: 2\n  m_ExternalVersionControlSupport: Visible Meta Files\n")
        self.assertEqual(ul.check_project_settings(self.project()), [])
        self.write("ProjectSettings/EditorSettings.asset",
                   "  m_SerializationMode: 1\n  m_ExternalVersionControlSupport: Hidden Meta Files\n")
        self.assertEqual(len(ul.check_project_settings(self.project())), 2)

    def test_commit_messages(self):
        p = self.project()
        good = ["test(combat): add damage cases for shield", "refactor(inventory): extract stack merge logic",
                "fix(board)!: reject duplicate seeds", "Merge branch 'main' into x"]
        bad = ["update stuff", "feat: no scope", "Fix(board): capital type", "test(combat):no space"]
        for m in good:
            self.assertEqual(ul.check_commit_message(p, m), [], m)
        for m in bad:
            self.assertEqual(rules_of(ul.check_commit_message(p, m)), ["UL300"], m)


def git(root, *args):
    env = dict(os.environ, GIT_AUTHOR_NAME="t", GIT_AUTHOR_EMAIL="t@t", GIT_COMMITTER_NAME="t",
               GIT_COMMITTER_EMAIL="t@t", GIT_CONFIG_GLOBAL=os.devnull, GIT_CONFIG_SYSTEM=os.devnull)
    return subprocess.run(["git", "-C", str(root), *args], capture_output=True, text=True, env=env, check=True)


class GitTests(Base):
    def setUp(self):
        super().setUp()
        git(self.root, "init", "-q", "-b", "main")

    def commit(self, msg):
        git(self.root, "add", "-A")
        git(self.root, "commit", "-q", "-m", msg)

    def test_gitignore_and_forbidden_tracked(self):
        self.write("Assets/A.cs", "x")
        self.write("Library/cache.bin", "x")
        self.write("Game.csproj", "x")
        self.commit("chore(repo): init")
        v = ul.check_git_hygiene(self.project())
        self.assertIn("UL203", rules_of(v))
        tracked = {x.path for x in v if x.rule == "UL204"}
        self.assertEqual(tracked, {"Library/cache.bin", "Game.csproj"})

    def test_clean_repo(self):
        self.write(".gitignore", "/[Ll]ibrary/\n/[Tt]emp/\n/[Ll]ogs/\n/[Oo]bj/\n/UserSettings/\n*.csproj\n*.sln\n")
        self.write("Assets/A.cs", "x")
        self.commit("chore(repo): init")
        self.assertEqual(ul.check_git_hygiene(self.project()), [])

    def test_check_commits_range(self):
        self.write("a.txt", "1")
        self.commit("chore(repo): init")
        self.write("a.txt", "2")
        self.commit("did some things")
        v = ul.check_commits(self.project(), "HEAD~1..HEAD")
        self.assertEqual(rules_of(v), ["UL300"])
        self.assertEqual(ul.check_commits(self.project(), "HEAD~1..HEAD~0") and 1, 1)

    def test_serialized_rename_detected(self):
        self.write("Assets/P.cs", "class P : MonoBehaviour {\n  [SerializeField] private int hp;\n  public float speed;\n}\n")
        self.commit("chore(repo): init")
        self.write("Assets/P.cs", "class P : MonoBehaviour {\n  [SerializeField] private int maxHp;\n  public float speed;\n}\n")
        v = ul.check_diff(self.project(), "HEAD")
        self.assertEqual([(x.rule, x.line) for x in v], [("UL301", 2)])
        self.assertIn("hp", v[0].message)

    def test_serialized_rename_with_attribute_ok(self):
        self.write("Assets/P.cs", "class P : MonoBehaviour {\n  public int hp;\n}\n")
        self.commit("chore(repo): init")
        self.write("Assets/P.cs", 'class P : MonoBehaviour {\n  [FormerlySerializedAs("hp")] public int maxHp;\n}\n')
        self.assertEqual(ul.check_diff(self.project(), "HEAD"), [])

    def test_unrelated_changes_ok(self):
        self.write("Assets/P.cs", "class P : MonoBehaviour {\n  public int hp;\n}\n")
        self.commit("chore(repo): init")
        self.write("Assets/P.cs", "class P : MonoBehaviour {\n  public int hp;\n  public string label;\n}\n")
        self.assertEqual(ul.check_diff(self.project(), "HEAD"), [])

    def test_rename_ignored_with_reason(self):
        self.write("Assets/P.cs", "class P : MonoBehaviour {\n  public int hp;\n}\n")
        self.commit("chore(repo): init")
        self.write("Assets/P.cs", "class P : MonoBehaviour {\n  public int maxHp; // lint-ignore: UL301 field mới hoàn toàn, chưa có dữ liệu\n}\n")
        self.assertEqual(ul.check_diff(self.project(), "HEAD"), [])


class CliTests(Base):
    def run_cli(self, *args, stdin=None):
        return subprocess.run([sys.executable, str(SCRIPT), "--root", str(self.root), *args],
                              input=stdin, capture_output=True, text=True)

    def setup_project(self):
        self.write("ProjectSettings/EditorSettings.asset",
                   "  m_SerializationMode: 2\n  m_ExternalVersionControlSupport: Visible Meta Files\n")
        (self.root / "Assets").mkdir(exist_ok=True)

    def test_all_fails_then_baseline_passes_then_new_violation_fails(self):
        self.setup_project()
        self.write("Assets/A.cs", "class A : MonoBehaviour { void Update() { GetComponent<X>(); } }\n")
        ul.make_meta(self.project(), ["Assets/A.cs"])
        r = self.run_cli("--all")
        self.assertEqual(r.returncode, 1, r.stdout + r.stderr)
        self.assertIn("UL001", r.stdout)
        bl = self.root / "tools/lint/baseline.json"
        self.assertEqual(self.run_cli("--all", "--write-baseline", str(bl)).returncode, 0)
        self.assertEqual(self.run_cli("--all").returncode, 0)
        self.write("Assets/B.cs", "class B { async void F() { } }\n")
        ul.make_meta(self.project(), ["Assets/B.cs"])
        r = self.run_cli("--all")
        self.assertEqual(r.returncode, 1)
        self.assertIn("UL006", r.stdout)
        self.assertNotIn("UL001", r.stdout)

    def test_github_format(self):
        self.setup_project()
        self.write("Assets/A.cs", "class A { async void F() { } }\n")
        ul.make_meta(self.project(), ["Assets/A.cs"])
        r = self.run_cli("--all", "--format", "github")
        self.assertEqual(r.returncode, 1)
        self.assertRegex(r.stdout, r"::error file=Assets/A\.cs,line=1,col=\d+,title=UL006::")

    def test_severity_override(self):
        self.setup_project()
        self.write("Assets/A.cs", "class A { async void F() { } }\n")
        ul.make_meta(self.project(), ["Assets/A.cs"])
        self.write("tools/lint/lint.config.json", json.dumps({"severity": {"UL006": "warning"}}))
        r = self.run_cli("--all")
        self.assertEqual(r.returncode, 0)
        self.assertIn("warning[UL006]", r.stdout)
        self.write("tools/lint/lint.config.json", json.dumps({"severity": {"UL006": "off"}}))
        self.assertNotIn("UL006", self.run_cli("--all").stdout)

    def test_hook_blocks_with_exit_2_and_stderr(self):
        self.setup_project()
        p = self.write("Assets/A.cs", "class A : MonoBehaviour { void Update() { GetComponent<X>(); } }\n")
        ul.make_meta(self.project(), ["Assets/A.cs"])
        payload = json.dumps({"tool_name": "Write", "tool_input": {"file_path": str(p)}})
        r = self.run_cli("--hook", stdin=payload)
        self.assertEqual(r.returncode, 2, r.stdout + r.stderr)
        self.assertIn("UL001", r.stderr)
        self.assertIn("Awake", r.stderr)
        self.assertEqual(r.stdout, "")

    def test_hook_silent_when_clean(self):
        self.setup_project()
        p = self.write("Assets/A.cs", "class A { }\n")
        ul.make_meta(self.project(), ["Assets/A.cs"])
        r = self.run_cli("--hook", stdin=json.dumps({"tool_input": {"file_path": str(p)}}))
        self.assertEqual((r.returncode, r.stdout, r.stderr), (0, "", ""))

    def test_hook_requires_meta_for_new_script(self):
        self.setup_project()
        p = self.write("Assets/New/A.cs", "class A { }\n")
        r = self.run_cli("--hook", stdin=json.dumps({"tool_input": {"file_path": str(p)}}))
        self.assertEqual(r.returncode, 2)
        self.assertIn("UL200", r.stderr)
        self.assertIn("--make-meta", r.stderr)
        self.assertEqual(self.run_cli("--make-meta", "Assets/New/A.cs").returncode, 0)
        r = self.run_cli("--hook", stdin=json.dumps({"tool_input": {"file_path": str(p)}}))
        self.assertEqual(r.returncode, 0, r.stderr)

    def test_hook_ignores_irrelevant_input(self):
        self.setup_project()
        outside = Path(tempfile.gettempdir()) / "unity_lint_outside.cs"
        outside.write_text("class A { async void F() {} }")
        self.addCleanup(outside.unlink)
        for payload in ["", "not json", "{}", json.dumps({"tool_input": {"file_path": str(outside)}}),
                        json.dumps({"tool_input": {"file_path": str(self.root / "Assets/missing.cs")}}),
                        json.dumps({"tool_input": {"file_path": str(self.write("README.md", "x"))}})]:
            with self.subTest(payload=payload[:30]):
                self.assertEqual(self.run_cli("--hook", stdin=payload).returncode, 0)

    def test_check_message_cli(self):
        self.setup_project()
        self.assertEqual(self.run_cli("--check-message", "feat(board): add seeds").returncode, 0)
        r = self.run_cli("--check-message", "Update things")
        self.assertEqual(r.returncode, 1)
        self.assertIn("UL300", r.stdout)

    def test_no_args_prints_help(self):
        self.assertEqual(self.run_cli().returncode, 2)

    def test_shipped_config_is_valid(self):
        cfg = ul.load_config(SCRIPT.parent / "lint.config.json")
        self.assertIn("UL001", ul.RULES)
        self.assertEqual(cfg["pure_logic_paths"], [])


if __name__ == "__main__":
    unittest.main()
