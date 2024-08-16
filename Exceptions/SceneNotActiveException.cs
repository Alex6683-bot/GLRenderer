using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLRenderer.Rendering
{
    public class SceneNotActiveException : Exception
    {
        public SceneNotActiveException(Scene scene) 
        {
            Console.WriteLine($"Attempted to use unactive scene {scene}");
        }
    }
}
