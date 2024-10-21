using OpenTK;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Runtime.CompilerServices;

namespace GLRenderer
{
    public class Input
    {
        private MouseState mouseState;
        private KeyboardState keyboardState;
        public void ConfigureInput(MouseState mouse, KeyboardState keyboard)
        {
            mouseState = mouse;
            keyboardState = keyboard;
        }

        public MouseState MouseCallBack()
        {
            if (mouseState != null) return mouseState;
            throw new NullReferenceException("MouseState not configured");
        }

        public KeyboardState KeyboardCallBack()
        {
            if (keyboardState != null) return keyboardState;
            throw new NullReferenceException("KeyboardState not configured");
        }
    }
}