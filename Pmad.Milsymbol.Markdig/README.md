# Pmad.Milsymbol.Markdig

A Markdig extension that allows rendering military symbols (SVG) directly in Markdown documents.

## Installation

```bash
dotnet add package Pmad.Milsymbol.Markdig
```

## Usage

### Basic Setup

```csharp
using Markdig;
using Pmad.Milsymbol.Markdig;

var pipeline = new MarkdownPipelineBuilder()
    .UseMilsymbol()
    .Build();

var markdown = "Friend Infantry: :milsymbol[10031000131211050000]:";
var html = Markdown.ToHtml(markdown, pipeline);
```

### Custom Symbol Generator

You can provide your own `SymbolIconGenerator` instance with custom settings:

```csharp
using Pmad.Milsymbol.Icons;
using Markdig;
using Pmad.Milsymbol.Markdig;

var generator = new SymbolIconGenerator(SymbolStandard.App6d);
var pipeline = new MarkdownPipelineBuilder()
    .UseMilsymbol(generator)
    .Build();

var markdown = "Friend Infantry: :milsymbol[10031000131211050000]:";
var html = Markdown.ToHtml(markdown, pipeline);
```

## Syntax

The extension adds an inline syntax for military symbols:

```
:milsymbol[SIDC]:
```

Where `SIDC` is a valid Symbol Identification Code (20 or 30 digits).

### Options Syntax

You can specify rendering options using a key-value syntax:

```
:milsymbol[SIDC]{key1=value1, key2=value2}:
```

### Available Options

| Option | Type | Description | Example |
|--------|------|-------------|---------|
| `size` | number | Size of the symbol | `size=50` |
| `strokeWidth` | number | Width of symbol strokes | `strokeWidth=4` |
| `outlineWidth` | number | Width of symbol outline | `outlineWidth=2` |
| `uniqueDesignation` | string | Unit designation (top text) | `uniqueDesignation="A-1"` |
| `additionalInformation` | string | Additional info (bottom text) | `additionalInformation="Ready"` |
| `higherFormation` | string | Higher formation (top right) | `higherFormation="2BDE"` |
| `commonIdentifier` | string | Common identifier (bottom right) | `commonIdentifier="01"` |
| `reinforcedReduced` | string | Reinforced/reduced indicator | `reinforcedReduced="(+)"` |
| `direction` | number | Direction arrow (degrees) | `direction=45` |

**Note:** String values with spaces or special characters should be enclosed in double quotes.

### Examples

**Basic symbols:**
```markdown
# Military Symbols Demo

This is a friend infantry unit: :milsymbol[10031000131211050000]:

This is an enemy armor unit: :milsymbol[10061000161211000000]:

You can use symbols inline in text, in lists:
- Friend: :milsymbol[10031000131211050000]:
- Enemy: :milsymbol[10061000161211000000]:

Or in tables:
| Unit | Symbol |
|------|--------|
| Infantry | :milsymbol[10031000131211050000]: |
| Armor | :milsymbol[10061000161211000000]: |
```

**Symbols with options:**
```markdown
# Unit Roster

## Alpha Company

- **A-1 Platoon** (Ready): :milsymbol[10031000131211050000]{uniqueDesignation="A-1", additionalInformation="Ready", higherFormation="ALPHA"}:
- **A-2 Platoon** (Moving NE): :milsymbol[10031000131211050000]{uniqueDesignation="A-2", direction=45}:
- **Large symbol**: :milsymbol[10031000131211050000]{size=50, uniqueDesignation="HQ"}:

## Bravo Company

Enemy contact: :milsymbol[10061000161211000000]{uniqueDesignation="HOSTILE", additionalInformation="Engaged"}:
```

**Custom sizing:**
```markdown
Small: :milsymbol[10031000131211050000]{size=20}:
Medium: :milsymbol[10031000131211050000]{size=30}:
Large: :milsymbol[10031000131211050000]{size=50}:
```

## Features

- Inline rendering of military symbols as SVG
- Support for all APP-6D symbol codes (20 or 30 digits)
- Full support for symbol rendering options (size, text labels, direction, etc.)
- Works with standard Markdig pipeline
- Thread-safe symbol generation
- Invalid SIDC codes are left as-is in the output
- Quoted string support for text options with spaces or special characters

## SIDC Format

The SIDC (Symbol Identification Code) must be:
- 20 or 30 digits long
- All numeric characters
- A valid APP-6D symbol code

For more information about SIDC codes, see the [main Pmad.Milsymbol documentation](../README.md).
