using System.Text.Json.Serialization;

namespace Pmad.Milsymbol.App6d
{
    [JsonSerializable(typeof(Dictionary<string, App6dSymbolSet>))]
    internal partial class App6dSymbolDatabaseContext : JsonSerializerContext
    {
    }
}
