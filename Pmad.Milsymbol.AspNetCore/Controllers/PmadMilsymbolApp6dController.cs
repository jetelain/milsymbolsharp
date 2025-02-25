using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Pmad.Milsymbol.App6d;
using Pmad.Milsymbol.AspNetCore.SymbolSelector.Bookmarks;

namespace Pmad.Milsymbol.AspNetCore.Controllers
{
    public class PmadMilsymbolApp6dController : Controller
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


        [HttpGet("/lib/pmad-milsymbol/app6d/all")]
        public IActionResult AllSymbols(string code)
        {
            return View(app6D);
        }

        [HttpPost("/lib/pmad-milsymbol/bookmarks")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoreBookmarks(string bookmarks)
        {
            var service = HttpContext.RequestServices.GetService<ISymbolBookmarksService>();
            if (service == null)
            {
                return NotFound();
            }
            if (!await service.CanUseBookmarksAsync(User))
            {
                return Forbid();
            }
            List<string>? data;
            try
            {
                data = JsonSerializer.Deserialize<List<string>>(bookmarks);
            }
            catch (JsonException)
            {
                return BadRequest();
            }
            if (data == null || data.Any(sidc => !App6dSymbolId.IsFormatValid(sidc)))
            {
                return BadRequest();
            }
            await service.SaveBookmarksAsync(User, data);
            return Ok();
        }

    }
}
