using System;

namespace Coomes.SpendingReports.Api.Transactions
{
    public class Transaction 
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}