# API-Dokumentation

**Commit-Referenz:** 5c37e68  
**Dokumentations-Stand:** Dezember 2025

---

## Inhaltsverzeichnis

1. [Übersicht](#übersicht)
2. [TemplateEngine](#templateengine)
3. [RazorTemplateEngine](#razortemplateengine)
4. [Interfaces](#interfaces)
5. [Konfiguration](#konfiguration)
6. [Erweiterungsmethoden](#erweiterungsmethoden)
7. [Datentypen](#datentypen)

---

## Übersicht

Die MbSoftLab.TemplateEngine.Core Bibliothek bietet zwei Hauptklassen für Template-Verarbeitung:

- **TemplateEngine / TemplateEngine<T>:** Für einfache String-basierte Templates
- **RazorTemplateEngine<T>:** Für komplexe Razor-basierte Templates

Beide implementieren das `ITemplateEngine<T>` Interface.

---

## TemplateEngine

### TemplateEngine (nicht-generisch)

```csharp
public class TemplateEngine : TemplateEngine<object>, ITemplateEngine<object>
```

**Beschreibung:** Vereinfachte Version von `TemplateEngine<T>` mit `object` als TemplateDataModel-Typ.

#### Konstruktoren

```csharp
// 1. Mit DataModel und Template
public TemplateEngine(object templateDataModel, string stringTemplate)

// 2. Nur mit DataModel
public TemplateEngine(object templateDataModel)

// 3. Parameterlos (Eigenschaften später setzen)
public TemplateEngine()
```

#### Beispiel

```csharp
var person = new { FirstName = "Max", LastName = "Mustermann" };
var engine = new TemplateEngine(person, "Hallo ${FirstName} ${LastName}!");
string result = engine.CreateStringFromTemplate();
// Ergebnis: "Hallo Max Mustermann!"
```

---

### TemplateEngine<T> (generisch)

```csharp
public class TemplateEngine<T> : ITemplateEngine<T>
```

**Beschreibung:** Generische Template-Engine für typisierte DataModels.

#### Eigenschaften

| Eigenschaft | Typ | Standardwert | Beschreibung |
|-------------|-----|--------------|--------------|
| `OpeningDelimiter` | `string` | `"${"` | Anfangs-Delimiter für Platzhalter |
| `CloseingDelimiter` | `string` | `"}"` | End-Delimiter für Platzhalter |
| `TemplateDataModel` | `T` | - | Datenmodell mit Properties/Methoden |
| `TemplateString` | `string` | - | Template-String mit Platzhaltern |
| `NullStringValue` | `string` | `"NULL"` | Ersatzwert für NULL-Properties |
| `CultureInfo` | `CultureInfo` | `en-US` | Kultur für Zahlen-/Datumsformatierung |
| `Config` | `ITemplateEngineConfig<T>` | - | Zentrale Konfiguration |

#### Konstruktoren

```csharp
// 1. Mit DataModel und Template
public TemplateEngine(T templateDataModel, string stringTemplate)

// 2. Nur mit DataModel
public TemplateEngine(T templateDataModel)

// 3. Parameterlos
public TemplateEngine()
```

#### Methoden

##### CreateStringFromTemplate()

```csharp
public string CreateStringFromTemplate()
```

**Beschreibung:** Erstellt String aus aktuellem TemplateString und TemplateDataModel.

**Rückgabe:** Verarbeiteter String mit ersetzten Platzhaltern.

**Beispiel:**
```csharp
var engine = new TemplateEngine<Person>();
engine.TemplateDataModel = person;
engine.TemplateString = "Hallo ${FirstName}!";
string result = engine.CreateStringFromTemplate();
```

---

##### CreateStringFromTemplate(string stringTemplate)

```csharp
public string CreateStringFromTemplate(string stringTemplate = null)
```

**Parameter:**
- `stringTemplate` - Optionaler Template-String (überschreibt `TemplateString`)

**Rückgabe:** Verarbeiteter String.

**Beispiel:**
```csharp
var engine = new TemplateEngine<Person>(person);
string result = engine.CreateStringFromTemplate("${FirstName} ${LastName}");
```

---

##### CreateStringFromTemplate(T templateDataModel)

```csharp
public string CreateStringFromTemplate(T templateDataModel)
```

**Parameter:**
- `templateDataModel` - Neues DataModel (überschreibt `TemplateDataModel`)

**Rückgabe:** Verarbeiteter String.

**Beispiel:**
```csharp
var engine = new TemplateEngine<Person>();
engine.TemplateString = "Hallo ${FirstName}!";
string result = engine.CreateStringFromTemplate(person);
```

---

##### CreateStringFromTemplate(T templateDataModel, string stringTemplate)

```csharp
public string CreateStringFromTemplate(T templateDataModel, string stringTemplate)
```

**Parameter:**
- `templateDataModel` - Neues DataModel
- `stringTemplate` - Neuer Template-String

**Rückgabe:** Verarbeiteter String.

**Beispiel:**
```csharp
var engine = new TemplateEngine<Person>();
string result = engine.CreateStringFromTemplate(person, "Hallo ${FirstName}!");
```

---

### Template-Syntax

#### Property-Platzhalter

```csharp
// DataModel
public class Person {
    public string FirstName { get; set; }
    public int Age { get; set; }
}

// Template
"Name: ${FirstName}, Alter: ${Age}"
```

#### Methoden-Platzhalter

```csharp
// DataModel
public class Person {
    public string GetFullName() {
        return $"{FirstName} {LastName}";
    }
}

// Template
"Vollständiger Name: ${GetFullName()}"
```

**Wichtig:** Nur parameterlose öffentliche Methoden werden unterstützt!

#### Custom Delimiters

```csharp
var engine = new TemplateEngine<Person>(person, "[[FirstName]] [[LastName]]");
engine.OpeningDelimiter = "[[";
engine.CloseingDelimiter = "]]";
string result = engine.CreateStringFromTemplate();
```

---

## RazorTemplateEngine

### RazorTemplateEngine<T>

```csharp
public class RazorTemplateEngine<T> : ITemplateEngine<T> 
    where T : TemplateDataModel<T>
```

**Beschreibung:** Template-Engine für Razor-Syntax (.cshtml).

**Einschränkung:** Benötigt `TemplateDataModel<T>` als Basis-Klasse für DataModel.

#### Konstruktoren

```csharp
// 1. Mit IRazorEngine (für Dependency Injection)
public RazorTemplateEngine(IRazorEngine razorEngine)

// 2. Parameterlos (erstellt eigene RazorEngine-Instanz)
public RazorTemplateEngine()

// 3. Mit DataModel und Template
public RazorTemplateEngine(T dataModel, string templateString)
```

#### Eigenschaften

| Eigenschaft | Typ | Beschreibung |
|-------------|-----|--------------|
| `TemplateDataModel` | `T` | Datenmodell (muss von `TemplateDataModel<T>` erben) |
| `TemplateString` | `string` | Razor-Template (.cshtml Syntax) |
| `Config` | `ITemplateEngineConfig<T>` | Konfiguration |

**Hinweis:** `OpeningDelimiter`, `CloseingDelimiter`, `NullStringValue` und `CultureInfo` werden ignoriert (Razor hat eigene Syntax).

#### Methoden

##### CreateStringFromTemplate()

```csharp
public string CreateStringFromTemplate(string csHtmlTemplate = null)
```

**Parameter:**
- `csHtmlTemplate` - Optionales Razor-Template (überschreibt `TemplateString`)

**Rückgabe:** Gerendeter HTML-String.

**Beispiel:**
```csharp
var engine = new RazorTemplateEngine<Person>();
engine.TemplateString = "@Model.FirstName @Model.LastName";
string result = engine.CreateStringFromTemplate(person);
```

---

##### CreateStringFromTemplate(T templateDataModel)

```csharp
public string CreateStringFromTemplate(T templateDataModel)
```

**Parameter:**
- `templateDataModel` - Neues DataModel

**Rückgabe:** Gerendeter HTML-String.

---

##### CreateStringFromTemplate(T templateDataModel, string csHtmlTemplate)

```csharp
public string CreateStringFromTemplate(T templateDataModel, string csHtmlTemplate)
```

**Parameter:**
- `templateDataModel` - Neues DataModel
- `csHtmlTemplate` - Neues Razor-Template

**Rückgabe:** Gerendeter HTML-String.

---

### Razor-Template-Syntax

```cshtml
@* Person-Daten anzeigen *@
<h1>@Model.FirstName @Model.LastName</h1>

@* Bedingte Anzeige *@
@if (Model.Age >= 18) {
    <p>Volljährig</p>
} else {
    <p>Minderjährig</p>
}

@* Listen iterieren *@
<ul>
@foreach(var tag in Model.Tags) {
    <li>@tag</li>
}
</ul>

@* Verschachtelte Objekte *@
<p>Adresse: @Model.Address.Street, @Model.Address.PostCode</p>
```

---

## Interfaces

### ITemplateEngine<T>

```csharp
public interface ITemplateEngine<T>
{
    string CloseingDelimiter { get; set; }
    ITemplateEngineConfig<T> Config { get; set; }
    CultureInfo CultureInfo { get; set; }
    string NullStringValue { get; set; }
    string OpeningDelimiter { get; set; }
    T TemplateDataModel { get; set; }
    string TemplateString { get; set; }
    
    string CreateStringFromTemplate(string stringTemplate = null);
    string CreateStringFromTemplate(T templateDataModel);
    string CreateStringFromTemplate(T templateDataModel, string stringTemplate);
}
```

**Beschreibung:** Gemeinsames Interface für beide Template-Engine-Typen.

**Verwendung:** Ermöglicht austauschbare Nutzung von TemplateEngine und RazorTemplateEngine.

**Beispiel:**
```csharp
public void ProcessTemplate(ITemplateEngine<Person> engine, Person person) {
    string result = engine.CreateStringFromTemplate(person);
    // Funktioniert mit beiden Engine-Typen
}
```

---

### ITemplateEngineConfig<T>

```csharp
public interface ITemplateEngineConfig<T>
{
    string OpeningDelimiter { get; set; }
    string CloseingDelimiter { get; set; }
    string TemplateString { get; set; }
    T TemplateDataModel { get; set; }
    string NullStringValue { get; set; }
    CultureInfo CultureInfo { get; set; }
}
```

**Beschreibung:** Interface für Template-Engine-Konfiguration.

---

## Konfiguration

### TemplateEngineConfig<T>

```csharp
public class TemplateEngineConfig<T> : ITemplateEngineConfig<T>
```

**Beschreibung:** Konfigurationsklasse für zentrale Engine-Einstellungen.

#### Beispiel

```csharp
var config = new TemplateEngineConfig<Person> {
    OpeningDelimiter = "{{",
    CloseingDelimiter = "}}",
    TemplateString = "Hallo {{FirstName}}!",
    TemplateDataModel = person,
    NullStringValue = "???",
    CultureInfo = CultureInfo.GetCultureInfo("de-DE")
};

var engine = new TemplateEngine<Person>();
engine.Config = config;

string result = engine.CreateStringFromTemplate();
```

**Vorteile:**
- Zentrale Konfiguration
- Wiederverwendbar
- Testfreundlich

---

## Erweiterungsmethoden

### TemplateEngineExtensions

```csharp
public static class TemplateEngineExtensions
```

#### CreateStringFromTemplateWithJson<T>

```csharp
public static string CreateStringFromTemplateWithJson<T>(
    this ITemplateEngine<T> templateEngine, 
    string jsonData)
```

**Beschreibung:** Deserialisiert JSON zu DataModel und erstellt String.

**Parameter:**
- `jsonData` - JSON-String mit Daten

**Rückgabe:** Verarbeiteter String.

**Beispiel:**
```csharp
string json = "{\"FirstName\":\"Max\",\"LastName\":\"Mustermann\"}";
var engine = new TemplateEngine<Person>();
engine.TemplateString = "Hallo ${FirstName}!";
string result = engine.CreateStringFromTemplateWithJson(json);
// Ergebnis: "Hallo Max!"
```

---

#### LoadTemplateFromFile<T>

```csharp
public static void LoadTemplateFromFile<T>(
    this ITemplateEngine<T> templateEngine, 
    string path)
```

**Beschreibung:** Lädt Template-String aus Datei.

**Parameter:**
- `path` - Dateipfad zum Template

**Beispiel:**
```csharp
var engine = new TemplateEngine<Person>(person);
engine.LoadTemplateFromFile("templates/greeting.txt");
string result = engine.CreateStringFromTemplate();
```

**Template-Datei (greeting.txt):**
```
Hallo ${FirstName} ${LastName}!
Ihr Alter: ${Age}
```

---

## Datentypen

### Unterstützte Typen (TemplateEngine)

Die `TemplateEngine<T>` Klasse unterstützt folgende Datentypen für Properties:

#### Primitive Typen
- `string`
- `byte`
- `sbyte`
- `char`
- `short` (Int16)
- `ushort` (UInt16)
- `int` (Int32)
- `uint` (UInt32)
- `long` (Int64)
- `ulong` (UInt64)

#### Numerische Typen
- `decimal`
- `double`

#### Weitere Typen
- `DateTime`
- `bool` (Boolean)

#### Nicht unterstützte Typen
- **Object** (generisches object)
- **Custom Classes** (eigene Klassen)
- **Collections** (List, Array, Dictionary, IEnumerable, etc.)

**Workaround für Collections:** Verwenden Sie `RazorTemplateEngine<T>` für komplexe Datenstrukturen.

---

### TemplateDataModel<T>

```csharp
public class TemplateDataModel<T> : RazorEngineTemplateBase
{
    [JsonIgnore]
    public new T Model { get; set; }
    
    public string GetNullstringValue()
}
```

**Beschreibung:** Basis-Klasse für DataModels in `RazorTemplateEngine<T>`.

**Verwendung:**
```csharp
public class Person : TemplateDataModel<Person> {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public List<string> Tags { get; set; }
}
```

**Wichtig:** Nur für `RazorTemplateEngine<T>` erforderlich, nicht für `TemplateEngine<T>`.

---

## Kulturabhängige Formatierung

### CultureInfo-Verwendung

```csharp
var person = new Person { 
    Salary = 1234.56, 
    BirthDate = new DateTime(1990, 5, 15) 
};

// Deutsche Formatierung
var engineDE = new TemplateEngine<Person>(person, "Gehalt: ${Salary}, Geburt: ${BirthDate}");
engineDE.CultureInfo = CultureInfo.GetCultureInfo("de-DE");
string resultDE = engineDE.CreateStringFromTemplate();
// Ergebnis: "Gehalt: 1234,56, Geburt: 15.05.1990 00:00:00"

// US-Formatierung
var engineUS = new TemplateEngine<Person>(person, "Salary: ${Salary}, Birth: ${BirthDate}");
engineUS.CultureInfo = CultureInfo.GetCultureInfo("en-US");
string resultUS = engineUS.CreateStringFromTemplate();
// Ergebnis: "Salary: 1234.56, Birth: 5/15/1990 12:00:00 AM"
```

---

## Fehlerbehandlung

### NotSupportedException

Wird geworfen, wenn ein nicht unterstützter Datentyp verwendet wird:

```csharp
public class Person {
    public string Name { get; set; }
    public List<string> Tags { get; set; } // Nicht unterstützt!
}

var engine = new TemplateEngine<Person>(person, "${Tags}");
// Wirft: NotSupportedException: Type 'System.Collections.Generic.List`1[System.String]' not supported
```

**Lösung:** Verwenden Sie `RazorTemplateEngine<T>` für Collections.

---

## Best Practices

### 1. Typsichere Nutzung mit Generics

```csharp
// ✅ Gut: Typsicher
var engine = new TemplateEngine<Person>(person, template);

// ❌ Vermeiden: Untypisiert
var engine = new TemplateEngine(person, template);
```

### 2. Config-Objekt für Wiederverwendung

```csharp
// ✅ Gut: Wiederverwendbare Konfiguration
var config = new TemplateEngineConfig<Person> {
    OpeningDelimiter = "{{",
    CloseingDelimiter = "}}",
    NullStringValue = "N/A"
};

var engine1 = new TemplateEngine<Person> { Config = config };
var engine2 = new TemplateEngine<Person> { Config = config };
```

### 3. Erweiterungsmethoden nutzen

```csharp
// ✅ Gut: Fluent API
var engine = new TemplateEngine<Person>();
engine.LoadTemplateFromFile("template.txt");
string result = engine.CreateStringFromTemplate(person);

// ❌ Vermeiden: Manuelles File-Handling
string template = File.ReadAllText("template.txt");
var engine = new TemplateEngine<Person>(person, template);
```

### 4. RazorTemplateEngine für komplexe Szenarien

```csharp
// ✅ Gut: RazorTemplateEngine für Listen
var engine = new RazorTemplateEngine<Person>();
engine.TemplateString = "@foreach(var tag in Model.Tags) { <li>@tag</li> }";

// ❌ Nicht möglich: TemplateEngine unterstützt keine Collections
var engine = new TemplateEngine<Person>(person, "${Tags}"); // Fehler!
```

---

**Letzte Aktualisierung:** Dezember 2025  
**Commit-Referenz:** 5c37e68
