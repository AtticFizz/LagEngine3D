using Rendering.Buffers;
using Rendering.LinearAlgebra;
using System.Drawing;
using System.Runtime.Versioning;

namespace Rendering.Geometry.Primitives3;

[SupportedOSPlatform("windows")]
public class Edge3
{
    public Vector3 From { get; set; }
    public Vector3 To { get; set; }

    public Edge3(Vector3 from, Vector3 to)
    {
        From = from;
        To = to;
    }

    public void Render(ref RenderBuffer renderBuffer, Color color)
    {
        Renderer.DrawLine_Naive(ref renderBuffer, From.ToPoint2D(), To.ToPoint2D(), color);
    }
}
