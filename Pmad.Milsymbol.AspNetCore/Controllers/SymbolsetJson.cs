using Pmad.Milsymbol.App6d;

namespace Pmad.Milsymbol.AspNetCore.Controllers
{
    public class SymbolsetJson
    {
        public SymbolsetJson(List<IconJson> icons, List<ModifierOrAmplifierJson> modifiers1, List<ModifierOrAmplifierJson> modifiers2, List<ModifierOrAmplifierJson> amplifiers)
        {
            Icons = icons;
            Modifiers1 = modifiers1;
            Modifiers2 = modifiers2;
            Amplifiers = amplifiers;
        }

        public SymbolsetJson(App6dSymbolSet symbolSet)
        {
            Icons = symbolSet.MainIcons.Where(i => i.IsPointRendering).Select(i => new IconJson(i)).ToList();
            Modifiers1 = symbolSet.Modifiers1.Select(m => new ModifierOrAmplifierJson(m)).ToList();
            Modifiers2 = symbolSet.Modifiers2.Select(m => new ModifierOrAmplifierJson(m)).ToList();
            Amplifiers = symbolSet.Amplifiers.Select(a => new ModifierOrAmplifierJson(a)).ToList();
        }

        public List<IconJson> Icons { get; }
        public List<ModifierOrAmplifierJson> Modifiers1 { get; }
        public List<ModifierOrAmplifierJson> Modifiers2 { get; }
        public List<ModifierOrAmplifierJson> Amplifiers { get; }
    }
}