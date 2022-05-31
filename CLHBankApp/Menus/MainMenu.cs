using CLHBankApp.Managers;
using CLHBankApp.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Menus
{
    public class MainMenu
    {
        StaffMenu staffMenu;
        CustomerMenu cutomerMenu;
        ICustomerManager customerManager;
        
        public MainMenu()
        {
            customerManager = new CustomerManager();
            staffMenu = new StaffMenu(customerManager);
            cutomerMenu = new CustomerMenu(customerManager);
            TransactionManager trMger = new TransactionManager();
            AccountTypeManager acctMger = new AccountTypeManager();
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
                            staffMenu.Menu();
                            break;
                        case 2:
                            cutomerMenu.Menu();
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
            Console.WriteLine("1.\tStaff Menu.");
            Console.WriteLine("2.\tCustomer Menu.");
            Console.WriteLine("0.\tExit.");
        }
    }
}
