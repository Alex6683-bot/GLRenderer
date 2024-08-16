using GLEntitySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLRenderer
{
    class ComponentNotFoundException<T> : Exception where T : IComponent
    {
        public ComponentNotFoundException(Entity entity)
        {
            Console.WriteLine($"{entity} doesn't contain component of type {typeof(T)}");
        }
    }
}
