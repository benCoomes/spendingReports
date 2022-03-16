using System.Linq;
using System.Collections.Generic;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Reports
{
    // WHAT IF - 
    // SpendingByCategories and SpendingCategory became the SAME class (SpendingCategory).. 
    // Transactions would have 'layers' of categories. 
    // The transactions passed down to the sub-SpendingCategories would have the top layer category removed
    // Once a SpendingCategory has all 'Uncategoried' transactions it is a 'leaf category and doesn't have further sub-categories

    public class SpendingByCategories
    {
        private ICollection<Transaction> _transactions;
        public IDictionary<string, SpendingCategory> Categories { get; private set; }
        public double Net {get => _transactions.Sum(t => t.Amount); }
        public double Gain {get => _transactions.Where(t => t.Amount > 0).Sum(t => t.Amount); }
        public double Loss {get => _transactions.Where(t => t.Amount < 0).Sum(t => t.Amount); }

        public SpendingByCategories(ICollection<Transaction> transactions)
        {
            _transactions = transactions;
            GenerateCategories();
        }

        private void GenerateCategories()
        {
            Categories = _transactions
                .GroupBy(t => t.Category)
                .ToDictionary(tg => tg.Key, tg => new SpendingCategory(tg));
        }
    }
}