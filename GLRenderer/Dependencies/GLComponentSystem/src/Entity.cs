using OpenTK.Mathematics;
using System.Diagnostics;

namespace GLComponentSystem
{
    public class Entity
    {
        private List<Component> _components;
        public List<Component> components { get => _components; }

        //Origin
        public Vector3 position = new Vector3(0);
        public Vector3 scale = new Vector3(1);
        public Vector3 rotation = new Vector3(0);

        //Callback
        public static Action<Entity, Component> OnComponentAdded { get; set; }
        public static Action<Entity, Component> OnComponentRemoved { get; set; }

        //Flagging
        #region FlagCallbacks
        private void SingleTonFlagCallBack(Entity entity, Component component, ComponentFlag flag)
        {
            List<Component> components = entity.components.Where(x => x.GetType() == component.GetType()).ToList();
            if (components.Count > 1) //Check for duplicate components
                throw new ComponentFlagException($"Cannot add duplicate component of {component} with flag: {flag}");
        }
        #endregion

        private Dictionary<ComponentFlag, Action<Entity, Component, ComponentFlag>> _flagActionLookup;
        private Dictionary<ComponentFlag, Action<Entity, Component, ComponentFlag>> flagActionLookup
        {
            get
            {
                _flagActionLookup ??= new()  //assigning if null
                {
                    {ComponentFlag.SingletonFlag,  SingleTonFlagCallBack}
                };
                return _flagActionLookup;
            }
        }
        
        public Entity()
        {
            _components = new List<Component>();
        }

        public void Update()
        {
            foreach (var component in _components) component.OnUpdate(this);
        }

        /// <summary>
        /// Gets the first component of the given type from entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Component deriving from IComponent interface. Returns null if not found</returns>
        public T? GetComponent<T>() where T : Component
        {
            return (T)_components.Where(x => x.GetType() == typeof(T)).FirstOrDefault();
        }

        /// <summary>
        /// Gets components of the given type from entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List of components deriving of the give type. Returns null if not found</returns>
        public List<T>? GetComponents<T>() where T : Component
        {
            List<Component> components = _components.Where(x => x.GetType() == typeof(T)).ToList();
            return new List<T>(components.Cast<T>());
        }

        /// <summary>
        /// Returns all the components in the entity
        /// </summary>
        /// <returns></returns>
        public List<Component>? GetComponents()
        {
            return _components;
        }

        /// <summary>
        /// Checks if given component type is attached to entity
        /// </summary>
        /// <returns>True if exists, if not false</returns>
        public bool HasComponent<T>() where T : Component
        {
            return GetComponent<T>() != null;
        }

        /// <summary>
        /// Adds a component deriving from Component base class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public T AddComponent<T>(T component) where T : Component
        {
            _components.Add(component);
            OnComponentAdded?.Invoke(this, component);
            component.OnStart(this);

            foreach (ComponentFlag componentFlag in component.flags) flagActionLookup[componentFlag].Invoke(this, component, componentFlag);

            return component;
        }


        /// <summary>
        /// Removes a component deriving from Component base clas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns>Returns whether the removal was successful</returns>
        public bool RemoveComponent<T>(T component) where T : Component
        {
            bool success = _components.Remove(component);
            OnComponentRemoved?.Invoke(this, component);

            component.OnUnload(this);

            return success;
        }

        /// <summary>
        /// Unloads the given entity and clears all the components attached to the entity
        /// </summary>
        public void Destroy()
        {
            for (int i = 0; i < components.Count; i++) RemoveComponent(components[i]);
        }

    }
}
