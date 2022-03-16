using System;
using Coomes.SpendingReports.Api.Transactions.Operations;
using Coomes.SpendingReports.Api.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; 

namespace Coomes.SpendingReports.Web.Controllers
{
    [ApiController]
    [Route("import")]
    public class ImportController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Import(
            [FromServices] ImportTransactions importOperation)
        {
            var form = await Request.ReadFormAsync();
            var transactions = new List<Transaction>();

            if(form.Files.Count != 1) {
                return BadRequest($"Expected the form to contain 1 file but found {form.Files.Count}");
            }

            using(var stream = form.Files.Single().OpenReadStream())
            {
                await importOperation.Execute(stream, new WellsFargoAdapter.WellsFargoCsvReader());
                return Ok();
            }
        }
    }
}

