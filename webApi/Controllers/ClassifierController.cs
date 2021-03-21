using System;
using Coomes.SpendingReports.Api.Categories.Operations;
using Coomes.SpendingReports.Api.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; 
using DTO = Coomes.SpendingReports.Dto;

namespace Coomes.SpendingReports.Web.Controllers
{
    [ApiController]
    [Route("classifier")]
    public class ClassifierController 
    {
        [HttpGet]
        public async Task<IEnumerable<DTO.Classifier>> GetClassifiers([FromServices] GetClassifiers getClassifiers) 
        {
            var classifiers = await getClassifiers.Execute();
            return classifiers.Select(t => t.ToDTO());
        }
    }
}

