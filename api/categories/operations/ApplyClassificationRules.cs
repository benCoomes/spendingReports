using System;
using System.Threading.Tasks;    
using System.Collections.Generic;
using Coomes.SpendingReports.Api.Transactions.Operations;

namespace Coomes.SpendingReports.Api.Categories.Operations 
{
    public class ApplyClassificationRules 
    {
        private GetTransactions _getTransactions;
        private UpdateTransactions _updateTransactions;

        public ApplyClassificationRules(GetTransactions getTransactions, UpdateTransactions updateTransactions)
        {
            _getTransactions = getTransactions;
            _updateTransactions = updateTransactions;
        }

        public Task Execute() 
        {
            throw new NotImplementedException();
        }
    }
}