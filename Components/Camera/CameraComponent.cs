using GLComponentSystem;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using GLRenderer.Rendering;
using OpenTK.Windowing.Desktop;
using System.Data;
using GLRenderer.SceneSystem;

namespace GLRenderer.Components
{
    public class CameraComponent : Component, ICamera
    {
        //Camera data pass

        #region Fields
        private CameraData _cameraData = new CameraData();
        private UBO<CameraData> UBO;
        private Vector3 direction = Vector3.UnitZ;
        private Input input;
        private WindowSettings windowSettings;
        #endregion

        #region Properties
        public float speed { get; set; } = 5.5f;
        public float FOV { get; set; } = 60.0f;
        public float rotationSpeed { get; set; } = 0.025f;
        public CameraData cameraData { get => _cameraData; }
        public bool inputEnabled {get; set;} = true;
        #endregion

        public CameraComponent(Input input, WindowSettings windowSettings)
        {
            this.input = input;
            this.windowSettings = windowSettings;
        }

        public override void OnStart(Entity entity)
        {
            base.OnStart(entity);

            flags.Add(ComponentFlag.SingletonFlag);

            _cameraData.UpdateCameraData(entity.position, direction, FOV, (int)windowSettings.GetFrameBufferSize().X, (int)windowSettings.GetFrameBufferSize().Y);
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

                if (input.KeyboardCallBack().IsKeyDown(Keys.W)) entity.position += forward * speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.S)) entity.position -= forward * speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.A)) entity.position += right * speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.D)) entity.position -= right * speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.E)) entity.position += up * speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.Q)) entity.position -= up * speed * Time.deltaTime;

                //FOV -= Input.MouseCallBack().ScrollDelta.Y; //gotta be removed

                //Mouse
                entity.rotation.X -= input.MouseCallBack().Delta.Y * rotationSpeed;
                entity.rotation.Y += input.MouseCallBack().Delta.X * rotationSpeed;
            }
            #endregion

            direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(entity.rotation.X)) * (float)Math.Cos(MathHelper.DegreesToRadians(entity.rotation.Y));
            direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(entity.rotation.X));
            direction.Z = (float)Math.Cos(MathHelper.DegreesToRadians(entity.rotation.X)) * (float)Math.Sin(MathHelper.DegreesToRadians(entity.rotation.Y));
            direction = Vector3.Normalize(direction);

            entity.rotation.X = Math.Clamp(entity.rotation.X, -89.0f, 89.0f);
            
            
            _cameraData.UpdateCameraData(entity.position, direction, FOV, (int)windowSettings.GetFrameBufferSize().X, (int)windowSettings.GetFrameBufferSize().Y);
            UBO.UpdateUBO(ref _cameraData);
        }

    }
}
