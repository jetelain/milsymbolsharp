using System.Linq;

namespace Milsymbol.Symbols.App6d
{
    public class App6dSymbolIdInfos
    {
        private readonly App6dSymbolId symbol;
        private readonly App6dSymbolSet set;
        private readonly App6dMainIcon icon;
        private readonly App6dModifier1 mod1;
        private readonly App6dModifier2 mod2;
        private readonly App6dSize size;

        public App6dSymbolIdInfos(App6dSymbolId symbol, App6dSymbolSet set, App6dMainIcon icon, App6dModifier1 mod1, App6dModifier2 mod2, App6dSize size)
        {
            this.symbol = symbol;
            this.set = set;
            this.icon = icon;
            this.mod1 = mod1;
            this.mod2 = mod2;
            this.size = size;
        }

        public static App6dSymbolIdInfos From(string sdic)
        {
            return From(new App6dSymbolId(sdic), App6dSymbolDatabase.Default);
        }

        public static App6dSymbolIdInfos From(App6dSymbolId symbol)
        {
            return From(symbol, App6dSymbolDatabase.Default);
        }

        public static App6dSymbolIdInfos From(App6dSymbolId symbol, App6dSymbolDatabase db)
        {
            var set = db.GetSymbolSet(symbol.SymbolSet);
            var icon = set.MainIcons.FirstOrDefault(i => i.Code == symbol.Icon);
            var mod1 = set.Modifiers1.FirstOrDefault(i => i.Code == symbol.Modifier1);
            var mod2 = set.Modifiers2.FirstOrDefault(i => i.Code == symbol.Modifier2);
            var size = set.Sizes.FirstOrDefault(i => i.Code == symbol.Size);
            return new App6dSymbolIdInfos(symbol, set, icon, mod1, mod2, size);
        }

        public App6dStandardIdentity1 StandardIdentity1 => symbol.StandardIdentity1;

        public App6dStandardIdentity2 StandardIdentity2 => symbol.StandardIdentity2;

        public string SymbolSet => set.Name;

        public string Entity => icon?.Entity;

        public string EntityType => icon?.EntityType;

        public string EntitySubtype => icon?.EntitySubtype;

        public string FirstModifier => mod1?.FirstModifier;

        public string SecondModifier => mod2?.SecondModifier;

        public string Size => size?.Name;
    }
}
