# Phase 4: Asset Store packages (research)

Status: **Research only, for owner review.** No code, package or setting was changed for this note. Checked 2026-11-13
against Unity 6000.6.7f1, URP 17.6.0, the 2D Renderer (`Assets/Settings/Renderer2D.asset`), one camera with a
PixelPerfectCamera (assets PPU 1, 640x360, grid snapping = upscale render texture), and the WebGL build in `Build/web`
(9.9 MB today).

Sound was parked in [phase-4-prep.md](phase-4-prep.md) ("out of Phase 4 scope unless the owner says otherwise"). This
note assumes the owner now wants it looked at.

## Summary

| Package | Downloaded | Imported into Athanor | Version | Verdict |
|---|---|---|---|---|
| PrimeTween | yes | **yes** (UPM package from a local .tgz, plus `Assets/Plugins/PrimeTween`) | 1.5.2 (2026-10-28) | Good fit. Migrate the one-shot tweens through a thin wrapper that snaps to pixels. |
| FMOD for Unity 2.03 | yes | **no** | 2.03.15 (from `fmod.cs`, `VERSION.number = 0x00020315`) | Usable on WebGL with care (async banks, audio unlock on the first click). Needs FMOD Studio and a sound project, neither of which exists yet. |
| AERO - Volumetric Fog and Mist | yes | **no** | 1.8.0 (2026-06-13, from its README) | **Not a fit** for our 2D Renderer: it raymarches the depth buffer and lights itself with 3D URP lights. Use a sprite fog layer lit by Light2D instead. |

Where the files are:

| Package | Path |
|---|---|
| PrimeTween, Asset Store download | `~/Library/Unity/Asset Store-5.x/Kyrylo Kuzyk/Editor ExtensionsAnimation/PrimeTween High-Performance Animations and Sequences.unitypackage` (13 MB) |
| PrimeTween, in the project | `Athanor/Packages/manifest.json` has `"com.kyrylokuzyk.primetween": "file:../Packages/com.kyrylokuzyk.primetween.tgz"`; the tarball is `Athanor/Packages/com.kyrylokuzyk.primetween.tgz`; resolved to `Athanor/Library/PackageCache/com.kyrylokuzyk.primetween@48629e19e803`; the Asset Store part is `Athanor/Assets/Plugins/PrimeTween` (installer, a second copy of the same .tgz, and a demo) |
| FMOD, Asset Store download | `~/Library/Unity/Asset Store-5.x/FMOD/Editor ExtensionsAudio/FMOD for Unity 203.unitypackage` (99 MB) |
| AERO, Asset Store download | `~/Library/Unity/Asset Store-5.x/Mirza Beig/ShadersFullscreen Camera Effects/AERO - Volumetric Fog and Mist.unitypackage` (38 MB) |

I searched the whole of `/Users/bunker/Desktop/New Folder` for FMOD and AERO. There is no `Assets/Plugins/FMOD`, no
`FMODStudioSettings`, no `.fspro`, no `.bank`, no `Assets/Mirza`, and nothing in `ProjectSettings` (the only hit is
Unity's own `switchUseLegacyFmodPriorities`). To read their contents I unpacked the two `.unitypackage` files into a
temporary folder outside the project.

## Top risks

1. **AERO does not work with the 2D Renderer in any useful way.** Its shader reads `SampleSceneDepth`, calls
   `GetMainLight()` and the additional-lights loop, and samples URP shadows and Adaptive Probe Volumes. Our sprites write
   no depth, and Light2D are not URP `Light`s. At best it would draw a flat, unlit tint over the whole frame. And because
   our HUD, cards, text and fades are all sprites drawn by the same camera, a fullscreen pass would fog the UI too.
2. **FMOD on WebGL**: banks load asynchronously over HTTP, and the browser keeps audio silent until the first user
   gesture. Title music that starts before the first click is never heard. FMOD also makes the build larger (a static
   `.a` is linked into the wasm, and the banks are downloaded on top) and has a revenue-capped licence that requires a
   credit line.
3. **Licences and the repo.** The PrimeTween licence forbids distributing its `.tgz` or source in derivative products,
   though a private team repo is fine. The FMOD EULA forbids redistributing SDK files other than the runtime
   libraries. Asset Store content (AERO) may not be redistributed either. We are about to commit
   `Packages/com.kyrylokuzyk.primetween.tgz` and `Assets/Plugins/` (they are untracked now). If the GitHub repo
   (`Crucible_Fable`) is public, or will become public, these files must not go into it.

Smaller risks are listed per package below.

---

## 1. PrimeTween 1.5.2

### Installed

- Installed as a UPM package from a local tarball. `packages-lock.json` records it as `file:../Packages/com.kyrylokuzyk.primetween.tgz`.
  The path is relative to `Athanor/Packages/`, so it resolves to `Athanor/Packages/com.kyrylokuzyk.primetween.tgz`.
- `Assets/Plugins/PrimeTween/internal/com.kyrylokuzyk.primetween-1.5.2.tgz` is the same file (both 1,724,328 bytes). The
  installer normally points the manifest at that copy. The owner copied it to `Packages/` instead. One copy is enough.
- Assemblies: `PrimeTween.Runtime` (autoReferenced, allowUnsafeCode, optional references to Burst, UniTask, TMP and
  InputSystem that switch on through version defines), `PrimeTween.Editor` (editor only), `PrimeTween.Installer` (editor
  only), and **`PrimeTween.Demo` and `PrimeTween.Debug`, which build for all platforms** and so end up in the WebGL build.
  The demo also brings about 12 MB of 3D van and sand assets into the repo (they are not in the build unless something
  references them). Recommendation: delete `Assets/Plugins/PrimeTween/Demo` after a look (owner decision).
- Licence (`license.md` in the package): free for use in free and commercial games. Not allowed: "distribute
  PrimeTween's source code and tarball (.tgz) archive inside derivative products", or resell it. It is allowed
  "internally within a team or company, for example through a **private** Git repository". The free version is also
  published on npm (see the README's "Install via UPM" section, https://github.com/KyryloKuzyk/PrimeTween#install-via-unity-package-manager-upm).
  A scoped-registry install keeps the tarball out of git.

### What it does, and the API we would use

From `readme.md` and `Runtime/*.cs` in the package (online: https://github.com/KyryloKuzyk/PrimeTween):

| Need | API |
|---|---|
| Move | `Tween.Position(t, end, dur, ease)`, `Tween.LocalPosition(...)`, plus the `PositionX/Y` and `LocalPositionX/Y` forms |
| Scale | `Tween.Scale(t, end, dur, ease)` (a float or a Vector3) |
| Fade / tint a SpriteRenderer | `Tween.Color(spriteRenderer, end, dur)`, `Tween.Alpha(spriteRenderer, end, dur)` |
| Anything else (our `Label.Alpha`, Light2D, pixel-snapped moves) | `Tween.Custom(target, start, end, dur, (target, v) => ..., ease)`. Supports float, double, Color, Vector2/3/4, Quaternion, Rect. |
| Shake | `Tween.ShakeLocalPosition`, `Tween.ShakeCamera`, `Tween.ShakeCustom(target, start, ShakeSettings, (t, v) => ...)` |
| Delay | `Tween.Delay(seconds)` (allocation-free, usable in Sequences, coroutines and await) |
| Sequence | `Sequence.Create(cycles, cycleMode).Chain(a).Group(b).Insert(atTime, c).ChainDelay(s).ChainCallback(...)` |
| Repeat | `cycles:` (-1 = forever), `CycleMode.Restart / Yoyo / Incremental / Rewind`, `SetRemainingCycles(...)` |
| Ease | `Ease.OutCubic`, `Ease.InCubic`, `Ease.OutBack`, `Ease.InOutSine`, etc.; an `AnimationCurve`; parametric `Easing.Overshoot(s)`, `Easing.Bounce`, `Easing.Elastic` |
| Control | `tween.isAlive`, `.Stop()`, `.Complete()`, `Tween.StopAll(onTarget)`, `Tween.CompleteAll(onTarget)`, `.isPaused`, `.progress`, `.timeScale`, `.OnComplete(target, t => ...)`, `.OnUpdate`, `OnFinally` (new in 1.5.2) |
| Global config | `PrimeTweenConfig.SetTweensCapacity(n)` (call at startup), `defaultEase`, `defaultUpdateType`, the `warn*` flags, `ManualUpdate(...)` |

- **Zero allocation.** The library itself never allocates at runtime once the tween pool is big enough. The editor's
  PrimeTweenManager inspector shows "Max alive tweens"; pass that number to `SetTweensCapacity` at boot. Lambdas
  that capture locals still allocate, as they do today with `Anim.Over`. The non-allocating pattern is the
  `target`-first overload: `Tween.Custom(this, 0, 1, dur, (self, v) => self.field = v)`. `async`/`await` and
  `StartCoroutine` allocate a little, as the README says.
- **Time.** Tweens use scaled time by default (`useUnscaledTime: false`, the same default for shakes), so they behave
  like `Anim.Over`, which uses `Time.deltaTime`. We never change `Time.timeScale` today. `Tween.GlobalTimeScale(...)`
  would animate it if we ever want a hit-stop. `UpdateType.LateUpdate` is available, which matters for anything that
  touches the camera (see ScreenFx below).
- **Coroutines.** `yield return tween.ToYieldInstruction();` works inside our existing `IEnumerator` flow. Do not
  `yield return tween` directly: the struct gets boxed (`warnStructBoxingAllocationInCoroutine`).
- **await.** `await Tween...` / `await sequence` works "on all platforms, even on WebGL" (no threads). We don't need it:
  GameFlow is built on coroutines and should stay that way.
- **Destroyed targets.** A tween stops if its `UnityEngine.Object` target is destroyed (`warnIfTargetDestroyed`).
  That is safer than `Anim.Over` closures, which keep writing to whatever they captured.

### Compatibility with this project

| Item | Status |
|---|---|
| Unity 6000.6 | Fine. 1.5.2 says "Unity 7 support"; the package minimum is 2018.4. |
| WebGL | Fine. No threads. The README says it works on WebGL. |
| URP / 2D | Not involved (it only writes Transform, SpriteRenderer and similar values). |
| **Pixel snapping** | **Not automatic.** `Tween.Position` writes float positions, so a moving sprite can sit at a sub-pixel position. With grid snapping set to upscale render texture, the low-res render rounds it to a pixel anyway, but inconsistently between objects, which shows as crawl or wobble. Every movement tween has to go through a wrapper that writes `Px.Snap(v)`, as `Anim.Over` callers do by hand today (`Mathf.Round(...)`, `Px.Snap(...)`). |
| asmdef | `Athanor.Game.asmdef` has to add `"PrimeTween.Runtime"` to `references`. autoReferenced only applies to Assembly-CSharp, not to our asmdefs. `Athanor.Core` (noEngineReferences) must not reference it. `Athanor.Game.Tests` may need it if tests touch the wrapper. |
| Tests | `tools/CoreTests` only builds Core, so it is unaffected. |

### Current motion code (inventory)

`Scripts/Game/Support/Anim.cs` has `Anim.Over(seconds, step)`, a coroutine that calls `step(t)` every frame with
`Time.deltaTime` and ends on exactly 1, plus `Anim.Wait(seconds, skip)` and the eases `OutCubic`, `InCubic`, `InOut`
(smoothstep), `OutBack` (c = 1.70158, the standard Penner value, same as `Ease.OutBack`) and `Hump` (sin 0..pi..0).

`Anim.Over` calls by file (17 in total):

| File | Calls | What moves |
|---|---|---|
| Boot/GameFlow.cs | 4 | `Drawn`: spark trail from tile to hub (Lerp + InOut, emits sparks per frame); `Points`: gold trail to the counter, Lerp + Hump arc; `ToRack`: new-material icon pops in with OutBack scale 0 to 2, then flies to a rack slot with Lerp + Hump, scale 2 to 1, `Px.Snap` |
| Cutscene/SimpleCutscene.cs | 4 | Descent: banner drops in (OutBack, rounded), workshop scrolls up (InCubic, rounded), depth name fades in and settles (OutCubic, rounded), fade out of the name and rune |
| Ui/EventView.cs | 2 | Popup open: shade fade plus body slide-up with OutBack (rounded); close: shade fade |
| Ui/CatalystChoiceView.cs | 2 | Deal: shade, heading and card alphas (the cards' own motion lives in its Update); pick: fade out with InCubic |
| Fx/ScreenFx.cs | 2 | `FadeOut` / `FadeIn` of the black rect |
| Ui/RunOverView.cs | 1 | Popup open: shade plus body slide-up with OutBack (rounded), same as EventView |
| Bench/TileView.cs | 1 | `Consume`: icon fades and grows by 40 % |
| Bench/BenchView.cs | 1 | `FloatText`: label rises 22 px with OutCubic (`Px.Snap`), fades over the last 30 % |

`Anim.Wait`: 11 calls, all in GameFlow (beats in the Ignite resolution, the ending, the loss).
`StartCoroutine`: 13, all in GameFlow (parallel FloatText, Consume, Drawn, Points, ToRack, Resolve, Main).

Hand-written per-frame motion (Update or LateUpdate, not `Anim.Over`):

| File | Kind | Migrate? |
|---|---|---|
| Fx/ScreenFx.cs | White flash (quadratic decay) and camera shake (sin/cos, rounded, reset to `camHome` every LateUpdate) | Optional. Shake could be `Tween.ShakeCustom` with a snapping setter on LateUpdate; flash could be `Tween.Custom`. The LateUpdate that sets `cam.position = camHome` every frame would fight any camera tween, so it has to go if shake moves. |
| Bench/CardView.cs | Hand cards follow `home + lift` with exponential smoothing plus `MoveTowards` for lift and slide, a selected-card bob, a deal-in delay | **No.** The target changes every frame (hover, drag, fan). A spring follow is the right tool; tweens would be restarted constantly. |
| Ui/CatalystChoiceView.cs | Hover lift with `MoveTowards` | No (same reason). The deal-in could become a Sequence. |
| Bench/HudView.cs | Potency number roll (`MoveTowards` with a speed that depends on distance), bump decay, pulse | Leave. Could be `Tween.Custom<double>`, but the current roll restarts cleanly when the goal changes. |
| Bench/TileView.cs | Hover/ping pulse, idle bob, ring glow, `appear` ramp, flash decay | No: idle loops. `appear` and `flash` could be tweens, but there is little gain. |
| Bench/LinkView.cs, Bench/Dust.cs, Fx/Flame.cs, Fx/Sparks.cs, Fx/Flipbook.cs | Particle and loop animation | **No.** Per-frame loops over many motes or particles; a tween each would cost more and gain nothing. |
| Cutscene/ReactionCutscene.cs | A whole timeline driven by one `T` (gather, burst, rise, pillar), with skip jumping `T` to `Settled` | No for now. It is a hand-written timeline with skip semantics; it could become one Sequence later (skip = `sequence.progress = x` or `Complete()`), but that is a rewrite, not a migration. |
| Cutscene/SimpleCutscene.cs | Typewriter (`Type`), `Hold`, idle rune and embers in Update | Keep `Type` and `Hold` (they read skip). The four `Anim.Over` moves above can migrate. |
| Ui/TitleView.cs, Ui/GoalToast.cs | Pulse, appear ramp, toast age | Toast slide-in and out could be a small Sequence; the pulse stays. |

### Proposed migration

1. Add one wrapper, for example `Scripts/Game/Support/Motion.cs`, so the snap rule lives in one place and call sites
   stay short:
   - `Motion.Move(Transform t, Vector3 to, float s, Ease e)`: `Tween.Custom(t, t.position, to, s, (tr, v) => tr.position = Px.Snap(v), e)`
   - `Motion.MoveLocal(...)`, `Motion.Arc(t, from, to, height, s)` (Lerp plus `Anim.Hump`, snapped, for ToRack and Points)
   - `Motion.Fade(SpriteRenderer r, float a, float s)` (`Tween.Alpha`) and `Motion.Fade(Label l, ...)` (Custom)
   - `Motion.Pop(Transform t, float scale, float s)` (OutBack scale)
   - `Motion.Shake(...)`: snapped `ShakeCustom` on `UpdateType.LateUpdate`
   - All of these return the `Tween` or `Sequence`, and call sites yield on `.ToYieldInstruction()`.
2. Map the eases: `OutCubic` to `Ease.OutCubic`, `InCubic` to `Ease.InCubic`, `OutBack` to `Ease.OutBack` (same constant).
   `InOut` is smoothstep, which is close to `Ease.InOutSine` but not identical. Accept the small change, or keep exact
   curves with `Tween.Custom(0, 1, ...)` plus our own function. `Hump` has no ease; keep it as a function applied
   inside `Arc`.
3. Migrate in this order: the popups (EventView, RunOverView, identical code), FloatText, TileView.Consume,
   ScreenFx fades, the SimpleCutscene Descent moves, ToRack and Points, then CatalystChoice fades. `Drawn` emits sparks
   along its path every frame, so it is particle code and can stay `Anim.Over` or become `Tween.Custom` with an
   `OnUpdate`. Either is fine.
4. Hurry and skip: the flow currently shortens with `pace` and skips with flags. With tweens, pass the shorter
   duration as now, and on skip call `Complete()` on the running sequence (it jumps to its end values).
5. Call `PrimeTweenConfig.SetTweensCapacity(n)` in GameFlow.Awake. Find n by playing a long chain with the manager
   inspector open (a 6-chain fires many FloatTexts and trails at once; 200 is a safe starting guess).
6. Keep `Anim.cs` while any caller uses it. Once the moves are migrated, `Anim.Wait` (with its skip function) can stay
   as the beat helper.

### Before implementing (PrimeTween)

- [ ] Decide which copy of the tarball to keep (`Packages/` or `Assets/Plugins/PrimeTween/internal/`), or switch to the npm scoped registry; delete the other copy.
- [ ] Settle the repo question (public or private) before committing the `.tgz`.
- [ ] Delete `Assets/Plugins/PrimeTween/Demo` (or exclude it), so `PrimeTween.Demo` and `PrimeTween.Debug` stay out of the WebGL build.
- [ ] Add `PrimeTween.Runtime` to `Athanor.Game.asmdef` only; confirm `Athanor.Core` is untouched.
- [ ] Write `Motion` with the snap rule, plus a play-mode test that a moved sprite's position is whole numbers every frame.
- [ ] Measure the WebGL build size before and after (expect a small increase).
- [ ] Re-run `tools/web/webtest.mjs` and the autoplay after the migration (timings of popups and the rack flight change slightly).

### Motion migration (done 2026-11-13)

Owner decisions: PrimeTween for object movement; keep the copy under `Assets/Plugins` and the manifest's
`file:../Packages/com.kyrylokuzyk.primetween.tgz`; delete the demo; eases may change.

- `Assets/Plugins/PrimeTween/Demo` (and `Demo.meta`) deleted. Nothing referenced it (no asmdef, script or build
  scene). `PrimeTween.Demo` and `PrimeTween.Debug` are gone from `Library/ScriptAssemblies`.
- `Athanor.Game.asmdef` references `PrimeTween.Runtime`. `Athanor.Core` does not.
- New `Scripts/Game/Support/Motion.cs`: `Move` / `MoveLocal` (snapped with `Px.Snap` every frame), `Arc` (eased line
  plus an `Anim.Hump` lift over plain time, snapped; with from = to it is a hop on the spot), `Scale` (x and y), `Fade`
  for `SpriteRenderer` and `Label`, and `Wait(tween or sequence, hurry)`, which completes the animation at once when
  `hurry` returns true. Every call takes its start value, so nothing depends on the current state; the default ease
  is Linear (PrimeTween's own default is OutQuad). Scaled time, like `Anim.Over`. Call sites group tweens with
  `Sequence.Create().Group(..)` and wait with `.ToYieldInstruction()`.
- `GameFlow.Awake` calls `PrimeTweenConfig.SetTweensCapacity(200)`.

Moved to Motion (15 of the 17 `Anim.Over` calls):

| Where | Now |
|---|---|
| EventView open / close, RunOverView open | shade `Fade` + body `MoveLocal` (-40 to 0, OutBack) / shade `Fade` |
| BenchView.FloatText | `Move` +6 to +28 (OutCubic), `Fade` inserted at 70 % over the last 30 % |
| TileView.Consume | icon `Fade` 1 to 0 + `Scale` 1 to 1.4 |
| ScreenFx.FadeOut / FadeIn | `Fade` of the black rect |
| SimpleCutscene.Descent (4) | banner fade + drop (OutBack); workshop scroll up (InCubic) with banner fade; depth name fade + settle (OutCubic); fade out, `runeOn` 1 to 0 (`Tween.Custom` on the field), workshop rises (OutCubic) |
| GameFlow.ToRack | pop: `Scale` 0 to 2 (OutBack) + icon and halo `Move` up 8 px; flight: icon and halo `Arc` (height 20, InOutSine), `Scale` 2 to 1, halo `Fade`; the coroutine still emits the sparks each frame while the sequence is alive |
| CatalystChoiceView deal / pick | deal: one sequence, shade and heading fades, each card `Move` from 300 px below (OutBack) inserted 0.1 s apart, padded to the old length with `Tween.Delay`; pick: shade fade (InCubic), heading fades, the other cards fall 330 px (InCubic), the chosen one hops 10 px (`Arc` on the spot) then falls |

Kept, and why:

- `Anim.Over` in `GameFlow.Drawn` and `GameFlow.Points`: they move nothing, they emit sparks along a path every
  frame (particle code). `Anim.Over` itself is unchanged.
- `Anim.Wait` (the beats, with skip) and the ease functions (`TileView`, `ReactionCutscene`, `GoalToast` and
  `TitleView` still use them).
- `ScreenFx` flash and shake: left as they were. No camera tween was added, so the LateUpdate that resets the camera
  to `camHome` has nothing to fight.
- `ReactionCutscene`: clock-driven timeline with skip; untouched. CardView spring follow, hover lifts, idle pulses,
  particles: untouched.
- Hurry: unchanged. A click during Resolving still only shortens the pace of the reactions that follow (durations are
  scaled by `pace` as before); running tweens are not sped up. `Motion.Wait(.., hurry)` is there if we want that later.

Eases: `OutCubic`, `InCubic` and `OutBack` map to the same PrimeTween eases (same formulas). `InOut` (smoothstep) became
`Ease.InOutSine` (only in ToRack). Linear stays linear.

Checked: compiles with no errors or warnings (recompiled in the open Editor); EditMode tests: 256 passed. Not yet
checked in play: an autoplay run (F9 in the Editor, or `Athanor > Build for Web (quick, with autoplay)` plus
`tools/web/autoplay.mjs`), watching the console for PrimeTween warnings, and a look at a popup, a chain with FloatTexts,
a rack flight, a catalyst pick and a descent.

---

## 2. FMOD for Unity 2.03 (2.03.15)

### Installed

**Not installed.** It has only been downloaded to the Asset Store cache. Importing it would create `Assets/Plugins/FMOD`
(287 entries: `src/` with RuntimeManager, Settings, StudioEventEmitter, EventReference, StudioBankLoader, StudioListener,
StudioParameterTrigger, StudioGlobalParameterTrigger, the Timeline playables; `platforms/*` native libraries;
`addons/` Haptics and Resonance; `staging/`; `obsolete/`). On first import FMOD's setup wizard creates an
`FMODStudioSettings` asset in `Assets/Plugins/FMOD/Resources`.

- **FMOD Studio** (the desktop authoring app, version 2.03.x to match) and an `.fspro` project are separate. Neither is
  on this machine's project folder. All events and banks are authored there and built into `.bank` files.
- Assembly: `FMODUnity` (autoReferenced, allowUnsafeCode, define constraint `UNITY_2021_3_OR_NEWER`). It references
  `Unity.Timeline`, `Unity.Addressables` and `Unity.ResourceManager` (Addressables is not installed, which is fine:
  missing references are ignored and the code is behind `UNITY_ADDRESSABLES_EXIST`) and
  `Unity.RenderPipelines.Universal.Runtime`.
- Licence (`Assets/Plugins/FMOD/LICENSE.txt`, FMOD EULA):
  - The FMOD Engine is free for non-commercial use, and for limited commercial use when the development budget is under
    USD 600k **and** gross revenue or funding is under USD 200k per year, **and** the project is registered at
    https://www.fmod.com/profile#projects.
  - Every product needs an in-game credit containing "FMOD" and "Firelight Technologies Pty Ltd." (https://www.fmod.com/attribution).
    It goes in CREDITS.md and on the title screen next to the font credits.
  - SDK files may not be redistributed except the runtime libraries. That bears on committing `Assets/Plugins/FMOD` to a public repo.
  - Media in FMOD's examples may not be redistributed.

### What it does, and the API we would use

From `src/RuntimeManager.cs` and friends in the package; docs at https://fmod.com/docs/2.03/unity (user guide:
https://fmod.com/docs/2.03/unity/user-guide.html).

| Need | API |
|---|---|
| Fire-and-forget SFX | `RuntimeManager.PlayOneShot(EventReference e, Vector3 pos = default)`; `PlayOneShot("event:/sfx/ignite")` also works by path |
| Music and loops (need stop or parameters) | `EventInstance i = RuntimeManager.CreateInstance(e); i.start(); i.setParameterByName("Depth", d); i.stop(STOP_MODE.ALLOWFADEOUT); i.release();` |
| Reference events from the inspector | `[SerializeField] EventReference ignite;` (a GUID plus the path, with a picker that reads the built banks). We build our scene in code (`Make.*`), so a small ScriptableObject or static class of EventReferences (or path strings) fits better than per-object fields. |
| Scene components | `StudioEventEmitter` (play on start or trigger, stop on destroy), `StudioListener`, `StudioParameterTrigger`, `StudioGlobalParameterTrigger`, `StudioBankLoader` |
| Buses / VCAs | `RuntimeManager.GetBus("bus:/Music").setVolume(v)`, `GetVCA("vca:/SFX")`, `setPaused`, `stopAllEvents` |
| Global | `RuntimeManager.PauseAllEvents(bool)`, `MuteAllEvents(bool)`, `StudioSystem.setParameterByName(...)` (global parameters) |
| Snapshots | Snapshots are events in Studio; start and stop them like any instance. |
| Banks | Settings choose "load all banks at startup" or a specific list; `RuntimeManager.LoadBank(name, loadSamples)`, `HaveAllBanksLoaded`, `AnyBankLoading()`, `AnySampleDataLoading()`, `WaitForAllSampleLoading()`, `UnloadBank` |

Our game is 2D at a fixed camera, so 3D attenuation does not matter: use 2D events (no spatializer), or put the listener on
the camera and keep everything at z 0.

### WebGL specifics

- Native library: `platforms/html5/lib/3.1.39/libfmodstudio.a` (about 7 MB static library; the `L` variant, about
  11 MB, is the logging build for development). `PlatformWebGL.cs` picks the 3.1.39 Emscripten build for
  `UNITY_6000_0_OR_NEWER` and links it statically into the wasm. Unity 6000.6 may ship a newer Emscripten than 3.1.39.
  Whether it links cleanly is the **first thing to test** with an empty WebGL build after import.
- Output is Web Audio only (`OUTPUTTYPE.WEBAUDIO`) and single-threaded. The mixer runs on the main thread, and a
  larger DSP buffer (FMOD > Edit Settings > WebGL) trades latency for fewer crackles. Our project has
  `webGLThreadsSupport: 0`, which matches.
- **Banks load asynchronously.** On WebGL `RuntimeManager.LoadBank` starts a coroutine that fetches
  `StreamingAssets/<bank>.bank` with `UnityWebRequest` and then calls `loadBankMemory`. FMOD's docs say an event used
  before its bank has loaded returns `ERR_EVENT_NOTFOUND`, and they suggest a loading step that waits for the last bank
  (https://fmod.com/docs/2.03/unity/platform-specifics.html). For us: in `GameFlow.Main`, before `ShowTitle`, wait for
  `RuntimeManager.HaveAllBanksLoaded` (with a timeout so a missing bank cannot hang boot), while the screen is still
  black (we already start with `ScreenFx.I.SetBlack(1)`).
- **Audio unlock.** Browsers keep the AudioContext suspended until a user gesture. FMOD becomes audible once it detects
  an interaction (same page as above; Unity's own note: https://docs.unity3d.com/Manual/webgl-audio.html). Our first
  gesture is the title's click. So:
  - title music that starts at boot will be silent until the first click, and may then start mid-phrase;
  - better: start `event:/music/title` on the first click anywhere (or show a "click to start" first), or accept that
    the title is silent and the intro music starts on "Begin". Owner decision (see the questions).
  - WebKit (Safari) is reported to be less reliable at resuming. Test in Safari and in iOS Safari if we care about them.
- Build size: the static library adds to `Build.wasm`. I did not measure it; expect somewhere around 1 to 2 MB
  uncompressed, and measure it on the first test build. The banks come on top. Encode music as Vorbis at a moderate
  quality in Studio, and consider splitting a `Music` bank from a small `SFX` bank so SFX are ready first. Today's whole
  build is 9.9 MB.
- `tools/web/webtest.mjs` runs headless Chrome with `--mute-audio`. That should be fine, but check that a missing
  audio device or a suspended context does not throw during automated runs.
- Unity's own audio: none of our scripts use `AudioSource` or `AudioClip`. Once FMOD is in, Unity audio can be turned
  off (Project Settings > Audio > Disable Unity Audio) to save a little, unless the owner wants Unity audio for anything.

### Sound list (FIRST DRAFT, for the owner and the sound designer to change)

Event paths, mapped to where they would be fired from in today's code:

| Event | Kind | Fired from |
|---|---|---|
| `event:/music/title` | Loop | `GameFlow.ShowTitle` (after the unlock click; stop on Begin) |
| `event:/music/intro` | One-shot or loop with an end | `SimpleCutscene.Intro` (scene 1 to "CLICK TO BEGIN"; see phase-4-lore storyboard, about 44 s) |
| `event:/music/bench` | Loop, parameter `Depth` 1..5 (layers or stems get darker per depth) | Start in `PlayRun` after `FadeIn`; set `Depth` in `Mood(depth)` |
| `event:/music/descent` | Stinger | `SimpleCutscene.Descent` (banner "Depth cleared", then the scroll) |
| `event:/music/ending` | One-shot | `SimpleCutscene.Ending` (won run) |
| `event:/music/run_over` | Stinger, parameter `Won` 0/1 | `RunOverView.Show` |
| `event:/amb/cellar` | Loop, parameter `Depth` (drips, wind, hum) | With the bench music; dust and moonbeam mood |
| `event:/sfx/card_pick` | One-shot | BenchView press or drag start on a card |
| `event:/sfx/place` | One-shot | BenchView `Put` succeeds (card onto tile) |
| `event:/sfx/place_fail` | One-shot | `Put` refused |
| `event:/sfx/rack_return` | One-shot | Card dropped back to the hand or rack |
| `event:/sfx/redraw` | One-shot | `RedrawRequested` |
| `event:/sfx/ignite` | One-shot | `GameFlow.Ignite` |
| `event:/sfx/reaction` | One-shot, parameters `Size` (2..), `Chain` (0..6) | `GameFlow.React` (pitch or layer up with chain; same moment as the shake) |
| `event:/sfx/chain_step` | One-shot, parameter `Chain` | "CHAIN xN" FloatText in `React` |
| `event:/sfx/combo` | One-shot | "N-COMBO" FloatText in `React` |
| `event:/sfx/fizzle` | One-shot | `GameFlow.Fizzle` (the grey poof) |
| `event:/sfx/hint` | One-shot | "NEW HINT" in `Fizzle` |
| `event:/sfx/sputter` | One-shot | `GameFlow.Sputter` ("SPOILED") |
| `event:/sfx/discovery` | One-shot | `GameFlow.Discover` (the held-breath beat), then `ReactionCutscene.Play` |
| `event:/sfx/cutscene_gather` / `burst` / `rise` | One-shots or one event with markers | `ReactionCutscene`: gather until `GatherEnd`, the burst (the frame with Flash + Shake 5), the rise of the new material |
| `event:/sfx/points` | One-shot | `GameFlow.Points` trail arriving at the counter |
| `event:/sfx/to_rack` | One-shot | `GameFlow.ToRack` landing |
| `event:/sfx/consume` | One-shot | `TileView.Consume` (quiet; many at once, so limit voices in Studio) |
| `event:/sfx/lost` | One-shot | "Lost: ..." FloatText at the end of `Resolve` |
| `event:/sfx/depth_cleared` | One-shot | Start of `Descend` |
| `event:/sfx/catalyst_deal` / `catalyst_pick` | One-shots | `CatalystChoiceView.Choose` deal and pick (the gold burst) |
| `event:/sfx/aspect_pick` | One-shot | Aspect choice (same view) |
| `event:/sfx/event_open` | One-shot | `EventView.Show` |
| `event:/sfx/goal` | One-shot | `GoalToast` showing a reward |
| `event:/sfx/run_lost` | One-shot | `EndRun(false)` (the 3 px shake) |
| `event:/ui/button` / `ui/hover` | One-shots | `UiButton` click and hover |
| `event:/ui/journal_open` / `journal_close` / `journal_page` | One-shots | `JournalView.Open`, `Close`, page change |
| `event:/ui/tooltip` | One-shot (very quiet, or none) | `Tooltip.Show` |
| `event:/ui/type` | One-shot per few letters | `SimpleCutscene.Type` (typewriter) |

Buses and snapshots (draft):

| Bus | Holds | Notes |
|---|---|---|
| `bus:/Music` | music/* | A VCA for the player's music volume |
| `bus:/SFX` | sfx/* | A VCA for effects volume |
| `bus:/Ambience` | amb/* | Follows the SFX VCA, or its own |
| `bus:/UI` | ui/* | Not ducked by snapshots |

| Snapshot | When | Effect |
|---|---|---|
| `snapshot:/Cutscene` | During `ReactionCutscene.Play` and the intro | Duck bench music and ambience, open space for the cutscene sounds |
| `snapshot:/Journal` | Journal or goals open | Low-pass the music, lower ambience (the "reading" feel) |
| `snapshot:/Popup` | EventView, CatalystChoice, RunOver | Slight duck |
| `snapshot:/Depth` (or the `Depth` parameter) | Each depth | More reverb and darker EQ deeper down, matching `Mood(depth)` |

Also wanted: a settings place for the volumes (there is no settings screen today) and saving them with `Save`.

### Before implementing (FMOD)

- [ ] Owner registers the project at fmod.com and confirms the licence tier; add the credit line.
- [ ] Install FMOD Studio 2.03.x and create the `.fspro` (suggested location: `audio/Athanor.fspro` beside `Athanor/`, so it stays out of `Assets/`).
- [ ] Try the import in a branch or a copy first; build an **empty WebGL** with FMOD and one test bank and confirm it links with Unity 6000.6's Emscripten, plays after the first click, and measure the size increase.
- [ ] Choose bank import: StreamingAssets (the default; needed for WebGL async loading) or AssetBundle/Addressables.
- [ ] Add `FMODUnity` to `Athanor.Game.asmdef` only; Core must not reference it. Keep all FMOD calls behind one small `Sfx`/`Music` static facade in Game, so a missing bank or an unsupported browser fails silently.
- [ ] Wait for banks in `GameFlow.Main` before the title, with a timeout.
- [ ] Decide how the first click unlocks audio (see questions).
- [ ] Check the headless `webtest.mjs` and autoplay still pass.

---

## 3. AERO - Volumetric Fog and Mist 1.8.0

### Installed

**Not installed.** It has only been downloaded. Importing it would create `Assets/Mirza/AERO - Volumetric Fog and Mist`
and `Assets/Mirza/_VFXToolkit/_Demo` (218 entries, about 38 MB, mostly a 3D demo: lightmaps, reflection probes, APV
bake data, ProBuilder meshes, its own URP assets and a `UniversalRenderPipelineGlobalSettings.asset`). The scripts have
no asmdef, so they would compile into Assembly-CSharp. Its `PC_Renderer.asset` lists two renderer features whose
scripts are not in the package (`VolumetricFogRendererFeature`, `VolumetricFogLightSetupFeature`), so it would show
missing-script warnings. Licence: the standard Asset Store EULA (no licence file in the package); no redistribution of
its files.

Online docs: https://mirzabeig.notion.site/AERO-Volumetric-Fog-Mist-3c0d023ca81842509ad89c749307ac53 (linked from its README).

### What it is

From its `README.txt`, `Shaders/Volumetric Fog.hlsl`, `Scripts/*.cs` and `Settings/PC_Renderer.asset`:

- Not a Volume component. It is a **material on URP's `FullScreenPassRendererFeature`** (injection point 550 =
  after rendering transparents, with "fetch color buffer" on), plus a small `FullscreenCustomLightingFeature` that writes
  `_AmbientLighting` and `_AdditionalLightCount` (from `UniversalLightData`) into the material. Setup is either "use
  our RP asset" or "add a FullScreenPassRendererFeature and set the fog material".
- The shader is a raymarch: for each pixel it reads `SampleSceneDepth`, rebuilds the world position, and marches from
  the camera to that surface (or to `maxDistance`) in `_Steps` steps (4 in the Mobile material, 4 to 12 in the PC
  ones). At each step it samples noise and height-fog density, then lights the fog with `GetMainLight()` (with
  main-light shadows and self-shadow marching), the additional lights loop (with their shadows and cookies), ambient,
  and optionally Adaptive Probe Volumes. Release 1.8.0 added orthographic camera support.

### Does it fit our renderer?

No, not in a useful way:

| Need of AERO | Our project |
|---|---|
| A scene depth texture with real depth | The 2D Renderer does not give the opaque depth that this needs. Sprites don't write depth. Every pixel reads as the far plane (or nothing), so the march covers the same distance everywhere: a uniform tint, no volume. |
| URP `Light`s (main directional light, additional point/spot lights, shadow maps) | We only use `Light2D` (global ambient, the moonbeam, flame and tile glows, cutscene lights). `GetMainLight()` and the additional-light loop see none of them, so the fog is lit by ambient only. The "glow through the mist" look is lost. |
| 3D space to march through | Everything is at z 0 with an orthographic camera. |
| A frame where the UI is separate | Our HUD, cards, text, tooltips, popups and the fade rect are all world sprites in the same camera (no Canvas). A fullscreen pass after transparents would fog all of them. The 2D Renderer has no per-sorting-order injection point for a fullscreen pass. |
| Renderer features on Renderer2D | The 2D renderer asset does have a feature list (`m_RendererFeatures: []` in ours), and Unity's docs describe the Full Screen Pass Renderer Feature for URP renderers (https://docs.unity3d.com/Manual/urp/renderer-features/renderer-feature-full-screen-pass.html). There is a "Won't Fix" Unity bug about it covering only half the screen on Renderer2D with Shader Graph materials (https://issuetracker.unity3d.com/issues/urp-renderer-2d-full-screen-pass-renderer-feature-is-only-affecting-half-of-the-screen-when-a-material-created-from-a-shader-graph-is-used-as-a-pass-material). So the pass itself can probably run, but the fog would be wrong for the reasons above. |
| PixelPerfectCamera | With grid snapping set to upscale render texture, a fullscreen pass runs on the 640x360 target. Smooth noise would then be upscaled with point filtering into blocky but not "pixel art" gradients. It would also have to be checked against the crop frame (`m_CropFrame: 3`). |
| WebGL cost | Moderate at 640x360 with 4 steps, but the shader carries shadow, cookie and APV keyword variants we cannot use. Untested on WebGL2; WebGPU unknown. |

Switching the project to the Universal (3D) Renderer to suit AERO would cost us Light2D, the sprite lighting, and the
whole look. Not worth it.

### Where fog would help, and the alternative

Today `GameFlow.Mood(depth)` sets the global ambient Light2D colour and the moonbeam intensity from two arrays (five
depths: blue, teal, violet, red, pale gold; moon 0.62 down to 0.3, back to 0.5 at depth 5). It is called at the title,
at run start, and midway through the descent while the screen is covered. Fog would extend the same idea: "the deeper,
the stranger the colour and the weaker the moon", plus thicker air.

Proposed instead of AERO: **a sprite fog layer**, pixel art, inside the normal sprite sort.

- Two or three large tiling fog sprites (drawn at 1 px per unit, a few hundred px wide), placed at a sort order
  **between the room background and the bench** (and a thin one in front of the bench at low alpha if wanted), never
  above the HUD or popups. Scroll them slowly in opposite directions at whole-pixel steps (UV offset by whole texels,
  or `Px.Snap` positions), as `Dust` does with its motes.
- Use the **Sprite-Lit** material so the fog is lit by Light2D: the moonbeam and the flame glow would light the
  mist they pass through for free. A version with the moonbeam's shape cut out would give god rays.
- Add per-depth arrays next to `Ambient` and `Moon` in GameFlow: `FogColour[depth]`, `FogAlpha[depth]` (for example
  0.0, 0.10, 0.18, 0.26, 0.15), and set them in `Mood`. PrimeTween could ease them over the descent's hidden midpoint.
- Art: generate the fog texture with the existing `tools/art` pipeline, or export pixelfx's weather fog
  (`pixelfx/src/fx/weather/fog.js`, MIT; credit as for the other pixelfx sheets) with `tools/fx/export.sh`.
- If a shader is wanted later: a small Sprite-Lit Shader Graph with two scrolling noise textures, point sampled,
  with a dithered alpha (Bayer) for a pixel-art falloff, on the same layer sprites. That keeps it inside the 2D
  renderer and the Light2D system, and off the UI.

Cost: a few full-screen-sized transparent sprites, which is less than AERO and works the same on WebGL.

### Before implementing (fog)

- [ ] Owner confirms dropping AERO for this project (or tests it first in a **separate throwaway project**; do not import its 38 MB demo and RP assets into Athanor).
- [ ] Pick the look: a background layer only, or one in front of the bench too; how thick at depth 5.
- [ ] Draw or export the fog textures; check they tile and stay readable behind the bench and cards.
- [ ] Add the fog arrays to `Mood` and a sort-order slot in `Make`/layout that is below the HUD.
- [ ] Check legibility of the tiles and cards at depth 4 (red) and 5, and the WebGL frame time.

---

## Open questions for the owner

1. Is the GitHub repo private, and will it stay private? That decides whether the PrimeTween `.tgz`, `Assets/Plugins/FMOD`
   and any AERO files may be committed, or must be gitignored and installed per machine (PrimeTween from npm).
2. PrimeTween: keep the copy in `Packages/` or the installer's `Assets/Plugins/PrimeTween/internal/`? Delete the Demo?
3. PrimeTween: is the small change of the `InOut` ease to `Ease.InOutSine` acceptable, or should the curves stay exact?
4. FMOD: does the game qualify for the free indie tier (budget under USD 600k, revenue under USD 200k a year)? Who registers the project?
5. FMOD: who makes the sounds, and with which Studio version? Is there an existing `.fspro` anywhere else?
6. FMOD on WebGL: the title is silent until the first click. Do we add a "click to start" before the title, start title
   music on the first click, or start music only with the intro?
7. Volume controls: where should they live? There is no settings screen today.
8. AERO: OK to drop it in favour of a Light2D-lit sprite fog layer? If you bought it for a later 3D or URP-Universal
   project, it is still fine to keep in the Asset Store library.
9. Should fog cover only the room (background) or also drift in front of the bench at low alpha?
10. Sound was out of Phase 4 scope in phase-4-prep.md. Is it in scope now, or Phase 5?

**Fix after the play check (2026-11-13):** PrimeTween payloads are limited to 16 bytes. `Motion.Arc` passed a 32-byte
struct, so it returned dead tweens and the Sequences refused them. The arc data is now the tween's target (a small class,
one per arc), with a null check for a destroyed transform. `MotionTests` guards every Motion helper.
