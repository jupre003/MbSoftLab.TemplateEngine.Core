using System.Text.Json.Serialization;

namespace MbSoftLab.TemplateEngine.Core;

public class TemplateDataModel<T>
{
    [JsonIgnore]
    public T Model { get; set; }
    
    public string GetNullstringValue()
    {
        return this.GetNullstringValue();
    }
}