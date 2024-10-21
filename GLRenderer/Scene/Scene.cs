using GLComponentSystem;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using GLRenderer.Components;

namespace GLRenderer.SceneSystem
{
    public abstract class Scene
    {
        #region Fields
        private static List<Scene> _instancedScenes = new List<Scene>();
        private EntityManager entityManager;

        private GameWindow gameWindow;
        private WindowSettings windowSettings;
        private Input input = new Input();

        private Entity currentCamera;

        public static Scene currentScene { get; private set; }   
        public static List<Scene> instancedScenes { get => _instancedScenes; }
        #endregion

        #region Properties
        public GameWindow GameWindow {get => gameWindow;}  
        public WindowSettings WindowSettings {get => windowSettings;}
        public Input Input {get => input;}

        public Entity CurrentCamera{get => currentCamera;} 
        #endregion


        public Scene(GameWindow gameWindow)
        {
            _instancedScenes.Add(this);
            this.gameWindow = gameWindow;
            
            windowSettings = new WindowSettings();

            windowSettings.Configure(gameWindow);
            input.ConfigureInput(gameWindow.MouseState, gameWindow.KeyboardState);
        }

        #region Scene Management
        protected virtual void LoadScene() 
        {
            entityManager = new EntityManager();
        }

        protected virtual void UpdateScene()
        {
            foreach (Entity entity in entityManager.GetEntities())
            {
                entity.Update();
            }
            UpdateCameras();
        }

        protected virtual void UnloadScene()
        {
            entityManager.Unload();
        }
        
        public static void UpdateCurrentScene() //Pass the open gl frame event args from window update
        {
            if (currentScene == null) throw new NullReferenceException("Active scene is not initialized");
            currentScene.UpdateScene();

            //Store information such as delta time
        }

        public static void SetCurrentScene(Scene scene)
        {
            scene.LoadScene();
            currentScene?.UnloadScene();
            currentScene = scene;
        }

        private void UpdateCameras() //Only updates the current camera and camera with render to texture enbaled
        {
            if (currentCamera == null || !currentCamera.HasComponent<CameraComponent>())
                throw new NullReferenceException("Valid camera entity is not initialized in the scene");
            
            foreach (Entity camera in entityManager.GetCameras())
            {
                if (camera.GetComponent<CameraComponent>().RenderToTexture) camera.Update();
            }
            currentCamera.Update();
        }
        public void SetCurrentCamera(Entity cameraEntity)
        {
            if (cameraEntity.HasComponent<CameraComponent>()) currentCamera = cameraEntity;
            else throw new Exception($"Attempted to set current camera to entity that doesn't hold component of type: {typeof(CameraComponent)}");
        }
        #endregion

        #region Entity Management
        public Entity Instantiate()
        {
            Entity entity = new Entity();
            entityManager.AddEntity(entity);
            return entity;
        }

        public void Destroy(Entity entity)
        {
            entityManager.RemoveEntity(entity);
        }
        #endregion

    }
}
