namespace Pmad.Milsymbol.AspNetCore.Orbat
{
    public interface IOrbatUnit
    {
        string Sdic { get; }

        string? UniqueDesignation { get; }

        string? AdditionalInformation { get; }

        string? CommonIdentifier { get; }

        string? HigherFormation { get; }

        string? Href { get; }

        IEnumerable<IOrbatUnit>? SubUnits { get; }
    }
}
