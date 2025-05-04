Shader "Z/LightShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _TilTex ("TT", 2D) = "white" {}
        _NormTex ("NormalMap", 2D) = "bump" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _K ("K", Range(0,20)) = 0.0
        _EmCol ("Em_Col", Color) = (0,0,0,0)
        _LightPosition ("LP", Vector) = (0, 0, 0)
        _GreenLight ("GreenLight", Vector) = (0, 0, 0, 0)}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NormTex;
        sampler2D _TilTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_TilTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float4 _EmCol;
        float4 _LightPosition;
        float4 _GreenLight;
        float _K;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        /*
        float SoftMin(float a, float b) {
            return (exp(a) - exp(b)) / (exp(a) + exp(b) + 1e-6);
        }
        */

        /*vec3 mix(vec3 x, vec3 y, vec3 a){
        	vec3 sig = vec3(1.0) - а;
        	vec3 value = х * sig + у * а;
        	return value.x;
        }*/


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            //o.Emission = tex2D(_MainTex, IN.uv_MainTex) * _EmCol;
           	
            float x =  float((IN.uv_TilTex.x - _K) * 2 - 1);
            float y = float(IN.uv_TilTex.y * 2 - 1);

            x += _LightPosition.x;
            y += _LightPosition.y;
            x /= _LightPosition.z;
            y /= _LightPosition.w;

            float z = x * x + y * y;
            z = sqrt(z);
            z = 1 - z;
            float em = float3(z, z, z);
            o.Emission = em * _EmCol;
            o.Normal = UnpackNormal(tex2D(_NormTex, IN.uv_MainTex));
            
            //half3 em = half3(IN.uv_MainTex.xy, z)*2 - 1;
            /*if(em.x < 0){
                em.x = em.x * -1;
            }
            em.x = 1 - em.x;

            float3 em1 = float3(em.x, em.x, em.x);

            if(x < 0.1 || x > -0.1){
                em1 += 0.2;
            }

            if(y > 0.25){
            	float a = 0.1;
                em1 = em1 * (vec3(1.0) - vec3(a)) + vec3(0.0, 0.1, 0.0) * vec3(а);
            }
            //em1 += _LightPosition;*/
        }
        ENDCG
    }
    FallBack "Diffuse"
}
