using System.Threading.Tasks;    
using System.Collections.Generic;

namespace Coomes.SpendingReports.Api.Categories.Operations 
{
    public class GetClassifiers 
    {
        private IClassifierData _data;

        public GetClassifiers(IClassifierData data) 
        {
            _data = data;
        }

        public Task<ICollection<Classifier>> Execute() 
        {
            return _data.GetAll();
        }
    }
}