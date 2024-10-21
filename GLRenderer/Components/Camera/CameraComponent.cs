using GLComponentSystem;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using GLRenderer.Rendering;
using GLRenderer.SceneSystem;
using OpenTK.Graphics.ES11;

namespace GLRenderer.Components
{
    public class CameraComponent : Component
    {
        #region Fields
        private CameraData cameraData = new CameraData();
        private UBO<CameraData> UBO;
        private Vector3 direction = Vector3.UnitZ;

        private Framebuffer frameBuffer;

        private Input input;
        private WindowSettings windowSettings;

        private Scene renderScene;
        private bool isRenderingToTexture = false;

        #endregion

        #region Properties
        public float Speed { get; set; } = 5.5f;
        public float FOV { get; set; } = 60.0f;
        public float RotationSpeed { get; set; } = 0.025f;
        public bool RenderToTexture = false;

        public Scene RenderScene { get => renderScene; }
        public CameraData CameraData { get => cameraData; }

        public bool InputEnabled {get; set;} = true;
        #endregion

        public CameraComponent(Input input, WindowSettings windowSettings)
        {
            this.input = input;
            this.windowSettings = windowSettings;
        }

        public CameraComponent(Input input, WindowSettings windowSettings, bool RenderToTexture, Scene renderSceneTarget) 
            : this(input, windowSettings)
        {
            this.RenderToTexture = RenderToTexture;
            this.renderScene = renderSceneTarget;
        }

        public override void OnStart(Entity entity)
        {
            base.OnStart(entity);

            flags.Add(ComponentFlag.SingletonFlag);

            cameraData.UpdateCameraData(entity.position, direction, FOV, (int)windowSettings.GetFrameBufferSize().X, (int)windowSettings.GetFrameBufferSize().Y);
            UBO = new UBO<CameraData>(ref cameraData, "CameraData", 0);

            // if (RenderToTexture) 
            // {
            //     frameBuffer = new Framebuffer(windowSettings);
            // }                       
        }

        public override void OnUpdate(Entity entity)
        {
            base.OnUpdate(entity);

            // if (RenderToTexture)
            // {
            //     //Scene.SetCurrentScene(renderScene);
            //     if (!isRenderingToTexture)
            //     {
            //         Entity previousCamera = renderScene.CurrentCamera;
            //         isRenderingToTexture = true;

            //         frameBuffer.Bind();

            //         GL.Enable(EnableCap.DepthTest);
            //         GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

            //         Scene.currentScene.SetCurrentCamera(entity);
            //         Scene.UpdateCurrentScene();
            //         frameBuffer.Unbind();

            //         isRenderingToTexture = false;

            //         Scene.currentScene.SetCurrentCamera(previousCamera);
            //     }

            // }
            Input();
        }

        private void Input()
        {
            #region Input
            if (InputEnabled)
            {
                //Keyboard
                Vector3 right = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, direction));
                Vector3 up = Vector3.Normalize(Vector3.Cross(direction, right));
                Vector3 forward = Vector3.Normalize(Vector3.Cross(right, up));

                if (input.KeyboardCallBack().IsKeyDown(Keys.W)) Entity.position += forward * Speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.S)) Entity.position -= forward * Speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.A)) Entity.position += right * Speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.D)) Entity.position -= right * Speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.E)) Entity.position += up * Speed * Time.deltaTime;
                if (input.KeyboardCallBack().IsKeyDown(Keys.Q)) Entity.position -= up * Speed * Time.deltaTime;

                //FOV -= Input.MouseCallBack().ScrollDelta.Y; //gotta be removed

                //Mouse
                Entity.rotation.X -= input.MouseCallBack().Delta.Y * RotationSpeed;
                Entity.rotation.Y += input.MouseCallBack().Delta.X * RotationSpeed;
            }
            #endregion

            direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(Entity.rotation.X)) * (float)Math.Cos(MathHelper.DegreesToRadians(Entity.rotation.Y));
            direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(Entity.rotation.X));
            direction.Z = (float)Math.Cos(MathHelper.DegreesToRadians(Entity.rotation.X)) * (float)Math.Sin(MathHelper.DegreesToRadians(Entity.rotation.Y));
            direction = Vector3.Normalize(direction);

            Entity.rotation.X = Math.Clamp(Entity.rotation.X, -89.0f, 89.0f);
            
            
            cameraData.UpdateCameraData(Entity.position, direction, FOV, (int)windowSettings.GetFrameBufferSize().X, (int)windowSettings.GetFrameBufferSize().Y);
            UBO.UpdateUBO(ref cameraData);
        }

        public Texture GetTexture() => frameBuffer.texture;
    }
}
