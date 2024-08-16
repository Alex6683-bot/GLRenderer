using GLEntitySystem;
using GLRenderer.Components;
using GLRenderer.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace GLRenderer.Rendering
{
    public abstract class Scene
    {
        private static List<Scene> _instancedScenes = new List<Scene>();
        public static List<Scene> instancedScenes { get =>  _instancedScenes; }
        public static Scene currentScene { get; private set; }
        public Entity currentCamera { get; set; }

        public Scene()
        {
            _instancedScenes.Add(this);
        }
        public virtual void LoadScene() { }
        public virtual void UpdateScene() 
        {
            if (this != currentScene) throw new SceneNotActiveException(this);
        }

        public static void SetCurrentScene(Scene scene)
        {
            currentScene = scene;
        }

    }
}
