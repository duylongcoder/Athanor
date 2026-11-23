// Fails when the rotate-device table in the WebGL template differs from ui.rotate.title / ui.rotate.body in lang/en.json and lang/vi.json
// (Phase 5 W2-07, SPEC change 26). No dependencies, no browser, no Unity (Node 22+).
//   node tools/web/rotate_sync.mjs            exit 0 = in sync, 1 = differs, 2 = could not read
import { readFileSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';

const root = join(dirname(fileURLToPath(import.meta.url)), '..', '..');
export const TEMPLATE = join(root, 'Athanor/Assets/WebGLTemplates/Athanor/index.html');
const LANG = (l) => join(root, `Athanor/Assets/Resources/lang/${l}.json`);

// The table the template carries, parsed from the text between its rotate-table marker comments.
export function templateTable(html) {
  const m = /\/\*rotate-table\*\/([\s\S]*?)\/\*end-rotate-table\*\//.exec(html);
  if (!m) throw new Error('template has no /*rotate-table*/ ... /*end-rotate-table*/ block');
  return JSON.parse(m[1]);
}

/** Problems found, as strings; empty when the template and both lang files agree. */
export function check(html = readFileSync(TEMPLATE, 'utf8'), lang = (l) => JSON.parse(readFileSync(LANG(l), 'utf8'))) {
  const errs = [];
  let table;
  try { table = templateTable(html); } catch (e) { return [String(e.message)]; }
  const langs = Object.keys(table).sort().join(',');
  if (langs !== 'en,vi') errs.push(`template table has languages [${langs}], expected [en,vi]`);
  for (const l of ['en', 'vi']) {
    const entries = lang(l).entries ?? lang(l);
    const find = (k) => (Array.isArray(entries) ? entries.find((e) => e.key === k)?.text : entries[k]);
    for (const part of ['title', 'body']) {
      const key = `ui.rotate.${part}`, want = find(key), got = table[l]?.[part];
      if (want === undefined) errs.push(`${l}.json has no ${key}`);
      else if (got !== want) errs.push(`${l} ${part}: template ${JSON.stringify(got)} != ${l}.json ${JSON.stringify(want)}`);
      if (typeof got === 'string' && got !== got.normalize('NFC')) errs.push(`${l} ${part}: template text is not NFC`);
    }
  }
  return errs;
}

if (process.argv[1] === fileURLToPath(import.meta.url)) {
  let errs;
  try { errs = check(); } catch (e) { console.error('rotate_sync: ' + e.message); process.exit(2); }
  if (errs.length) { for (const e of errs) console.error('rotate_sync FAIL: ' + e); process.exit(1); }
  console.log('rotate_sync OK: template table equals ui.rotate.title/body in en.json and vi.json');
}
