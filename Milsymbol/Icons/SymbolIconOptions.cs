using Jint;
using Jint.Native;

namespace Pmad.Milsymbol.Icons
{
    public class SymbolIconOptions
    {
        public double? Size { get; set; }

        public double? StrokeWidth { get; set; }

        public double? OutlineWidth { get; set; }

        public string? UniqueDesignation { get; set; }

        public string? AdditionalInformation { get; set; }

        public string? HigherFormation { get; set; }

        public string? CommonIdentifier { get; set; }

        public string? ReinforcedReduced { get; set; }

        public double? Direction { get; set; }

        internal JsValue ToJsObject(Engine engine)
        {
            var obj = new JsObject(engine);
            if (Size != null)
            {
                obj.FastSetDataProperty("size", new JsNumber(Size.Value));
            }
            if (StrokeWidth != null)
            {
                obj.FastSetDataProperty("strokeWidth", new JsNumber(StrokeWidth.Value));
            }
            if (OutlineWidth != null)
            {
                obj.FastSetDataProperty("outlineWidth", new JsNumber(OutlineWidth.Value));
            }
            if (UniqueDesignation != null)
            {
                obj.FastSetDataProperty("uniqueDesignation", new JsString(UniqueDesignation));
            }
            if (AdditionalInformation != null)
            {
                obj.FastSetDataProperty("additionalInformation", new JsString(AdditionalInformation));
            }
            if (HigherFormation != null)
            {
                obj.FastSetDataProperty("higherFormation", new JsString(HigherFormation));
            }
            if (CommonIdentifier != null)
            {
                obj.FastSetDataProperty("commonIdentifier", new JsString(CommonIdentifier));
            }
            if (ReinforcedReduced != null)
            {
                obj.FastSetDataProperty("reinforcedReduced", new JsString(ReinforcedReduced));
            }
            if (Direction != null)
            {
                obj.FastSetDataProperty("direction", new JsNumber(Direction.Value));
            }
            return obj;
        }
    }
}
