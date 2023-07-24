using Renderables;
using Rendering.Buffers;
using Rendering.LinearAlgebra;

namespace Rendering.Geometry.Triangles;

public abstract class Mesh<T> : IRenderable where T : Triangle
{
    protected Vector3 _origin = new Vector3(0.0, 0.0, 0.0);
    protected List<T> _triangle = new List<T>();
    protected Vector3 _scale = new Vector3(1.0, 1.0, 1.0);

    public List<RenderFlag> renderFlags = new List<RenderFlag>() { RenderFlag.Surfaces };

    public Vector3 Position
    {
        get
        {
            return _origin;
        }
    }

    public Vector3 Scale
    {
        get
        {
            return _scale;
        }
    }

    public abstract void Render(ref RenderBuffer renderBuffer);
    public abstract void Rotate(double thetaX, double thetaY, double thetaZ);
    public abstract void SetPosition(Vector3 position);
    public abstract void SetScale(Vector3 scale);
    public abstract void Translate(Vector3 translationVector);
}
