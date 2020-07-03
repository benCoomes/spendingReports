using System;
using System.Threading.Tasks;    
using System.Collections.Generic;
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

        public void ApplyCategory(Transaction transaction)
        {
            if(transaction.Description.Contains(SearchValue))
            {
                transaction.Category = Category;
            }
        }
    }
}