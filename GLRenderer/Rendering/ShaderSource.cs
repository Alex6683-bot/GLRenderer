namespace GLRenderer.Rendering
{
    public static class ShaderSource
    {
        public static readonly string defaultFragShaderSource = @"#version 460 core

in vec2 TexCoords;
in vec3 Normal;
in vec3 ws_fragPos;

layout (std140, binding = 1) uniform LightData
{
	vec4 Lightposition;
	vec4 LightColor;
	float brightness;
	float specularStrength;
	float diffuseBrightness;
};
layout (row_major, binding = 0) uniform CameraData
{
	vec4 cameraPosition;
	mat4 view;
	mat4 perspective;
};

out vec4 FragColor;
vec3 finalColor;

uniform vec4 color;
uniform float usesTexture;
uniform sampler2D texture;

vec3 GetLightColor(vec4 inputColor)
{
	float ambientConstant = 0.1f;
	vec3 lightDirection = normalize(vec3(Lightposition) - ws_fragPos); 
	vec3 viewDir = normalize(vec3(cameraPosition) - ws_fragPos);
	vec3 reflection = reflect(-lightDirection, Normal);
	vec3 specular = pow(max(dot(viewDir, reflection), 0), 16) * specularStrength * vec3(LightColor);

	vec3 diffuse = max(0, dot((gl_FrontFacing ? 1 : -1) * Normal, lightDirection)) * vec3(1.0f);

	return vec3(inputColor) * ((diffuse + diffuseBrightness) * brightness) * vec3(LightColor);
}
void main()
{
	vec4 final;

	if (usesTexture == 0) final = color;
	else final = texture(texture, TexCoords) * color;

	FragColor = vec4(GetLightColor(final), 1.0f);
	//FragColor = vec4(vec3(diffuseBrightness), 1.0f);
}";
        public static readonly string defaultVertShaderSource = @"#version 460 core

layout (location = 0) in vec3 vertexPosition;
layout (location = 1) in vec2 texCoords;
layout (location = 2) in vec3 normal;

layout (row_major, binding = 0) uniform CameraData
{
	vec4 cameraPosition;
	mat4 view;
	mat4 perspective;
};

uniform mat4 model;
uniform mat4 normalMatrix;
out vec2 TexCoords;
out vec3 Normal;
out vec3 ws_fragPos;

void main()
{
	vec4 worldSpacePos = vec4(vertexPosition, 1.0) * model;
	gl_Position = worldSpacePos * view * perspective;

	vec3 modifiedNormals = normalize(normal * mat3(normalMatrix));
	TexCoords = texCoords;
	Normal = modifiedNormals;

	ws_fragPos = vec3(worldSpacePos);
}";
        private static Shader _defaultMeshShader = new Shader(ShaderInitType.Source, defaultVertShaderSource, defaultFragShaderSource);
        public static Shader defaultMeshShader
        {
            get => _defaultMeshShader;
        }
    }
}
