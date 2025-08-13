Shader "Unlit/EffectShow"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Show("Show", float) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque"}
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

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
                    float3 worldPos:TEXCOORD2;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _Show;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                //这一句是从上到下，若是从下到上，可改为：_show-i.worldPos.y
                //若是进行左右渐隐，则可把i.worldPos.y改为i.worldPos.x，前后则改为i.worldPos.z。
                //每个轴的两个方向，可通过改变diss的正负值来实现。
                float diss = i.worldPos.y - _Show;
                if (diss >= 0 && diss <= 0.05)
                {
                    col = float4(1, 0, 0, (0.05 - diss) / 0.05);
                }
                else {
                    clip(diss);
                }

                return col;
            }
            ENDCG
        }
        }
}
