using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers
{
    public class AccountTypeManager
    {
        static string file = @"Files\accountTypes.txt";
        static List<AccountType> accountTypes = new List<AccountType>();

        public AccountTypeManager()
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
                        var accountType = AccountType.ToAccountType(line);
                        accountTypes.Add(accountType);
                    }
                }
                else
                {
                    var path = "Files";
                    Directory.CreateDirectory(path);
                    var fileName = "accountTypes.txt";
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

        private static void WriteToFile(AccountType accountType)
        {
            try
            {
                using(StreamWriter write = new StreamWriter(file, true))
                {
                    write.WriteLine(accountType.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RefreshFile()
        {
            try
            {
                using (StreamWriter write = new StreamWriter(file))
                {
                    foreach (var accountType in accountTypes)
                    {
                        write.WriteLine(accountType.ToString());
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Create()
        {
            Console.Write("Enter Account type name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Account type charges: ");
            var charges = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Account type minimum balance: ");
            var minimumBal = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Account type maximum withdrawal: ");
            var maxWithdrawal = decimal.Parse(Console.ReadLine());

            int id = accountTypes.Count == 0 ? 1 : accountTypes[accountTypes.Count - 1].Id + 1;

            var accountType = new AccountType(id, name, charges, minimumBal, maxWithdrawal);
            accountTypes.Add(accountType);
            WriteToFile(accountType);
            Console.WriteLine("Created successfully...");
        }

        public static void Update(string name)
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
                RefreshFile();
                Console.WriteLine("Account type successfully updated...");
            }
        }

        public static AccountType GetAccountType(string name)
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
