using Pmad.Milsymbol.App6d;

namespace Pmad.Milsymbol.Test.App6d
{
    public class App6dSymbolIdTest
    {

        [Fact]
        public void Parse()
        {
            var symbol = new App6dSymbolId("10063000001202040000");
            Assert.Equal("10", symbol.Version);
            Assert.Equal(App6dContext.Reality, symbol.Context);
            Assert.Equal(App6dStandardIdentity.Hostile, symbol.StandardIdentity);
            Assert.Equal("30", symbol.SymbolSet);
            Assert.Equal(App6dStatus.Present, symbol.Status);
            Assert.Equal(App6dHqTfFd.None, symbol.HqTfFd);
            Assert.False(symbol.IsFeintDummy);
            Assert.False(symbol.IsHeadquarters);
            Assert.False(symbol.IsTaskForce);
            Assert.Equal("00", symbol.Amplifier);
            Assert.Equal("120204", symbol.Icon);
            Assert.Equal("00", symbol.Modifier1);
            Assert.Equal("00", symbol.Modifier2);
        }
    }
}
