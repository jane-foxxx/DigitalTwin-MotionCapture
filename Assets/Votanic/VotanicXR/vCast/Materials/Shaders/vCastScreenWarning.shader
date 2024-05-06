Shader "VotanicXR/ScreenWarning"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DetectDist("Proxi Detection Distance", Float) = 1
		_ShiftSpeed("Shift Speed", Float) = 1
		_BlinkSpeed("Blink Speed", Float) = 4
		_BlinkAlphaFrom("Blink Starting Alpha", Range(0, 1)) = 0.5
		_BlinkAlphaTo("Blink Ending Alpha", Range(0, 1)) = 1
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		ZTest off

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
				float4 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _ProxiTarget;
			float _DetectDist;
			float _ShiftSpeed;
			float _BlinkSpeed;
			float _BlinkAlphaFrom;
			float _BlinkAlphaTo;

			int _ProxiTargetLength = 0;
			float4 _ProxiTargets[64];

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv + float2(0, _Time.x * _ShiftSpeed));
				float magnitude = 4;
				float strength = 0;
				for (int j = 0; j < _ProxiTargetLength; j++)
				{
					strength += clamp((1 - length(i.worldPos.xyz - _ProxiTargets[j].xyz) / _DetectDist) * magnitude, 0, 1);
				}
				strength = clamp(strength, 0, 1);
				float blink = lerp(_BlinkAlphaFrom, _BlinkAlphaTo, (sin(_Time.y * _BlinkSpeed) + 1) / 2);
				col.a = strength * col.a * blink;

				return col;
			}
			ENDCG
		}
	}
}
