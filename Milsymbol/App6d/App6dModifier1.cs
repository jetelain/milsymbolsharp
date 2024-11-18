using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Pmad.Milsymbol.App6d
{
    [DebuggerDisplay("{Code} : {FirstModifier}")]
    public class App6dModifier1
    {
        [JsonPropertyName("First Modifier")]
        public string? FirstModifier { get; set; }

        [JsonPropertyName("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("Remarks")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Remarks { get; set; }
    }
}
