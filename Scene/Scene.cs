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
        private EntityManager entityManager = new EntityManager();
        private GameWindow gameWindow;
        private WindowSettings windowSettings = new WindowSettings();
        private Input input = new Input();
        private Entity currentCamera;
        #endregion

        #region Properties
        public GameWindow GameWindow {get => gameWindow;}  
        public WindowSettings WindowSettings {get => windowSettings;}
        public Input Input {get => input;}
        public Entity CurrentCamera{get => currentCamera;} 
        #endregion

        public static Scene currentScene { get; private set; }   
        public static List<Scene> instancedScenes { get => _instancedScenes; }

        public Scene(GameWindow gameWindow)
        {
            _instancedScenes.Add(this);
            this.gameWindow = gameWindow;
            
            windowSettings.Configure(gameWindow);
            input.ConfigureInput(gameWindow.MouseState, gameWindow.KeyboardState);

            this.LoadScene();
        }

        #region Scene Management
        protected virtual void LoadScene() { }
        protected virtual void UpdateScene()
        {
            foreach (Entity entity in entityManager.GetEntities())
            {
                entity.Update();
            }
            UpdateCurrentCamera();
        }

        protected virtual void UnloadScene()
        {
            entityManager.Unload();
        }
        public static void UpdateCurrentScene(FrameEventArgs args) //Pass the open gl frame event args from window update
        {
            if (currentScene == null) throw new NullReferenceException("Active scene is not initialized");
            currentScene.UpdateScene();

            //Store information such as delta time
            Time.Update(args);
        }
        public static void SetCurrentScene(Scene scene)
        {
            currentScene?.UnloadScene();
            currentScene = scene;
        }

        private void UpdateCurrentCamera()
        {
            if (!currentCamera.HasComponent<CameraComponent>() ||
                currentCamera == null) throw new NullReferenceException("Valid camera entity is not initialized in the scene");
            
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
            entity.Destory();
            entityManager.RemoveEntity(entity);
        }
        #endregion

    }
}
