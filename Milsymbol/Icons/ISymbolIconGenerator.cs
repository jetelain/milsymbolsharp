
namespace Pmad.Milsymbol.Icons
{
    public interface ISymbolIconGenerator
    {
        List<SymbolIcon> Generate(IEnumerable<string> codes, SymbolIconOptions options);
        SymbolIcon Generate(string sidc);
        SymbolIcon Generate(string sidc, SymbolIconOptions options);
    }
}