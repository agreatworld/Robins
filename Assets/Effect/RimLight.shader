Shader "Custom/RimLight"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
       // 描边相关属性
       [Toggle(_ShowOutline)] _ShowOutline("Show Outline", int) = 0
        _OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineWidth("Outline Width", range(1, 9)) = 4

           // 内发光相关属性
            [Toggle(_ShowInnerGlow)] _ShowInnerGlow("Show Inner Glow", int) = 0
            _InnerGlowColor("Inner Glow Color", Color) = (1, 1, 1, 1)
            [HideInInspector]_InnerGlowIntensity("Inner Glow Intensity", range(0, 1)) = 1
           [HideInInspector] _InnerGlowDensity("Inner Glow Density", float) = 10
               _InnerGlowWidth("Inner Glow Width", range(1, 4)) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv[9] : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            int _ShowOutline;
            float4 _OutlineColor;
            int _OutlineWidth;
            float4 _MainTex_TexelSize;



            int _ShowInnerGlow;
            float4 _InnerGlowColor;
            float _InnerGlowIntensity;
            int _InnerGlowDensity;
            float _InnerGlowWidth;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float2 center = v.uv;
                float2 texel = float2(_MainTex_TexelSize.x, _MainTex_TexelSize.y);
                o.uv[0] = center + float2(-texel.x, texel.y);
                o.uv[1] = center + float2(0, texel.y);
                o.uv[2] = center + float2(texel.x, texel.y);
                o.uv[3] = center + float2(-texel.x, 0);
                o.uv[4] = center;
                o.uv[5] = center + float2(texel.x, 0);
                o.uv[6] = center + float2(-texel.x, -texel.y);
                o.uv[7] = center + float2(0, -texel.y);
                o.uv[8] = center + float2(texel.x, -texel.y);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float CalculateRoundAlphaSum(float2 origin, float radius) {
                float deltaAngle = 360 / _InnerGlowDensity;
                float sum = 0;
                for (int j = 0; j < _InnerGlowDensity; ++j) {
                    float2 p = origin + radius * float2(cos(deltaAngle * j), sin(deltaAngle * j));
                    sum += tex2D(_MainTex, p).a;
                }
                return sum;

            }

            float TexelGroupAlphaSum(v2f i) {
                float sum = 0;
                for (int j = 0; j < 9; ++j) {
                    sum += tex2D(_MainTex, i.uv[j]).a;
                }
                return sum;
            }


            fixed4 frag(v2f i) : SV_Target
            {
                float4 result = float4(0, 0, 0 ,1);
                float4 originalColor = tex2D(_MainTex, i.uv[4]);
                float4 outlineColor = float4(0, 0, 0, 1);
                bool needOutline = false;
                if (_ShowOutline) {
                    float alphaSum = TexelGroupAlphaSum(i);
                    if (alphaSum < _OutlineWidth) {
                        outlineColor = _OutlineColor;
                        needOutline = true;
                    }
                }
                float4 innerGlowColor = float4(0, 0, 0, 1);
                if (_ShowInnerGlow) {
                    const float radius = 0.01;
                    float roundAlphaSum = CalculateRoundAlphaSum(i.uv[4], _InnerGlowIntensity * radius);
                    float innerGlowAlpha = 1 - roundAlphaSum / _InnerGlowDensity;
                    innerGlowAlpha *= innerGlowAlpha;
                    
                    innerGlowColor += lerp(originalColor, float4(_InnerGlowColor.rgb, innerGlowAlpha), innerGlowAlpha * _InnerGlowWidth);
                }
                if (needOutline) {
                    return outlineColor;
                }
                return innerGlowColor + outlineColor;
            }
            ENDCG
        }
    }
}
