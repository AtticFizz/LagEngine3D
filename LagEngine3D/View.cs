using MeshReaders;
using Options;
using Rendering;
using Rendering.Geometry.Triangles;
using Rendering.LinearAlgebra;

namespace LagEngine3D;

internal static class View
{
    private static string _file = "UtahTeapot.stl";
    private static string _filepath = Path.Combine(Directory.GetCurrentDirectory(), "Meshes", _file);

    public static Vector3 _meshRotation = new Vector3(0.0, 0.5, 0.0);

    private static SimpleMesh _simpleMesh = new SimpleMesh();
    private static NormalMesh _normalMesh = new NormalMesh();

    public static void OnStart()
    {
        RenderOptions.FieldOfView = 90.0;

        _simpleMesh = MeshReader.ReadSTL_ASCII_NoNormals(_filepath);
        _simpleMesh.Rotate(Math.PI, 0.0, 0.0);
        _simpleMesh.SetPosition(new Vector3(0.0, 0.5, 1.25));
        Renderer.ObjectsToRender.Add(_simpleMesh);
    }

    private static bool _loadedOnce = false;

    private static void LoadNormalMeshes()
    {
        if (!_loadedOnce)
        {
            _normalMesh = MeshReader.ReadSTL_ASCII_WithNormals(_filepath);
            _normalMesh.Rotate(Math.PI, 0.0, 0.0);
            _normalMesh.SetPosition(new Vector3(0.0, -2.0, 14));
            //_normalMesh.SetScale(new Vector3(1.5, 1.5, 1.5));
            _normalMesh.renderFlags.Add(RenderFlag.VertexNormals); // doesn't work yet
            Renderer.ObjectsToRender.Add(_normalMesh);

            _loadedOnce = true;
        }
    }

    public static void OnUpdate()
    {
        //LoadNormalMeshes();

        MeshTestMethod();
        //Cube3TestMethod();
    }

    private static void MeshTestMethod()
    {
        _simpleMesh.Rotate(Time.DeltaTime * _meshRotation.X, Time.DeltaTime * _meshRotation.Y, Time.DeltaTime * _meshRotation.Z);
        //_normalMesh.Rotate(Time.DeltaTime * _meshRotation.X, Time.DeltaTime * _meshRotation.Y, Time.DeltaTime * _meshRotation.Z);
    }

    //private Cube3 cube = new Cube3();
    //private Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
    //private Vector3 position = new Vector3(0.0f, 0.0f, 5.0f);
    //private double scaleDir = 0.01f;
    //private int positionDir = 4;

    //private Vector3 up = new Vector3(0.0f, -1.0f, 0.0f);
    //private Vector3 down = new Vector3(0.0f, 1.0f, 0.0f);
    //private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
    //private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    //private Vector3 translationVector = new Vector3(0.0f, 1.0f, 0.0f);
    //private double threshold = 1.5f;
    //private double translationScale = 0.1f;

    //private Edge3 edge = new Edge3(new Vector3(50, 50, 0), new Vector3(0, 100, 0));

    //private void Cube3TestMethod()
    //{
    //    //edge.From += new Vector3(1, 1, 1);
    //    //edge.To += new Vector3(1, 1, 1);
    //    //Renderer.DrawLine_Naive(ref _renderBuffer, edge.From.ToPoint2D(), edge.To.ToPoint2D(), LINE_COLOR);

    //    //scale = new Vector3(scale.X + scaleDir, scale.Y + scaleDir, scale.Z + scaleDir);
    //    //if (scale.X > 20)
    //    //    scaleDir = -0.01f;
    //    //else if (scale.X < 100)
    //    //    scaleDir = 0.01f;
    //    cube.SetScale(scale);

    //    cube.Rotate(0.0f, 0.03f, 0.0f);

    //    //position += translationScale * translationVector;
    //    //if (position.X > threshold && translationVector == right)
    //    //    translationVector = down;
    //    //else if (position.X < -threshold && translationVector == left)
    //    //    translationVector = up;
    //    //else if (position.Y > threshold && translationVector == down)
    //    //    translationVector = left;
    //    //else if (position.Y < -threshold && translationVector == up)
    //    //    translationVector = right;
    //    cube.SetPosition(position);

    //    cube.Render(ref _renderBuffer);
    //}
}
