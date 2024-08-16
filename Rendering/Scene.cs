using GLEntitySystem;
using GLRenderer.Components;
using GLRenderer.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace GLRenderer
{
    class Scene
    {
        private Entity _camera;
        public Entity camera 
        { get; set; }

        public virtual void LoadScene() { }
        public virtual void UpdateScene() { }
    }
}
