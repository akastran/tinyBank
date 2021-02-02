using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyBank.Core.Model
{
    public enum CustomerType
    {
        Undefined = 0,
        Personal = 1,
        Merchant = 2
    }
}
