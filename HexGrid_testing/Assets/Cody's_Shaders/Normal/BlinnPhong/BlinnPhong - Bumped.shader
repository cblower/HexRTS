Shader "Cody's/Opaque/BlinnPhong/Bumped Specular" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ColorScale("Color Scale", Range(1, 2)) = 1
		_SpecMap("Specularmap", 2D) = "black"{}
		_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1.0)
		_SpecPower("Specular Power", Range(0, 1)) = 0.5
		_BumpMap("Bumpmap", 2D) = "bump"{}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong

		sampler2D _MainTex;
		half _ColorScale;
		sampler2D _BumpMap;
		sampler2D _SpecMap;
		float _SpecPower;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			half _ColorScale;
			float2 uv_SpecMap;
			float _SpecPower;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _ColorScale;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Specular = _SpecPower;
			o.Gloss = tex2D(_SpecMap, IN.uv_SpecMap).rgb;
			}
		ENDCG
	} 
	FallBack "Diffuse"
}
