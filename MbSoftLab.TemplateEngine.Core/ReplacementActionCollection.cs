using System;
using System.Collections.Generic;

namespace MbSoftLab.TemplateEngine.Core;

class ReplacementActionCollection
{
    private Dictionary<string, Action<string, object>> replacementActions = new Dictionary<string, Action<string, object>>();
   
    public ReplacementActionCollection AddReplacementAction(Type type, Action<string, object> action)
    {
        if (!ReplacementActionForTypeExist(type))
            replacementActions.Add(type.Name, action);

        return this;
    }
   
    public bool ReplacementActionForTypeExist(Type valueType) => replacementActions.ContainsKey(valueType.Name);
   
    public void InvokeReplacementActionForType(Type valueType, string placeholderValueName, object value)
    {
        if (ReplacementActionForTypeExist(valueType))
            replacementActions[valueType.Name].Invoke(placeholderValueName, value);
        else
            throw new NotSupportedException($"Type '{valueType}' not supported by TemplateEngine.createStringFromTemplate().");
    }
}