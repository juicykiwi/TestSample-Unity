Shader "Custom/Unlit/MaskRect_150730_03" {
	Properties {
		_Color ("Color", Color) = (0.0, 0.0, 0.0, 0.0)
		_CutScreenPos ("Cut screen pos", Vector) = (0.0, 0.0, 0.0, 0.0)
		_CutWidth ("cutWidth", Float) = 0.0
		_CutHeight ("cutHeight", Float) = 0.0	
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
	
			struct VertOut
			{
				float4 svPos : SV_POSITION;
			#if SHADER_API_OPENGL 
				float4 scrPos;
			#endif
				fixed4 color : COLOR;
			};
			
			VertOut vert (appdata_t v)
			{
				VertOut o;
				o.svPos = mul(UNITY_MATRIX_MVP, v.vertexPos);
				
			#ifndef SHADER_API_OPENGL 
				o.svPos.y = o.svPos.y;
			#else
				// ComputeScreenPos :
				// Function define in the UnityCG.cginc file, 
				// this function returns the screen position for the fragment shader. 
				// The difference with the previous example where a WPOS semantic variable was used, 
				// is that this function is multiplatform and it does not need target 3.0.
				
				o.scrPos = ComputeScreenPos(o.svPos);
			#endif

				o.color = v.color;
				return o;
			}
				
			fixed4 frag (VertOut IN) : SV_Target
			{
				fixed4 retColor = _Color;
				
			#ifndef SHADER_API_OPENGL 
				float x = IN.svPos.x;
				float y = IN.svPos.y;
			#else
				float2 wcoord = (IN.scrPos.xy / IN.scrPos.w);
				
				float x = _ScreenParams.x * wcoord.x;
				float y = _ScreenParams.y * wcoord.y;
			#endif

				if (x >= _CutScreenPos.x - (_CutWidth * 0.5) &&
					x <= _CutScreenPos.x + (_CutWidth * 0.5) &&
					y >= _CutScreenPos.y - (_CutHeight * 0.5) &&
					y <= _CutScreenPos.y + (_CutHeight * 0.5))
				{
					retColor.a = 0.0;
				}
				
				return retColor;
			}
			
			ENDCG
		}
	}
	
	FallBack "Diffuse"
}
