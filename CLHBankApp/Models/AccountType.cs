using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Models
{
    public class AccountType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Charges { get; set; }
        public decimal MinimumBalance { get; set; }
        public decimal MaximumWithdraw { get; set; }

        public AccountType(int id, string name, decimal charges, decimal minimumBalance, decimal maximumWithdraw)
        {
            Id = id;
            Name = name;
            Charges = charges;
            MinimumBalance = minimumBalance;
            MaximumWithdraw = maximumWithdraw;
        }
    }
}
