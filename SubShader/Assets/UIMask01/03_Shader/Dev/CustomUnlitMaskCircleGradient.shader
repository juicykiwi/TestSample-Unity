Shader "Custom/Unlit/MaskCircleGradient" {
	Properties {
		_Color ("Color", Color) = (0.26, 0.19, 0.16, 0.0)
		_CutPos ("Cut pos", Vector) = (100.0, 100.0, 0.0, 0.0)
		_Radius ("Radius", Float) = 50.0
		_Gradient ("Gradient", Float) = 50.0
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
			float _Radius;
			float _Gradient;
			
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
//				float x = abs(_CutPos.x - IN.vertex.x);
//				float y = abs(_CutPos.y - IN.vertex.y);
//				
//				float sqrtValue = sqrt(x) + sqrt(y);
//				float lenghtValue = rsqrt(sqrtValue);
//				if (lenghtValue <= _Radius)
//				{
//					half4 col;
//					col.rgb = half3(0.0, 0.0, 0.0);
//					col.a = 0.0;
//					return col;
//				}

				float lengthValue = length(_CutPos.xy - IN.vertex.xy);
				if (lengthValue <= _Radius)
				{
					half4 col;
					col.rgb = half3(0.0, 0.0, 0.0);
					col.a = 0.0;
					return col;
				}
				else if (lengthValue <= _Radius + _Gradient)
				{
					half gradientRate = (lengthValue - _Radius) / _Gradient;
					
					half4 col;
					col.rgb = IN.color.rgb;
					col.a = gradientRate * IN.color.a;
					return col;
				}
				
				return IN.color;
			}
			
			ENDCG
		}
	}
	
	FallBack "Diffuse"
}
