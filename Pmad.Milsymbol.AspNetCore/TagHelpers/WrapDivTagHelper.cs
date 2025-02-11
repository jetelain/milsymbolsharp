using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Pmad.Milsymbol.AspNetCore.TagHelpers
{
    [HtmlTargetElement("pmad-wrap-div")]
    public class ClassOrStripTagHelper : TagHelper
    {
        [HtmlAttributeName("class")]
        public string? Class { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if ( string.IsNullOrEmpty(Class))
            {
                output.TagName = null;
            }
            else
            {
                output.TagName = "div";
                output.Attributes.SetAttribute("class", Class);
            }
            return Task.CompletedTask;
        }
    }
}
