# Milsymbol for C#

Simple wrapper around [milsymbol](https://github.com/spatialillusions/milsymbol) with a Javascript interpreter to allow server usage.

Includes a SVG to PNG render library based on Skia. This allow to have a PNG result very close to milsymbol running on Chrome. 

Despite having an interpreted JavaScript backend, performance is honorable : 0.3 msec per SVG symbol (vs 0.01 msec in browser, x30 slower), 1.5 msec per PNG symbol (vs 0.6 msec in browser, x3 slower).

Long term goal is to rewrite everything in C#, but keeping parity with JS library.
