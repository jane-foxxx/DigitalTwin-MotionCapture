// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/VotanicXR/Panoramic"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_RenderSpan("Render Span", Float) = 0
		_DisplaySpan("Display Span", Float) = 0
		_KeepAspectRatio("Keep Aspect Ratio", Range(0, 1)) = 0
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _RenderSpan;
			float _DisplaySpan;
			float _KeepAspectRatio;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// declare variables
				float azim, height, scaleFactor, aspectRatioCompenstaion;
				float get_s, get_t;
				fixed4 col;

				// calculate scale factor needed for render and display angle differences
				scaleFactor = tan(_DisplaySpan / 2) / tan(_RenderSpan / 2);

				// do if aspect ratio compansation is necessary
				if (_KeepAspectRatio > 0.5)
				{
					aspectRatioCompenstaion = _DisplaySpan / 2 / tan(_DisplaySpan / 2);
				}
				else
				{
					aspectRatioCompenstaion = 1;
				}

				// compute the expanded cylinderical unwarp coordinates
				azim = (i.uv.x - 0.5) * _DisplaySpan;
				height = (i.uv.y - 0.5) * scaleFactor;

				get_s = tan(azim) / tan(_RenderSpan / 2.0) / 2.0 + 0.5;
				get_t = height / cos(azim) * aspectRatioCompenstaion + 0.5;

				// retrieve target value from calculated coordinates, create horizontal black border at out of bound positions
				//if (abs(get_t - 0.5) > 0.5) // change to this conditional to see the wrapped border, debug purpose only
				if (abs(i.uv.y - 0.5) > 0.5 * cos(_DisplaySpan / 2) / aspectRatioCompenstaion / scaleFactor)
				{
					col = fixed4(0.0, 0.0, 0.0, 1.0);
				}
				else
				{
					col = tex2D(_MainTex, float2(get_s, get_t));
				}
				return col;
			}
			ENDCG
		}
	}
}
