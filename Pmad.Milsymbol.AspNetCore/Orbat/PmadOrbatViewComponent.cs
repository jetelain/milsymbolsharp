﻿using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Pmad.Milsymbol.AspNetCore.Services;
using Pmad.Milsymbol.Icons;

namespace Pmad.Milsymbol.AspNetCore.Orbat
{
    public class PmadOrbatViewComponent : ViewComponent
    {
        private readonly IApp6dSymbolGenerator _symbolGenerator;

        public PmadOrbatViewComponent(IApp6dSymbolGenerator symbolGenerator)
        {
            _symbolGenerator = symbolGenerator;
        }

        public async Task<IViewComponentResult> InvokeAsync(IOrbatUnit rootUnit, Func<IOrbatUnit, string?>? getTitle, Func<IOrbatUnit, string?>? getHref)
        {
            var model = new OrbatModel
            {
                RootUnit = await CreateViewModelAsync(rootUnit, getTitle, getHref)
            };

            // Level 1 / Root
            MakeUniformViewBoxes([model.RootUnit.SymbolIcon.Root!], true, 0.5);

            // Level 2 / Columns headers
            MakeUniformViewBoxes(model.RootUnit.SubUnits.Select(unit => unit.SymbolIcon.Root!), true, 0.4);

            // Level 3
            MakeUniformViewBoxes(model.RootUnit.SubUnits.SelectMany(unit => unit.SubUnits).Select(unit => unit.SymbolIcon.Root!), false, 0.3);
            
            return View(model);
        }

        private static void MakeUniformViewBoxes(IEnumerable<XElement> svgs, bool mustBeCentered, double sizeFactor)
        {
            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var maxWidth = double.MinValue;
            var maxHeight = double.MinValue;

            foreach (var svg in svgs)
            {
                var parts = svg.Attribute("viewBox")!.Value.Split(' ');
                minX = Math.Min(minX, double.Parse(parts[0], CultureInfo.InvariantCulture));
                minY = Math.Min(minY, double.Parse(parts[1], CultureInfo.InvariantCulture));
                maxWidth = Math.Max(maxWidth, double.Parse(parts[2], CultureInfo.InvariantCulture));
                maxHeight = Math.Max(maxHeight, double.Parse(parts[3], CultureInfo.InvariantCulture));
            }
            var x = minX;
            var width = maxWidth;

            if (mustBeCentered)
            {
                var dx = Math.Max(Math.Abs(minX - 100), Math.Abs((minX + maxWidth) - 100));
                x = 100 - dx;
                width = dx * 2;
            }

            var viewBoxString = FormattableString.Invariant($"{x:0.0} {minY:0.0} {width:0.0} {maxHeight:0.0}");
            var widthString = FormattableString.Invariant($"{width * sizeFactor:0.0}");
            var heightString = FormattableString.Invariant($"{maxHeight * sizeFactor:0.0}");

            foreach (var svg in svgs)
            {
                svg.SetAttributeValue("viewBox", viewBoxString);
                svg.SetAttributeValue("width", widthString);
                svg.SetAttributeValue("height", heightString);
            }
        }

        private async Task<OrbatUnitModel> CreateViewModelAsync(IOrbatUnit rootUnit, Func<IOrbatUnit, string?>? getTitle, Func<IOrbatUnit, string?>? getHref)
        {
            var icon = await _symbolGenerator.GenerateAsync(rootUnit.Sdic, new SymbolIconOptions()
            {
                UniqueDesignation = rootUnit.UniqueDesignation,
                CommonIdentifier = rootUnit.CommonIdentifier,
                HigherFormation = rootUnit.HigherFormation
            });

            var subUnits = rootUnit.SubUnits != null
                ? await Task.WhenAll(rootUnit.SubUnits.Select(subUnit => CreateViewModelAsync(subUnit, getTitle, getHref)))
                : Array.Empty<OrbatUnitModel>();

            return new OrbatUnitModel()
            {
                SymbolIcon = XDocument.Parse(icon.Svg),
                SubUnits = subUnits.ToList(),
                Href = getHref?.Invoke(rootUnit) ?? (rootUnit as IOrbatUnitViewModel)?.Href,
                Title = getTitle?.Invoke(rootUnit) ?? (rootUnit as IOrbatUnitViewModel)?.Title
            };
        }

    }
}
