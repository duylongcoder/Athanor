"""5b-06 shimmer numbers and viewing aids from the frame sequences of depthshimmer.sh (Pillow only, no numpy).
   python3 -I tools/web/depthshimmer.py <frameDir> <outDir>

Regions are in 640x360 art px; at display scale s (2 or 3) a region is s times larger on screen. Per pair of consecutive
pictures it reports, per region:  changed = share of pixels whose luma moved by more than 8 levels;  mean = mean luma change;
resid = the same share after the best whole-art-px shift of the second picture (|dx|,|dy| <= 6 art px), which removes a
clean parallax move and keeps what crawls: with nearest sampling a clean move leaves ~0, a shimmer leaves a residue.
"""
import glob, json, os, statistics, sys
from PIL import Image, ImageChops, ImageStat, ImageDraw

frames, out = sys.argv[1], sys.argv[2]
os.makedirs(out, exist_ok=True)
# name: (x0, y0, x1, y1) art px, read from the 2x pictures
REGIONS = {
    'wall (bricks, moves with sway)': (155, 22, 260, 58),
    'bench edge (gold frame, table lip)': (175, 238, 465, 252),
    'tiles (3x3 faces)': (212, 146, 428, 238),
    'fog over tiles (left, under fog sheet)': (0, 150, 105, 235),
    'HUD panel (control, must not move)': (10, 10, 158, 82),
}
SEQS = ['swayon', 'swayoff', 'swayon_nofx', 'swayoff_nofx']
THRESH = 8
MAXSHIFT = 6


def load(s, seq):
    return [Image.open(f).convert('L') for f in sorted(glob.glob(f'{frames}/{s}/{seq}/[0-9]*.png'))]


def crop(im, r, k, dx=0, dy=0):
    return im.crop(((r[0] + dx) * k, (r[1] + dy) * k, (r[2] + dx) * k, (r[3] + dy) * k))


def changed(a, b):
    d = ImageChops.difference(a, b)
    h = d.histogram()
    return 100.0 * sum(h[THRESH + 1:]) / sum(h), ImageStat.Stat(d).mean[0]


def resid(ia, ib, r, k):
    a = crop(ia, r, k)
    best = None
    for dy in range(-MAXSHIFT, MAXSHIFT + 1):
        for dx in range(-MAXSHIFT, MAXSHIFT + 1):
            c, _ = changed(a, crop(ib, r, k, dx, dy))
            if best is None or c < best[0]:
                best = (c, dx, dy)
    return best


res = {}
lines = []
for s in (2, 3):
    k = s
    lines.append(f'=== display scale {s}x ({640 * s}x{360 * s}) ===')
    for seq in SEQS:
        ims = load(f'{s}x', seq)
        t = json.load(open(f'{frames}/{s}x/{seq}/times.json'))
        gap = statistics.mean([b - a for a, b in zip(t, t[1:])])
        lines.append(f'-- {seq}: {len(ims)} pictures, {gap:.0f} ms apart')
        for name, r in REGIONS.items():
            ch, mn, rs, sh = [], [], [], []
            for ia, ib in zip(ims, ims[1:]):
                c, m = changed(crop(ia, r, k), crop(ib, r, k))
                ch.append(c); mn.append(m)
                if seq.startswith('swayon') and name.startswith(('wall', 'bench', 'tiles')):
                    q = resid(ia, ib, r, k); rs.append(q[0]); sh.append((q[1], q[2]))
            row = dict(changed_mean=round(statistics.mean(ch), 2), changed_p95=round(sorted(ch)[int(.95 * (len(ch) - 1))], 2), luma_mean=round(statistics.mean(mn), 3))
            txt = f'   {name:42s} changed {row["changed_mean"]:6.2f}% (p95 {row["changed_p95"]:6.2f}%)  mean luma {row["luma_mean"]:.3f}'
            if rs:
                row['resid_mean'] = round(statistics.mean(rs), 2); row['resid_p95'] = round(sorted(rs)[int(.95 * (len(rs) - 1))], 2)
                row['best_shift_art_px_mean_abs'] = round(statistics.mean([abs(a) + abs(b) for a, b in sh]), 2)
                txt += f'  | after best shift {row["resid_mean"]:6.2f}% (p95 {row["resid_p95"]:6.2f}%)'
            res[f'{s}x/{seq}/{name}'] = row
            lines.append(txt)
    # still pair: sway off, 1 s apart
    st = load(f'{s}x', 'still')
    lines.append('-- still pair (sway off, effects on, ~1 s apart)')
    for name, r in REGIONS.items():
        c, m = changed(crop(st[0], r, k), crop(st[1], r, k))
        res[f'{s}x/still/{name}'] = dict(changed=round(c, 2), luma_mean=round(m, 3))
        lines.append(f'   {name:42s} changed {c:6.2f}%  mean luma {m:.3f}')

    # viewing aids
    on = [Image.open(f).convert('RGB') for f in sorted(glob.glob(f'{frames}/{s}x/swayon/[0-9]*.png'))]
    crop_r = (150, 130, 480, 255)
    gif = [crop(im, crop_r, k).quantize(colors=128, dither=0) for im in on]
    gif[0].save(f'{out}/sway-{s}x-bench-crop.gif', save_all=True, append_images=gif[1:], duration=120, loop=0, optimize=False)
    fog = [Image.open(f).convert('RGB') for f in sorted(glob.glob(f'{frames}/{s}x/swayon/[0-9]*.png'))]
    fr = (0, 140, 215, 250)
    g2 = [crop(im, fr, k).quantize(colors=128, dither=0) for im in fog]
    g2[0].save(f'{out}/sway-{s}x-fog-over-tiles-crop.gif', save_all=True, append_images=g2[1:], duration=120, loop=0)
    # strip: fog/tile crop at 6 pictures across the sway
    idx = [0, 10, 20, 30, 40, 50]
    cw, chh = crop(fog[0], fr, k).size
    strip = Image.new('RGB', (cw * 3, chh * 2))
    for n, i in enumerate(idx):
        strip.paste(crop(fog[i], fr, k), ((n % 3) * cw, (n // 3) * chh))
    strip.save(f'{out}/strip-{s}x-fog-over-tiles.png')
    # amplified frame difference, whole picture, one sway pair and the still pair
    def amp(a, b, name):
        d = ImageChops.difference(a.convert('RGB'), b.convert('RGB')).point(lambda v: min(255, v * 8))
        d.save(f'{out}/{name}')
    amp(on[10], on[11], f'diff-{s}x-sway-pair-x8.png')
    stl = [Image.open(f).convert('RGB') for f in sorted(glob.glob(f'{frames}/{s}x/still/[0-9]*.png'))]
    amp(stl[0], stl[1], f'diff-{s}x-still-pair-x8.png')
    on[0].save(f'{out}/frame-{s}x-depth3-sway.png')
    Image.open(f'{frames}/{s}x/fog5/000.png').save(f'{out}/frame-{s}x-depth5-fog.png')
    Image.open(f'{frames}/{s}x/swayon_nofx/000.png').save(f'{out}/frame-{s}x-effects-off.png')

open(f'{out}/shimmer.txt', 'w').write('\n'.join(lines) + '\n')
json.dump(res, open(f'{out}/shimmer.json', 'w'), indent=1)
print('\n'.join(lines))
