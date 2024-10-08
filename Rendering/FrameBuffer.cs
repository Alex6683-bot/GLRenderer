using GLRenderer.SceneSystem;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace GLRenderer
{
    class Framebuffer
    {
        public int ID;
        public int textureID;
        public int renderBufferID;
        WindowSettings windowSettings;
        Texture texture;

        public Framebuffer(WindowSettings windowSettings)
        {
            ID = GL.GenFramebuffer();            
            Bind();

            CreateTexture();
            CreateRenderBuffer();
            AttachBuffers();
            
            Unbind();

            this.windowSettings = windowSettings;
        }
            
        private void CreateTexture()
        {
            textureID = GL.GenTexture();
            texture = new Texture(textureID);
            
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)windowSettings.GetFrameBufferSize().X, (int)windowSettings.GetFrameBufferSize().Y, 0, PixelFormat.Rgb, PixelType.UnsignedByte, 0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        private void CreateRenderBuffer()
        {
            renderBufferID = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBufferID);

            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, (int)windowSettings.GetFrameBufferSize().X, (int)windowSettings.GetFrameBufferSize().Y);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }

        private void AttachBuffers()
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, textureID, 0);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, renderBufferID);
        }
        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);
            GL.ReadBuffer(ReadBufferMode.ColorAttachment0);
        }
        public void Unbind() => GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }
}
