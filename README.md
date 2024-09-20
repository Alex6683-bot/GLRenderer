# GL Renderer
This library offers a set of abstraction and classes to make rendering meshes in OpenTK easier.

## Current issues
* Directory path issues (will be fixed in the next patch)

## Dependcies
* [Open TK](https://github.com/opentk/opentk) 
* [GLEntitySystem](https://github.com/Alex6683-bot/GLEntitySystem)

## How it works
GL Renderer comes with the ```Scene``` base class which is deived by a child class through which, the meshes are rendered.

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
