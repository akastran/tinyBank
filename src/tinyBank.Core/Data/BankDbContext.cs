using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyBank.Core.Model;

namespace tinyBank.Core.Data
{
    public class BankDbContext : DbContext
    {
        //var configuration = new ConfigurationBuilder()
        //        .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
        //        .AddJsonFile("appsettings.json", false)
        //        .Build();

        //var config = configuration.ReadAppConfiguration();

        public BankDbContext(
            DbContextOptions<BankDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .ToTable("Customer");

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.CustomerId)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .ToTable("Account");

            modelBuilder.Entity<Account>()
                .HasIndex(c => c.AccountId)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .ToTable("Transaction");

            modelBuilder.Entity<Transaction>()
                .HasIndex(c => c.TransactionId)
                .IsUnique();

            modelBuilder.Entity<CustomerTypes>()
                .ToTable("CustomerTypes");

            modelBuilder.Entity<CustomerTypes>()
                .HasIndex(c => c.CustomerTypeName)
                .IsUnique();
        }
    }
}
