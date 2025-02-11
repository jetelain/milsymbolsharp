using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Pmad.Milsymbol.App6d
{
    public class App6dSymbolDatabase
    {
        private static readonly Lazy<App6dSymbolDatabase> defaultInstance = new Lazy<App6dSymbolDatabase>(LoadEmbedded, true);

        private readonly Dictionary<string, App6dSymbolSet> symbolSets;

        public App6dSymbolDatabase(Dictionary<string,App6dSymbolSet> symbolSets)
        { 
            this.symbolSets = symbolSets;
        
        }

        public IEnumerable<App6dSymbolSet> SymbolSets => symbolSets.Values;

        public static App6dSymbolDatabase Default => defaultInstance.Value;

        public static App6dSymbolDatabase LoadEmbedded()
        {
            using(var stream = typeof(App6dSymbolDatabase).Assembly.GetManifestResourceStream("Pmad.Milsymbol.App6d.app6d.json")!)
            {
                var sets = JsonSerializer.Deserialize(stream, App6dSymbolDatabaseContext.Default.DictionaryStringApp6dSymbolSet)!;

                return new App6dSymbolDatabase(sets);
            }
        }

        public App6dSymbolSet GetSymbolSet(string code)
        {
            return symbolSets[code];
        }

        public bool TryGetSymbolSet(string code, [MaybeNullWhen(false)] out App6dSymbolSet symbolSet)
        {
            return symbolSets.TryGetValue(code, out symbolSet);
        }
    }
}
