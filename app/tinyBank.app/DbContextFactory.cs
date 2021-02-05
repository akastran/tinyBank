using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyBank.Core.Data;
using tinyBank.Core.Config.Extensions;

namespace tinyBank.app
{
    public class DbContextFactory : IDesignTimeDbContextFactory<BankDbContext>
    {
        public BankDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var config = configuration.ReadAppConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<BankDbContext>();

            optionsBuilder.UseSqlServer(
                config.TinyBankConnectionString,
                options => {
                    options.MigrationsAssembly("tinyBank.app");
                });

            return new BankDbContext(optionsBuilder.Options);
        }
    }
}
