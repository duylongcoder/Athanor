#!/usr/bin/env python3
"""Percent of pixels that differ between before*.png and after*.png pairs. Stdlib only (8-bit, non-interlaced PNG)."""
import struct, sys, zlib, glob, os

def decode(path):
    data = open(path, 'rb').read()
    assert data[:8] == b'\x89PNG\r\n\x1a\n', path
    pos, idat, plte = 8, b'', None
    while pos < len(data):
        ln, typ = struct.unpack('>I4s', data[pos:pos + 8])
        body = data[pos + 8:pos + 8 + ln]
        pos += 12 + ln
        if typ == b'IHDR':
            w, h, bd, ct, _, _, il = struct.unpack('>IIBBBBB', body)
        elif typ == b'PLTE':
            plte = body
        elif typ == b'IDAT':
            idat += body
    assert bd == 8 and il == 0, (path, bd, il)
    ch = {0: 1, 2: 3, 3: 1, 4: 2, 6: 4}[ct]
    raw = zlib.decompress(idat)
    stride = w * ch
    rows, prev = [], bytearray(stride)
    p = 0
    for _ in range(h):
        f = raw[p]; line = bytearray(raw[p + 1:p + 1 + stride]); p += 1 + stride
        if f == 1:
            for i in range(ch, stride): line[i] = (line[i] + line[i - ch]) & 255
        elif f == 2:
            for i in range(stride): line[i] = (line[i] + prev[i]) & 255
        elif f == 3:
            for i in range(stride):
                a = line[i - ch] if i >= ch else 0
                line[i] = (line[i] + ((a + prev[i]) >> 1)) & 255
        elif f == 4:
            for i in range(stride):
                a = line[i - ch] if i >= ch else 0
                b = prev[i]; c = prev[i - ch] if i >= ch else 0
                pa, pb, pc = abs(b - c), abs(a - c), abs(a + b - 2 * c)
                pr = a if pa <= pb and pa <= pc else (b if pb <= pc else c)
                line[i] = (line[i] + pr) & 255
        rows.append(line); prev = line
    # normalise to RGB tuples per pixel (alpha dropped when 4/2 channel: compare composite on RGB + alpha)
    px = []
    for line in rows:
        for x in range(w):
            if ct == 6: px.append(bytes(line[x * 4:x * 4 + 4]))
            elif ct == 2: px.append(bytes(line[x * 3:x * 3 + 3]) + b'\xff')
            elif ct == 3:
                i = line[x]; px.append(bytes(plte[i * 3:i * 3 + 3]) + b'\xff')
            elif ct == 0: g = line[x]; px.append(bytes([g, g, g, 255]))
            else: g = line[x * 2]; px.append(bytes([g, g, g, line[x * 2 + 1]]))
    return w, h, px

def diff(a, b):
    wa, ha, pa = decode(a); wb, hb, pb = decode(b)
    if (wa, ha) != (wb, hb):
        return None, f'size {wa}x{ha} vs {wb}x{hb}'
    n = sum(1 for x, y in zip(pa, pb) if x != y)
    return 100.0 * n / len(pa), f'{n} of {len(pa)} px'

if __name__ == '__main__':
    root = sys.argv[1]
    for d in sorted(glob.glob(os.path.join(root, '*'))):
        for b in sorted(glob.glob(os.path.join(d, 'before*.png'))):
            a = b.replace('/before', '/after')
            if not os.path.exists(a): print(f'{os.path.relpath(b, root)}: no after'); continue
            pct, note = diff(b, a)
            name = os.path.basename(d) + ('' if os.path.basename(b) == 'before.png' else ' ' + os.path.basename(b)[7:-4])
            print(f'{name}\t{"n/a" if pct is None else f"{pct:.3f} %"}\t{note}')
