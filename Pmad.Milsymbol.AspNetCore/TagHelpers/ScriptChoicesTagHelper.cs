using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Pmad.Milsymbol.AspNetCore.TagHelpers
{
    [HtmlTargetElement("pmad-script-choices")]
    public class ScriptChoicesTagHelper : TagHelper
    {
        private readonly ITagHelperComponentManager _manager;

        public ScriptChoicesTagHelper(ITagHelperComponentManager manager)
        {
            _manager = manager;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            AssetComponent.Get(_manager)
                .AddStylesheet("lib/pmad-milsymbol/css/choices.min.css")
                .AddScript("lib/pmad-milsymbol/js/choices.min.js");

            return Task.CompletedTask;
        }
    }
}
