using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using CsvHelper;
using Coomes.SpendingReports.Api.Categories;

namespace Coomes.SpendingReports.CsvData
{
    public class ClassifierData : IClassifierData
    {
        private const string STORAGEFILE = "classifiers.csv";
        private readonly CsvRepository<ClassifierDataModel> _storage;

        public ClassifierData(string storageDir)
        { 
            _storage = new CsvRepository<ClassifierDataModel>(storageDir, STORAGEFILE);
        }

        public Task<ICollection<Classifier>> GetAll()
        {
            _storage.EnsureInitialized();

            var domainModels = _storage.Entities.Select(t => t.ToDomainModel()).ToList();
            return Task.FromResult((ICollection<Classifier>)domainModels);
        }
    }
}