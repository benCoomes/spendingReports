using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Coomes.SpendingReports.Api.Transactions.Operations;
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
                Console.WriteLine($"SpendingReports cannot be run without arguments. Try one of the supported commands: \nImport");
            }

            try
            {
                InitConfiguration();
            }
            catch(MissingConfigurationException ex)
            {
                Console.WriteLine("Required configurations are missing:\n\t" + ex.Message);
                return;
            }

            var command = args[0];
            switch(command)
            {
                case("Import"):
                    Import(args);
                    break;
                case("Summary"):
                    Summary(args);
                    break;
                default:
                    Console.WriteLine("The command '${command}' is not recognized. Please try of the supported commands: \nImport");
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

        static void Summary(string[] args)
        {
            var storage = new TransactionData(_settings.StorageLocation);
            var operation = new GetTransactions(storage);
            var transactions = operation.Execute().GetAwaiter().GetResult();

            var sum = transactions.Sum(t => t.Amount);

            Console.WriteLine($"You have saved ${sum} monies."); 
        }

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
