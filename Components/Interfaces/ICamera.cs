using GLRenderer.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLRenderer.Components
{
    public interface ICamera
    {
        public void SetEyeSpace(Shader shader);
    }
}
