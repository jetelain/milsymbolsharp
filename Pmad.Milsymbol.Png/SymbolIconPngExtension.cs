using Pmad.Milsymbol.Icons;
using SkiaSharp;
using Svg.Skia;

namespace Pmad.Milsymbol.Png
{
    public static class SymbolIconPngExtension
    {
        public static byte[] ToPng(this SymbolIcon icon, float scale = 1f)
        {
            var mem = new MemoryStream();
            SaveToPng(icon, mem, scale);
            return mem.ToArray();
        }

        public static void SaveToPng(this SymbolIcon icon, Stream target, float scale = 1f)
        {
            using (var xsvg = new SKSvg())
            {
                if (xsvg.FromSvg(icon.Svg) == null)
                {
                    throw new InvalidOperationException("Generated SVG seems invalid");
                }
                xsvg.Save(target, SKColor.Empty, SKEncodedImageFormat.Png, 100, scale, scale);
            }
        }
    }
}
