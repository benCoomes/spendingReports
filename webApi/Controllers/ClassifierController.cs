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
    public class ClassifierController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<DTO.Classifier>> GetClassifiers([FromServices] GetClassifiers getClassifiers) 
        {
            var classifiers = await getClassifiers.Execute();
            return classifiers.Select(t => t.ToDTO());
        }

        [HttpPost]
        public async Task<ActionResult<DTO.Classifier>> AddClassifier([FromServices] AddClassifier addClassifier, Dto.Classifier newClassifier) 
        {
            var classifier = await addClassifier.Execute(newClassifier); 
            return Ok(classifier.ToDTO());
        }
    }
}

