using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Categories 
{
    public class Classifier
    {
        public string Category { get; private set; }
        public string SearchValue { get; private set; }

        public Classifier(string searchValue, string category)
        {
            SearchValue = searchValue;
            Category = category;
        }

        public bool Apply(Transaction transaction)
        {
            if(!transaction.IsCategorized() && transaction.Description.Contains(SearchValue))
            {
                transaction.Category = Category;
                return true;
            }
            return false;
        }

        public Dto.Classifier ToDTO() 
        {
            return new Dto.Classifier 
            {
                Category = this.Category,
                SearchValue = this.SearchValue
            };
        }
    }
}