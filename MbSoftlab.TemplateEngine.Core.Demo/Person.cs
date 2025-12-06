using System.Collections.Generic;

namespace MbSoftLab.TemplateEngine.Core.Demo
{
    public class Person : TemplateDataModel<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Tags { get; set; }
        public Address Address { get; set; }
        public List<Order> Orders { get; set; }
    }
}
