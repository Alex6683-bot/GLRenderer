using OpenTK;
using OpenTK.Windowing.Common;

namespace GLRenderer.Rendering
{
    public static class Time
    {
        public static float deltaTime {get; private set;}
        public static void Update(FrameEventArgs args)
        {
            deltaTime = (float)args.Time;
        }
    }
}