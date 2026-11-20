"""Itch.io page art for Athanor: banner, page background, embed background.

Built from the game's own sprites and fonts at native pixel scale, then scaled up by an
integer factor (nearest neighbour) so pixels stay crisp.

    python3 production/itch/make_itch_art.py      # writes banner.png, background.png, embed_bg.png here
"""
from pathlib import Path
import math
from PIL import Image, ImageDraw, ImageFont, ImageFilter

ROOT = Path(__file__).resolve().parents[2]
ART = ROOT / "Athanor/Assets/Art"
OUT = Path(__file__).resolve().parent

BG = (14, 11, 16)          # page base colour, also the bottom fade of background.png
GLOW = (255, 140, 40)


def img(rel):
    return Image.open(ART / rel).convert("RGBA")


def font(name, size):
    return ImageFont.truetype(str(ART / "Fonts" / name), size)


def darken(im, k):
    """Multiply RGB by k (0..1), keep alpha."""
    r, g, b, a = im.split()
    r, g, b = (c.point(lambda v: int(v * k)) for c in (r, g, b))
    return Image.merge("RGBA", (r, g, b, a))


def radial(w, h, cx, cy, rx, ry, color, strength, steps=6):
    """Banded (pixel-art style) radial glow layer."""
    layer = Image.new("RGBA", (w, h), (0, 0, 0, 0))
    px = layer.load()
    for y in range(h):
        for x in range(w):
            d = math.hypot((x - cx) / rx, (y - cy) / ry)
            if d < 1:
                t = math.ceil((1 - d) * steps) / steps    # quantised bands
                px[x, y] = (*color, int(255 * strength * t * t))
    return layer


def vignette(w, h, strength, power=2.2):
    layer = Image.new("RGBA", (w, h), (0, 0, 0, 0))
    px = layer.load()
    for y in range(h):
        for x in range(w):
            d = math.hypot((x - w / 2) / (w / 2), (y - h / 2) / (h / 2)) / math.sqrt(2)
            px[x, y] = (*BG, int(255 * min(1, strength * d ** power * 2)))
    return layer


def logo(text, fnt, outline=(40, 16, 8), shadow=(0, 0, 0)):
    """Title-screen style logo: cream-to-orange banded gradient, dark outline, drop shadow."""
    l, t, r, b = fnt.getbbox(text)
    w, h = r - l + 6, b - t + 7
    mask = Image.new("L", (w, h), 0)
    d = ImageDraw.Draw(mask)
    d.fontmode = "1"
    d.text((3 - l, 3 - t), text, font=fnt, fill=255)
    ramp = [(255, 246, 222), (255, 226, 178), (252, 196, 128), (240, 152, 74), (214, 104, 40), (160, 62, 22)]
    grad = Image.new("RGBA", (w, h))
    gp = grad.load()
    for y in range(h):
        c = ramp[min(len(ramp) - 1, max(0, int((y - 3) / max(1, h - 7) * len(ramp))))]
        for x in range(w):
            gp[x, y] = (*c, 255)
    out = Image.new("RGBA", (w, h), (0, 0, 0, 0))
    edge = mask.filter(ImageFilter.MaxFilter(3))
    sh = Image.new("RGBA", (w, h), (*shadow, 200))
    out.paste(sh, (0, 2), edge)
    out.paste(Image.new("RGBA", (w, h), (*outline, 255)), (0, 0), edge)
    out.paste(grad, (0, 0), mask)
    return out


def text_img(text, fnt, color, outline=(20, 10, 8)):
    l, t, r, b = fnt.getbbox(text)
    w, h = r - l + 4, b - t + 4
    mask = Image.new("L", (w, h), 0)
    d = ImageDraw.Draw(mask)
    d.fontmode = "1"
    d.text((2 - l, 2 - t), text, font=fnt, fill=255)
    out = Image.new("RGBA", (w, h), (0, 0, 0, 0))
    out.paste(Image.new("RGBA", (w, h), (*outline, 255)), (0, 0), mask.filter(ImageFilter.MaxFilter(3)))
    out.paste(Image.new("RGBA", (w, h), (*color, 255)), (0, 0), mask)
    return out


def put(canvas, sprite, cx, cy):
    """Paste sprite centred on (cx, cy)."""
    canvas.alpha_composite(sprite, (int(cx - sprite.width / 2), int(cy - sprite.height / 2)))


def icon_glow(canvas, name, cx, cy, color=GLOW, s=0.35):
    canvas.alpha_composite(radial(canvas.width, canvas.height, cx, cy, 20, 20, color, s, 4))
    put(canvas, img(f"Icons/{name}.png"), cx, cy)


def wall(w, h, k):
    """Workshop wall tiled to w x h (brick part only), darkened by k."""
    src = img("Background/workshop_wall.png").crop((0, 0, 640, 128))
    bricks = src.crop((0, 0, 256, 64))          # a plain brick patch, no window/shelves
    out = Image.new("RGBA", (w, h))
    for y in range(0, h, 64):
        for x in range(0, w, 256):
            out.alpha_composite(bricks, (x, y))
    return darken(out, k)


def scale(im, n):
    return im.resize((im.width * n, im.height * n), Image.NEAREST).convert("RGB")


# --- Banner: 960 x 240 (320 x 80 native, x3) -------------------------------------------
def banner():
    w, h = 320, 80
    c = Image.new("RGBA", (w, h), (*BG, 255))
    c.alpha_composite(wall(w, h, 0.38))
    c.alpha_composite(radial(w, h, w / 2, h / 2 + 2, 150, 46, GLOW, 0.55))
    c.alpha_composite(vignette(w, h, 0.8, 1.6))
    lg = logo("Athanor", font("Alagard.ttf", 48))
    put(c, lg, w / 2, 32)
    st = text_img("An alchemist's descent", font("Alagard.ttf", 16), (255, 214, 92))
    put(c, st, w / 2, 66)
    for name, x, y, col in [("ember_coal", 18, 22, GLOW), ("glowmoss", 46, 40, (120, 255, 120)),
                            ("moondust", 18, 60, (180, 190, 255)),
                            ("star_shard", 302, 22, (255, 230, 140)), ("quicksilver", 274, 40, (190, 220, 255)),
                            ("phoenix_feather", 302, 60, GLOW)]:
        icon_glow(c, name, x, y, col, 0.18)
    sp = img("Fx/sparkle.png")
    for x, y in [(62, 14), (258, 66), (40, 70), (282, 12), (70, 64), (250, 18)]:
        put(c, sp, x, y)
    return scale(c, 3)


# --- Background: 1920 x 1080 (640 x 360 native, x3) ------------------------------------
def background():
    w, h = 640, 360
    c = Image.new("RGBA", (w, h), (*BG, 255))
    room = darken(img("Background/workshop_wall.png"), 0.55)
    c.alpha_composite(room)
    # candles with glow on both shelves (left 15-165, right 475-625 at y~120); these show either side of
    # itch's 960 px page column
    cand = img("Background/candle.png")
    fl = img("Background/flame_small.png").crop((3, 0, 11, 10))
    for x in (84, 158, 484, 617):          # gaps between the shelf items
        c.alpha_composite(radial(w, h, x, 104, 46, 40, GLOW, 0.28, 5))
        put(c, cand, x, 112)
        put(c, fl, x, 99)
    # fade the floor half and the bottom edge into the page colour
    fade = Image.new("RGBA", (w, h), (0, 0, 0, 0))
    fp = fade.load()
    for y in range(h):
        a = 0 if y < 128 else min(255, int(255 * ((y - 128) / (h - 128)) ** 0.7))
        for x in range(w):
            fp[x, y] = (*BG, a)
    c.alpha_composite(fade)
    c.alpha_composite(vignette(w, h, 0.6, 2.4))
    return scale(c, 3)


# --- Embed background: 1280 x 720 (640 x 360 native, x2) -------------------------------
def embed_bg():
    w, h = 640, 360
    c = Image.new("RGBA", (w, h), (*BG, 255))
    c.alpha_composite(darken(img("Background/workshop_wall.png"), 0.6))
    # warm hearth glow from the cauldron fire
    c.alpha_composite(radial(w, h, w / 2, 300, 260, 150, GLOW, 0.45))
    caul = img("Background/cauldron.png")
    logs = img("Background/logs.png")
    flames = img("Background/flame_big.png")
    cy = 250                                  # cauldron centre; rim y~191-215, bowl bottom ~305, legs to 334
    put(c, logs, w / 2, 338)
    put(c, flames.crop((28, 0, 154, 17)), w / 2, 318)
    put(c, caul, w / 2, cy)
    # glowing brew filling the mouth (local mouth centre ~ (100, 40))
    brew = Image.new("RGBA", (w, h), (0, 0, 0, 0))
    bd = ImageDraw.Draw(brew)
    for (rx, ry), col in [((62, 8), (196, 92, 30)), ((52, 6), (240, 150, 50)), ((34, 4), (255, 210, 110)),
                          ((14, 2), (255, 244, 200))]:
        bd.ellipse((w / 2 - rx, cy - 45 - ry, w / 2 + rx, cy - 45 + ry), fill=(*col, 255))
    c.alpha_composite(brew)
    c.alpha_composite(radial(w, h, w / 2, cy - 60, 110, 40, (255, 180, 80), 0.3, 4))
    sp = img("Fx/sparkle.png")
    for x, y in [(282, 178), (356, 168), (304, 150), (338, 186), (262, 160), (380, 182), (322, 130)]:
        put(c, sp, x, y)
    c.alpha_composite(vignette(w, h, 0.7, 2.0))
    lg = logo("Athanor", font("Alagard.ttf", 64))
    put(c, lg, w / 2, 62)
    st = text_img("An alchemist's descent", font("Alagard.ttf", 16), (255, 214, 92))
    put(c, st, w / 2, 104)
    return scale(c, 2)


# --- Cover: 630 x 500 (315 x 250 native, x2) -------------------------------------------
def cover():
    w, h = 315, 250
    c = Image.new("RGBA", (w, h), (*BG, 255))
    c.alpha_composite(darken(img("Background/workshop_wall.png").crop((162, 0, 162 + w, h)), 0.6))
    c.alpha_composite(radial(w, h, w / 2, 220, 190, 120, GLOW, 0.5))
    cy = 172                                   # cauldron centre: rim ~113, bowl bottom ~227
    put(c, img("Background/flame_big.png").crop((28, 0, 154, 17)), w / 2, 238)
    put(c, img("Background/cauldron.png"), w / 2, cy)
    brew = Image.new("RGBA", (w, h), (0, 0, 0, 0))
    bd = ImageDraw.Draw(brew)
    for (rx, ry), col in [((62, 8), (196, 92, 30)), ((52, 6), (240, 150, 50)), ((34, 4), (255, 210, 110)),
                          ((14, 2), (255, 244, 200))]:
        bd.ellipse((w / 2 - rx, cy - 45 - ry, w / 2 + rx, cy - 45 + ry), fill=(*col, 255))
    c.alpha_composite(brew)
    c.alpha_composite(radial(w, h, w / 2, cy - 58, 110, 36, (255, 180, 80), 0.3, 4))
    # ingredients falling into the brew
    for name, x, y, col in [("ember_coal", 92, 92, GLOW), ("moondust", 132, 78, (180, 190, 255)),
                            ("glowmoss", 184, 82, (120, 255, 120)), ("star_shard", 224, 94, (255, 230, 140))]:
        icon_glow(c, name, x, y, col, 0.22)
    sp = img("Fx/sparkle.png")
    for x, y in [(110, 108), (158, 100), (206, 108), (70, 70), (248, 72), (150, 64), (40, 150), (276, 140)]:
        put(c, sp, x, y)
    c.alpha_composite(vignette(w, h, 0.65, 2.2))
    put(c, logo("Athanor", font("Alagard.ttf", 48)), w / 2, 30)
    put(c, text_img("An alchemist's descent", font("Alagard.ttf", 16), (255, 214, 92)), w / 2, 58)
    return scale(c, 2)


# --- Screenshots: five 1280 x 720 frames from the WebGL check runs ---------------------
SHOTS = ROOT / "production/qa/evidence/phase7/baseline/web"
PICKS = [("screenshot_1_bench.png", "autoplay/p030.png"),          # depth 4, a big reaction on the bench
         ("screenshot_2_discovery.png", "autoplay/p031.png"),      # New Discovery: Thunder Ingot
         ("screenshot_3_descent.png", "descents/d3_en_2_2.png"),   # descent card: The Humming Dark
         ("screenshot_4_catalyst.png", "descents/c2_choice_en.png"),  # choose a catalyst
         ("screenshot_5_journal.png", "webtest3/a_13_2_5_journal.png")]  # the Journal


def screenshot(rel):
    """Copy a check-run frame and paint out the 'Development Build' stamp (x 1166-1275, y 698-714)
    with the colour just left of it on each row."""
    im = Image.open(SHOTS / rel).convert("RGB")
    px = im.load()
    for y in range(697, 716):
        fill = px[1160, y]
        for x in range(1162, 1280):
            px[x, y] = fill
    return im


if __name__ == "__main__":
    for name, fn in [("banner.png", banner), ("background.png", background), ("embed_bg.png", embed_bg),
                     ("cover.png", cover)] + [(n, (lambda r=r: screenshot(r))) for n, r in PICKS]:
        im = fn()
        im.save(OUT / name, optimize=True)
        print(name, im.size, (OUT / name).stat().st_size // 1024, "KB")
