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
        public CustomerType CustomerType { get; set; }
        public string AFM { get; set; }
        public List<Account> Accounts { get; set; }
        public PaymentMethod CustomerPaymentMethod { get; set; }
        public decimal TotalGross { get; set; }

        public Customer()
        {
            Accounts = new List<Account>();
        }
    }
}
