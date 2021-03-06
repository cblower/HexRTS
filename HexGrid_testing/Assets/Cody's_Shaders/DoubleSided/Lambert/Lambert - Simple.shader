﻿Shader "Cody's/DoubleSided/Lambert/Simple" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ColorScale("Color Scale", Range(1, 2)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		cull Front
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		half _ColorScale;
		
		void vert (inout appdata_full v) {
	    	v.normal *= -1;
	    }
	    
		struct Input {
			float2 uv_MainTex;
			half _ColorScale;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _ColorScale;
		}
		ENDCG
		
		cull Back
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		half _ColorScale;
	    
		struct Input {
			float2 uv_MainTex;
			half _ColorScale;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _ColorScale;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
