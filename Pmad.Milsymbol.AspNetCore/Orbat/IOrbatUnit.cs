namespace Pmad.Milsymbol.AspNetCore.Orbat
{
    public interface IOrbatUnit
    {
        string Sdic { get; }

        string? UniqueDesignation { get; }

        string? CommonIdentifier { get; }

        string? HigherFormation { get; }

        IEnumerable<IOrbatUnit>? SubUnits { get; }
    }
}
