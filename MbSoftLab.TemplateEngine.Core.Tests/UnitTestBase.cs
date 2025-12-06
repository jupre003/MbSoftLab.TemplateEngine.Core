using MbSoftLab.TemplateEngine.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TemplateEngineCore.Tests
{
    [TestFixture]
    public class UnitTestBase
    {
        public TemplateDataModelDummy GetAWrongTemplateDataModelDummy()
        {
            TemplateDataModelDummy templateDataModelFake = new TemplateDataModelDummy
            {
                DummyStringProp1 = "DummyStringProp1Value",
                DummyIntProp1 = 1,
                DummBoolProp1 = true,
                DummyBoolQProp1 = true,
                DummyDoubleProp1 = 1.75,
                DummyObjectProp1 = new DummyCustomObject(),
                DummyInt64Prop = 1234567,
                DummyUInt16 = 8,
                DummyInt16 = -8,
                DummyDecimalProp = 55,
                DummyCharProp = 'c',
                DummyByteProp = 255,
                DummySByteProp = -5

            };

            return templateDataModelFake;
        }

        public TemplateDataModelDummy GetTemplateDataModelDummy()
        {
            TemplateDataModelDummy templateDataModelFake = new TemplateDataModelDummy
            {
                DummyStringProp1 = "DummyStringProp1Value",
                DummyIntProp1 = 1,
                DummBoolProp1 = true,
                DummyBoolQProp1 = true,
                DummyDoubleProp1 = 1.75,
                DummyDateTimeProp1 = DateTime.Parse("01.01.2020 00:00:00"),
                //DummyObjectProp1 = new DummyCustomObject(),
                DummyInt64Prop = 1234567,
                DummyUInt16 = 8,
                DummyInt16 = -8,
                DummyDecimalProp = 55,
                DummyCharProp = 'c',
                DummyByteProp = 255,
                DummySByteProp = -5

            };
       
                return templateDataModelFake;
        }
        public string GetDummyJson()
        {
            return System.IO.File.ReadAllText("DummyJsonData.json");
        }
        public TemplateDataModelDummyWithList GetTemplateDataModelDummyWithListAndMethod()
        {
            TemplateDataModelDummyWithList templateDataModelFake = new TemplateDataModelDummyWithList
            {
                DummyStringProp1 = "DummyStringProp1Value",
                DummyStringListProp2 = new List<string>() { "asd", "asd" }

            };

            return templateDataModelFake;
        }
        public TemplateDataModelDummyWithMethods GetTemplateDataModelDummyWithMethods()
        {
            TemplateDataModelDummyWithMethods templateDataModelFake = new TemplateDataModelDummyWithMethods();

            return templateDataModelFake;
        }
    }
}