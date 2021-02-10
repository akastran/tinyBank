using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyBank.Core.Config;
using tinyBank.Core.Config.Extensions;
using tinyBank.Core.Data;

namespace tinyBank.Core.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(
            this IServiceCollection @this, IConfiguration configuration)
        {
            @this.AddSingleton<AppConfig>(
                configuration.ReadAppConfiguration());

            @this.AddDbContext<BankDbContext>(
                 (serviceProvider, optionsBuilder) => {
                     var appConfig = serviceProvider.GetRequiredService<AppConfig>();

                     optionsBuilder.UseSqlServer(appConfig.TinyBankConnectionString);
                 });

            @this.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
