using Coomes.SpendingReports.CsvData;
using Coomes.SpendingReports.Api.Categories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coomes.SpendingReports.IntegrationTest
{
    [TestClass]
    public class ClassifierDataTests
    {
        [TestMethod]
        public async Task GetAll_ReturnsPersistedClassifiers()
        {
            // arrange
            InitialData.ResetStorage();
            var sut = new ClassifierData(InitialData.StoreLocation);

            // act
            var classifiers = await sut.GetAll();

            // assert
            classifiers.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task Add_PersistsClassifier() 
        {
            // arrange
            InitialData.ResetStorage();
            var newClassifier = new Classifier("new category", "new search text");
            var sut = new ClassifierData(InitialData.StoreLocation);
            var originalCount = (await sut.GetAll()).Count;

            // act
            var addResult = await sut.Add(newClassifier);
            var postAddCount = (await sut.GetAll()).Count;

            // assert
            addResult.Category.Should().Be(newClassifier.Category);
            addResult.SearchValue.Should().Be(newClassifier.SearchValue);
            postAddCount.Should().Be(originalCount + 1);
        }
    }
}
