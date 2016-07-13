Shader "Custom/AStarTestTileShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input
        {
			float2 uv_MainTex;
			float3 worldPos ;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

        float RoundValue(float value);

		void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float checkValueX = RoundValue(IN.worldPos.x);
			float checkValueY = RoundValue(IN.worldPos.y);
			float checkValue = checkValueX + (int)checkValueY;
			
			fixed4 c = fixed4(0.0, 0.0, 0.0, 0.0);
			if (fmod(checkValue, 2.0) < 1.0)
			{
				c.rgb = fixed3(0.8, 0.8, 0.8);
				c.a = 1.0;
			}
			else
			{
				c.rgb = fixed3(1.0, 1.0, 1.0);
				c.a = 1.0;
			}
			
			// Albedo comes from a texture tinted by color
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

        float RoundValue(float value)
        {
            if (value + 0.5 >= 0.0)
            {
                return abs(value + 0.5);
            }

            return abs(value + 0.5) + 1.0;
        }
		ENDCG
	} 
	FallBack "Diffuse"
}
