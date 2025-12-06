using System;
using System.Globalization;

namespace MbSoftLab.TemplateEngine.Core;

interface IPlaceholderValueRaplacer
{
    void ReplacePlaceholderWithValue(Type valueType, string placeholderValueName, object value);
    string OutputString { get; }
    CultureInfo CultureInfo { get; set; }
}