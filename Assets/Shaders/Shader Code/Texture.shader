Shader "Unlit/Texture"
{
    Properties{
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader{
        Tags {
             "RenderType"="Opaque" 
             "Queue"="Transparent"
        }

        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v){
                v2f o;
                o.worldPos = mul(UNITY_MATRIX_M, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                //o.uv.y += _Time.y * 0.1;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target{

                float2 topdownProjection = i.worldPos.xz;
                //fixed4 col = tex2D(_MainTex, i.uv);

                float4 col = tex2D(_MainTex, topdownProjection);
                return col;
            }
            ENDCG
        }
    }
}
