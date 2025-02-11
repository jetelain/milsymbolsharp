using System.Text.Json.Serialization;

namespace Pmad.Milsymbol.AspNetCore.SymbolSelector.Bookmarks
{
    public class SymbolBookmark
    {
        [JsonPropertyName("sidc")]
        public required string Sidc { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }
    }
}