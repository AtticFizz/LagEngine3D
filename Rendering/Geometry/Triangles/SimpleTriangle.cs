using Options;
using Renderables;
using Rendering.Buffers;
using Rendering.LinearAlgebra;
using System.Drawing;
using System.Runtime.Versioning;

namespace Rendering.Geometry.Triangles;

[SupportedOSPlatform("windows")]
public class SimpleTriangle : Triangle
{
    public SimpleTriangle() { }

    public SimpleTriangle(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        Vertex = new Vector3[] { point1, point2, point3 };
    }

    public override void Translate(Vector3 translationVector)
    {
        Vertex[0] += translationVector;
        Vertex[1] += translationVector;
        Vertex[2] += translationVector;
    }

    public override void Rotate(double thetaX, double thetaY, double thetaZ)
    {
        Vertex[0].Rotate(thetaX, thetaY, thetaZ);
        Vertex[1].Rotate(thetaX, thetaY, thetaZ);
        Vertex[2].Rotate(thetaX, thetaY, thetaZ);
    }

    public override void Render(ref RenderBuffer renderBuffer)
    {
        Vector3 vNormal = GetSurfaceNormal();

        if (!(vNormal.Dot(Vertex[0] - RenderOptions.vCamera) < 0 || vNormal.Dot(Vertex[1] - RenderOptions.vCamera) < 0 || vNormal.Dot(Vertex[2] - RenderOptions.vCamera) < 0))
        {
            int l = (int)Clamp(vNormal.Dot(RenderOptions.vLight) * RenderOptions.BaseBrightness, 0, 255);

            //if (l < 50)
            //    l = 50;
            //else if (l < 200)
            //    l = 25;
            //else
            //    l = 255;

            Renderer.FillTriangle_Barycentric(ref renderBuffer, this, Color.FromArgb(l, l, l));
        }

        if (RenderOptions.RenderWireframe)
            RenderWireframe(ref renderBuffer);
    }
}
