using CLHBankApp.Enums;
using CLHBankApp.Exceptions;
using CLHBankApp.Managers.Interfaces;
using CLHBankApp.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers
{
    public class CustomerManager: ICustomerManager
    {
        string file = @"Files\customers.txt";
        public static List<Customer> customers = new List<Customer>();
        
        public CustomerManager()
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
                        var customer = Customer.ToCustomer(line);
                        customers.Add(customer);
                    }
                }
                else
                {
                    var path = "Files";
                    Directory.CreateDirectory(path);
                    var fileName = "customers.txt";
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

        private void WriteToFile(Customer customer)
        {
            try
            {
                using(StreamWriter write = new StreamWriter(file, true))
                {
                    write.WriteLine(customer.ToString());
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RefreshFile()
        {
            try
            {
                using (StreamWriter write = new StreamWriter(file))
                {
                    foreach (var customer in customers)
                    {
                        write.WriteLine(customer.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Register(string AccountTypeName)
        {
            Console.Write("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            Console.Write("Enter your phone number: ");
            string phone = Console.ReadLine();
            Console.Write("Enter your address: ");
            string address = Console.ReadLine();
            Console.Write("Enter your gender\n1 for Male\n2 for Female\n3 for others: ");
            int gender;
            while(!int.TryParse(Console.ReadLine(), out gender) && (gender > 0 && gender < 4))
            {
                Console.Write("Invalid option, please enter 1, 2 or 3: ");
            }
            Console.Write("Enter your date of birth(format: yyyy/mm/dd): ");
            DateTime dob = DateTime.Parse(Console.ReadLine());
            if((DateTime.Now.Year - dob.Year) < 18 && AccountTypeName != "Student Account")
            {
                AccountTypeName = "Student Account";
            }

            Console.Write("Enter your 4 digit unique pin: ");
            string pin = Console.ReadLine();
            while(pin.Length > 4 || pin.Length < 4)
            {
                Console.WriteLine("Your pin length must be 4.");
                pin = Console.ReadLine();
            }

            string accountNo = GenerateAccNo();
            int id = customers.Count == 0 ? 1 : customers[customers.Count - 1].Id + 1;
            var customer = new Customer(id, firstName, lastName, email, password, phone, (Gender)gender, address, dob, Role.Customer, accountNo, pin, AccountTypeName);

            customers.Add(customer);
            WriteToFile(customer);
            Console.WriteLine($"You have successfully created an account with us, your account number is: {accountNo}");
        }

        public void DepositMoney(Customer customer, bool self)
        {
            decimal amount;
            if (self)
            {
                Console.Write("Enter Amount: ");
                while(!decimal.TryParse(Console.ReadLine(), out amount))
                {
                    Console.WriteLine("Invalid amount\nTry again...");
                }
                customer.AccountBalance += amount;

                var details = $"successfully deposited {amount} into you account.";
                TransactionManager.AddNewTransaction(customer.FullName(), details, amount, TransactionType.Deposit, customer.AccountNo);
                RefreshFile();
                Console.WriteLine($"You have successfully deposited {amount} into you account.");
            }
            else
            {
                Console.Write("Enter account number: ");
                string accountNo = Console.ReadLine();
                while (!ExistByAccNo(accountNo))
                {
                    Console.WriteLine("Account Number you entered does not exist, try again");
                    accountNo = Console.ReadLine();
                }

                Console.Write("Enter Amount: ");
                while (!decimal.TryParse(Console.ReadLine(), out amount))
                {
                    Console.WriteLine("Invalid amount\nTry again...");
                }

                var receiver = GetCustomerByAccountNo(accountNo);
                receiver.AccountBalance += amount;

                var details = $"successfull deposit of {amount} into {receiver.FullName()}.";
                TransactionManager.AddNewTransaction(customer.FullName(), details, amount, TransactionType.Deposit, receiver.AccountNo);
                RefreshFile();
                Console.WriteLine($"You have successfully deposited {amount} into you {receiver.FullName()}.");

            }

        }

        public void TransferMoney(Customer customer)
        {
            decimal amount;
            Console.Write("Enter account number: ");
            string accountNo = Console.ReadLine();
            while (!ExistByAccNo(accountNo))
            {
                Console.WriteLine("Account Number you entered does not exist, try again");
                accountNo = Console.ReadLine();
            }

            Console.Write("Enter Amount: ");
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid amount\nTry again...");
            }

            var receiver = GetCustomerByAccountNo(accountNo);
            Console.Write($"You a sending {amount} to {receiver.FullName()}\nEnter your 4 digit pin to continue: ");
            string pin = Console.ReadLine();
            if(customer.Pin == pin)
            {
                var acctType = AccountTypeManager.GetAccountType(customer.AccountTypeName);
                var totalTransfer = amount + acctType.Charges;
                if(customer.AccountBalance < (totalTransfer + acctType.MinimumBalance))
                {
                    Console.WriteLine("Sorry, you do have sufficient balance for this transaction.");
                }
                else
                {
                    customer.AccountBalance -= totalTransfer;
                    receiver.AccountBalance += amount;
                    RefreshFile();
                    Console.WriteLine($"You successfully transfered {amount} to {receiver.FullName()}");

                    var details = $"successfull transfer of {amount} with {acctType.Charges} charges to {receiver.FullName()}.";
                    TransactionManager.AddNewTransaction(customer.FullName(), details, amount, TransactionType.Transfer, customer.AccountNo);

                    var details2 = $"you receive the amount of {amount} from {customer.FullName()}.";
                    TransactionManager.AddNewTransaction(receiver.FullName(), details, amount, TransactionType.Transfer, receiver.AccountNo);
                }
            }
        }

        public void MakeWithdraw(Customer customer)
        {
            decimal amount;
            Console.Write("Enter Amount: ");
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid amount\nTry again...");
            }
            Console.Write("Please, enter your 4 digit pin to proceed: ");
            var pin = Console.ReadLine();
            if(customer.Pin == pin)
            {
                var acctType = AccountTypeManager.GetAccountType(customer.AccountTypeName);

                if(amount > acctType.MaximumWithdraw)
                {
                    Console.WriteLine($"Customer with {customer.AccountTypeName} type can only withdraw {acctType.MaximumWithdraw} once.");
                    return;
                }

                var totalWithdraw = amount + acctType.Charges;
                if((totalWithdraw + acctType.MinimumBalance) > customer.AccountBalance)
                {
                    Console.WriteLine("Insufficient balance...");
                    return;
                }

                customer.AccountBalance -= totalWithdraw;
                RefreshFile();
                var details = $"Successfull withdrawal of {amount} with {acctType.Charges} charges from your account.";
                TransactionManager.AddNewTransaction(customer.FullName(), details, amount, TransactionType.Withdraw, customer.AccountNo);
                Console.WriteLine(details);
            }
        }

        public void ListAll()
        {
            foreach(var customer in customers)
            {
                Print(customer);
            }
        }

        public Customer Login()
        {
            Console.Write("Enter your email: ");
            var email = Console.ReadLine();
            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            foreach(var customer in customers)
            {
                if (customer.Email == email && customer.Password == password)
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer GetCustomerByAccountNo(string accountNo)
        {
            foreach (var customer in customers)
            {
                if (customer.AccountNo == accountNo)
                {
                    return customer;
                }
            }
            return null;
        }

        public void PrintAcccountBalance(string accountNo)
        {
            var customer = GetCustomerByAccountNo(accountNo);
            if(customer == null)
            {
                throw new NotFoundException($"There is no custoemr with this account number {accountNo}");
            }
            else
            {
                Console.WriteLine(customer.AccountBalance);
            }
        }

        private string GenerateAccNo()
        {
            Random r = new Random();
            return $"00{r.Next(1000, 9999)}{r.Next(1000, 9999)}";
        }

        private bool ExistByAccNo(string accountNo)
        {
            foreach(var customer in customers)
            {
                if(customer.AccountNo == accountNo)
                {
                    return true;
                }
            }
            return false;
        }

        public void Print(Customer customer)
        {
            Console.WriteLine($"{customer.Id}\t{customer.FullName()}\t{customer.AccountNo}\t{customer.AccountTypeName}\t{customer.AccountBalance}\t");
        }

        public void GetCustomer(string acctNo)
        {
            foreach (var customer in customers)
            {
                if (customer.AccountNo == acctNo)
                {
                    Print(customer);
                    return;
                }
            }
            Console.WriteLine($"No customer with the account number {acctNo}.");
        }
    }
}
