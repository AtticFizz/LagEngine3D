using Options;
using Rendering.Buffers;
using Rendering.LinearAlgebra;
using System.Drawing;
using System.Runtime.Versioning;

namespace Rendering.Geometry.Triangles;

[SupportedOSPlatform("windows")]
public class NormalTriangle : Triangle
{
    public Vector3[] Normal { get; set; }

    public NormalTriangle(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        Vertex = new Vector3[] { point1, point2, point3 };
        Vector3 normal = (point1 - point2).Cross(point3 - point2);
        Normal = new Vector3[] { normal.Copy(), normal.Copy(), normal.Copy() };
    }

    public NormalTriangle(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 normal1, Vector3 normal2, Vector3 normal3)
    {
        Vertex = new Vector3[] { point1, point2, point3 };
        Normal = new Vector3[] { normal1, normal2, normal3 };
    }

    public override void Translate(Vector3 translationVector)
    {
        for (int i = 0; i < 3; i++)
            Vertex[i] += translationVector;
    }

    public override void Rotate(double thetaX, double thetaY, double thetaZ)
    {
        for (int i = 0; i < 3; i++)
        {
            Vertex[i].Rotate(thetaX, thetaY, thetaZ);
            Normal[i].Rotate(thetaX, thetaY, thetaZ);
        }
    }

    public override void Render(ref RenderBuffer renderBuffer)
    {
        Vector3 vNormal = GetSurfaceNormal();

        if (!(vNormal.Dot(Vertex[0] - RenderOptions.vCamera) < 0 || vNormal.Dot(Vertex[1] - RenderOptions.vCamera) < 0 || vNormal.Dot(Vertex[2] - RenderOptions.vCamera) < 0))
        {
            if (RenderOptions.RenderSurfaces)
            {
                int l = (int)Clamp(vNormal.Dot(RenderOptions.vLight) * RenderOptions.BaseBrightness, 0, 255);
                Renderer.FillTriangle_Barycentric(ref renderBuffer, this, Color.FromArgb(l, l, l));
            }
        }

        if (RenderOptions.RenderWireframe)
            RenderWireframe(ref renderBuffer);

        if (RenderOptions.RenderNormals)
        { 
            for (int i = 0; i < 3; i++)
            {
                Renderer.DrawLine_Naive(ref renderBuffer, Vertex[i], Vertex[i] - RenderOptions.NormalScale * Normal[i], RenderOptions.NormalColor);
            }
        }
    }
}
