using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GLRenderer.Rendering
{
    public class Shader
    {
        public int ID;

        public Shader(string vertexShaderPath, string fragmentShaderPath)
        {
            //Reading Shader Code
            string vertexShaderSource = File.ReadAllText(vertexShaderPath);
            string fragmentShaderSource = File.ReadAllText(fragmentShaderPath);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            //Compile both shaders
            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int successv);
            if (successv == 0)
            {
                string infoLog = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(fragmentShader);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int successf);
            if (successf == 0)
            {
                string infoLog = GL.GetShaderInfoLog(fragmentShader);
                Console.WriteLine(infoLog);
            }

            //Create shader program
            ID = GL.CreateProgram();

            GL.AttachShader(ID, vertexShader);
            GL.AttachShader(ID, fragmentShader);

            GL.LinkProgram(ID);
        }

        public void RunShader()
        {
            GL.UseProgram(ID);
        }
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(ID, attribName);
        }

        //Uniform Setting

        public void SetUniformFloat(string name, float value)
        {
            int location = GL.GetUniformLocation(ID, name);
            GL.Uniform1(location, value);
        }
        public void SetUniformMatrix4(string name, Matrix4 value)
        {
            int location = GL.GetUniformLocation(ID, name);
            GL.UniformMatrix4(location, true, ref value);
        }
        public void SetUniformVector3(string name, Vector3 value)
        {
            int location = GL.GetUniformLocation(ID, name);
            GL.Uniform3(location, value);
        }
        public void SetUniformVector4(string name, Vector4 value)
        {
            int location = GL.GetUniformLocation(ID, name);
            GL.Uniform4(location, value);
        }

    }
}


