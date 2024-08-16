#version 460 core

in vec2 _texCoords;

out vec4 FragColor;
vec3 finalColor;

uniform vec4 color;

void main()
{
	FragColor = color;
}