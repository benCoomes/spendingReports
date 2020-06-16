using System.Threading.Tasks;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Reports.Operations
{
    public class SpendingByCategory
    {
        private ITransactionData _transactions;

        public SpendingByCategory(ITransactionData transactions)
        {
            _transactions = transactions;
        }

        public async Task<SpendingByCategoryReport> Execute()
        {
            var transactions = await _transactions.GetAll();
            return new SpendingByCategoryReport(transactions);
        }
    }
}