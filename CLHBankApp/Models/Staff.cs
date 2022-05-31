using CLHBankApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Models
{
    public class Staff:Person
    {
        public string StaffNo { get; set; }

        public Staff(int id, string firstName, string lastName, string email, string password, string phoneNo, Gender gender, string address, DateTime dateOfBirth, Role role):base(id, firstName, lastName, email, password, phoneNo, gender, address, dateOfBirth, role)
        {
            //StaffNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper();
            StaffNo = $"ST{id.ToString("000")}";
        }

        public Staff(int id, string firstName, string lastName, string email, string password, string phoneNo, Gender gender, string address, DateTime dateOfBirth, Role role, string staffNo) : base(id, firstName, lastName, email, password, phoneNo, gender, address, dateOfBirth, role)
        {
            StaffNo = staffNo;
        }

        public override string ToString()
        {
            return $"{Id}\t{FirstName}\t{LastName}\t{Email}\t{Password}\t{PhoneNo}\t{Gender}\t{Address}\t{DateOfBirth}\t{Role}\t{StaffNo}";
        }

        public static Staff ToStaff(string str)
        {
            var myStaff = str.Split("\t");
            var id = int.Parse(myStaff[0]);
            string firstName = myStaff[1];
            string lastName = myStaff[2];
            string email = myStaff[3];
            string password = myStaff[4];
            string phoneNo = myStaff[5];
            Gender gender = (Gender)Enum.Parse(typeof(Gender), myStaff[6]);
            string address = myStaff[7];
            DateTime dateOfBirth = DateTime.Parse(myStaff[8]);
            Role role = (Role)Enum.Parse(typeof(Role), myStaff[9]);
            string staff = myStaff[10];
            

            return new Staff(id, firstName, lastName, email, password, phoneNo, gender, address, dateOfBirth, role, staff);
        }
    }
}
