using Microsoft.AspNetCore.Mvc.Rendering;
using Pmad.Milsymbol.App6d;

namespace Pmad.Milsymbol.AspNetCore.SymbolSelector
{
    internal class SymbolSelectorModel
    {
        public required string BaseId { get; internal set; }
        public required string Name { get; internal set; }
        public required App6dSymbolId SymbolId { get; internal set; }
        public required App6dSymbolDatabase App6d { get; internal set; }
    }
}