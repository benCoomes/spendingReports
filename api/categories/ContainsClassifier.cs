using System;
using System.Threading.Tasks;    
using System.Collections.Generic;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Categories 
{
    public class ContainsClassifier : Classifier
    {
        private readonly string _searchValue;
        private readonly string _category;

        public ContainsClassifier(string searchValue, string category)
        {
            _searchValue = searchValue;
            _category = category;
        }

        public void ApplyCategory(Transaction transaction)
        {
            if(transaction.Description.Contains(_searchValue))
            {
                transaction.Category = _category;
            }
        }
    }
}