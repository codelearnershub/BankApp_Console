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

        public Customer(int id, string firstName, string lastName, string email, string password, string phoneNo, Gender gender, string address, DateTime dateOfBirth, Role role, string accountNo, decimal accountBalance, string pin, string accountTypeName) : base(id, firstName, lastName, email, password, phoneNo, gender, address, dateOfBirth, role)
        {
            AccountNo = accountNo;
            AccountBalance = accountBalance;
            Pin = pin;
            AccountTypeName = accountTypeName;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }

        public override string ToString()
        {
            return $"{Id}\t{FirstName}\t{LastName}\t{Email}\t{Password}\t{PhoneNo}\t{Gender}\t{Address}\t{DateOfBirth}\t{Role}\t{AccountNo}\t{Pin}\t{AccountTypeName}\t{AccountBalance}";
        }

        public static Customer ToCustomer(string str)
        {
            var myCust = str.Split("\t");
            var id = int.Parse(myCust[0]);
            string firstName = myCust[1];
            string lastName = myCust[2];
            string email = myCust[3];
            string password = myCust[4];
            string phoneNo = myCust[5];
            Gender gender = (Gender) Enum.Parse(typeof(Gender), myCust[6]);
            string address = myCust[7];
            DateTime dateOfBirth = DateTime.Parse(myCust[8]);
            Role role = (Role)Enum.Parse(typeof(Role), myCust[9]);
            string accountNo = myCust[10];
            string pin = myCust[11];
            string accountTypeName = myCust[12];
            decimal accountBalance = decimal.Parse(myCust[13]);

            return new Customer(id, firstName, lastName, email, password, phoneNo, gender, address, dateOfBirth, role, accountNo, accountBalance, pin, accountTypeName);
        }
    }
}
