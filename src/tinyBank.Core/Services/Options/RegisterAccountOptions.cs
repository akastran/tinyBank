using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyBank.Core.Services.Options
{
    public class RegisterAccountOptions
    {
        public string AccountId { get; set; }
        public decimal AccountBalance { get; set; }
        public int CustomerId { get; set; }
    }
}
