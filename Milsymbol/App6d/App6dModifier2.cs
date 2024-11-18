using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Pmad.Milsymbol.App6d
{
    [DebuggerDisplay("{Code} : {SecondModifier}")]
    public class App6dModifier2
    {
        [JsonPropertyName("Second Modifier")]
        public string? SecondModifier { get; set; }

        [JsonPropertyName("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("Remarks")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Remarks { get; set; }
    }
}
