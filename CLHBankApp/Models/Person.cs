using CLHBankApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Role Role { get; set; }

        protected Person(int id, string firstName, string lastName, string email, string password, string phoneNo, Gender gender, string address, DateTime dateOfBirth, Role role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNo = phoneNo;
            Gender = gender;
            Address = address;
            DateOfBirth = dateOfBirth;
            Role = role;
        }
    }
}
