using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyBank.Core.Model;
using tinyBank.Core.Services.Options;

namespace tinyBank.Core.Services
{
    public interface IAccountService
    {
        public Account Register(RegisterAccountOptions options);
    }
}
