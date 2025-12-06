using System.Globalization;

namespace MbSoftLab.TemplateEngine.Core;

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