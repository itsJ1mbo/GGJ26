Shader "Custom/Stencil_VisibleOnlyOn2_VertexColor"
{
    Properties
    {
        [HDR] _BaseColor("Color Tint", Color) = (1,1,1,1)
        _MainTex("Sprite Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent" 
            "Queue"="Transparent" 
            "RenderPipeline"="UniversalPipeline"
        }

        Stencil
        {
            Ref 2
            Comp Equal
            Pass Keep
        }

        Blend One OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR; // Color del Sprite Renderer
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _BaseColor;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                // Pasamos el color del vértice (Sprite Renderer) al fragment
                output.color = input.color * _BaseColor; 
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                half4 texColor = tex2D(_MainTex, input.uv);
                
                // Multiplicamos el color de la textura por el color del vértice
                // Esto hará que el color que elijas en el Sprite Renderer se vea
                return texColor * input.color;
            }
            ENDHLSL
        }
    }
}