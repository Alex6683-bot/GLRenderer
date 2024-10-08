using System.Text;
using GLComponentSystem;
using GLRenderer.Components;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GLRenderer.SceneSystem
{
    class EntityManager
    {
        public EntityManager()
        {
            //Subscribe to entity callbacks
            Entity.OnComponentAdded += AppendCamera;
            Entity.OnComponentRemoved += RemoveCamera;
        }
        private List<Entity> entities = new List<Entity>();
        private List<Entity> cameras = new List<Entity>();

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }
        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public void AppendCamera(Entity entity, Component component)
        {
            if (component.GetType() == typeof(CameraComponent))
            {
                entities.Remove(entity);
                cameras.Add(entity);
            }
        }
        public void RemoveCamera(Entity entity, Component component)
        {
            if (component.GetType() == typeof(CameraComponent))
            {
                cameras.Remove(entity);
                entities.Add(entity);
            }
        }
        
        public void Unload()
        {
            Entity.OnComponentAdded -= AppendCamera;
            Entity.OnComponentRemoved -= RemoveCamera;
        }
        public Entity[] GetEntities() => entities.ToArray();
        public Entity[] GetCameras() => cameras.ToArray();
    }
}