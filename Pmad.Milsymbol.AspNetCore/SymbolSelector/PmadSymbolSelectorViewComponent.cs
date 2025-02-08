using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pmad.Milsymbol.App6d;

namespace Pmad.Milsymbol.AspNetCore.SymbolSelector
{
    public class PmadSymbolSelectorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PmadSymbolSelectorOptions options)
        {
            if (string.IsNullOrEmpty(options.Value))
            {
                options.Value = "10031000000000000000";
            }
            var app6d = App6dSymbolDatabase.Default;
            var sdic = new App6dSymbolId(options.Value);
            var model = new SymbolSelectorModel
            {
                Name = options.Name,
                BaseId = options.Id,
                App6d = app6d,
                SymbolId = sdic,
                AllSymbolsHref = options.AllSymbolsHref ?? "/lib/pmad-milsymbol/app6d/all",
                BookmarksHref = options.BookmarksHref
            };
            return View(options.Layout.ToString(), model);
        }
    }
}
