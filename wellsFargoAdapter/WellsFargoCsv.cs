using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper;
using Coomes.SpendingReports.Api.Transactions;

namespace Coomes.SpendingReports.WellsFargoAdapter
{
    public static class WellsFargoCsv
    {
        public static IEnumerable<Transaction> GetTransactions(string path) 
        {
            using(var stream = File.OpenRead(path))
            using(var streamReader = new StreamReader(stream))
            using(var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.HasHeaderRecord = false;
                return csvReader
                    .GetRecords<WellsFargoCSVTransaction>()
                    .Select(wft => wft.ToDomainModel());
            }
        }
    }
}
