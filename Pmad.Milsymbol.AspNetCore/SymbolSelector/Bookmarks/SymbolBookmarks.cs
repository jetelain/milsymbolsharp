using System.Text.Json.Serialization;

namespace Pmad.Milsymbol.AspNetCore.SymbolSelector.Bookmarks
{
    public class SymbolBookmarks
    {
        public required List<SymbolBookmark> Bookmarks { get; set; }

        public required DateTime LastModifiedUtc { get; set; }
    }
}