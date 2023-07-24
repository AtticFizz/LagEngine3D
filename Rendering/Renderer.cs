using Options;
using Renderables;
using Rendering.Buffers;
using Rendering.Geometry.Primitives3;
using Rendering.Geometry.Triangles;
using Rendering.LinearAlgebra;
using System.Drawing;
using System.Runtime.Versioning;

namespace Rendering;

[SupportedOSPlatform("windows")]
public static class Renderer
{
    public static List<IRenderable> ObjectsToRender = new List<IRenderable>();

    public static void DrawLine_Naive(ref RenderBuffer image, Edge3 edge, Color color)
    {
        DrawLine_Naive(ref image, ToPoint2D(edge.From), ToPoint2D(edge.To), color);
    }
    public static void DrawLine_Naive(ref RenderBuffer image, Vector3 v1, Vector3 v2, Color color)
    {
        Vector3 v1_2D = ToPoint2D(v1);
        Vector3 v2_2D = ToPoint2D(v2);
        DrawLine_Naive(ref image, (int)v1_2D.X, (int)v1_2D.Y, (int)v2_2D.X, (int)v2_2D.Y, color);
    }
    public static void DrawLine_Naive(ref RenderBuffer image, int x1, int y1, int x2, int y2, Color color)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;

        if (Math.Abs(dx) >= Math.Abs(dy))
        {
            if (x1 > x2)
            {
                (x1, x2) = (x2, x1);
                (y1, y2) = (y2, y1);
            }

            for (int x = x1; x <= x2; x++)
            {
                int y = y2;
                if (dx != 0)
                    y = y1 + (dy * (x - x1) / dx);

                if (x < 0 || x >= image.Width || y < 0 || y >= image.Height)
                    continue;

                image[x, y] = color;
            }
        }
        else
        {
            if (y1 > y2)
            {
                (x1, x2) = (x2, x1);
                (y1, y2) = (y2, y1);
            }

            for (int y = y1; y <= y2; y++)
            {
                int x = x2;
                if (dy != 0)
                    x = x1 + (dx * (y - y1) / dy);

                if (x < 0 || x >= image.Width || y < 0 || y >= image.Height)
                    continue;

                image[x, y] = color;
            }
        }
    }

    // v1 = A, v2 = B, p3 = C, P = (x, y)
    public static void FillTriangle_Barycentric(ref RenderBuffer renderBuffer, SimpleTriangle triangle, Color color)
    {
        FillTriangle_Barycentric(ref renderBuffer, ToPoint2D(triangle.Vertex[0]), ToPoint2D(triangle.Vertex[1]), ToPoint2D(triangle.Vertex[2]), color);
    }
    public static void FillTriangle_Barycentric(ref RenderBuffer renderBuffer, Vector3 p1, Vector3 p2, Vector3 p3, Color color)
    {
        int maxX = (int)Math.Max(p1.X, Math.Max(p2.X, p3.X));
        int minX = (int)Math.Min(p1.X, Math.Min(p2.X, p3.X));
        int maxY = (int)Math.Max(p1.Y, Math.Max(p2.Y, p3.Y));
        int minY = (int)Math.Min(p1.Y, Math.Min(p2.Y, p3.Y));

        double wa = p1.X * (p3.Y - p1.Y);
        double wb = p3.X - p1.X;
        double wc = p3.Y - p1.Y;
        double wd = p2.Y - p1.Y;
        double w1Div = (p2.Y - p1.Y) * (p3.X - p1.X) - (p2.X - p1.X) * (p3.Y - p1.Y);

        Vector3 v1 = p2 - p1;
        Vector3 v2 = p2 - p3;
        Vector3 sn = v1.Cross(v2);

        for (int y = minY; y <= maxY; y++)
        {
            if (y < 0 || y >= renderBuffer.Height)
                continue;

            for (int x = minX; x <= maxX; x++)
            {
                if (x < 0 || x >= renderBuffer.Width)
                    continue;

                double w1 = (wa + (y - p1.Y) * wb - x * wc) / (double)w1Div;
                double w2 = (y - p1.Y - w1 * wd) / (double)wc;

                double z = (sn.X * p1.X + sn.Y * p1.Y + sn.Z * p1.Z - sn.X * x - sn.Y * y) / sn.Z;

                if (w1 >= 0 && w2 >= 0 && w1 + w2 <= 1 && z < renderBuffer.Depth[x, y])
                {
                    renderBuffer[x, y] = color;
                    renderBuffer.Depth[x, y] = z;
                }
            }
        }
    }

    public static void FillTriangle_Barycentric(ref RenderBuffer renderBuffer, NormalTriangle normalTriangle, Color color)
    {
        FillTriangle_Barycentric_Gouraud(
            ref renderBuffer,
            ToPoint2D(normalTriangle.Vertex[0]),
            ToPoint2D(normalTriangle.Vertex[1]),
            ToPoint2D(normalTriangle.Vertex[2]),
            normalTriangle.Normal[0],
            normalTriangle.Normal[1],
            normalTriangle.Normal[2],
            color
        );
    }
    public static void FillTriangle_Barycentric_Gouraud(ref RenderBuffer renderBuffer, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p1Normal, Vector3 p2Normal, Vector3 p3Normal, Color color)
    {
        int maxX = (int)Math.Max(p1.X, Math.Max(p2.X, p3.X));
        int minX = (int)Math.Min(p1.X, Math.Min(p2.X, p3.X));
        int maxY = (int)Math.Max(p1.Y, Math.Max(p2.Y, p3.Y));
        int minY = (int)Math.Min(p1.Y, Math.Min(p2.Y, p3.Y));

        double wa = p1.X * (p3.Y - p1.Y);
        double wb = p3.X - p1.X;
        double wc = p3.Y - p1.Y;
        double wd = p2.Y - p1.Y;
        double w1Div = (p2.Y - p1.Y) * (p3.X - p1.X) - (p2.X - p1.X) * (p3.Y - p1.Y);

        Vector3 v1 = p2 - p1;
        Vector3 v2 = p2 - p3;
        Vector3 sn = v1.Cross(v2);

        double alphaSpan1 = Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y);
        double alphaSpan2 = Math.Abs(p2.X - p3.X) + Math.Abs(p2.Y - p3.Y);

        for (int y = minY; y <= maxY; y++)
        {
            if (y < 0 || y >= renderBuffer.Height)
                continue;


            for (int x = minX; x <= maxX; x++)
            {
                if (x < 0 || x >= renderBuffer.Width)
                    continue;

                double w1 = (wa + (y - p1.Y) * wb - x * wc) / (double)w1Div;
                double w2 = (y - p1.Y - w1 * wd) / (double)wc;
                double z = (sn.X * p1.X + sn.Y * p1.Y + sn.Z * p1.Z - sn.X * x - sn.Y * y) / sn.Z;

                if (w1 >= 0 && w2 >= 0 && w1 + w2 <= 1 && z < renderBuffer.Depth[x, y])
                {
                    //double alphaDisplacement = Math.Abs(p2.X - x) + Math.Abs(p2.Y - y);
                    //double alpha1 = alphaDisplacement / alphaSpan1;
                    //Vector3 normalInt1 = Lerp(alpha1, p2Normal, p1Normal);
                    //double alpha2 = alphaDisplacement / alphaSpan2;
                    //Vector3 normalInt2 = Lerp(alpha2, p2Normal, p3Normal);
                    //Vector3 vNormal = Lerp(0.5, normalInt1, normalInt2).Normalized;
                    Vector3 vNormal = Lerp(x, y, p2Normal, p1Normal, p3Normal).Normalized;
                    int l = (int)Clamp(vNormal.Dot(RenderOptions.vLight) * RenderOptions.BaseBrightness, 0, 255);
                    renderBuffer[x, y] = Color.FromArgb(l, l, l);
                    renderBuffer.Depth[x, y] = z;
                }
            }
        }
    }

    /// <summary>
    /// Linearly interpolates between two vectors.
    /// </summary>
    /// <param name="alpha">Must be in interval [0, 1]</param>
    private static Vector3 Lerp(double alpha, Vector3 v1, Vector3 v2)
    {
        return alpha * v1 + (1 - alpha) * v2;
    }

    private static Vector3 Lerp(int x, int y, Vector3 n1, Vector3 n2, Vector3 n3)
    {
        return (1 + x + y) * n1 + x * n2 + y * n3;
    }

    private static double Clamp(double t, double min, double max)
    {
        if (t < min)
            return min;
        if (t > max)
            return max;
        return t;
    }

    private static Vector3 ToPoint2D(Vector3 point)
    {
        double z = point.Z;
        if (RenderOptions.ZFar - RenderOptions.ZNear != 0)
        {
            double q = RenderOptions.ZFar / (RenderOptions.ZFar - RenderOptions.ZNear);
            z = q * (point.Z - RenderOptions.ZNear);
        }

        //double distanceModifier = 4 * ((10.0 * Math.Log10(z / 10.0) * Math.Sqrt(z / 10.0)) / (3.0 * Math.Sin(10.0 * z) + 7.0)) * Math.Cos(2.0 * z) * Math.Sin(10.0 * z) + 10.0; 
        double distanceModifier = z;

        double x = point.Z == 0 ? point.X : point.X / distanceModifier;
        double y = point.Z == 0 ? point.Y : point.Y / distanceModifier;

        x = (RenderOptions.AspectRatio * RenderOptions.FieldOfViewAdjusted * x * RenderOptions.ScreenWidth * 0.5) + RenderOptions.ScreenWidth * 0.5;
        y = (RenderOptions.FieldOfViewAdjusted * y * RenderOptions.ScreenHeight * 0.5) + RenderOptions.ScreenHeight * 0.5;

        return new Vector3(x, y, z);
    }

    private static double Lerp(double t, double a1, double a2)
    {
        return a1 + t * (a2 - a1);
    }

    private static Vector3 Lerp(Vector3 t, Vector3 a1, Vector3 a2)
    {
        Vector3 vLerp = new Vector3();
        vLerp.X = Lerp(t.X, a1.X, a2.X);
        vLerp.Y = Lerp(t.Y, a1.Y, a2.Y);
        vLerp.Z = Lerp(t.Z, a1.Z, a2.Z);
        return vLerp;
    }
}
