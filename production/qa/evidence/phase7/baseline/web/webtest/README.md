# baseline/web/webtest: failed run, not valid evidence

First `tools/web/webtest.mjs` run against the Phase 7 baseline build (2026-11-19). It stopped at step `vi_5_3` with
`TEST ERROR the cards never took a click: vi_5_3` (the known `offerFrom` flake, docs/ATHANOR.md section 7 item 11(c)); 81 screenshots,
0 console errors. Log: `../webtest.log`.

Not a full run. The valid baseline run is `../webtest3` (log `../webtest3.log`: 129 screenshots, 0 errors). Kept, not deleted;
nothing refers to this folder.
