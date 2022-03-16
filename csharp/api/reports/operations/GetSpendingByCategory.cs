using System.Threading.Tasks;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Reports.Operations
{
    public class GetSpendingByCategory
    {
        private ITransactionData _transactions;

        public GetSpendingByCategory(ITransactionData transactions)
        {
            _transactions = transactions;
        }

        public async Task<SpendingByCategories> Execute()
        {
            var transactions = await _transactions.GetAll();
            return new SpendingByCategories(transactions);
        }
    }
}