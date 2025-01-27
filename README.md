# GL Renderer
This library offers a set of abstraction and classes for rendering meshes and scenes easier in OpenTK.

## Dependencies
* [Open TK](https://github.com/opentk/opentk) 
* [GLComponentSystem](https://github.com/Alex6683-bot/GLEntitySystem) (link is unavailable but it is embedded in the project files)
* [StbImageSharp](https://github.com/StbSharp/StbImageSharp)

## How it works
GL Renderer comes with the ```Scene``` base class which is derived by a child class through which, 'entities' are used.

```
class SampleScene : Scene
{
    protected override void LoadScene()
    {
        //Scene loading logic here
    }
    protected override void UpdateScene()
    {
        //Scene update logic here
    }
    
}
```
### Rendering the scene
In order to render the created scene, you use the scene in the ```GameWindow``` class you create through the OpenTK library. See [OpenTK docs](https://opentk.net/learn/chapter1/2-hello-triangle.html?tabs=onload-opentk4%2Conrender-opentk4%2Cresize-opentk4).

**The full documentation is under construction and you can currently refer the project's workings under the TestProj csproj in this repo** 

