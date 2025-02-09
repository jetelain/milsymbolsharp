using System.Security.Claims;
using Pmad.Milsymbol.AspNetCore.SymbolSelector.Bookmarks;

namespace MvcDemoBS5
{
    public class SampleSymbolBookmarksService : ISymbolBookmarksService
    {
        public Task<bool> CanUseBookmarksAsync(ClaimsPrincipal user)
        {
            return Task.FromResult(true);
        }

        public Task<SymbolBookmarks> GetBookmarksAsync(ClaimsPrincipal user)
        {
            return Task.FromResult(new SymbolBookmarks() {
                Bookmarks = new List<SymbolBookmark>() { new SymbolBookmark() { Sidc = "10031000001211050000" } },
                LastModifiedUtc = DateTime.MinValue.ToUniversalTime()
            });
        }

        public Task SaveBookmarksAsync(ClaimsPrincipal user, List<string> bookmarks)
        {
            return Task.CompletedTask;
        }
    }
}
