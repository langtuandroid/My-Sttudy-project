Shader "Unlit/HealthBar"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        _Health ("Health", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" 
                "Queue"="Transparent"
            }

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
            float _Health;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 HealthColor = lerp(float3(1, 0, 0), float3(0, 1, 0), _Health);
                float3 BgColor = float3(0, 0, 0);
                
                float HealthBarMask = _Health > i.uv.x;

                float3 OutColor = lerp(BgColor, HealthColor, HealthBarMask);
                
                return float4(OutColor, 0);
            }
            ENDCG
        }
    }
}
