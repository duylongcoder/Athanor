#!/usr/bin/env bash
# 5b-06: frame sequences of the DepthTest build for the shimmer check, at 2x (1280x720) and 3x (1920x1080) on the GPU.
# DepthTest served on :8081 (Build/web-depth). Pictures are large: write them outside the repo, then run depthshimmer.py.
#   bash tools/web/depthshimmer.sh <frameDir>
cd "$(dirname "$0")/../.." || exit 1
F=${1:?frame dir}
for sc in 2x:1280x720 3x:1920x1080; do s=${sc%%:*}; v=${sc##*:}
  # depth 3 (fog and tilt-shift on), sway on, then sway off for the noise floor and the still pair, then effects off
  node tools/web/depthprobe.mjs http://127.0.0.1:8081/ "$F/$s" $v \
"key:3,wait:2,frames:swayon:60:200,key:s,wait:1,frames:swayoff:30:200,frames:still:2:1000,key:e,wait:1,frames:swayoff_nofx:30:200,key:s,wait:1,frames:swayon_nofx:60:200,key:e,key:s,key:5,wait:2,frames:fog5:2:500" --gpu
done
echo SHIMMER CAPTURE DONE
