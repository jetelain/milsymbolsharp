using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Pmad.Milsymbol.App6d
{
    [DebuggerDisplay("{Code} : {Name}")]
    public class App6dSymbolSet
    {
        [JsonPropertyName("symbolset")]
        public required string Code { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("mainIcon")]
        public required List<App6dMainIcon> MainIcons { get; set; }

        [JsonPropertyName("modifier1")]
        public required List<App6dModifier1> Modifiers1 { get; set; }

        [JsonPropertyName("modifier2")]
        public required List<App6dModifier2> Modifiers2 { get; set; }

        [JsonPropertyName("amplifier")]
        public List<App6dAmplifier> Amplifiers { get; set; } = new List<App6dAmplifier>();
    }
}
