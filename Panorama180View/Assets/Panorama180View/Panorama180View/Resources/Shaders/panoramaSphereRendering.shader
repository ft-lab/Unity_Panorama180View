//----------------------------------------------------------------.
// 球に対して、Equirectangular360/Equirectangular180 SBS/FishEye180 SBSのステレオパノラマ投影を行う.
//----------------------------------------------------------------.
Shader "Hidden/Panorama180View/panoramaSphereRendering"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Intensity ("Intensity", Range (0, 10.0)) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue"="geometry-100" }

		LOD 100
        ZWrite On

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

            #define UNITY_PI2 (UNITY_PI * 2.0)

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
            float _Intensity;
			int _Mode;			// 0 : Equirectangular360 TopAndBottom、1 : Equirectangular180 SideBySide、2 : FishEye180 SideBySide.

			int _TransitionType;    // 0 : 遷移しない、 1 : フェードイン、 2 : フェードアウト、 3 : ブレンド.
			sampler2D _DestTex;  	// 状態遷移時の移行先のテクスチャ.
			float _TPos;		 	// 遷移の移行値 (0.0 - 1.0).
			float4 _FadeInColor; 	// フェードインの色.
			float4 _FadeOutColor; 	// フェードアウトの色.

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//o.uv = v.uv;

				return o;
			}
			
			/**
			 * テクスチャ上のUV位置を計算.
			 */
			float2 calcUV (float2 _uv) {
                float2 uv = _uv;

				if (_Mode == 2) {
					// FishEyeからequirectangularの変換.
					// reference : http://paulbourke.net/dome/fish2/
					float theta = UNITY_PI2 * (uv.x - 0.5);
					float phi   = UNITY_PI * (uv.y - 0.5);
					float sinP = sin(phi);
					float cosP = cos(phi);
					float sinT = sin(theta);
					float cosT = cos(theta);
					float3 vDir = float3(cosP * sinT, cosP * cosT, sinP);

					theta = atan2(vDir.z, vDir.x);
					phi   = atan2(sqrt(vDir.x * vDir.x + vDir.z * vDir.z), vDir.y);
					float r = phi / UNITY_PI; 

					uv.x = 0.5 + r * cos(theta);
					uv.y = 0.5 + r * sin(theta);
					uv.x *= 0.5;

					if (unity_StereoEyeIndex == 1) {
						uv.x += 0.5;
					}
				} if (_Mode == 0) {					// Equirectangular360 TopAndBottom.
					uv.y *= 0.5;
					if (unity_StereoEyeIndex == 0) {
						uv.y += 0.5;
					}
				} else if (_Mode == 1) {			// Equirectangular180 SideBySide.
					uv.x -= 0.25;
					if (unity_StereoEyeIndex == 1) {
						uv.x += 0.5;
					}
				}
				return uv;
			}

			float4 frag (v2f i) : SV_Target
			{
                float2 uv = i.uv;

				if (uv.x < 0.25 || uv.x > 0.75) return float4(0.0, 0.0, 0.0, 1.0);

				// UV値を計算.
				uv = calcUV(uv);

				float4 col = tex2D(_MainTex, uv);
                col.rgb *= _Intensity;

				if (_TransitionType == 1) {		// フェードイン.
					col.rgb = lerp(_FadeInColor.rgb, col.rgb, _TPos);

				} else if (_TransitionType == 2) {		// フェードアウト.
					col.rgb = lerp(col.rgb, _FadeOutColor.rgb, _TPos);

				} else if (_TransitionType == 3) {		// ブレンド.
					float4 col2 = tex2D(_DestTex, uv);
					col2.rgb *= _Intensity;
					col.rgb = lerp(col.rgb, col2.rgb, _TPos);
				}

				return col;
			}
			ENDCG
		}
	}
}
