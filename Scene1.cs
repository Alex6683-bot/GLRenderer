using GLComponentSystem;
using GLRenderer;
using GLRenderer.Components;
using GLRenderer.Rendering;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using GLRenderer.SceneSystem;
using OpenTK.Graphics.ES20;

class Scene1 : Scene
{
    Framebuffer testFrameBuffer;
    Entity light;

    public Scene1(GameWindow gameWindow) : base(gameWindow) { }

    protected override void LoadScene()
    {
        Shader rubixCubeShader = new Shader(ShaderInitType.FilePath, Extension.ToRelativePath(@"Scenes\Shaders\VERTEX\cubeShader.vert"),
           Extension.ToRelativePath(@"Scenes\Shaders\FRAGMENT\rubiksCubeShader.frag"));
        
        Shader checkerBoard = new Shader(ShaderInitType.FilePath, Extension.ToRelativePath(@"Scenes\Shaders\VERTEX\cubeShader.vert"),
           Extension.ToRelativePath(@"Scenes\Shaders\FRAGMENT\checkerboard.frag"));

        Shader lightShader = new Shader(ShaderInitType.FilePath, Extension.ToRelativePath(@"Scenes\Shaders\VERTEX\cubeShader.vert"),
           Extension.ToRelativePath(@"Scenes\Shaders\FRAGMENT\lightShader.frag"));
        
        // Shader chessFrameShader = new Shader(ShaderInitType.FilePath, Extension.ToRelativePath(@"Scenes\Shaders\VERTEX\cubeShader.vert"),
        //    Extension.ToRelativePath(@"Scenes\Shaders\FRAGMENT\ChessFrameShader.frag"));

        Material brickMaterial = new Material();
        brickMaterial.texture = new Texture(Extension.ToRelativePath(@"Scenes\Textures\bricks.png"))
        {
            textureMagFilter = OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest,
            textureMinFilter = OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest
        };

        Material glowStoneMaterial = new Material();
        glowStoneMaterial.texture = new Texture(Extension.ToRelativePath(@"Scenes\Textures\glowstone.png"))        
        {
            textureMagFilter = OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest,
            textureMinFilter = OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest
        };
        glowStoneMaterial.shader = lightShader;
        
        base.LoadScene();


        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                Entity brick = Instantiate();
                brick.position = new Vector3(x, 0.6f, y);
                brick.AddComponent<MeshRenderer>(new MeshRenderer(MeshData.cube)).material = brickMaterial;  
            }
        }

        Entity chessBoard = Instantiate();
        chessBoard.scale = new Vector3(15.0f, 0, 15.0f);
        chessBoard.scale.Y = 0.3f;
        //chessBoard.position.Y = 2f;
        chessBoard.AddComponent<MeshRenderer>(new MeshRenderer(MeshData.cube)).material.shader = checkerBoard;

        
        light = Instantiate();
        light.position = new Vector3(0, 6f, 0f);
        light.AddComponent<LightComponent>(new LightComponent()).color = new Vector4(1.0f, 0.925f, 0.5f, 1.0f);
        light.AddComponent<MeshRenderer>(new MeshRenderer(MeshData.cube)).material = glowStoneMaterial;
        light.GetComponent<LightComponent>().diffuseBrightness = 0.3f;


        Entity camera = Instantiate();
        camera.position = new Vector3(0, 10, 0);
        camera.AddComponent<CameraComponent>(new CameraComponent(Input, WindowSettings));


        SetCurrentCamera(camera);
        //camera.RemoveComponent<CameraComponent>(camera.GetComponent<CameraComponent>());
        //camera1.RemoveComponent<CameraComponent>(camera1.GetComponent<CameraComponent>());
        //rubixCube.RemoveComponent<MeshRenderer>(rubixCube.GetComponent<MeshRenderer>());
    }

    protected override void UpdateScene()
    {
        //testFrameBuffer.Bind();
        base.UpdateScene();
        if (Input.KeyboardCallBack().IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.I)) Destroy(light);
        //Console.WriteLine(Time.deltaTime);
    }
}

