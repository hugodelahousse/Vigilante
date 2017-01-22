Shader "Custom/CRTImageEffect" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_MaskTex("Mask texture", 2D) = "white" {}
		_NoiseTex("Noise texture", 2D) = "white" {}
		_maskBlend("Mask blending", Float) = 0.5
		_maskExp("Mask exponent", Float) = 0.2
		_noiseSampleScale("Noise Sample Scale", Float) = 80
	}
		SubShader{
		Pass{
		CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform sampler2D _MaskTex;
		uniform sampler2D _NoiseTex;

		fixed _maskBlend;
		fixed _maskExp;
		fixed _noiseSampleScale;

		fixed4 frag(v2f_img i) : COLOR{
			fixed2 uvs = (i.uv * 2) - fixed2(1, 1);

			fixed len = length(uvs);

			fixed4 noise = tex2D(_NoiseTex, i.uv * _noiseSampleScale + fixed2(_Time.z, 0));

			uvs = uvs * pow(len, _maskExp);
			uvs = (uvs + fixed2(1, 1)) / 2;
			fixed4 base = tex2D(_MainTex, uvs);
			len = pow(len, 1.3) * 0.3 + 0.1;
			return base * (1 - _maskBlend * len) + noise * _maskBlend * len;
		}
		ENDCG
		}
	}
}