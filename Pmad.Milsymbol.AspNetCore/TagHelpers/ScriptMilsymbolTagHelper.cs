using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Pmad.Milsymbol.AspNetCore.TagHelpers
{
    [HtmlTargetElement("pmad-script-milsymbol")]
    public class ScriptMilsymbolTagHelper : TagHelper
    {
        private readonly ITagHelperComponentManager _manager;

        public ScriptMilsymbolTagHelper(ITagHelperComponentManager manager)
        {
            _manager = manager;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            AssetComponent.Get(_manager).AddScript("lib/pmad-milsymbol/js/milsymbol.js");

            return Task.CompletedTask;
        }
    }
}
