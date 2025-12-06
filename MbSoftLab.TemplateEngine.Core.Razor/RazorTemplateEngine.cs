using RazorEngineCore;
using System;
using System.ComponentModel;
using System.Globalization;

namespace MbSoftLab.TemplateEngine.Core;

public class RazorTemplateEngine<T> : ITemplateEngine<T> where T : TemplateDataModel<T>
{
    public string CloseingDelimiter { get { Console.WriteLine($"RazorTemplateEngine has no {nameof(CloseingDelimiter)}"); return ""; } set => Console.WriteLine($"Can not set {nameof(CloseingDelimiter)} for RazorTemplateEngine"); }
    ITemplateEngineConfig<T> _config;
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
    public CultureInfo CultureInfo { get { Console.WriteLine($"{nameof(RazorTemplateEngine<T>)} can not maipulate {nameof(CultureInfo)}"); return null; } set => Console.WriteLine($"{nameof(RazorTemplateEngine<T>)} can not maipulate {nameof(CultureInfo)}"); }

    public string NullStringValue
    {
        get => "String.Empty";
        set { Console.WriteLine(new WarningException($"{nameof(RazorTemplateEngine<T>)} can not maipulate {nameof(NullStringValue)}").Message); }
    }

    public string OpeningDelimiter
    {
        get
        {
            Console.WriteLine(new WarningException($"RazorTemplateEngine has no {nameof(OpeningDelimiter)}").Message);
            return "";
        }
        set
        {
            Console.WriteLine(new WarningException($"Can not set {nameof(OpeningDelimiter)} for RazorTemplateEngine").Message);
        }
    }
    
    public T TemplateDataModel { get; set; }
    public string TemplateString { get; set; }
    IRazorEngine razorEngine;
    
    public RazorTemplateEngine(IRazorEngine razorEngine)
    {
        this.razorEngine = razorEngine;
    }
    
    public RazorTemplateEngine()
    {
        razorEngine = new RazorEngine();
    }

    public RazorTemplateEngine(T dataModel, string templateString)
    {
        razorEngine = new RazorEngine();
        TemplateDataModel = dataModel;
        TemplateString = templateString;
    }
    
    public string CreateStringFromTemplate(string csHtmlTemplate = null)
    {
        TemplateString = csHtmlTemplate ?? TemplateString;
        IRazorEngineCompiledTemplate template = razorEngine.Compile(TemplateString);

        string result = template.Run(TemplateDataModel);
        return result;
    }

    public string CreateStringFromTemplate(T templateDataModel)
    {
        TemplateDataModel = templateDataModel;
        return CreateStringFromTemplate();
    }

    public string CreateStringFromTemplate(T templateDataModel, string csHtmlTemplate)
    {
        TemplateDataModel = templateDataModel;
        TemplateString = csHtmlTemplate;
        return CreateStringFromTemplate();
    }
}
