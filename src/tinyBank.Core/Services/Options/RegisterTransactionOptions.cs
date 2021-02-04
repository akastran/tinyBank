using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyBank.Core.Services.Options
{
    public class RegisterTransactionOptions
    {
        public string AccountId { get; set; }
        public DateTimeOffset TransactionCreated { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}
