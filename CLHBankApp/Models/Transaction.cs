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
    }
}
