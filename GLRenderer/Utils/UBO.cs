using GLRenderer.Components;
using OpenTK.Graphics.OpenGL;
using System.Runtime.CompilerServices;

namespace GLRenderer.Rendering
{
    public class UBO<T> where T : unmanaged
    {
        public int ID { get; private set; }
        public int bindingPoint;

        public UBO(ref T data, string name, int bindingPoint)
        {
            ID = GL.GenBuffer();
            this.bindingPoint = bindingPoint;
            
            Bind();
            foreach (Shader shader in Shader.instancedShaders)
            {
                int Index = GL.GetUniformBlockIndex(shader.ID, name);
                //Console.WriteLine(Index);
                GL.UniformBlockBinding(shader.ID, Index, bindingPoint);
            }
            GL.BufferData(BufferTarget.ArrayBuffer, Unsafe.SizeOf<T>(), ref data, BufferUsageHint.DynamicDraw);
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, bindingPoint, ID);
            UnBind();
        }

        public void UpdateUBO(ref T data)
        {
            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, Unsafe.SizeOf<T>(), ref data, BufferUsageHint.DynamicDraw);
            UnBind();
        }
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, bindingPoint, ID);
        }
        public void UnBind() => GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

    }
}
