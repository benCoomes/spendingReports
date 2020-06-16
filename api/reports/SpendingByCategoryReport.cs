using System.Collections.Generic;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Reports
{
    public class SpendingByCategoryReport
    {
        private ICollection<Transaction> _transactions;
        public IDictionary<string, double> Categories { get; private set; }

        public SpendingByCategoryReport(ICollection<Transaction> transactions)
        {
            _transactions = transactions;
            GroupUncategorizedTransactions();
            CalculateCategorySpending();
        }

        private void GroupUncategorizedTransactions()
        {
            foreach(var transaction in _transactions)
            {
                if(string.IsNullOrWhiteSpace(transaction.Category))
                {
                    transaction.Category = "Uncategorized";
                }
            }
        }

        private void CalculateCategorySpending()
        {
            Categories = new Dictionary<string, double>();

            foreach(var transaction in _transactions)
            {
                if(!Categories.ContainsKey(transaction.Category))
                {
                    Categories.Add(transaction.Category, transaction.Amount);
                }
                else
                {
                    Categories[transaction.Category] += transaction.Amount;
                }
            }
        }
    }
}