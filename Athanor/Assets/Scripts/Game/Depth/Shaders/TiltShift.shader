// The HD-2D tilt-shift: a per-row Gaussian (sigma in art px, 0 inside the sharp band), separable: pass 0 horizontal,
// pass 1 vertical. Drawn by DepthTiltShiftFeature on the world camera only, before post-processing (bloom, vignette).
Shader "Athanor/Tilt Shift"
{
    Properties
    {
        _TiltBand("Band top, bottom, feather (art rows), max sigma (art px)", Vector) = (145, 235, 110, 1.5)
        _FlipY("Texture rows run top-down", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off ZTest Always Cull Off Blend Off

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

        float4 _TiltBand;
        float _FlipY;

        // blur sigma of this pixel's art row, in screen pixels of the world texture (K per art px)
        float SigmaPx(float2 uv)
        {
            float artY = (_FlipY > 0.5 ? uv.y : 1.0 - uv.y) * 360.0;
            float d = max(0.0, max(_TiltBand.x - artY, artY - _TiltBand.y));
            return _TiltBand.w * smoothstep(0.0, 1.0, saturate(d / _TiltBand.z)) * (_ScreenParams.y / 360.0);
        }

        half4 Blur(float2 uv, float2 dir)
        {
            half4 c = SAMPLE_TEXTURE2D_LOD(_BlitTexture, sampler_PointClamp, uv, 0);
            float s = SigmaPx(uv);
            if (s < 0.05) return c;
            float2 texel = dir / _ScreenParams.xy;
            float inv = 1.0 / (2.0 * s * s);
            int r = min((int)ceil(s * 3.0), 16);
            half4 sum = c;
            float total = 1.0;
            // two taps per bilinear fetch
            [loop] for (int i = 1; i <= r; i += 2)
            {
                float w1 = exp(-i * i * inv), w2 = i + 1 <= r ? exp(-(i + 1) * (i + 1) * inv) : 0.0;
                float w = w1 + w2, o = (i * w1 + (i + 1) * w2) / w;
                sum += (SAMPLE_TEXTURE2D_LOD(_BlitTexture, sampler_LinearClamp, uv + texel * o, 0)
                      + SAMPLE_TEXTURE2D_LOD(_BlitTexture, sampler_LinearClamp, uv - texel * o, 0)) * w;
                total += 2.0 * w;
            }
            return sum / total;
        }
        ENDHLSL

        Pass
        {
            Name "Tilt Shift Horizontal"
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            half4 Frag(Varyings input) : SV_Target { return Blur(input.texcoord, float2(1, 0)); }
            ENDHLSL
        }
        Pass
        {
            Name "Tilt Shift Vertical"
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            half4 Frag(Varyings input) : SV_Target { return Blur(input.texcoord, float2(0, 1)); }
            ENDHLSL
        }
    }
}
