using Pmad.Milsymbol.AspNetCore.TagHelpers;

namespace Pmad.Milsymbol.AspNetCore.SymbolSelector
{
    public class PmadSymbolSelectorOptions
    {
        public required string Id { get; set; }
        public required string? Name { get; set; }
        public required string Value { get; set; }
        public SymbolSelectorLayout Layout { get; set; } = SymbolSelectorLayout.Default;
        public string? AllSymbolsHref { get; set; }
        public string? BookmarksHref { get; set; }
    }
}
