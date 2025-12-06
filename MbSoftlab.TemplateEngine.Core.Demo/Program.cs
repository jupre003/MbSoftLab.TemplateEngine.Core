using MbSoftLab.TemplateEngine.Core;
using MbSoftLab.TemplateEngine.Core.Demo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

Person testModel = new Person
{
    FirstName = "Justin",
    LastName = "LastName",
    Tags = new List<string> { "Tag1", "Tag2", "Tag3" },
    Address = new Address
    {
        Street = "Straße",
        PostCode = "7872"
    },
    Orders = new List<Order>
    {
        new()
        {
            Id = 1,
            Products = new List<Product>
            {
                new() { ProductName = "Product1", Price = 150 },
                new() { ProductName = "Product2", Price = 50 }
            }
        }
    }
};

ITemplateEngine<Person> templateEngine = new RazorTemplateEngine<Person>();
templateEngine.LoadTemplateFromFile<Person>("TestModel.cshtml");
var templateFileContent = templateEngine.CreateStringFromTemplate(testModel);
Console.WriteLine();
Console.WriteLine(templateFileContent);

var htmlFileName = Path.Combine(Path.GetTempPath(), "temp.html");
await File.WriteAllTextAsync(htmlFileName, templateFileContent);
OpenInDefaultBrowser(htmlFileName);

void OpenInDefaultBrowser(string filePath)
{
    try
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c start \"\" \"{filePath}\"",
                CreateNoWindow = true
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", filePath);
        }
        else
        {
            Process.Start("xdg-open", filePath);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Konnte Browser nicht öffnen: {ex.Message}");
        Console.WriteLine($"Pfad: {filePath}");
    }
}