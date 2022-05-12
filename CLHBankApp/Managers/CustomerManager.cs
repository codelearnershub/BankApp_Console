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

            Random random = new Random(11);
            string accountNo = random.Next().ToString();
            NoOfCustomers++;
            
            var customer = new Customer(NoOfCustomers, firstName, lastName, email, password, phone, (Gender)gender, address, dob, Role.Customer, accountNo, pin, AccountTypeName);

            customers.Add(customer);
            Console.WriteLine($"You have successfully created an account with us, your account number is: {accountNo}");
        }
    }
}
