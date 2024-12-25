using Pmad.Milsymbol.AspNetCore.Orbat;

namespace MvcDemo.Models
{
    public class HomeViewModel
    {

        public IOrbatUnit RootUnit { get; } = new OrbatUnitSample(common: "1er FORBAN DE MARINE -")
        {
            new OrbatUnitSample(common: "1ERE SECTION - COMBAT ET MANŒUVRE")
            {
                new OrbatUnitSample(common: "11.1.Tag-"),
                new OrbatUnitSample(common: "12.1.Tag-"),
                new OrbatUnitSample(common: "13.1.Tag-"),
            },
            new OrbatUnitSample(common: "2EME SECTION - APPUIS")
            {
                new OrbatUnitSample(common: "21.2.Tag-"),
                new OrbatUnitSample(common: "22.2.Tag-"),
                new OrbatUnitSample(common: "23.2.Tag-"),
            },
            new OrbatUnitSample(common: "3EME PELOTON - CAVALERIE BLINDEE")
            {
                new OrbatUnitSample(common: "31.3.Tag-"),
                new OrbatUnitSample(common: "32.3.Tag-"),
                new OrbatUnitSample(common: "33.3.Tag-"),
            },
            new OrbatUnitSample(common: "4EME SECTION - AIDE A L'ENGAGEMENT DEBARQUE")
            {
                new OrbatUnitSample(common: "41.4.Tag-"),
                new OrbatUnitSample(common: "42.4.Tag-"),
                new OrbatUnitSample(common: "43.4.Tag-"),
            },


        };

    }
}
