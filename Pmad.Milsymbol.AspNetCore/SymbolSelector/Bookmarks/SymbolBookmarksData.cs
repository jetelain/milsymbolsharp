using System.Text.Json.Serialization;

namespace Pmad.Milsymbol.AspNetCore.SymbolSelector.Bookmarks
{
    internal class SymbolBookmarksData
    {
        [JsonPropertyName("bookmarks")]
        public required List<SymbolBookmark> Bookmarks { get; set; }

        [JsonPropertyName("timestamp")]
        public required DateTime LastModifiedUtc { get; set; }

        [JsonPropertyName("endpoint")]
        public required string Endpoint { get; set; }

        [JsonPropertyName("token")]
        public required string? Token { get; set; }
    }
}
