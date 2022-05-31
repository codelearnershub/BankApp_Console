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
    public class StaffMenu
    {
        IStaffManager staffManager;
        ICustomerManager customerManager;

        public StaffMenu(ICustomerManager _customerManager)
        {
            staffManager = new StaffManager();
            customerManager = _customerManager;
        }

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
                            var staff = staffManager.Login();
                            if (staff == null)
                            {
                                Console.Write("Invalid email or password...\nPress enter key to try again.");
                                Console.ReadKey();
                            }
                            else
                            {
                                OtherMenu(staff);
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
            Console.WriteLine("1.\tLogin.");
            Console.WriteLine("0.\tGo back to main menu.");
        }

        private void PrintOtherMenu()
        {

            Console.WriteLine("1.\tAdd new account type.");
            Console.WriteLine("2.\tEdit account type.");
            Console.WriteLine("3.\tAdd new staff.");
            Console.WriteLine("4.\tGet all transactions.");
            Console.WriteLine("5.\tGet all transactions of customer.");
            Console.WriteLine("6.\tGet all transactions in a date.");
            Console.WriteLine("7.\tGet all transactions of customer in a date.");
            Console.WriteLine("8.\tGet account balance of a customer.");
            Console.WriteLine("9.\tPrint details of a customer.");
            Console.WriteLine("10.\tGet all customers.");
            Console.WriteLine("11.\tGet all staff.");
            Console.WriteLine("0.\tLogout.");
        }

        public void OtherMenu(Staff staff)
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
                            staffManager.CreateAccountType(staff);
                            HookScreen();
                            break;
                        case 2:
                            staffManager.UpdateAccountType(staff);
                            HookScreen();
                            break;
                        case 3:
                            staffManager.AddNewStaff(staff);
                            HookScreen();
                            break;
                        case 4:
                            TransactionManager.GetAll();
                            HookScreen();
                            break;
                        case 5:
                            Console.Write("Enter the customer account number: ");
                            var accountNo = Console.ReadLine();
                            TransactionManager.GetAllByCustomer(accountNo);
                            HookScreen();
                            break;
                        case 6:
                            Console.Write("Enter the date(yyyy/mm/dd): ");
                            DateTime date;
                            while(!DateTime.TryParse(Console.ReadLine(), out date))
                            {
                                Console.WriteLine("Invalid format, try again...");
                            }
                            TransactionManager.GetAllByDate(date);
                            HookScreen();
                            break;
                        case 7:
                            Console.Write("Enter the customer account number: ");
                            var _accountNo = Console.ReadLine();
                            Console.Write("Enter the date(yyyy/mm/dd): ");
                            DateTime _date;
                            while (!DateTime.TryParse(Console.ReadLine(), out _date))
                            {
                                Console.WriteLine("Invalid format, try again...");
                            }
                            TransactionManager.GetAllByCustomerOnDate(_accountNo, _date);
                            HookScreen();
                            break;
                        case 8:
                            Console.Write("Enter the customer account number: ");
                            var _acctNo = Console.ReadLine();
                            var cust = customerManager.GetCustomerByAccountNo(_acctNo);
                            if(cust != null)
                            {
                                Console.WriteLine($"The account balance of the customer is: {cust.AccountBalance}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid account number");
                            }
                            HookScreen();
                            break;
                        case 9:
                            Console.Write("Enter the customer account number: ");
                            var acctNo = Console.ReadLine();
                            customerManager.GetCustomer(acctNo);
                            HookScreen();
                            break;
                        case 10:
                            customerManager.ListAll();
                            HookScreen();
                            break;
                        case 11:
                            staffManager.GetAll();
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
