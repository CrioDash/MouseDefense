Shader "Examples/CubemapExample"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _Cubemap("Cubemap", CUBE) = "white" {}
    }
    SubShader
    {
        Tags { 
            "RenderType"="Opaque" 
            "Queue" = "Geometry"
            "Render Pipeline" = "UniversalPipeline"
        }

        Pass
        {
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float3 normalWS: TEXCOORD0;
            };

            samplerCUBE _Cubemap;

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float4 _BaseTex_ST;
            CBUFFER_END
           
            v2f vert (appdata v)
            {
                v2f o;
                o.positionCS = TransformObjectToHClip(v.positionOS);
                o.normalWS = TransformObjectToWorldNormal(v.normalOS);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 cubemapSample = texCUBE(_Cubemap, i.normalWS);
                return cubemapSample;
            }
            ENDHLSL
        }
    }
}
