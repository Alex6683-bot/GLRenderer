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
        int VAO;
        public MeshRenderer(MeshData meshData)
        {
            _meshData = meshData;

            renderCamera = (CameraController)meshData.camera.GetComponent<CameraController>();
            if (renderCamera == null) throw new ComponentNotFoundException<CameraController>(meshData.camera);
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

            GL.VertexAttribPointer(meshData.shader.GetAttribLocation("vertexPosition"), 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(meshData.shader.GetAttribLocation("vertexPosition"));

            //index buffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, meshData.indices.Length * sizeof(uint), meshData.indices, BufferUsageHint.StaticDraw);

            //texture coord buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, texCoordVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, meshData.texCoords.Length * sizeof(float), meshData.texCoords, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(meshData.shader.GetAttribLocation("texCoords"), 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(meshData.shader.GetAttribLocation("texCoords"));

            GL.BindVertexArray(0);
        }

        private void SetUniforms()
        {
            Matrix4 model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(renderRotation.X)) *
                            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(renderRotation.Y)) *
                            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(renderRotation.Z)) *
                            Matrix4.CreateScale(renderScale) *
                            Matrix4.CreateTranslation(renderPosition);

            meshData.shader.SetUniformMatrix4("model", model);
        }

        public void RenderMesh()
        {
            GL.BindVertexArray(VAO);
            meshData.shader.RunShader();
            SetUniforms();
            renderCamera.SetEyeSpace(meshData.shader);

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

        //Shader
        public Shader shader { get; set; } = ShaderSource.defaultShader;

        //Camera
        public Entity camera { get; set; }

        public MeshData() { }
    }
}
