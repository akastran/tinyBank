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

        public Customer Register(RegisterCustomerOptions options)
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
    }
}
