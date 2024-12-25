using Pmad.Milsymbol.AspNetCore.Orbat;

namespace MvcDemo.Models
{
    public class HomeViewModel
    {

        public IOrbatUnit RootUnit { get; } = new OrbatUnitSample(common: "1er FORBAN DE MARINE")
        {
            new OrbatUnitSample(common: "1ERE SECTION", higher:"COMBAT ET MANŒUVRE")
            {
                new OrbatUnitSample(common: "11.1"),
                new OrbatUnitSample(common: "12.1"),
                new OrbatUnitSample(common: "13.1"),
            },
            new OrbatUnitSample(common: "2EME SECTION", higher:"APPUIS")
            {
                new OrbatUnitSample(common: "21.2"),
                new OrbatUnitSample(common: "22.2"),
                new OrbatUnitSample(common: "23.2"),
            },
            new OrbatUnitSample(common: "3EME PELOTON", higher:"CAVALERIE BLINDEE")
            {
                new OrbatUnitSample(common: "31.3"),
                new OrbatUnitSample(common: "32.3"),
                new OrbatUnitSample(common: "33.3"),
            },
            new OrbatUnitSample(common: "4EME SECTION", higher:"AIDE A L'ENGAGEMENT DEBARQUE")
            {
                new OrbatUnitSample(common: "41.4"),
                new OrbatUnitSample(common: "42.4"),
                new OrbatUnitSample(common: "43.4"),
            },


        };

    }
}
