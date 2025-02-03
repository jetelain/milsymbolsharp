using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pmad.Milsymbol.App6d;

namespace Pmad.Milsymbol.AspNetCore.SymbolSelector
{
    public class PmadSymbolSelectorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string id, string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = "10031000000000000000";
            }
            var app6d = App6dSymbolDatabase.Default;
            var sdic = new App6dSymbolId(value);
            var model = new SymbolSelectorModel
            {
                Name = name,
                BaseId = id,
                App6d = app6d,
                SymbolId = sdic
            };
            return View(model);
        }
    }
}
