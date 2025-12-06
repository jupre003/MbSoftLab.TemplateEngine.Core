using MbSoftLab.TemplateEngine.Core;
using NUnit.Framework;
using System;
using System.Globalization;
using System.IO;

namespace TemplateEngineCore.Tests
{
    [TestFixture]
    public class RazorTemplateEngineUnitTest : UnitTestBase
    {
        [Test]
        public void can_create_a_valid_string_from_template()
        {
            //Arrange  
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>@Model.DummyStringProp1</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string expectedReturnString = "<TagName>DummyStringProp1Value</TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate();

            //Assert 
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [Test]
        public void can_create_a_valid_string_from_template_with_json()
        {
            //Arrange  
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(); //SUT = [S]ystem [U]nder [T]est
            sut.TemplateString = "<TagName>@Model.DummyStringProp1</TagName>";
            string expectedReturnString = "<TagName>DummyStringProp1Value</TagName>";
            string jsonData = GetDummyJson();

            //Act 
            string returnString = sut.CreateStringFromTemplateWithJson(jsonData);

            //Assert 
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [Test]
        public void can_handle_null_Values_in_Propertys()
        {
            //Arrange
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>@Model.DummyStringProp2</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string expectedReturnString = "<TagName></TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate();

            //Assert
            Assert.AreEqual(expectedReturnString, returnString);
        }
        //[Test] //ToDo: Handle Custom NullValueString for RazorTemplateEngine
        public void can_set_a_custom_null_value_String()
        {
            //Arrange 
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>@Model.DummyStringProp2.GetNullstringValue()</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "Nothing";
            string expectedReturnString = "<TagName>Nothing</TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate();

            //Assert
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [Test]
        public void can_set_a_template()
        {
            //Arrange
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>@Model.DummyStringProp2</TagName>"); //SUT = [S]ystem [U]nder [T]est
            sut.NullStringValue = "";
            sut.TemplateString = "<MyTag>@Model.DummyStringProp2</MyTag>";
            string expectedReturnString = "<MyTag></MyTag>";

            //Act
            string returnString = sut.CreateStringFromTemplate();

            //Assert
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [Test]
        public void can_use_the_config()
        {
            //Arrange   
            TemplateEngineConfig<TemplateDataModelDummy> templateEngineConfig = new TemplateEngineConfig<TemplateDataModelDummy>()
            {
                OpeningDelimiter = "{{",
                CloseingDelimiter = "}}",
                NullStringValue = "---",
                TemplateDataModel = GetTemplateDataModelDummy(),
                TemplateString = "<TagName>@Model.DummyStringProp2</TagName>"
            };
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(); //SUT = [S]ystem [U]nder [T]est
            sut.Config = templateEngineConfig;

            string expectedReturnString = "<TagName></TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate();

            //Assert 
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [Test]
        public void can_use_the_generic_config()
        {
            //Arrange 
            TemplateEngineConfig<TemplateDataModelDummy> templateEngineConfig = new TemplateEngineConfig<TemplateDataModelDummy>()
            {
                OpeningDelimiter = "{{",
                CloseingDelimiter = "}}",
                NullStringValue = "",
                TemplateDataModel = GetTemplateDataModelDummy(),
                TemplateString = "<TagName>@Model.DummyStringProp2</TagName>"
            };
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>();
            sut.Config = templateEngineConfig;

            string expectedReturnString = "<TagName></TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate();

            //Assert
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [Test]
        public void can_set_a_template_and_model_on_creating()
        {
            //Arrange
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>();
            sut.NullStringValue = "";
            string expectedReturnString = "<TagName></TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate(GetTemplateDataModelDummy(), "<TagName>@Model.DummyStringProp2</TagName>");

            //Assert 
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [Test]
        public void can_set_a_model_on_creating()
        {
            //Arrange 
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>();
            sut.NullStringValue = "";
            sut.TemplateString = "<TagName>@Model.DummyStringProp2</TagName>";
            string expectedReturnString = "<TagName></TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate(GetTemplateDataModelDummy());

            //Assert
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [Test]
        public void throws_excepton_if_fileLoading_failed()
        {
            //Arrange  
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>@Model.DummyObjectProp1</TagName>"); //SUT = [S]ystem [U]nder [T]est

            //Assert 
            Assert.Throws<FileNotFoundException>(delegate
            {
                //Act
                sut.LoadTemplateFromFile("NonExistingFile.txt");
            });
        }
        [Test]
        public void can_handle_a_DataModel_with_a_list_property()
        {
            //Arrange 
            var sut = new RazorTemplateEngine<TemplateDataModelDummyWithList>(GetTemplateDataModelDummyWithListAndMethod(), "<TagName>@Model.DummyStringListProp2</TagName>"); //SUT = [S]ystem [U]nder [T]est
            //Act / Assert 
            Assert.DoesNotThrow(delegate { sut.CreateStringFromTemplate(); });
        }
        [Test]
        public void can_handle_a_Method_in_DataModel_without_Exeption()
        {
            //Arrange   
            var sut = new RazorTemplateEngine<TemplateDataModelDummyWithList>(GetTemplateDataModelDummyWithListAndMethod(), "<TagName>@Model.StringReturningMethod()</TagName>"); //SUT = [S]ystem [U]nder [T]est
            Assert.DoesNotThrow(delegate { sut.CreateStringFromTemplate(); });
        }
        [Test]
        [TestCase("StringReturningMethod()", "StringReturnValue")]
        [TestCase("BoolReturningMethod()", "True")]
        public void can_handle_return_values_from_a_method(string methodName, string returnValue)
        {
            //Arrange 
            var sut = new RazorTemplateEngine<TemplateDataModelDummyWithMethods>(GetTemplateDataModelDummyWithMethods(), "<TagName>@Model." + methodName + "</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string expectedReturnString = "<TagName>" + returnValue + "</TagName>";

            //Act
            string returnString = sut.CreateStringFromTemplate();

            //Assert 
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [TestCase]
        public void can_handle_return_values_from_IntReturningMethod()
        {
            //Arrange 
            string methodName = "IntReturningMethod()";
            string returnValue = Convert.ToString(Convert.ToInt32("12"));
            var sut = new RazorTemplateEngine<TemplateDataModelDummyWithMethods>(GetTemplateDataModelDummyWithMethods(), "<TagName>@Model." + methodName + "</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string expectedReturnString = "<TagName>" + returnValue + "</TagName>";

            //Act
            string returnString = sut.CreateStringFromTemplate();

            //Assert 
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [TestCase]
        public void can_handle_return_values_from_DoubleReturningMethod()
        {
            //Arrange 
            string methodName = "DoubleReturningMethod()";
            double value = 1.2;
            string returnValue = Convert.ToString(value);
            var sut = new RazorTemplateEngine<TemplateDataModelDummyWithMethods>(GetTemplateDataModelDummyWithMethods(), "<TagName>@Model." + methodName + "</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string expectedReturnString = "<TagName>" + returnValue + "</TagName>";

            //Act
            string returnString = sut.CreateStringFromTemplate();

            //Assert 
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [Test]
        [TestCase("DummyByteProp", "255")]
        [TestCase("DummySByteProp", "-5")]
        [TestCase("DummyCharProp", "c")]
        [TestCase("DummyUInt16", "8")]
        [TestCase("DummyInt16", "-8")]
        [TestCase("DummyIntProp1", "1")]
        [TestCase("DummyInt64Prop", "1234567")]
        [TestCase("DummyDecimalProp", "55")]
        [TestCase("DummBoolProp1", "True")]
        [TestCase("DummyBoolQProp1", "True")]// Bool With Null 
        [TestCase("DummyBoolProp2", "False")]
        [TestCase("DummyBoolQProp2", "")]
        //[TestCase("DummyDoubleProp1", "1.75")]
        public void can_handle_values_from_propertys(string propertyName, string expectedOutput)
        {
            //Arrange 
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>@Model." + propertyName + "</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string expectedReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate();

            //Assert  
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [TestCase]
        public void can_handle_double_values_from_propertys()
        {
            //Arrange   
            string propertyName = "DummyDoubleProp1";
            double value = 1.75;
            string expectedOutput = Convert.ToString(value);
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>@Model." + propertyName + "</TagName>"); //SUT = [S]ystem [U]nder [T]est
            string expectedReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate();

            //Assert 
            Assert.AreEqual(expectedReturnString, returnString);
        }
        [TestCase]
        public void can_handle_date_values_from_propertys()
        {
            string propertyName = "DummyDateTimeProp1";
            string expectedOutput = Convert.ToString(Convert.ToDateTime("01.01.2020 00:00:00"));

            //Arrange -> Vorbereiten der Testumgebung und der benötigten Prameter   
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>@Model." + propertyName + "</TagName>"); //SUT = [S]ystem [U]nder [T]est

            string expectedReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act Ausführen der zu testenden Funktion
            string returnString = sut.CreateStringFromTemplate();

            //Assert Prüfen der Ergebnisse 
            Assert.AreEqual(expectedReturnString, returnString);
        }
        //[TestCase] ToDo: Add Support for Change Culture info
        public void can_create_and_use_SpecificCulture()
        {
            //Arrange   
            string propertyName = "DummyDoubleProp1";
            double value = 1.75;
            string expectedOutput = Convert.ToString(value);
            var sut = new RazorTemplateEngine<TemplateDataModelDummy>(GetTemplateDataModelDummy(), "<TagName>@Model." + propertyName + "</TagName>");
            sut.CultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            string expectedReturnString = "<TagName>" + expectedOutput + "</TagName>";

            //Act 
            string returnString = sut.CreateStringFromTemplate();

            //Assert 
            Assert.AreEqual(expectedReturnString, returnString);
        }
    }
}