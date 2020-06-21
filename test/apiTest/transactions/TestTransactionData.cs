using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.ApiTest.Transactions 
{
    public class TestTransactionData : ITransactionData 
    {
        private List<Transaction> _transactions;

        public TestTransactionData() 
        {
            _transactions = new List<Transaction>();
        }

        public Task<ICollection<Transaction>> GetAll() 
        {
            ICollection<Transaction> result = _transactions.ToList();
            return Task.FromResult(result);
        }

        public Task Add(Transaction transaction)
        {
            return Add(new List<Transaction>{ transaction });
        }

        public Task Add(IEnumerable<Transaction> transactions)
        {
            _transactions.AddRange(transactions);
            return Task.CompletedTask;
        }

        public async Task Update(IEnumerable<Transaction> updates)
        {
            foreach(var update in updates)
            {
                await Update(update);
            }
        }

        public Task Update(Transaction update)
        {
            foreach(var existing in _transactions)
            {
                if(existing.IsSameAs(update))
                    existing.Category = update.Category;
            }
            return Task.CompletedTask;
        }
    }
}