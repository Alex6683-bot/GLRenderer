namespace GLRenderer.Rendering
{
    public static class ShaderSource
    {
        public static Shader defaultShader
        {
            get => new Shader(Utilities.ToRelativePath(@"GLRenderer\Rendering\Shaders\defaultShader.vert"), Utilities.ToRelativePath(@"GLRenderer\Rendering\Shaders\defaultShader.frag"));
        }
    }
}

