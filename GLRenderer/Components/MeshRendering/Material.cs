using GLRenderer.Rendering;
using OpenTK.Graphics.ES11;
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
        public Texture texture = null;
        public void SetUniforms()
        {
            shader.SetUniformVector4("color", color);
            shader.SetUniformFloat("usesTexture", texture != null ? 1 : 0);
        }
        public void Update()
        {
            texture?.BindTexture();
        }
    }
}