using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.Api.Categories;

namespace Coomes.SpendingReports.ApiTest.Categories
{
    [TestClass]
    public class ContainsClassifierTests
    {
        [TestMethod]
        public void ContainsClassifier_AppliesWhenMatch()
        {
            var trans = new Transaction
            {
                Amount = 123.45,
                Description = "This is the description",
                Date = DateTime.Parse("2020-04-02")
            };

            var sut = new ContainsClassifier("description", "Expected Category");

            sut.ApplyCategory(trans);

            trans.Category.Should().Be("Expected Category");
        }

        [TestMethod]
        public void ContainsClassifier_DoesNotApplyWhenNoMatch()
        {
            var originalCategory = "Original Category";
            var trans = new Transaction
            {
                Amount = 123.45,
                Description = "This is the description",
                Date = DateTime.Parse("2020-04-02"),
                Category = originalCategory
            };

            var sut = new ContainsClassifier("foobar", "Unoriginal Category");

            sut.ApplyCategory(trans);

            trans.Category.Should().Be(originalCategory);
        }
    }
}
