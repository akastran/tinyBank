using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task Add_CustomerAsync()
        {
            var options = new Services.Options.RegisterCustomerOptions()
            {
                CustomerName = "John2",
                //AFM = $"{System.Guid.NewGuid()}",
                AFM = System.Guid.NewGuid().ToString().Substring(0,9),
                CustomerType = CustomerType.Merchant,
                CustomerPaymentMethod = PaymentMethod.BankTransfer
            };

            var customer = await _customers.RegisterCustomerAsync(options);

            Assert.NotNull(customer);

            var savedCustomer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == customer.CustomerId)
                .SingleOrDefaultAsync();
            
            Assert.NotNull(savedCustomer);
            Assert.Equal(options.CustomerName, savedCustomer.CustomerName);
            Assert.Equal(options.AFM, savedCustomer.AFM);
            Assert.Equal(options.CustomerType, savedCustomer.CustomerType);
            Assert.Equal(options.CustomerPaymentMethod, savedCustomer.CustomerPaymentMethod);
        }

        [Fact]
        public async Task Get_CustomerAsync()
        {
            var options = new Services.Options.RetrieveCustomerOptions()
            {
                CustomerId = 3
            };

            var customer = await _customers.RetrieveCustomerAsync(options);

            Assert.NotNull(customer);

            Console.WriteLine(customer.CustomerId);

            var savedCustomer = _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == customer.CustomerId)
                .SingleOrDefault();

            Assert.NotNull(savedCustomer);

            Console.WriteLine(savedCustomer.CustomerId);
        }

        [Fact]
        public async Task Change_CustomerAsync()
        {
            var options = new Services.Options.UpdateCustomerOptions()
            {
                CustomerId = 3,
                CustomerName = "Manolis"
                //AFM = $"{System.Guid.NewGuid()}",
                //AFM = System.Guid.NewGuid().ToString().Substring(0, 9),
                //CustomerType = CustomerType.Personal,
                //CustomerPaymentMethod = PaymentMethod.BankTransfer
            };

            var customer = await _customers.UpdateCustomerAsync(options);

            Assert.NotNull(customer);

            var savedCustomer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == customer.CustomerId)
                .SingleOrDefaultAsync();

            Assert.NotNull(savedCustomer);
            Assert.Equal(options.CustomerName, savedCustomer.CustomerName);
            //Assert.Equal(options.AFM, savedCustomer.AFM);
            //Assert.Equal(options.CustomerType, savedCustomer.CustomerType);
            //Assert.Equal(options.CustomerPaymentMethod, savedCustomer.CustomerPaymentMethod);
        }

        [Fact]
        public async Task Delete_CustomerAsync()
        {
            var options = new Services.Options.DeleteCustomerOptions()
            {
                CustomerName = "John2"
            };

            var customer = await _customers.DeleteCustomerAsync(options);

            Assert.NotNull(customer);

            var savedCustomer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerName == customer.CustomerName)
                .SingleOrDefaultAsync();

            Assert.Null(savedCustomer);
        }
    }
}
