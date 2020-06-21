using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.Api.Transactions.Operations;

namespace Coomes.SpendingReports.ApiTest.Transactions
{
    [TestClass]
    public class UpdateTransactionsTest
    {
        [TestMethod]
        public async Task UpdateTransactions_UpdatesCategoryOfExistingTransactions()
        {
            var existingTransactions = new List<Transaction>
            {
                new Transaction
                {
                    Amount = 123.45,
                    Description = "Description",
                    Date = DateTime.Parse("2020-01-02"),
                    Category = "Original Category"
                }
            };

            var updatedTransactions = existingTransactions
                .Select(t => new Transaction
                {
                    Amount = t.Amount,
                    Description = t.Description,
                    Date = t.Date,
                    Category = "Updated Category"
                })
                .ToList();
            
            var transactionData = new TestTransactionData();
            await transactionData.Add(existingTransactions);

            // act
            var sut = new UpdateTransactions(transactionData);
            await sut.Execute(updatedTransactions);
            var actualTransactions = await new GetTransactions(transactionData).Execute();

            // assert
            actualTransactions.Should().BeEquivalentTo(updatedTransactions);
        }
    }
}
