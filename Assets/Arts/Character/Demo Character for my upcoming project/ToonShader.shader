Shader "Unlit/ToonShader"
{
    Properties{
        _MainTex ("Texture", 2D) = "white" {}
        _Brightness ("Brightness", Range(0.0, 1.0)) = 0.3
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

            float Toon(float3 normal, float3 lightDir){
                
                float NormalDotLight = max(0.0,dot(normalize(normal), normalize(lightDir)));

                return floor(NormalDotLight/0.3);
            }

            struct appdata{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f{
                float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Brightness;

            v2f vert (appdata v){
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target{
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= Toon(i.worldNormal, _WorldSpaceLightPos0.zyx) + _Brightness;
                return col;
            }
            ENDCG
        }
    }
}
