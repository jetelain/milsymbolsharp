using System;
using System.IO;
using SkiaSharp;
using Svg.Skia;

namespace Milsymbol.Symbols
{
    public class Symbol
    {
        public string Svg { get; }

        public double Width { get; }

        public double Height { get; }

        public double AnchorX { get; }

        public double AnchorY { get; }

        internal Symbol(string svg, double w, double h, double x, double y)
        {
            Svg = svg;
            Width = w;
            Height = h;
            AnchorX = x;
            AnchorY = y;
        }

        public byte[] ToPng(float scale = 1f)
        {
            var mem = new MemoryStream();
            SaveToPng(mem, scale);
            return mem.ToArray();
        }

        public void SaveToPng(Stream target, float scale = 1f)
        {
            using (var xsvg = new SKSvg())
            {
                if (xsvg.FromSvg(Svg) == null)
                {
                    throw new InvalidOperationException("Generated SVG seems invalid");
                }
                xsvg.Save(target, SKColor.Empty, SKEncodedImageFormat.Png, 100, scale, scale);
            }
        }
    }
}