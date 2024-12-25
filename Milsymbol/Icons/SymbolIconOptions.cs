using Jint;
using Jint.Native;

namespace Pmad.Milsymbol.Icons
{
    public class SymbolIconOptions
    {
        public double? Size { get; set; }

        public double? StrokeWidth { get; set; }

        public string? UniqueDesignation { get; set; }

        public string? AdditionalInformation { get; set; }

        public string? HigherFormation { get; set; }

        public string? CommonIdentifier { get; set; }

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
            return obj;
        }
    }
}
