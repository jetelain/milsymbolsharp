# Quick Reference

## Basic Syntax

```
:milsymbol[SIDC]:
```

## With Options

```
:milsymbol[SIDC]{option1=value1, option2=value2}:
```

## Common Options Examples

### Text Labels
```markdown
:milsymbol[10031000131211050000]{uniqueDesignation="A-1"}:
:milsymbol[10031000131211050000]{additionalInformation="Ready"}:
:milsymbol[10031000131211050000]{higherFormation="2BDE"}:
:milsymbol[10031000131211050000]{commonIdentifier="01"}:
:milsymbol[10031000131211050000]{reinforcedReduced="(+)"}:
```

### Size
```markdown
:milsymbol[10031000131211050000]{size=30}:
:milsymbol[10031000131211050000]{size=50}:
```

### Direction
```markdown
:milsymbol[10031000131211050000]{direction=0}:    <!-- North -->
:milsymbol[10031000131211050000]{direction=45}:   <!-- NE -->
:milsymbol[10031000131211050000]{direction=90}:   <!-- East -->
:milsymbol[10031000131211050000]{direction=180}:  <!-- South -->
```

### Combined
```markdown
:milsymbol[10031000131211050000]{uniqueDesignation="A-1", higherFormation="ALPHA", size=40}:
:milsymbol[10031000131211050000]{uniqueDesignation="B-2", direction=45, additionalInformation="Moving"}:
```

## Common SIDC Examples

| Description | SIDC | Markdown |
|-------------|------|----------|
| Friend Infantry | `10031000131211050000` | `:milsymbol[10031000131211050000]:` |
| Friend Armor | `10031000161211000000` | `:milsymbol[10031000161211000000]:` |
| Enemy Infantry | `10061000131211050000` | `:milsymbol[10061000131211050000]:` |
| Enemy Armor | `10061000161211000000` | `:milsymbol[10061000161211000000]:` |
| Neutral | `10041000131211050000` | `:milsymbol[10041000131211050000]:` |
| Unknown | `10011000131211050000` | `:milsymbol[10011000131211050000]:` |

## Tips

- Use double quotes for strings with spaces: `uniqueDesignation="Alpha Company"`
- Separate multiple options with commas
- Numeric values don't need quotes: `size=40`
- Direction is in degrees: 0=North, 90=East, 180=South, 270=West
- Empty options `{}` is valid
- Invalid SIDC or syntax will be left as plain text
