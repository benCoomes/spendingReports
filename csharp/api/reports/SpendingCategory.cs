using System;
using System.Linq;
using System.Collections.Generic;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Reports
{
    public class SpendingCategory
    {
        private List<Transaction> _transactions;

        public string Name { get; private set; }
        public double Net { get => _transactions.Sum(t => t.Amount); }
        public double Gain { get => _transactions.Where(t => t.Amount > 0).Sum(t => t.Amount); }
        public double Loss { get => _transactions.Where(t => t.Amount < 0).Sum(t => t.Amount); }

        public SpendingCategory(IEnumerable<Transaction> transactions) 
        {
            _transactions = transactions.ToList();
            Name = _transactions.FirstOrDefault()?.Category;
            if(!_transactions.All(t => t.Category == Name))
            {
                throw new InvalidOperationException($"Cannot create a {nameof(SpendingCategory)} with transactions that have different category names.");
            }
        }
    }
}