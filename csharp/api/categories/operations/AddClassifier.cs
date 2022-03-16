using System.Threading.Tasks;    
using System.Collections.Generic;
using Dto = Coomes.SpendingReports.Dto;

namespace Coomes.SpendingReports.Api.Categories.Operations 
{
    public class AddClassifier 
    {
        private IClassifierData _data;

        public AddClassifier(IClassifierData data) 
        {
            _data = data;
        }

        public Task<Classifier> Execute(Dto.Classifier dto) 
        {
            var domainModel = new Classifier(dto);
            return _data.Add(domainModel);
        }
    }
}