using System;
using System.Threading.Tasks;    
using System.Collections.Generic;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Categories 
{
    public interface Classifier
    {
        void ApplyCategory(Transaction transaction);
    }
}