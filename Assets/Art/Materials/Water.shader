Shader "Water" {
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		#pragma target 3.0

		struct Input {
			float4 vertColor;
		};

        fixed4 _Color;

		void vert(inout appdata_full v, out Input o){
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.vertColor = v.color;
			v.vertex.y = (sin(_Time.z + v.vertex.x) + 1) / 200;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color;
		}
		ENDCG
	}
	FallBack "Diffuse"
}