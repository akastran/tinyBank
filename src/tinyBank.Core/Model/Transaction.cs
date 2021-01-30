using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyBank.Core.Model
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public DateTimeOffset TransactionCreated { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}
