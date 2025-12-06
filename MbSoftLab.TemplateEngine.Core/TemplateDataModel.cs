using RazorEngineCore;
using System.Text.Json.Serialization;

namespace MbSoftLab.TemplateEngine.Core;

public class TemplateDataModel<T> : RazorEngineTemplateBase
{
    [JsonIgnore]
    public new T Model { get; set; }
    
    public string GetNullstringValue()
    {
        return this.GetNullstringValue();
    }
}