using System.Globalization;

namespace MbSoftLab.TemplateEngine.Core;

/// <summary>
/// <inheritdoc cref="ITemplateEngineConfig"/>
/// </summary>
public class TemplateEngineConfig : TemplateEngineConfig<object> { }
/// <summary>
/// <inheritdoc cref="ITemplateEngineConfig{T}"/>
/// </summary>
public class TemplateEngineConfig<T> : ITemplateEngineConfig<T>
{
    public CultureInfo CultureInfo { get; set; } = CultureInfo.CreateSpecificCulture("en-US");
    public string OpeningDelimiter { get => _openingDelimiter; set => _openingDelimiter = value.Trim(); }
    private string _openingDelimiter;
    public string CloseingDelimiter { get => _closeingDelimiter; set => _closeingDelimiter = value.Trim(); }
    private string _closeingDelimiter;
    public string TemplateString { get => _templateString; set => _templateString = value; }
    private string _templateString;
    public T TemplateDataModel { get => _templateDataModel; set => _templateDataModel = value; }
    private T _templateDataModel;
    public string NullStringValue { get => _nullStringValue; set => _nullStringValue = value; }
    private string _nullStringValue;
}