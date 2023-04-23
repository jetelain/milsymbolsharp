# Milsymbol for C#

Simple wrapper around [milsymbol](https://github.com/spatialillusions/milsymbol) with a Javascript interpreter to allow server usage.

Includes a SVG to PNG render library based on Skia. This allow to have a PNG result very close to milsymbol running on Chrome. 

Despite having an interpreted JavaScript backend, performance is quite good : 0.3 msec per SVG only symbol, 1.5 msec per PNG symbol.
