using Rendering.Buffers;
using Rendering.LinearAlgebra;
using System.Runtime.Versioning;

namespace Rendering.Geometry.Triangles;

[SupportedOSPlatform("windows")]
public class NormalMesh : Mesh<NormalTriangle>
{
    public NormalMesh() { }

    public NormalMesh(List<NormalTriangle> triangles, Vector3 origin)
    {
        _triangle = triangles;
        _origin = origin;
        SmoothNormals();
    }

    private void SmoothNormals()
    {
        List<NormalTriangle> smoothTriangles = new List<NormalTriangle>();

        for (int i = 0; i < _triangle.Count; i++)
        {
            Vector3[] smoothNormals = new Vector3[3];

            for (int j = 0; j < 3; j++)
            {
                smoothNormals[j] = _triangle[i].Normal[j];

                for (int k = 0; k < _triangle.Count; k++)
                {
                    if (i == k)
                        continue;

                    for (int l = 0; l < 3; l++)
                    {
                        if (_triangle[i].Vertex[j] == _triangle[k].Vertex[l])
                        {
                            smoothNormals[j] += _triangle[k].Normal[l];
                        }
                    }
                }

                smoothNormals[j].Normalize();
            }

            smoothTriangles.Add(new NormalTriangle(_triangle[i].Vertex[0], _triangle[i].Vertex[1], _triangle[i].Vertex[2], smoothNormals[0], smoothNormals[1], smoothNormals[2]));
        }

        _triangle = smoothTriangles;
    }

    public override void Render(ref RenderBuffer renderBuffer)
    {
        foreach (NormalTriangle triangle in _triangle)
            triangle.Render(ref renderBuffer);
    }

    public override void Rotate(double thetaX, double thetaY, double thetaZ)
    {
        foreach (NormalTriangle triangle in _triangle)
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
        foreach (NormalTriangle triangle in _triangle)
            triangle.Translate(translationVector);
    }
}
