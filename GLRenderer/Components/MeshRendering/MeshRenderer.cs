using GLComponentSystem;
using GLRenderer.Rendering;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GLRenderer.Components
{
    public class MeshRenderer : Component
    {
        private MeshData _meshData;
        public MeshData meshData { get => _meshData; }

        //Rendering
        private Vector3 renderPosition = new Vector3(0);
        private Vector3 renderRotation = new Vector3(0);
        private Vector3 renderScale = new Vector3(1);
        private Material _material = new Material();
        public Material material { get => _material; set => _material = value; }

        int VAO;
        public MeshRenderer(MeshData meshData)
        {
            _meshData = meshData;
        }
        public override void OnStart(Entity entity)
        {
            base.OnStart(entity);

            flags.Add(ComponentFlag.SingletonFlag);
            InitializeRendering();
        }

        public override void OnUpdate(Entity entity)
        {
            base.OnUpdate(entity);
            renderPosition = entity.position;
            renderRotation = entity.rotation;
            renderScale = entity.scale;
            RenderMesh();
        }

        #region Rendering
        public void InitializeRendering()
        {
            int vertexVBO, texCoordVBO, normalVBO, vertexColorVBO;
            int EBO;

            VAO = GL.GenVertexArray();

            GL.BindVertexArray(VAO);

            vertexVBO = GL.GenBuffer();
            texCoordVBO = GL.GenBuffer();
            normalVBO = GL.GenBuffer();
            vertexColorVBO = GL.GenBuffer();

            EBO = GL.GenBuffer();

            //vertex buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, meshData.vertices.Length * sizeof(float), meshData.vertices, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //index buffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, meshData.indices.Length * sizeof(uint), meshData.indices, BufferUsageHint.DynamicDraw);

            //texture coord buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, texCoordVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, meshData.texCoords.Length * sizeof(float), meshData.texCoords, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);

            //normal buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, normalVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, meshData.normals.Length * sizeof(float), meshData.normals, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(2);

            //Vertex Color Buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexColorVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, meshData.vertexColors.Length * sizeof(float), meshData.vertexColors, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            GL.EnableVertexAttribArray(3);

            GL.BindVertexArray(0);
        }

        private void SetUniforms()
        {
            Matrix4 rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(renderRotation.X)) *
                               Matrix4.CreateRotationY(MathHelper.DegreesToRadians(renderRotation.Y)) *
                               Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(renderRotation.Z));
            Matrix4 scale = Matrix4.CreateScale(renderScale);
            Matrix4 model = scale * rotation * Matrix4.CreateTranslation(renderPosition);

            Matrix4 normalMatrix = Matrix4.Transpose(Matrix4.Invert(scale * rotation));

            _material.shader.SetUniformMatrix4("model", model);
            _material.shader.SetUniformMatrix4("normalMatrix", normalMatrix);
        }

        public void RenderMesh()
        {
            GL.BindVertexArray(VAO);
            
            material.Update();
            material.shader.RunShader();

            material.SetUniforms();
            SetUniforms();
            
            GL.DrawElements(PrimitiveType.Triangles, meshData.indices.Length, DrawElementsType.UnsignedInt, 0);
        }
        #endregion
    }
}
