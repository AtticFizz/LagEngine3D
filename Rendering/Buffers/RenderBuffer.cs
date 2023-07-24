using System.Drawing;
using System.Runtime.Versioning;

namespace Rendering.Buffers;

[SupportedOSPlatform("windows")]
public class RenderBuffer
{
    private Bitmap _image;

    public Bitmap Image
    {
        get
        {
            return _image;
        }
    }

    public DepthBuffer Depth { get; set; }

    public int Width { get; private set; }
    public int Height { get; private set; }

    public Color this[int x, int y]
    {
        get
        {
            return _image.GetPixel(x, y);
        }
        set
        {
            _image.SetPixel(x, y, value);
        }
    }

    public RenderBuffer(int width, int height)
    {
        _image = new Bitmap(width, height);
        Depth = new DepthBuffer(width, height);
        Width = width;
        Height = height;
    }

    public void Clear(Color color)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                _image.SetPixel(x, y, color);
                Depth[x, y] = double.MaxValue;
            }
        }
    }
}
