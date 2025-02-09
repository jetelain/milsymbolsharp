using System.Security.Claims;

namespace Pmad.Milsymbol.AspNetCore.SymbolSelector.Bookmarks
{
    public interface ISymbolBookmarksService
    {
        Task<bool> CanUseBookmarksAsync(ClaimsPrincipal user);

        Task<SymbolBookmarks> GetBookmarksAsync(ClaimsPrincipal user);

        Task SaveBookmarksAsync(ClaimsPrincipal user, List<string> bookmarks);
    }
}
