using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace MandelbrotDemo.Web
{
    public class MandelbrotImageGenerator : IHttpHandler
    {
        private const int _max = 128;     // Maximum number of iterations
        private const double _escape = 4; // Escape value squared
        
        public void ProcessRequest(HttpContext context)
        {
            // Grab input parameters
            int level = Int32.Parse(context.Request["level"]);
            int x = Int32.Parse(context.Request["x"]);
            int y = Int32.Parse(context.Request["y"]);
            int width = Int32.Parse(context.Request["width"]);
            int height = Int32.Parse(context.Request["height"]);
            
            // Generate the bitmap
            Bitmap bitmap = DrawMandelbrotTile(level, x, y, width, height);

            // Set the response’s content type to image/jpeg
            context.Response.ContentType = "image/jpeg";

            // Write the image to the HTTP response
            bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);

            // Clean up and return
            bitmap.Dispose ();
        }

        public bool IsReusable
        {
            get { return true; }
        }

        private Bitmap DrawMandelbrotTile(int level, int posx, int posy, int width, int height)
        {
            // Create a bitmap to represent the requested tile
            Bitmap tile = new Bitmap(width, height);

            // Compute the number of tiles in each direction at this level
            int cx = Math.Max(1, (int)Math.Pow(2, level) / width);
            int cy = Math.Max(1, (int)Math.Pow(2, level) / height);

            // Compute starting values for real and imaginary components
            // (from -2.0 - 1.5i  to 1.0 + 1.5i)
            double r0 = -2.0 + (3.0 * posx / cx);
            double i0 = -1.5 + (3.0 * posy / cy);

            // Compute increments for real and imaginary components
            double dr = (3.0 / cx) / (width - 1);
            double di = (3.0 / cy) / (height - 1);

            // Iterate by row and column checking each pixel for
            // inclusion in the Mandelbrot set
            for (int x = 0; x < width; x++)
            {
                double cr = r0 + (x * dr);

                for (int y = 0; y < height; y++)
                {
                    double ci = i0 + (y * di);
                    double zr = cr;
                    double zi = ci;
                    int count = 0;

                    while (count < _max)
                    {
                        double zr2 = zr * zr;
                        double zi2 = zi * zi;

                        if (zr2 + zi2 > _escape)
                        {
                            tile.SetPixel(x, y, ColorMapper.GetColor(count, _max));
                            break;
                        }

                        zi = ci + (2.0 * zr * zi);
                        zr = cr + zr2 - zi2;
                        count++;
                    }

                    if (count == _max)
                        tile.SetPixel(x, y, Color.Black);
                }
            }

            // Return the bitmap
            return tile;
        }
    }
}
