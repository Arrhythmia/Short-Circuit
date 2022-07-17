Shader "Wrap/Diffuse Texture" {

        Properties {
            _MainTex ("Texture", 2D) = "white" {}
			_WrapScale ("Wrap scale", Range(0,1)) = 0.5
			_WrapAdd ("Wrap add", Range(0,1)) = 0.5
        }

        SubShader {
			Tags { "RenderType" = "Opaque" }

        CGPROGRAM
		#pragma surface surf WrapLambert

		float _WrapScale;
		float _WrapAdd;

		half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot (s.Normal, lightDir);
			half diff = NdotL * _WrapScale + _WrapAdd;
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten);
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
		};
    
		sampler2D _MainTex;
			void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
		}
  
        ENDCG
        }
        Fallback "Diffuse"
    }