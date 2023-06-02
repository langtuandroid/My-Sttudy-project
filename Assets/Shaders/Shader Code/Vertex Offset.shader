Shader "Unlit/Vertex Offset"
{
     Properties
    {
        _ColorA ("ColorA", Color) = (1, 1, 1, 1)
        _ColorB ("ColorB", Color) = (1, 1, 1, 1)
        
        _ColorStart("Color Start", Range(0,1)) = 1
        _ColorEnd("Color End", Range(0,1)) = 1
        
        _TriangleWaveCount("TriangleWave", Range(1, 10)) = 2
        
        _WaveAmp("Wave Amplitude", Range(0, 1)) = 0.1
        
    }
    SubShader
    {
        Tags {
             "RenderType"="Opaque" 
        }

        Pass
        {
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
            float _WaveAmp;

             float GetWave(float2 uv)
            {
                float2 uvsCenter =  uv * 2 - 1;

                float radialDistance = length(uvsCenter);
                
                float wave = cos((radialDistance - _Time.y * 0.1) * TAU * _TriangleWaveCount) * 0.5 + 0.5;

                return wave;
            }
            
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

                v.vertex.y = GetWave(v.uv) * _WaveAmp;
                
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
                
                float4 gradient = lerp(_ColorA, _ColorB, GetWave(i.uv) );
                
                return GetWave(i.uv) * gradient;
            }
            ENDCG
        }
        
    }
}
