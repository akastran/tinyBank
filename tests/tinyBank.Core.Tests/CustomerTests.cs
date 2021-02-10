using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using tinyBank.Core.Model;
using tinyBank.Core.Services;
using Xunit;

namespace tinyBank.Core.Tests
{
    public class CustomerTests : IClassFixture<TinyBankFixture>
    {
        private ICustomerService _customers;
        private Data.BankDbContext _dbContext;

        public CustomerTests(TinyBankFixture fixture)
        {
            _customers = fixture.Scope.ServiceProvider
                .GetRequiredService<ICustomerService>();
            _dbContext = fixture.Scope.ServiceProvider
                .GetRequiredService<Data.BankDbContext>();
        }

        [Fact]
        public void Add_Customer()
        {
            var options = new Services.Options.RegisterCustomerOptions()
            {
                CustomerName = "Kitsos",
                //AFM = $"{System.Guid.NewGuid()}",
                AFM = System.Guid.NewGuid().ToString().Substring(0,9),
                CustomerType = CustomerType.Personal,
                CustomerPaymentMethod = PaymentMethod.BankTransfer
            };

            var customer = _customers.Register(options);

            Assert.NotNull(customer);

            var savedCustomer = _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == customer.CustomerId)
                .SingleOrDefault();
            
            Assert.NotNull(savedCustomer);
            Assert.Equal(options.CustomerName, savedCustomer.CustomerName);
            Assert.Equal(options.AFM, savedCustomer.AFM);
            Assert.Equal(options.CustomerType, savedCustomer.CustomerType);
            Assert.Equal(options.CustomerPaymentMethod, savedCustomer.CustomerPaymentMethod);
        }
    }
}
