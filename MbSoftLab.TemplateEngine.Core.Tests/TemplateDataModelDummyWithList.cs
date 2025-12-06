using MbSoftLab.TemplateEngine.Core;
using System.Collections.Generic;

namespace TemplateEngineCore.Tests
{
    public class TemplateDataModelDummyWithList:TemplateDataModel<TemplateDataModelDummyWithList>
    {
        public string DummyStringProp1 { get; set; }
        public List<string> DummyStringListProp2 { get; set; }
        public IList<string> DummyStringIListProp { get; set; }
        public Dictionary<string, int> DummyStringDicProp { get; set; }
        public IDictionary<string, int> DummyStringIDicProp { get; set; }

        public IEnumerable<string> DummyStringIEnumerableProp { get; set; }

        public string StringReturningMethod()
        {
            return "StringReturnValue";
        }

    }
}
