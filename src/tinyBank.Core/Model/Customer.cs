using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyBank.Core.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public CustomerTypes CustomerType { get; set; }
        public List<Account> Accounts { get; set; }
        public PaymentMethod CustomerPaymentMethod { get; set; }

        public Customer()
        {
            Accounts = new List<Account>();
        }
    }
}
