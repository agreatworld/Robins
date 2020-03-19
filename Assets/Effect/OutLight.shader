Shader "Custom/OutLight"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_LightColor("Light Color", Color) = (1, 1, 1, 1)
		_GlowColor("Glow Colot", COlor) = (1, 1, 1, 1)
	}
		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

			float4 _LightColor;
			float4 _GlowColor;
			float2 _MainTex_TexelSize;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v) {
				v2f o;
				o.uv = v.uv;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			sampler2D _MainTex;

			float getRadio(float2 p) {
				float sum = 0;
				float count = 10;
				float length = 0.017;
				float deltaAngle = 360 / count;
				[unroll] for (int i = 0; i < count; ++i) {
					sum += tex2D(_MainTex, p + length * float2(cos(i * deltaAngle), sin(i * deltaAngle))).a;
				}
				float radio = sum / count;
				return radio;
			}


			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

			float radio = getRadio(i.uv);
			float4 result = col;
			if (col.a == 0) {
				result = _LightColor;
				return result;
			}
			if (radio < 0.66) {
				result = lerp(col, _GlowColor,1 - radio);
				if (radio < 0.56)
				result = lerp(col, _LightColor,1 - radio);
			}
			return result;
			}
		ENDCG
	}
		}
}
