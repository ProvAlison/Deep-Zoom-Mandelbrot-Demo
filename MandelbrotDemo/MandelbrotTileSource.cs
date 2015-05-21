using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace MandelbrotDemo
{
    public class MandelbrotTileSource : MultiScaleTileSource
    {
        private int _width;  // Tile width
        private int _height; // Tile height

        public MandelbrotTileSource(int imageWidth, int imageHeight, int tileWidth, int tileHeight) :
            base(imageWidth, imageHeight, tileWidth, tileHeight, 0)
        {
            _width = tileWidth;
            _height = tileHeight;
        }

        protected override void GetTileLayers(int level, int posx, int posy, IList<object> sources)
        {
            string source = string.Format(
                "http://localhost:50216/MandelbrotImageGenerator.ashx?level={0}&x={1}&y={2}&width={3}&height={4}",
                level, posx, posy, _width, _height);

            sources.Add(new Uri(source, UriKind.Absolute));
        }
    }
}
