using System;
using GLRenderer.SceneSystem;


namespace GLRenderer.Test
{
    class Program
    {
        public static void Main()
        {
            using Window window = new Window(1200, 1200, "GLRenderer");
            window.Run();
            Console.ReadKey();
        }

    }
}