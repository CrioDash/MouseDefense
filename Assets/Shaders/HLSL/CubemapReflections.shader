Shader "Examples/CubemapReflections"
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
                float3 reflectWS: TEXCOORD0;
            };

            samplerCUBE _Cubemap;

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float4 _BaseTex_ST;
            CBUFFER_END
           
            v2f vert (appdata v)
            {
                v2f o;
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                float3 normalWS = TransformObjectToWorldNormal(v.normalOS);
                float3 positionWS = mul(unity_ObjectToWorld, v.positionOS).xyz;
                float3 viewDirWS = GetWorldSpaceNormalizeViewDir(positionWS);
                o.reflectWS = reflect(-viewDirWS, normalWS);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 cubemapSample = texCUBE(_Cubemap, i.reflectWS);
                return cubemapSample;
            }
            ENDHLSL
        }
    }
}
