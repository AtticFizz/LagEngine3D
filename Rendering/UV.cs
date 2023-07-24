using System.Drawing;
using System.Runtime.Versioning;

namespace Rendering;

[SupportedOSPlatform("windows")]
public static class UV
{
    public static Bitmap Create(int width, int height)
    {
        Bitmap noiseImage = new(width, height);

        double widthRatio = width / (double)255;
        double heightRatio = height / (double)255;

        for (int y = 0; y < height; y++)
        {
            byte r = (byte)(y / heightRatio);
            for (int x = 0; x < width; x++)
            {
                if (y == 0)
                {
                    byte g = (byte)(x / widthRatio);
                    Color pixelColor = Color.FromArgb(255, r, g, 0);
                    noiseImage.SetPixel(x, y, pixelColor);
                }
                else
                {
                    byte g = noiseImage.GetPixel(x, 0).G;
                    Color pixelColor = Color.FromArgb(255, r, g, 0);
                    noiseImage.SetPixel(x, y, pixelColor);
                }
            }
        }

        return noiseImage;
    }
}