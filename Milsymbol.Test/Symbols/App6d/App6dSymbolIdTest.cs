using Milsymbol.Symbols.App6d;

namespace Milsymbol.Test.Symbols.App6d
{
    public class App6dSymbolIdTest
    {

        [Fact]
        public void Parse()
        {
            var symbol = new App6dSymbolId("10063000001202040000");
            Assert.Equal("10", symbol.Version);
            Assert.Equal(App6dStandardIdentity1.Reality, symbol.StandardIdentity1);
            Assert.Equal(App6dStandardIdentity2.Hostile, symbol.StandardIdentity2);
            Assert.Equal("30", symbol.SymbolSet);
            Assert.Equal(App6dStatus.Present, symbol.Status);
            Assert.Equal(App6dDummyHqTaskForce.None, symbol.DummyHqTaskForce);
            Assert.False(symbol.IsDummy);
            Assert.False(symbol.IsHeadquarters);
            Assert.False(symbol.IsTaskForce);
            Assert.Equal("00", symbol.Size);
            Assert.Equal("120204", symbol.Icon);
            Assert.Equal("00", symbol.Modifier1);
            Assert.Equal("00", symbol.Modifier2);
        }
    }
}
