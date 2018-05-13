Shader "Outline/Highlight"
{
	Properties
	{
		_Color("Main Color", Color) = (0.5, 0.5, 0.5, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineWidth("Outline Width", Range(1.0, 5.0)) = 1.01
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
	}

	// Declare CG code outside of SubShader (can do inside, or in Pass)
	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata
	{
		float4 vertex: POSITION;
		float3 normal: NORMAL;
	};

	struct v2f
	{
		float4 pos: POSITION;
		float3 normal: NORMAL;
	};

	float _OutlineWidth;
	float4 _OutlineColor;

	v2f vert(appdata v)
	{
		v.vertex.xyz *= _OutlineWidth;
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		return o;
	}

	ENDCG

	SubShader
	{
		Tags{ "Queue" = "Transparent" }
	
		// R ender the Outline
		Pass
		{
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i): Color
			{
				return _OutlineColor;
			}

			ENDCG
		}

		// Normal Render = Render the Object on top of Outline, in this case.
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
