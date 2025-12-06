# Release Notes

## Version 1.0.8-preview2

**Release-Datum:** In Entwicklung  
**Commit-Referenz:** 5c37e68

### Übersicht

Diese Preview-Version erweitert die MbSoftLab.TemplateEngine.Core um Razor-Template-Unterstützung und verbessert den Build- und Veröffentlichungsprozess.

---

## ✨ Neue Features

### 1. Razor Template Engine
Die neue `RazorTemplateEngine<T>` Klasse ermöglicht die Nutzung von Razor-Syntax für komplexe HTML-Templates:

```csharp
Person person = new Person { FirstName = "Max", LastName = "Mustermann" };
var engine = new RazorTemplateEngine<Person>();
engine.LoadTemplateFromFile<Person>("template.cshtml");
string result = engine.CreateStringFromTemplate(person);
```

**Vorteile:**
- Volle Razor-Syntax-Unterstützung (Schleifen, Bedingungen, etc.)
- Typsichere Template-Erstellung
- Kompilierte Templates für bessere Performance

### 2. Erweiterte Template-Funktionalität
- **Methodenaufrufe:** Templates können jetzt parameterlose öffentliche Methoden aufrufen: `${MethodName()}`
- **JSON-Unterstützung:** Direkte Deserialisierung von JSON in TemplateDataModel
- **Template aus Datei laden:** `LoadTemplateFromFile<T>()` Erweiterungsmethode

### 3. Einheitliches Interface-Design
- `ITemplateEngine<T>` als gemeinsames Interface für beide Engine-Typen
- `ITemplateEngineConfig<T>` für konsistente Konfiguration
- Bessere Erweiterbarkeit und Testbarkeit

---

## 🔧 Verbesserungen

### Build und Deployment
- **Modernisierter Release-Workflow:** 
  - Umstellung von veralteter NuGet-Publish-Action auf native dotnet-Befehle
  - `dotnet pack` für Package-Erstellung
  - `dotnet nuget push` für NuGet-Veröffentlichung
  - Verbesserte Zuverlässigkeit und Wartbarkeit

### Code-Qualität
- XML-Dokumentation für alle öffentlichen APIs
- Einheitliche Fehlerbehandlung
- Kultur-spezifische Formatierung konfigurierbar

---

## 📦 Technische Spezifikationen

### Framework und Versionen
- **Target Framework:** .NET 8.0
- **Assembly Version:** 1.0.8.2
- **Package Version:** 1.0.8-preview2
- **Lizenz:** MIT

### Abhängigkeiten
- RazorEngineCore 2020.10.1

### Build-Konfiguration
- Automatische Package-Generierung beim Build
- XML-Dokumentationsdatei wird generiert
- NuGet-Package mit Logo und vollständigen Metadaten

---

## 🎯 Anwendungsfälle

### Simple String-Templates
Ideal für einfache Platzhalter-Ersetzungen in Konfigurationsdateien, E-Mails oder Berichten.

### Komplexe HTML-Templates
Mit der Razor-Engine können Sie komplexe HTML-Dokumente mit Schleifen, Bedingungen und verschachtelten Strukturen erstellen.

### Code-Generierung
Nutzen Sie die Template-Engines zur automatischen Code-, Konfigurations- oder Dokumentationsgenerierung.

---

## 📝 Migration von früheren Versionen

Die API ist abwärtskompatibel. Bestehender Code funktioniert weiterhin:

```csharp
// Alter Code funktioniert weiterhin
var engine = new TemplateEngine(dataModel, template);
string result = engine.CreateStringFromTemplate();
```

Neue Features können optional genutzt werden:

```csharp
// Neuer Code mit Razor
var razorEngine = new RazorTemplateEngine<MyModel>(dataModel, razorTemplate);
string result = razorEngine.CreateStringFromTemplate();
```

---

## 🐛 Bekannte Einschränkungen

1. **Collections nicht unterstützt** in der einfachen TemplateEngine
   - Workaround: Verwenden Sie RazorTemplateEngine für Listen und Arrays

2. **Nur parameterlose Methoden** werden unterstützt
   - Zukünftige Versionen könnten Methoden mit Parametern unterstützen

3. **XML-Dokumentations-Warnung** bei `LoadTemplateFromFile`
   - Wird in der nächsten Version behoben

---

## 🔗 Links und Ressourcen

- **NuGet Package:** https://www.nuget.org/packages/MbSoftLab.TemplateEngine.Core/
- **GitHub Repository:** https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core
- **Issues:** https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/issues
- **Dokumentation:** Siehe `/docs` Verzeichnis

---

## 🙏 Danksagung

Danke an alle Mitwirkenden und Nutzer für ihr Feedback und ihre Unterstützung bei der Weiterentwicklung dieses Projekts.

---

**Commit-Referenz für diese Release Notes:** 5c37e68
