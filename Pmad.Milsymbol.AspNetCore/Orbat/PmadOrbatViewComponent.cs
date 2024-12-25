using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Pmad.Milsymbol.Icons;

namespace Pmad.Milsymbol.AspNetCore.Orbat
{
    public class PmadOrbatViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IOrbatUnit rootUnit)
        {
            var generator = new SymbolIconGenerator();
            var model = new OrbatModel
            {
                RootUnit = CreateViewModel(generator, rootUnit)
            };

            MakeUniformViewBoxes([model.RootUnit.SymbolIcon.Root!], true, 0.5);

            MakeUniformViewBoxes(model.RootUnit.SubUnits.Select(unit => unit.SymbolIcon.Root!), true, 0.4);

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

        private IEnumerable<OrbatUnitModel> All(OrbatUnitModel model)
        {
            yield return model;
            if (model.SubUnits != null)
            {
                foreach (var subUnit in model.SubUnits)
                {
                    foreach (var subSubUnit in All(subUnit))
                    {
                        yield return subSubUnit;
                    }
                }
            }
        }

        private OrbatUnitModel CreateViewModel(SymbolIconGenerator generator, IOrbatUnit rootUnit)
        {
            var icon = generator.Generate(rootUnit.Sdic, new SymbolIconOptions()
            {
                UniqueDesignation = rootUnit.UniqueDesignation,
                AdditionalInformation = rootUnit.AdditionalInformation,
                CommonIdentifier = rootUnit.CommonIdentifier,
                HigherFormation = rootUnit.HigherFormation
            });

            return new OrbatUnitModel()
            {
                SymbolIcon = XDocument.Parse(icon.Svg),
                SubUnits = rootUnit.SubUnits?.Select(subUnit => CreateViewModel(generator, subUnit)).ToList() ?? new List<OrbatUnitModel>(),
                Initial = rootUnit
            };
        }

    }
}
