using GLComponentSystem;
using GLRenderer.Components;
using OpenTK.Mathematics;

namespace GLRenderer.Components
{
    public struct CameraData()
    {
        Vector4 position;
        Matrix4 view;
        Matrix4 perspective;

        public void UpdateCameraData(Vector3 position, Vector3 direction, float FOV, int width, int height)
        {
            this.position = new Vector4(position.X, position.Y, position.Z, 1.0f);
            this.view = Matrix4.LookAt(position, position + direction, Vector3.UnitY);
            this.perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), width / (float)height, 0.1f, 100.0f);
        }
    }
}