Shader "Unlit/Mapper"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        // _uvTopLeft ("UV Top Left", Vector) = (0, 1, 0, 0)
        // _uvTopRight ("UV Top Right", Vector) = (1, 1, 0, 0)
        // _uvBottomLeft ("UV Bottom Left", Vector) = (0, 0, 0, 0)
        // _uvBottomRight ("UV Bottom Right", Vector) = (1, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _uvTopLeft;
            float4 _uvTopRight;
            float4 _uvBottomLeft;
            float4 _uvBottomRight;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                float2 uv2 = lerp(
                    lerp(_uvTopLeft.xy, _uvTopRight.xy, uv.x),
                    lerp(_uvBottomLeft.xy, _uvBottomRight.xy, uv.x),
                    uv.y
                );

                uv2 = lerp(
                    lerp(_uvTopLeft.xy, _uvTopRight.xy, uv.x),
                    lerp(_uvBottomLeft.xy, _uvBottomRight.xy, uv.x),
                    1 - uv.y
                );

                if (uv2.x < 0 || uv2.x > 1) { return fixed4(0, 0, 0, 0); }
                if (uv2.y < 0 || uv2.y > 1) { return fixed4(0, 0, 0, 0); }

                return tex2D(_MainTex, uv2);
            }
            ENDCG
        }
    }
}
