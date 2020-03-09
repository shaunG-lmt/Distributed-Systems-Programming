using System;
using System.Collections.Generic;

namespace EntityFrameworkLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new MyContext())
            {
                Address addr = new Address()
                {
                    House_Name_or_Number = "64",
                    Street = "Jeff Street",
                    City = "England",
                    Postcode = "JE21FF",
                    Country = "England",

                    People = new List<Person>()
                };

                Person prsn = new Person()
                {
                    First_Name = "Jeff",
                    Middle_Names = "Is",
                    Last_Name = "Name",
                    Date_of_Birth = new DateTime(1960, 12, 21),
                    Address = addr
                };
                ctx.Addresses.Add(addr);
                ctx.People.Add(prsn);
                ctx.SaveChanges();
            }
        }
    }
}
