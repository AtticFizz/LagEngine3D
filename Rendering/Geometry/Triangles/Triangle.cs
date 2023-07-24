using Rendering.Buffers;
using System.Runtime.Versioning;
using Rendering.LinearAlgebra;
using Renderables;
using Options;

namespace Rendering.Geometry.Triangles;

[SupportedOSPlatform("windows")]
public abstract class Triangle : IRenderable
{
    public Vector3[] Vertex = new Vector3[3];

    public abstract void Translate(Vector3 translationVector);

    /// <summary>
    /// Rotates trangle around world origin (0.0, 0.0, 0.0).
    /// </summary>
    public abstract void Rotate(double thetaX, double thetaY, double thetaZ);

    public abstract void Render(ref RenderBuffer image);

    protected Vector3 GetSurfaceNormal()
    {
        Vector3 v1 = Vertex[0] - Vertex[1];
        Vector3 v2 = Vertex[2] - Vertex[1];
        return v1.Cross(v2).Normalized;
    }

    protected static double Clamp(double t, double min, double max)
    {
        if (t < min)
            return min;
        if (t > max)
            return max;
        return t;
    }

    protected void RenderWireframe(ref RenderBuffer renderBuffer)
    {
        Renderer.DrawLine_Naive(ref renderBuffer, Vertex[0], Vertex[1], RenderOptions.MeshColor);
        Renderer.DrawLine_Naive(ref renderBuffer, Vertex[1], Vertex[2], RenderOptions.MeshColor);
        Renderer.DrawLine_Naive(ref renderBuffer, Vertex[2], Vertex[0], RenderOptions.MeshColor);
    }
}
