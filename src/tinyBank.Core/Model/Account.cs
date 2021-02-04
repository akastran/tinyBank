using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyBank.Core.Model
{
    public class Account
    {
        public string AccountId { get; set; }
        public decimal AccountBalance { get; set; }
        public int CustomerId { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Account()
        {
            Transactions = new List<Transaction>();
        }
    }
}
