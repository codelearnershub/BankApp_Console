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

        public Staff(int id, string firstName, string lastName, string email, string password, string phoneNo, Gender gender, string address, DateTime dateOfBirth, Role role, string staffNo):base(id, firstName, lastName, email, password, phoneNo, gender, address, dateOfBirth, role)
        {
            //StaffNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper();
            StaffNo = $"ST{id.ToString("000")}";
        }
    }
}
