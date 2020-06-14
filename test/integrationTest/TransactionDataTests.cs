using Coomes.SpendingReports.CsvData;
using Coomes.SpendingReports.Api.Transactions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Coomes.SpendingReports.IntegrationTest
{
    [TestClass]
    public class TransactionDataTests
    {
        [TestMethod]
        public async Task GetAll_ReturnsPersistedTransactions()
        {
            // arrange
            InitialData.ResetStorage();
            var sut = new TransactionData(InitialData.StoreLocation);

            // act
            var transactions = await sut.GetAll();

            // assert
            transactions.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task Add_PersistsTransactions()
        {
            // arrange
            InitialData.ResetStorage();
            var sut = new TransactionData(InitialData.StoreLocation);
            
            // act
            var preAdd = await sut.GetAll();
            await sut.Add(new Transaction
            {
                Date = DateTime.Parse("2020-04-29"),
                Amount = -123.23,
                Description = "Kroger Groceries Inc #123019-003289"
            });
            var postAdd = await sut.GetAll();

            // assert
            postAdd.Count.Should().Be(preAdd.Count + 1);
        }
    }
}
