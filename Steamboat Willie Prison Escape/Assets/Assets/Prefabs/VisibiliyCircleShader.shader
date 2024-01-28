Shader "Custom/VisibilityCircle" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Radius("Visibility Radius", Range(1.0, 10.0)) = 5.0
    }

        SubShader{
            Tags { "Queue" = "Transparent" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _Radius;

                v2f vert(appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.vertex.xy * 0.5 + 0.5;
                    return o;
                }

                half4 frag(v2f i) : SV_Target {
                    float3 worldPos = mul(unity_ObjectToWorld, float4(i.uv.x, i.uv.y, 0, 1)).xyz;
                    float3 playerPos = _WorldSpaceCameraPos;

                    // Check if the player is within the visibility radius
                    bool isPlayerVisible = distance(worldPos, playerPos) <= _Radius;

                    // If player is visible, display the texture; otherwise, make it transparent
                    half4 col = tex2D(_MainTex, i.uv);
                    col.a = isPlayerVisible ? 1.0 : 0.0;

                    return col;
                }

                ENDCG
            }
        }
}
