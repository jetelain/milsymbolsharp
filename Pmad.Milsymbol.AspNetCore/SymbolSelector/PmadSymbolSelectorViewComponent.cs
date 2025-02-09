using Microsoft.AspNetCore.Mvc;
using Pmad.Milsymbol.App6d;

namespace Pmad.Milsymbol.AspNetCore.SymbolSelector
{
    public class PmadSymbolSelectorViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PmadSymbolSelectorOptions options)
        {
            var app6d = App6dSymbolDatabase.Default;
            if (!App6dSymbolId.TryParse(options.Value, out var sidc))
            {
                sidc = new App6dSymbolId("10031000000000000000");
            }
            var model = new SymbolSelectorModel
            {
                Name = options.Name,
                BaseId = options.Id,
                App6d = app6d,
                SymbolId = sidc,
                AllSymbolsHref = options.AllSymbolsHref ?? "/lib/pmad-milsymbol/app6d/all",
                BookmarksHref = options.BookmarksHref
            };
            return Task.FromResult<IViewComponentResult>(View(options.Layout.ToString(), model));
        }
    }
}
