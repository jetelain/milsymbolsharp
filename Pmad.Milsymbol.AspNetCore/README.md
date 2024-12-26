# Milsymbol for ASP.NET Core

## Setup

In `Program.cs`, on the ServiceCollection, add the Milsymbol components.
```csharp
builder.Services.AddMilsymbolMvcComponents();
```

In `Program.cs`, on the ApplicationBuilder, add the Milsymbol static files middleware.
```csharp
app.UseMilsymbolStaticFiles();
```

In `_ViewImports.cshtml`, add the Milsymbol tag helpers.
```
@addTagHelper *, Pmad.Milsymbol.AspNetCore
```

## ORBAT Component

Implement the `IOrbatUnit` interface, or use the provided class `OrbatUnitViewModel`, and in the model, add a property of type `IOrbarUnit`
```csharp
public required IOrbatUnit RootUnit { get; set; }
```

In the view (.cshtml file), add the ORBAT component.
```
<pmad-orbat root-unit="@Model.RootUnit" />
```

To set a tooltip, or a link on each unit symbol, you can :
- implement the interface `IOrbatUnitViewModel` on the model and provide `Href` and `Title`,
- or use the provided class `OrbatUnitTitleViewModel` and set `Href` and `Title`
- or set delegates on `pmad-orbat` tag
```
<pmad-orbat 
    root-unit="@Model.RootUnit"
    unit-title="@(u => ((YourOrbatUnit)u).Label)"
    unit-link-controller="Units" unit-link-action="Details" unit-link-route="@(u => new { id = ((YourOrbatUnit)u).UnitID })" />
```
