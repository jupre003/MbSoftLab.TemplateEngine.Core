using MbSoftLab.TemplateEngine.Core;

namespace TemplateEngineCore.Tests
{
    public class TemplateDataModelDummyWithMethods:TemplateDataModel<TemplateDataModelDummyWithMethods>
    {

        public string StringReturningMethod()
        {
            return "StringReturnValue";
        }
        public string StringReturningMethod(string parameter)
        {
            return parameter;
        }
        public bool BoolReturningMethod()
        {
            return true;
        }
        public int IntReturningMethod()
        {
            return 12;
        }
        public double DoubleReturningMethod()
        {
            return 1.2;
        }
        private double PrivateDoubleReturningMethod() // Test if has a effect
        {
            return 1.2;
        }


    }
}
