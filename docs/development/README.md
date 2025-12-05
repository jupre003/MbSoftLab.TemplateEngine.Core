# Entwickler-Leitfaden

**Commit-Referenz:** 5c37e68  
**Dokumentations-Stand:** Dezember 2025

---

## Inhaltsverzeichnis

1. [Entwicklungsumgebung einrichten](#entwicklungsumgebung-einrichten)
2. [Projekt-Struktur](#projekt-struktur)
3. [Build und Tests](#build-und-tests)
4. [Code-Konventionen](#code-konventionen)
5. [Contribution Guidelines](#contribution-guidelines)
6. [CI/CD Pipeline](#cicd-pipeline)
7. [Versionierung](#versionierung)
8. [Debugging-Tipps](#debugging-tipps)

---

## Entwicklungsumgebung einrichten

### Voraussetzungen

- **.NET 8.0 SDK** oder höher
- **IDE:** Visual Studio 2022, Visual Studio Code, oder JetBrains Rider
- **Git** für Versionskontrolle

### Installation

```bash
# Repository klonen
git clone https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core.git
cd MbSoftLab.TemplateEngine.Core

# Dependencies wiederherstellen
dotnet restore

# Projekt bauen
dotnet build

# Tests ausführen
dotnet test
```

### Empfohlene VS Code Extensions

- C# Dev Kit
- .NET Extension Pack
- NuGet Package Manager
- EditorConfig for VS Code

### Empfohlene Visual Studio Workloads

- .NET Desktop-Entwicklung
- ASP.NET- und Webentwicklung

---

## Projekt-Struktur

```
MbSoftLab.TemplateEngine.Core/
├── .github/
│   └── workflows/              # GitHub Actions CI/CD
│       ├── BuildFromDevelop.yml
│       ├── BuildFromMaster.yml
│       └── Release.yml
├── MbSoftLab.TemplateEngine.Core/          # Haupt-Bibliothek
│   ├── ITemplateEngine.cs                   # Interface für Template-Engines
│   ├── TemplateEngine.cs                    # Simple Template-Engine
│   ├── RazorTemplateEngine.cs               # Razor-basierte Engine
│   ├── TemplateDataModel.cs                 # Basis-Klasse für Razor-Models
│   ├── TemplateDataModelProcessor.cs        # Property/Methoden-Verarbeitung
│   ├── PlaceholderValueRaplacer.cs          # Platzhalter-Ersetzung
│   ├── ReplacementActionCollection.cs       # Typ-spezifische Actions
│   ├── ITemplateEngineConfig.cs             # Konfigurations-Interface
│   ├── TemplateEngineConfig.cs              # Konfigurations-Implementierung
│   ├── TemplateEngineExtensions.cs          # Erweiterungsmethoden
│   └── MbSoftLab.TemplateEngine.Core.csproj # Projekt-Datei
├── MbSoftLab.TemplateEngine.Core.Tests/    # Unit-Tests
│   ├── TemplateEngineUnitTest.cs
│   ├── RazorTemplateEngineUnitTest.cs
│   ├── TemplateDataModelDummy.cs            # Test-Fixtures
│   └── *.cs                                 # Weitere Test-Dateien
├── MbSoftlab.TemplateEngine.Core.Demo/     # Demo-Anwendung
│   ├── Program.cs
│   ├── Person.cs
│   ├── Address.cs
│   ├── Order.cs
│   └── TestModel.cshtml                     # Razor-Template-Beispiel
├── docs/                                    # Dokumentation
│   ├── architecture/
│   ├── api/
│   ├── examples/
│   └── development/
├── CHANGELOG.md                             # Versions-Historie
├── RELEASENOTES.md                          # Release-Informationen
├── README.md                                # Haupt-Dokumentation
└── MbSoftLab.TemplateEngine.Core.sln       # Solution-Datei
```

---

## Build und Tests

### Lokaler Build

```bash
# Debug-Build
dotnet build

# Release-Build
dotnet build --configuration Release

# NuGet-Package erstellen
dotnet pack --configuration Release
```

**Output:** Das NuGet-Package wird in `bin/Release/` erstellt.

### Tests ausführen

```bash
# Alle Tests
dotnet test

# Mit Verbose-Output
dotnet test --verbosity detailed

# Nur spezifische Tests
dotnet test --filter "FullyQualifiedName~TemplateEngineUnitTest"

# Code Coverage (optional)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Demo-Anwendung ausführen

```bash
cd MbSoftlab.TemplateEngine.Core.Demo
dotnet run
```

Die Demo-Anwendung zeigt ein Razor-Template-Beispiel und öffnet das Ergebnis im Browser.

---

## Code-Konventionen

### Namenskonventionen

- **Klassen:** PascalCase (`TemplateEngine`, `PlaceholderValueRaplacer`)
- **Interfaces:** PascalCase mit `I`-Prefix (`ITemplateEngine`)
- **Properties:** PascalCase (`OpeningDelimiter`)
- **Private Felder:** camelCase mit `_`-Prefix (`_outputString`)
- **Methoden:** PascalCase (`CreateStringFromTemplate`)
- **Parameter:** camelCase (`templateDataModel`)

### Code-Stil

```csharp
// ✅ Gut
public class TemplateEngine<T>
{
    private string _templateString;
    
    public string TemplateString 
    { 
        get => _templateString; 
        set => _templateString = value; 
    }
    
    public string CreateStringFromTemplate()
    {
        // Implementation
    }
}

// ❌ Vermeiden
public class templateEngine<t>
{
    public string templatestring;
    
    public string create_string() { }
}
```

### XML-Dokumentation

Alle öffentlichen APIs müssen XML-Dokumentation haben:

```csharp
/// <summary>
/// Ersetzt alle Properties von templateDataModel im stringTemplate.
/// Die Property-Namen müssen mit den Platzhaltern übereinstimmen.
/// </summary>
/// <param name="templateDataModel">Datenmodell mit Properties</param>
/// <param name="stringTemplate">Template mit Platzhaltern</param>
/// <returns>Verarbeiteter String mit ersetzten Platzhaltern</returns>
public string CreateStringFromTemplate(T templateDataModel, string stringTemplate)
{
    // Implementation
}
```

### EditorConfig

Das Projekt verwendet `.editorconfig` für konsistente Code-Formatierung:

```ini
[*.cs]
indent_style = space
indent_size = 4
end_of_line = crlf
charset = utf-8
trim_trailing_whitespace = true
insert_final_newline = true
```

---

## Contribution Guidelines

### Branch-Strategie

- **master:** Produktions-Code (stabil)
- **develop:** Entwicklungs-Branch (neueste Features)
- **feature/\*:** Feature-Branches (von develop abzweigen)
- **bugfix/\*:** Bugfix-Branches
- **hotfix/\*:** Dringende Fixes für master

### Workflow

1. **Fork erstellen** oder auf **develop** Branch wechseln
   ```bash
   git checkout develop
   git pull origin develop
   ```

2. **Feature-Branch erstellen**
   ```bash
   git checkout -b feature/mein-neues-feature
   ```

3. **Änderungen implementieren**
   - Code schreiben
   - Tests hinzufügen
   - Dokumentation aktualisieren

4. **Tests ausführen**
   ```bash
   dotnet test
   ```

5. **Commit**
   ```bash
   git add .
   git commit -m "feat: Beschreibung des Features"
   ```

6. **Push und Pull Request**
   ```bash
   git push origin feature/mein-neues-feature
   ```
   Dann Pull Request auf GitHub erstellen.

### Commit-Message-Konventionen

Folgen Sie [Conventional Commits](https://www.conventionalcommits.org/):

```
feat: Neue Funktion hinzufügen
fix: Bug beheben
docs: Dokumentation aktualisieren
style: Code-Formatierung (keine funktionale Änderung)
refactor: Code-Refactoring
test: Tests hinzufügen oder ändern
chore: Build-Prozess oder Tools ändern
```

**Beispiele:**
```
feat: RazorTemplateEngine für HTML-Templates hinzufügen
fix: NULL-Wert-Behandlung in PlaceholderValueRaplacer korrigieren
docs: API-Dokumentation für CreateStringFromTemplate erweitern
test: Unit-Tests für Custom Delimiters hinzufügen
```

---

## CI/CD Pipeline

### GitHub Actions Workflows

#### 1. BuildFromDevelop.yml

**Trigger:** Push oder Pull Request auf `develop` Branch

**Schritte:**
- Checkout Code
- Setup .NET 8.0
- Restore Dependencies
- Build (Debug)
- Run Tests

```yaml
name: BuildFromDevelop
on:
  push:
    branches: [ develop ]
  pull_request:
    branches: [ develop ]

jobs:
  build:
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@v4
    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: 8.0.x
    - name: Restore
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal
```

#### 2. BuildFromMaster.yml

**Trigger:** Push auf `master` Branch

**Schritte:** Identisch zu BuildFromDevelop, aber für master.

#### 3. Release.yml

**Trigger:** GitHub Release wird veröffentlicht

**Schritte:**
- Checkout Code
- Setup .NET 8.0
- Restore Dependencies
- Build (Release)
- Create NuGet Package
- Publish to NuGet.org

```yaml
name: Release
on:
  release:
    types: 
      - published

jobs:
  publish-nuget:
    runs-on: windows-latest
    steps:
    - uses: actions/checkout@v4
    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: 8.0.x
    - name: Restore
      run: dotnet restore
    - name: Build
      run: dotnet build --configuration Release --no-restore
    - name: Pack
      run: dotnet pack MbSoftLab.TemplateEngine.Core/MbSoftLab.TemplateEngine.Core.csproj --configuration Release --no-restore -o ./artifacts
    - name: Push to NuGet
      run: dotnet nuget push ./artifacts/*.nupkg --api-key ${{ secrets.NUGET_API_KEY }} --source https://api.nuget.org/v3/index.json --skip-duplicate
```

### Lokale Build-Validierung vor Push

```bash
# Vor jedem Push ausführen
dotnet restore
dotnet build --configuration Release
dotnet test
dotnet pack --configuration Release
```

---

## Versionierung

### Semantic Versioning

Projekt folgt [Semantic Versioning 2.0.0](https://semver.org/):

```
MAJOR.MINOR.PATCH[-PRERELEASE]

1.0.8-preview2
│ │ │  └─ Pre-Release-Identifier
│ │ └─── PATCH: Bugfixes
│ └───── MINOR: Neue Features (abwärtskompatibel)
└─────── MAJOR: Breaking Changes
```

### Versions-Einstellungen

In `MbSoftLab.TemplateEngine.Core.csproj`:

```xml
<PropertyGroup>
    <AssemblyVersion>1.0.8.2</AssemblyVersion>
    <FileVersion>1.0.8.2</FileVersion>
    <Version>1.0.8</Version>
    <PackageVersion>1.0.8-preview2</PackageVersion>
</PropertyGroup>
```

**Ändern für neue Version:**
1. `AssemblyVersion` erhöhen (für .NET-Assemblies)
2. `Version` anpassen (NuGet-Package-Hauptversion)
3. `PackageVersion` setzen (inklusive Pre-Release-Suffix falls notwendig)
4. CHANGELOG.md und RELEASENOTES.md aktualisieren

---

## Debugging-Tipps

### 1. Template-Verarbeitung debuggen

```csharp
// Breakpoint vor und nach Verarbeitung setzen
var engine = new TemplateEngine<Customer>(customer, template);

// Template vor Verarbeitung prüfen
string beforeProcessing = engine.TemplateString;

string result = engine.CreateStringFromTemplate();

// Ergebnis nach Verarbeitung prüfen
Console.WriteLine($"Vor: {beforeProcessing}");
Console.WriteLine($"Nach: {result}");
```

### 2. Reflection-Debugging

```csharp
// Properties des DataModels inspizieren
var properties = customer.GetType().GetProperties();
foreach (var prop in properties)
{
    var value = prop.GetValue(customer);
    Console.WriteLine($"{prop.Name}: {value}");
}
```

### 3. Razor-Kompilierung debuggen

```csharp
try
{
    var razorEngine = new RazorTemplateEngine<Person>();
    razorEngine.TemplateString = razorTemplate;
    string result = razorEngine.CreateStringFromTemplate(person);
}
catch (RazorEngineCompilationException ex)
{
    Console.WriteLine("Razor-Kompilierungs-Fehler:");
    Console.WriteLine(ex.Message);
    Console.WriteLine("Errors:");
    foreach (var error in ex.Errors)
    {
        Console.WriteLine($"  - {error}");
    }
}
```

### 4. Unit-Test-Debugging

```bash
# Einzelnen Test mit Debugging ausführen
dotnet test --filter "MethodName=can_create_a_valid_string_from_template"

# Visual Studio: Rechtsklick auf Test → "Debug Test"
# VS Code: "Debug Test" im Test Explorer
```

### 5. Performance-Analyse

```csharp
using System.Diagnostics;

var stopwatch = Stopwatch.StartNew();
var engine = new TemplateEngine<Customer>(customer, template);
string result = engine.CreateStringFromTemplate();
stopwatch.Stop();

Console.WriteLine($"Template-Verarbeitung: {stopwatch.ElapsedMilliseconds}ms");
```

---

## Erweiterte Entwicklungs-Themen

### Neue Datentypen hinzufügen

Um einen neuen Datentyp in `TemplateEngine` zu unterstützen:

1. **ReplacementAction registrieren** in `PlaceholderValueRaplacer.cs`:

```csharp
private void RegisterReplacementActions()
{
    _replacementActionCollection
        // ... bestehende Actions ...
        .AddReplacementAction(typeof(Guid), (placeholderValueName, value) => 
            ReplaceValueInOutputString(placeholderValueName, (Guid)value));
}
```

2. **Tests hinzufügen** in `TemplateEngineUnitTest.cs`:

```csharp
[Test]
public void can_handle_guid_values()
{
    var guid = Guid.NewGuid();
    var model = new { Id = guid };
    var engine = new TemplateEngine(model, "ID: ${Id}");
    string result = engine.CreateStringFromTemplate();
    Assert.AreEqual($"ID: {guid}", result);
}
```

### Custom Template-Engine erstellen

Implementieren Sie `ITemplateEngine<T>`:

```csharp
public class MyCustomEngine<T> : ITemplateEngine<T>
{
    public string OpeningDelimiter { get; set; }
    public string CloseingDelimiter { get; set; }
    public T TemplateDataModel { get; set; }
    public string TemplateString { get; set; }
    public string NullStringValue { get; set; }
    public CultureInfo CultureInfo { get; set; }
    public ITemplateEngineConfig<T> Config { get; set; }
    
    public string CreateStringFromTemplate(string stringTemplate = null)
    {
        // Eigene Implementierung
    }
    
    public string CreateStringFromTemplate(T templateDataModel)
    {
        // Eigene Implementierung
    }
    
    public string CreateStringFromTemplate(T templateDataModel, string stringTemplate)
    {
        // Eigene Implementierung
    }
}
```

---

## Trouble-Shooting

### Problem: Build schlägt fehl

```bash
# Dependencies neu laden
dotnet restore --force
dotnet clean
dotnet build
```

### Problem: Tests schlagen fehl

```bash
# Alle Build-Artefakte löschen
dotnet clean
rm -rf */bin */obj
dotnet build
dotnet test
```

### Problem: NuGet-Package kann nicht erstellt werden

```bash
# Pack mit Verbose-Output
dotnet pack --configuration Release --verbosity detailed
```

### Problem: Razor-Templates funktionieren nicht

**Prüfen:**
1. Erbt DataModel von `TemplateDataModel<T>`?
2. Ist `@Model` in Template korrekt?
3. RazorEngineCore-Dependency installiert?

```bash
dotnet list package | grep RazorEngineCore
```

---

## Ressourcen

### Offizielle Dokumentation

- [.NET 8.0 Dokumentation](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-8)
- [NuGet Package Dokumentation](https://learn.microsoft.com/nuget/)
- [GitHub Actions Dokumentation](https://docs.github.com/actions)

### Dependencies

- [RazorEngineCore](https://github.com/adoconnection/RazorEngineCore)

### Code-Qualität

- [CodeFactor](https://www.codefactor.io/repository/github/mbsoftlab/mbsoftlab.templateengine.core)

---

**Letzte Aktualisierung:** Dezember 2025  
**Commit-Referenz:** 5c37e68
