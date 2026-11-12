#!/bin/bash
python3 - <<'PY'
import base64
p=open('page.html').read()
fonts={'Alagard':'f/Alagard.ttf','Romulus':'f/Romulus.ttf','PixOpB':'f/PixelOperator-Bold.ttf','PixOp8':'f/PixelOperator8.ttf','BitPotion':'f/BitPotion.ttf'}
css='<style>'+''.join("@font-face{font-family:%s;src:url(data:font/ttf;base64,%s)}"%(k,base64.b64encode(open(v,'rb').read()).decode()) for k,v in fonts.items())+'</style>'
js=open('art.js').read()+'\n'+open('scene.js').read()
open('out.html','w').write(p.replace('<!--FONTS-->',css).replace('<!--SCRIPTS-->','<script>\n'+js+'\n</script>'))
PY
