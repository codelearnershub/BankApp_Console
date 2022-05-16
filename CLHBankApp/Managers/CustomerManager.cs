using CLHBankApp.Enums;
using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers
{
    public class CustomerManager
    {
        public static int NoOfCustomers = 0;
        AccountTypeManager acctTypeManager = new AccountTypeManager();
        public static List<Customer> customers = new List<Customer>();

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
            if((DateTime.Now.Year - dob.Year) < 18 && AccountTypeName != "StudentAccount")
            {
                AccountTypeName = "StudentAccount";
            }

            Console.Write("Enter your 4 digit unique pin: ");
            string pin = Console.ReadLine();

            string accountNo = GenerateAccNo();
            NoOfCustomers++;
            
            var customer = new Customer(NoOfCustomers, firstName, lastName, email, password, phone, (Gender)gender, address, dob, Role.Customer, accountNo, pin, AccountTypeName);

            customers.Add(customer);
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
                var acctType = acctTypeManager.GetAccountType(customer.AccountTypeName);
                var totalTransfer = amount + acctType.Charges;
                if(customer.AccountBalance < totalTransfer)
                {
                    Console.WriteLine("Sorry, you do have sufficient balance for this transaction.");
                }
                else
                {
                    customer.AccountBalance -= totalTransfer;
                    receiver.AccountBalance += amount;

                    Console.WriteLine($"You successfully transfered {amount} to {receiver.FullName()}");

                    var details = $"successfull transfer of {amount} with {acctType.Charges} charges to {receiver.FullName()}.";
                    TransactionManager.AddNewTransaction(customer.FullName(), details, amount, TransactionType.Transfer, customer.AccountNo);

                    var details2 = $"you receive the amount of {amount} from {customer.FullName()}.";
                    TransactionManager.AddNewTransaction(receiver.FullName(), details, amount, TransactionType.Transfer, receiver.AccountNo);
                }
            }
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

    }
}
