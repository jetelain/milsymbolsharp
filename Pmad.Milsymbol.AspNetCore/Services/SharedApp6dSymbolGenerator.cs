using Pmad.Milsymbol.Icons;

namespace Pmad.Milsymbol.AspNetCore.Services
{
    internal sealed class SharedApp6dSymbolGenerator : IApp6dSymbolGenerator, IDisposable
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private SymbolIconGenerator? symbolIconGenerator;

        public void Dispose()
        {
            symbolIconGenerator?.Dispose();
        }

        public async Task<SymbolIcon> GenerateAsync(string sidc, SymbolIconOptions options)
        {
            await semaphore.WaitAsync();
            try
            {
                if (symbolIconGenerator == null)
                {
                    symbolIconGenerator = new SymbolIconGenerator();
                }
                return symbolIconGenerator.Generate(sidc, options);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
