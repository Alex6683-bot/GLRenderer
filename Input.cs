using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GLRenderer
{
    public static class Input
    {
        private static MouseState mouseState;
        private static KeyboardState keyboardState;
        public static void ConfigureInput(MouseState mouse, KeyboardState keyboard)
        {
            mouseState = mouse;
            keyboardState = keyboard;
        }

        public static MouseState MouseCallBack()
        {
            if (mouseState != null) return mouseState;
            throw new Exception("MouseState not configured");
        }

        public static KeyboardState KeyboardCallBack()
        {
            if (keyboardState != null) return keyboardState;
            throw new Exception("KeyboardState not configured");
        }
    }
}
