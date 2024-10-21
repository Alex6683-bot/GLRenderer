using GLRenderer.SceneSystem;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;


namespace GLRenderer.Test
{
    class Window : GameWindow
    {
        SampleScene smpScene;
        public Window(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            Size = (width, height),
            Title = title
        })
        { }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.0f, 0.0f, 0.1f, 1.0f);
            CursorState = CursorState.Grabbed;

            smpScene = new SampleScene(this);

            Scene.SetCurrentScene(smpScene);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.DepthTest);

            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Scene.UpdateCurrentScene();
            Time.Update(args);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

        }
        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }
    }
}