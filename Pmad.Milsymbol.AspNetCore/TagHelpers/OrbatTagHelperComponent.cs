using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Pmad.Milsymbol.AspNetCore.TagHelpers
{
    internal sealed class OrbatTagHelperComponent : TagHelperComponent
    {

        public OrbatTagHelperComponent()
        {

        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "head", StringComparison.OrdinalIgnoreCase))
            {
                var file = Setup.MilsymbolStaticFiles.GetFileInfo("lib/pmad-milsymbol/css/orbat.css");

                output.PostContent.AppendHtml($"<link rel=\"stylesheet\" href=\"/lib/pmad-milsymbol/css/orbat.css?{file.LastModified.Ticks}\" />");
            }
            return Task.CompletedTask;
        }
    }
}