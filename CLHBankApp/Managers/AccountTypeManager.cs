using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers
{
    public class AccountTypeManager
    {
        public static int NoOfAccountType = 0;
        public List<AccountType> accountTypes = new List<AccountType>();

        public void Create()
        {
            Console.Write("Enter Account type name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Account type charges: ");
            var charges = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Account type minimum balance: ");
            var minimumBal = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Account type maximum withdrawal: ");
            var maxWithdrawal = decimal.Parse(Console.ReadLine());
            NoOfAccountType++;
            var accountType = new AccountType(NoOfAccountType, name, charges, minimumBal, maxWithdrawal);
            accountTypes.Add(accountType);
            Console.WriteLine("Created successfully...");
        }

        public void Update(string name)
        {
            AccountType accountType = GetAccountType(name);

            if(accountType == null)
            {
                Console.WriteLine($"Account Type with the name {name} does not exist.");
            }
            else
            {
                Console.Write("Enter Account type charges: ");
                accountType.Charges = decimal.Parse(Console.ReadLine());
                Console.Write("Enter Account type minimum balance: ");
                accountType.MinimumBalance = decimal.Parse(Console.ReadLine());
                Console.Write("Enter Account type maximum withdrawal: ");
                accountType.MaximumWithdraw = decimal.Parse(Console.ReadLine());
                
                Console.WriteLine("Account type successfully updated...");
            }
        }

        public AccountType GetAccountType(string name)
        {
            foreach(var accountType in accountTypes)
            {
                if(accountType.Name == name)
                {
                    return accountType;
                }
            }
            return null;
        }
    }
}
