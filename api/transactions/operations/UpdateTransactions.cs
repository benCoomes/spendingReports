using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coomes.SpendingReports.Api.Transactions.Operations
{
    public class UpdateTransactions 
    {
        private ITransactionData _data;

        public UpdateTransactions(ITransactionData data) 
        {
            _data = data;
        }

        public Task Execute(IEnumerable<Transaction> transactions)
        {
            return _data.Update(transactions);
        }
    }
}