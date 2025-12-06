using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace MbSoftLab.TemplateEngine.Core;

/// <summary>
/// A simple StringTemplateengine for .NET. <br></br>
/// See <a href="https://github.com/mbsoftlab/MbSoftLab.TemplateEngine.Core/blob/master/README.md"/> for more details
/// </summary>
public class TemplateEngine : TemplateEngine<object>, ITemplateEngine<object>
{
    #region --- CONSTRUCTORS
    public TemplateEngine(object templateDataModel, string stringTemplate) : base(templateDataModel, stringTemplate)
    {

    }
    public TemplateEngine(object templateDataModel) : base(templateDataModel)
    {

    }

    public TemplateEngine()
    {

    }
    #endregion
}

public class TemplateEngine<T> : ITemplateEngine<T>
{
    private string _outputString;

    #region --- PUBLIC PROPERTYS
    /// <summary>
    /// Beginning Char for a PlaceholderProperty. The Defaultvalue ist "${".
    /// </summary>
    public string OpeningDelimiter { get => _openingDelimiter; set => _openingDelimiter = value.Trim(); }
    private string _openingDelimiter = "${";
    /// <summary>
    /// Ending Char for a PlaceholderProperty. Der Default ist "}".
    /// </summary>
    public string CloseingDelimiter { get => _closeingDelimiter; set => _closeingDelimiter = value.Trim(); }
    private string _closeingDelimiter = "}";
    /// <summary>
    /// Model with propertys to fill ${PlaceholderPropertys} in the template. The propertynames at DataModel has to be equal with ${Placeholder}
    /// </summary>
    public T TemplateDataModel { get => _templateDataModel; set => _templateDataModel = value; }
    T _templateDataModel;
    /// <summary>
    /// The Templatestring with ${PlaceholderPropertys}
    /// </summary>
    public string TemplateString
    {
        get => _templateString;
        set
        {
            if (value != null && value != _templateString)
                _templateString = value;
        }
    }
    string _templateString;
    /// <summary>
    /// Get or Set the string for NULL-Values. Default = NULL. 
    /// </summary>
    public string NullStringValue { get => _nullStringValue; set => _nullStringValue = value; }
    string _nullStringValue = "NULL";
    public CultureInfo CultureInfo { get; set; } = CultureInfo.CreateSpecificCulture("en-US");

    public ITemplateEngineConfig<T> Config
    {
        get => _config;
        set
        {
            _config = value;
            this.NullStringValue = _config.NullStringValue;
            this.OpeningDelimiter = _config.OpeningDelimiter;
            this.CloseingDelimiter = _config.CloseingDelimiter;
            this.TemplateDataModel = _config.TemplateDataModel;
            this.TemplateString = _config.TemplateString;
            this.CultureInfo = _config.CultureInfo;
        }
    }
    private ITemplateEngineConfig<T> _config;
    #endregion

    #region --- CONSTRUCTORS
    public TemplateEngine(T templateDataModel, string stringTemplate)
    {
        _templateDataModel = templateDataModel;
        _templateString = stringTemplate;
    }
    public TemplateEngine(T templateDataModel)
    {
        _templateDataModel = templateDataModel;
    }

    public TemplateEngine()
    {

    }

    #endregion

    /// <summary>
    /// Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
    /// Example: public string MyProperty  => ${MyProperty}
    /// </summary>
    /// <returns>File with Data from TemplateDataModel </returns>
    public string CreateStringFromTemplate(string stringTemplate = null)
    {
        try
        {
            TemplateString = stringTemplate;
            return CreateStringFromTemplate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
    /// Example: public string MyProperty  => ${MyProperty}
    /// </summary>
    /// <returns>File with Data from TemplateDataModel </returns>
    public string CreateStringFromTemplate(T templateDataModel, string stringTemplate)
    {
        TemplateString = stringTemplate;
        _templateDataModel = templateDataModel;
        return CreateStringFromTemplate();
    }
    /// <summary>
    /// Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
    /// Example: public string MyProperty  => ${MyProperty}
    /// </summary>
    /// <returns>File with Data from TemplateDataModel </returns>
    public string CreateStringFromTemplate(T templateDataModel)
    {
        _templateDataModel = templateDataModel;
        return CreateStringFromTemplate();
    }

    /// <summary>
    /// Replaces all Propertys of templateDataModel in stringTemplate. The Popertynames from templateDataModel a the name of ${Placeholder} have to be equal. 
    /// Example: public string MyProperty  => ${MyProperty}
    /// </summary>
    /// <returns>File with Data from TemplateDataModel </returns>
    private string CreateStringFromTemplate()
    {
        _outputString = _templateString;

        IPlaceholderValueRaplacer placeholderValueRaplacer = new PlaceholderValueRaplacer(_outputString, _nullStringValue);
        placeholderValueRaplacer.CultureInfo = CultureInfo;
        TemplateDataModelProcessor templateDataModelProcessor = new TemplateDataModelProcessor(_openingDelimiter, _closeingDelimiter, placeholderValueRaplacer);
        templateDataModelProcessor.ProcessTemplateDataModell(_templateDataModel);

        return placeholderValueRaplacer.OutputString;
    }
}