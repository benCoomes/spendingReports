using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.Api.Transactions.Operations;
using Coomes.SpendingReports.Api.Reports.Operations;
using Coomes.SpendingReports.CsvData;
using Coomes.SpendingReports.Api.Categories.Operations;

namespace Coomes.SpendingReports.ConsoleApp
{
    class Program
    {
        static Settings _settings = new Settings();

        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                DisplayLine($"SpendingReports cannot be run without arguments. Try one of the supported commands: Import");
                return;
            }

            try
            {
                InitConfiguration();
            }
            catch(MissingConfigurationException ex)
            {
                DisplayLine($"Required configurations are missing:\n\t" + ex.Message);
                return;
            }

            var command = args[0].ToLower();
            switch(command)
            {
                case("import"):
                    RunImport(args);
                    break;
                case("report"):
                    Report(args);
                    break;
                default:
                    DisplayLine($"The command '${command}' is not recognized. Please try of the supported commands: \nImport");
                    break;
            }
        }

        static void RunImport(string[] args)
        {
            var filePath = GetImportFile();
            var transactions = new List<Transaction>();
            var adapter = new WellsFargoAdapter.WellsFargoCsvReader(); 
            using(var stream = File.OpenRead(filePath))
            {
                transactions.AddRange(adapter.ReadAllAsync(stream).GetAwaiter().GetResult());
            }
            
            var transactionData = new TransactionData(_settings.StorageLocation);
            var importOperation = new ImportTransactions(transactionData);

            var classifierData = new ClassifierData(_settings.StorageLocation);
            var classifyOperation = new ApplyClassificationRules(classifierData);

            Import.Run(transactions, importOperation, classifyOperation);

            string GetImportFile() 
            {
                if(args.Length <2)
                {
                    DisplayLine("Please specify the file to import: ");
                    return Console.ReadLine();
                }
                else
                {
                    return args[1];
                }
            }
        }

        static void Report(string[] args)
        {
            var storage = new TransactionData(_settings.StorageLocation);
            var operation = new GetSpendingByCategory(storage);
            var report = operation.Execute().GetAwaiter().GetResult();

            DisplayLine("");
            DisplayLine("            Spending Report:           ");
            DisplayLine("");
            DisplayLine("Category".PadRight(20) + "\tAmount");
            DisplayLine("=======================================");
            foreach(var category in report.Categories.Values)
            {
                DisplayLine(category.Name.PadRight(20) + "\t" + category.Net.ToString("C"));
            }
            DisplayLine("=======================================");
            DisplayLine($"Gain: " + report.Gain.ToString("C"));
            DisplayLine($"Loss: " + report.Loss.ToString("C"));
            DisplayLine($"Net:  " + report.Net.ToString("C"));
            DisplayLine("");
        }

        static void DisplayLine(string line) => Console.WriteLine(line);
        static void Display(string line) => Console.Write(line);

        static void InitConfiguration()
        {
            var dll = Assembly.GetExecutingAssembly().Location;
            var dir = Path.GetDirectoryName(dll);
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(dir)
                .AddJsonFile("appsettings.json"); //todo: add to source w/o secrets
            var config = configBuilder.Build();

            _settings.StorageLocation = config["storageLocation"];
            if(string.IsNullOrWhiteSpace(_settings.StorageLocation))
                throw new MissingConfigurationException("storageLocation must be set.");
        }

        class Settings
        {
            public string StorageLocation { get; set; }
        }
    }

    class MissingConfigurationException : Exception
    {
        public MissingConfigurationException(string message) : base(message)
        {}
    }
}
