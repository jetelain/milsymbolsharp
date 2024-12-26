namespace Pmad.Milsymbol.AspNetCore.Orbat
{
    public interface IOrbatUnitViewModel : IOrbatUnit
    {
        string? Href { get; }

        string? Title { get; }
    }
}
