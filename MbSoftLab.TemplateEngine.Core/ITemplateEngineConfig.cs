using System.Globalization;

namespace MbSoftLab.TemplateEngine.Core;

/// <summary>
/// 
/// </summary>
public interface ITemplateEngineConfig : ITemplateEngineConfig<object> { }
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITemplateEngineConfig<T>
{
    string OpeningDelimiter { get; set; }
    string CloseingDelimiter { get; set; }
    string TemplateString { get; set; }
    T TemplateDataModel { get; set; }
    string NullStringValue { get; set; }
    CultureInfo CultureInfo { get; set; }
}