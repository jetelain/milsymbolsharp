using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pmad.Milsymbol.AspNetCore
{
    internal class Bootstrap4DesignSystem : IDesignSystem
    {
        public string FormRow => "form-row";
        public string ControlLabel => "control-label";
        public string? InputGroupAppend => "input-group-append";

        public IHtmlContent CreateToggleButton(IHtmlContent content, string type, string? id, string? name, string? value)
        {
            var labelTag = new TagBuilder("label");
            labelTag.AddCssClass("btn btn-sm btn-outline-secondary");

            var inputTag = new TagBuilder("input");
            inputTag.Attributes.Add("type", type);
            if (!string.IsNullOrEmpty(name))
            {
                inputTag.Attributes.Add("name", name);
            }
            if (!string.IsNullOrEmpty(id))
            {
                inputTag.Attributes.Add("id", id);
            }
            if (!string.IsNullOrEmpty(value))
            {
                inputTag.Attributes.Add("value", value);
            }
            labelTag.InnerHtml.AppendHtml(inputTag.RenderSelfClosingTag());
            labelTag.InnerHtml.AppendHtml(content);

            return labelTag;
        }
    }
}
