# Milsymbol for ASP.NET Core

## Setup

In `Program.cs`, on the ServiceCollection, add the Milsymbol components.
```csharp
builder.Services.AddMilsymbolComponents();
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

Implement the `IOrbatUnit` interface, and in the model, add a property of type `IOrbarUnit`
```csharp
public required IOrbatUnit RootUnit { get; set; }
```

In the view (.cshtml file), add the ORBAT component.
```
<pmad-orbat root-unit="@Model.RootUnit" />
```
