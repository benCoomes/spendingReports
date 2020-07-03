using System.Threading.Tasks;
using System.Collections.Generic;

namespace Coomes.SpendingReports.Api.Categories
{
    public interface IClassifierData
    {
        Task<ICollection<IClassifier>> GetAll();
    }
}