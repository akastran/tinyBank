using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using tinyBank.Core.Data;
using tinyBank.Core.Model;

namespace tinyBank.app
{
    class Program
    {
        const string connectionString =
            "Server=localhost;Database=tinyBankdb;User Id=sa;Password=admin!@#123;";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var optionsBuilder = new DbContextOptionsBuilder<BankDbContext>();
            optionsBuilder.UseSqlServer(
                connectionString,
                options =>
                {
                    options.MigrationsAssembly("tinyBank.app");
                });

            using (var db = new BankDbContext(optionsBuilder.Options))
            {
                //db.Add(new Customer 
                //{ 
                //    CustomerName = "Andreas", 
                //    CustomerPaymentMethod = PaymentMethod.BankTransfer, 
                //    CustomerType = "Personal" 
                //});
                //db.Add(new Customer 
                //{
                //    CustomerName = "Michael",
                //    CustomerPaymentMethod = PaymentMethod.BankTransfer,
                //    CustomerType = "Personal"
                //});
                //db.Add(new Customer
                //{
                //    CustomerName = "Michael",
                //    CustomerPaymentMethod = PaymentMethod.Card,
                //    CustomerType = "Personal"
                //});
                //db.SaveChanges();

                List<Customer> results = db.Set<Customer>().
                    Where(cust => cust.CustomerPaymentMethod == PaymentMethod.BankTransfer).
                    ToList();

                foreach (Customer item in results)
                {
                    Console.WriteLine($"Selected: Customer Name {item.CustomerName} with id {item.CustomerId}");
                };
            }
        }
    }
}
