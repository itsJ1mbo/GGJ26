Shader "Custom/StencilOnly_Invisible"
{
    Properties
    {
        // Necesitamos la textura para que el Stencil respete la forma del círculo (Alpha Clipping)
        _MainTex("Alpha Mask (Sprite)", 2D) = "white" {}
        _Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent" 
            "Queue"="Transparent-1" // Se dibuja antes que los objetos normales
            "RenderPipeline"="UniversalPipeline"
        }

        // --- EL TRUCO DE INVISIBILIDAD ---
        ColorMask 0    // 0 significa que NO escribe en los canales R, G, B ni A.
        ZWrite Off     // No escribe en la profundidad.

        Stencil
        {
            Ref 1
            Comp Always
            Pass IncrSat // Suma +1 al stencil
        }

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
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Cutoff;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // Leemos el alpha de la textura para que el stencil tenga forma de círculo
                half4 texColor = tex2D(_MainTex, input.uv);
                
                // Si el pixel es muy transparente, no marcamos el stencil
                clip(texColor.a - _Cutoff);

                return half4(0,0,0,0);
            }
            ENDHLSL
        }
    }
}