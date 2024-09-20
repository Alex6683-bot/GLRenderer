namespace GLRenderer.Rendering
{
    public static class ShaderSource
    {
        private static Shader _defaultMeshShader = new Shader(Utilities.ToRelativePath(@"GLRenderer\Rendering\Shaders\defaultShader.vert"), Utilities.ToRelativePath(@"GLRenderer\Rendering\Shaders\defaultShader.frag"));
        public static Shader defaultMeshShader
        {
            get => _defaultMeshShader;
        }
    }
}
