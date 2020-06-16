using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Coomes.SpendingReports.Api.Transactions.Operations;
using Coomes.SpendingReports.Api.Reports.Operations;
using Coomes.SpendingReports.CsvData;

namespace Coomes.SpendingReports.ConsoleApp
{
    class Program
    {
        static Settings _settings = new Settings();

        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Display($"SpendingReports cannot be run without arguments. Try one of the supported commands: Import");
                return;
            }

            try
            {
                InitConfiguration();
            }
            catch(MissingConfigurationException ex)
            {
                Display($"Required configurations are missing:\n\t" + ex.Message);
                return;
            }

            var command = args[0].ToLower();
            switch(command)
            {
                case("import"):
                    Import(args);
                    break;
                case("report"):
                    Report(args);
                    break;
                default:
                    Display($"The command '${command}' is not recognized. Please try of the supported commands: \nImport");
                    break;
            }
        }

        static void Import(string[] args)
        {
            string filePath;
            if(args.Length <2)
            {
                Console.WriteLine("Please specify the file to import: ");
                filePath = Console.ReadLine();
            }
            else
            {
                filePath = args[1];
            }

            var transactions = WellsFargoAdapter.WellsFargoCsv.GetTransactions(filePath);
            var storage = new TransactionData(_settings.StorageLocation);
            var operation = new ImportTransactions(storage);

            operation.Execute(transactions).GetAwaiter().GetResult(); // todo - async?
        }

        static void Report(string[] args)
        {
            var storage = new TransactionData(_settings.StorageLocation);
            var operation = new SpendingByCategory(storage);
            var report = operation.Execute().GetAwaiter().GetResult();

            Display("");
            Display("            Spending Report:           ");
            Display("");
            Display("Category".PadRight(20) + "\tAmount");
            Display("=======================================");
            foreach((string category, double amount) in report.Categories)
            {
                Display(category.PadRight(20) + "\t" + amount.ToString("C"));
            }
            Display("=======================================");
            Display($"Total: {report.NetTotal}");
            Display("");
        }

        static void Display(string line) => Console.WriteLine(line);

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
