using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyBank.Core.Model;
using tinyBank.Core.Services.Options;

namespace tinyBank.Core.Services
{
    public interface ICustomerService
    {
        public Task<Customer> RegisterCustomerAsync(RegisterCustomerOptions options);
        //public Customer RegisterCustomer(RegisterCustomerOptions options);
        public Task<Customer> RetrieveCustomerAsync(RetrieveCustomerOptions options);
        public Task<Customer> UpdateCustomerAsync(UpdateCustomerOptions options);
        public Task<Customer> DeleteCustomerAsync(DeleteCustomerOptions options);
        public Result<List<Customer>> ParseFile(string path);
    }
}
