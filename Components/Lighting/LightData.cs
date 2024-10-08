using System;
using GLRenderer;
using GLComponentSystem;
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
        float diffuseBrightness;

        public void UpdateLightData(Vector3 lightPosition, float brightness, float diffuseBrightness, Vector4 color, float specularStrength)
        {
            this.lightPosition = new Vector4(lightPosition.X, lightPosition.Y, lightPosition.Z, 1.0f);
            this.brightness = brightness;
            this.color = color;
            this.specularStrength = specularStrength;
            this.diffuseBrightness = diffuseBrightness;
        }
    }
}