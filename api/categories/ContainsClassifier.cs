using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Categories 
{
    public class ContainsClassifier : IClassifier
    {
        public string SearchValue { get; private set; }
        public string Category { get; private set; }

        public ContainsClassifier(string searchValue, string category)
        {
            SearchValue = searchValue;
            Category = category;
        }

        public bool Apply(Transaction transaction)
        {
            if(transaction.Description.Contains(SearchValue))
            {
                transaction.Category = Category;
                return true;
            }
            return false;
        }
    }
}