using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GLRenderer
{
    public class ShaderUniform<T>
    {
        public T Value {get; private set;}
        public string Name {get; private set;}

        public ShaderUniform(string name, ref T value)
        {
            Name = name;
            Value = value;
        }

        public void Update(int shaderHandle)
        {
            int location = GL.GetUniformLocation(shaderHandle, Name);

            if (Value == null || Name == null || location == -1) return;

            if (typeof(T) == null) return;
            else if (typeof(T) == typeof(float)) GL.Uniform1(location, (float)(object)Value);
            else if (typeof(T) == typeof(Vector2)) GL.Uniform2(location, (Vector2)(object)Value);
            else if (typeof(T) == typeof(Vector3)) GL.Uniform3(location, (Vector3)(object)Value);
            else if (typeof(T) == typeof(Vector4)) GL.Uniform4(location, (Vector4)(object)Value);
            else if (typeof(T) == typeof(Matrix2))
            {
                 Matrix2 value = (Matrix2)(object)Value;
                 GL.UniformMatrix2(location, true, ref value);
            }
            else if (typeof(T) == typeof(Matrix3))
            {
                 Matrix3 value = (Matrix3)(object)Value;
                 GL.UniformMatrix3(location, true, ref value);
            }
            else if (typeof(T) == typeof(Matrix4))
            {
                 Matrix4 value = (Matrix4)(object)Value;
                 GL.UniformMatrix4(location, true, ref value);
            }
        }
    }
}