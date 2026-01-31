Shader "Custom/MeltDualPhase"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _FillLevel ("Fill Level", Range(-0.3, 1.3)) = -0.3
        _EdgeSize ("Edge Softness", Range(0.0, 0.2)) = 0.05
        _ShowImage ("Show Background Image", Float) = 1
    }

    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off 
        Lighting Off 
        ZWrite Off 
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _FillLevel;
            float _EdgeSize;
            float _ShowImage;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 1. Preparamos el color del juego
                fixed4 gameColor = tex2D(_MainTex, i.uv);
                gameColor *= _ShowImage; // Si es 0, se vuelve transparente

                // 2. Preparamos el color del líquido (Blanco)
                fixed4 liquidColor = fixed4(1, 1, 1, 1);

                // 3. Calculamos la altura con ruido
                float noise = tex2D(_NoiseTex, i.uv * float2(1.0, 0.5)).r;
                float pixelHeight = i.uv.y + (noise * 0.1); 

                // 4. MÁSCARA (La lógica corregida)
                // Calculamos qué parte es IMAGEN (arriba del nivel) y qué parte es LÍQUIDO (abajo)
                // Si pixelHeight > FillLevel -> devuelve 1 (Imagen)
                // Si pixelHeight < FillLevel -> devuelve 0 (Líquido)
                float mask = smoothstep(_FillLevel, _FillLevel + _EdgeSize, pixelHeight);

                // 5. Mezclar: Donde la máscara es 1 ponemos juego, donde es 0 ponemos blanco
                return lerp(liquidColor, gameColor, mask);
            }
        ENDCG
        }
    }
}