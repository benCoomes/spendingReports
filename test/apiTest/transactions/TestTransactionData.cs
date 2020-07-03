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
            ICollection<Transaction> result = _transactions.Select(t => DeepCopy(t)).ToList();
            return Task.FromResult(result);
        }

        public Task Add(Transaction transaction)
        {
            return Add(new List<Transaction>{ transaction });
        }

        public Task Add(IEnumerable<Transaction> transactions)
        {
            var copied = transactions.Select(t => DeepCopy(t));
            _transactions.AddRange(copied);
            return Task.CompletedTask;
        }
        
        private Transaction DeepCopy(Transaction original)
        {
            return new Transaction
            {
                Category = original.Category,
                Amount = original.Amount,
                Description = original.Description,
                Date = original.Date
            };
        }
    }
}