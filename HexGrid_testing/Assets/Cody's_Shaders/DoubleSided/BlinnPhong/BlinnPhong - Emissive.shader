Shader "Cody's/DoubleSided/BlinnPhong/Emissive" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ColorScale("Color Scale", Range(1, 2)) = 1
		_SpecMap("Specularmap", 2D) = "black"{}
		_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1.0)
		_SpecPower("Specular Power", Range(0, 1)) = 0.5
		_EmitMap("Emissive Map", 2D) = "black"{}
		_EmitPower("Emit Power", Range(0, 2)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		cull Front
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert

		sampler2D _MainTex;
		half _ColorScale;
		sampler2D _SpecMap;
		half _SpecPower;
		sampler2D _EmitMap;
		half _EmitPower;
		
		void vert (inout appdata_full v) {
	    	v.normal *= -1;
	    }

		struct Input {
			float2 uv_MainTex;
			half _ColorScale;
			float2 uv_SpecMap;
			half _SpecPower;
			float2 uv_EmitMap;
			half _EmitPower;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _ColorScale;
			o.Specular = _SpecPower;
			o.Gloss = tex2D(_SpecMap, IN.uv_SpecMap).rgb;
			o.Emission = tex2D(_EmitMap, IN.uv_EmitMap).rgb * _EmitPower;
		}
		ENDCG
		
		cull Back
		CGPROGRAM
		#pragma surface surf BlinnPhong

		sampler2D _MainTex;
		half _ColorScale;
		sampler2D _SpecMap;
		half _SpecPower;
		sampler2D _EmitMap;
		half _EmitPower;

		struct Input {
			float2 uv_MainTex;
			half _ColorScale;
			float2 uv_SpecMap;
			half _SpecPower;
			float2 uv_EmitMap;
			half _EmitPower;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _ColorScale;
			o.Specular = _SpecPower;
			o.Gloss = tex2D(_SpecMap, IN.uv_SpecMap).rgb;
			o.Emission = tex2D(_EmitMap, IN.uv_EmitMap).rgb * _EmitPower;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
