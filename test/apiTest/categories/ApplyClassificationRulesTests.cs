using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.Api.Transactions.Operations;
using Coomes.SpendingReports.Api.Categories.Operations;
using Coomes.SpendingReports.ApiTest.Transactions;

namespace Coomes.SpendingReports.ApiTest.Categories
{
    [TestClass]
    public class ApplyClassificationRulesTests
    {
        [TestMethod]
        public void ApplyClassificationRules_SetsTransactionCategories()
        {
            var originalTransactions = new List<Transaction>
            {
                new Transaction
                {
                    Amount = -1,
                    Description = "null category",
                    Category = null,
                    Date  = DateTime.Parse("2020-01-01")
                },
                new Transaction
                {
                    Amount = -1,
                    Description = "empty category",
                    Category = "",
                    Date  = DateTime.Parse("2020-01-01")
                },
                new Transaction
                {
                    Amount = -1,
                    Description = "filled category",
                    Category = "original category",
                    Date  = DateTime.Parse("2020-01-01")
                }
            };
            
            var sut = new ApplyClassificationRules();    

            var updatedTransactions = sut.Execute(originalTransactions);

            var nullCategory = updatedTransactions.Where(t => t.Description == "null category").Single();
            var emptyCategory = updatedTransactions.Where(t => t.Description == "empty category").Single();
            var filledCategory = updatedTransactions.Where(t => t.Description == "filled category").Single();
            emptyCategory.Category.Should().Be("expected category");
            nullCategory.Category.Should().Be("expected category");
            filledCategory.Category.Should().Be("original category");
        }
    }         
}
