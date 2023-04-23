using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Esprima;
using Jint;
using Jint.Native;

namespace Milsymbol.Symbols
{
    public class SymbolGenerator : IDisposable
    {
        private readonly Engine engine;
        private readonly JsValue symbolFunction;

        public SymbolGenerator()
        {
            engine = new Engine();
            engine.Execute(LoadScript());
            symbolFunction = engine.GetValue("ms").Get(new JsString("Symbol"));
        }

        private static Esprima.Ast.Script LoadScript()
        {
            string lib = GetEmbeddedScript();
            var parser = new JavaScriptParser();
            var script = parser.ParseScript(lib);
            return script;
        }

        private static string GetEmbeddedScript()
        {
            string lib;
            using (var reader = new StreamReader(typeof(SymbolGenerator).Assembly.GetManifestResourceStream("Milsymbol.Symbols.milsymbol.js") ?? throw new InvalidOperationException()))
            {
                lib = reader.ReadToEnd();
            }
            return lib;
        }

        public Symbol Generate(string sidc, SymbolOptions options)
        {
            lock (engine)
            {
                return Generate(sidc, options.ToJsObject(engine));
            }
        }

        public List<Symbol> Generate(IEnumerable<string> codes, SymbolOptions options)
        {
            lock (engine)
            {
                var optionsJS = options.ToJsObject(engine);
                return codes.Select(sidc => Generate(sidc, optionsJS)).ToList();
            }
        }

        private Symbol Generate(string sidc, JsValue optionsJS)
        {
            var result = engine.Construct(symbolFunction, new JsValue[] { new JsString(sidc), optionsJS });
            var svg = engine.Invoke(result.Get(new JsString("asSVG")), result, new object[0]).ToString();
            var size = engine.Invoke(result.Get(new JsString("getSize")), result, new object[0]);
            var anchor = engine.Invoke(result.Get(new JsString("getAnchor")), result, new object[0]);
            var w = size.Get(new JsString("width")).AsNumber();
            var h = size.Get(new JsString("height")).AsNumber();
            var x = anchor.Get(new JsString("x")).AsNumber();
            var y = anchor.Get(new JsString("y")).AsNumber();
            return new Symbol(svg, w, h, x, y);
        }

        public void Dispose()
        {
            engine.Dispose();
        }
    }
}
