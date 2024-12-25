using System.Xml.Linq;

namespace Pmad.Milsymbol.AspNetCore.Orbat
{
    internal class OrbatUnitModel
    {
        public required XDocument SymbolIcon { get; set; }

        public string SymbolIconSvg => SymbolIcon.ToString(SaveOptions.DisableFormatting);

        public string? Href => Initial.Href;

        public required List<OrbatUnitModel> SubUnits { get; set; }

        public required IOrbatUnit Initial { get; set; }
    }
}
