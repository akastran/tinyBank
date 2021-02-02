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
            Console.WriteLine("tinyBank initiating...");

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

                //db.Add(new CustomerTypes
                //{
                //    CustomerTypeName = "Personal"
                //});

                //db.Add(new CustomerTypes
                //{
                //    CustomerTypeName = "Merchant"
                //});

                //db.SaveChanges();

                List<CustomerTypes> customerTypes = db.Set<CustomerTypes>().ToList();

                var newCustomer = new Customer()
                {
                    CustomerName = "Kostas PLC",
                    CustomerPaymentMethod = PaymentMethod.BankTransfer,
                    CustomerType = customerTypes.Where(c => c.CustomerTypeName == "Merchant").ToString()
                };

                newCustomer.Accounts.Add(
                    new Account()
                    {
                        AccountBalance = 100
                    });

                newCustomer.Accounts.Add(
                    new Account()
                    {
                        AccountBalance = (decimal)-300.32
                    });

                db.Add(newCustomer);
                db.SaveChanges();

                List<Customer> results = db.Set<Customer>().
                    Where(cust => cust.CustomerPaymentMethod == PaymentMethod.BankTransfer).
                    ToList();

                foreach (Customer item in results)
                {
                    Console.WriteLine($"Selected: Customer Name {item.CustomerName} with id {item.CustomerId}");
                    foreach (Account itemAccount in item.Accounts)
                    {
                        Console.WriteLine($"Selected: Account Blance {itemAccount.AccountBalance} with id {itemAccount.AccountId}");
                    }
                };
            }
        }
    }
}
