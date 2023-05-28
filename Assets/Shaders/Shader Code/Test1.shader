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
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color + float4(0, _Range, 0, 1);
            }
            ENDCG
        }
    }
}
