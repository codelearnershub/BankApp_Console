using CLHBankApp.Managers;
using CLHBankApp.Managers.Interfaces;
using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Menus
{
    public class CustomerMenu
    {
        ICustomerManager customerManager = new CustomerManager();

        public void Menu()
        {
            var exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintMenu();
                int op;
                if (int.TryParse(Console.ReadLine(), out op))
                {
                    switch (op)
                    {
                        case 1:
                            Console.Write("Choose an account type:\n1 for Savings Account\n2 for Current Account\n3 for Student Account.");
                            var option = int.Parse(Console.ReadLine());
                            switch(option)
                            {
                                case 1:
                                    customerManager.Register("Savings Account");
                                    break;
                                case 2:
                                    customerManager.Register("Current Account");
                                    break;

                                case 3:
                                    customerManager.Register("Student Account");
                                    break;

                                default:
                                    Console.WriteLine("Invalid input...");
                                    break;
                            }HookScreen();
                            
                            break;
                        case 2:
                            var customer = customerManager.Login();
                            if(customer == null)
                            {
                                Console.Write("Invalid email or password...\nPress enter key to try again.");
                                Console.ReadKey();
                            }
                            else
                            {
                                OtherMenu(customer);
                                Console.WriteLine("Thank you for using our app.");
                                HookScreen();
                            }
                            break;
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid inpute...\nPress any key to try again...");
                            Console.ReadKey();
                            break;
                    }
                }

            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("==================================");
            Console.WriteLine("====== Welcome to CLH Bank App ======");
            Console.WriteLine("==================================");
            Console.WriteLine();
            Console.WriteLine("1.\tRegister an account.");
            Console.WriteLine("2.\tLogin.");
            Console.WriteLine("0.\tGo back to main menu.");
        }

        private void PrintOtherMenu()
        {
           
            Console.WriteLine("1.\tDeposit.");
            Console.WriteLine("2.\tTransfer.");
            Console.WriteLine("3.\tWithdraw.");
            Console.WriteLine("4.\tGet all my transactions.");
            Console.WriteLine("5.\tGet account balance.");
            Console.WriteLine("6.\tPrint my details");
            Console.WriteLine("0.\tLogout.");
        }

        public void OtherMenu(Customer customer)
        {
            var exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintOtherMenu();
                int op;
                if (int.TryParse(Console.ReadLine(), out op))
                {
                    switch (op)
                    {
                        case 1:
                            Console.WriteLine("1\tSelf\n2\tDeposite to other person's account. ");
                            var option = int.Parse(Console.ReadLine());
                            switch (option)
                            {
                                case 1:
                                    customerManager.DepositMoney(customer, true);
                                    break;
                                case 2:
                                    customerManager.DepositMoney(customer, false);
                                    break;
                                default:
                                    Console.WriteLine("Invalid input...");
                                    break;
                            }
                            HookScreen();
                            break;
                        case 2:
                            customerManager.TransferMoney(customer);
                            HookScreen();
                            break;
                        case 3:
                            customerManager.MakeWithdraw(customer);
                            HookScreen();
                            break;
                        case 4:
                            TransactionManager.GetAllByCustomer(customer.AccountNo);
                            HookScreen();
                            break;
                        case 5:
                            Console.WriteLine("Your account balance is: "+customer.AccountBalance);
                            HookScreen();
                            break;
                        case 6:
                            customerManager.Print(customer);
                            HookScreen();
                            break;
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid inpute...\nPress any key to try again...");
                            Console.ReadKey();
                            break;
                    }
                }

            }
        }

        private void HookScreen()
        {
            Console.WriteLine("Press enter key to continue...");
            Console.ReadKey();
        }

    }
}
