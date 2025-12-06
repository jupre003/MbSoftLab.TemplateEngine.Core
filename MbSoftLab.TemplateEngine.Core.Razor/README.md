# MbSoftLab.TemplateEngine.Core.Razor

Optionales Erweiterungspaket für MbSoftLab.TemplateEngine.Core, das die `RazorTemplateEngine<T>` bereitstellt.

## Installation

```bash
# .NET CLI
dotnet add package MbSoftLab.TemplateEngine.Core.Razor

# Oder Projekt-Referenz (Monorepo)
dotnet add <IhrProjekt>.csproj reference MbSoftLab.TemplateEngine.Core.Razor/MbSoftLab.TemplateEngine.Core.Razor.csproj
```

## Verwendung

```csharp
using MbSoftLab.TemplateEngine.Core;

public class Person : TemplateDataModel<Person>
{
    public string FirstName { get; set; }
}

var person = new Person { FirstName = "Anna" };

var engine = new RazorTemplateEngine<Person>();
engine.TemplateString = "<h1>@Model.FirstName</h1>";
string html = engine.CreateStringFromTemplate(person);
```

Hinweis: `TemplateDataModel<T>` ist nun eine einfache Klasse im Core (ohne Razor-Basis). Die Razor-Engine erhält intern `Model` als Datenmodell (`template.Run(TemplateDataModel?.Model)`).
