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
            var transaction = new Transaction
            {
                Amount = -1,
                Description = "some transaction",
                Category = "",
                Date  = DateTime.Parse("2020-01-01")
            };
            var transactions = new List<Transaction> { transaction };
            
            var classifier = new Classifier("some transaction", "expected category");
            var classifierData = new Mock<IClassifierData>();
            classifierData
                .Setup(cd => cd.GetAll())
                .ReturnsAsync(new List<Classifier> { classifier });

            var sut = new ApplyClassificationRules(classifierData.Object);    

            await sut.Execute(transactions);

            transaction.Category.Should().Be("expected category");
        }
    }         
}
