Shader "Examples/Silhouette"
{
    Properties
    {
       _ForegroundColor("Foreground Color", Color) = (1,1,1,1)
       _BackgroundColor("Background Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent" 
            "Queue" = "Transparent"
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
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            struct appdata
            {
                float4 positionOS : POSITION;
            };

            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float4 positionSS : TEXCOORD0;
            };
            

            CBUFFER_START(UnityPerMaterial)
                float4 _ForegroundColor;
                float4 _BackgroundColor;
            CBUFFER_END
           
            v2f vert (appdata v)
            {
                v2f o;
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                o.positionSS = ComputeScreenPos(o.positionCS);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 screenUVs = i.positionSS.xy / i.positionSS.w;
                float rawDepth = SampleSceneDepth(screenUVs);
                float scene01Depth = Linear01Depth(rawDepth, _ZBufferParams);

                float4 output = lerp(_ForegroundColor, _BackgroundColor, scene01Depth);

                return output;
                
            }
            ENDHLSL
        }
        
        Pass
        {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}
            
            ZWrite On
            ColorMask 0
            
            HLSLPROGRAM
            
                #pragma vertex DepthOnlyVertex
                #pragma fragment DepthOnlyFragment
            
                #include "Packages/com.unity.render-pipelines.universal/Shaders/UnlitInput.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/Shaders/DepthOnlyPass.hlsl"
            
                #pragma multi_compile_instancing
                #pragma multi_compile _ DOTS_INSTANCING_ON
            
            ENDHLSL
        }
    }
}
