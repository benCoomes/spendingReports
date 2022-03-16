using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coomes.SpendingReports.Api.Categories.Operations;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.Api.Transactions.Operations;

namespace Coomes.SpendingReports.ConsoleApp 
{
    public class Import 
    {
        private const char _accept = 'a';
        private readonly static string _helpText = $"Accept [{_accept} or Enter], New Category [type new category]";

        public static void Run(ICollection<Transaction> transactions, ImportTransactions importOperation, ApplyClassificationRules classificationOperation)
        {
            classificationOperation.Execute(transactions).GetAwaiter().GetResult();
            InteractiveModifications(transactions);
            ConfirmImport(transactions);
            importOperation.Execute(transactions).GetAwaiter().GetResult();
            DisplayLine("Import complete.");
        }

        private static void InteractiveModifications(ICollection<Transaction> transactions) 
        {
            DisplayLine(_helpText);
            foreach(var trans in transactions) 
            {
                ModifyTransaction(trans);
            }
        }    

        private static void ModifyTransaction(Transaction trans) 
        {
            while(true) 
            {  
                Display($"{GetDisplayString(trans)}: ");
                var command = Console.ReadLine()?.Trim() ?? "";

                if(command.Length == 0 || (command.Length == 1 && command[0] == _accept)) 
                {
                    break;
                }
                else if(command.Length >= 1) 
                {
                    trans.Category = command;
                    break;
                }
                else {
                    DisplayLine(_helpText);
                }
            }
        }

        private static void ConfirmImport(ICollection<Transaction> transactions) 
        {
            DisplayLine("\nPrinting transactions for review...");
            DisplayLine("===============================================");
            foreach(var trans in transactions)
            {
                Task.Delay(25).GetAwaiter().GetResult();
                DisplayLine(GetDisplayString(trans));
            }
            
            DisplayLine("\nPress enter to confirm or ctrl + C to exit.");
            Console.ReadLine();
        }

        private static string FitString(string input, int len)
        {
            if(string.IsNullOrEmpty(input)) 
                return input;
            else if(input.Length > len)
                return input.Substring(0, len);
            else
                return input.PadRight(len, ' ');
        }

        private static string GetDisplayString(Transaction trans)
        {
            var date = FitString(trans.Date.ToShortDateString(), 10);
            var description = FitString(trans.Description, 50);
            var amount = FitString(trans.Amount.ToString("C"), 10);
            var category = FitString(trans.Category, 20);
            
            return $"{date}\t{description}\t{amount}\t{category}";
        }
        private static void DisplayLine(string line) => Console.WriteLine(line);
        private static void Display(string line) => Console.Write(line);
    }
}