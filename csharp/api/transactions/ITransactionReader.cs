using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Coomes.SpendingReports.Api.Transactions 
{
    public interface ITransactionReader 
    {
        Task<IEnumerable<Transaction>> ReadAllAsync(Stream stream);
    }
}