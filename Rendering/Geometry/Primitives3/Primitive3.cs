using Renderables;
using Rendering.Buffers;
using Rendering.LinearAlgebra;
using System.Drawing;
using System.Runtime.Versioning;

namespace Rendering.Geometry.Primitives3;

[SupportedOSPlatform("windows")]
public abstract class Primitive3 : IRenderable
{
    protected List<Vector3> _points;
    protected List<Edge3> _edges;

    public Color Color { get; set; }

    public Vector3 Position { get; protected set; }
    public Vector3 Scale { get; protected set; }

    protected Primitive3()
    {
        _points = new List<Vector3>();
        _edges = new List<Edge3>();

        Color = Color.White;
        Position = new Vector3();
        Scale = new Vector3(1, 1, 1);
    }

    protected abstract void SetEdges();

    public abstract void SetScale(Vector3 scale);
    public abstract void SetPosition(Vector3 position);

    public abstract void Translate(Vector3 translationVector);
    public abstract void Rotate(double angleDegress, Vector3 axis);
    public abstract void Render(ref RenderBuffer renderBuffer);
}
