using Pmad.Milsymbol.App6d;

namespace Pmad.Milsymbol.AspNetCore.Controllers
{
    public class ModifierOrAmplifierJson
    {
        public ModifierOrAmplifierJson(string code, string label)
        {
            Code = code;
            Label = label;
        }

        public ModifierOrAmplifierJson(App6dModifier1 m)
        {
            Code = m.Code ?? string.Empty;
            Label = m.FirstModifier ?? string.Empty;
        }

        public ModifierOrAmplifierJson(App6dModifier2 m)
        {
            Code = m.Code ?? string.Empty;
            Label = m.SecondModifier ?? string.Empty;
        }

        public ModifierOrAmplifierJson(App6dAmplifier a)
        {
            Code = a.Code ?? string.Empty;
            Label = a.Name ?? string.Empty;
        }

        public string Code { get; }

        public string Label { get; }
    }
}