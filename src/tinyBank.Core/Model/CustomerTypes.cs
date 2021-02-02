using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyBank.Core.Model
{
    public class CustomerTypes
    {
        [Key]
        public string CustomerTypeName { get; set; }
    }
}
