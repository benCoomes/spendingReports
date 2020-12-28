using System.Threading.Tasks;    
using System.Collections.Generic;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Categories.Operations 
{
    public class ApplyClassificationRules 
    {
        private IClassifierData _classifierData;

        public ApplyClassificationRules(IClassifierData classifierData) {
            _classifierData = classifierData;
        }

        public async Task<ICollection<Transaction>> Execute(ICollection<Transaction> transactions)
        {
            var classifiers = await _classifierData.GetAll();
            
            foreach(var trans in transactions)
            {
                foreach(var classifier in classifiers) 
                {
                        if(classifier.Apply(trans))
                        {
                            break;
                        }
                }
            }

            return transactions;
        }
    }
}