﻿Shader "VacuumShaders/Terrain To Mesh/Legacy Shaders/Diffuse/4 Textures" 
{
	Properties 
	{
		_Color("Tint Color", color) = (1, 1, 1, 1)
		_V_T2M_Control ("Control Map (RGBA)", 2D) = "black" {}

		//TTM	
		[V_T2M_SplatDiffuseMap] _V_T2M_Splat1 ("Layer 1 (R)", 2D) = "white" {}
		[HideInInspector] _V_T2M_Splat1_uvScale("", float) = 1	

		[V_T2M_SplatDiffuseMap] _V_T2M_Splat2 ("Layer 2 (G)", 2D) = "white" {}
		[HideInInspector] _V_T2M_Splat2_uvScale("", float) = 1	

		[V_T2M_SplatDiffuseMap] _V_T2M_Splat3 ("Layer 3 (B)", 2D) = "white" {}
		[HideInInspector] _V_T2M_Splat3_uvScale("", float) = 1	

		[V_T2M_SplatDiffuseMap] _V_T2M_Splat4 ("Layer 4 (A)", 2D) = "white" {}
		[HideInInspector] _V_T2M_Splat4_uvScale("", float) = 1	
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		//LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
		//#pragma exclude_renderers flash


		#define V_T2M_3_TEX
		#define V_T2M_4_TEX

		#include "../cginc/T2M_Deferred.cginc"		

		ENDCG
	} 

	FallBack "Legacy Shaders/Diffuse"
}
