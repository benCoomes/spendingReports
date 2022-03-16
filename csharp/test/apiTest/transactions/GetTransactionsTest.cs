using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.Api.Transactions.Operations;

namespace Coomes.SpendingReports.ApiTest.Transactions
{
    [TestClass]
    public class GetTransactionsTest
    {
        [TestMethod]
        public async Task GetTransactions_ReturnsAllTransactions()
        {
            // given 
            var testData = new TestTransactionData();
            var expectedTransactions = new List<Transaction>
            {
                new Transaction { Amount = 1.01, Description = "T1", Date = DateTime.Parse("01-01-2020") },
                new Transaction { Amount = -1.01, Description = "T2", Date = DateTime.Parse("01-02-2020") },
                new Transaction { Amount = 202.01, Description = "T3", Date = DateTime.Parse("01-03-2020") }
            };
            await testData.Add(expectedTransactions);

            var getTransactions = new GetTransactions(testData);
            
            // when 
            var actualTransactions = await getTransactions.Execute();

            // then 
            actualTransactions.Should().BeEquivalentTo(expectedTransactions);
        }
    }
}
