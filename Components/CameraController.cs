using GLEntitySystem;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GLRenderer.Rendering
{
    public class CameraController : IComponent
    {
        private Vector3 _position;
        public float speed { get; set; } = 7.5f;
        public float FOV { get; set; } = 60.0f;
        public float rotationSpeed { get; set; } = 0.03f;
        private Vector3 direction = Vector3.UnitZ;

        public void SetEyeSpace(Shader shader)
        {
            Matrix4 view = Matrix4.LookAt(_position, _position + direction, Vector3.UnitY);
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), 1, 0.1f, 100.0f);

            shader.SetUniformMatrix4("view", view);
            shader.SetUniformMatrix4("projection", perspective);
        }
        public void OnStart(Entity entity)
        {
            _position = entity.position;
        }

        public void OnUpdate(Entity entity)
        {

            #region INPUT
            //Keyboard
            Vector3 right = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, direction));
            Vector3 up = Vector3.Normalize(Vector3.Cross(direction, right));
            Vector3 forward = Vector3.Normalize(Vector3.Cross(right, up));

            if (Input.KeyboardCallBack().IsKeyDown(Keys.W)) entity.position += forward * speed * (float)Window.deltaTime;
            if (Input.KeyboardCallBack().IsKeyDown(Keys.S)) entity.position -= forward * speed * (float)Window.deltaTime;
            if (Input.KeyboardCallBack().IsKeyDown(Keys.A)) entity.position += right * speed * (float)Window.deltaTime;
            if (Input.KeyboardCallBack().IsKeyDown(Keys.D)) entity.position -= right * speed * (float)Window.deltaTime;
            if (Input.KeyboardCallBack().IsKeyDown(Keys.E)) entity.position += up * speed * (float)Window.deltaTime;
            if (Input.KeyboardCallBack().IsKeyDown(Keys.Q)) entity.position -= up * speed * (float)Window.deltaTime;

            //Mouse
            entity.rotation.X -= Input.MouseCallBack().Delta.Y * rotationSpeed;
            entity.rotation.Y += Input.MouseCallBack().Delta.X * rotationSpeed;
            #endregion

            _position = entity.position;

            direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(entity.rotation.X)) * (float)Math.Cos(MathHelper.DegreesToRadians(entity.rotation.Y));
            direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(entity.rotation.X));
            direction.Z = (float)Math.Cos(MathHelper.DegreesToRadians(entity.rotation.X)) * (float)Math.Sin(MathHelper.DegreesToRadians(entity.rotation.Y));

            direction = Vector3.Normalize(direction);

            entity.rotation.X = Math.Clamp(entity.rotation.X, -89.1f, 89.1f);
        }
    }
}
