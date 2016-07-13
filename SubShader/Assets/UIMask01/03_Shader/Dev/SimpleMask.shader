Shader "Custom/SimpleMask" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
	
		CGPROGRAM
		
		#pragma surface surf Lambert
		
		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {
			if (IN.screenPos.x > 1 && IN.screenPos.x < 2 &&
				IN.screenPos.y > 1 && IN.screenPos.y < 2)
			{
				o.Albedo = 0.5;
			}
		}
		
		ENDCG
	}
	Fallback "Diffuse"
}
