using System.Threading.Tasks;    
using System.Collections.Generic;

namespace Coomes.SpendingReports.Api.Transactions.Operations 
{
    public class GetTransactions 
    {
        private ITransactionData _data;

        public GetTransactions(ITransactionData data)
        {
            _data = data;
        }

        public Task<ICollection<Transaction>> Execute() 
        {
            return _data.GetAll();
        }
    }
}