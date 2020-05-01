using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DistSysLab7
{
    class Program
    {
        static void Main()
        {
            #region add manual entry
            //using (var ctx = new Lab7Context())
            //{
            //    Address addr = new Address() { House_Name_or_Number = "1076", Street = "Some Street", City = "Some City", County = "Some County", Country = "UK", Postcode = "Some Postcode", People = new List<Person>() };
            //    BankAccount accnt = new BankAccount() { Balance = 50.0m };
            //    Person prsn = new Person() { First_Name = "Jane", Last_Name = "Doe", Date_of_Birth = new DateTime(2010, 10, 1), Age = 40, Address = addr, BankAccount = accnt };

            //    ctx.Addresses.Add(addr);
            //    ctx.BankAccounts.Add(accnt);
            //    ctx.People.Add(prsn);

            //    ctx.SaveChanges();
            //}
            #endregion

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    using (var ctx = new Lab7Context())
                    {
                        Person prsn = ctx.People.First();
                        decimal balance = prsn.BankAccount.Balance;
                        decimal balancechange;
                        do
                        {
                            Console.WriteLine("Enter a balance modifier");
                        }
                        while (!decimal.TryParse(Console.ReadLine(), out balancechange));
                        balance += balancechange;
                        prsn.BankAccount.Balance = balance;
                        ctx.SaveChanges();
                        return;
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("Oh no! Looks like the database was modified whilst you were making your change.Try again.");
                }
            }
            Console.WriteLine("Data access failed three times - perhaps try again later.");
        }
    }
}