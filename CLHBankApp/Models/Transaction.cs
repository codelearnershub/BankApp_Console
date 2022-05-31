using CLHBankApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string AccountNo { get; set; }

        public Transaction(int id, string customerName, string details, DateTime date, decimal amount, TransactionType type, string acctNo)
        {
            Id = id;
            CustomerName = customerName;
            Details = details;
            Date = date;
            Amount = amount;
            Type = type;
            AccountNo = acctNo;
        }

        public override string ToString()
        {
            return $"{Id}\t{CustomerName}\t{Details}\t{Date}\t{Amount}\t{Type}\t{AccountNo}";
        }

        public static Transaction ToTransaction(string str)
        {
            var trans = str.Split("\t");
            int id = int.Parse(trans[0]);
            string customerName = trans[1];
            string details = trans[2];
            DateTime date = DateTime.Parse(trans[3]);
            decimal amount = decimal.Parse(trans[4]);
            TransactionType type = (TransactionType)Enum.Parse(typeof(TransactionType), trans[5]);
            string acctNo = trans[6];

            return new Transaction(id, customerName, details, date, amount, type, acctNo);
        }
    }
}
