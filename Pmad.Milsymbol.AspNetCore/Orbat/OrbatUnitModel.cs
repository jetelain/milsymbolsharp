using System.Xml.Linq;

namespace Pmad.Milsymbol.AspNetCore.Orbat
{
    internal class OrbatUnitModel
    {
        public required XDocument SymbolIcon { get; set; }

        public string SymbolIconSvg => SymbolIcon.ToString(SaveOptions.DisableFormatting);

        public required string? Href { get; set; }

        public required string? Title { get; set; }

        public required List<OrbatUnitModel> SubUnits { get; set; }
    }
}
