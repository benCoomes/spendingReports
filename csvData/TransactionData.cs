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
        
        private string _path;
        private bool _initialized;
        private List<TransactionDataModel> _transactions;

        public TransactionData(string storageDir)
        {
            if(!Directory.Exists(storageDir)) 
            {
                Directory.CreateDirectory(storageDir);    
            }

            _path = Path.Combine(storageDir, STORAGEFILE);
        }

        public Task<ICollection<Transaction>> GetAll()
        {
            EnsureInitialized();

            var domainModels = _transactions.Select(t => t.ToDomainModel()).ToList();
            return Task.FromResult((ICollection<Transaction>)domainModels);
        }

        public Task Add(Transaction transaction)
        {
            return Add(new List<Transaction> {transaction});
        }

        public Task Add(IEnumerable<Transaction> transactions)
        {
            EnsureInitialized();

            foreach(var transaction in transactions)
            {
                _transactions.Add(transaction.ToDataModel());
            }
            SaveToFile();

            return Task.CompletedTask;
        }

        private void EnsureInitialized()
        {
            if(_initialized) return; 

            if(File.Exists(_path))
            {
                ReadFromFile();
            }
            else {
                _transactions = new List<TransactionDataModel>();
                SaveToFile();
            }

            _initialized = true; 
        }

        private void ReadFromFile()
        {
            using(var stream = File.OpenRead(_path))
            using(var streamReader = new StreamReader(stream))
            using(var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                _transactions = csvReader.GetRecords<TransactionDataModel>().ToList();
            }
        }

        private void SaveToFile()
        {
            using(var writer = new StreamWriter(_path))
            using(var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(_transactions);
            }
        }
    }
}