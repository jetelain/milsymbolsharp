using System.Diagnostics;
using Pmad.Milsymbol.App6d;
using Pmad.Milsymbol.Icons;
using Pmad.Milsymbol.Png;

namespace Pmad.Milsymbol.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var generator = new SymbolIconGenerator();

            var builder = new App6dSymbolIdBuilder();

            // + IsHeadquarters true/false
            // + App6dStatus Present/Planned

            var sw = Stopwatch.StartNew();
            var count = 0;
            var real = 0;
            foreach (var stdid in Enum.GetValues<App6dStandardIdentity>())
            {
                builder.StandardIdentity = stdid;
                foreach (var set in App6dSymbolDatabase.Default.SymbolSets)
                {
                    builder.SymbolSet = set.Code;
                    Console.WriteLine(set.Code);
                    foreach (var icon in set.MainIcons.Concat(new[] { new App6dMainIcon { Code = "000000" } }))
                    {
                        if (icon.IsPointRendering)
                        { 
                            builder.Icon = icon.Code;
                            var sdic = builder.ToSIDC();
                            var ms = generator.Generate(sdic, new SymbolIconOptions());
                            if (!ms.IsUnknownSymbol)
                            {
                                var png = ms.ToPng();
                                var dir = Path.Combine("c:\\temp\\app6d", ((int)stdid).ToString(), set.Code);
                                Directory.CreateDirectory(dir);
                                File.WriteAllBytes(Path.Combine(dir, $"{icon.Code}.png"), png);
                                real++;
                            }
                        }
                        count++;
                    }
                }
            }
            Console.WriteLine($"{count} symbols, {real} generated, {sw.ElapsedMilliseconds / (double)count} msec per symbol");
            Console.WriteLine($"{sw.ElapsedMilliseconds} elapsed");
        }
    }
}