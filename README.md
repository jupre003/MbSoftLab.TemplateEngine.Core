# MbSoftLab.TemplateEngine.Core

[![Build (develop)](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/workflows/BuildFromDevelop/badge.svg?branch=develop)](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/actions)
[![Build (master)](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/workflows/BuildFromMaster/badge.svg?branch=master)](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/actions)
[![Release](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/workflows/Release/badge.svg)](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/actions)
[![CodeFactor](https://www.codefactor.io/repository/github/mbsoftlab/mbsoftlab.templateengine.core/badge)](https://www.codefactor.io/repository/github/mbsoftlab/mbsoftlab.templateengine.core)
[![NuGet](https://img.shields.io/nuget/v/MbSoftLab.TemplateEngine.Core.svg)](https://www.nuget.org/packages/MbSoftLab.TemplateEngine.Core/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> Eine leistungsstarke und flexible Template-Engine für .NET 8.0 mit Unterstützung für einfache String-Templates und komplexe Razor-Templates.

---

## 🚀 Schnellstart

### Installation

```bash
dotnet add package MbSoftLab.TemplateEngine.Core
```

### Einfaches Beispiel

```csharp
using MbSoftLab.TemplateEngine.Core;

var person = new { FirstName = "Max", LastName = "Mustermann" };
var engine = new TemplateEngine(person, "Hallo ${FirstName} ${LastName}!");
string result = engine.CreateStringFromTemplate();
// Output: "Hallo Max Mustermann!"
```

### Razor-Template-Beispiel

```csharp
public class Person : TemplateDataModel<Person>
{
    public string FirstName { get; set; }
    public List<string> Tags { get; set; }
}

var person = new Person { 
    FirstName = "Anna", 
    Tags = new List<string> { "Developer", "Designer" } 
};

var engine = new RazorTemplateEngine<Person>();
engine.TemplateString = @"
<h1>@Model.FirstName</h1>
<ul>
@foreach(var tag in Model.Tags) {
    <li>@tag</li>
}
</ul>";

string html = engine.CreateStringFromTemplate(person);
```

---

## ✨ Features

### Zwei leistungsstarke Engines

- **TemplateEngine<T>** - Schnell und einfach für String-basierte Templates
  - Property-Platzhalter: `${PropertyName}`
  - Methoden-Aufrufe: `${MethodName()}`
  - Anpassbare Delimiters
  - Kultur-spezifische Formatierung
  
- **RazorTemplateEngine<T>** - Flexibel für komplexe HTML-Templates
  - Volle Razor-Syntax
  - Listen und Collections
  - Bedingungen und Schleifen
  - Verschachtelte Objekte

### Unterstützte Datentypen

✅ String, Byte, Short, Int, Long, Decimal, Double, DateTime, Boolean  
❌ Collections (nur mit RazorTemplateEngine)

---

## 📚 Dokumentation

**Vollständige Dokumentation verfügbar unter [`/docs`](/docs):**

| Dokument | Beschreibung |
|----------|--------------|
| [📖 Übersicht](/docs/README.md) | Dokumentations-Einstieg |
| [🏗️ Architektur](/docs/architecture.md) | System-Design und Komponenten |
| [📋 API-Referenz](/docs/api.md) | Vollständige API-Dokumentation |
| [💡 Beispiele](/docs/examples.md) | 16+ praktische Code-Beispiele |
| [👨‍💻 Entwickler-Leitfaden](/docs/development.md) | Contribution Guidelines |
| [📝 CHANGELOG](/CHANGELOG.md) | Versions-Historie |
| [🎉 Release Notes](/RELEASENOTES.md) | Aktuelle Version 1.0.8-preview2 |

---

## 💡 Verwendungsbeispiele

### Template aus Datei laden

```csharp
var engine = new TemplateEngine<Customer>(customer);
engine.LoadTemplateFromFile("email-template.txt");
string email = engine.CreateStringFromTemplate();
```

### JSON-Daten verwenden

```csharp
string jsonData = "{\"Name\":\"Lisa\",\"Email\":\"lisa@example.com\"}";
var engine = new TemplateEngine<Customer>();
engine.TemplateString = "Kunde: ${Name}, E-Mail: ${Email}";
string result = engine.CreateStringFromTemplateWithJson(jsonData);
```

### Custom Delimiters

```csharp
var engine = new TemplateEngine<Person>(person, "[[FirstName]] [[LastName]]");
engine.OpeningDelimiter = "[[";
engine.CloseingDelimiter = "]]";
```

### NULL-Werte behandeln

```csharp
var engine = new TemplateEngine<Customer>(customer, "${Email}");
engine.NullStringValue = "Keine Angabe";
```

Weitere Beispiele und Tutorials finden Sie in der [Beispiele-Dokumentation](/docs/examples.md).

---

## 🔧 Hauptfunktionen

### TemplateEngine

| Feature | Beschreibung |
|---------|--------------|
| Property-Binding | `${PropertyName}` für einfache Werte |
| Methoden-Aufrufe | `${MethodName()}` für parameterlose Methoden |
| Custom Delimiters | Anpassbare Start-/End-Zeichen |
| NULL-Behandlung | Konfigurierbarer NULL-String |
| Formatierung | Kultur-spezifisch (CultureInfo) |
| JSON-Support | Direkte Deserialisierung |
| File-Loading | Templates aus Dateien laden |

### RazorTemplateEngine

| Feature | Beschreibung |
|---------|--------------|
| Razor-Syntax | Volle C#-Unterstützung in Templates |
| Collections | Listen, Arrays, IEnumerable |
| Kontrollstrukturen | `@if`, `@foreach`, `@for`, `@switch` |
| Verschachtelung | Komplexe Objekthierarchien |
| Type-Safety | Generische Typisierung |

---

## 📦 NuGet Package

```bash
# .NET CLI
dotnet add package MbSoftLab.TemplateEngine.Core

# Package Manager
Install-Package MbSoftLab.TemplateEngine.Core

# PackageReference
<PackageReference Include="MbSoftLab.TemplateEngine.Core" Version="1.0.8-preview2" />
```

**NuGet Gallery:** https://www.nuget.org/packages/MbSoftLab.TemplateEngine.Core/

---

## 🤝 Contributing

Wir freuen uns über Beiträge! Bitte lesen Sie unseren [Entwickler-Leitfaden](/docs/development.md) für:

- Entwicklungsumgebung einrichten
- Code-Konventionen
- Branch-Strategie
- Pull Request Prozess

---

## 📄 Lizenz

Dieses Projekt ist unter der [MIT-Lizenz](LICENSE) lizenziert.

Copyright © 2021 MbSoftLab

---

## 🔗 Links

- **GitHub Repository:** https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core
- **Issues/Feedback:** https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/issues
- **NuGet Package:** https://www.nuget.org/packages/MbSoftLab.TemplateEngine.Core/

---

## 🆕 Version 1.0.8-preview2

**Highlights:**
- ✨ RazorTemplateEngine für komplexe HTML-Templates
- ✨ Erweiterte Methoden-Aufrufe in Templates
- 🔧 Verbesserter Build- und Release-Prozess
- 📚 Umfassende deutsche Dokumentation

Siehe [Release Notes](/RELEASENOTES.md) für Details.

---

<p align="center">
  <sub>Built with ❤️ by MbSoftLab</sub>
</p>
