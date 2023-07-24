using Rendering.Geometry.Triangles;
using Rendering.LinearAlgebra;
using System.Runtime.Versioning;

namespace MeshReaders;

[SupportedOSPlatform("windows")]
public static class MeshReader
{
    public static SimpleMesh ReadSTL_ASCII_NoNormals(string filepath)
    {
        List<SimpleTriangle> triangles = new List<SimpleTriangle>();
        string[] lines = File.ReadAllLines(filepath);

        for (int i = 0; i < lines.Length; i++)
        {
            if (!lines[i].Contains("vertex"))
                continue;

            string[] parts = lines[i].Split(' ');
            double x1 = double.Parse(parts[1]);
            double y1 = double.Parse(parts[2]);
            double z1 = double.Parse(parts[3]);
            Vector3 v1 = new Vector3(x1, y1, z1);

            parts = lines[i + 1].Split(' ');
            double x2 = double.Parse(parts[1]);
            double y2 = double.Parse(parts[2]);
            double z2 = double.Parse(parts[3]);
            Vector3 v2 = new Vector3(x2, y2, z2);

            parts = lines[i + 2].Split(' ');
            double x3 = double.Parse(parts[1]);
            double y3 = double.Parse(parts[2]);
            double z3 = double.Parse(parts[3]);
            Vector3 v3 = new Vector3(x3, y3, z3);

            triangles.Add(new SimpleTriangle(v1, v2, v3));
            i += 3;
        }

        return new SimpleMesh(triangles, new Vector3(0.0, 0.0, 0.0));
    }

    public static NormalMesh ReadSTL_ASCII_WithNormals(string filepath)
    {
        List<NormalTriangle> triangles = new List<NormalTriangle>();
        string[] lines = File.ReadAllLines(filepath);

        for (int i = 0; i < lines.Length; i++)
        {
            if (!lines[i].Contains("vertex"))
                continue;

            string[] parts = lines[i].Split(' ');
            double x1 = double.Parse(parts[1]);
            double y1 = double.Parse(parts[2]);
            double z1 = double.Parse(parts[3]);
            Vector3 v1 = new Vector3(x1, y1, z1);

            parts = lines[i + 1].Split(' ');
            double x2 = double.Parse(parts[1]);
            double y2 = double.Parse(parts[2]);
            double z2 = double.Parse(parts[3]);
            Vector3 v2 = new Vector3(x2, y2, z2);

            parts = lines[i + 2].Split(' ');
            double x3 = double.Parse(parts[1]);
            double y3 = double.Parse(parts[2]);
            double z3 = double.Parse(parts[3]);
            Vector3 v3 = new Vector3(x3, y3, z3);

            triangles.Add(new NormalTriangle(v1, v2, v3));
            i += 3;
        }

        return new NormalMesh(triangles, new Vector3(0.0, 0.0, 0.0));
    }
}