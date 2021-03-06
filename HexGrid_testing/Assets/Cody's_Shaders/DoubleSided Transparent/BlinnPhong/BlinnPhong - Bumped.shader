﻿Shader "Cody's/DoubleSided Transparent/BlinnPhong/Bumped Specular" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ColorScale("Color Scale", Range(1, 2)) = 1
		_Cutoff("Alpha Cutoff", Range(0, 1)) = 0.5
		_CutoffRange ("Alpha Cutoff Range", Range (0,1)) = 0.1
		_SpecMap("Specularmap", 2D) = "black"{}
		_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1.0)
		_SpecPower("Specular Power", Range(0, 1)) = 0.5
		_BumpMap("Bumpmap", 2D) = "bump"{}
	}
	SubShader {
		Tags { "Queue"="Transparent-20" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200
		
		cull Front
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert alpha
		#pragma exclude_renderers flash

		sampler2D _MainTex;
		half _ColorScale;
		sampler2D _BumpMap;
		sampler2D _SpecMap;
		float _SpecPower;
		half _Cutoff;
		half _CutoffRange;
		
		void vert (inout appdata_full v) {
	    	v.normal *= -1;
	    }

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			half _ColorScale;
			float2 uv_SpecMap;
			float _SpecPower;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half r = _Cutoff * (1 + _CutoffRange * 2) - _CutoffRange;
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _ColorScale;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Specular = _SpecPower;
			o.Gloss = tex2D(_SpecMap, IN.uv_SpecMap).rgb;
			o.Alpha = (tex2D (_MainTex, IN.uv_MainTex).a - r) * (1 / (_CutoffRange));
			}
		ENDCG
		
		cull Back
		CGPROGRAM
		#pragma surface surf BlinnPhong alpha
		#pragma exclude_renderers flash

		sampler2D _MainTex;
		half _ColorScale;
		sampler2D _BumpMap;
		sampler2D _SpecMap;
		float _SpecPower;
		half _Cutoff;
		half _CutoffRange;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			half _ColorScale;
			float2 uv_SpecMap;
			float _SpecPower;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half r = _Cutoff * (1 + _CutoffRange * 2) - _CutoffRange;
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _ColorScale;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Specular = _SpecPower;
			o.Gloss = tex2D(_SpecMap, IN.uv_SpecMap).rgb;
			o.Alpha = (tex2D (_MainTex, IN.uv_MainTex).a - r) * (1 / (_CutoffRange));
			}
		ENDCG
	} 
	FallBack "Diffuse"
}
