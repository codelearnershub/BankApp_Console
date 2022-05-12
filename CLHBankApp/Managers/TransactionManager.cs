using CLHBankApp.Enums;
using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers
{
    public class TransactionManager
    {
        public static int NoOfTransation = 0;
        public static List<Transaction> transactions = new List<Transaction>();

        public static void AddNewTransaction(string name, string details, decimal amount, TransactionType type, string acctNo)
        {
            NoOfTransation++;
            var date = DateTime.Now;
            Transaction transaction = new Transaction(NoOfTransation, name, details, date, amount, type, acctNo);
            transactions.Add(transaction);
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
