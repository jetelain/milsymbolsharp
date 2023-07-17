using System.Diagnostics;
using Milsymbol.App6d;
using Milsymbol.Icons;

namespace Milsymbol.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var generator = new SymbolIconGenerator();

            var builder = new App6dSymbolIdBuilder();

            var sw = Stopwatch.StartNew();
            var count = 0;
            var real = 0;
            foreach (var id2 in Enum.GetValues<App6dStandardIdentity2>())
            {
                builder.StandardIdentity2 = id2;
                foreach (var set in App6dSymbolDatabase.Default.SymbolSets)
                {
                    builder.SymbolSet = set.Code;
                    Console.WriteLine(set.Code);
                    foreach (var icon in set.MainIcons)
                    {
                        if (icon.IsPointRendering)
                        { 
                            builder.Icon = icon.Code;
                            var sdic = builder.ToSIDC();
                            var ms = generator.Generate(sdic, new SymbolIconOptions());
                            if (!ms.IsUnknownSymbol)
                            {
                                var png = ms.ToPng();
                                //var dir = Path.Combine("c:\\temp\\app6d", sdic.Substring(0, 4));
                                //Directory.CreateDirectory(dir);
                                //File.WriteAllBytes(Path.Combine(dir, $"{sdic.Substring(4)}.png"), png);
                                real++;
                            }
                        }
                        count++;
                    }
                }
            }
            Console.WriteLine($"{count} symbols, {real} generated, {sw.ElapsedMilliseconds / (double)count} msec per symbol");
        }
    }
}