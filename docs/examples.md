# Beispiele und Tutorials

**Commit-Referenz:** 5c37e68  
**Dokumentations-Stand:** Dezember 2025

---

## Inhaltsverzeichnis

1. [Schnellstart](#schnellstart)
2. [Einfache Beispiele](#einfache-beispiele)
3. [Fortgeschrittene Beispiele](#fortgeschrittene-beispiele)
4. [Razor-Templates](#razor-templates)
5. [Praxis-Szenarien](#praxis-szenarien)
6. [Tipps und Tricks](#tipps-und-tricks)

---

## Schnellstart

### Installation

```bash
# NuGet Package Manager
Install-Package MbSoftLab.TemplateEngine.Core

# .NET CLI
dotnet add package MbSoftLab.TemplateEngine.Core
```

Optional: Razor-Unterstützung

```bash
dotnet add package MbSoftLab.TemplateEngine.Core.Razor
```

### Erstes Beispiel (30 Sekunden)

```csharp
using MbSoftLab.TemplateEngine.Core;

// 1. Datenmodell erstellen
var person = new { FirstName = "Max", LastName = "Mustermann" };

// 2. Template definieren
string template = "Hallo ${FirstName} ${LastName}!";

// 3. Template-Engine initialisieren
var engine = new TemplateEngine(person, template);

// 4. String erstellen
string result = engine.CreateStringFromTemplate();

Console.WriteLine(result);
// Output: Hallo Max Mustermann!
```

---

## Einfache Beispiele

### Beispiel 1: Grundlegende Verwendung

```csharp
using MbSoftLab.TemplateEngine.Core;

public class Customer
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int OrderCount { get; set; }
}

// Daten
var customer = new Customer 
{ 
    Name = "Anna Schmidt",
    Email = "anna@example.com",
    OrderCount = 5
};

// Template
string template = @"
Kunde: ${Name}
E-Mail: ${Email}
Anzahl Bestellungen: ${OrderCount}
";

// Verarbeitung
var engine = new TemplateEngine<Customer>(customer, template);
string result = engine.CreateStringFromTemplate();

Console.WriteLine(result);
```

**Output:**
```
Kunde: Anna Schmidt
E-Mail: anna@example.com
Anzahl Bestellungen: 5
```

---

### Beispiel 2: Property-Injection

```csharp
// Engine ohne Daten erstellen
var engine = new TemplateEngine<Customer>();

// Später Daten setzen
engine.TemplateDataModel = customer;
engine.TemplateString = "Hallo ${Name}!";

// Verarbeiten
string result = engine.CreateStringFromTemplate();
```

**Vorteil:** Flexibel für Dependency Injection und Testszenarien.

---

### Beispiel 3: Custom Delimiters

```csharp
var customer = new Customer { Name = "Bob" };

var engine = new TemplateEngine<Customer>(customer, "[[Name]]");
engine.OpeningDelimiter = "[[";
engine.CloseingDelimiter = "]]";

string result = engine.CreateStringFromTemplate();
// Output: Bob
```

**Use-Case:** Konflikte mit anderen Template-Syntaxen vermeiden (z.B. Angular, Vue.js).

---

### Beispiel 4: NULL-Wert-Behandlung

```csharp
var customer = new Customer 
{ 
    Name = "Charlie",
    Email = null  // NULL-Wert
};

var engine = new TemplateEngine<Customer>(customer, "E-Mail: ${Email}");
string result = engine.CreateStringFromTemplate();
// Output: E-Mail: NULL

// Custom NULL-String
engine.NullStringValue = "Keine Angabe";
result = engine.CreateStringFromTemplate();
// Output: E-Mail: Keine Angabe
```

---

### Beispiel 5: Kultur-spezifische Formatierung

```csharp
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime Available { get; set; }
}

var product = new Product
{
    Name = "Laptop",
    Price = 1299.99m,
    Available = new DateTime(2025, 12, 24)
};

// Deutsche Formatierung
var engineDE = new TemplateEngine<Product>(product, 
    "Produkt: ${Name}\nPreis: ${Price}\nVerfügbar ab: ${Available}");
engineDE.CultureInfo = System.Globalization.CultureInfo.GetCultureInfo("de-DE");
Console.WriteLine(engineDE.CreateStringFromTemplate());

// Output:
// Produkt: Laptop
// Preis: 1299,99
// Verfügbar ab: 24.12.2025 00:00:00
```

---

### Beispiel 6: Template aus Datei laden

**Template-Datei:** `email-template.txt`
```
Sehr geehrte/r ${Name},

vielen Dank für Ihre ${OrderCount} Bestellungen.

Mit freundlichen Grüßen
Ihr Team
```

**C#-Code:**
```csharp
var customer = new Customer 
{ 
    Name = "Diana Weber",
    OrderCount = 3
};

var engine = new TemplateEngine<Customer>(customer);
engine.LoadTemplateFromFile("email-template.txt");
string result = engine.CreateStringFromTemplate();

Console.WriteLine(result);
```

---

### Beispiel 7: Methoden in Templates

```csharp
public class Invoice
{
    public string InvoiceNumber { get; set; }
    public decimal NetAmount { get; set; }
    public decimal TaxRate { get; set; }
    
    // Parameterlose Methode
    public decimal GetGrossAmount()
    {
        return NetAmount * (1 + TaxRate);
    }
    
    public string GetFormattedInvoiceNumber()
    {
        return $"INV-{InvoiceNumber}";
    }
}

var invoice = new Invoice
{
    InvoiceNumber = "2025-001",
    NetAmount = 100m,
    TaxRate = 0.19m
};

string template = @"
Rechnungsnummer: ${GetFormattedInvoiceNumber()}
Nettobetrag: ${NetAmount}
Bruttobetrag: ${GetGrossAmount()}
";

var engine = new TemplateEngine<Invoice>(invoice, template);
string result = engine.CreateStringFromTemplate();
```

**Output:**
```
Rechnungsnummer: INV-2025-001
Nettobetrag: 100
Bruttobetrag: 119
```

**Wichtig:** Nur parameterlose öffentliche Methoden funktionieren!

---

## Fortgeschrittene Beispiele

### Beispiel 8: JSON-Daten verwenden

```csharp
string jsonData = @"
{
    ""Name"": ""Eve Johnson"",
    ""Email"": ""eve@example.com"",
    ""OrderCount"": 7
}";

var engine = new TemplateEngine<Customer>();
engine.TemplateString = "Kunde ${Name} hat ${OrderCount} Bestellungen.";

string result = engine.CreateStringFromTemplateWithJson(jsonData);
// Output: Kunde Eve Johnson hat 7 Bestellungen.
```

---

### Beispiel 9: Konfigurationsobjekt verwenden

```csharp
var config = new TemplateEngineConfig<Customer>
{
    OpeningDelimiter = "{{",
    CloseingDelimiter = "}}",
    NullStringValue = "---",
    TemplateString = "{{Name}} ({{Email}})",
    TemplateDataModel = customer,
    CultureInfo = System.Globalization.CultureInfo.GetCultureInfo("de-DE")
};

var engine = new TemplateEngine<Customer>();
engine.Config = config;

string result = engine.CreateStringFromTemplate();
```

**Vorteil:** Wiederverwendbare Konfiguration, ideal für Unit-Tests.

---

### Beispiel 10: Mehrere Templates mit gleichen Daten

```csharp
var customer = new Customer { Name = "Frank", Email = "frank@test.de" };

var emailEngine = new TemplateEngine<Customer>(customer);
var smsEngine = new TemplateEngine<Customer>(customer);

// E-Mail-Template
emailEngine.TemplateString = "Hallo ${Name}, Ihre E-Mail: ${Email}";
string email = emailEngine.CreateStringFromTemplate();

// SMS-Template (kürzer)
smsEngine.TemplateString = "Hallo ${Name}!";
string sms = smsEngine.CreateStringFromTemplate();
```

---

### Beispiel 11: Dynamisches Template-Switching

```csharp
var customer = new Customer { Name = "Grace", OrderCount = 10 };

var engine = new TemplateEngine<Customer>(customer);

// Verschiedene Templates je nach Kontext
string template = customer.OrderCount > 5 
    ? "Vielen Dank für Ihre ${OrderCount} Bestellungen, ${Name}!"
    : "Hallo ${Name}, Sie haben ${OrderCount} Bestellungen.";

engine.TemplateString = template;
string result = engine.CreateStringFromTemplate();
```

---

## Razor-Templates (optional)

### Beispiel 12: Einfaches Razor-Template

```csharp
using MbSoftLab.TemplateEngine.Core;
// Hinweis: Stellen Sie sicher, dass das Paket MbSoftLab.TemplateEngine.Core.Razor installiert ist.

public class Person : TemplateDataModel<Person>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

var person = new Person 
{ 
    FirstName = "Hans",
    LastName = "Müller",
    Age = 35
};

// Razor steht erst nach Installation von MbSoftLab.TemplateEngine.Core.Razor zur Verfügung
var engine = new RazorTemplateEngine<Person>();

string razorTemplate = @"
<h1>@Model.FirstName @Model.LastName</h1>
<p>Alter: @Model.Age Jahre</p>
";

engine.TemplateString = razorTemplate;
string html = engine.CreateStringFromTemplate(person);
```

**Output:**
```html
<h1>Hans Müller</h1>
<p>Alter: 35 Jahre</p>
```

---

### Beispiel 13: Razor mit Bedingungen

```csharp
public class User : TemplateDataModel<User>
{
    public string Name { get; set; }
    public bool IsPremium { get; set; }
    public int Points { get; set; }
}

var user = new User 
{ 
    Name = "Ines",
    IsPremium = true,
    Points = 1250
};

string template = @"
<div class='user-card'>
    <h2>@Model.Name</h2>
    
    @if (Model.IsPremium) {
        <span class='badge'>Premium-Mitglied</span>
    }
    
    <p>Punkte: @Model.Points</p>
    
    @if (Model.Points > 1000) {
        <p class='highlight'>Sie haben genug Punkte für eine Belohnung!</p>
    }
</div>
";

var engine = new RazorTemplateEngine<User>(user, template);
string html = engine.CreateStringFromTemplate();
```

---

### Beispiel 14: Razor mit Listen (foreach)

```csharp
public class ShoppingList : TemplateDataModel<ShoppingList>
{
    public string Owner { get; set; }
    public List<string> Items { get; set; }
}

var list = new ShoppingList
{
    Owner = "Julia",
    Items = new List<string> { "Milch", "Brot", "Eier", "Butter" }
};

string template = @"
<h2>Einkaufsliste von @Model.Owner</h2>
<ul>
@foreach(var item in Model.Items) {
    <li>@item</li>
}
</ul>
<p>Gesamt: @Model.Items.Count Artikel</p>
";

var engine = new RazorTemplateEngine<ShoppingList>(list, template);
string html = engine.CreateStringFromTemplate();
```

**Output:**
```html
<h2>Einkaufsliste von Julia</h2>
<ul>
    <li>Milch</li>
    <li>Brot</li>
    <li>Eier</li>
    <li>Butter</li>
</ul>
<p>Gesamt: 4 Artikel</p>
```

---

### Beispiel 15: Razor mit verschachtelten Objekten

```csharp
public class Address : TemplateDataModel<Address>
{
    public string Street { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
}

public class Customer : TemplateDataModel<Customer>
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public List<Order> Orders { get; set; }
}

public class Order : TemplateDataModel<Order>
{
    public int Id { get; set; }
    public decimal Total { get; set; }
}

var customer = new Customer
{
    Name = "Klaus Werner",
    Address = new Address 
    { 
        Street = "Hauptstraße 1",
        City = "München",
        PostCode = "80331"
    },
    Orders = new List<Order>
    {
        new Order { Id = 1, Total = 49.99m },
        new Order { Id = 2, Total = 129.50m }
    }
};

string template = @"
<div class='customer'>
    <h1>@Model.Name</h1>
    
    <div class='address'>
        <h3>Adresse</h3>
        <p>@Model.Address.Street<br/>
           @Model.Address.PostCode @Model.Address.City</p>
    </div>
    
    <div class='orders'>
        <h3>Bestellungen</h3>
        <table>
            <thead>
                <tr><th>ID</th><th>Summe</th></tr>
            </thead>
            <tbody>
            @foreach(var order in Model.Orders) {
                <tr>
                    <td>@order.Id</td>
                    <td>@order.Total.ToString(""C"")</td>
                </tr>
            }
            </tbody>
        </table>
    </div>
</div>
";

var engine = new RazorTemplateEngine<Customer>(customer, template);
string html = engine.CreateStringFromTemplate();
```

---

### Beispiel 16: Razor-Template aus Datei laden

**Template-Datei:** `invoice.cshtml`
```cshtml
@* Rechnungs-Template *@
<html>
<head>
    <title>Rechnung @Model.InvoiceNumber</title>
</head>
<body>
    <h1>Rechnung @Model.InvoiceNumber</h1>
    
    <table>
    @foreach(var item in Model.Items) {
        <tr>
            <td>@item.ProductName</td>
            <td>@item.Quantity</td>
            <td>@item.Price.ToString("C")</td>
        </tr>
    }
    </table>
    
    <p><strong>Gesamt: @Model.Total.ToString("C")</strong></p>
</body>
</html>
```

**C#-Code:**
```csharp
var invoice = new Invoice 
{ 
    InvoiceNumber = "2025-123",
    Items = items,
    Total = 299.99m
};

var engine = new RazorTemplateEngine<Invoice>(invoice);
engine.LoadTemplateFromFile("invoice.cshtml");
string html = engine.CreateStringFromTemplate();

// HTML in Datei speichern
File.WriteAllText("invoice-2025-123.html", html);
```

---

## Praxis-Szenarien

### Szenario 1: E-Mail-Versand

```csharp
public class EmailService
{
    private readonly ITemplateEngine<EmailData> _engine;
    
    public EmailService()
    {
        _engine = new TemplateEngine<EmailData>();
    }
    
    public string GenerateWelcomeEmail(string userName, string email)
    {
        var data = new EmailData 
        { 
            UserName = userName,
            Email = email,
            RegistrationDate = DateTime.Now
        };
        
        _engine.LoadTemplateFromFile("templates/welcome-email.txt");
        return _engine.CreateStringFromTemplate(data);
    }
}

public class EmailData
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
}
```

**Template:** `welcome-email.txt`
```
Hallo ${UserName},

herzlich willkommen!

Ihre E-Mail-Adresse: ${Email}
Registriert am: ${RegistrationDate}

Viele Grüße
Ihr Team
```

---

### Szenario 2: Report-Generierung

```csharp
public class ReportGenerator
{
    public string GenerateMonthlyReport(ReportData data)
    {
        var config = new TemplateEngineConfig<ReportData>
        {
            TemplateDataModel = data,
            CultureInfo = CultureInfo.GetCultureInfo("de-DE"),
            NullStringValue = "Keine Daten"
        };
        
        var engine = new TemplateEngine<ReportData> { Config = config };
        engine.LoadTemplateFromFile("templates/monthly-report.txt");
        
        return engine.CreateStringFromTemplate();
    }
}
```

---

### Szenario 3: HTML-Berichte mit Razor

```csharp
public class HtmlReportService
{
    public void GenerateAndSaveReport(SalesData data, string outputPath)
    {
        var engine = new RazorTemplateEngine<SalesData>();
        engine.LoadTemplateFromFile("templates/sales-report.cshtml");
        
        string html = engine.CreateStringFromTemplate(data);
        
        File.WriteAllText(outputPath, html);
    }
}

public class SalesData : TemplateDataModel<SalesData>
{
    public string Month { get; set; }
    public decimal Revenue { get; set; }
    public List<Sale> TopSales { get; set; }
}
```

---

### Szenario 4: Konfigurations-Dateien generieren

```csharp
public class ConfigGenerator
{
    public void GenerateAppConfig(AppSettings settings)
    {
        string template = @"
server.host=${Host}
server.port=${Port}
database.connection=${DatabaseConnection}
logging.level=${LogLevel}
";
        
        var engine = new TemplateEngine<AppSettings>(settings, template);
        string config = engine.CreateStringFromTemplate();
        
        File.WriteAllText("app.config", config);
    }
}
```

---

## Tipps und Tricks

### Tipp 1: Performance bei vielen gleichen Templates

```csharp
// ❌ Nicht effizient
for (int i = 0; i < 1000; i++) {
    var engine = new TemplateEngine<Customer>(customers[i], template);
    results[i] = engine.CreateStringFromTemplate();
}

// ✅ Effizient
var engine = new TemplateEngine<Customer>();
engine.TemplateString = template;
for (int i = 0; i < 1000; i++) {
    results[i] = engine.CreateStringFromTemplate(customers[i]);
}
```

---

### Tipp 2: Template-Validierung

```csharp
public bool ValidateTemplate(string template, Type modelType)
{
    try
    {
        var instance = Activator.CreateInstance(modelType);
        var engine = new TemplateEngine(instance, template);
        engine.CreateStringFromTemplate();
        return true;
    }
    catch
    {
        return false;
    }
}
```

---

### Tipp 3: Collections mit TemplateEngine (Workaround)

```csharp
// Statt Collections direkt zu verwenden
public class Report
{
    public string Item1 { get; set; }
    public string Item2 { get; set; }
    public string Item3 { get; set; }
    
    // Oder: Methode mit String-Join
    public string GetAllItems()
    {
        return string.Join(", ", new[] { Item1, Item2, Item3 });
    }
}

string template = "Items: ${GetAllItems()}";
```

**Besser:** Verwenden Sie `RazorTemplateEngine<T>` für echte Listen!

---

### Tipp 4: Thread-Sicherheit

```csharp
// ❌ Nicht thread-safe
var sharedEngine = new TemplateEngine<Customer>();

// ✅ Thread-safe: Jeder Thread eigene Instanz
ThreadPool.QueueUserWorkItem(_ => {
    var engine = new TemplateEngine<Customer>(customer, template);
    var result = engine.CreateStringFromTemplate();
});
```

---

### Tipp 5: Fehlerbehandlung

```csharp
try
{
    var engine = new TemplateEngine<Customer>(customer, template);
    string result = engine.CreateStringFromTemplate();
}
catch (NotSupportedException ex)
{
    Console.WriteLine($"Nicht unterstützter Typ: {ex.Message}");
    // Fallback zu RazorTemplateEngine
}
catch (Exception ex)
{
    Console.WriteLine($"Fehler bei Template-Verarbeitung: {ex.Message}");
}
```

---

**Letzte Aktualisierung:** Dezember 2025  
**Commit-Referenz:** 5c37e68
