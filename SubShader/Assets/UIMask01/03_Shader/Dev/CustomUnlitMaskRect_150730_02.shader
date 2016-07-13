Shader "Custom/Unlit/MaskRect_150730_02" {
	Properties {
		_Color ("Color", Color) = (0.26, 0.19, 0.16, 0.0)
		_CutScreenPos ("Cut screen pos", Vector) = (0.0, 0.0, 0.0, 0.0)
		_CutWidth ("cutWidth", Float) = 0.1
		_CutHeight ("cutHeight", Float) = 0.1	
	}
	
	SubShader {
		
		LOD 200
		
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members scrPos)
#pragma exclude_renderers d3d11 xbox360
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			
			#include "UnityCG.cginc"
			
			float4 _Color;
			float4 _CutScreenPos;
			float _CutWidth;
			float _CutHeight;
			
			struct appdata_t
			{
				float4 vertexPos : POSITION;
				fixed4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertexSvPos : SV_POSITION;
				float4 scrPos;
				fixed4 color : COLOR;
			};
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertexSvPos = mul(UNITY_MATRIX_MVP, v.vertexPos);
				o.scrPos = ComputeScreenPos(o.vertexSvPos);
				
				o.color = v.color;
				return o;
			}
				
			fixed4 frag (v2f IN) : SV_Target
			{
				_ScreenParams;
			
				float2 wcoord = (IN.scrPos.xy / IN.scrPos.w);
				fixed4 color = _Color;
				
				float x = _ScreenParams.x * wcoord.x;
				float y = _ScreenParams.y * wcoord.y;
				
//				if (wcoord.x >= _CutScreenPos.x - (_CutWidth * 0.5) &&
//					wcoord.x <= _CutScreenPos.x + (_CutWidth * 0.5) &&
//					wcoord.y >= _CutScreenPos.y - (_CutHeight * 0.5) &&
//					wcoord.y <= _CutScreenPos.y + (_CutHeight * 0.5))
				if ((x >= 50 && x <= 100) || (y >= 200 && y <= 300))
				{
					half4 col;
					col.rgb = half3(0.0, 0.0, 0.0);
					col.a = 0.0;
					return col;
				}
				
				return color;
			}
			
			ENDCG
		}
	}
	
	FallBack "Diffuse"
}
