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
        public Customer RegisterCustomer(RegisterCustomerOptions options);
        public Customer RetrieveCustomer(RetrieveCustomerOptions options);
        public Customer UpdateCustomer(UpdateCustomerOptions options);
        public Customer DeleteCustomer(DeleteCustomerOptions options);
    }
}
