using System;

namespace GLComponentSystem
{
    class ComponentFlagException : Exception
    {
        public ComponentFlagException(string message) : base(message) { }
    }
}