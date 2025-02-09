using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pmad.Milsymbol.AspNetCore
{
    internal class Boostrap5 : IDesignSystem
    {
        public string FormRow => "row";
        public string ControlLabel => "form-label";
        public string? InputGroupAppend => null;

        public IHtmlContent CreateToggleButton(IHtmlContent content, string type, string? id, string? name, string? value)
        {
            var inputTag = new TagBuilder("input");
            inputTag.Attributes.Add("type", type);
            inputTag.AddCssClass("btn-check");
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
            inputTag.Attributes.Add("autocomplete", "off");

            var labelTag = new TagBuilder("label");
            labelTag.AddCssClass("btn btn-sm btn-outline-secondary");
            if (!string.IsNullOrEmpty(id))
            {
                labelTag.Attributes.Add("for", id);
            }
            labelTag.InnerHtml.AppendHtml(content);

            var htmlContentBuilder = new HtmlContentBuilder();
            htmlContentBuilder.AppendHtml(inputTag.RenderSelfClosingTag());
            htmlContentBuilder.AppendHtml(labelTag);
            return htmlContentBuilder;
        }
    }
}
