// The sibling of Athanor/Sprite Glow for batched sparks (SparkMesh) and particles: unlit, adds its colour to what is
// behind it, vertex colour times _Intensity. Vertex colours are floats on a SparkMesh, so HDR colours past 1 reach the
// bloom as they did on SpriteRenderers. With CENTER_SNAP (ParticleSystem with the Center vertex stream in
// TEXCOORD0.zw) each quad is moved so its centre lands on a whole pixel; SparkMesh snaps on the CPU instead.
Shader "Athanor/Particle Glow"
{
    Properties
    {
        [MainTexture] _MainTex ("Texture", 2D) = "white" {}
        _Intensity ("Intensity", Float) = 1
    }

    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" "IgnoreProjector" = "True" }

        Blend SrcAlpha One, One OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #pragma vertex Vert
            #pragma fragment Frag
            #pragma multi_compile_local _ CENTER_SNAP

            struct Attributes
            {
                float3 positionOS : POSITION;
                float4 color : COLOR;
                float4 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                half _Intensity;
            CBUFFER_END

            Varyings Vert(Attributes input)
            {
                Varyings o;
                float3 p = input.positionOS;
                #if defined(CENTER_SNAP)
                    float2 centre = input.uv.zw;
                    p.xy = round(centre) + (p.xy - centre);
                #endif
                o.positionCS = TransformObjectToHClip(p);
                o.color = input.color;
                o.color.rgb *= _Intensity;
                o.uv = input.uv.xy;
                return o;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv) * input.color;
            }
            ENDHLSL
        }
    }
}
