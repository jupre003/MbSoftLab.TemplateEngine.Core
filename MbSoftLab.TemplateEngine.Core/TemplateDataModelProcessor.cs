using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MbSoftLab.TemplateEngine.Core;

class TemplateDataModelProcessor
{
    string _openingDelimiter, _closeingDelimiter;
    private List<String> _methodBlacklist = new List<string>() { "ToString", "GetType", "Equals", "GetHashCode" };

    IPlaceholderValueRaplacer _placeholderValueRaplacer;

    private void AddMethodsFromTemplateDataModelBaseClassToBlacklist()
    {
        new TemplateDataModel<object>().GetType()
             .GetMethods().ToList()
             .ForEach(method => _methodBlacklist.Add(method.Name));
    }
    public TemplateDataModelProcessor(string openingDelimiter, string closeingDelimiter, IPlaceholderValueRaplacer placeholderValueRaplacer)
    {
        _openingDelimiter = openingDelimiter;
        _closeingDelimiter = closeingDelimiter;
        _placeholderValueRaplacer = placeholderValueRaplacer;
        AddMethodsFromTemplateDataModelBaseClassToBlacklist();
    }
    public void ProcessTemplateDataModell(object templateDataModel)
    {
        ProcessTemplateDataModelClassMethods(templateDataModel);
        ProcessTemplateDataModelClassPropertys(templateDataModel);
    }
    private void ProcessTemplateDataModelClassMethods(object templateDataModel)
    {
        Type t = templateDataModel.GetType();
        List<MethodInfo> methodInfos = t.GetMethods().Where(mi => mi.IsSpecialName == false
                                                        && !_methodBlacklist.Contains(mi.Name)
                                                        ).ToList();

        foreach (MethodInfo methodInfo in methodInfos)
        {
            if (methodInfo.IsPublic)
            {
                var parameters = methodInfo.GetParameters();
                if (parameters.Count() > 0) continue; // TODO: Methoden mit Parameter unterstüzen. Dazu über Methodenname die angegebenen Parameter in der HTML file auslesen

                var instance = Activator.CreateInstance(t);
                object methodValue = methodInfo.Invoke(instance, parameters);
                Type methodValueType = methodInfo.ReturnType;
                string methodName = _openingDelimiter + methodInfo.Name + "()" + _closeingDelimiter;
                try
                {
                    _placeholderValueRaplacer.ReplacePlaceholderWithValue(methodValueType, methodName, methodValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
    private void ProcessTemplateDataModelClassPropertys(object templateDataModel)
    {
        Type t = templateDataModel.GetType();
        PropertyInfo[] propertyInfos = t.GetProperties();

        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            if ((propertyInfo.CanRead))
            {
                object propertyValue = propertyInfo.GetValue(templateDataModel, null);
                string propertyName = _openingDelimiter + propertyInfo.Name + _closeingDelimiter;
                Type propertyValueType = null;

                if (propertyValue != null) propertyValueType = propertyValue?.GetType();
                else propertyValueType = typeof(string);
                try
                {
                    _placeholderValueRaplacer.ReplacePlaceholderWithValue(propertyValueType, propertyName, propertyValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}