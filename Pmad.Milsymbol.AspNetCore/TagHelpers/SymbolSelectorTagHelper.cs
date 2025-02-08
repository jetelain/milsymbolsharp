using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Pmad.Milsymbol.AspNetCore.SymbolSelector;

namespace Pmad.Milsymbol.AspNetCore.TagHelpers
{
    [HtmlTargetElement("pmad-symbol-selector")]
    public class SymbolSelectorTagHelper : TagHelper
    {
        private readonly IViewComponentHelper _viewComponentHelper;
        private readonly ITagHelperComponentManager _manager;

        public SymbolSelectorTagHelper(ITagHelperComponentManager manager, IViewComponentHelper viewComponentHelper)
        {
            _viewComponentHelper = viewComponentHelper;
            _manager = manager;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression? For { get; set; }

        [HtmlAttributeName("layout")]
        public SymbolSelectorLayout Layout { get; set; }

        [HtmlAttributeName("all-symbols-href")]
        public string? AllSymbolsHref { get; set; }

        [HtmlAttributeName("bookmarks-href")]
        public string? BookmarksHref { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null; // Remove the <pmad-orbat> root element

            (_viewComponentHelper as IViewContextAware)?.Contextualize(ViewContext);

            var content = await _viewComponentHelper.InvokeAsync(typeof(PmadSymbolSelectorViewComponent),
                new Dictionary<string, object?> {
                    {"options", 
                        new PmadSymbolSelectorOptions()
                        {
                            Name = For?.Name ?? "selector",
                            Id = For?.Name ?? "selector",
                            Value = For?.Model?.ToString() ?? string.Empty,
                            Layout = Layout,
                            AllSymbolsHref = AllSymbolsHref,
                            BookmarksHref = BookmarksHref
                        }
                    }
                });

            output.Content.SetHtmlContent(content);

            AssetComponent.Get(_manager)
                .AddStylesheet("lib/pmad-milsymbol/css/choices.min.css")
                .AddStylesheet("lib/pmad-milsymbol/css/symbol-selector.css")
                .AddScript("lib/pmad-milsymbol/js/choices.min.js")
                .AddScript("lib/pmad-milsymbol/js/milsymbol.js")
                .AddScript("lib/pmad-milsymbol/js/symbol-selector.js");
        }
    }
}
