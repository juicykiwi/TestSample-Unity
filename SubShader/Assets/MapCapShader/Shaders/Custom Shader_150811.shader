Shader "Custom/MultiShader" {
	Properties {
		_TintColor ("Tint Color", Color) = (0.5, 0.5, 0.5, 0)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_SelfIllumination ("Self Illumination", Range(0.0,2.0)) = 1.0
		_AffectingLight ("Affecting Light ", Range(0.0,20.0)) = 10.0
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	    _BumpMap ("Bumpmap", 2D) = "bump" {}
	    _RimColor ("Rim Color", Color) = (1,1,1,1)
	    _RimPower ("Rim Power", Range(0.1,8.0)) = 4.0
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

		}

	SubShader {
		Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Opaque" }
		LOD 400
		cull off

		CGPROGRAM
		#pragma surface surf BlinnPhong alphatest:_Cutoff

		sampler2D _MainTex;
		fixed4 _TintColor;
		half _Shininess;
		half _AffectingLight;
		uniform fixed _SelfIllumination;

		struct Input {
			half2 uv_MainTex;
			half2 uv_BumpMap;
			half3 viewDir;
		};	
		sampler2D _BumpMap;
		half4 _RimColor;
		half _RimPower;

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		
			o.Albedo = tex / _AffectingLight;
			
			
			o.Gloss = tex.a;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			half3 selfIllumination = (tex * tex.a * _SelfIllumination).rgb * _TintColor.rgb;
			o.Emission = _RimColor.rgb * pow (rim, _RimPower) + selfIllumination;
			o.Alpha = tex.a * tex.a;
			o.Specular = _Shininess;
			}

	      ENDCG
	} 

    Fallback "Diffuse"

  }


