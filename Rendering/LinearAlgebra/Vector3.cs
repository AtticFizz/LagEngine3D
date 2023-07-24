using Options;

namespace Rendering.LinearAlgebra;

public class Vector3
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public double Length
    {
        get
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
    }

    public Vector3 Normalized
    {
        get
        {
            double length = Length;
            return new Vector3(X / length, Y / length, Z / length);
        }
    }

    public Vector3(double x = 0, double y = 0, double z = 0)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double Dot(Vector3 other)
    {
        return X * other.X + Y * other.Y + Z * other.Z;
    }

    public Vector3 Cross(Vector3 other)
    {
        double x = Y * other.Z - Z * other.Y;
        double y = Z * other.X - X * other.Z;
        double z = X * other.Y - Y * other.X;
        return new Vector3(x, y, z);
    }

    public void Normalize()
    {
        double length = Length;
        X /= length;
        Y /= length;
        Z /= length;
    }

    public void RotateAroundX(double theta)
    {
        double y = Y * (double)Math.Cos(theta) - Z * (double)Math.Sin(theta);
        double z = Y * (double)Math.Sin(theta) + Z * (double)Math.Cos(theta);
        Y = y;
        Z = z;
    }

    public void RotateAroundY(double theta)
    {
        double x = X * (double)Math.Cos(theta) - Z * (double)Math.Sin(theta);
        double z = X * (double)Math.Sin(theta) + Z * (double)Math.Cos(theta);
        X = x;
        Z = z;
    }

    public void RotateAroundZ(double theta)
    {
        double x = X * (double)Math.Cos(theta) - Y * (double)Math.Sin(theta);
        double y = X * (double)Math.Sin(theta) + Y * (double)Math.Cos(theta);
        X = x;
        Y = y;
    }

    public void Rotate(double thetaX, double thetaY, double thetaZ) // optimize
    {
        RotateAroundX(thetaX);
        RotateAroundY(thetaY);
        RotateAroundZ(thetaZ);
    }

    public void Rotate(double theta, Vector3 axis) // rotation on x axis does not work
    {
        double sin = Math.Sin(theta);
        double cos = Math.Cos(theta);

        double ux = axis.X;
        double uy = axis.Y;
        double uz = axis.Z;

        double x = X * (cos + ux * ux * (1 - cos)) + Y * (uy * ux * (1 - cos) + uz * sin) + Z * (uz * ux * (1 - cos) - uy * sin);
        double y = X * (ux * uy * (1 - cos) - uz * sin) + Y * (cos + uy * uy * (1 - cos)) + Z * ux * uy * (1 - cos) + ux * sin;
        double z = X * (ux * uz * (1 - cos) + uy * sin) + Y * (uy * uz * (1 - cos) - ux * sin) + Z * (cos + uz * uz * (1 - cos));

        X = (double)x;
        Y = (double)y;
        Z = (double)z;
    }

    public Vector3 ToPoint2D()
    {
        double z = Z;
        if (RenderOptions.ZFar - RenderOptions.ZNear != 0)
        {
            double q = RenderOptions.ZFar / (RenderOptions.ZFar - RenderOptions.ZNear);
            z = q * (Z - RenderOptions.ZNear);
        }

        //double distanceModifier = 4 * ((10.0 * Math.Log10(z / 10.0) * Math.Sqrt(z / 10.0)) / (3.0 * Math.Sin(10.0 * z) + 7.0)) * Math.Cos(2.0 * z) * Math.Sin(10.0 * z) + 10.0; 
        double distanceModifier = z;

        double x = Z == 0 ? X : X / distanceModifier;
        double y = Z == 0 ? Y : Y / distanceModifier;

        x = (RenderOptions.AspectRatio * RenderOptions.FieldOfViewAdjusted * x * RenderOptions.ScreenWidth * 0.5) + RenderOptions.ScreenWidth * 0.5;
        y = (RenderOptions.FieldOfViewAdjusted * y * RenderOptions.ScreenHeight * 0.5) + RenderOptions.ScreenHeight * 0.5;

        return new Vector3(x, y, z);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public static bool operator ==(Vector3 left, Vector3 right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null)
            return false;
        if (right is null)
            return false;
        return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
    }

    public static bool operator !=(Vector3 left, Vector3 right)
    {
        return !(left == right);
    }

    public static Vector3 operator +(Vector3 left, Vector3 right)
    {
        return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static Vector3 operator -(Vector3 left, Vector3 right)
    {
        return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }

    public static Vector3 operator *(double k, Vector3 v)
    {
        return new Vector3(k * v.X, k * v.Y, k * v.Z);
    }

    public static Vector3 operator /(Vector3 v, double k)
    {
        return new Vector3(v.X / k, v.Y / k, v.Z / k);
    }

    /// <summary>
    /// Vector multiplication component by component.
    /// </summary>
    public static Vector3 operator *(Vector3 left, Vector3 right)
    {
        return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
    }

    /// <summary>
    /// Vector division component by component.
    /// </summary>
    public static Vector3 operator /(Vector3 left, Vector3 right)
    {
        return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        return X == ((Vector3)obj).X && Y == ((Vector3)obj).Y && Z == ((Vector3)obj).Z;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
    }

    public Vector3 Copy()
    {
        return new Vector3(X, Y, Z);
    }
}
