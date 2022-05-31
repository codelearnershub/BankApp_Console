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

        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Charges}\t{MinimumBalance}\t{MaximumWithdraw}";
        }

        public static AccountType ToAccountType(string str)
        {
            var type = str.Split("\t");
            int id = int.Parse(type[0]);
            string name = type[1];
            decimal charges = decimal.Parse(type[2]); 
            decimal minimumBalance = decimal.Parse(type[3]); 
            decimal maximumWithdraw = decimal.Parse(type[4]);

            return new AccountType(id, name, charges, minimumBalance, maximumWithdraw);
        }
    }
}
