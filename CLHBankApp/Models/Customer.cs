using CLHBankApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Models
{
    public class Customer:Person
    {
        public string AccountNo { get; set; }
        public decimal AccountBalance { get; set; }
        public string Pin { get; set; }
        public string AccountTypeName { get; set; }

        public Customer(int id, string firstName, string lastName, string email, string password, string phoneNo, Gender gender, string address, DateTime dateOfBirth, Role role, string acctNo, string pin, string acctType) : base(id, firstName, lastName, email, password, phoneNo, gender, address, dateOfBirth, role)
        {
            AccountNo = acctNo;
            Pin = pin;
            AccountTypeName = acctType;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
