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


layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec3 inColor;
layout(location = 2) in vec2 inTexCoord;
layout(location = 3) in vec3 inNormal;
layout(location = 4) in uint inModelNr;
layout(location = 5) in uint inTextureNr;

layout(location = 0) out vec3 fragColor;
layout(location = 1) out vec2 fragTexCoord;
layout(location = 2) out uint fragTexNr;
layout(location = 3) out vec3 WorldNormal;
layout(location = 4) out vec3 WorldPosition;


out gl_PerVertex {
    vec4 gl_Position;
};

 vec3 GetRVector(vec3 N, vec3 L)
 {
        return  normalize((2 * (dot(N,L))*N)-L);
}


vec3 GetColorFromLight(vec3 lightPosition, vec4 objectColor)
{
        vec3 dist=lightPosition-WorldPosition;
        float distancer=length(dist);
        dist=normalize(dist);
        vec3 normal=normalize(WorldNormal);

        float cos = dot(normal,dist);

        if(cos<0) cos=0;        
        
        vec3 diffuse=vec3(objectColor*cos*ubo.diffuseRatio.x);

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
    gl_Position = ubo.proj * ubo.view * ubo.model[inModelNr] * vec4(inPosition, 1.0);

 

    fragColor = inColor;

    fragTexCoord = inTexCoord;
    fragTexNr=inTextureNr;

    WorldPosition=vec3(ubo.view*ubo.model[inModelNr] * vec4(inPosition, 1.0));
    WorldNormal=vec3(ubo.view *ubo.model[inModelNr]* vec4(inNormal, 0.0));


    vec4 objectColor =texture(sampler2D(textures[fragTexNr], samp), fragTexCoord);

    vec3 endColor=30*GetColorFromLight(vec3(ubo.Lighthouse),objectColor)+GetColorFromLight(vec3(0.0,-0.0,-0.1),objectColor)+vec3(ubo.ambient);
        endColor=clamp(endColor,0.0,1.0);

                fragColor=endColor;
}