using System;
using Coomes.SpendingReports.Api.Transactions.Operations;
using Coomes.SpendingReports.Api.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; 
using DTO = Coomes.SpendingReports.Dto;

namespace Coomes.SpendingReports.Web.Controllers
{
    [ApiController]
    [Route("transaction")]
    public class TransactionController 
    {
        [HttpGet]
        public async Task<IEnumerable<DTO.Transaction>> GetTransactions([FromServices] GetTransactions operation) 
        {
            var transactions = await operation.Execute();
            return transactions.Select(t => t.ToDTO());
        }

        [HttpPost]
        public Task AddTransaction([FromServices] ImportTransactions operation, DTO.Transaction transactionDTO) 
        {
            var transaction = new Transaction() 
            {
                Amount = transactionDTO.Amount,
                Category = transactionDTO.Category,
                Description = transactionDTO.Description,
                Date = transactionDTO.Date
            };

            return operation.Execute(new List<Transaction> { transaction });
        }
    }
}

