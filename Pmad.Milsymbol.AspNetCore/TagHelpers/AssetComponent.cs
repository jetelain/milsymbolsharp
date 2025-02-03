using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Pmad.Milsymbol.AspNetCore.TagHelpers
{
    internal sealed class AssetComponent : TagHelperComponent
    {
        public List<string> Stylesheets { get; } = new List<string>() {  };

        public List<string> Scripts { get; } = new List<string>() {  };

        public AssetComponent()
        {

        }

        public static AssetComponent Get(ITagHelperComponentManager manager)
        {
            var instance = manager.Components.OfType<AssetComponent>().FirstOrDefault();
            if (instance == null)
            {
                manager.Components.Add(instance = new AssetComponent());
            }
            return instance;
        }

        public AssetComponent AddStylesheet(string asset)
        {
            if (!Stylesheets.Contains(asset))
            {
                Stylesheets.Add(asset);
            }
            return this;
        }

        public AssetComponent AddScript(string asset)
        {
            if (!Scripts.Contains(asset))
            {
                Scripts.Add(asset);
            }
            return this;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "head", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var asset in Stylesheets)
                {
                    output.PostContent.AppendHtml($"<link rel=\"stylesheet\" href=\"/{asset}?v={GetVersion(asset)}\" />");
                }
            }
            else if (string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var asset in Scripts)
                {
                    output.PostContent.AppendHtml($"<script src=\"/{asset}?v={GetVersion(asset)}\"></script>");
                }
            }
            return Task.CompletedTask;
        }

        private static string GetVersion(string asset)
        {
#if DEBUG
            var file = AspNetCoreMilsymbolExtensions.MilsymbolStaticFiles.GetFileInfo(asset);
            return file.LastModified.Ticks.ToString();
#else
            return ThisAssembly.AssemblyFileVersion;
#endif
        }
    }
}