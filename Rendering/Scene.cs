using GLEntitySystem;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;

namespace GLRenderer.Rendering
{
    public abstract class Scene
    {
        private static List<Scene> _instancedScenes = new List<Scene>();
        public static List<Scene> instancedScenes { get => _instancedScenes; }
        public static Scene currentScene { get; private set; }
        public List<Entity> entities { get; private set; } = new List<Entity>();    
        private GameWindow _gameWindow;
        public GameWindow GameWindow {get => _gameWindow;}  

        public Scene(GameWindow gameWindow)
        {
            _instancedScenes.Add(this);
            this._gameWindow = gameWindow;
            this.LoadScene();
        }
        protected virtual void LoadScene() { }
        protected virtual void UpdateScene()
        {
            foreach (Entity entity in entities)
            {
                entity.Update();
            }
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
            currentScene = scene;
        }
    }
}
