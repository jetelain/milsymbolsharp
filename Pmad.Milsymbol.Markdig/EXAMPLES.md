*# Milsymbol Markdown Examples

This document demonstrates the various ways to use military symbols in Markdown.

## Basic Usage

Simple symbol without options:

:milsymbol[10031000131211050000]:

Multiple symbols in a sentence:
The friendly forces :milsymbol[10031000131211050000]: engaged the enemy armor :milsymbol[10061000131205000051]: at grid 123456.

## Size Options

Small (20px):
:milsymbol[10031000131211050000]{size=20}:

Default:
:milsymbol[10031000131211050000]:

Medium (40px):
:milsymbol[10031000131211050000]{size=40}:

Large (60px):
:milsymbol[10031000131211050000]{size=60}:

## Text Labels

### Unique Designation (Bottom Left Text)

Unit identifier on top:
:milsymbol[10031000131211050000]{uniqueDesignation="A-1"}:

### Additional Information (Center Right Text)

Status or additional info at bottom:
:milsymbol[10031000131211050000]{additionalInformation="Ready"}:

### Higher Formation (Bottom Right Text)

Parent unit or formation:
:milsymbol[10031000131211050000]{higherFormation="2BDE"}:

### Common Identifier (Center Right Text)

Common identifier or sequence:
:milsymbol[10031000131211050000]{commonIdentifier="01"}:

### Reinforced/Reduced Indicator

Show unit strength modifier:
:milsymbol[10031000131211050000]{reinforcedReduced="(+)"}:

Reduced unit:
:milsymbol[10031000131211050000]{reinforcedReduced="(-)"}:

## Complete Unit Example

Full unit with all text labels:
:milsymbol[10031000131211050000]{uniqueDesignation="A-1", additionalInformation="Combat Ready", higherFormation="2BDE", commonIdentifier="01", reinforcedReduced="(+)"}:

## Direction Arrows

Unit moving East (90°):
:milsymbol[10031000131211050000]{direction=90}:

Unit moving North-East (45°):
:milsymbol[10031000131211050000]{direction=45}:

Unit moving North (0°):
:milsymbol[10031000131211050000]{direction=0}:

Unit moving South (180°):
:milsymbol[10031000131211050000]{direction=180}:

## Stroke and Outline Width

Default stroke:
:milsymbol[10031000131211050000]:

Thicker stroke:
:milsymbol[10031000131211050000]{strokeWidth=6}:

With outline:
:milsymbol[10031000131211050000]{outlineWidth=4}:

Both customized:
:milsymbol[10031000131211050000]{strokeWidth=6, outlineWidth=4}:

## Practical Examples

### Order of Battle (ORBAT)

#### 2nd Brigade Combat Team

- **Brigade HQ**: :milsymbol[10031000131211050000]{uniqueDesignation="2BCT", higherFormation="1DIV", size=40}:
  - **Alpha Company**: :milsymbol[10031000131211050000]{uniqueDesignation="A", higherFormation="2BCT"}:
    - 1st Platoon: :milsymbol[10031000131211050000]{uniqueDesignation="A-1", higherFormation="ALPHA"}:
    - 2nd Platoon: :milsymbol[10031000131211050000]{uniqueDesignation="A-2", higherFormation="ALPHA"}:
    - 3rd Platoon: :milsymbol[10031000131211050000]{uniqueDesignation="A-3", higherFormation="ALPHA"}:
  - **Bravo Company**: :milsymbol[10031000131211050000]{uniqueDesignation="B", higherFormation="2BCT"}:
    - 1st Platoon: :milsymbol[10031000131211050000]{uniqueDesignation="B-1", higherFormation="BRAVO"}:
    - 2nd Platoon: :milsymbol[10031000131211050000]{uniqueDesignation="B-2", higherFormation="BRAVO"}:

### Situation Report (SITREP)

Current positions as of 1200Z:

| Unit | Status | Position | Symbol |
|------|--------|----------|--------|
| A-1 | Combat Ready | Grid 1234 | :milsymbol[10031000131211050000]{uniqueDesignation="A-1", additionalInformation="Ready"}: |
| A-2 | Moving North | Grid 1235 | :milsymbol[10031000131211050000]{uniqueDesignation="A-2", direction=0}: |
| B-1 | Reinforced | Grid 1244 | :milsymbol[10031000131211050000]{uniqueDesignation="B-1", reinforcedReduced="(+)"}: |
| Enemy | Engaged | Grid 1300 | :milsymbol[10061000161211000000]{uniqueDesignation="HOSTILE", additionalInformation="Engaged"}: |

### Movement Orders

1. Alpha Company :milsymbol[10031000131211050000]{uniqueDesignation="A"}:
   - Current: Grid 1234
   - Move to: Grid 1300 :milsymbol[10031000131211050000]{uniqueDesignation="A", direction=45}:
   - Status: Ready to move

2. Bravo Company :milsymbol[10031000131211050000]{uniqueDesignation="B"}:
   - Current: Grid 1244  
   - Move to: Grid 1310 :milsymbol[10031000131211050000]{uniqueDesignation="B", direction=90}:
   - Status: Awaiting orders

## Different Symbol Types

### Ground Units

Infantry: :milsymbol[10031000131211050000]:
Armor: :milsymbol[10031000131205000051]:
Artillery: :milsymbol[10031000131303000000]:
Aviation: :milsymbol[10030100001101000000]:

### Hostile Forces

Enemy Infantry: :milsymbol[10061000131211050000]:
Enemy Armor: :milsymbol[10061000131205000051]:
Enemy Artillery: :milsymbol[10061000131303000000]:

### Neutral and Unknown

Neutral: :milsymbol[10041000000000000000]:
Unknown: :milsymbol[10011000000000000000]:

## Notes

- All text values with spaces must be in double quotes: `uniqueDesignation="A Company"`
- Multiple options are separated by commas: `{size=40, uniqueDesignation="A-1"}`
- Direction is in degrees (0=North, 90=East, 180=South, 270=West)
- Empty options block `{}` is valid and renders with defaults
