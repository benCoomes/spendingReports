using System;
using DTO = Coomes.SpendingReports.Dto;

namespace Coomes.SpendingReports.Api.Transactions
{
    public class Transaction 
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        
        private string _category;
        public string Category { 
            get
            {
                if(!IsCategorized()) {
                    return Constants.Uncategorized;
                }
                else 
                {
                    return _category;
                }
            }
            set 
            {
                _category = value;
            } 
        }

        public bool IsCategorized() 
        {
            return !string.IsNullOrWhiteSpace(_category);
        }

        public DTO.Transaction ToDTO() 
        {
            return new DTO.Transaction
            {
                Date = this.Date,
                Amount = this.Amount,
                Description = this.Description,
                Category = this.Category
            };
        }
    }
}