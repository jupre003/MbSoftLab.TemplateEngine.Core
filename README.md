# MbSoftLab.TemplateEngine.Core

![BuildFromDevelop](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/workflows/BuildFromDevelop/badge.svg?branch=develop) ![BuildFromMaster](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/workflows/BuildFromMaster/badge.svg?branch=master)![Release](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/workflows/Release/badge.svg) 

*Codequality (master branch)*
[![CodeFactor](https://www.codefactor.io/repository/github/mbsoftlab/mbsoftlab.templateengine.core/badge)](https://www.codefactor.io/repository/github/mbsoftlab/mbsoftlab.templateengine.core)

*Codequality (develop branch)*
[![CodeFactor](https://www.codefactor.io/repository/github/mbsoftlab/mbsoftlab.templateengine.core/badge/develop)](https://www.codefactor.io/repository/github/mbsoftlab/mbsoftlab.templateengine.core/overview/develop)
```csharp
namespace MbSoftLab.TemplateEngine.Core
{
    public class TemplateEngine : TemplateEngine<object>{}

    public class TemplateEngine<T>{...}
}
```
 
 
The TemplateEngine replaces values from properties of C# classes in template strings.
The C# class with the data holding properties is called the TemplateDataModel class. 
The string with the placeholders is called a string template or template string.


You can bind the value from your C-property to your string template by using `${YourPropertyName}`. 
You can set a custom delimiters. Use the `OpeningDelimiter` and `CloseingDelimiter` properties to handle this.

Also you can access parameterless public methods of TemplateDataModell classes by using `${MethodName()}` in your template. 


The default is `${` for the start delimiter and `}` for the end delimiter.


```csharp
 Person person = new Person
 {
     FirstName = "Jo",
     LastName="Doe"
 };

string template = "<MyTag>${FirstName}, ${LastName}</MyTag>";

TemplateEngine templateEngine = new TemplateEngine(person,template);
string outputString = templateEngine.CreateStringFromTemplate();

Console.Write(outputString); // Output: <MyTag>Jo, Doe</MyTag> 
```

---

## Install Package

**NuGet Package:** 
https://www.nuget.org/packages/MbSoftLab.TemplateEngine.Core/

```PM
PM> Install-Package MbSoftLab.TemplateEngine.Core
```

---

## Methods 
|Methodname                                                            |Description                                                     |
|------------------------------------------------------------------------|-----------------------------------------------------------------|
|`string CreateStringFromTemplate([string template])`                |*Creates a String from Datamodell and Template*   |
|`string CreateStringFromJson(templateEngine, string jsonData)`                          |*Loads the Templatedata from JSON*.  
  |
|`void LoadTemplateFromFile(string filename)`                          |*Loads a Stringtemplate from file*.                  |
|`TemplateEngine()`                                                   |Constructor         |
|`TemplateEngine(object templateDataModel, string stringTemplate)`     |Constructor         |
|`TemplateEngine(object templateDataModel)`                            |Constructor         |
|`TemplateEngine<T>()`                                               |Constructor         |
|`TemplateEngine<T>(T templateDataModel, string stringTemplate)`     |Constructor         |
|`TemplateEngine<T>(T templateDataModel)`                            |Constructor         |

---

## Propertys 

|Propertyname                            |Datatype          |Description                                                           |
|----------------------------------------|------------------|-------------------------------------------------------------------------|
|`OpeningDelimiter`                    |String            |Set the beginning delimiter for propertyreplacement                     |
|`CloseingDelimiter`                   |String            |Set the ending delimiter for propertyreplacement                       |
|`TemplateDataModel`                      |Generic / object  |Modell with Properys for Dataholding                                     |
|`TemplateString`                      |string            |Templatestring                                                     |
|`NullStringValue`                     |string            |String for NULL-Values                                              |
|`CultureInfo`                     |CultureInfo           |Culture for Double and DateTime values                                   |

---

## Exampels

### **Load the template and fill with Data from modell**
```csharp
// Create a modell Class for Data
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjectName = "Projektname"
 };

string template = "<MyTag>${ProjectName}</MyTag>";

TemplateEngine templateEngine = new TemplateEngine(templateDataModel,template);
string outputString = templateEngine.CreateStringFromTemplate();

Console.Write(outputString); // Output: <MyTag>ProjectName</MyTag> 
```
### **Load template from file**
```csharp
    TemplateDataModel templateDataModel = new TemplateDataModel
    {
      ProjectName = "Projektname",
      CustomerId = "1234",
      ProjectUrl = "https://google.com"
    };

     TemplateEngine templateEngine = new TemplateEngine(templateDataModel);
     templateEngine.LoadTemplateFromFile("Html.template.html");
     string outputString = templateEngine.CreateStringFromTemplate();

     Console.WriteLine(outputString);
```


### **Template and model over PropertyInjection** 
```csharp
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjectName = "Projectname",
     CustomerId = "1234",
     ProjectUrl = "https://google.com"
 };

 string template = "<p>${ProjectName}</p>";
 TemplateEngine templateEngine = new TemplateEngine();
 templateEngine.TemplateDataModel = templateDataModel;
 templateEngine.TemplateString = template;
 Console.WriteLine(templateEngine.CreateStringFromTemplate());

```

### **Template and model over DependencyInjection** 
```csharp

  TemplateDataModel templateDataModel = new TemplateDataModel
  {
      ProjectName = "Projectname",
      CustomerId = "1234",
      ProjectUrl = "https://google.com"
  };

  string template = "<p>${ProjectName}</p>";
  TemplateEngine templateEngine = new TemplateEngine(templateDataModel,template);
  Console.WriteLine(templateEngine.CreateStringFromTemplate());


```
### **TemplateEngine with PropertyInjection and generic type**
```csharp
 TemplateDataModel templateDataModel = new TemplateDataModel
 {
     ProjectName = "Projectname",
     CustomerId = null,
     ProjectUrl = "https://google.de"
 };

 string template = "<p>{{ProjectName}}</p><p>{{CustomerId}}</p>";
 TemplateEngine<TemplateDataModel> templateEngine = new TemplateEngine<TemplateDataModel>()
 {
     TemplateDataModel = templateDataModel,
     TemplateString = template,
     OpeningDelimiter = "{{",
     CloseingDelimiter = "}}",
     NullStringValue = "???"
 };
 Console.WriteLine(templateEngine.CreateStringFromTemplate());

```


---

 ## Datatype compatibility
 
- ✔ String
- ✔ Byte
- ✔ Short
- ✔ UShort
- ✔ Long
- ✔ ULong
- ✔ SByte
- ✔ Char
- ✔ UInt16
- ✔ Int32
- ✔ UInt64
- ✔ Int16
- ✔ Int32
- ✔ Int64
- ✔ Decimal
- ✔ Double
- ✔ DateTime
- ✔ Boolean
- ❌ Object
- ❌ CustomClasses
- ❌ IList, List, Dictionary, IEnumerable, etc..

 

---
 
## Repo 

https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core
---

## 📚 Documentation

**Umfassende Dokumentation verfügbar in `/docs`:**

- **[Architektur](/docs/architecture/)** - Technische Übersicht und Design-Entscheidungen
- **[API-Dokumentation](/docs/api/)** - Vollständige API-Referenz
- **[Beispiele & Tutorials](/docs/examples/)** - Praktische Codebeispiele
- **[Entwickler-Leitfaden](/docs/development/)** - Contribution Guidelines und Build-Prozess
- **[CHANGELOG](/CHANGELOG.md)** - Versions-Historie
- **[RELEASENOTES](/RELEASENOTES.md)** - Aktuelle Release-Informationen

**Schnellstart:** Siehe [docs/README.md](/docs/README.md)

---

## 🆕 Neu in Version 1.0.8-preview2

### RazorTemplateEngine für komplexe HTML-Templates

```csharp
public class Person : TemplateDataModel<Person>
{
    public string FirstName { get; set; }
    public List<string> Tags { get; set; }
}

var person = new Person { FirstName = "Max", Tags = new List<string> { "Tag1", "Tag2" } };
var engine = new RazorTemplateEngine<Person>();

string razorTemplate = @"
<h1>@Model.FirstName</h1>
<ul>
@foreach(var tag in Model.Tags) {
    <li>@tag</li>
}
</ul>
";

engine.TemplateString = razorTemplate;
string html = engine.CreateStringFromTemplate(person);
```

**Features:**
- Volle Razor-Syntax-Unterstützung
- Listen und Collections
- Bedingungen und Schleifen
- Verschachtelte Objekte

Mehr Details in den [Release Notes](/RELEASENOTES.md).

---

## Issues 

[report an issue](https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/issues)

---

**Commit-Referenz für diese Dokumentation:** 5c37e68