using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkLab
{
    class Address
    {
        public string House_Name_or_Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public ICollection<Person> People { get; set; }
        public Address() { }
    }
}
