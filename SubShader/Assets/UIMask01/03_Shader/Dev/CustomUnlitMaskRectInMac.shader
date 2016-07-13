Shader "Custom/Unlit/MaskRectInMac" {
	Properties {
		_Color ("Color", Color) = (0.26, 0.19, 0.16, 0.0)
		_CutPos ("Cut pos", Vector) = (100.0, 100.0, 0.0, 0.0)
		_CutWidth ("cutWidth", Float) = 100
		_CutHeight ("cutHeight", Float) = 100	
	}
	
	SubShader {
		
		LOD 200
		
		Tags
		{
			"Queue" = "Alphatest"
			"IgnoreProjector" = "false"
			"RenderType" = "Alphatest"
		}
		
		Pass
		{
			Cull Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			
			float4 _Color;
			float4 _CutPos;
			float _CutWidth;
			float _CutHeight;
			
//			struct appdata {
//				float4 vertex : POSITION;
//				float3 normal : NORMAL;
//			};
//
//			struct v2f {
//				float4 pos : SV_POSITION;
//				UNITY_FOG_COORDS(0)
//				fixed4 color : COLOR;
//			};
			
			struct appdata_t
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 uvgrab : TEXCOORD0;
				float2 uvflag : TEXCOORD1;
				fixed4 color : COLOR;
			};
			
//			v2f vert(appdata v) {
//				v2f o;
//				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//
//				float3 norm   = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal));
//				float2 offset = TransformViewToProjection(norm.xy);
//
//				o.pos.xy += offset * o.pos.z * _Outline;
//				o.color = _OutlineColor;
//				UNITY_TRANSFER_FOG(o,o.pos);
//				return o;
//			}

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;	// Use this.
				#endif
				
//				o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;

				if (o.vertex.y == 0)
				{
					o.uvflag.x = 1;
				}
				else
				{
					o.uvflag.x = 0;
				}
				
				
				o.uvgrab.x = (o.vertex.x + o.vertex.w) * 0.5;
				o.uvgrab.y = (o.vertex.y * scale + o.vertex.w) * 0.5;
				
				o.uvgrab.zw = o.vertex.zw;
				
				o.color.rgb = _Color.rgb;
				o.color.a = _Color.a;
				
				return o;
			}
				
			fixed4 frag (v2f IN) : COLOR
			{
//				if (IN.vertex.x >= _CutPos.x - (_CutWidth * 0.5) &&
//					IN.vertex.x <= _CutPos.x + (_CutWidth * 0.5) &&
//					IN.vertex.y >= _CutPos.y - (_CutHeight * 0.5) &&
//					IN.vertex.y <= _CutPos.y + (_CutHeight * 0.5))
//				if (IN.vertex.x >= _CutPos.x - 0.5 &&
//					IN.vertex.x <= _CutPos.x + 0.5 &&
//					IN.vertex.y >= _CutPos.y - 0.5 &&
//					IN.vertex.y <= _CutPos.y + 0.5)

				
//				if (IN.uvflag.x == 1.0)
//				{
//					return IN.color;
//				}

				if (IN.uvgrab.x >= _CutPos.x - 0.1 &&
					IN.uvgrab.x <= _CutPos.x + 0.1 &&
					IN.uvgrab.y >= _CutPos.y - 0.1 &&
					IN.uvgrab.y <= _CutPos.y + 0.1)


//				if (IN.uvgrab.x > 0.5 && IN.uvgrab.y > 0.5)
				{
					half4 col;
					col.rgb = half3(0.0, 0.0, 0.0);
					col.a = 0.0;
					return col;
				}
				
				return IN.color;
			}
			
			ENDCG
		}
	}
	
	Fallback off
}
