using CLHBankApp.Enums;
using CLHBankApp.Managers.Interfaces;
using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers
{
    public class StaffManager : IStaffManager
    {
        public static List<Staff> staffs;
        string file = @"Files\staffs.txt";
        
        public StaffManager()
        {
            staffs = new List<Staff>();
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
                        var staff = Staff.ToStaff(line);
                        staffs.Add(staff);
                    }
                }
                else
                {
                    var path = "Files";
                    Directory.CreateDirectory(path);
                    var fileName = "staffs.txt";
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

        private void WriteToFile(Staff staff)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(file, true))
                {
                    write.WriteLine(staff.ToString());
                }
            }
            catch (Exception ex)
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
                    foreach (var staff in staffs)
                    {
                        write.WriteLine(staff.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddNewStaff(Staff staff)
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
                int id = staffs.Count == 0 ? 1 : staffs[staffs.Count - 1].Id + 1;
                var newStaff = new Staff(id, firstName, lastName, email, password, phone, (Gender)gender, address, dob, (Role)role);

                staffs.Add(newStaff);
                WriteToFile(newStaff);
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
                AccountTypeManager.Create();
            }
        }

        public Staff Login()
        {
            Console.Write("Enter your email: ");
            var email = Console.ReadLine();
            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            foreach (var staff in staffs)
            {
                if (staff.Email == email && staff.Password == password)
                {
                    return staff;
                }
            }
            return null;
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
                AccountTypeManager.Update(name);
            }
        }

        public void GetAll()
        {
            foreach (var staff in staffs)
            {
                Print(staff);
            }
        }

        public void GetStaffDetails(string staffNo)
        {
            var staff = GetStaff(staffNo);
            if(staff == null)
            {
                Console.WriteLine("Invalid staff number.");
            }
            else
            {
                Print(staff);
            }
        }

        private Staff GetStaff(string staffNo)
        {
            foreach(var staff in staffs)
            {
                if(staff.StaffNo == staffNo)
                {
                    return staff;
                }
            }
            return null;
        }

        private void Print(Staff staff)
        {
            Console.WriteLine($"{staff.Id}\t{staff.FirstName}\t{staff.LastName}\t{staff.Email}");
        }
    }
}
