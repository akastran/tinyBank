using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyBank.Core.Data;
using tinyBank.Core.Model;
using tinyBank.Core.Services.Options;

namespace tinyBank.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private BankDbContext _dbContext;

        public CustomerService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> RegisterCustomerAsync(RegisterCustomerOptions options)
        {
            if (string.IsNullOrWhiteSpace(options?.CustomerName))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options?.AFM))
            {
                return null;
            }

            if (options.AFM.Length != 9)
            {
                return null;
            }

            if (options?.CustomerType == CustomerType.Undefined)
            {
                return null;
            }

            if (options?.CustomerPaymentMethod == PaymentMethod.Undefined)
            {
                return null;
            }

            var customer = new Customer()
            {
                CustomerName = options.CustomerName,
                AFM = options.AFM,
                CustomerType = options.CustomerType,
                CustomerPaymentMethod = options.CustomerPaymentMethod
            };

            _dbContext.Add(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> RetrieveCustomerAsync(RetrieveCustomerOptions options)
        {
            var dbCustomer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == options.CustomerId)
                .SingleOrDefaultAsync();

            return dbCustomer;
        }

        public async Task<Customer> UpdateCustomerAsync(UpdateCustomerOptions options)
        {
            var dbCustomer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == options.CustomerId)
                .SingleOrDefaultAsync();

            if (options?.CustomerName != null)
            {
                if (options?.CustomerName == string.Empty)
                {
                    return null;
                }
                else
                    dbCustomer.CustomerName = options.CustomerName;
            }

            if (options?.AFM != null)
            {
                if (options?.AFM == string.Empty)
                {
                    return null;
                }
                else if (options.AFM.Length != 9)
                {
                    return null;
                }
                else
                {
                    dbCustomer.AFM = options.AFM;
                }
            }

            if (options?.CustomerType != CustomerType.Undefined)
            {
                dbCustomer.CustomerType = options.CustomerType;
            }

            if (options?.CustomerPaymentMethod != PaymentMethod.Undefined)
            {
                    
                dbCustomer.CustomerPaymentMethod = options.CustomerPaymentMethod;
            }

            _dbContext.Update(dbCustomer);
            await _dbContext.SaveChangesAsync();

            return dbCustomer;
        }

        public async Task<Customer> DeleteCustomerAsync(DeleteCustomerOptions options)
        {
            var dbCustomer = await _dbContext.Set<Customer>()
                .Where(c => c.CustomerName == options.CustomerName)
                .SingleOrDefaultAsync();

            _dbContext.Remove(dbCustomer);
            await _dbContext.SaveChangesAsync();

            return dbCustomer;
        }
    }
}
