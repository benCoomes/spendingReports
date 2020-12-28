using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.Api.Categories.Operations;
using Coomes.SpendingReports.Api.Categories;
using Moq;

namespace Coomes.SpendingReports.ApiTest.Categories
{
    [TestClass]
    public class ApplyClassificationRulesTests
    {
        [TestMethod]
        public async Task ApplyClassificationRules_SetsTransactionCategories()
        {
            var originalTransactions = new List<Transaction>
            {
                new Transaction
                {
                    Amount = -1,
                    Description = "some transaction",
                    Category = "original category",
                    Date  = DateTime.Parse("2020-01-01")
                }
            };
            
            var classifier = new ContainsClassifier("some transaction", "expected category");
            var classifierData = new Mock<IClassifierData>();
            classifierData
                .Setup(cd => cd.GetAll())
                .ReturnsAsync(new List<IClassifier> { classifier });

            var sut = new ApplyClassificationRules(classifierData.Object);    

            var updatedTransactions = await sut.Execute(originalTransactions);

            var updatedTransaction = updatedTransactions.Single();
            updatedTransaction.Category.Should().Be("expected category");
        }
    }         
}
