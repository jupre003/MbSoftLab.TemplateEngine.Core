using MbSoftLab.TemplateEngine.Core;
using System;

namespace TemplateEngineCore.Tests
{
    public class TemplateDataModelDummy : TemplateDataModel<TemplateDataModelDummy>
    {
        public string DummyStringProp1 { get; set; }
        public string DummyStringProp2 { get; set; }
        public DateTime DummyDateTimeProp1 { get; set; }
        public int DummyIntProp1 { get; set; }
        public int DummyIntProp2 { get; set; }
        public Int64 DummyInt64Prop { get; set; }
        public UInt16 DummyUInt16 { get; set; }
        public Int16 DummyInt16 { get; set; }
        public Decimal DummyDecimalProp { get; set; }
        public byte DummyByteProp { get; set; }
        public sbyte DummySByteProp { get; set; }
        public char DummyCharProp { get; set; }
        public double DummyDoubleProp1 { get; set; }
        public double DummyDoubleProp2 { get; set; }
        public bool DummBoolProp1 { get; set; }
        public bool DummyBoolProp2 { get; set; }
        public bool? DummyBoolQProp1 { get; set; }
        public bool? DummyBoolQProp2 { get; set; }
        public object DummyObjectProp1 { get; set; }
        public object DummyObjectProp2 { get; set; }
    }
}