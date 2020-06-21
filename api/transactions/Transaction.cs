using System;

namespace Coomes.SpendingReports.Api.Transactions
{
    public class Transaction 
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        /// <Summary>
        /// Determines if 'other' refers to the same transaction as this transaction.
        /// </Summary>
        public bool IsSameAs(Transaction other)
        {
            return other.Date == this.Date
                && other.Amount == this.Amount
                && other.Description == this.Description;
        }
    }
}