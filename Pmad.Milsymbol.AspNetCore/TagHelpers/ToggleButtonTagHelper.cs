using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Pmad.Milsymbol.AspNetCore.TagHelpers
{
    [HtmlTargetElement("pmad-toggle-button")]
    public class ToggleButtonTagHelper : TagHelper
    {
        private readonly IDesignSystem designSystem;

        public ToggleButtonTagHelper(IDesignSystem designSystem) 
        { 
            this.designSystem = designSystem;
        }

        [HtmlAttributeName("type")]
        public string Type { get; set; } = "checkbox";
        [HtmlAttributeName("id")]
        public string? Id { get; set; }
        [HtmlAttributeName("name")]

        public string? Name { get; set; }
        [HtmlAttributeName("value")]

        public string? Value { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            
            output.Content.SetHtmlContent(designSystem.CreateToggleButton(await output.GetChildContentAsync(), Type, Id, Name, Value));

        }
    }
}
