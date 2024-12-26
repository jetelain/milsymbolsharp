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
#if DEBUG
                var file = AspNetCoreMilsymbolExtensions.MilsymbolStaticFiles.GetFileInfo("lib/pmad-milsymbol/css/orbat.css");
                var version = file.LastModified.Ticks.ToString();
#else
                var version = ThisAssembly.AssemblyFileVersion;
#endif
                output.PostContent.AppendHtml($"<link rel=\"stylesheet\" href=\"/lib/pmad-milsymbol/css/orbat.css?v={version}\" />");
            }
            return Task.CompletedTask;
        }
    }
}