using GLRenderer.Rendering;
using OpenTK.Mathematics;

namespace GLRenderer.Components
{
    public struct MeshData
    {
        public float[] vertices { get; set; }
        public uint[] indices { get; set; }
        public float[] normals { get; set; } = new float[] { };
        public float[] texCoords { get; set; } = new float[] { };
        public float[] vertexColors { get; set; } = new float[] { };
        public MeshData() { }

        // Pre-defined meshes
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
                -0.5f,  0.5f, -0.5f,  // Top-left
                 0.5f,  0.5f, -0.5f,  // Top-right
                 0.5f, -0.5f, -0.5f,  // Bottom-right

                // Left face
                -0.5f, -0.5f, -0.5f,  // Bottom-left
                -0.5f, -0.5f,  0.5f,  // Bottom-right
                -0.5f,  0.5f,  0.5f,  // Top-right
                -0.5f,  0.5f, -0.5f,  // Top-left

                // Right face
                 0.5f, -0.5f, -0.5f,  // Bottom-left
                 0.5f,  0.5f, -0.5f,  // Top-left
                 0.5f,  0.5f,  0.5f,  // Top-right
                 0.5f, -0.5f,  0.5f,  // Bottom-right

                // Top face
                -0.5f,  0.5f, -0.5f,  // Bottom-left
                -0.5f,  0.5f,  0.5f,  // Bottom-right
                 0.5f,  0.5f,  0.5f,  // Top-right
                 0.5f,  0.5f, -0.5f,  // Top-left

                // Bottom face
                -0.5f, -0.5f, -0.5f,  // Bottom-left
                 0.5f, -0.5f, -0.5f,  // Bottom-right
                 0.5f, -0.5f,  0.5f,  // Top-right
                -0.5f, -0.5f,  0.5f   // Top-left
            },
            indices = new uint[]
            {
                // Front face
                0, 1, 2,
                2, 3, 0,

                // Back face
                4, 5, 6,
                6, 7, 4,

                // Left face
                8, 9, 10,
                10, 11, 8,

                // Right face
                12, 13, 14,
                14, 15, 12,

                // Top face
                16, 17, 18,
                18, 19, 16,

                // Bottom face
                20, 21, 22,
                22, 23, 20
           },
           texCoords = new float[]
           {
               // Front face
               0.0f, 0.0f,  // Bottom-left
               1.0f, 0.0f,  // Bottom-right
               1.0f, 1.0f,  // Top-right
               0.0f, 1.0f,  // Top-left
           
               // Back face
               1.0f, 0.0f,  // Bottom-right
               1.0f, 1.0f,  // Top-right
               0.0f, 1.0f,  // Top-left
               0.0f, 0.0f,  // Bottom-left
           
               // Left face
               0.0f, 0.0f,  // Bottom-left
               1.0f, 0.0f,  // Bottom-right
               1.0f, 1.0f,  // Top-right
               0.0f, 1.0f,  // Top-left
           
               // Right face
               1.0f, 0.0f,  // Bottom-right
               1.0f, 1.0f,  // Top-right
               0.0f, 1.0f,  // Top-left
               0.0f, 0.0f,  // Bottom-left
           
               // Top face
               0.0f, 1.0f,  // Top-left
               0.0f, 0.0f,  // Bottom-left
               1.0f, 0.0f,  // Bottom-right
               1.0f, 1.0f,  // Top-right
           
               // Bottom face
               0.0f, 0.0f,  // Bottom-left
               1.0f, 0.0f,  // Bottom-right
               1.0f, 1.0f,  // Top-right
               0.0f, 1.0f   // Top-left
           },

            normals = new float[]
           {
                // Front face normals
                0.0f,  0.0f,  1.0f,  // Bottom-left
                0.0f,  0.0f,  1.0f,  // Bottom-right
                0.0f,  0.0f,  1.0f,  // Top-right
                0.0f,  0.0f,  1.0f,  // Top-left

                // Back face normals
                0.0f,  0.0f, -1.0f,  // Bottom-left
                0.0f,  0.0f, -1.0f,  // Top-left
                0.0f,  0.0f, -1.0f,  // Top-right
                0.0f,  0.0f, -1.0f,  // Bottom-right

                // Left face normals
                -1.0f,  0.0f,  0.0f, // Bottom-left
                -1.0f,  0.0f,  0.0f, // Bottom-right
                -1.0f,  0.0f,  0.0f, // Top-right
                -1.0f,  0.0f,  0.0f, // Top-left

                // Right face normals
                1.0f,  0.0f,  0.0f,  // Bottom-left
                1.0f,  0.0f,  0.0f,  // Top-left
                1.0f,  0.0f,  0.0f,  // Top-right
                1.0f,  0.0f,  0.0f,  // Bottom-right

                // Top face normals
                0.0f,  1.0f,  0.0f,  // Bottom-left
                0.0f,  1.0f,  0.0f,  // Bottom-right
                0.0f,  1.0f,  0.0f,  // Top-right
                0.0f,  1.0f,  0.0f,  // Top-left

                // Bottom face normals
                0.0f, -1.0f,  0.0f,  // Bottom-left
                0.0f, -1.0f,  0.0f,  // Bottom-right
                0.0f, -1.0f,  0.0f,  // Top-right
                0.0f, -1.0f,  0.0f   // Top-left
            },
        };
        public static MeshData pyramid = new MeshData()
        {
            vertices = new float[]
            {
                     1.0f, -1.0f,  1.0f,  // Base front-right
                     1.0f, -1.0f, -1.0f,  // Base back-right
                    -1.0f, -1.0f, -1.0f,  // Base back-left
                    -1.0f, -1.0f,  1.0f,  // Base front-left
                     0.0f, 1.0f,  0.0f   // Apex
            },
            indices = new uint[]
            {
                     // Base face
                     0, 1, 2,
                     2, 3, 0,

                     // Side faces
                     0, 1, 4,
                     1, 2, 4,
                     2, 3, 4,
                     3, 0, 4
            },
            texCoords = new float[]
            {
                    // Base face (quadrilateral)
                    0.0f, 0.0f,  // Bottom-left
                    1.0f, 0.0f,  // Bottom-right
                    1.0f, 1.0f,  // Top-right
                    0.0f, 1.0f,  // Top-left
                    
                    // Apex (for the triangular faces, same apex coordinate)
                    0.5f, 1.0f   // Apex of the triangle
            },
            normals = new float[]
            {
                // Base face normals (all facing down)
                0.0f, -1.0f,  0.0f,
                0.0f, -1.0f,  0.0f,
                0.0f, -1.0f,  0.0f,
                0.0f, -1.0f,  0.0f,
        
                // Normal for face 0-1-4 (right-front)
                0.707f,  0.5f,  0.5f,
                0.707f,  0.5f,  0.5f,
                0.707f,  0.5f,  0.5f,
        
                // Normal for face 1-2-4 (right-back)
                0.707f,  0.5f, -0.5f,
                0.707f,  0.5f, -0.5f,
                0.707f,  0.5f, -0.5f,
        
                // Normal for face 2-3-4 (left-back)
                -0.707f,  0.5f, -0.5f,
                -0.707f,  0.5f, -0.5f,
                -0.707f,  0.5f, -0.5f,
        
                // Normal for face 3-0-4 (left-front)
                -0.707f,  0.5f,  0.5f,
                -0.707f,  0.5f,  0.5f,
                -0.707f,  0.5f,  0.5f
            }   
        };
        public static MeshData plane = new MeshData()
        {
            vertices = new float[]
            {
                // X, Y, Z
                -1.0f,  -1.0f,  0.0f,  // Bottom-left
                 1.0f,  -1.0f,  0.0f,  // Bottom-right
                 1.0f,   1.0f,  0.0f,  // Top-right
                -1.0f,   1.0f,  0.0f   // Top-left
            },

            indices = new uint[]
            {
                0, 1, 2,   // First triangle (bottom-left, bottom-right, top-right)
                2, 3, 0    // Second triangle (top-right, top-left, bottom-left)
            },
            
            texCoords = new float[]
            {
                // U, V (Texture coordinates)
                0.0f, 0.0f,  // Bottom-left
                1.0f, 0.0f,  // Bottom-right
                1.0f, 1.0f,  // Top-right
                0.0f, 1.0f   // Top-left
            },

            normals = new float[]
            {
                // X, Y, Z (Normals pointing up for all vertices)
                0.0f, 0.0f, 1.0f,  // Bottom-left
                0.0f, 0.0f, 1.0f,  // Bottom-right
                0.0f, 0.0f, 1.0f,  // Top-right
                0.0f, 0.0f, 1.0f   // Top-left
            }
        };

    }
}