# Quick Reference

## Basic Syntax

```
:ms[SIDC]:
```

## With Options

```
:ms[SIDC, option1=value1, option2=value2]:
```

## Short Option Names

| Long Name | Short | Description |
|-----------|-------|-------------|
| `size` | `s` | Symbol size |
| `strokeWidth` | `sw` | Stroke width |
| `outlineWidth` | `ow` | Outline width |
| `uniqueDesignation` | `ud` | Unit designation |
| `additionalInformation` | `ai` | Additional info |
| `higherFormation` | `hf` | Higher formation |
| `commonIdentifier` | `ci` | Common identifier |
| `reinforcedReduced` | `rr` | Reinforced/reduced |
| `direction` | `d` | Direction arrow |

## Common Options Examples

### Text Labels (Long Form)
```markdown
:ms[10031000131211050000, uniqueDesignation="A-1"]:
:ms[10031000131211050000, additionalInformation="Ready"]:
:ms[10031000131211050000, higherFormation="2BDE"]:
:ms[10031000131211050000, commonIdentifier="01"]:
:ms[10031000131211050000, reinforcedReduced="(+)"]:
```

### Text Labels (Short Form)
```markdown
:ms[10031000131211050000, ud="A-1"]:
:ms[10031000131211050000, ai="Ready"]:
:ms[10031000131211050000, hf="2BDE"]:
:ms[10031000131211050000, ci="01"]:
:ms[10031000131211050000, rr="(+)"]:
```

### Size
```markdown
:ms[10031000131211050000, size=30]:
:ms[10031000131211050000, size=50]:
:ms[10031000131211050000, s=30]:
:ms[10031000131211050000, s=50]:
```

### Direction
```markdown
:ms[10031000131211050000, direction=0]:    <!-- North -->
:ms[10031000131211050000, direction=45]:   <!-- NE -->
:ms[10031000131211050000, direction=90]:   <!-- East -->
:ms[10031000131211050000, direction=180]:  <!-- South -->

<!-- Or using short form: -->
:ms[10031000131211050000, d=0]:    <!-- North -->
:ms[10031000131211050000, d=45]:   <!-- NE -->
:ms[10031000131211050000, d=90]:   <!-- East -->
:ms[10031000131211050000, d=180]:  <!-- South -->
```

### Combined (Long Form)
```markdown
:ms[10031000131211050000, uniqueDesignation="A-1", higherFormation="ALPHA", size=40]:
:ms[10031000131211050000, uniqueDesignation="B-2", direction=45, additionalInformation="Moving"]:
```

### Combined (Short Form)
```markdown
:ms[10031000131211050000, ud="A-1", hf="ALPHA", s=40]:
:ms[10031000131211050000, ud="B-2", d=45, ai="Moving"]:
```

## Common SIDC Examples

| Description | SIDC | Markdown |
|-------------|------|----------|
| Friend Infantry | `10031000131211050000` | `:ms[10031000131211050000]:` |
| Friend Armor | `10031000131205000051` | `:ms[10031000131205000051]:` |
| Enemy Infantry | `10061000131211050000` | `:ms[10061000131211050000]:` |
| Enemy Armor | `10061000131205000051` | `:ms[10061000131205000051]:` |
| Neutral | `10041000000000000000` | `:ms[10041000000000000000]:` |
| Unknown | `10011000000000000000` | `:ms[10011000000000000000]:` |

Tools (based on Pmad.Milsymbol.AspNetCore) : 
- Symbol creator : https://maps.plan-ops.fr/Symbols
- All supported SIDC : https://maps.plan-ops.fr/Symbols/All

## Tips

- Use double quotes for strings with spaces: `uniqueDesignation="Alpha Company"` or `ud="Alpha Company"`
- Separate multiple options with commas: `:ms[SIDC, size=40, ud="A-1"]:`
- Numeric values don't need quotes: `size=40` or `s=40`
- Direction is in degrees: 0=North, 90=East, 180=South, 270=West
- Use either long names or short names for options
- Invalid SIDC or syntax will be left as plain text
