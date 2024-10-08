using System;
using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;


namespace GLRenderer.SceneSystem
{
    public class WindowSettings
    {
        GameWindow gameWindow;

        public void Configure(GameWindow _gameWindow)
        {
            gameWindow = _gameWindow;
        }

        public Vector2 GetFrameBufferSize()
        {
            if (gameWindow == null) throw new NullReferenceException("Window settings is not configured to a valid game window class");
            return gameWindow.FramebufferSize;
        }

        public string GetCurrentWindowTitle()
        {
            if (gameWindow == null) throw new NullReferenceException("Window settings is not configured to a valid game window class");
            return gameWindow.Title;
        }
    }
}