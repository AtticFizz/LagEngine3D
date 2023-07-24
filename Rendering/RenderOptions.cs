using Rendering.LinearAlgebra;
using System.Drawing;

namespace Options;

public static class RenderOptions
{
    private static double _screenWidth;
    public static double ScreenWidth
    {
        get
        {
            return _screenWidth;
        }
        set
        {
            _screenWidth = value;
            if (_screenHeight != 0.0)
                AspectRatio = _screenHeight / _screenWidth;
        }
    }

    private static double _screenHeight;
    public static double ScreenHeight
    {
        get
        {
            return _screenHeight;
        }
        set
        {
            _screenHeight = value;
            if (_screenWidth != 0.0)
                AspectRatio = _screenHeight / _screenWidth;
        }
    }

    public static double AspectRatio;

    private static double _fieldOfView;
    public static double FieldOfView
    {
        get
        {
            return _fieldOfView;
        }
        set
        {
            _fieldOfView = value;
            FieldOfViewAdjusted = 1.0 / Math.Tan(_fieldOfView / 360.0f * Math.PI);
        }
    }

    public static double FieldOfViewAdjusted { get; private set; }

    public static double ZNear = 0.1;
    public static double ZFar = 1000.0;

    public static Color BackgroundColor = Color.FromArgb(24, 24, 24);
    public static Color SurfaceColor = Color.White;
    public static Color MeshColor = Color.DarkGreen;
    public static Color NormalColor = Color.Blue;

    public static double NormalScale = 0.1;

    public static Vector3 vCamera = new Vector3(0.0, 0.0, 0.0);
    public static Vector3 vLight = new Vector3(0.5, 0.5, 0.1).Normalized;
    public static double BaseBrightness = 255;

    public static string InfoTextLoading = "LoadingMesh";

    public static bool RenderWireframe = false;
    public static bool RenderNormals = false;
    public static bool RenderSurfaces = true;
}