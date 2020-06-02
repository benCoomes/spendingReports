using System;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.CsvData
{
    internal class TransactionDataModel
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public Transaction ToDomainModel() 
        {
            return new Transaction
            {
                Date = this.Date,
                Amount = this.Amount,
                Description = this.Description,
                Category = this.Category
            };
        }
    }

    internal static class TransactionExtensisons
    {
        public static TransactionDataModel ToDataModel(this Transaction domainModel)
        {
            return new TransactionDataModel
            {
                Date = domainModel.Date,
                Amount = domainModel.Amount,
                Description = domainModel.Description,
                Category = domainModel.Category
            };
        }
    }

}