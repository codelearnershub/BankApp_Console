using CLHBankApp.Enums;
using CLHBankApp.Managers.Interfaces;
using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers
{
    public class StaffManager : IStaffManager
    {
        public static int NoOfStaffs = 0;        
        public static List<Staff> staffs = new List<Staff>();
        AccountTypeManager accountTypeManager = new AccountTypeManager();

        public void AddNewManger(Staff staff)
        {
            if(staff.Role != Role.Manager)
            {
                Console.WriteLine("Sorry, only Manager can add a new staff.");
            }
            else
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
                while (!int.TryParse(Console.ReadLine(), out gender) && (gender > 0 && gender < 4))
                {
                    Console.Write("Invalid option, please enter 1, 2 or 3: ");
                }
                Console.Write("Enter your date of birth(format: yyyy/mm/dd): ");
                DateTime dob = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter staff role\n1 for Manager\n2 for CustomerCare\n3 for Cashier\n4 for Security: ");
                int role;
                while(!int.TryParse(Console.ReadLine(), out role))
                {
                    Console.Write("Invalid role...\nTry again: ");
                }
                
                NoOfStaffs++;

                var newStaff = new Staff(NoOfStaffs, firstName, lastName, email, password, phone, (Gender)gender, address, dob, (Role)role);

                staffs.Add(newStaff);
                Console.WriteLine($"You have successfully added a new staff with staff number {newStaff.StaffNo}.");
            }
        }

        public void CreateAccountType(Staff staff)
        {
            if (staff.Role != Role.Manager)
            {
                Console.WriteLine("Sorry, only Manager can create account type.");
            }
            else
            {
                accountTypeManager.Create();
            }
        }

        public void UpdateAccountType(Staff staff)
        {
            if (staff.Role != Role.Manager)
            {
                Console.WriteLine("Sorry, only Manager can update account type.");
            }
            else
            {
                Console.Write("Enter name of account type to edit: ");
                var name = Console.ReadLine();
                accountTypeManager.Update(name);
            }
        }
    }
}
