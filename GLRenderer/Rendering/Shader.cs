using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;

namespace GLRenderer.Rendering
{

    public enum ShaderInitType
    {
        Source, FilePath
    }
    public class Shader
    {
        public int ID;
        private static List<Shader> _instancedShaders = new List<Shader>();
        public static List<Shader> instancedShaders {get =>  _instancedShaders;}

        public Shader(ShaderInitType initType, string vertex, string fragment)
        {
            //Reading Shader Code
            
            string vertexShaderSource = "";
            string fragmentShaderSource = "";

            if (initType == ShaderInitType.FilePath)
            {
                vertexShaderSource = File.ReadAllText(vertex);
                fragmentShaderSource = File.ReadAllText(fragment);
            }
            else if (initType == ShaderInitType.Source)
            {
                vertexShaderSource = vertex;
                fragmentShaderSource = fragment;
            }
            
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

            _instancedShaders.Add(this);
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

        public void SetUniformVector2(string name, Vector2 value)
        {
           int location = GL.GetUniformLocation(ID, name); 
           GL.Uniform2(location, value);
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

        public void SetUniformMatrix2(string name, Matrix2 value)
        {
           int location = GL.GetUniformLocation(ID, name); 
           GL.UniformMatrix2(location, true, ref value);
        }

        public void SetUniformMatrix3(string name, Matrix3 value)
        {
           int location = GL.GetUniformLocation(ID, name); 
           GL.UniformMatrix3(location, true, ref value);
        }

        public void SetUniformMatrix4(string name, Matrix4 value)
        {
           int location = GL.GetUniformLocation(ID, name); 
           GL.UniformMatrix4(location, true, ref value);
        }

    }
}


