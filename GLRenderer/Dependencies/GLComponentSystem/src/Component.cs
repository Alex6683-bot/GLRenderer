namespace GLComponentSystem
{
    public abstract class Component
    {
        public List<ComponentFlag> flags { get; } = new List<ComponentFlag>();
        public Entity Entity;

        public virtual void OnStart(Entity entity)
        {
            Entity = entity;
        }

        public virtual void OnUpdate(Entity entity)
        { }

        public virtual void OnUnload(Entity entity)
        { }

    }
}
