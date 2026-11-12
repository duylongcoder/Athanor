#!/usr/bin/env bash
# Exports the pixelfx effects the game plays as sprite sheets (PNG + JSON) into the Unity project.
#
#   tools/fx/export.sh                 uses ./pixelfx
#   PIXELFX=/path/to/pixelfx tools/fx/export.sh
#
# Afterwards, in Unity: Athanor > Import Effect Sheets.
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/../.." && pwd)"
PFX="${PIXELFX:-$ROOT/pixelfx}"
OUT="$ROOT/Athanor/Assets/Art/Fx/Sheets"
IDS=(
  explosions/implosion      # the moment two materials meet in the cauldron
  holy/holy-pillar          # the column of light under a new discovery
  pickups/collect-burst     # a known reaction on the bench
  spawn/poof                # a pair that does not react
)

[ -f "$PFX/tools/render.mjs" ] || { echo "pixelfx not found at $PFX (set PIXELFX=/path/to/pixelfx)" >&2; exit 1; }
TMP="$(mktemp -d)"; trap 'rm -rf "$TMP"' EXIT
mkdir -p "$OUT"
for id in "${IDS[@]}"; do
  name="${id#*/}"
  node "$PFX/tools/render.mjs" "$id" --sheet --no-actor --out "$TMP"
  cp "$TMP/$id/$name.png" "$TMP/$id/$name.json" "$OUT/"
done
echo "Wrote ${#IDS[@]} sheets to $OUT. Now in Unity: Athanor > Import Effect Sheets."
