Shader "Cody's/Opaque/Lambert/Emissive" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ColorScale("Color Scale", Range(1, 2)) = 1
		_EmitMap("Emissive Map", 2D) = "black"{}
		_EmitPower("Emit Power", Range(0, 2)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		half _ColorScale;
		sampler2D _EmitMap;
		float _EmitPower;

		struct Input {
			float2 uv_MainTex;
			half _ColorScale;
			float2 uv_EmitMap;
			float _EmitPower;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _ColorScale;
			o.Emission = tex2D(_EmitMap, IN.uv_EmitMap).rgb * _EmitPower;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
