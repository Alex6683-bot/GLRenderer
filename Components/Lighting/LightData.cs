using System;
using GLRenderer;
using GLEntitySystem;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GLRenderer.Components
{
    public struct LightData
    {
        Vector4 lightPosition;
        Vector4 color;
        float brightness;
        float specularStrength;

        public void UpdateLightData(Vector3 lightPosition, float brightness, Vector4 color, float specularStrength)
        {
            this.lightPosition = new Vector4(lightPosition.X, lightPosition.Y, lightPosition.Z, 1.0f);
            this.brightness = brightness;
            this.color = color;
            this.specularStrength = specularStrength;
        }
    }
}