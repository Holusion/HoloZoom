// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/CameraFlop"
{
 HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        int _FlopH, _FlopV;

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float2 uv = i.texcoord;
            if(_FlopH) {
                uv.x = 1- uv.x;
            }
            if(_FlopV) {
                uv.y = 1 - uv.y;
            }
            float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
            return color;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}
