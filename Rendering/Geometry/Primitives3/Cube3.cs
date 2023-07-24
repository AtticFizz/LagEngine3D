using Rendering;
using Rendering.Buffers;
using Rendering.Geometry.Primitives3;
using Rendering.LinearAlgebra;
using System.Runtime.Versioning;

namespace Geometry.Primitives3;

[SupportedOSPlatform("windows")]
public class Cube3 : Primitive3
{
    public Cube3() : base()
    {
        _points = new List<Vector3>()
        {
            new Vector3(1, 1, 1),
            new Vector3(1, -1, 1),
            new Vector3(-1, -1, 1),
            new Vector3(-1, 1, 1),
            new Vector3(1, 1, -1),
            new Vector3(1, -1, -1),
            new Vector3(-1, -1, -1),
            new Vector3(-1, 1, -1),
        };

        SetEdges();
    }

    protected override void SetEdges()
    {
        _edges = new List<Edge3>()
        {
            new Edge3(_points[0], _points[1]),
            new Edge3(_points[1], _points[2]),
            new Edge3(_points[2], _points[3]),
            new Edge3(_points[3], _points[0]),

            new Edge3(_points[0], _points[4]),
            new Edge3(_points[1], _points[5]),
            new Edge3(_points[2], _points[6]),
            new Edge3(_points[3], _points[7]),

            new Edge3(_points[4], _points[5]),
            new Edge3(_points[5], _points[6]),
            new Edge3(_points[6], _points[7]),
            new Edge3(_points[7], _points[4]),
        };
   }

    public override void Rotate(double theta, Vector3 axis) // need rework
    {
        for (int i = 0; i < _points.Count; i++)
        {
            Vector3 PositionToPoint = _points[i] - Position;
            PositionToPoint.Rotate(theta, axis);
            _points[i] = Position + PositionToPoint;
        }
        SetEdges();
    }

    public void Rotate(double xRotation, double yRotation, double zRotation)
    {
        for (int i = 0; i < _points.Count; i++)
        {
            Vector3 PositionToPoint = _points[i] - Position;
            PositionToPoint.RotateAroundX(xRotation);
            PositionToPoint.RotateAroundY(yRotation);
            PositionToPoint.RotateAroundZ(zRotation);
            _points[i] = Position + PositionToPoint;
        }
        SetEdges();
    }

    public override void SetScale(Vector3 scale) // needs rework
    {
        Vector3 factor = scale - Scale;
        Scale = scale;
        for (int i = 0; i < _points.Count; i++)
        {
            double x = (_points[i].X - Position.X) * factor.X;
            double y = (_points[i].Y - Position.Y) * factor.Y;
            double z = (_points[i].Z - Position.Z) * factor.Z;
            _points[i] += new Vector3(x, y, z);
        }
        SetEdges();
    }

    public override void SetPosition(Vector3 position)
    {
        Vector3 translationVector = position - Position;
        Position = position;
        for (int i = 0; i < _points.Count; i++)
            _points[i] += translationVector;
        SetEdges();
    }

    public override void Translate(Vector3 translationVector)
    {
        Position += translationVector;
        for (int i = 0; i < _points.Count; i++)
            _points[i] += translationVector;
        SetEdges();
    }

    public override void Render(ref RenderBuffer image)
    {
        for (int i = 0; i < _edges.Count; i++)
            Renderer.DrawLine_Naive(ref image, _edges[i].From.ToPoint2D(), _edges[i].To.ToPoint2D(), Color);
    }
}
