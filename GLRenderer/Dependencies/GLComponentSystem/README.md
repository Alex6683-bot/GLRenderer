# GLComponentSystem
GLComponentSystem is a simple component framework I made to experiment and learn Component Architecture concepts by applying it in my Open GL projects. It may not be the most efficient, but the code will keep improving and will be added new and new features and optmisations. 
At basic level, the framework consists of entities that could hold components. Components determine how the entity acts before and during runtime.
Currently, these entities are used to render meshes in an OpenGL context but more sophisticated components will be added.
## Create Entity
Entities are created with the entity class 
```
Entity entity = new Entity()
```
To add components, use the ```AddComponent()``` method.
```
entity.AddComponent<T>(new T())
```

Additionally, you can also get a specific component or get all the components from the entity.
```
entity.GetComponent<T>() //Gets the first component of the given type
entity.GetComponents<T>() //Gets all the components of the type
```

## Create Components
Components can be created by inheriting the	```Component``` base class.
```
class CustomComponent : Component
{
	public void OnStart(Entity entity) { }
	public void OnUpdate(Entity entity) { }
}
```
The Component class consists of the ```OnStart()``` and ```Onupdate()``` methods. 
```OnStart()```is executed as soon as the component is added.
```OnUpdate()``` is executed every frame.

The two methods are also passed with a reference of the parent entity. So you could modify it's behaviour.