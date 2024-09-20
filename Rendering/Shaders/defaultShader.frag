#version 460 core

in vec2 _texCoords;
in vec3 _normal;
in vec3 ws_fragPos;

layout (std140, binding = 1) uniform LightData
{
	vec4 Lightposition;
	vec4 LightColor;
	float brightness;
	float specularStrength;
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

void main()
{
	vec3 lightDirection = normalize(vec3(Lightposition) - ws_fragPos); 
	vec3 viewDir = normalize(vec3(cameraPosition) - ws_fragPos);
	vec3 reflection = reflect(-lightDirection, _normal);
	vec3 specular = pow(max(dot(viewDir, reflection), 0), 32) * specularStrength * vec3(LightColor);

	vec3 ambientColor = vec3(color * brightness);
	vec3 diffuse = max(0, dot(_normal, lightDirection)) * vec3(LightColor);
	FragColor = vec4((ambientColor + diffuse + specular) * vec3(color), 1.0f);
}