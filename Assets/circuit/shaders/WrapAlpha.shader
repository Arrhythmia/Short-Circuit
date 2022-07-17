	Shader "Wrap/Diffuse Alpha Cutout" {

         Properties {
			 _Color ("Main Color", Color) = (1,1,1,1)
			 _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
			 _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
			 _WrapScale ("Wrap scale", Range(0,1)) = 0.5
			 _WrapAdd ("Wrap add", Range(0,1)) = 0.5
		 }

 
		 SubShader {
			 Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
			 //LOD 200

        CGPROGRAM
		#pragma surface surf WrapLambert alphatest:_Cutoff

		 fixed4 _Color;
		 sampler2D _MainTex;
		 float _WrapScale;
		 float _WrapAdd;
 
		 struct Input {
			 float2 uv_MainTex;
		 };

		half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot (s.Normal, lightDir);
			half diff = NdotL * _WrapScale + _WrapAdd;
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten);
			c.a = s.Alpha;
			return c;
		}
    

		void surf (Input IN, inout SurfaceOutput o) {
			 fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			 o.Albedo = c.rgb;
			 o.Alpha = c.a;
		 }
  
        ENDCG
        }
        Fallback "Diffuse"
    }