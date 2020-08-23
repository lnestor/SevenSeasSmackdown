Shader "Custom/Water" {
    Properties {
        _ShallowColor ("Shallow Color", Color) = (0.69, 0.96, 1, 1)
        _DeepColor ("Deep Color", Color) = (0.35, 0.65, 0.75, 1)
        _DeepDepth ("Deep Color Depth", Float) = 1

        _WaveTexture ("Wave Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", Float) = 1
        _WaveAmp ("Wave Amplitude", Float) = 1
    }

    SubShader {
        Tags { "RenderType" = "Opaque" }

        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };

            sampler2D _WaveTexture;
            float _WaveSpeed;
            float _WaveAmp;

            v2f vert(appdata v) {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);

                // Waves
                float noiseSample = tex2Dlod(_WaveTexture, float4(v.uv.xy, 0, 0));
                o.vertex.x += cos(_Time * noiseSample * _WaveSpeed) * _WaveAmp;
                o.vertex.y += sin(_Time * noiseSample * _WaveSpeed) * _WaveAmp;

                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);

                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 _ShallowColor;
            fixed4 _DeepColor;
            float _DeepDepth;

            sampler2D _CameraDepthTexture;

            float3 _CamPos;
            sampler2D _RippleRT;
            float _CamSize;

            fixed4 frag(v2f i) : SV_TARGET {
                // Depth
                float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepth01);
                float depthDiff = existingDepthLinear - i.screenPos.w;

                // Ripples
                float2 rippleUV = i.worldPos.xz - _CamPos.xz;
                rippleUV = rippleUV / (_CamSize * 2);
                rippleUV += 0.5;

                float ripples = tex2D(_RippleRT, rippleUV).b;
                // ripples = step(0.99, ripples * 3);

                // Water color
                float colorT = saturate(depthDiff / _DeepDepth);
                fixed4 waterColor = lerp(_ShallowColor, _DeepColor, colorT);

                fixed4 col = waterColor + ripples;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
                // return waterColor + ripples;
                // return waterColor;
            }

            ENDCG
        }
    }
}
