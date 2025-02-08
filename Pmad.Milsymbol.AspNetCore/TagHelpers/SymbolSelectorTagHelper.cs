using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Pmad.Milsymbol.AspNetCore.Orbat;
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

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null; // Remove the <pmad-orbat> root element
            
            (_viewComponentHelper as IViewContextAware)?.Contextualize(ViewContext);

            var content = await _viewComponentHelper.InvokeAsync(typeof(PmadSymbolSelectorViewComponent),
                new Dictionary<string, object?> {
                    {"id", For?.Name ?? "selector"},
                    {"name", For?.Name ?? "selector"},
                    {"value", For?.Model?.ToString() ?? string.Empty},
                    {"layout", Layout},
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
