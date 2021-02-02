using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using tinyBank.Core.Config.Extensions;
using tinyBank.Core.Data;
using tinyBank.Core.Model;

namespace tinyBank.app
{
    class Program
    {
        //const string connectionString =
        //    "Server=localhost;Database=tinyBankdb;User Id=sa;Password=admin!@#123;";

        static void Main(string[] args)
        {
            Console.WriteLine("tinyBank initiating...");

            var configuration = new ConfigurationBuilder()
            .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
            .AddJsonFile("appsettings.json", false)
            .Build();

            var config = configuration.ReadAppConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(
                config.TinyBankConnectionString,
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

                //var customerTypes = db.Set<CustomerTypes>().ToList();

                var newCustomer = new Customer()
                {
                    CustomerName = "Kostas3 PLC",
                    CustomerPaymentMethod = PaymentMethod.BankTransfer,
                    CustomerType = CustomerTypes.Merchant
                };

                newCustomer.Accounts.Add(
                    new Account()
                    {
                        AccountBalance = 150
                    });

                newCustomer.Accounts.Add(
                    new Account()
                    {
                        AccountBalance = -350.32M
                    });

                db.Add(newCustomer);
                db.SaveChanges();

                var results = db.Set<Customer>().
                    Where(cust => cust.CustomerPaymentMethod == PaymentMethod.BankTransfer).
                    ToList();

                foreach (var item in results)
                {
                    Console.WriteLine($"Selected: Customer Name {item.CustomerName} with id {item.CustomerId}");
                    foreach (var itemAccount in item.Accounts)
                    {
                        Console.WriteLine($"Selected: Account Balance {itemAccount.AccountBalance} with id {itemAccount.AccountId}");
                    }
                };
            }
        }
    }
}
