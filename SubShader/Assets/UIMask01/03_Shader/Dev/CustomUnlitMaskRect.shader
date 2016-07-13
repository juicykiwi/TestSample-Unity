Shader "Custom/Unlit/MaskRect" {
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
			
			float4 _Color;
			float4 _CutPos;
			float _CutWidth;
			float _CutHeight;
			
			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
			
			v2f o;
			
			v2f vert (appdata_t v)
			{
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;
				o.color.rgb = _Color.rgb;
				o.color.a = _Color.a;
				return o;
			}
				
			fixed4 frag (v2f IN) : COLOR
			{
				if (IN.vertex.x >= _CutPos.x - (_CutWidth * 0.5) &&
					IN.vertex.x <= _CutPos.x + (_CutWidth * 0.5) &&
					IN.vertex.y >= _CutPos.y - (_CutHeight * 0.5) &&
					IN.vertex.y <= _CutPos.y + (_CutHeight * 0.5))
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
	
	FallBack "Diffuse"
}
