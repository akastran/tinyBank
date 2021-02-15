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

        public Customer RegisterCustomer(RegisterCustomerOptions options)
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
            _dbContext.SaveChanges();

            return customer;
        }

        public Customer RetrieveCustomer(RetrieveCustomerOptions options)
        {
            var dbCustomer = _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == options.CustomerId)
                .SingleOrDefault();

            return dbCustomer;
        }

        public Customer UpdateCustomer(UpdateCustomerOptions options)
        {
            var dbCustomer = _dbContext.Set<Customer>()
                .Where(c => c.CustomerId == options.CustomerId)
                .SingleOrDefault();

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
            _dbContext.SaveChanges();

            return dbCustomer;
        }

        public Customer DeleteCustomer(DeleteCustomerOptions options)
        {
            var dbCustomer = _dbContext.Set<Customer>()
                .Where(c => c.CustomerName == options.CustomerName)
                .SingleOrDefault();

            _dbContext.Remove(dbCustomer);
            _dbContext.SaveChanges();

            return dbCustomer;
        }
    }
}
