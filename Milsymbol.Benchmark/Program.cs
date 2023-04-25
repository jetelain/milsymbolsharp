using Jint.Native;
using Jint;
using Milsymbol.Symbols;
using System.Diagnostics;

namespace Milsymbol.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var generator = new SymbolGenerator();

            var sw = Stopwatch.StartNew();
            for (var i = 0; i < 10000; ++i)
            {
                generator.Generate("10063000001202040000", new SymbolOptions()).ToPng(); ;
            }
            Console.WriteLine($"JINT: {sw.ElapsedMilliseconds / 10000d} msec per symbol");
        }
    }
}