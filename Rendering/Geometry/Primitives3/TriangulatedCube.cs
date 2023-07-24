using Rendering.Buffers;
using Rendering.Geometry.Triangles;
using Rendering.LinearAlgebra;
using System.Runtime.Versioning;

namespace Geometry.Primitives3;

[SupportedOSPlatform("windows")]
public class TriangulatedCube
{
    private Vector3 _origin;
    private List<SimpleTriangle> _triangles;

    public Vector3 Position
    {
        get
        {
            return _origin;
        }
    }

    public TriangulatedCube()
    {
        _origin = new Vector3(0, 0, 0);

        _triangles = new List<SimpleTriangle>()
        {
            new SimpleTriangle(new Vector3(-0.5, 0.5, 0.5), new Vector3(0.5, 0.5, 0.5), new Vector3(-0.5, -0.5, 0.5)),
            new SimpleTriangle(new Vector3(0.5, 0.5, 0.5), new Vector3(0.5, -0.5, 0.5), new Vector3(-0.5, -0.5, 0.5)),
            new SimpleTriangle(new Vector3(-0.5, 0.5, 0.5), new Vector3(-0.5, 0.5, -0.5), new Vector3(0.5, 0.5, 0.5)),
            new SimpleTriangle(new Vector3(0.5, 0.5, 0.5), new Vector3(-0.5, 0.5, -0.5), new Vector3(0.5, 0.5, -0.5)),
            new SimpleTriangle(new Vector3(0.5, 0.5, 0.5), new Vector3(0.5, 0.5, -0.5), new Vector3(0.5, -0.5, 0.5)),
            new SimpleTriangle(new Vector3(0.5, -0.5, 0.5), new Vector3(0.5, 0.5, -0.5), new Vector3(0.5, -0.5, -0.5)),
            new SimpleTriangle(new Vector3(0.5, -0.5, 0.5), new Vector3(-0.5, -0.5, -0.5), new Vector3(-0.5, -0.5, 0.5)),
            new SimpleTriangle(new Vector3(0.5, -0.5, 0.5), new Vector3(0.5, -0.5, -0.5), new Vector3(-0.5, -0.5, -0.5)),
            new SimpleTriangle(new Vector3(-0.5, 0.5, 0.5), new Vector3(-0.5, -0.5, 0.5), new Vector3(-0.5, -0.5, -0.5)),
            new SimpleTriangle(new Vector3(-0.5, 0.5, 0.5), new Vector3(-0.5, -0.5, -0.5), new Vector3(-0.5, 0.5, -0.5)),
            new SimpleTriangle(new Vector3(-0.5, 0.5, -0.5), new Vector3(0.5, -0.5, -0.5), new Vector3(0.5, 0.5, -0.5)),
            new SimpleTriangle(new Vector3(-0.5, 0.5, -0.5), new Vector3(-0.5, -0.5, -0.5), new Vector3(0.5, -0.5, -0.5))
        };
    }

    public void Render(ref RenderBuffer image)
    {
        foreach (SimpleTriangle triangle in _triangles)
            triangle.Render(ref image);
    }

    public void Rotate(double thetaX, double thetaY, double thetaZ)
    {
        foreach (SimpleTriangle triangle in _triangles)
        {
            triangle.Translate(-1 * _origin);
            triangle.Rotate(thetaX, thetaY, thetaZ);
            triangle.Translate(_origin);
        }
    }

    public void SetPosition(Vector3 position)
    {
        Vector3 originToPosition = position - _origin;
        Translate(originToPosition);
    }

    public void SetScale(Vector3 scale)
    {
        throw new NotImplementedException();
    }

    public void Translate(Vector3 translationVector)
    {
        _origin += translationVector;
        foreach (SimpleTriangle triangle in _triangles)
            triangle.Translate(translationVector);
    }
}
