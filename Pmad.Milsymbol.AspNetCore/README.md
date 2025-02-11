# Milsymbol for ASP.NET Core

## Setup

In `Program.cs`, on the Mvc Builder, add the Milsymbol components.
```csharp
builder.Services
    .AddControllersWithViews()
        .AddMilsymbolMvcComponents();
```

In `Program.cs`, on the ApplicationBuilder, add the Milsymbol static files middleware.
```csharp
app.UseMilsymbolStaticFiles();
```

In `_ViewImports.cshtml`, add the Milsymbol tag helpers.
```
@addTagHelper *, Pmad.Milsymbol.AspNetCore
```

## ORBAT Component `<pmad-orbat />`

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

## Symbol Component `<pmad-symbol-selector />`

APP-6D symbol selector component.

This component requires Boostrap 4 or 5. Version of bootstrap is automatically detected. Anyway, you can force the version by setting the `DesignSystem` in `AddMilsymbolMvcComponents`.
```csharp
builder.Services
    .AddControllersWithViews()
        .AddMilsymbolMvcComponents(DesignSystem.Boostrap5); // DesignSystem.Automatic by default
```

Basic usage with Symbol as string property in the model:
```
<pmad-symbol-selector asp-for="Symbol" />
```

Component has differents layouts:
- `layout="Default"` (default): Only dropdown lists
- `layout="Extended"`: Standard identity and Dummy/HQ/TF are selected with buttons, other categories are selected with dropdown lists

Please note that this component will automaticly include choices.js and milsymbol.js.