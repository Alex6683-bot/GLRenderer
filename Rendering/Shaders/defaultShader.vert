#version 460 core

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
out vec2 _texCoords;
out vec3 _normal;
out vec3 ws_fragPos;

void main()
{
	vec4 worldSpacePos = vec4(vertexPosition, 1.0) * model;
	gl_Position = worldSpacePos * view * perspective;

	vec4 modifiedNormals = normalize(vec4(normal, 1.0) * normalMatrix);
	_texCoords = texCoords;
	_normal = vec3(modifiedNormals);

	ws_fragPos = vec3(worldSpacePos);
}