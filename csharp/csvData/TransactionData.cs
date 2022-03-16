using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using CsvHelper;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.CsvData
{
    public class TransactionData : ITransactionData
    {
        private const string STORAGEFILE = "transactions.csv";
        private CsvRepository<TransactionDataModel> _storage;

        public TransactionData(string storageDir)
        {
            _storage = new CsvRepository<TransactionDataModel>(storageDir, STORAGEFILE);
        }

        public Task<ICollection<Transaction>> GetAll()
        {
            _storage.EnsureInitialized();

            var domainModels = _storage.Entities.Select(t => t.ToDomainModel()).ToList();
            return Task.FromResult((ICollection<Transaction>)domainModels);
        }

        public Task Add(Transaction transaction)
        {
            return Add(new List<Transaction> {transaction});
        }

        public Task Add(IEnumerable<Transaction> transactions)
        {
            _storage.EnsureInitialized();

            foreach(var transaction in transactions)
            {
                _storage.Entities.Add(transaction.ToDataModel());
            }
            _storage.SaveToFile();

            return Task.CompletedTask;
        }
    }
}