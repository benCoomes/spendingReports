using Coomes.SpendingReports.CsvData;
using Coomes.SpendingReports.Api.Transactions;
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
    }
}
