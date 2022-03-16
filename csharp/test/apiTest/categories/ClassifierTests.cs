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
    public class ClassifierTests
    {
        [TestMethod]
        public void Classifier_AppliesWhenMatch()
        {
            var trans = new Transaction
            {
                Amount = 123.45,
                Description = "This is the description",
                Date = DateTime.Parse("2020-04-02")
            };

            var sut = new Classifier("description", "Expected Category");

            var didApply = sut.Apply(trans);

            didApply.Should().BeTrue();
            trans.Category.Should().Be("Expected Category");
        }

        [TestMethod]
        public void Classifier_DoesNotOverwriteExistingCategory()
        {
            var trans = new Transaction
            {
                Amount = 123.45,
                Description = "This is the description",
                Date = DateTime.Parse("2020-04-02"),
                Category = "Original Category"
            };

            var sut = new Classifier("description", "New Category");

            var didApply = sut.Apply(trans);

            didApply.Should().BeFalse();
            trans.Category.Should().Be("Original Category");
        }

        [TestMethod]
        public void Classifier_DoesNotApplyWhenNoMatch()
        {
            var originalCategory = "Original Category";
            var trans = new Transaction
            {
                Amount = 123.45,
                Description = "This is the description",
                Date = DateTime.Parse("2020-04-02"),
                Category = originalCategory
            };

            var sut = new Classifier("foobar", "Unoriginal Category");

            var didApply = sut.Apply(trans);

            didApply.Should().BeFalse();
            trans.Category.Should().Be(originalCategory);
        }
    }
}
