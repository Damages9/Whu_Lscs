// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ShaderForTest"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white"{}									//主纹理
	_EdgeAlphaThreshold("Edge Alpha Threshold", Float) = 1.0					//边界透明度和的阈值
		_EdgeColor("Edge Color", Color) = (0,0,0,1)									//边界颜色
		_EdgeDampRate("Edge Damp Rate", Float) = 2									//边缘渐变的分母
		_OriginAlphaThreshold("OriginAlphaThreshold", range(0.1, 1)) = 0.2			//原始颜色透明度剔除的阈值
		[Toggle(_ShowOutline)] _DualGrid("Show Outline", Int) = 0					//Toggle开关来控制是否显示边缘
	}

		SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
	{
		Ztest Always Cull Off ZWrite Off
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#pragma shader_feature _ShowOutline
#include "UnityCG.cginc"
		sampler2D _MainTex;
	half4 _MainTex_TexelSize;
	fixed _EdgeAlphaThreshold;
	fixed4 _EdgeColor;
	float _EdgeDampRate;
	float _OriginAlphaThreshold;

	struct v2f
	{
		float4 vertex : SV_POSITION;
		float2 uv[9] : TEXCOORD0;
	};

	half CalculateAlphaSumAround(v2f i)
	{
		half texAlpha;
		half alphaSum = 0;
		for (int it = 0; it < 9; it++)
		{
			texAlpha = tex2D(_MainTex, i.uv[it]).w;
			alphaSum += texAlpha;
		}

		return alphaSum;
	}

	v2f vert(appdata_img v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);

		half2 uv = v.texcoord;

		o.uv[0] = uv + _MainTex_TexelSize.xy * half2(-1, -1);
		o.uv[1] = uv + _MainTex_TexelSize.xy * half2(0, -1);
		o.uv[2] = uv + _MainTex_TexelSize.xy * half2(1, -1);
		o.uv[3] = uv + _MainTex_TexelSize.xy * half2(-1, 0);
		o.uv[4] = uv + _MainTex_TexelSize.xy * half2(0, 0);
		o.uv[5] = uv + _MainTex_TexelSize.xy * half2(1, 0);
		o.uv[6] = uv + _MainTex_TexelSize.xy * half2(-1, 1);
		o.uv[7] = uv + _MainTex_TexelSize.xy * half2(0, 1);
		o.uv[8] = uv + _MainTex_TexelSize.xy * half2(1, 1);

		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
#if defined(_ShowOutline)
		half alphaSum = CalculateAlphaSumAround(i);
	float isNeedShow = alphaSum > _EdgeAlphaThreshold;
	float damp = saturate((alphaSum - _EdgeAlphaThreshold) * _EdgeDampRate);
	fixed4 orign = tex2D(_MainTex, i.uv[4]);
	float isOrigon = orign.a > _OriginAlphaThreshold;
	fixed3 finalColor = lerp(_EdgeColor.rgb, orign.rgb, isOrigon);

	return fixed4(finalColor.rgb, isNeedShow * damp);
#endif
	
	return tex2D(_MainTex, i.uv[4]);
	}

		ENDCG
	}
	}
}
