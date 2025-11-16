Shader "Hidden/UnderwaterEffect"
{
    Properties { _DepthStrength("Depth Strength", Range(0,1)) = 0.5 }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes { float4 positionOS : POSITION; float2 uv : TEXCOORD0; };
            struct Varyings { float4 positionHCS : SV_POSITION; float2 uv : TEXCOORD0; };

            Varyings Vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformVertex(v.positionOS);
                o.uv = v.uv;
                return o;
            }

            TEXTURE2D(_CameraOpaqueTexture); SAMPLER(sampler_CameraOpaqueTexture);
            float _DepthStrength;

            half4 Frag(Varyings i) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, i.uv);
                float depth = LinearEyeDepth(SampleSceneDepth(i.uv), _ZBufferParams);
                float fog = saturate(depth * _DepthStrength * 0.05);
                col.rgb = lerp(col.rgb, float3(0.25, 0.6, 0.7), fog);
                return col;
            }
            ENDHLSL
        }
    }
}
