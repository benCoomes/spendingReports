using System.Linq;
using System.Threading.Tasks;    
using System.Collections.Generic;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Categories.Operations 
{
    public class ApplyClassificationRules 
    {
        public ICollection<Transaction> Execute(ICollection<Transaction> transactions)
        {
            foreach(var trans in transactions)
                if(trans.Category == Constants.Uncategorized)
                    trans.Category = "expected category";

            return transactions;
        }
    }
}