using GLComponentSystem;
using GLRenderer.Components;
using GLRenderer.SceneSystem;
using OpenTK.Windowing.Desktop;


namespace GLRenderer.Test
{
    internal class SampleScene : Scene
    {
        public SampleScene(GameWindow gameWindow) : base(gameWindow)
        {
        }

        protected override void LoadScene()
        {
            base.LoadScene();
            Entity camera = Instantiate();
            Entity light = Instantiate();
            Entity cube = Instantiate();

            camera.AddComponent<CameraComponent>(new CameraComponent(Input, WindowSettings));
            light.AddComponent<LightComponent>(new LightComponent());
            cube.AddComponent<MeshRenderer>(new MeshRenderer(MeshData.cube));

            light.position.Y = 5;
            light.position.X = 5;

            SetCurrentCamera(camera);
        }

        protected override void UpdateScene()
        {
            base.UpdateScene();
            if (Input.KeyboardCallBack().IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape)) Environment.Exit(69);
        }
    }
}

