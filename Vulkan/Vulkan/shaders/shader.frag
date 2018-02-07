#version 450
#extension GL_ARB_separate_shader_objects : enable

layout(binding = 0) uniform UniformBufferObject {
    mat4 model[64];
    mat4 view;
    mat4 proj;
    vec4 Lighthouse;
    vec4 spotExponent;
    vec4 constantAttenuation; // K0   
    vec4 linearAttenuation;   // K1   
    vec4 quadraticAttenuation;// K2  
    vec4 ambient;              // Dcli   
    vec4 diffuseRatio;
    vec4 SpecularRatio;
        vec4 Type;
} ubo;


layout(binding = 1) uniform sampler samp;
layout(binding = 2) uniform texture2D textures[5];

layout(location = 0) in vec3 fragColor;
layout(location = 1) in vec2 fragTexCoord;
layout(location = 2) in flat int fragTexNr;
layout(location = 3) in vec3 WorldNormal;
layout(location = 4) in vec3 WorldPosition;

layout(location = 0) out vec4 outColor;

 vec3 GetRVector(vec3 N, vec3 L)
 {
        return normalize((2 * (dot(N,L))*N)-L);
}

vec3 GetColorFromLight(vec3 lightPosition, vec4 objectColor, int spotlight)
{
        vec3 dist=lightPosition-WorldPosition;
        float distancer=length(dist);

        dist=normalize(dist);

        vec3 normal=normalize(WorldNormal);

        float cos = dot(normal,dist);

        if(cos<0) cos=0;        
        
        vec3 diffuse=vec3(objectColor*cos*ubo.diffuseRatio.x);

        if(spotlight==1)
        {
                float dirDiff=dot(-dist,vec3(0.0,0.0,-1.0));
                clamp(dirDiff,0.0,1.0);
                dirDiff=pow(dirDiff,20);
                diffuse=diffuse*dirDiff;
        }

        vec3 R = GetRVector(normal, dist);
        vec3 normWorld=normalize(WorldNormal);
        float coef = pow(dot(R, normWorld), 100) * ubo.SpecularRatio.x;
        vec3 specular=vec3(coef,coef,coef);

        specular=clamp(specular,0.0,1.0);
        diffuse=clamp(diffuse,0.0,1.0);

        return (diffuse+specular)/(pow(distancer,2)/500);
}

void main() 
{
        vec4 objectColor =texture(sampler2D(textures[fragTexNr], samp), fragTexCoord);

        vec3 endColor=20*GetColorFromLight(vec3(ubo.Lighthouse),objectColor,0)+0.2*GetColorFromLight(vec3(0.3,-0.3,-1.0),objectColor,1)+vec3(ubo.ambient);
        endColor=clamp(endColor,0.0,1.0);

        outColor=vec4(endColor,0);

        if(ubo.Type.x==4)
                outColor=vec4(fragColor,0);
}

