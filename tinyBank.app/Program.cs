using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
                db.Add(new Customer { 
                    //CustomerId = 1, 
                    CustomerName = "Andreas", 
                    CustomerPaymentMethod = PaymentMethod.BankTransfer, 
                    CustomerType = "Personal" });
                db.SaveChanges();
            }
        }
    }
}
