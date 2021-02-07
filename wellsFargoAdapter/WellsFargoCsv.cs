using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper;
using Coomes.SpendingReports.Api.Transactions;
using System.Threading.Tasks;

namespace Coomes.SpendingReports.WellsFargoAdapter
{
    public class WellsFargoCsvReader : ITransactionReader
    {
        public Task<IEnumerable<Transaction>> ReadAllAsync(Stream stream)
        {
            using(var streamReader = new StreamReader(stream))
            using(var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.HasHeaderRecord = false;
                var transactions = csvReader
                    .GetRecords<WellsFargoCSVTransaction>()
                    .Select(wft => wft.ToDomainModel())
                    .ToList();
                return Task.FromResult<IEnumerable<Transaction>>(transactions);
            }
        }
    }
}
