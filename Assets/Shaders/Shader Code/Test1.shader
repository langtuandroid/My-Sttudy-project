Shader "Unlit/Test1"
{
    Properties
    {
        _Color ("Color", Color) = (1, 0, 0, 1)
        _Range ("Colloer Controller Range", Int) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Color;
            int _Range;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normals : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.normal = UnityObjectToWorldNormal(v.normals);
                o.normal = mul(v.normals, (float3x3)unity_WorldToObject);
                //o.normal = v.normals;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return float4(i.uv.yyy, 1);
            }
            ENDCG
        }
    }
}
