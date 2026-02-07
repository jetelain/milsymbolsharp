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

var markdown = "Friend Infantry: :ms[10031000131211050000]:";
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

var markdown = "Friend Infantry: :ms[10031000131211050000]:";
var html = Markdown.ToHtml(markdown, pipeline);
```

## Syntax

The extension adds an inline syntax for military symbols:

```
:ms[SIDC]:
```

Where `SIDC` is a valid Symbol Identification Code (20 or 30 digits).

### Options Syntax

You can specify rendering options as comma-separated key-value pairs within the brackets:

```
:ms[SIDC, key1=value1, key2=value2]:
```

### Available Options

| Option | Short | Type | Description | Example |
|--------|-------|------|-------------|---------|
| `size` | `s` | number | Size of the symbol | `size=50` or `s=50` |
| `strokeWidth` | `sw` | number | Width of symbol strokes | `strokeWidth=4` or `sw=4` |
| `outlineWidth` | `ow` | number | Width of symbol outline | `outlineWidth=2` or `ow=2` |
| `uniqueDesignation` | `ud` | string | Unit designation (top text) | `uniqueDesignation="A-1"` or `ud="A-1"` |
| `additionalInformation` | `ai` | string | Additional info (bottom text) | `additionalInformation="Ready"` or `ai="Ready"` |
| `higherFormation` | `hf` | string | Higher formation (top right) | `higherFormation="2BDE"` or `hf="2BDE"` |
| `commonIdentifier` | `ci` | string | Common identifier (bottom right) | `commonIdentifier="01"` or `ci="01"` |
| `reinforcedReduced` | `rr` | string | Reinforced/reduced indicator | `reinforcedReduced="(+)"` or `rr="(+)"` |
| `direction` | `d` | number | Direction arrow (degrees) | `direction=45` or `d=45` |

**Note:** String values with spaces or special characters should be enclosed in double quotes. Both long and short option names can be used interchangeably.

### Examples

**Basic symbols:**
```markdown
# Military Symbols Demo

This is a friend infantry unit: :ms[10031000131211050000]:

This is an enemy armor unit: :ms[10061000161211000000]:

You can use symbols inline in text, in lists:
- Friend: :ms[10031000131211050000]:
- Enemy: :ms[10061000161211000000]:

Or in tables:
| Unit | Symbol |
|------|--------|
| Infantry | :ms[10031000131211050000]: |
| Armor | :ms[10061000161211000000]: |
```

**Symbols with options (long form):**
```markdown
# Unit Roster

## Alpha Company

- **A-1 Platoon** (Ready): :ms[10031000131211050000, uniqueDesignation="A-1", additionalInformation="Ready", higherFormation="ALPHA"]:
- **A-2 Platoon** (Moving NE): :ms[10031000131211050000, uniqueDesignation="A-2", direction=45]:
- **Large symbol**: :ms[10031000131211050000, size=50, uniqueDesignation="HQ"]:

## Bravo Company

Enemy contact: :ms[10061000161211000000, uniqueDesignation="HOSTILE", additionalInformation="Engaged"]:
```

**Symbols with options (short form):**
```markdown
# Unit Roster

## Alpha Company

- **A-1 Platoon** (Ready): :ms[10031000131211050000, ud="A-1", ai="Ready", hf="ALPHA"]:
- **A-2 Platoon** (Moving NE): :ms[10031000131211050000, ud="A-2", d=45]:
- **Large symbol**: :ms[10031000131211050000, s=50, ud="HQ"]:

## Bravo Company

Enemy contact: :ms[10061000161211000000, ud="HOSTILE", ai="Engaged"]:
```

**Custom sizing:**
```markdown
Small: :ms[10031000131211050000, size=20]:
Medium: :ms[10031000131211050000, size=30]:
Large: :ms[10031000131211050000, size=50]:

Or using short form:
Small: :ms[10031000131211050000, s=20]:
Medium: :ms[10031000131211050000, s=30]:
Large: :ms[10031000131211050000, s=50]:
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
