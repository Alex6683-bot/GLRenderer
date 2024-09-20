namespace GLRenderer.Rendering
{
    public static class ShaderSource
    {
        private static Shader _defaultMeshShader = new Shader(Extension.ToRelativePath(@"GLRenderer\Rendering\Shaders\defaultShader.vert"), Extension.ToRelativePath(@"GLRenderer\Rendering\Shaders\defaultShader.frag"));
        public static Shader defaultMeshShader
        {
            get => _defaultMeshShader;
        }
    }
}
