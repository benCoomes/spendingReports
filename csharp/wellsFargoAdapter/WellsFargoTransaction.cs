using System;
using CsvHelper.Configuration.Attributes;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.WellsFargoAdapter
{
    public class WellsFargoCSVTransaction 
    {
        [Index(0)]
        public DateTime Date {get; set;}
        [Index(1)]
        public double Amount {get; set;}
        [Index(4)]
        public string Description {get; set;}

        public Transaction ToDomainModel() 
        {
            return new Transaction
            {
                Date = this.Date,
                Amount = this.Amount,
                Description = this.Description
            };
        }
    }
}