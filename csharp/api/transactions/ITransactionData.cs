using System.Threading.Tasks;
using System.Collections.Generic;

namespace Coomes.SpendingReports.Api.Transactions
{
    public interface ITransactionData
    {
        Task<ICollection<Transaction>> GetAll();
        Task Add(Transaction transaction);
        Task Add(IEnumerable<Transaction> transactions);
    }
}