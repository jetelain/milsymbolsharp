﻿using Milsymbol.Icons;

namespace Milsymbol.Test.Icons
{
    public class SymbolIconGeneratorTest
    {
        [Fact]
        public void SymbolGenerator_Generate()
        {
            using var generator = new SymbolIconGenerator();
            var symbol = generator.Generate("30031000131211050000", new SymbolIconOptions());
            Assert.Equal(158, symbol.Width);
            Assert.Equal(135.5, symbol.Height);
            Assert.Equal(81.5, symbol.AnchorY);
            Assert.Equal(79, symbol.AnchorX);
            Assert.Equal("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.2\" baseProfile=\"tiny\" width=\"158\" height=\"135.5\" viewBox=\"21 18.5 158 135.5\"><path d=\"M25,50 l150,0 0,100 -150,0 z\" stroke-width=\"4\" stroke=\"black\" fill=\"rgb(128,224,255)\" fill-opacity=\"1\" ></path><path d=\"M25,50 L175,150 M25,150 L175,50\" stroke-width=\"4\" stroke=\"black\" fill=\"black\" ></path><path d=\"M125,80 C150,80 150,120 125,120 L75,120 C50,120 50,80 75,80 Z\" stroke-width=\"4\" stroke=\"black\" fill=\"none\" ></path><path d=\"M55,50L55,150\" stroke-width=\"4\" stroke=\"black\" fill=\"black\" ></path><g transform=\"translate(0,0)\" stroke-width=\"4\" stroke=\"black\" fill=\"none\" ><circle cx=\"115\" cy=\"30\" r=\"7.5\" fill=\"black\" ></circle><circle cx=\"85\" cy=\"30\" r=\"7.5\" fill=\"black\" ></circle></g></svg>", symbol.Svg);
        }
    }
}
