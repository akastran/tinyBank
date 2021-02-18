using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tinyBank.Core.Constants;
using tinyBank.Core.Model;
using tinyBank.Core.Services;
using Xunit;

namespace tinyBank.Core.Tests
{
    public class CustomerTests : IClassFixture<TinyBankFixture>
    {
        private ICustomerService _customer;
        private Data.BankDbContext _dbContext;

        public CustomerTests(TinyBankFixture fixture)
        {
            _customer = fixture.Scope.ServiceProvider
                .GetRequiredService<ICustomerService>();
            _dbContext = fixture.Scope.ServiceProvider
                .GetRequiredService<Data.BankDbContext>();
        }

        [Fact]
        public async Task Add_CustomerAsync()
        {
            var options = new Services.Options.RegisterCustomerOptions()
            {
                CustomerName = "John",
                //AFM = $"{System.Guid.NewGuid()}",
                AFM = System.Guid.NewGuid().ToString().Substring(0,9),
                CustomerType = CustomerType.Personal,
                CustomerPaymentMethod = PaymentMethod.BankTransfer,
                TotalGross = 0M
            };

            var customer = await _customer.RegisterCustomerAsync(options);

            Assert.NotNull(customer);

            var savedCustomer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == customer.CustomerId)
                .SingleOrDefaultAsync();
            
            Assert.NotNull(savedCustomer);
            Assert.Equal(options.CustomerName, savedCustomer.CustomerName);
            Assert.Equal(options.AFM, savedCustomer.AFM);
            Assert.Equal(options.CustomerType, savedCustomer.CustomerType);
            Assert.Equal(options.CustomerPaymentMethod, savedCustomer.CustomerPaymentMethod);
            Assert.Equal(options.TotalGross, savedCustomer.TotalGross);
        }

        [Fact]
        public async Task Get_CustomerAsync()
        {
            var options = new Services.Options.RetrieveCustomerOptions()
            {
                CustomerId = 3
            };

            var customer = await _customer.RetrieveCustomerAsync(options);

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

            var customer = await _customer.UpdateCustomerAsync(options);

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

            var customer = await _customer.DeleteCustomerAsync(options);

            Assert.NotNull(customer);

            var savedCustomer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerName == customer.CustomerName)
                .SingleOrDefaultAsync();

            Assert.Null(savedCustomer);
        }

        [Fact]
        public void ParseExcelFile_Success()
        {
            var result = _customer.ParseFile(
                @"C:\Users\E40141\source\repos\tinyBank\tests\tinyBank.Core.Tests\bin\Debug\net5.0\Files\Book1.xlsx");

            Assert.Equal(ResultCode.Success, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);

            var customer = result.Data[0];
            Assert.Equal("Dimitris", customer.CustomerName);
            Assert.Equal("987654321", customer.AFM);
            Assert.Equal(1500.33M, customer.TotalGross);
            Assert.Equal(CustomerType.Personal, customer.CustomerType);
            Assert.Equal(PaymentMethod.Card, customer.CustomerPaymentMethod);

            customer = result.Data[1];
            Assert.Equal("Andreas", customer.CustomerName);
            Assert.Equal("123456789", customer.AFM);
            Assert.Equal(30.75M, customer.TotalGross);
            Assert.Equal(CustomerType.Personal, customer.CustomerType);
            Assert.Equal(PaymentMethod.Card, customer.CustomerPaymentMethod);
        }

        [Fact]
        public async Task ParseExcelFile_And_Register_Customer_SuccessAsync()
        {
            var result = _customer.ParseFile(
                @"C:\Users\E40141\source\repos\tinyBank\tests\tinyBank.Core.Tests\bin\Debug\net5.0\Files\Book1.xlsx");

            Assert.Equal(ResultCode.Success, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);

            var customer = result.Data[0];
            Assert.Equal("Dimitris", customer.CustomerName);
            Assert.Equal("987654321", customer.AFM);
            Assert.Equal(1500.33M, customer.TotalGross);
            Assert.Equal(CustomerType.Personal, customer.CustomerType);
            Assert.Equal(PaymentMethod.Card, customer.CustomerPaymentMethod);

            customer = result.Data[1];
            Assert.Equal("Andreas", customer.CustomerName);
            Assert.Equal("123456789", customer.AFM);
            Assert.Equal(30.75M, customer.TotalGross);
            Assert.Equal(CustomerType.Personal, customer.CustomerType);
            Assert.Equal(PaymentMethod.Card, customer.CustomerPaymentMethod);

            var newCustomer = new Customer();
            var savedCustomer = new Customer();

            foreach (var item in result.Data)
            {
                var options = new Services.Options.RegisterCustomerOptions()
                {
                    CustomerName = item.CustomerName,
                    AFM = item.AFM,
                    CustomerType = item.CustomerType,
                    CustomerPaymentMethod = item.CustomerPaymentMethod,
                    TotalGross = item.TotalGross
                };

                newCustomer = await _customer.RegisterCustomerAsync(options);

                Assert.NotNull(newCustomer);

                savedCustomer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerName == newCustomer.CustomerName)
                .SingleOrDefaultAsync();

                Assert.NotNull(savedCustomer);
                Assert.Equal(options.CustomerName, savedCustomer.CustomerName);
                Assert.Equal(options.AFM, savedCustomer.AFM);
                Assert.Equal(options.CustomerType, savedCustomer.CustomerType);
                Assert.Equal(options.CustomerPaymentMethod, savedCustomer.CustomerPaymentMethod);
                Assert.Equal(options.TotalGross, savedCustomer.TotalGross);
            }
        }

        [Fact]
        public async Task ExportExcel_SuccessAsync()
        {
            var options = new Services.Options.RetrieveCustomerOptions()
            {
                CustomerId = 5
            };

            var customer = await _customer.RetrieveCustomerAsync(options);

            Assert.NotNull(customer);

            var newCustomerList = new List<Customer>();
            newCustomerList.Add(customer);

            var path = @"C:\Users\E40141\source\repos\tinyBank\tests\tinyBank.Core.Tests\bin\Debug\net5.0\Files\Book2.xlsx";
            var result = _customer.ExportCustomerFileAsync(
                newCustomerList,
                path);
        }
    }
}
