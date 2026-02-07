# Milsymbol Markdown Examples

This document demonstrates the various ways to use military symbols in Markdown.

## Basic Usage

Simple symbol without options:

:ms[10031000131211050000]:

Multiple symbols in a sentence:
The friendly forces :ms[10031000131211050000]: engaged the enemy armor :ms[10061000131205000051]: at grid 123456.

## Size Options

Small (20px):
:ms[10031000131211050000, size=20]:

Default:
:ms[10031000131211050000]:

Medium (40px):
:ms[10031000131211050000, size=40]:

Large (60px):
:ms[10031000131211050000, size=60]:

Using short syntax (s for size):
:ms[10031000131211050000, s=60]:

## Text Labels

### Unique Designation (Bottom Left Text)

Unit identifier on top:
:ms[10031000131211050000, uniqueDesignation="A-1"]:

Using short syntax (ud):
:ms[10031000131211050000, ud="A-1"]:

### Additional Information (Center Right Text)

Status or additional info at bottom:
:ms[10031000131211050000, additionalInformation="Ready"]:

Using short syntax (ai):
:ms[10031000131211050000, ai="Ready"]:

### Higher Formation (Bottom Right Text)

Parent unit or formation:
:ms[10031000131211050000, higherFormation="2BDE"]:

Using short syntax (hf):
:ms[10031000131211050000, hf="2BDE"]:

### Common Identifier (Center Right Text)

Common identifier or sequence:
:ms[10031000131211050000, commonIdentifier="01"]:

Using short syntax (ci):
:ms[10031000131211050000, ci="01"]:

### Reinforced/Reduced Indicator

Show unit strength modifier:
:ms[10031000131211050000, reinforcedReduced="(+)"]:

Using short syntax (rr):
:ms[10031000131211050000, rr="(+)"]:

Reduced unit:
:ms[10031000131211050000, rr="(-)"]:

## Complete Unit Example

Full unit with all text labels (long form):
:ms[10031000131211050000, uniqueDesignation="A-1", additionalInformation="Combat Ready", higherFormation="2BDE", commonIdentifier="01", reinforcedReduced="(+)"]:

Full unit with all text labels (short form):
:ms[10031000131211050000, ud="A-1", ai="Combat Ready", hf="2BDE", ci="01", rr="(+)"]:

## Direction Arrows

Unit moving East (90°):
:ms[10031000131211050000, direction=90]:

Using short syntax (d):
:ms[10031000131211050000, d=90]:

Unit moving North-East (45°):
:ms[10031000131211050000, d=45]:

Unit moving North (0°):
:ms[10031000131211050000, d=0]:

Unit moving South (180°):
:ms[10031000131211050000, d=180]:

## Stroke and Outline Width

Default stroke:
:ms[10031000131211050000]:

Thicker stroke:
:ms[10031000131211050000, strokeWidth=6]:

Using short syntax (sw):
:ms[10031000131211050000, sw=6]:

With outline:
:ms[10031000131211050000, outlineWidth=4]:

Using short syntax (ow):
:ms[10031000131211050000, ow=4]:

Both customized:
:ms[10031000131211050000, strokeWidth=6, outlineWidth=4]:

Using short syntax:
:ms[10031000131211050000, sw=6, ow=4]:

## Practical Examples

### Order of Battle (ORBAT)

#### 2nd Brigade Combat Team

- **Brigade HQ**: :ms[10031000131211050000, ud="2BCT", hf="1DIV", s=40]:
  - **Alpha Company**: :ms[10031000131211050000, ud="A", hf="2BCT"]:
    - 1st Platoon: :ms[10031000131211050000, ud="A-1", hf="ALPHA"]:
    - 2nd Platoon: :ms[10031000131211050000, ud="A-2", hf="ALPHA"]:
    - 3rd Platoon: :ms[10031000131211050000, ud="A-3", hf="ALPHA"]:
  - **Bravo Company**: :ms[10031000131211050000, ud="B", hf="2BCT"]:
    - 1st Platoon: :ms[10031000131211050000, ud="B-1", hf="BRAVO"]:
    - 2nd Platoon: :ms[10031000131211050000, ud="B-2", hf="BRAVO"]:

### Situation Report (SITREP)

Current positions as of 1200Z:

| Unit | Status | Position | Symbol |
|------|--------|----------|--------|
| A-1 | Combat Ready | Grid 1234 | :ms[10031000131211050000, ud="A-1", ai="Ready"]: |
| A-2 | Moving North | Grid 1235 | :ms[10031000131211050000, ud="A-2", d=0]: |
| B-1 | Reinforced | Grid 1244 | :ms[10031000131211050000, ud="B-1", rr="(+)"]: |
| Enemy | Engaged | Grid 1300 | :ms[10061000161211000000, ud="HOSTILE", ai="Engaged"]: |

### Movement Orders

1. Alpha Company :ms[10031000131211050000, ud="A"]:
   - Current: Grid 1234
   - Move to: Grid 1300 :ms[10031000131211050000, ud="A", d=45]:
   - Status: Ready to move

2. Bravo Company :ms[10031000131211050000, ud="B"]:
   - Current: Grid 1244  
   - Move to: Grid 1310 :ms[10031000131211050000, ud="B", d=90]:
   - Status: Awaiting orders

## Different Symbol Types

### Ground Units

Infantry: :ms[10031000131211050000]:
Armor: :ms[10031000131205000051]:
Artillery: :ms[10031000131303000000]:
Aviation: :ms[10030100001101000000]:

### Hostile Forces

Enemy Infantry: :ms[10061000131211050000]:
Enemy Armor: :ms[10061000131205000051]:
Enemy Artillery: :ms[10061000131303000000]:

### Neutral and Unknown

Neutral: :ms[10041000000000000000]:
Unknown: :ms[10011000000000000000]:

## Short Option Names Reference

For more concise syntax, use these short names:

- `s` = `size`
- `sw` = `strokeWidth`
- `ow` = `outlineWidth`
- `ud` = `uniqueDesignation`
- `ai` = `additionalInformation`
- `hf` = `higherFormation`
- `ci` = `commonIdentifier`
- `rr` = `reinforcedReduced`
- `d` = `direction`

## Notes

- All text values with spaces must be in double quotes: `ud="A Company"`
- Multiple options are separated by commas: `:ms[SIDC, size=40, ud="A-1"]:`
- Direction is in degrees (0=North, 90=East, 180=South, 270=West)
- Options can use either long names or short names

