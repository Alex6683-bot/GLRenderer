using OpenTK.Graphics.OpenGL;
using System.Runtime.CompilerServices;

namespace GLRenderer.Components
{
    public interface ICamera
    {
        CameraData CameraData { get; }
    }
}
