using CLHBankApp.Enums;
using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers
{
    public class TransactionManager
    {
        static string file = @"Files\transactions.txt";
        public static List<Transaction> transactions = new List<Transaction>();

        public TransactionManager()
        {
            ReadFromFile();
        }

        private void ReadFromFile()
        {
            try
            {
                if (File.Exists(file))
                {
                    var allLines = File.ReadAllLines(file);
                    foreach (var line in allLines)
                    {
                        var transaction = Transaction.ToTransaction(line);
                        transactions.Add(transaction);
                    }
                }
                else
                {
                    var path = "Files";
                    Directory.CreateDirectory(path);
                    var fileName = "transactions.txt";
                    var fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void WriteToFile(Transaction transaction)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(file, true))
                {
                    write.WriteLine(transaction.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AddNewTransaction(string name, string details, decimal amount, TransactionType type, string acctNo)
        {
            var date = DateTime.Now;
            int id = transactions.Count == 0 ? 1 : transactions[transactions.Count - 1].Id + 1;
            Transaction transaction = new Transaction(id, name, details, date, amount, type, acctNo);
            transactions.Add(transaction);
            WriteToFile(transaction);
        }

        private static void PrintTransaction(Transaction transaction)
        {
            Console.WriteLine($"{transaction.Id}\t{transaction.CustomerName}\t{transaction.Details}\t{transaction.Amount}\t{transaction.Type}\t{transaction.Date}");
        }

        public static void GetAll()
        {
            var count = 0;
            foreach(var tran in transactions)
            {
                PrintTransaction(tran);
                count++;
            }
            if(count == 0)
            {
                Console.WriteLine("There is no transaction yet...");
            }
        }

        public static void GetAllByDate(DateTime date)
        {
            int count = 0;
            foreach(var transaction in transactions)
            {
                if(transaction.Date.Date == date.Date)
                {
                    PrintTransaction(transaction);
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine($"There is no transaction on {date}.");
            }
        }

        public static void GetAllByCustomer(string accountNo)
        {
            int count = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.AccountNo == accountNo)
                {
                    PrintTransaction(transaction);
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("There is no transaction from the customer yet.");
            }
        }

        public static void GetAllByCustomerOnDate(string accountNo, DateTime date)
        {
            int count = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.AccountNo == accountNo && transaction.Date.Date == date.Date)
                {
                    PrintTransaction(transaction);
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine($"There is no transaction this customer on {date}.");
            }
        }
    }
}
