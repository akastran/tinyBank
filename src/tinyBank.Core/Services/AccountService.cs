using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyBank.Core.Data;
using tinyBank.Core.Model;
using tinyBank.Core.Services.Options;

namespace tinyBank.Core.Services
{
    public class AccountService : IAccountService
    {
        private BankDbContext _dbContext;

        public AccountService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account Register(RegisterAccountOptions options)
        {
            if (string.IsNullOrWhiteSpace(options?.AccountId))
            {
                return null;
            }

            if ((options?.CustomerId == null) || (options?.CustomerId == 0))
            {
                return null;
            }

            var account = new Account()
            {
                AccountId = options.AccountId,
                AccountBalance = 0,
                CustomerId = options.CustomerId
            };

            _dbContext.Add(account);
            _dbContext.SaveChanges();

            return account;
        }
    }
}
