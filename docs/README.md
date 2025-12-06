# MbSoftLab.TemplateEngine.Core - Dokumentation

**Version:** 1.0.8-preview2  
**Commit-Referenz:** 5c37e68  
**Dokumentations-Stand:** Dezember 2025

---

## Willkommen

Willkommen zur offiziellen Dokumentation der **MbSoftLab.TemplateEngine.Core** Bibliothek - einer leistungsstarken und flexiblen Template-Engine für .NET 8.0.

Diese Bibliothek bietet zwei verschiedene Ansätze zur Template-Verarbeitung:
- **TemplateEngine<T>** - Schnell und einfach für String-basierte Templates
- **RazorTemplateEngine<T>** - Mächtig und flexibel für komplexe HTML-Templates mit Razor-Syntax

---

## 📚 Dokumentations-Struktur

### [Architektur](architecture.md)
Detaillierte technische Übersicht der Bibliothek:
- Gesamt-Architektur und Komponenten-Diagramme
- Klassen-Hierarchie und Beziehungen
- Datenfluss und Verarbeitungs-Pipelines
- Design-Entscheidungen und Begründungen
- Erweiterungspunkte für zukünftige Features

**Für wen:** Entwickler, die die interne Struktur verstehen wollen

### [API-Dokumentation](api.md)
Vollständige API-Referenz:
- Alle öffentlichen Klassen und Interfaces
- Methoden-Signaturen und Parameter
- Eigenschaften und deren Verwendung
- Template-Syntax für beide Engine-Typen
- Unterstützte Datentypen
- Best Practices und Fehlerbehandlung

**Für wen:** Entwickler, die die API verwenden

### [Beispiele und Tutorials](examples.md)
Praktische Codebeispiele:
- Schnellstart-Anleitung
- Einfache bis fortgeschrittene Beispiele
- Razor-Template-Beispiele
- Real-World-Szenarien (E-Mail, Reports, Konfigurationen)
- Tipps und Tricks für effiziente Nutzung

**Für wen:** Einsteiger und Entwickler, die konkrete Anwendungsfälle suchen

### [Entwickler-Leitfaden](development.md)
Informationen für Contributors:
- Entwicklungsumgebung einrichten
- Projekt-Struktur und Organisation
- Build, Test und Debugging
- Code-Konventionen und Standards
- Contribution Guidelines
- CI/CD Pipeline-Details

**Für wen:** Contributors und Maintainer

---

## 🚀 Schnellstart

### Installation

```bash
# NuGet Package Manager
Install-Package MbSoftLab.TemplateEngine.Core

# .NET CLI
dotnet add package MbSoftLab.TemplateEngine.Core
```

### Erstes Beispiel

```csharp
using MbSoftLab.TemplateEngine.Core;

// Datenmodell
var person = new { FirstName = "Max", LastName = "Mustermann" };

// Template
string template = "Hallo ${FirstName} ${LastName}!";

// Template-Engine
var engine = new TemplateEngine(person, template);

// String erstellen
string result = engine.CreateStringFromTemplate();
// Output: "Hallo Max Mustermann!"
```

Mehr Beispiele finden Sie in der [Beispiele-Dokumentation](examples.md).

---

## 📖 Wichtige Dokumente

### [CHANGELOG.md](/CHANGELOG.md)
Komplette Versions-Historie mit allen Änderungen, neuen Features und Bugfixes.

### [RELEASENOTES.md](/RELEASENOTES.md)
Detaillierte Release-Informationen für die aktuelle Version mit Migration-Guides und bekannten Einschränkungen.

### [README.md](/README.md)
Projekt-Übersicht mit grundlegenden Informationen und Links.

---

## 🎯 Häufige Anwendungsfälle

### 1. Einfache String-Templates
Für Platzhalter-Ersetzungen in Konfigurationen, E-Mails oder einfachen Texten.

```csharp
var engine = new TemplateEngine<Customer>(customer, "Hallo ${Name}!");
```

→ Siehe [Einfache Beispiele](/docs/examples/#einfache-beispiele)

### 2. Komplexe HTML-Templates
Für dynamische HTML-Generierung mit Schleifen, Bedingungen und verschachtelten Objekten.

```csharp
var engine = new RazorTemplateEngine<Person>();
engine.TemplateString = "@foreach(var item in Model.Items) { <li>@item</li> }";
```

→ Siehe [Razor-Templates](/docs/examples/#razor-templates)

### 3. Report-Generierung
Für automatisierte Berichte aus Datenmodellen.

→ Siehe [Praxis-Szenarien](/docs/examples/#praxis-szenarien)

### 4. Code-Generierung
Für Template-basierte Code- oder Konfigurations-Generierung.

→ Siehe [Entwickler-Leitfaden](/docs/development/)

---

## 💡 Kernkonzepte

### Template-Engine (String-basiert)
- **Platzhalter:** `${PropertyName}` oder `${MethodName()}`
- **Custom Delimiters:** Konfigurierbar
- **NULL-Behandlung:** Anpassbarer NULL-String
- **Formatierung:** Kultur-abhängig für Zahlen und Datum
- **Performance:** Sehr schnell für einfache Templates

### RazorTemplateEngine (Razor-basiert)
- **Razor-Syntax:** Volle C#-Unterstützung
- **Schleifen:** `@foreach`, `@for`
- **Bedingungen:** `@if`, `@switch`
- **Collections:** Listen und Arrays unterstützt
- **Kompilierung:** Templates werden kompiliert für bessere Performance

---

## 🔧 Funktions-Übersicht

### Unterstützte Datentypen (TemplateEngine)
✅ String, Byte, Short, Int, Long, Decimal, Double, DateTime, Boolean  
❌ Collections, Custom Classes (→ Nutzen Sie RazorTemplateEngine)

### Features
- [x] Property-basierte Platzhalter
- [x] Methoden-Aufrufe (parameterlos)
- [x] Custom Delimiters
- [x] NULL-Wert-Konfiguration
- [x] Kultur-spezifische Formatierung
- [x] JSON-Deserialisierung
- [x] Template aus Datei laden
- [x] Razor-Template-Unterstützung
- [x] Generische Type-Safety
- [x] Interface-basiertes Design
- [x] Konfigurations-Objekt

---

## 📊 Versions-Informationen

### Aktuelle Version: 1.0.8-preview2

**Highlights:**
- Razor-Template-Engine für komplexe HTML-Templates
- Erweiterte Methoden-Aufrufe in Templates
- Verbesserter Build- und Release-Prozess
- Umfassende Dokumentation

**Breaking Changes:** Keine (abwärtskompatibel)

Siehe [RELEASENOTES.md](/RELEASENOTES.md) für Details.

---

## 🤝 Contribution

Wir freuen uns über Beiträge! Bitte lesen Sie den [Entwickler-Leitfaden](/docs/development/#contribution-guidelines) für:
- Branch-Strategie
- Commit-Konventionen
- Pull Request Process
- Code-Standards

---

## 📝 Support und Feedback

### Issues melden
[GitHub Issues](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/issues)

### Fragen stellen
[GitHub Discussions](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/discussions)

### Code-Qualität prüfen
[![CodeFactor](https://www.codefactor.io/repository/github/mbsoftlab/mbsoftlab.templateengine.core/badge)](https://www.codefactor.io/repository/github/mbsoftlab/mbsoftlab.templateengine.core)

---

## 📦 Links

- **NuGet Package:** https://www.nuget.org/packages/MbSoftLab.TemplateEngine.Core/
- **GitHub Repository:** https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core
- **License:** MIT

---

## 🗺️ Dokumentations-Roadmap

### Geplante Erweiterungen
- [ ] Video-Tutorials
- [ ] Interaktive Code-Beispiele
- [ ] Performance-Benchmarks
- [ ] Migration-Guides von anderen Template-Engines
- [ ] FAQ-Sektion
- [ ] Troubleshooting-Guide

---

**Letzte Aktualisierung:** Dezember 2025  
**Commit-Referenz:** 5c37e68

---

*Diese Dokumentation wurde automatisch generiert und wird kontinuierlich aktualisiert.*
