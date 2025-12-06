using System;
using System.Globalization;

namespace MbSoftLab.TemplateEngine.Core;

class PlaceholderValueRaplacer : IPlaceholderValueRaplacer
{
    string _outputString;
    readonly string _nullStringValue;
    public string OutputString => _outputString;
    public CultureInfo CultureInfo { get; set; } = CultureInfo.CreateSpecificCulture("en-US");

    private ReplacementActionCollection _replacementActionCollection = new ReplacementActionCollection();
    public PlaceholderValueRaplacer(string outputString, string nullStringValue)
    {
        _outputString = outputString;
        _nullStringValue = nullStringValue;
        RegisterReplacementActions();
    }
    private void RegisterReplacementActions()
    {
        _replacementActionCollection
            .AddReplacementAction(typeof(string), (placeholderValueName, value) => ReplaceNullableStringValueInOutputString(placeholderValueName, (string)value))
            .AddReplacementAction(typeof(byte), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (byte)value))
            .AddReplacementAction(typeof(short), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (short)value))
            .AddReplacementAction(typeof(ushort), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (ushort)value))
            .AddReplacementAction(typeof(long), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (long)value))
            .AddReplacementAction(typeof(ulong), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (ulong)value))
            .AddReplacementAction(typeof(sbyte), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (sbyte)value))
            .AddReplacementAction(typeof(char), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (char)value))
            .AddReplacementAction(typeof(UInt16), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (UInt16)value))
            .AddReplacementAction(typeof(UInt32), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (UInt32)value))
            .AddReplacementAction(typeof(UInt64), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (UInt64)value))
            .AddReplacementAction(typeof(Int16), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (Int16)value))
            .AddReplacementAction(typeof(Int32), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (Int32)value))
            .AddReplacementAction(typeof(Int64), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (Int64)value))
            .AddReplacementAction(typeof(Decimal), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (Decimal)value))
            .AddReplacementAction(typeof(Double), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (double)value))
            .AddReplacementAction(typeof(DateTime), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, (DateTime)value))
            .AddReplacementAction(typeof(Boolean), (placeholderValueName, value) => ReplaceValueInOutputString(placeholderValueName, ((bool)value)));
    }
    private void ReplaceValueInOutputString(string placeholderValueName, object value)
    {
        string replacementForCulture = Convert.ToString(value, CultureInfo);
        _outputString = _outputString.Replace(placeholderValueName, replacementForCulture);
    }
    private void ReplaceNullableStringValueInOutputString(string placeholderValueName, object value)
    {
        if (value == null)
            _outputString = _outputString.Replace(placeholderValueName, _nullStringValue);
        else
            _outputString = _outputString.Replace(placeholderValueName, (String)value);
    }
    public void ReplacePlaceholderWithValue(Type valueType, string placeholderValueName, object value)
    {
        if (IsNoCollection(valueType))
            _replacementActionCollection.InvokeReplacementActionForType(valueType, placeholderValueName, value);
    }

    private bool IsNoCollection(Type valueType) => valueType.FullName.Contains("System.Collections.Generic") == false;
}