using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coomes.SpendingReports.Api.Transactions.Operations
{
    public class ImportTransactions 
    {
        private ITransactionData _data;

        public ImportTransactions(ITransactionData data)
        {
            _data = data;
        }

        public Task Execute(IEnumerable<Transaction> transactions)
        {
            return _data.Add(transactions);
        }

        public async Task Execute(Stream rawTransactionCsvStream, ITransactionReader reader)
        {
            var transactions = await reader.ReadAllAsync(rawTransactionCsvStream);
            await Execute(transactions);
        }
    }
}