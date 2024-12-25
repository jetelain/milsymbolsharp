using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Pmad.Milsymbol.AspNetCore.Orbat;

namespace Pmad.Milsymbol.AspNetCore.TagHelpers
{
    [HtmlTargetElement("pmad-orbat")]
    public class OrbatTagHelper : TagHelper
    {
        private readonly IViewComponentHelper _viewComponentHelper;
        private readonly ITagHelperComponentManager _manager;
        private readonly IFileVersionProvider _fileVersionProvider;

        public OrbatTagHelper(ITagHelperComponentManager manager, IViewComponentHelper viewComponentHelper, IFileVersionProvider fileVersionProvider)
        {
            _viewComponentHelper = viewComponentHelper;
            _manager = manager;
            _fileVersionProvider = fileVersionProvider;
        }

        [HtmlAttributeName("root-unit")]
        public IOrbatUnit? RootUnit { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            
            output.TagName = null; // Remove the <pmad-orbat> root element
            if (RootUnit != null)
            {
                (_viewComponentHelper as IViewContextAware)?.Contextualize(ViewContext);
                var content = await _viewComponentHelper.InvokeAsync(typeof(PmadOrbatViewComponent), new { rootUnit = RootUnit });
                output.Content.SetHtmlContent(content);
            }
            if (!_manager.Components.OfType<OrbatTagHelperComponent>().Any())
            {
                _manager.Components.Add(new OrbatTagHelperComponent());
            }
        }
    }
}
