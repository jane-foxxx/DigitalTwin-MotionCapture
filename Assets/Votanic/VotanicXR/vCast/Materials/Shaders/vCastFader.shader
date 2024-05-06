Shader "VotanicXR/Fader" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_OnTopAlpha("OnTopAlpha", float) = 0.5
		[PerRendererData]
		_Position("Position", Vector) = (0.0, 0.0, 0.0)
		[PerRendererData]
		_Distance("Distance", float) = 0.01
	}

	SubShader{
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		//Cull Off
		Blend SrcAlpha OneMinusSrcAlpha

	// Include functions common to both passes
	CGINCLUDE
		struct appdata_t {
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};
		struct v2f {
			float4 vertex : SV_POSITION;
			fixed4 color : COLOR;
			half2 texcoord : TEXCOORD0;
		};

		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"
		sampler2D _MainTex;
		float4 _MainTex_ST;
		float4 _Color;
		float _OnTopAlpha;
		bool _Text;
		float3 _Position;
		float _Distance;

		v2f vert(appdata_t v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.color = v.color * _Color;
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			return o;
		}
	ENDCG

		// Pass for fully visible parts of the object
		Pass {
			ZTest LEqual
			CGPROGRAM
				fixed4 frag(v2f i) : COLOR
				{
					fixed4 col;
					col = tex2D(_MainTex, i.texcoord) * i.color;
					clip(col.a - 0.01);
					col.a = col.a * step(length(_Position - _WorldSpaceCameraPos), _Distance);
					return col;
				}
			ENDCG
		}

		// Pass for obscured parts of the object
		Pass {
			ZTest Greater
			ZWrite Off
			CGPROGRAM
				fixed4 frag(v2f i) : COLOR
				{
					fixed4 col;
					col = tex2D(_MainTex, i.texcoord) * i.color;
					col.a *= _OnTopAlpha;
					clip(col.a - 0.01);
					col.a = col.a * step(length(_Position - _WorldSpaceCameraPos), _Distance);
					return col;
				}
			ENDCG
		}
	}
}