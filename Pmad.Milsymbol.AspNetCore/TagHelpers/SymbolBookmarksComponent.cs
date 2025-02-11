using System.Text.Json;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Pmad.Milsymbol.AspNetCore.SymbolSelector.Bookmarks;

namespace Pmad.Milsymbol.AspNetCore.TagHelpers
{
    internal class SymbolBookmarksComponent : TagHelperComponent
    {
        private readonly ISymbolBookmarksService bookmarksService;
        private readonly ViewContext viewContext;

        public SymbolBookmarksComponent(ISymbolBookmarksService bookmarksService, ViewContext viewContext)
        {
            this.bookmarksService = bookmarksService;
            this.viewContext = viewContext;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase))
            {
                if (await bookmarksService.CanUseBookmarksAsync(viewContext.HttpContext.User))
                {
                    var antiforgery = viewContext.HttpContext.RequestServices.GetRequiredService<IAntiforgery>();
                    var data = await bookmarksService.GetBookmarksAsync(viewContext.HttpContext.User);
                    output.Attributes.SetAttribute("data-pmad-milsymbol-bookmarks", JsonSerializer.Serialize(new SymbolBookmarksData()
                    {
                        Bookmarks = data.Bookmarks,
                        LastModifiedUtc = data.LastModifiedUtc,
                        Token = antiforgery.GetAndStoreTokens(viewContext.HttpContext).RequestToken,
                        Endpoint = "/lib/pmad-milsymbol/bookmarks"
                    }));
                }
            }
        }
    }
}
