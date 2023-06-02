Shader "Unlit/Vertex Offset"
{
     Properties
    {
        _ColorA ("ColorA", Color) = (1, 1, 1, 1)
        _ColorB ("ColorB", Color) = (1, 1, 1, 1)
        
        _ColorStart("Color Start", Range(0,1)) = 1
        _ColorEnd("Color End", Range(0,1)) = 1
        
        _TriangleWaveCount("TriangleWave", Range(1, 10)) = 2
        
    }
    SubShader
    {
        Tags {
             "RenderType"="Transparent" 
             "Queue"="Transparent"
        }

        Pass
        {
            Cull Off
            ZWrite Off //Depth Buffer Off
            Blend One One //Additive
            //Blend DstColor Zero //Multiply
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define  TAU 6.283185307179586

            float4 _ColorA;
            float4 _ColorB;
            float _ColorStart;
            float _ColorEnd;
            int _TriangleWaveCount;
            
            struct MeshData
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

            v2f vert (MeshData v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normals;
                o.uv = v.uv;
                return o;
            }

            float InverseLerp(float a, float b, float v) 
            {
                return (v-a)/(b-a);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // float xOffset = cos(i.uv.x * TAU * 5) * 0.02;
                // float t = cos((i.uv.y + xOffset - _Time.y * 0.1) * 6.283185307179586 * _TriangleWaveCount) * 0.5 + 0.5;
                //
                // t *= 1-i.uv.y;
                //
                // float topBottomRemover = (abs(i.normal.y) < 0.999);
                //
                // float wave =  t * topBottomRemover;
                //
                // float4 gradient = lerp(_ColorA, _ColorB, i.uv.y);
                //
                // return gradient * wave;
            }
            ENDCG
        }
        
    }
}
