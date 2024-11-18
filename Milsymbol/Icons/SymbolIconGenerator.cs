using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Jint;
using Jint.Native;

namespace Pmad.Milsymbol.Icons
{
    public class SymbolIconGenerator : IDisposable
    {
        private readonly Engine engine;
        private readonly JsValue symbolFunction;

        public SymbolIconGenerator(string standard = "APP6")
        {
            engine = new Engine();
            engine.Execute(GetEmbeddedScript());
            var ms = engine.GetValue("ms");
            symbolFunction = ms.Get(new JsString("Symbol"));
            engine.Invoke(ms.Get(new JsString("setStandard")), ms, new[] { new JsString(standard) });
        }

        private static string GetEmbeddedScript()
        {
            string lib;
            using (var reader = new StreamReader(typeof(SymbolIconGenerator).Assembly.GetManifestResourceStream("Pmad.Milsymbol.Icons.milsymbol.js") ?? throw new InvalidOperationException()))
            {
                lib = reader.ReadToEnd();
            }
            return lib;
        }

        /// <summary>
        /// Generate a symbol from a symbol identification coding
        /// </summary>
        /// <param name="sidc">Symbol identification coding</param>
        /// <param name="options">Symbol generation options</param>
        /// <returns></returns>
        public SymbolIcon Generate(string sidc, SymbolIconOptions options)
        {
            lock (engine)
            {
                return Generate(sidc, options.ToJsObject(engine));
            }
        }

        public List<SymbolIcon> Generate(IEnumerable<string> codes, SymbolIconOptions options)
        {
            lock (engine)
            {
                var optionsJS = options.ToJsObject(engine);
                return codes.Select(sidc => Generate(sidc, optionsJS)).ToList();
            }
        }

        private SymbolIcon Generate(string sidc, JsValue optionsJS)
        {
            var result = engine.Construct(symbolFunction, new JsValue[] { new JsString(sidc), optionsJS });
            var svg = engine.Invoke(result.Get(new JsString("asSVG")), result, new object[0]).ToString();
            var size = engine.Invoke(result.Get(new JsString("getSize")), result, new object[0]);
            var anchor = engine.Invoke(result.Get(new JsString("getAnchor")), result, new object[0]);
            var w = size.Get(new JsString("width")).AsNumber();
            var h = size.Get(new JsString("height")).AsNumber();
            var x = anchor.Get(new JsString("x")).AsNumber();
            var y = anchor.Get(new JsString("y")).AsNumber();
            return new SymbolIcon(svg, w, h, x, y);
        }

        public void Dispose()
        {
            engine.Dispose();
        }
    }
}
