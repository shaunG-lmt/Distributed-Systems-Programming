using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DistSysLab7
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Date_of_Birth { get; set; }
        virtual public Address Address { get; set; }
        virtual public BankAccount BankAccount { get; set; }
        public int Age { get; set; }
        public Person() { }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
