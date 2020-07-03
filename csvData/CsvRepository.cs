using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper;

namespace Coomes.SpendingReports.CsvData
{
    internal class CsvRepository<T>
    {
        private string _path;
        private bool _initialized;
        public List<T> Entities;

        public CsvRepository(string storageDir, string storageFile)
        {
            if(!Directory.Exists(storageDir)) 
            {
                Directory.CreateDirectory(storageDir);    
            }

            _path = Path.Combine(storageDir, storageFile);
        }

        public void EnsureInitialized()
        {
            if(_initialized) return; 

            if(File.Exists(_path))
            {
                ReadFromFile();
            }
            else {
                Entities = new List<T>();
                SaveToFile();
            }

            _initialized = true; 
        }

        public void ReadFromFile()
        {
            using(var stream = File.OpenRead(_path))
            using(var streamReader = new StreamReader(stream))
            using(var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                Entities = csvReader.GetRecords<T>().ToList();
            }
        }

        public void SaveToFile()
        {
            using(var writer = new StreamWriter(_path))
            using(var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(Entities);
            }
        }
    }
}