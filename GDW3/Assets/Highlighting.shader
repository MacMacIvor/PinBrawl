Shader "Unlit/Highlighting"

{
    Properties
    {
        _Color("Main color", Color) = (0.5, 0.5, 0.5, 1)
        _MainTex("Texture", 2D) = "" {} //Choose the colour of the highlighted object (seems to only go with "red," otherwise it's grey)
        _HighlightWidth("Highlight width", Range(0.0, 10.0)) = 1.05 //Thickness of the highlight
    }

        CGINCLUDE
#include "UnityCG.cginc"

            float _HighlightWidth;
        float4 _HighlightColor;

        struct AppData
        {
            float3 normal : NORMAL;
            float4 vertex : POSITION;
        };
        struct v2f
        {
            float3 norm : NORMAL;
            float4 pos : POSITION;

        };


        //Multiply vertex by the normal to expand the object
        v2f vert(AppData v)
        {
            v.vertex.xyz = v.vertex.xyz * _HighlightWidth;
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            return o;
        }


        ENDCG
            SubShader
        {
             Pass
            {
                ZWrite Off
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                half4 frag(v2f i) : COLOR
                {
                    return _HighlightColor;
                }

                ENDCG
            }


            Pass
            {
                    ZWrite On
                    Material
                {
                    Diffuse[_Color]
                    Ambient[_Color]
                }
                Lighting On

                SetTexture[_MainTex]
                {
                    ConstantColor[_Color]
                }

                SetTexture[_MainTex]
                {
                    Combine previous * primary DOUBLE
                }


            }
        }
}