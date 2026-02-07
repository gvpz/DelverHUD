Shader "UI/BackgroundGradient"
{
    Properties
    {
        _ColorA ("Top-Left Color", Color) = (0,0,0,1)
        _ColorB ("Bottom-Right Color", Color) = (1,1,1,1)

        _Rotation ("Rotation (Degrees)", Range(0,360)) = 45
        _Offset ("Offset", Range(-1,1)) = 0
        _Scale  ("Scale", Range(0.01, 4)) = 1

        _TextureStrength ("Texture Strength", Range(0,1)) = 1

        _ShimmerColor ("Shimmer Color", Color) = (1,1,1,1)
        _ShimmerIntensity ("Shimmer Intensity", Range(0, 2)) = 0.35
        _ShimmerWidth ("Shimmer Width", Range(0.001, 0.5)) = 0.08
        _ShimmerSpeed ("Shimmer Speed", Range(0.01, 5)) = 1.0
        _ShimmerPeriod ("Shimmer Period (sec)", Range(0.2, 10)) = 2.5
        _ShimmerAngle ("Shimmer Angle (deg)", Range(0, 360)) = 0
        _ShimmerUseDiagonalTLBR ("Shimmer TL->BR", Float) = 1
        _ShimmerChance ("Shimmer Chance (0-1)", Range(0,1)) = 0.25
        _ShimmerGate ("Shimmer Active Fraction", Range(0.05,1)) = 0.30

        _ShimmerZoom ("Shimmer Magnification", Range(0, 0.15)) = 0.04
        _ShimmerZoomSoftness ("Shimmer Zoom Softness", Range(0.1, 4)) = 1.0

        _MainTex ("Sprite Texture", 2D) = "white" {}

        [HideInInspector]_StencilComp ("Stencil Comparison", Float) = 8
        [HideInInspector]_Stencil ("Stencil ID", Float) = 0
        [HideInInspector]_StencilOp ("Stencil Operation", Float) = 0
        [HideInInspector]_StencilWriteMask ("Stencil Write Mask", Float) = 255
        [HideInInspector]_StencilReadMask ("Stencil Read Mask", Float) = 255
        [HideInInspector]_ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "BackgroundGradient"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _ColorA;
                float4 _ColorB;
                float  _Rotation;
                float  _Offset;
                float  _Scale;

                float  _TextureStrength;

                float4 _ShimmerColor;
                float  _ShimmerIntensity;
                float  _ShimmerWidth;
                float  _ShimmerSpeed;
                float  _ShimmerPeriod;
                float  _ShimmerAngle;
                float  _ShimmerUseDiagonalTLBR;
                float  _ShimmerChance;
                float  _ShimmerGate;

                float  _ShimmerZoom;
                float  _ShimmerZoomSoftness;
            CBUFFER_END

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            struct Attributes
            {
                float3 positionOS : POSITION;
                float4 color      : COLOR;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 color       : COLOR;
                float2 uv          : TEXCOORD0;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            float2 RotateUV(float2 uv, float degrees)
            {
                float2 p = uv - 0.5;
                float r = radians(degrees);
                float s = sin(r);
                float c = cos(r);
                float2 rp = float2(p.x * c - p.y * s, p.x * s + p.y * c);
                return rp + 0.5;
            }

            float SoftBand(float x, float width)
            {
                return saturate(1.0 - smoothstep(0.0, width, x));
            }

            float Hash11(float x)
            {
                return frac(sin(x * 127.1) * 43758.5453123);
            }

            half4 frag(Varyings i) : SV_Target
            {
                half4 texBase = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                float2 uvR = RotateUV(i.uv, _Rotation);

                float t = (uvR.x + (1.0 - uvR.y)) * 0.5;
                t = (t - 0.5) / max(_Scale, 1e-5) + 0.5 + _Offset;
                t = saturate(t);

                half4 col = lerp(_ColorA, _ColorB, t);

                float period = max(_ShimmerPeriod, 1e-4);
                float cyclePos = _Time.y / period;

                float cycleIndex = floor(cyclePos);
                float cycleT = frac(cyclePos);

                float r = Hash11(cycleIndex);
                float trigger = step(r, saturate(_ShimmerChance));

                float activeFrac = saturate(_ShimmerGate);
                float gateIn  = smoothstep(0.00, 0.05, cycleT);
                float gateOut = 1.0 - smoothstep(activeFrac, min(activeFrac + 0.10, 1.0), cycleT);
                float gate = trigger * gateIn * gateOut;

                float passT = frac((cycleT / max(activeFrac, 1e-4)) * _ShimmerSpeed);

                float2 dir;
                if (_ShimmerUseDiagonalTLBR > 0.5)
                {
                    dir = normalize(float2(1.0, -1.0));
                }
                else
                {
                    float a = radians(_ShimmerAngle);
                    dir = normalize(float2(cos(a), sin(a)));
                }

                float pRaw = dot(i.uv, dir);

                float p0 = dot(float2(0,0), dir);
                float p1 = dot(float2(1,0), dir);
                float p2 = dot(float2(0,1), dir);
                float p3 = dot(float2(1,1), dir);

                float pMin = min(min(p0, p1), min(p2, p3));
                float pMax = max(max(p0, p1), max(p2, p3));

                float p = (pRaw - pMin) / max(pMax - pMin, 1e-5);

                float width = _ShimmerWidth;
                float center = lerp(-width, 1.0 + width, passT);

                float band = SoftBand(abs(p - center), width);

                float shimmerMask = saturate(band * gate);
                shimmerMask = pow(shimmerMask, max(_ShimmerZoomSoftness, 1e-4));

                float zoom = _ShimmerZoom * shimmerMask;
                float2 uvFromCenter = (i.uv - 0.5);
                float2 uvZoom = 0.5 + uvFromCenter / (1.0 + zoom);

                half4 texZoom = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvZoom);
                half4 tex = lerp(texBase, texZoom, shimmerMask);

                half3 rgbMul = col.rgb * tex.rgb;
                col.rgb = lerp(col.rgb, rgbMul, _TextureStrength);
                col.a  *= tex.a;

                col.rgb += _ShimmerColor.rgb * (_ShimmerIntensity * band * gate);

                col *= i.color;

                return col;
            }
            ENDHLSL
        }
    }
}