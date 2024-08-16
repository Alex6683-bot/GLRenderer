using GLEntitySystem;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using GLRenderer.Rendering;

namespace GLRenderer.Components
{
    public class MeshRenderer : IComponent
    {
        private MeshData _meshData;
        public MeshData meshData { get => _meshData; }

        //Rendering
        private Vector3 renderPosition = new Vector3(0);
        private Vector3 renderRotation = new Vector3(0);
        private Vector3 renderScale = new Vector3(1);
        private CameraController renderCamera;

        public Material material { get; set; } = new Material();
        int VAO;
        public MeshRenderer(MeshData meshData, Entity camera)
        {
            _meshData = meshData;

            renderCamera = (CameraController)camera.GetComponent<CameraController>();
            if (renderCamera == null) throw new ComponentNotFoundException<CameraController>(camera);
        }
        public void OnStart(Entity entity)
        {
            InitializeRendering();
        }

        public void OnUpdate(Entity entity)
        {
            renderPosition = entity.position;
            renderRotation = entity.rotation;
            renderScale = entity.scale;
            RenderMesh();
        }

        #region Rendering
        public void InitializeRendering()
        {
            int vertexVBO, texCoordVBO, normalVBO;
            int EBO;

            VAO = GL.GenVertexArray();

            GL.BindVertexArray(VAO);

            vertexVBO = GL.GenBuffer();
            texCoordVBO = GL.GenBuffer();
            normalVBO = GL.GenBuffer();

            EBO = GL.GenBuffer();

            //vertex buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, meshData.vertices.Length * sizeof(float), meshData.vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(material.shader.GetAttribLocation("vertexPosition"), 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(material.shader.GetAttribLocation("vertexPosition"));

            //index buffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, meshData.indices.Length * sizeof(uint), meshData.indices, BufferUsageHint.StaticDraw);

            //texture coord buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, texCoordVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, meshData.texCoords.Length * sizeof(float), meshData.texCoords, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(material.shader.GetAttribLocation("texCoords"), 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(material.shader.GetAttribLocation("texCoords"));

            GL.BindVertexArray(0);
        }

        private void SetUniforms()
        {
            Matrix4 model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(renderRotation.X)) *
                            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(renderRotation.Y)) *
                            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(renderRotation.Z)) *
                            Matrix4.CreateScale(renderScale) *
                            Matrix4.CreateTranslation(renderPosition);

            material.shader.SetUniformMatrix4("model", model);
        }

        public void RenderMesh()
        {
            GL.BindVertexArray(VAO);
            material.shader.RunShader();

            material.SetUniforms();
            SetUniforms();
            renderCamera.SetEyeSpace(material.shader);

            GL.DrawElements(PrimitiveType.Triangles, meshData.indices.Length, DrawElementsType.UnsignedInt, 0);
        }
        #endregion
    }

    public struct MeshData
    {
        private float[] _texCoords;
        private float[] _normals;
        public float[] vertices { get; set; }
        public uint[] indices { get; set; }
        public float[] normals { get; set; } = new float[] { };
        public float[] texCoords { get; set; } = new float[] { };
        public MeshData() { }

        //Pre defined meshes
        public static MeshData cube = new MeshData()
        {
            vertices = new float[]
            {
                // Front face
                -0.5f, -0.5f,  0.5f,  // Bottom-left
                 0.5f, -0.5f,  0.5f,  // Bottom-right
                 0.5f,  0.5f,  0.5f,  // Top-right
                -0.5f,  0.5f,  0.5f,  // Top-left

                // Back face
                -0.5f, -0.5f, -0.5f,  // Bottom-left
                 0.5f, -0.5f, -0.5f,  // Bottom-right
                 0.5f,  0.5f, -0.5f,  // Top-right
                -0.5f,  0.5f, -0.5f,  // Top-left
            },
            indices = new uint[]
            {
                // Front face
                0, 1, 2,
                2, 3, 0,

                // Right face
                1, 5, 6,
                6, 2, 1,

                // Back face
                7, 6, 5,
                5, 4, 7,

                // Left face
                4, 0, 3,
                3, 7, 4,

                // Bottom face
                4, 5, 1,
                1, 0, 4,

                // Top face
                3, 2, 6,
                6, 7, 3
            },
            texCoords = new float[]
            {
                // Front face
                0.0f, 0.0f,  // Bottom-left
                1.0f, 0.0f,  // Bottom-right
                1.0f, 1.0f,  // Top-right
                0.0f, 1.0f,  // Top-left

                // Back face
                1.0f, 0.0f,  // Bottom-left
                0.0f, 0.0f,  // Bottom-right
                0.0f, 1.0f,  // Top-right
                1.0f, 1.0f,  // Top-left
             }
        };

    }
    public class Material
    {
        public Vector4 color = new Vector4(0.5f, 1.0f, 0.5f, 1.0f);
        public Shader shader { get; set; } = ShaderSource.defaultShader;

        public void SetUniforms()
        {
            shader.SetUniformVector4("color", color);
        }
    }
}
