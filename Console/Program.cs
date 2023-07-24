using MeshReaders;
using Rendering.Buffers;
using Rendering.Geometry.Triangles;
using Rendering.LinearAlgebra;
using System.Runtime.Versioning;

namespace ConsoleApp;

internal static class Application
{
    [SupportedOSPlatform("windows")]
    private static void Main()
    {
        const string filepath = "Y:\\Software\\Codes\\C#\\TesseractEngine4D\\TesseractEngine4D\\Meshes\\UtahTeapot.stl";
        SimpleMesh mesh = MeshReader.ReadSTL_ASCII_NoNormals(filepath);
        mesh.Rotate(Math.PI, 0, 0);

        Vector3 meshPosition = new Vector3(0.0, 0.5, 1.5);
        Vector3 meshRotation = new Vector3(0.0, 0.04, 0.0);

        RenderBuffer image = new RenderBuffer(100, 100);
        for (int i = 0; i < 2; i++)
        {
            mesh.SetPosition(meshPosition);
            mesh.Rotate(meshRotation.X, meshRotation.Y, meshRotation.Z);
            mesh.Render(ref image);
        }
    }
}