using Microsoft.EntityFrameworkCore;
using Npoi.Mapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyBank.Core.Constants;
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

            if (options?.TotalGross == null)
            {
                return null;
            }

            var customer = new Customer()
            {
                CustomerName = options.CustomerName,
                AFM = options.AFM,
                CustomerType = options.CustomerType,
                CustomerPaymentMethod = options.CustomerPaymentMethod,
                TotalGross = options.TotalGross
            };

            _dbContext.Add(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
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

            if (options?.TotalGross == null)
            {
                return null;
            }

            var customer = new Customer()
            {
                CustomerName = options.CustomerName,
                AFM = options.AFM,
                CustomerType = options.CustomerType,
                CustomerPaymentMethod = options.CustomerPaymentMethod,
                TotalGross = options.TotalGross
            };

            _dbContext.Add(customer);
            _dbContext.SaveChanges();

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

            if (options?.TotalGross != null)
            {

                dbCustomer.TotalGross = options.TotalGross;
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

        public Result<List<Customer>> ParseFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return new Result<List<Customer>>()
                {
                    Code = ResultCode.BadRequest,
                    ErrorMessage = $"Invalid file path {path}"
                };
            }

            if (!File.Exists(path))
            {
                return new Result<List<Customer>>()
                {
                    Code = ResultCode.NotFound,
                    ErrorMessage = $"File {path} was not found"
                };
            }

            var listOfCustomers = new List<Customer>();

            var mapper = new Mapper(path);

            mapper.Map<Customer>(1, $"{nameof(Customer.CustomerName)}",
                (columnInfo, customer) => {

                    var customerName = columnInfo.CurrentValue.ToString();

                    ((Customer)customer).CustomerName = customerName;

                    return true;
                });

            mapper.Map<Customer>("Gross", $"{nameof(Customer.TotalGross)}",
                (columnInfo, customer) => {

                    var totalGross = decimal.Parse(
                        columnInfo.CurrentValue.ToString(), CultureInfo.InvariantCulture);

                    ((Customer)customer).TotalGross = totalGross;

                    return true;
                });

            mapper.Map<Customer>("Type", $"{nameof(Customer.CustomerType)}",
                (columnInfo, customer) => {

                    var customerType = Enum.Parse<CustomerType>(columnInfo.CurrentValue.ToString(), true);

                    ((Customer)customer).CustomerType = customerType;

                    return true;
                });

            mapper.Map<Customer>("Method", $"{nameof(Customer.CustomerPaymentMethod)}",
                (columnInfo, customer) => {

                    var paymentMethod = Enum.Parse<PaymentMethod>(columnInfo.CurrentValue.ToString(), true);

                    ((Customer)customer).CustomerPaymentMethod = paymentMethod;

                    return true;
                });

            mapper.Map<Customer>("AFM", $"{nameof(Customer.AFM)}",
                (columnInfo, customer) => {

                    var afm = columnInfo.CurrentValue.ToString();

                    ((Customer)customer).AFM = afm;

                    return true;
                });

            var results = mapper
                .Take<Customer>()
                .Select(row => row.Value)
                .ToList();

            return new Result<List<Customer>>()
            {
                Code = ResultCode.Success,
                Data = results
            };
        }
    }
}
