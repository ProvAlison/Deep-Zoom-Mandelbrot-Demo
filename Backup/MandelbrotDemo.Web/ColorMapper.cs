using System;
using System.Drawing;

namespace MandelbrotDemo
{
    public class ColorMapper
    {
        public static Color GetColor(int count, int max)
        {
            int h = max >> 1; // Divide max by 2
            int q = max >> 2; // Divide max by 4

            int r = (count * 255) / max;
            int g = ((count % h) * 255) / h;
            int b = ((count % q) * 255) / q;

            return Color.FromArgb(r, g, b);
        }
    }
}
