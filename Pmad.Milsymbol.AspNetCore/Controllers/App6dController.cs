using Microsoft.AspNetCore.Mvc;
using Pmad.Milsymbol.App6d;

namespace Pmad.Milsymbol.AspNetCore.Controllers
{
    public class App6dController : Controller
    {
        private readonly App6dSymbolDatabase app6D = App6dSymbolDatabase.Default;

        [HttpGet("/lib/pmad-milsymbol/app6d/{code}.json")]
        [ResponseCache(Duration = 86400)]
        public IActionResult SymbolSet(string code)
        {
            if (!app6D.TryGetSymbolSet(code, out var symbolSet))
            {
                return NotFound();
            }
            return Json(new SymbolsetJson(symbolSet));
        }

    }
}
