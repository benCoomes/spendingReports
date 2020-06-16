using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.Api.Reports.Operations;

namespace Coomes.SpendingReports.ApiTest.Transactions
{
    [TestClass]
    public class SpendingByCategoryTest
    {
        [TestMethod]
        public async Task SpendingByCategory_SumsCategories()
        {
            // given 
            var testData = new TestTransactionData();
            var expectedTransactions = new List<Transaction>
            {
                new Transaction { Amount = -100.00, Category = "Cat1", Description = "T1", Date = DateTime.Parse("01-01-2020") },
                new Transaction { Amount = -50.50, Category = "Cat1", Description = "T2", Date = DateTime.Parse("01-02-2020") },
                new Transaction { Amount = 200.00, Category = "Cat1", Description = "T3", Date = DateTime.Parse("01-03-2020") }
            };
            await testData.Add(expectedTransactions);

            var sut = new SpendingByCategory(testData);
            
            // when 
            var report = await sut.Execute();

            // then
            report.Categories["Cat1"].Should().Be(49.50);
        }

        [TestMethod]
        public async Task SpendingByCategory_HandlesMultipleCategories()
        {
            // given 
            var testData = new TestTransactionData();
            var expectedTransactions = new List<Transaction>
            {
                new Transaction { Amount = -100.00, Category = "Cat1", Description = "T1", Date = DateTime.Parse("01-01-2020") },
                new Transaction { Amount = -50.50, Category = "Cat1", Description = "T2", Date = DateTime.Parse("01-02-2020") },
                new Transaction { Amount = 200.00, Category = "Cat2", Description = "T3", Date = DateTime.Parse("01-03-2020") }
            };
            await testData.Add(expectedTransactions);

            var sut = new SpendingByCategory(testData);
            
            // when 
            var report = await sut.Execute();

            // then
            report.Categories["Cat1"].Should().Be(-150.50);
            report.Categories["Cat2"].Should().Be(200);
        }

        [TestMethod]
        public async Task SpendingByCategory_HandlesUncategoriezedTransactions()
        {
            // given 
            var testData = new TestTransactionData();
            var expectedTransactions = new List<Transaction>
            {
                new Transaction { Amount = -100.00, Category = "\t\t   ", Description = "T1", Date = DateTime.Parse("01-01-2020") },
                new Transaction { Amount = -50.50, Description = "T2", Date = DateTime.Parse("01-02-2020") },
                new Transaction { Amount = 200.00, Category = "", Description = "T3", Date = DateTime.Parse("01-03-2020") }
            };
            await testData.Add(expectedTransactions);

            var sut = new SpendingByCategory(testData);
            
            // when 
            var report = await sut.Execute();

            // then
            report.Categories["Uncategorized"].Should().Be(49.50);
        }

        [TestMethod]
        public async Task SpendingByCategory_GeneratesNetTotal()
        {
            // given 
            var testData = new TestTransactionData();
            var expectedTransactions = new List<Transaction>
            {
                new Transaction { Amount = -100.00, Category = "Cat1", Description = "T1", Date = DateTime.Parse("01-01-2020") },
                new Transaction { Amount = -50.50, Description = "Cat2", Date = DateTime.Parse("01-02-2020") },
                new Transaction { Amount = 200.00, Category = "Cat3", Description = "T3", Date = DateTime.Parse("01-03-2020") }
            };
            await testData.Add(expectedTransactions);

            var sut = new SpendingByCategory(testData);
            
            // when 
            var report = await sut.Execute();

            // then
            report.NetTotal.Should().Be(49.50);
        }
    }
}
