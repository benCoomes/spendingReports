using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Categories 
{
    public interface IClassifier
    {
        void ApplyCategory(Transaction transaction);
    }
}