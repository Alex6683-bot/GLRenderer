using GLRenderer.Rendering;
using OpenTK.Mathematics;

namespace GLRenderer.Components
{
    public class Material
    {
        private Shader _shader = ShaderSource.defaultMeshShader;
        public Vector4 color = new Vector4(1.0f);
        public Shader shader
        {
            get => _shader;
            set => _shader = value;
        }
        public void SetUniforms()
        {
            shader.SetUniformVector4("color", color);
        }
    }
}