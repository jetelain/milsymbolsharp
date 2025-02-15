# Milsymbol for C#

[![NuGet](https://img.shields.io/nuget/v/Pmad.Milsymbol?logo=nuget)](https://www.nuget.org/packages/Pmad.Milsymbol/) [![NuGet](https://img.shields.io/nuget/v/Pmad.Milsymbol.Png?logo=nuget)](https://www.nuget.org/packages/Pmad.Milsymbol.Png/) 

## Symbol Icon rendering

Simple wrapper around [milsymbol](https://github.com/spatialillusions/milsymbol) with a Javascript interpreter to allow server usage.

Includes a SVG to PNG render library based on Skia. This allow to have a PNG result very close to milsymbol running on Chrome. 

Despite having an interpreted JavaScript backend, performance is honorable : 0.3 msec per SVG symbol (vs 0.01 msec in browser, x30 slower), 1.5 msec per PNG symbol (vs 0.6 msec in browser, x3 slower).

Long term goal is to rewrite everything in C#, but keep parity with JS library.

Png render requires the package Pmad.Milsymbol.Png (to avoid SkiaSharp dependency when only SVG render is required).

Usage:
```
using var generator = new SymbolIconGenerator();
var symbolPng = generator.Generate("30031000131211050000").ToPng();
```

## APP-6D database

`App6dSymbolDatabase.Default.SymbolSets` : Information about APP-6D coding.

`App6dSymbolIdBuilder` : Tool to generate an APP-6D Symbol identification coding.

`App6dSymbolId` : Tool to parse an APP-6D Symbol identification coding.