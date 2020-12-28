using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.Api.Categories 
{
    public interface IClassifier
    {
        bool Apply(Transaction transaction);
    }
}