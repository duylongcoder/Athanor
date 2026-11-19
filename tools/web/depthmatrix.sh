#!/usr/bin/env bash
# 5b-06: the frame-time matrix. Needs the release builds served: Main on :8080 (Build/web), DepthTest on :8081 (Build/web-depth).
#   bash tools/web/depthmatrix.sh <outDir>
cd "$(dirname "$0")/../.." || exit 1
E=${1:?usage: bash tools/web/depthmatrix.sh <outDir>}
D="wait:3,perf:default:15,key:5,wait:2,perf:depth5:15,key:e,wait:2,perf:fxoff_depth5:15"
M="wait:3,perf:title:15"
for sc in 2x:1280x720 3x:1920x1080; do s=${sc%%:*}; v=${sc##*:}
  for mode in gpu-capped gpu-uncapped; do
    fl="--gpu"; [ $mode = gpu-uncapped ] && fl="--gpu --uncapped"
    node tools/web/depthprobe.mjs http://127.0.0.1:8081/ "$E/depth-$s-$mode" $v $D $fl
    node tools/web/depthprobe.mjs http://127.0.0.1:8080/ "$E/main-$s-$mode" $v $M $fl
  done
  node tools/web/depthprobe.mjs http://127.0.0.1:8081/ "$E/depth-$s-swiftshader" $v "wait:3,perf:default:20,key:e,wait:2,perf:fxoff:20"
  node tools/web/depthprobe.mjs http://127.0.0.1:8080/ "$E/main-$s-swiftshader" $v "wait:3,perf:title:20"
done
echo MATRIX DONE
