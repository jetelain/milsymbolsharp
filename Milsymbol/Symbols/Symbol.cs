﻿using System;
using System.IO;
using SkiaSharp;
using Svg.Skia;

namespace Milsymbol.Symbols
{
    public class Symbol
    {
        private static readonly string UnknownSymbolSvg = "<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.2\" baseProfile=\"tiny\" width=\"108\" height=\"108\" viewBox=\"46 46 108 108\"><path d=\"m 94.8206,78.1372 c -0.4542,6.8983 0.6532,14.323 5.3424,19.6985 4.509,5.6933 11.309,9.3573 14.98,15.7283 3.164,6.353 -0.09,14.245 -5.903,17.822 -7.268,4.817 -18.6219,2.785 -22.7328,-5.249 -1.5511,-2.796 -2.3828,-5.931 -2.8815,-9.071 -3.5048,0.416 -7.0093,0.835 -10.5142,1.252 0.8239,8.555 5.2263,17.287 13.2544,21.111 7.8232,3.736 17.1891,3.783 25.3291,1.052 8.846,-3.103 15.737,-11.958 15.171,-21.537 0.05,-6.951 -4.272,-12.85 -9.134,-17.403 -4.526,-4.6949 -11.048,-8.3862 -12.401,-15.2748 -1.215,-2.3639 -0.889,-8.129 -0.889,-8.129 z m -0.6253,-20.5177 0,11.6509 11.6527,0 0,-11.6509 z\" stroke-width=\"4\" stroke=\"none\" fill=\"black\" ></path></svg>";

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

        public bool IsUnknownSymbol => Svg == UnknownSymbolSvg;

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