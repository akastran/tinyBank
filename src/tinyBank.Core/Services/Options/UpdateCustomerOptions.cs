using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyBank.Core.Model;

namespace tinyBank.Core.Services.Options
{
    public class UpdateCustomerOptions
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string AFM { get; set; }
        public CustomerType CustomerType { get; set; }
        public PaymentMethod CustomerPaymentMethod { get; set; }
    }
}
