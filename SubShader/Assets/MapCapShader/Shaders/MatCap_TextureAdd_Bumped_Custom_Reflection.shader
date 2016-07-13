// MatCap Shader, (c) 2013,2014 Jean Moreno

Shader "MatCap/Bumped/Textured Add Custom Reflection"
{
	Properties
	{
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_MatCap ("MatCap (RGB)", 2D) = "white" {}
		_ReflectionMask ("Reflection Mask", 2D) = "white" {}
		_ReflectionValue ("Reflection Value", Range(0.0, 5.0)) = 1.0
		_ReflectionCap ("Reflection Cap (RGB)", 2D) = "white" {}
	}
	
	Subshader
	{
		Tags { "RenderType"="Opaque" }
		
		Pass
		{
			Tags { "LightMode" = "Always" }
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				struct v2f
				{
					float4 pos	: SV_POSITION;
					float2 uv 	: TEXCOORD0;
					float2 uv_bump : TEXCOORD1;
					
					float3 c0 : TEXCOORD2;
					float3 c1 : TEXCOORD3;
					
					float2 uv_reflectionMask : TEXCOORD4;
				};
				
				uniform float4 _MainTex_ST;
				uniform float4 _BumpMap_ST;
				uniform float4 _ReflectionMask_ST;
				
				v2f vert (appdata_tan v)
				{
					v2f o;
					o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.uv_bump = TRANSFORM_TEX(v.texcoord,_BumpMap);
					
					TANGENT_SPACE_ROTATION;
					o.c0 = mul(rotation, UNITY_MATRIX_IT_MV[0].xyz);
					o.c1 = mul(rotation, UNITY_MATRIX_IT_MV[1].xyz);
					
					o.uv_reflectionMask = TRANSFORM_TEX(v.texcoord,_ReflectionMask);
					
					return o;
				}
				
				uniform float4 _Color;
				uniform sampler2D _MainTex;
				uniform sampler2D _BumpMap;
				uniform sampler2D _MatCap;
				uniform sampler2D _ReflectionMask;
				uniform float _ReflectionValue;
				uniform sampler2D _ReflectionCap;
				
				fixed4 frag (v2f i) : COLOR
				{
					fixed4 tex = tex2D(_MainTex, i.uv);
					
					fixed3 normals = UnpackNormal(tex2D(_BumpMap, i.uv_bump));
					half2 capCoord = half2(dot(i.c0, normals), dot(i.c1, normals));
					float4 mc = tex2D(_MatCap, capCoord*0.5+0.5);
					float4 rc = tex2D(_ReflectionCap, capCoord*0.5+0.5);
					
					fixed4 reflectionMaskTex = tex2D(_ReflectionMask, i.uv_reflectionMask);
					if (reflectionMaskTex.r == 0.0 && reflectionMaskTex.g == 0.0 && reflectionMaskTex.b == 0.0)
					{
						return _Color * (tex + (mc*2.0)-1.0);
					}
					else
					{
						//return _Color * (tex + (mc*2.0) - 1.0);
						//return (_Color * (tex + (mc*2.0) - 1.0)) * _ReflectionValue;
						//return (_Color * (tex + (rc*2.0) - 1.0));
						return (_Color * (tex + (mc*2.0) - 1.0 + (rc*2.0) - 1.0));
					}
				}
			ENDCG
		}
	}
	
	Fallback "VertexLit"
}