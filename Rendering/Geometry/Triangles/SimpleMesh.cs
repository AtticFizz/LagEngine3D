using Rendering.Buffers;
using Rendering.LinearAlgebra;
using System.Runtime.Versioning;

namespace Rendering.Geometry.Triangles;

[SupportedOSPlatform("windows")]
public class SimpleMesh : Mesh<SimpleTriangle>
{
    public SimpleMesh() { }

    public SimpleMesh(List<SimpleTriangle> triangles, Vector3 origin)
    {
        _triangle = triangles;
        _origin = origin;
    }

    public override void Render(ref RenderBuffer image)
    {
        foreach (SimpleTriangle triangle in _triangle)
            triangle.Render(ref image);
    }

    public override void Rotate(double thetaX, double thetaY, double thetaZ)
    {
        foreach (SimpleTriangle triangle in _triangle)
        {
            triangle.Translate(-1 * _origin);
            triangle.Rotate(thetaX, thetaY, thetaZ);
            triangle.Translate(_origin);
        }
    }

    public override void SetPosition(Vector3 position)
    {
        Vector3 originToPosition = position - _origin;
        Translate(originToPosition);
    }

    public override void SetScale(Vector3 scale)
    {
        Vector3 scaleChange = scale / _scale;
        _scale = scale;
        for (int i = 0; i < _triangle.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _triangle[i].Vertex[j] = _origin + (_triangle[i].Vertex[j] - _origin) * scaleChange;
            }
        }
    }

    public override void Translate(Vector3 translationVector)
    {
        _origin += translationVector;
        foreach (SimpleTriangle triangle in _triangle)
            triangle.Translate(translationVector);
    }
}