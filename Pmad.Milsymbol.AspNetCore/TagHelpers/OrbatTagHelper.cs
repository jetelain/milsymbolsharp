using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
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
        private readonly IUrlHelperFactory _urlHelperFactory;
        private IUrlHelper _url;

        public OrbatTagHelper(ITagHelperComponentManager manager, IViewComponentHelper viewComponentHelper, IUrlHelperFactory urlHelperFactory)
        {
            _viewComponentHelper = viewComponentHelper;
            _manager = manager;
            _urlHelperFactory = urlHelperFactory;
        }

        [HtmlAttributeName("root-unit")]
        public IOrbatUnit? RootUnit { get; set; }

        [HtmlAttributeName("unit-title")]
        public Func<IOrbatUnit,string?>? GetUnitTitle { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        [HtmlAttributeName("unit-link-controller")]
        public string? UnitLinkController { get; set; }

        [HtmlAttributeName("unit-link-action")]
        public string? UnitLinkAction { get; set; }

        [HtmlAttributeName("unit-link-route")]
        public Func<IOrbatUnit, object?>? GetUnitLinkRoute { get; set; }

        private string? GetUnitHref(IOrbatUnit unit)
        {
            if (GetUnitLinkRoute != null)
            {
                if (_url == null)
                {
                    _url = _urlHelperFactory.GetUrlHelper(ViewContext!);
                }
                return _url.Action(UnitLinkAction, UnitLinkController, GetUnitLinkRoute(unit));
            }
            return null;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null; // Remove the <pmad-orbat> root element
            if (RootUnit != null)
            {
                (_viewComponentHelper as IViewContextAware)?.Contextualize(ViewContext);

                var content = await _viewComponentHelper.InvokeAsync(typeof(PmadOrbatViewComponent),
                    new Dictionary<string, object?> { 
                        { "rootUnit", RootUnit }, 
                        { "getTitle", GetUnitTitle }, 
                        { "getHref", (Func<IOrbatUnit, string?>)GetUnitHref } 
                    });

                output.Content.SetHtmlContent(content);
            }

            if (!_manager.Components.OfType<OrbatTagHelperComponent>().Any())
            {
                _manager.Components.Add(new OrbatTagHelperComponent());
            }
        }
    }
}
