# Changelog

Alle wichtigen Änderungen an diesem Projekt werden in dieser Datei dokumentiert.

Das Format basiert auf [Keep a Changelog](https://keepachangelog.com/de/1.0.0/),
und dieses Projekt folgt [Semantic Versioning](https://semver.org/lang/de/).

## [Unveröffentlicht]

### Geändert
- Veröffentlichungsprozess aktualisiert: NuGet-Publish-Aktion durch `dotnet pack` und `dotnet nuget push` ersetzt (Commit: 5c37e68)

## [1.0.8-preview2] - Stand: Commit 5c37e68

### Hinzugefügt
- RazorTemplateEngine für komplexe HTML-Templates mit Razor-Syntax
- Unterstützung für Razor-Templates mit der `RazorTemplateEngine<T>` Klasse
- `ITemplateEngine<T>` Interface für beide Template-Engine-Implementierungen
- `ITemplateEngineConfig<T>` Interface und `TemplateEngineConfig<T>` Klasse für Konfiguration
- Erweiterungsmethoden `CreateStringFromTemplateWithJson<T>` und `LoadTemplateFromFile<T>`
- Demo-Projekt mit Razor-Template-Beispielen
- Unterstützung für parameterlose öffentliche Methoden im TemplateDataModel (Syntax: `${MethodName()}`)

### Funktionen
- Einfacher String-basierter Template-Engine (`TemplateEngine` und `TemplateEngine<T>`)
- Razor-basierter Template-Engine (`RazorTemplateEngine<T>`)
- Anpassbare Delimiter (Standard: `${` und `}`)
- Konfigurierbare NULL-Wert-Behandlung (Standard: "NULL")
- Kultur-spezifische Formatierung für Datum und Zahlen (Standard: en-US)
- JSON-Deserialisierung für TemplateDataModel
- Laden von Templates aus Dateien

### Unterstützte Datentypen
- Primitive Typen: String, Byte, Short, UShort, Long, ULong, SByte, Char
- Numerische Typen: Int16, Int32, Int64, UInt16, UInt32, UInt64, Decimal, Double
- Weitere Typen: DateTime, Boolean

### Technische Details
- Target Framework: .NET 8.0
- Assembly Version: 1.0.8.2
- Package Version: 1.0.8-preview2
- Abhängigkeit: RazorEngineCore 2020.10.1

### Build und CI/CD
- GitHub Actions Workflows für Build (Develop und Master Branch)
- GitHub Actions Workflow für Release und NuGet-Veröffentlichung
- Automatische NuGet-Package-Generierung beim Build

---

## Versions-Historie (Zusammenfassung)

Die Version 1.0.8-preview2 ist die aktuelle Entwicklungsversion mit Razor-Template-Unterstützung.

**Commit-Referenz für diese Dokumentation:** 5c37e68 (Basis-Commit für Dokumentation)
