﻿Shader "Light2D/Sprites/FogOfWar"
{
	Properties
	{
		[HideInInspector] _MainTex ("Sprite Texture", 2D) = "white" {}
 
		_Color ("Tint", Color) = (1,1,1,1)

		// Camera 1
		[HideInInspector] _Cam1_Texture ("Camera 1 Texture", 2D) = "white" {}
		[HideInInspector] _Cam1_Rect ("Camera 1 Rect", Vector) = (0, 0, 0, 0)
		[HideInInspector] _Cam1_Rotation ("Camera 1 Rotation", Float) = 0

		// Camera 2
		[HideInInspector] _Cam2_Texture ("Camera 2 Texture", 2D) = "white" {}
		[HideInInspector] _Cam2_Rect ("Camera 2 Rect", Float) = (0, 0, 0, 0)
		[HideInInspector] _Cam2_Rotation ("Camera 2 Rotation", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass {

		CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD1;
                float2 worldPos : TEXCOORD0;
			};
			
			fixed4 _Color;

            // Cam 1
			sampler2D _Cam1_Texture;
            vector _Cam1_Rect;
            float _Cam1_Rotation;

			 // Cam 2
			sampler2D _Cam2_Texture;
			vector _Cam2_Rect;
            float _Cam2_Rotation;

			sampler2D _MainTex;

			v2f vert(appdata_t IN)
			{
				v2f OUT;

				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
                
                OUT.worldPos = mul (unity_ObjectToWorld, IN.vertex);

				return OUT;
			}

            bool InCamera (float2 pos, float2 rectPos, float2 rectSize) {
				rectPos -= rectSize / 2;
                return (pos.x < rectPos.x || pos.x > rectPos.x + rectSize.x || pos.y < rectPos.y || pos.y > rectPos.y + rectSize.y) == false;
            }

			float2 TransformToCamera(float2 pos, float rotation) {
				float c = cos(-rotation);
				float s = sin(-rotation);
		
				float x = pos.x;
				float y = pos.y;

				pos.x = x * c - y * s;
				pos.y = x * s + y * c;

                return(pos);
            }

			fixed4 GetPixelByID(float id, float2 texcoord) {
				if (id < 0.5f) {
					return(tex2D (_Cam1_Texture, texcoord));
				} else if (id < 1.5f) {
					return(tex2D (_Cam2_Texture, texcoord));
				}
				
				return(fixed4(0, 0, 0, 0));
			}

			fixed4 OutputColor(v2f IN, float2 posInCamera, float2 cameraSize, float id) {
				float2 texcoord = posInCamera;
				texcoord += cameraSize / 2;
				texcoord /= cameraSize;
			
				fixed4 lightPixel = GetPixelByID(id, texcoord);
				fixed4 spritePixel = tex2D (_MainTex, IN.texcoord) * IN.color; 

				float multiplier = (lightPixel.r + lightPixel.g + lightPixel.b) / 3;

				spritePixel.a *= multiplier;

				if (spritePixel.a > 1) {
					spritePixel.a = 1;
				}

				spritePixel.rgb *= spritePixel.a; 
				
				return spritePixel;
			}
        
			fixed4 frag(v2f IN) : SV_Target
			{
				if (_Cam1_Rect.z > 0) {
					float2 camera_1_Size = float2(_Cam1_Rect.z, _Cam1_Rect.w);
					float2 posInCamera1 = TransformToCamera(IN.worldPos - float2(_Cam1_Rect.x, _Cam1_Rect.y),  _Cam1_Rotation);

					if (InCamera(posInCamera1, float2(0, 0), camera_1_Size)) {
                    	return OutputColor(IN, posInCamera1, camera_1_Size, 0);
					}
				}

				if (_Cam2_Rect.z > 0) {
					float2 camera_2_Size = float2(_Cam2_Rect.z, _Cam2_Rect.w);
					float2 posInCamera2 = TransformToCamera(IN.worldPos - float2(_Cam2_Rect.x, _Cam2_Rect.y),  _Cam2_Rotation);
					
					if (InCamera(posInCamera2, float2(0, 0), camera_2_Size)) {
						return OutputColor(IN, posInCamera2, camera_2_Size, 1);
					}
                }

				fixed4 spritePixel = tex2D (_MainTex, IN.texcoord) * IN.color;  

				spritePixel.rgb *= spritePixel.a;

				return spritePixel;
			}

		    ENDCG
		}
	}
}