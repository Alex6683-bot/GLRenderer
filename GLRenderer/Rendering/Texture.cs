using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using StbImageSharp;


namespace GLRenderer
{
    public class Texture
    {
        #region Fields

        int Handle;
        private TextureWrapMode _textureWrapModeS = TextureWrapMode.Repeat;
        private TextureWrapMode _textureWrapModeT = TextureWrapMode.Repeat;

        private TextureMinFilter _textureMinFilter = TextureMinFilter.Linear;
        private TextureMagFilter _textureMagFilter = TextureMagFilter.Linear;

        #endregion

        public TextureWrapMode textureWrapModeS 
        {
            get => _textureWrapModeS;
            set {
                if (_textureWrapModeS != value) GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)value);
                _textureWrapModeS = value;
            }
        }

        public TextureWrapMode textureWrapModeT 
        {
            get => _textureWrapModeT;
            set {
                if (_textureWrapModeT != value) ConfigureTextureSettings();
                _textureWrapModeT = value;
            }
        }

        public TextureMinFilter textureMinFilter 
        {
            get => _textureMinFilter;
            set {
                if (_textureMinFilter != value) ConfigureTextureSettings();
                _textureMinFilter = value;
            }
        }

        public TextureMagFilter textureMagFilter 
        {
            get => _textureMagFilter;
            set {
                if (_textureMagFilter != value) ConfigureTextureSettings();
                _textureMagFilter = value;
            }
        }

        public Texture(string path)
        {
            Handle = GL.GenTexture();

            BindTexture();
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            ConfigureTextureSettings();
        }

        public Texture(byte[] pixels, int width, int height)
        {
            Handle = GL.GenTexture();

            BindTexture();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);

            ConfigureTextureSettings();
        }

        public Texture(int width, int height)
        {
            Handle = GL.GenTexture();

            BindTexture();
            GL.BindTexture(TextureTarget.Texture2D, Handle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, 0);

            ConfigureTextureSettings();
        }

        public Texture(int Handle) { this.Handle = Handle; }

        private void ConfigureTextureSettings()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)textureWrapModeS);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)textureWrapModeT);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)textureMinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)textureMagFilter);
        }

        public void BindTexture(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

    }
}
