using GLEntitySystem;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using GLRenderer.Rendering;
using OpenTK.Windowing.Desktop;

namespace GLRenderer.Components
{
    public class CameraComponent : Component, ICamera
    {
        //Camera data pass
        private CameraData _cameraData = new CameraData();
        private UBO<CameraData> UBO;
        private GameWindow gameWindow;
        private Vector3 direction = Vector3.UnitZ;
        public float speed { get; set; } = 5.5f;
        public float FOV { get; set; } = 60.0f;
        public float rotationSpeed { get; set; } = 0.025f;
        public CameraData cameraData { get => _cameraData; }
        public bool inputEnabled {get; set;} = true;

        public CameraComponent(GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;
        }
        public override void OnStart(Entity entity)
        {
            base.OnStart(entity);

            flags.Add(ComponentFlag.SingletonFlag);

            _cameraData.UpdateCameraData(entity.position, direction, FOV, gameWindow.FramebufferSize.X, gameWindow.FramebufferSize.Y);
            UBO = new UBO<CameraData>(ref _cameraData, "CameraData", 0);
            
        }

        public override void OnUpdate(Entity entity)
        {
            base.OnUpdate(entity);
            #region INPUT

            if (inputEnabled)
            {
                //Keyboard
                Vector3 right = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, direction));
                Vector3 up = Vector3.Normalize(Vector3.Cross(direction, right));
                Vector3 forward = Vector3.Normalize(Vector3.Cross(right, up));

                if (gameWindow.KeyboardState.IsKeyDown(Keys.W)) entity.position += forward * speed * Time.deltaTime;
                if (gameWindow.KeyboardState.IsKeyDown(Keys.S)) entity.position -= forward * speed * Time.deltaTime;
                if (gameWindow.KeyboardState.IsKeyDown(Keys.A)) entity.position += right * speed * Time.deltaTime;
                if (gameWindow.KeyboardState.IsKeyDown(Keys.D)) entity.position -= right * speed * Time.deltaTime;
                if (gameWindow.KeyboardState.IsKeyDown(Keys.E)) entity.position += up * speed * Time.deltaTime;
                if (gameWindow.KeyboardState.IsKeyDown(Keys.Q)) entity.position -= up * speed * Time.deltaTime;

                //FOV -= Input.MouseCallBack().ScrollDelta.Y; //gotta be removed

                //Mouse
                entity.rotation.X -= gameWindow.MouseState.Delta.Y * rotationSpeed;
                entity.rotation.Y += gameWindow.MouseState.Delta.X * rotationSpeed;
            }
            #endregion

            direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(entity.rotation.X)) * (float)Math.Cos(MathHelper.DegreesToRadians(entity.rotation.Y));
            direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(entity.rotation.X));
            direction.Z = (float)Math.Cos(MathHelper.DegreesToRadians(entity.rotation.X)) * (float)Math.Sin(MathHelper.DegreesToRadians(entity.rotation.Y));
            direction = Vector3.Normalize(direction);

            entity.rotation.X = Math.Clamp(entity.rotation.X, -89.0f, 89.0f);

            _cameraData.UpdateCameraData(entity.position, direction, FOV, gameWindow.FramebufferSize.X, gameWindow.FramebufferSize.Y);
            UBO.UpdateUBO(ref _cameraData);
        }

    }
}
