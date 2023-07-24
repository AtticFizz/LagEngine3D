namespace Rendering.Buffers;

public class DepthBuffer
{
    private double[,] _values;
    
    public int Width { get; internal set; }
    public int Height { get; internal set; }

    public double this[int x, int y]
    {
        get
        {
            return _values[x, y];
        }
        set
        {
            _values[x, y] = value;
        }
    }

    public DepthBuffer(double[,] values)
    {
        Width = values.GetLength(0);
        Height = values.GetLength(1);
        _values = values;
    }

    public DepthBuffer(int width, int height, double defaultValue = double.MaxValue)
    {
        Width = width;
        Height = height;
        _values = new double[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                _values[x, y] = defaultValue;
            }
        }
    }
}