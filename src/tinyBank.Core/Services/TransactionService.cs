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
    public class TransactionService : ITransactionService
    {
        private BankDbContext _dbContext;

        public TransactionService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Transaction Register(RegisterTransactionOptions options)
        {
            if (string.IsNullOrWhiteSpace(options?.AccountId))
            {
                return null;
            }

            if (options?.TransactionAmount == 0)
            {
                return null;
            }

            var transaction = new Transaction()
            {
                AccountId = options.AccountId,
                TransactionCreated = DateTimeOffset.Now,
                TransactionAmount = options.TransactionAmount
            };

            _dbContext.Add(transaction);
            _dbContext.SaveChanges();

            return transaction;
        }
    }
}
