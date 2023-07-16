using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Milsymbol.Symbols.App6d
{
    [DebuggerDisplay("{Code} : {Entity} - {EntityType} - {EntitySubtype}")]
    public class App6dMainIcon
    {
        [JsonPropertyName("Entity")]
        public string Entity { get; set; }

        [JsonPropertyName("Entity Type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string EntityType { get; set; }

        [JsonPropertyName("Entity Subtype")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string EntitySubtype { get; set; }

        [JsonPropertyName("Code")]
        public string Code { get; set; }

        [JsonPropertyName("Remarks")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Remarks { get; set; }

        [JsonPropertyName("Geometric Rendering")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string GeometricRendering { get; set; }

        public bool IsPointRendering => GeometricRendering == null || GeometricRendering == "Point";
    }
}
