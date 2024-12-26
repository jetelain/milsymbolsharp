namespace Pmad.Milsymbol.AspNetCore.Orbat
{
    /// <summary>
    /// Default implementation of <see cref="IOrbatUnit"/>.
    /// </summary>
    public sealed class OrbatUnit : IOrbatUnit
    {
        public string Sdic { get; set; } = "30031000000000000000";

        public string? UniqueDesignation { get; set; }

        public string? AdditionalInformation { get; set; }

        public string? CommonIdentifier { get; set; }

        public string? HigherFormation { get; set; }

        public string? Href { get; set; }

        public List<IOrbatUnit>? SubUnits { get; set; }

        IEnumerable<IOrbatUnit>? IOrbatUnit.SubUnits => SubUnits;
    }
}
