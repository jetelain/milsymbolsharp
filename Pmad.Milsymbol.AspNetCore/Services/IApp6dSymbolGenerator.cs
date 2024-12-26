using Pmad.Milsymbol.Icons;

namespace Pmad.Milsymbol.AspNetCore.Services
{
    public interface IApp6dSymbolGenerator
    {
        Task<SymbolIcon> GenerateAsync(string sidc, SymbolIconOptions options);
    }
}
