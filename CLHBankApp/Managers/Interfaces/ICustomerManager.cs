using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers.Interfaces
{
    public interface ICustomerManager
    {
        public void Register(string AccountTypeName);
        public void DepositMoney(Customer customer, bool self);
        public void TransferMoney(Customer customer);
        public void MakeWithdraw(Customer customer);
        public void ListAll();
        public void Print(Customer customer);
        public void GetCustomer(string acctNo);
        public Customer GetCustomerByAccountNo(string accountNo);
        public Customer Login();
    }
}
