using System.Text.Json;

namespace MbSoftLab.TemplateEngine.Core;

public static class TemplateEngineExtensions
{
    /// <summary>
    /// Load the TemplateDataModel from JSON and builds a String with this Data
    /// </summary>
    public static string CreateStringFromTemplateWithJson<T>(this ITemplateEngine<T> templateEngine, string jsonData)
    {
        templateEngine.TemplateDataModel = JsonSerializer.Deserialize<T>(jsonData);
        return templateEngine.CreateStringFromTemplate();
    }
    /// <summary>
    /// Loads a Templatestring from File
    /// </summary>
    /// <param name="path">Path to File with Templatestring.</param>
    public static void LoadTemplateFromFile<T>(this ITemplateEngine<T> templateEngine, string path)
    {
        templateEngine.TemplateString = System.IO.File.ReadAllText(path);
    }
}