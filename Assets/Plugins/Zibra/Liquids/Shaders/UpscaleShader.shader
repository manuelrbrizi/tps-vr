Shader "ZibraLiquids/UpscaleShader"
{
    SubShader
    {
        Pass
        {
            // Premultiplied alpha blending
            Blend One OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma instancing_options procedural:setup
            #pragma vertex VSMain
            #pragma fragment PSMain
            #pragma target 3.0

            struct VSIn
            {
                uint vertexID : SV_VertexID;
            };

            struct VSOut
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct PSOut
            {
                float4 color : COLOR;
                float depth : DEPTH;
            };
            
            // Camera params
            float2 TextureScale;
            
            // Input resources
            sampler2D ShadedWater;
            sampler2D WaterDepth;

            float2 GetFlippedUV(float2 uv)
            {
                if (_ProjectionParams.x > 0)
                    return float2(uv.x, 1 - uv.y);
                return uv;
            }

            VSOut VSMain(VSIn input)
            {
                VSOut output;

                float2 vertexBuffer[4] = {
                    float2(0.0f, 0.0f),
                    float2(0.0f, 1.0f),
                    float2(1.0f, 0.0f),
                    float2(1.0f, 1.0f),
                    };
                uint indexBuffer[6] = {0, 1, 2, 2, 1, 3};
                uint indexID = indexBuffer[input.vertexID];

                float2 uv = vertexBuffer[indexID];
                float2 flippedUV = GetFlippedUV(uv);

                output.position = float4(2 * flippedUV.x - 1, 1 - 2 * flippedUV.y, 0.5, 1.0);
                output.uv = uv;

                return output;
            }

            PSOut PSMain(VSOut input)
            {
                PSOut output;

                output.color = tex2D(ShadedWater, input.uv * TextureScale);
                // iPhone 7 GPU crash workaround
                if (output.color.a > 0.0f)
                {
                    output.depth = tex2D(WaterDepth, input.uv * TextureScale).x;
                }

                return output;
            }
            ENDCG
        }
    }
}
